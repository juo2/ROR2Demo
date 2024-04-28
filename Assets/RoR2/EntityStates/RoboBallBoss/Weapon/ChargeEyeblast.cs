using System;
using RoR2;
using UnityEngine;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001E5 RID: 485
	public class ChargeEyeblast : BaseState
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x000249A4 File Offset: 0x00022BA4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeEyeblast.baseDuration / this.attackSpeedStat;
			UnityEngine.Object modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(ChargeEyeblast.attackString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(ChargeEyeblast.muzzleString);
					if (transform && ChargeEyeblast.chargeEffectPrefab)
					{
						this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeEyeblast.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeInstance.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
				}
			}
			if (modelAnimator)
			{
				base.PlayCrossfade("Gesture, Additive", "ChargeEyeBlast", "ChargeEyeBlast.playbackRate", this.duration, 0.1f);
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00024A98 File Offset: 0x00022C98
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00024AB8 File Offset: 0x00022CB8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(this.GetNextState());
				return;
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00024AE8 File Offset: 0x00022CE8
		public virtual EntityState GetNextState()
		{
			return new FireEyeBlast();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000A32 RID: 2610
		public static float baseDuration = 1f;

		// Token: 0x04000A33 RID: 2611
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000A34 RID: 2612
		public static string attackString;

		// Token: 0x04000A35 RID: 2613
		public static string muzzleString;

		// Token: 0x04000A36 RID: 2614
		private float duration;

		// Token: 0x04000A37 RID: 2615
		private GameObject chargeInstance;
	}
}
