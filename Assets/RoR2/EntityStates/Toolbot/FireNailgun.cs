using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000198 RID: 408
	public class FireNailgun : BaseNailgunState
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x0001F203 File Offset: 0x0001D403
		protected override float GetBaseDuration()
		{
			return FireNailgun.baseRefireInterval;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001F20A File Offset: 0x0001D40A
		public override void OnEnter()
		{
			base.OnEnter();
			this.loopSoundID = Util.PlaySound(FireNailgun.spinUpSound, base.gameObject);
			base.animateNailgunFiring = true;
			this.refireStopwatch = this.duration;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001F23C File Offset: 0x0001D43C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.refireStopwatch += Time.fixedDeltaTime;
			if (this.refireStopwatch >= this.duration)
			{
				base.PullCurrentStats();
				this.refireStopwatch -= this.duration;
				Ray aimRay = base.GetAimRay();
				Vector3 direction = aimRay.direction;
				Vector3 axis = Vector3.Cross(Vector3.up, direction);
				float num = Mathf.Sin((float)this.fireNumber * 0.5f);
				Vector3 vector = Quaternion.AngleAxis(base.characterBody.spreadBloomAngle * num, axis) * direction;
				vector = Quaternion.AngleAxis((float)this.fireNumber * -65.454544f, direction) * vector;
				aimRay.direction = vector;
				base.FireBullet(aimRay, 1, 0f, 0f);
			}
			if (base.isAuthority && (!base.IsKeyDownAuthority() || base.characterBody.isSprinting))
			{
				this.outer.SetNextState(new NailgunSpinDown
				{
					activatorSkillSlot = base.activatorSkillSlot
				});
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001F344 File Offset: 0x0001D544
		public override void OnExit()
		{
			base.animateNailgunFiring = false;
			AkSoundEngine.StopPlayingID(this.loopSoundID);
			base.OnExit();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040008DA RID: 2266
		public static float baseRefireInterval;

		// Token: 0x040008DB RID: 2267
		public static string spinUpSound;

		// Token: 0x040008DC RID: 2268
		private float refireStopwatch;

		// Token: 0x040008DD RID: 2269
		private uint loopSoundID;
	}
}
