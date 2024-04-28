using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MajorConstruct.Weapon
{
	// Token: 0x02000292 RID: 658
	public class ChargeLaser : BaseState
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x00030448 File Offset: 0x0002E648
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackParameterName, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000304B0 File Offset: 0x0002E6B0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireLaser());
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04000DB5 RID: 3509
		[SerializeField]
		public float duration;

		// Token: 0x04000DB6 RID: 3510
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000DB7 RID: 3511
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000DB8 RID: 3512
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x04000DB9 RID: 3513
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000DBA RID: 3514
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000DBB RID: 3515
		[SerializeField]
		public string enterSoundString;
	}
}
