using System;
using UnityEngine;

namespace RoR2.VoidRaidCrab
{
	// Token: 0x02000B6C RID: 2924
	[RequireComponent(typeof(Animator))]
	public class CentralLegControllerAnimationEventReceiver : MonoBehaviour
	{
		// Token: 0x06004271 RID: 17009 RVA: 0x00113324 File Offset: 0x00111524
		public void VoidRaidCrabFootStep(string targetName)
		{
			Transform transform = this.childLocator.FindChild(targetName);
			if (transform)
			{
				LegController componentInParent = transform.GetComponentInParent<LegController>();
				if (componentInParent && componentInParent.mainBodyHasEffectiveAuthority)
				{
					componentInParent.DoToeConcussionBlastAuthority(null, true);
				}
			}
		}

		// Token: 0x0400406D RID: 16493
		public CentralLegController target;

		// Token: 0x0400406E RID: 16494
		public ChildLocator childLocator;
	}
}
