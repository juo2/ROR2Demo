using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F4 RID: 244
	public class ChargeCorruptHandBeam : BaseSkillState
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00012088 File Offset: 0x00010288
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.GetAimRay();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			base.characterBody.SetAimTimer(3f);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001211A File Offset: 0x0001031A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new FireCorruptHandBeam
				{
					activatorSkillSlot = base.activatorSkillSlot
				});
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000478 RID: 1144
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000479 RID: 1145
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400047A RID: 1146
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400047B RID: 1147
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x0400047C RID: 1148
		[SerializeField]
		public string muzzle;

		// Token: 0x0400047D RID: 1149
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400047E RID: 1150
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x0400047F RID: 1151
		private float duration;
	}
}
