using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200043A RID: 1082
	public class BaseSlideState : BaseState
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x000568DC File Offset: 0x00054ADC
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(BaseSlideState.soundString, base.gameObject);
			if (base.inputBank)
			{
				base.characterDirection;
			}
			if (NetworkServer.active)
			{
				Util.CleanseBody(base.characterBody, true, false, false, false, false, false);
			}
			if (BaseSlideState.slideEffectPrefab && base.characterBody)
			{
				Vector3 position = base.characterBody.corePosition;
				Quaternion rotation = Quaternion.identity;
				Transform transform = base.FindModelChild(BaseSlideState.slideEffectMuzzlestring);
				if (transform)
				{
					position = transform.position;
				}
				if (base.characterDirection)
				{
					rotation = Util.QuaternionSafeLookRotation(this.slideRotation * base.characterDirection.forward, Vector3.up);
				}
				EffectManager.SimpleEffect(BaseSlideState.slideEffectPrefab, position, rotation, false);
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000569B4 File Offset: 0x00054BB4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				Vector3 a = Vector3.zero;
				if (base.inputBank && base.characterDirection)
				{
					a = base.characterDirection.forward;
				}
				if (base.characterMotor)
				{
					float num = BaseSlideState.speedCoefficientCurve.Evaluate(base.fixedAge / BaseSlideState.duration);
					base.characterMotor.rootMotion += this.slideRotation * (num * this.moveSpeedStat * a * Time.fixedDeltaTime);
				}
				if (base.fixedAge >= BaseSlideState.duration)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00056A73 File Offset: 0x00054C73
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				this.PlayImpactAnimation();
			}
			base.OnExit();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000026ED File Offset: 0x000008ED
		private void PlayImpactAnimation()
		{
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040018E9 RID: 6377
		public static float duration;

		// Token: 0x040018EA RID: 6378
		public static AnimationCurve speedCoefficientCurve;

		// Token: 0x040018EB RID: 6379
		public static AnimationCurve jumpforwardSpeedCoefficientCurve;

		// Token: 0x040018EC RID: 6380
		public static string soundString;

		// Token: 0x040018ED RID: 6381
		public static GameObject slideEffectPrefab;

		// Token: 0x040018EE RID: 6382
		public static string slideEffectMuzzlestring;

		// Token: 0x040018EF RID: 6383
		protected Vector3 slideVector;

		// Token: 0x040018F0 RID: 6384
		protected Quaternion slideRotation;
	}
}
