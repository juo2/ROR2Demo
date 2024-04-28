using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.Mine
{
	// Token: 0x02000399 RID: 921
	public class BaseMineArmingState : BaseState
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x00048774 File Offset: 0x00046974
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlayAttackSpeedSound(this.onEnterSfx, base.gameObject, this.onEnterSfxPlaybackRate);
			if (!string.IsNullOrEmpty(this.pathToChildToEnable))
			{
				this.enabledChild = base.transform.Find(this.pathToChildToEnable);
				if (this.enabledChild)
				{
					this.enabledChild.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000487E1 File Offset: 0x000469E1
		public override void OnExit()
		{
			if (this.enabledChild)
			{
				this.enabledChild.gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x040014E1 RID: 5345
		[SerializeField]
		public float damageScale;

		// Token: 0x040014E2 RID: 5346
		[SerializeField]
		public float forceScale;

		// Token: 0x040014E3 RID: 5347
		[SerializeField]
		public float blastRadiusScale;

		// Token: 0x040014E4 RID: 5348
		[SerializeField]
		public float triggerRadius;

		// Token: 0x040014E5 RID: 5349
		[SerializeField]
		public string onEnterSfx;

		// Token: 0x040014E6 RID: 5350
		[SerializeField]
		public float onEnterSfxPlaybackRate;

		// Token: 0x040014E7 RID: 5351
		[SerializeField]
		public string pathToChildToEnable;

		// Token: 0x040014E8 RID: 5352
		private Transform enabledChild;
	}
}
