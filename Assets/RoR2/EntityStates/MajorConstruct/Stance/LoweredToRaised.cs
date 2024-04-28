using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MajorConstruct.Stance
{
	// Token: 0x0200028F RID: 655
	public class LoweredToRaised : BaseState
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x00030318 File Offset: 0x0002E518
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParameter, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00030380 File Offset: 0x0002E580
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new Raised());
			}
		}

		// Token: 0x04000DA7 RID: 3495
		[SerializeField]
		public float duration;

		// Token: 0x04000DA8 RID: 3496
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000DA9 RID: 3497
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000DAA RID: 3498
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000DAB RID: 3499
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000DAC RID: 3500
		[SerializeField]
		public string animationPlaybackRateParameter;

		// Token: 0x04000DAD RID: 3501
		[SerializeField]
		public string enterSoundString;
	}
}
