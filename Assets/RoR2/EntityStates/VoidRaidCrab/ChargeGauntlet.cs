using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000115 RID: 277
	public class ChargeGauntlet : BaseState
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x00014F3C File Offset: 0x0001313C
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
			PhasedInventorySetter component2 = base.GetComponent<PhasedInventorySetter>();
			if (component2 && NetworkServer.active)
			{
				component2.AdvancePhase();
			}
			if (this.nextSkillDef)
			{
				GenericSkill genericSkill = base.skillLocator.FindSkillByDef(this.skillDefToReplaceAtStocksEmpty);
				if (genericSkill && genericSkill.stock == 0)
				{
					genericSkill.SetBaseSkill(this.nextSkillDef);
				}
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015069 File Offset: 0x00013269
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				VoidRaidGauntletController.instance;
				this.outer.SetNextState(new ChannelGauntlet());
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000150A2 File Offset: 0x000132A2
		public override void OnExit()
		{
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x0400058E RID: 1422
		[SerializeField]
		public float duration;

		// Token: 0x0400058F RID: 1423
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04000590 RID: 1424
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000591 RID: 1425
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000592 RID: 1426
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000593 RID: 1427
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000594 RID: 1428
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000595 RID: 1429
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x04000596 RID: 1430
		[SerializeField]
		public SkillDef nextSkillDef;

		// Token: 0x04000597 RID: 1431
		private GameObject chargeEffectInstance;
	}
}
