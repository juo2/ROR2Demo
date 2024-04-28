using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000114 RID: 276
	public class ChargeFinalStand : BaseWardWipeState
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00014DCC File Offset: 0x00012FCC
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
			PhasedInventorySetter component2 = base.GetComponent<PhasedInventorySetter>();
			if (component2 && NetworkServer.active)
			{
				component2.AdvancePhase();
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00014EE0 File Offset: 0x000130E0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				FireFinalStand nextState = new FireFinalStand();
				this.outer.SetNextState(nextState);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00014F1B File Offset: 0x0001311B
		public override void OnExit()
		{
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04000586 RID: 1414
		[SerializeField]
		public float duration;

		// Token: 0x04000587 RID: 1415
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04000588 RID: 1416
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000589 RID: 1417
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400058A RID: 1418
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400058B RID: 1419
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400058C RID: 1420
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400058D RID: 1421
		private GameObject chargeEffectInstance;
	}
}
