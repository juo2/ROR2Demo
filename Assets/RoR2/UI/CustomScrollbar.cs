using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CF0 RID: 3312
	public class CustomScrollbar : MPScrollbar
	{
		// Token: 0x06004B70 RID: 19312 RVA: 0x00135BDE File Offset: 0x00133DDE
		protected override void Awake()
		{
			base.Awake();
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00135BE6 File Offset: 0x00133DE6
		protected override void Start()
		{
			base.Start();
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00135BF0 File Offset: 0x00133DF0
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				this.hovering = false;
				return;
			case Selectable.SelectionState.Highlighted:
				Util.PlaySound("Play_UI_menuHover", RoR2Application.instance.gameObject);
				this.hovering = true;
				return;
			case Selectable.SelectionState.Pressed:
				this.hovering = true;
				return;
			case Selectable.SelectionState.Selected:
				break;
			case Selectable.SelectionState.Disabled:
				this.hovering = false;
				break;
			default:
				return;
			}
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00135C54 File Offset: 0x00133E54
		public void OnClickCustom()
		{
			Util.PlaySound("Play_UI_menuClick", RoR2Application.instance.gameObject);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00135C6C File Offset: 0x00133E6C
		private void LateUpdate()
		{
			if (Application.isPlaying)
			{
				if (this.showImageOnHover)
				{
					float target = this.hovering ? 1f : 0f;
					Color color = this.imageOnHover.color;
					float a = Mathf.SmoothDamp(color.a, target, ref this.imageOnHoverAlphaVelocity, 0.03f, 100f, Time.unscaledDeltaTime);
					Color color2 = new Color(color.r, color.g, color.g, a);
					this.imageOnHover.color = color2;
				}
				if (this.imageOnInteractable)
				{
					this.imageOnInteractable.enabled = base.interactable;
				}
			}
		}

		// Token: 0x04004814 RID: 18452
		private Vector3 originalPosition;

		// Token: 0x04004815 RID: 18453
		public bool scaleButtonOnHover = true;

		// Token: 0x04004816 RID: 18454
		public bool showImageOnHover;

		// Token: 0x04004817 RID: 18455
		public Image imageOnHover;

		// Token: 0x04004818 RID: 18456
		public Image imageOnInteractable;

		// Token: 0x04004819 RID: 18457
		private bool hovering;

		// Token: 0x0400481A RID: 18458
		private float imageOnHoverAlphaVelocity;
	}
}
