using System;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200043E RID: 1086
	public class SlideForwardState : BaseSlideState
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00056C44 File Offset: 0x00054E44
		public override void OnEnter()
		{
			this.slideRotation = Quaternion.identity;
			base.OnEnter();
			base.PlayCrossfade("FullBody Override", "SlideForward", "Slide.playbackRate", BaseSlideState.duration, 0.05f);
			base.PlayCrossfade("Body", "Run", 0.05f);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00056C96 File Offset: 0x00054E96
		public override void OnExit()
		{
			base.OnExit();
		}
	}
}
