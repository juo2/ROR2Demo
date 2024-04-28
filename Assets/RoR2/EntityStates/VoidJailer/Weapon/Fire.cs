using System;
using UnityEngine;

namespace EntityStates.VoidJailer.Weapon
{
	// Token: 0x02000158 RID: 344
	public class Fire : GenericProjectileBaseState
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00019E74 File Offset: 0x00018074
		public override void OnEnter()
		{
			this.muzzleTransform = base.FindModelChild("ClawMuzzle");
			base.OnEnter();
			base.characterBody.SetAimTimer(this.duration + 3f);
			for (int i = 1; i < Fire.totalProjectileCount; i++)
			{
				this.FireProjectile();
			}
			this.priorityReductionDuration = Fire.basePriorityReductionDuration / this.attackSpeedStat;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019ED7 File Offset: 0x000180D7
		protected override Ray ModifyProjectileAimRay(Ray aimRay)
		{
			aimRay.origin += UnityEngine.Random.insideUnitSphere * Fire.maxRandomDistance;
			return aimRay;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00019EFB File Offset: 0x000180FB
		protected override void PlayAnimation(float duration)
		{
			base.PlayAnimation(Fire.animationLayerName, Fire.animationStateName, Fire.animationPlaybackRateName, duration);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019F13 File Offset: 0x00018113
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge <= this.priorityReductionDuration)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x04000753 RID: 1875
		public static string animationLayerName;

		// Token: 0x04000754 RID: 1876
		public static string animationStateName;

		// Token: 0x04000755 RID: 1877
		public static string animationPlaybackRateName;

		// Token: 0x04000756 RID: 1878
		public static int totalProjectileCount;

		// Token: 0x04000757 RID: 1879
		public static float maxRandomDistance;

		// Token: 0x04000758 RID: 1880
		public static float basePriorityReductionDuration;

		// Token: 0x04000759 RID: 1881
		private float priorityReductionDuration;

		// Token: 0x0400075A RID: 1882
		private Transform muzzleTransform;
	}
}
