using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Gup
{
	// Token: 0x02000339 RID: 825
	public class GupSpikesState : BasicMeleeAttack
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x000401E8 File Offset: 0x0003E3E8
		public override void OnEnter()
		{
			base.OnEnter();
			base.StartAimMode(0f, false);
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterDirection.forward;
			}
			if (this.hitBoxGroup)
			{
				foreach (HitBox hitBox in this.hitBoxGroup.hitBoxes)
				{
					if (hitBox.gameObject.name == this.initialHitboxName)
					{
						this.initialHitBox = hitBox;
						return;
					}
				}
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00040275 File Offset: 0x0003E475
		protected override void PlayAnimation()
		{
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration, this.crossfadeDuration);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004029B File Offset: 0x0003E49B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.initialHitBox)
			{
				this.initialHitBox.enabled = (this.animator.GetFloat(this.initialHitboxActiveParameter) > 0.5f);
			}
		}

		// Token: 0x04001297 RID: 4759
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04001298 RID: 4760
		[SerializeField]
		public string animationStateName;

		// Token: 0x04001299 RID: 4761
		[SerializeField]
		public string playbackRateParam;

		// Token: 0x0400129A RID: 4762
		[SerializeField]
		public float crossfadeDuration;

		// Token: 0x0400129B RID: 4763
		[SerializeField]
		public string initialHitboxActiveParameter;

		// Token: 0x0400129C RID: 4764
		[SerializeField]
		public string initialHitboxName;

		// Token: 0x0400129D RID: 4765
		private HitBox initialHitBox;
	}
}
