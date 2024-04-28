using System;
using System.Collections.Generic;
using RoR2.Audio;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000714 RID: 1812
	[RequireComponent(typeof(TeamFilter))]
	[RequireComponent(typeof(GenericOwnership))]
	public class GrandParentSunController : MonoBehaviour
	{
		// Token: 0x06002564 RID: 9572 RVA: 0x000A1AE1 File Offset: 0x0009FCE1
		private void Awake()
		{
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.ownership = base.GetComponent<GenericOwnership>();
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000A1AFB File Offset: 0x0009FCFB
		private void Start()
		{
			if (this.activeLoopDef)
			{
				Util.PlaySound(this.activeLoopDef.startSoundName, base.gameObject);
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000A1B24 File Offset: 0x0009FD24
		private void OnDestroy()
		{
			if (this.activeLoopDef)
			{
				Util.PlaySound(this.activeLoopDef.stopSoundName, base.gameObject);
			}
			if (this.damageLoopDef)
			{
				Util.PlaySound(this.damageLoopDef.stopSoundName, base.gameObject);
			}
			if (this.stopSoundName != null)
			{
				Util.PlaySound(this.stopSoundName, base.gameObject);
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000A1B94 File Offset: 0x0009FD94
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.ServerFixedUpdate();
			}
			if (this.damageLoopDef)
			{
				bool flag = this.isLocalPlayerDamaged;
				this.isLocalPlayerDamaged = false;
				foreach (HurtBox hurtBox in this.cycleTargets)
				{
					CharacterBody characterBody = null;
					if (hurtBox && hurtBox.healthComponent)
					{
						characterBody = hurtBox.healthComponent.body;
					}
					if (characterBody && (characterBody.bodyFlags & CharacterBody.BodyFlags.OverheatImmune) != CharacterBody.BodyFlags.None && characterBody.hasEffectiveAuthority)
					{
						Vector3 position = base.transform.position;
						Vector3 corePosition = characterBody.corePosition;
						RaycastHit raycastHit;
						if (!Physics.Linecast(position, corePosition, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
						{
							this.isLocalPlayerDamaged = true;
						}
					}
				}
				if (this.isLocalPlayerDamaged && !flag)
				{
					Util.PlaySound(this.damageLoopDef.startSoundName, base.gameObject);
					return;
				}
				if (!this.isLocalPlayerDamaged && flag)
				{
					Util.PlaySound(this.damageLoopDef.stopSoundName, base.gameObject);
				}
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000A1CD4 File Offset: 0x0009FED4
		private void ServerFixedUpdate()
		{
			float num = Mathf.Clamp01(this.previousCycle.timeSince / this.cycleInterval);
			int num2 = (num == 1f) ? this.cycleTargets.Count : Mathf.FloorToInt((float)this.cycleTargets.Count * num);
			Vector3 position = base.transform.position;
			while (this.cycleIndex < num2)
			{
				HurtBox hurtBox = this.cycleTargets[this.cycleIndex];
				if (hurtBox && hurtBox.healthComponent)
				{
					CharacterBody body = hurtBox.healthComponent.body;
					if ((body.bodyFlags & CharacterBody.BodyFlags.OverheatImmune) == CharacterBody.BodyFlags.None)
					{
						Vector3 corePosition = body.corePosition;
						Ray ray = new Ray(position, corePosition - position);
						RaycastHit raycastHit;
						if (!Physics.Linecast(position, corePosition, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
						{
							float num3 = Mathf.Max(1f, raycastHit.distance);
							body.AddTimedBuff(this.buffDef, this.nearBuffDuration / num3);
							if (this.buffApplyEffect)
							{
								EffectData effectData = new EffectData
								{
									origin = corePosition,
									rotation = Util.QuaternionSafeLookRotation(-ray.direction),
									scale = body.bestFitRadius
								};
								effectData.SetHurtBoxReference(hurtBox);
								EffectManager.SpawnEffect(this.buffApplyEffect, effectData, true);
							}
							int num4 = body.GetBuffCount(this.buffDef) - this.minimumStacksBeforeApplyingBurns;
							if (num4 > 0)
							{
								InflictDotInfo inflictDotInfo = default(InflictDotInfo);
								inflictDotInfo.dotIndex = DotController.DotIndex.Burn;
								inflictDotInfo.attackerObject = this.ownership.ownerObject;
								inflictDotInfo.victimObject = body.gameObject;
								inflictDotInfo.damageMultiplier = 1f;
								GenericOwnership genericOwnership = this.ownership;
								CharacterBody characterBody;
								if (genericOwnership == null)
								{
									characterBody = null;
								}
								else
								{
									GameObject ownerObject = genericOwnership.ownerObject;
									characterBody = ((ownerObject != null) ? ownerObject.GetComponent<CharacterBody>() : null);
								}
								CharacterBody characterBody2 = characterBody;
								if (characterBody2 && characterBody2.inventory)
								{
									inflictDotInfo.totalDamage = new float?(0.5f * characterBody2.damage * this.burnDuration * (float)num4);
									StrengthenBurnUtils.CheckDotForUpgrade(characterBody2.inventory, ref inflictDotInfo);
								}
								DotController.InflictDot(ref inflictDotInfo);
							}
						}
					}
				}
				this.cycleIndex++;
			}
			if (this.previousCycle.timeSince >= this.cycleInterval)
			{
				this.previousCycle = Run.FixedTimeStamp.now;
				this.cycleIndex = 0;
				this.cycleTargets.Clear();
				this.SearchForTargets(this.cycleTargets);
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000A1F5C File Offset: 0x000A015C
		private void SearchForTargets(List<HurtBox> dest)
		{
			this.bullseyeSearch.searchOrigin = base.transform.position;
			this.bullseyeSearch.minAngleFilter = 0f;
			this.bullseyeSearch.maxAngleFilter = 180f;
			this.bullseyeSearch.maxDistanceFilter = this.maxDistance;
			this.bullseyeSearch.filterByDistinctEntity = true;
			this.bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			this.bullseyeSearch.viewer = null;
			this.bullseyeSearch.RefreshCandidates();
			dest.AddRange(this.bullseyeSearch.GetResults());
		}

		// Token: 0x04002938 RID: 10552
		private TeamFilter teamFilter;

		// Token: 0x04002939 RID: 10553
		private GenericOwnership ownership;

		// Token: 0x0400293A RID: 10554
		public BuffDef buffDef;

		// Token: 0x0400293B RID: 10555
		[Min(0.001f)]
		public float cycleInterval = 1f;

		// Token: 0x0400293C RID: 10556
		[Min(0.001f)]
		public float nearBuffDuration = 1f;

		// Token: 0x0400293D RID: 10557
		[Min(0.001f)]
		public float maxDistance = 1f;

		// Token: 0x0400293E RID: 10558
		public int minimumStacksBeforeApplyingBurns = 4;

		// Token: 0x0400293F RID: 10559
		public float burnDuration = 5f;

		// Token: 0x04002940 RID: 10560
		public GameObject buffApplyEffect;

		// Token: 0x04002941 RID: 10561
		[SerializeField]
		private LoopSoundDef activeLoopDef;

		// Token: 0x04002942 RID: 10562
		[SerializeField]
		private LoopSoundDef damageLoopDef;

		// Token: 0x04002943 RID: 10563
		[SerializeField]
		private string stopSoundName;

		// Token: 0x04002944 RID: 10564
		private Run.FixedTimeStamp previousCycle = Run.FixedTimeStamp.negativeInfinity;

		// Token: 0x04002945 RID: 10565
		private int cycleIndex;

		// Token: 0x04002946 RID: 10566
		private List<HurtBox> cycleTargets = new List<HurtBox>();

		// Token: 0x04002947 RID: 10567
		private BullseyeSearch bullseyeSearch = new BullseyeSearch();

		// Token: 0x04002948 RID: 10568
		private bool isLocalPlayerDamaged;
	}
}
