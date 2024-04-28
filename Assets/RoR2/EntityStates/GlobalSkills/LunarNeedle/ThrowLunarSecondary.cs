using System;
using EntityStates.Mage.Weapon;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.GlobalSkills.LunarNeedle
{
	// Token: 0x02000374 RID: 884
	public class ThrowLunarSecondary : BaseThrowBombState
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x000467F9 File Offset: 0x000449F9
		protected override void PlayThrowAnimation()
		{
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00046819 File Offset: 0x00044A19
		protected override void ModifyProjectile(ref FireProjectileInfo projectileInfo)
		{
			projectileInfo.speedOverride = Util.Remap(this.charge, 0f, 1f, this.minSpeed, this.maxSpeed);
			projectileInfo.useSpeedOverride = true;
		}

		// Token: 0x04001469 RID: 5225
		[SerializeField]
		public float minSpeed;

		// Token: 0x0400146A RID: 5226
		[SerializeField]
		public float maxSpeed;

		// Token: 0x0400146B RID: 5227
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400146C RID: 5228
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400146D RID: 5229
		[SerializeField]
		public string playbackRateParam;
	}
}
