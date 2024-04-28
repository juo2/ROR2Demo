using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MajorConstruct
{
	// Token: 0x0200028B RID: 651
	public class Spawn : BaseState
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
			MasterSpawnSlotController component = base.GetComponent<MasterSpawnSlotController>();
			if (NetworkServer.active && this.padPrefab && component)
			{
				RaycastHit raycastHit = default(RaycastHit);
				for (int i = 0; i < this.numPads; i++)
				{
					Quaternion quaternion = Quaternion.Euler(0f, 360f * ((float)i / (float)this.numPads), 0f);
					Vector3 vector = base.characterBody.corePosition + quaternion * Vector3.forward * this.padRingRadius;
					Vector3 origin = vector + Vector3.up * this.maxRaycastDistance;
					if (Physics.Raycast(vector, Vector3.up, out raycastHit, this.maxRaycastDistance, LayerIndex.world.mask))
					{
						origin = raycastHit.point;
					}
					if (Physics.Raycast(origin, Vector3.down, out raycastHit, this.maxRaycastDistance * 2f, LayerIndex.world.mask) && Vector3.Distance(base.characterBody.corePosition, raycastHit.point) < this.maxPadDistance)
					{
						if (this.alignPadsToNormal)
						{
							quaternion = Quaternion.FromToRotation(Vector3.up, raycastHit.normal) * quaternion;
						}
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.padPrefab, raycastHit.point, quaternion);
						NetworkedBodySpawnSlot component2 = gameObject.GetComponent<NetworkedBodySpawnSlot>();
						if (component2)
						{
							component.slots.Add(component2);
						}
						NetworkServer.Spawn(gameObject);
						if (this.padEffectPrefab)
						{
							EffectData effectData = new EffectData
							{
								origin = raycastHit.point,
								rotation = quaternion
							};
							EffectManager.SpawnEffect(this.padEffectPrefab, effectData, true);
						}
					}
				}
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000300C4 File Offset: 0x0002E2C4
		private void CheckForDepleteStocks(SkillSlot slot, bool deplete)
		{
			GenericSkill skill = base.skillLocator.GetSkill(slot);
			if (deplete && skill)
			{
				skill.RemoveAllStocks();
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000300EF File Offset: 0x0002E2EF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00030118 File Offset: 0x0002E318
		public override void OnExit()
		{
			if (base.isAuthority)
			{
				this.CheckForDepleteStocks(SkillSlot.Primary, this.depleteStocksPrimary);
				this.CheckForDepleteStocks(SkillSlot.Secondary, this.depleteStocksSecondary);
				this.CheckForDepleteStocks(SkillSlot.Utility, this.depleteStocksUtility);
				this.CheckForDepleteStocks(SkillSlot.Special, this.depleteStocksSpecial);
			}
			base.OnExit();
		}

		// Token: 0x04000D8A RID: 3466
		[SerializeField]
		public float duration;

		// Token: 0x04000D8B RID: 3467
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000D8C RID: 3468
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000D8D RID: 3469
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000D8E RID: 3470
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000D8F RID: 3471
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000D90 RID: 3472
		[SerializeField]
		public int numPads;

		// Token: 0x04000D91 RID: 3473
		[SerializeField]
		public float padRingRadius;

		// Token: 0x04000D92 RID: 3474
		[SerializeField]
		public float maxRaycastDistance;

		// Token: 0x04000D93 RID: 3475
		[SerializeField]
		public GameObject padPrefab;

		// Token: 0x04000D94 RID: 3476
		[SerializeField]
		public float maxPadDistance;

		// Token: 0x04000D95 RID: 3477
		[SerializeField]
		public GameObject padEffectPrefab;

		// Token: 0x04000D96 RID: 3478
		[SerializeField]
		public bool alignPadsToNormal;

		// Token: 0x04000D97 RID: 3479
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000D98 RID: 3480
		[SerializeField]
		public bool depleteStocksPrimary;

		// Token: 0x04000D99 RID: 3481
		[SerializeField]
		public bool depleteStocksSecondary;

		// Token: 0x04000D9A RID: 3482
		[SerializeField]
		public bool depleteStocksUtility;

		// Token: 0x04000D9B RID: 3483
		[SerializeField]
		public bool depleteStocksSpecial;
	}
}
