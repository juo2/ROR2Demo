using System;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200043D RID: 1085
	public class SlideRightState : BaseSlideState
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x00056C08 File Offset: 0x00054E08
		public override void OnEnter()
		{
			this.slideRotation = Quaternion.AngleAxis(90f, Vector3.up);
			base.OnEnter();
			base.PlayCrossfade("FullBody Override", "SlideRight", "Slide.playbackRate", BaseSlideState.duration, 0.05f);
		}
	}
}
