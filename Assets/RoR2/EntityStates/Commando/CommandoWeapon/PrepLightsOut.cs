using System;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F4 RID: 1012
	public class PrepLightsOut : BaseState
	{
		// Token: 0x06001235 RID: 4661 RVA: 0x000510F0 File Offset: 0x0004F2F0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepLightsOut.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "PrepRevolver", "PrepRevolver.playbackRate", this.duration);
			base.PlayAnimation("Gesture, Override", "PrepRevolver", "PrepRevolver.playbackRate", this.duration);
			Util.PlaySound(PrepLightsOut.prepSoundString, base.gameObject);
			if (PrepLightsOut.specialCrosshairPrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, PrepLightsOut.specialCrosshairPrefab, CrosshairUtils.OverridePriority.Skill);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				if (this.childLocator)
				{
					Transform transform = this.childLocator.FindChild("MuzzlePistol");
					if (transform && PrepLightsOut.chargePrefab)
					{
						this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(PrepLightsOut.chargePrefab, transform.position, transform.rotation);
						this.chargeEffect.transform.parent = transform;
						ScaleParticleSystemDuration component = this.chargeEffect.GetComponent<ScaleParticleSystemDuration>();
						if (component)
						{
							component.newDuration = this.duration;
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0005123A File Offset: 0x0004F43A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireLightsOut());
				return;
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00051269 File Offset: 0x0004F469
		public override void OnExit()
		{
			EntityState.Destroy(this.chargeEffect);
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001757 RID: 5975
		public static float baseDuration = 3f;

		// Token: 0x04001758 RID: 5976
		public static GameObject chargePrefab;

		// Token: 0x04001759 RID: 5977
		public static GameObject specialCrosshairPrefab;

		// Token: 0x0400175A RID: 5978
		public static string prepSoundString;

		// Token: 0x0400175B RID: 5979
		private GameObject chargeEffect;

		// Token: 0x0400175C RID: 5980
		private float duration;

		// Token: 0x0400175D RID: 5981
		private ChildLocator childLocator;

		// Token: 0x0400175E RID: 5982
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
	}
}
