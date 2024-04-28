using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando
{
	// Token: 0x020003E6 RID: 998
	public class SlideState : BaseState
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0004F304 File Offset: 0x0004D504
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SlideState.soundString, base.gameObject);
			if (base.inputBank && base.characterDirection)
			{
				base.characterDirection.forward = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
			}
			if (base.characterMotor)
			{
				this.startedStateGrounded = base.characterMotor.isGrounded;
			}
			if (SlideState.jetEffectPrefab)
			{
				Transform transform = base.FindModelChild("LeftJet");
				Transform transform2 = base.FindModelChild("RightJet");
				if (transform)
				{
					UnityEngine.Object.Instantiate<GameObject>(SlideState.jetEffectPrefab, transform);
				}
				if (transform2)
				{
					UnityEngine.Object.Instantiate<GameObject>(SlideState.jetEffectPrefab, transform2);
				}
			}
			base.characterBody.SetSpreadBloom(0f, false);
			if (!this.startedStateGrounded)
			{
				this.PlayAnimation("Body", "Jump");
				Vector3 velocity = base.characterMotor.velocity;
				velocity.y = base.characterBody.jumpPower;
				base.characterMotor.velocity = velocity;
				return;
			}
			base.PlayAnimation("Body", "SlideForward", "SlideForward.playbackRate", SlideState.slideDuration);
			if (SlideState.slideEffectPrefab)
			{
				Transform parent = base.FindModelChild("Base");
				this.slideEffectInstance = UnityEngine.Object.Instantiate<GameObject>(SlideState.slideEffectPrefab, parent);
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004F488 File Offset: 0x0004D688
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				float num = this.startedStateGrounded ? SlideState.slideDuration : SlideState.jumpDuration;
				if (base.inputBank && base.characterDirection)
				{
					base.characterDirection.moveVector = base.inputBank.moveVector;
					this.forwardDirection = base.characterDirection.forward;
				}
				if (base.characterMotor)
				{
					float num2;
					if (this.startedStateGrounded)
					{
						num2 = SlideState.forwardSpeedCoefficientCurve.Evaluate(base.fixedAge / num);
					}
					else
					{
						num2 = SlideState.jumpforwardSpeedCoefficientCurve.Evaluate(base.fixedAge / num);
					}
					base.characterMotor.rootMotion += num2 * this.moveSpeedStat * this.forwardDirection * Time.fixedDeltaTime;
				}
				if (base.fixedAge >= num)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004F586 File Offset: 0x0004D786
		public override void OnExit()
		{
			this.PlayImpactAnimation();
			if (this.slideEffectInstance)
			{
				EntityState.Destroy(this.slideEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004F5AC File Offset: 0x0004D7AC
		private void PlayImpactAnimation()
		{
			Animator modelAnimator = base.GetModelAnimator();
			int layerIndex = modelAnimator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				modelAnimator.SetLayerWeight(layerIndex, 1f);
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040016B9 RID: 5817
		public static float slideDuration;

		// Token: 0x040016BA RID: 5818
		public static float jumpDuration;

		// Token: 0x040016BB RID: 5819
		public static AnimationCurve forwardSpeedCoefficientCurve;

		// Token: 0x040016BC RID: 5820
		public static AnimationCurve jumpforwardSpeedCoefficientCurve;

		// Token: 0x040016BD RID: 5821
		public static string soundString;

		// Token: 0x040016BE RID: 5822
		public static GameObject jetEffectPrefab;

		// Token: 0x040016BF RID: 5823
		public static GameObject slideEffectPrefab;

		// Token: 0x040016C0 RID: 5824
		private Vector3 forwardDirection;

		// Token: 0x040016C1 RID: 5825
		private GameObject slideEffectInstance;

		// Token: 0x040016C2 RID: 5826
		private bool startedStateGrounded;
	}
}
