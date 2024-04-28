using System;
using RoR2;
using UnityEngine;

namespace EntityStates.DeepVoidPortalBattery
{
	// Token: 0x020003D4 RID: 980
	public class BaseDeepVoidPortalBatteryState : BaseState
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x0004D508 File Offset: 0x0004B708
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.onEnterSoundString, base.gameObject);
			GameObject gameObject = base.FindModelChildGameObject(this.onEnterChildToEnable);
			if (gameObject)
			{
				gameObject.SetActive(true);
			}
			this.PlayAnimation("Base", this.animationStateName);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004D55C File Offset: 0x0004B75C
		public override void OnExit()
		{
			GameObject gameObject = base.FindModelChildGameObject(this.onEnterChildToEnable);
			if (gameObject)
			{
				gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x0400163F RID: 5695
		[SerializeField]
		public string onEnterSoundString;

		// Token: 0x04001640 RID: 5696
		[SerializeField]
		public string onEnterChildToEnable;

		// Token: 0x04001641 RID: 5697
		[SerializeField]
		public string animationStateName;
	}
}
