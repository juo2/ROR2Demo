using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DA7 RID: 3495
	[RequireComponent(typeof(EventFunctions))]
	[RequireComponent(typeof(Animator))]
	public class UITransitionController : MonoBehaviour
	{
		// Token: 0x06005019 RID: 20505 RVA: 0x0014BA4A File Offset: 0x00149C4A
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.PushMecanimTransitionInParameters();
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0014BA60 File Offset: 0x00149C60
		private void PushMecanimTransitionInParameters()
		{
			this.animator.SetFloat("transitionInSpeed", this.transitionInSpeed);
			switch (this.transitionIn)
			{
			case UITransitionController.TransitionStyle.Instant:
				this.animator.SetTrigger("InstantIn");
				return;
			case UITransitionController.TransitionStyle.CanvasGroupAlphaFade:
				this.animator.SetTrigger("CanvasGroupAlphaFadeIn");
				return;
			case UITransitionController.TransitionStyle.SwipeYScale:
				this.animator.SetTrigger("SwipeYScaleIn");
				return;
			case UITransitionController.TransitionStyle.SwipeXScale:
				this.animator.SetTrigger("SwipeXScaleIn");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0014BAE4 File Offset: 0x00149CE4
		private void PushMecanimTransitionOutParameters()
		{
			this.animator.SetFloat("transitionOutSpeed", this.transitionOutSpeed);
			switch (this.transitionOut)
			{
			case UITransitionController.TransitionStyle.Instant:
				this.animator.SetTrigger("InstantOut");
				return;
			case UITransitionController.TransitionStyle.CanvasGroupAlphaFade:
				this.animator.SetTrigger("CanvasGroupAlphaFadeOut");
				return;
			case UITransitionController.TransitionStyle.SwipeYScale:
				this.animator.SetTrigger("SwipeYScaleOut");
				return;
			case UITransitionController.TransitionStyle.SwipeXScale:
				this.animator.SetTrigger("SwipeXScaleOut");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0014BB68 File Offset: 0x00149D68
		private void Update()
		{
			if (this.transitionOutAtEndOfLifetime && !this.done)
			{
				this.stopwatch += Time.deltaTime;
				if (this.stopwatch >= this.lifetime)
				{
					this.PushMecanimTransitionOutParameters();
					this.done = true;
				}
			}
		}

		// Token: 0x04004CC3 RID: 19651
		public UITransitionController.TransitionStyle transitionIn;

		// Token: 0x04004CC4 RID: 19652
		public UITransitionController.TransitionStyle transitionOut;

		// Token: 0x04004CC5 RID: 19653
		public float transitionInSpeed = 1f;

		// Token: 0x04004CC6 RID: 19654
		public float transitionOutSpeed = 1f;

		// Token: 0x04004CC7 RID: 19655
		public bool transitionOutAtEndOfLifetime;

		// Token: 0x04004CC8 RID: 19656
		public float lifetime;

		// Token: 0x04004CC9 RID: 19657
		private float stopwatch;

		// Token: 0x04004CCA RID: 19658
		private Animator animator;

		// Token: 0x04004CCB RID: 19659
		private bool done;

		// Token: 0x02000DA8 RID: 3496
		public enum TransitionStyle
		{
			// Token: 0x04004CCD RID: 19661
			Instant,
			// Token: 0x04004CCE RID: 19662
			CanvasGroupAlphaFade,
			// Token: 0x04004CCF RID: 19663
			SwipeYScale,
			// Token: 0x04004CD0 RID: 19664
			SwipeXScale
		}
	}
}
