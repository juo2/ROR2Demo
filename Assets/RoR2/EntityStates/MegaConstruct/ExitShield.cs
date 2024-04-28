using System;
using UnityEngine;

namespace EntityStates.MegaConstruct
{
	// Token: 0x02000287 RID: 647
	public class ExitShield : BaseState
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x0002FBFD File Offset: 0x0002DDFD
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackParameterName, this.baseDuration);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002FC36 File Offset: 0x0002DE36
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.baseDuration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000D69 RID: 3433
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000D6A RID: 3434
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000D6B RID: 3435
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x04000D6C RID: 3436
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000D6D RID: 3437
		private float duration;
	}
}
