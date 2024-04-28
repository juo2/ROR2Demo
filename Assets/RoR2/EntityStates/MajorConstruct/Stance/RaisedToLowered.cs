using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MajorConstruct.Stance
{
	// Token: 0x02000291 RID: 657
	public class RaisedToLowered : BaseState
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x000303B0 File Offset: 0x0002E5B0
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

		// Token: 0x06000B92 RID: 2962 RVA: 0x00030418 File Offset: 0x0002E618
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new Lowered());
			}
		}

		// Token: 0x04000DAE RID: 3502
		[SerializeField]
		public float duration;

		// Token: 0x04000DAF RID: 3503
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000DB0 RID: 3504
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000DB1 RID: 3505
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000DB2 RID: 3506
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000DB3 RID: 3507
		[SerializeField]
		public string animationPlaybackRateParameter;

		// Token: 0x04000DB4 RID: 3508
		[SerializeField]
		public string enterSoundString;
	}
}
