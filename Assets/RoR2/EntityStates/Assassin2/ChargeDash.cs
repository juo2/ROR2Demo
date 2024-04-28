using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Assassin2
{
	// Token: 0x02000485 RID: 1157
	public class ChargeDash : BaseState
	{
		// Token: 0x060014AB RID: 5291 RVA: 0x0005BC54 File Offset: 0x00059E54
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(ChargeDash.enterSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			AimAnimator component = this.modelTransform.GetComponent<AimAnimator>();
			this.duration = ChargeDash.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (component)
			{
				component.enabled = true;
			}
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (this.modelAnimator)
			{
				this.PlayAnimation("Gesture", "PreAttack");
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0005BD1E File Offset: 0x00059F1E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new DashStrike());
				return;
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A65 RID: 6757
		public static float baseDuration = 1.5f;

		// Token: 0x04001A66 RID: 6758
		public static string enterSoundString;

		// Token: 0x04001A67 RID: 6759
		private Animator modelAnimator;

		// Token: 0x04001A68 RID: 6760
		private float duration;

		// Token: 0x04001A69 RID: 6761
		private int slashCount;

		// Token: 0x04001A6A RID: 6762
		private Transform modelTransform;

		// Token: 0x04001A6B RID: 6763
		private Vector3 oldVelocity;

		// Token: 0x04001A6C RID: 6764
		private bool dashComplete;
	}
}
