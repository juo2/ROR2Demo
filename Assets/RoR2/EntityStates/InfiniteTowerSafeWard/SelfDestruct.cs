using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002FC RID: 764
	public class SelfDestruct : BaseSafeWardState
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x00039456 File Offset: 0x00037656
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00039482 File Offset: 0x00037682
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x040010A7 RID: 4263
		[SerializeField]
		public float duration;

		// Token: 0x040010A8 RID: 4264
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040010A9 RID: 4265
		[SerializeField]
		public string animationStateName;

		// Token: 0x040010AA RID: 4266
		[SerializeField]
		public string enterSoundString;
	}
}
