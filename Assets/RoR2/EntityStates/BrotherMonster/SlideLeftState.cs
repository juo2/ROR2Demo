using System;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200043C RID: 1084
	public class SlideLeftState : BaseSlideState
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x00056BC4 File Offset: 0x00054DC4
		public override void OnEnter()
		{
			this.slideRotation = Quaternion.AngleAxis(-90f, Vector3.up);
			base.OnEnter();
			base.PlayCrossfade("FullBody Override", "SlideLeft", "Slide.playbackRate", BaseSlideState.duration, 0.05f);
		}
	}
}
