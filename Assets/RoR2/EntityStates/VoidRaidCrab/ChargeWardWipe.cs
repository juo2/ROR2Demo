using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000116 RID: 278
	public class ChargeWardWipe : BaseWardWipeState
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x000150B8 File Offset: 0x000132B8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator && this.chargeEffectPrefab)
			{
				Transform transform = modelChildLocator.FindChild(this.muzzleName) ?? base.characterBody.coreTransform;
				if (transform)
				{
					this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
					this.chargeEffectInstance.transform.parent = transform;
					ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = this.duration;
					}
				}
			}
			this.fogDamageController = base.GetComponent<FogDamageController>();
			this.fogDamageController.enabled = true;
			this.safeWards = new List<GameObject>();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000151AC File Offset: 0x000133AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.safeWardSpawnCard)
			{
				float time = base.fixedAge / this.duration;
				float f = this.safeWardSpawnCurve.Evaluate(time);
				DirectorPlacementRule directorPlacementRule = null;
				while ((float)this.safeWards.Count < Mathf.Floor(f))
				{
					if (directorPlacementRule == null)
					{
						directorPlacementRule = new DirectorPlacementRule
						{
							placementMode = DirectorPlacementRule.PlacementMode.Approximate,
							maxDistance = this.maxDistanceToInitialWard,
							minDistance = 0f,
							spawnOnTarget = base.gameObject.transform,
							preventOverhead = true
						};
					}
					if (this.safeWards.Count > 0)
					{
						directorPlacementRule.maxDistance = this.maxDistanceBetweenConsecutiveWards;
						directorPlacementRule.minDistance = this.minDistanceBetweenConsecutiveWards;
						directorPlacementRule.spawnOnTarget = this.safeWards[this.safeWards.Count - 1].transform;
						directorPlacementRule.placementMode = DirectorPlacementRule.PlacementMode.Approximate;
					}
					GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.safeWardSpawnCard, directorPlacementRule, Run.instance.stageRng));
					if (gameObject)
					{
						NetworkServer.Spawn(gameObject);
						if (this.fogDamageController)
						{
							IZone component = gameObject.GetComponent<IZone>();
							this.fogDamageController.AddSafeZone(component);
						}
						this.safeWards.Add(gameObject);
					}
					else
					{
						Debug.LogError("Unable to spawn safe ward instance.  Are there any ground nodes?");
					}
				}
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				FireWardWipe nextState = new FireWardWipe();
				this.outer.SetNextState(nextState);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00015334 File Offset: 0x00013534
		public override void OnExit()
		{
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04000598 RID: 1432
		[SerializeField]
		public float duration;

		// Token: 0x04000599 RID: 1433
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400059A RID: 1434
		[SerializeField]
		public string muzzleName;

		// Token: 0x0400059B RID: 1435
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400059C RID: 1436
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400059D RID: 1437
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400059E RID: 1438
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400059F RID: 1439
		[SerializeField]
		public InteractableSpawnCard safeWardSpawnCard;

		// Token: 0x040005A0 RID: 1440
		[SerializeField]
		public AnimationCurve safeWardSpawnCurve;

		// Token: 0x040005A1 RID: 1441
		[SerializeField]
		public float minDistanceBetweenConsecutiveWards;

		// Token: 0x040005A2 RID: 1442
		[SerializeField]
		public float maxDistanceBetweenConsecutiveWards;

		// Token: 0x040005A3 RID: 1443
		[SerializeField]
		public float maxDistanceToInitialWard;

		// Token: 0x040005A4 RID: 1444
		private GameObject chargeEffectInstance;
	}
}
