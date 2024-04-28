using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000106 RID: 262
	public class ChargeHandBeam : BaseState
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00013A84 File Offset: 0x00011C84
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

		// Token: 0x060004A1 RID: 1185 RVA: 0x00013B16 File Offset: 0x00011D16
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new FireHandBeam());
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000521 RID: 1313
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000522 RID: 1314
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000523 RID: 1315
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000524 RID: 1316
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x04000525 RID: 1317
		[SerializeField]
		public string muzzle;

		// Token: 0x04000526 RID: 1318
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000527 RID: 1319
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x04000528 RID: 1320
		private float duration;
	}
}
