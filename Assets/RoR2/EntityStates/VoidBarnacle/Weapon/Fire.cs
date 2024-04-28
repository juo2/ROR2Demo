using System;
using UnityEngine;

namespace EntityStates.VoidBarnacle.Weapon
{
	// Token: 0x02000164 RID: 356
	public class Fire : GenericProjectileBaseState
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x0001AEA0 File Offset: 0x000190A0
		public override void OnEnter()
		{
			this.duration = this.baseDuration / this.attackSpeedStat;
			this._interFireballDuration = this.duration / (float)this.numberOfFireballs;
			this._animationDuration = this._interFireballDuration;
			this.muzzleTransform = base.FindModelChild(this.targetMuzzle);
			base.OnEnter();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.stopwatch >= this._animationDuration && this.numberOfFireballs > 0)
			{
				this._animationDuration += this._animationDuration;
				this.PlayAnimation(this._animationDuration);
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001AF36 File Offset: 0x00019136
		protected override void PlayAnimation(float duration)
		{
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateName, this._interFireballDuration);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001AF56 File Offset: 0x00019156
		protected override void FireProjectile()
		{
			base.FireProjectile();
			if (this.numberOfFireballs > 1)
			{
				this.firedProjectile = false;
				this.delayBeforeFiringProjectile += this._interFireballDuration;
			}
			this.numberOfFireballs--;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040007A4 RID: 1956
		[SerializeField]
		public int numberOfFireballs;

		// Token: 0x040007A5 RID: 1957
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040007A6 RID: 1958
		[SerializeField]
		public string animationStateName;

		// Token: 0x040007A7 RID: 1959
		[SerializeField]
		public string animationPlaybackRateName;

		// Token: 0x040007A8 RID: 1960
		private float _interFireballDuration;

		// Token: 0x040007A9 RID: 1961
		private float _animationDuration;

		// Token: 0x040007AA RID: 1962
		private Transform muzzleTransform;
	}
}
