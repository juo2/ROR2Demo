using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MinorConstruct
{
	// Token: 0x0200026A RID: 618
	public class BaseHideState : BaseState
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002C358 File Offset: 0x0002A558
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			Transform transform = base.FindModelChild(this.childToEnable);
			if (transform)
			{
				transform.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002C3B0 File Offset: 0x0002A5B0
		public override void OnExit()
		{
			Transform transform = base.FindModelChild(this.childToEnable);
			if (transform)
			{
				transform.gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x04000C56 RID: 3158
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000C57 RID: 3159
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000C58 RID: 3160
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000C59 RID: 3161
		[SerializeField]
		public string childToEnable;
	}
}
