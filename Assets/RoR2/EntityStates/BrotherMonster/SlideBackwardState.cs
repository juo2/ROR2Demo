using System;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200043F RID: 1087
	public class SlideBackwardState : BaseSlideState
	{
		// Token: 0x06001377 RID: 4983 RVA: 0x00056C9E File Offset: 0x00054E9E
		public override void OnEnter()
		{
			this.slideRotation = Quaternion.AngleAxis(-180f, Vector3.up);
			base.OnEnter();
			base.PlayCrossfade("FullBody Override", "SlideBackward", "Slide.playbackRate", BaseSlideState.duration, 0.05f);
		}
	}
}
