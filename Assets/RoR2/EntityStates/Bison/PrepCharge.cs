using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bison
{
	// Token: 0x02000457 RID: 1111
	public class PrepCharge : BaseState
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x00058730 File Offset: 0x00056930
		public override void OnEnter()
		{
			base.OnEnter();
			this.prepDuration = PrepCharge.basePrepDuration / this.attackSpeedStat;
			base.characterBody.SetAimTimer(this.prepDuration);
			base.PlayCrossfade("Body", "PrepCharge", "PrepCharge.playbackRate", this.prepDuration, 0.2f);
			Util.PlaySound(PrepCharge.enterSoundString, base.gameObject);
			Transform modelTransform = base.GetModelTransform();
			AimAnimator component = modelTransform.GetComponent<AimAnimator>();
			if (component)
			{
				component.enabled = true;
			}
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (modelTransform)
			{
				ChildLocator component2 = modelTransform.GetComponent<ChildLocator>();
				if (component2)
				{
					Transform transform = component2.FindChild("ChargeIndicator");
					if (transform && PrepCharge.chargeEffectPrefab)
					{
						this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(PrepCharge.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeEffectInstance.transform.parent = transform;
						ScaleParticleSystemDuration component3 = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component3)
						{
							component3.newDuration = this.prepDuration;
						}
					}
				}
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00058869 File Offset: 0x00056A69
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0005888C File Offset: 0x00056A8C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch > this.prepDuration && base.isAuthority)
			{
				this.outer.SetNextState(new Charge());
				return;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001974 RID: 6516
		public static float basePrepDuration;

		// Token: 0x04001975 RID: 6517
		public static string enterSoundString;

		// Token: 0x04001976 RID: 6518
		public static GameObject chargeEffectPrefab;

		// Token: 0x04001977 RID: 6519
		private float stopwatch;

		// Token: 0x04001978 RID: 6520
		private float prepDuration;

		// Token: 0x04001979 RID: 6521
		private GameObject chargeEffectInstance;
	}
}
