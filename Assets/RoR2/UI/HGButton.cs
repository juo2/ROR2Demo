using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D03 RID: 3331
	public class HGButton : MPButton
	{
		// Token: 0x06004BE3 RID: 19427 RVA: 0x00138A39 File Offset: 0x00136C39
		protected override void Awake()
		{
			base.Awake();
			this.textMeshProUGui = base.GetComponent<TextMeshProUGUI>();
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00138A50 File Offset: 0x00136C50
		protected override void Start()
		{
			base.Start();
			base.onClick.AddListener(new UnityAction(this.OnClickCustom));
			if (this.updateTextOnHover && !this.hoverLanguageTextMeshController)
			{
				Debug.LogErrorFormat("HGButton \"{0}\" is missing an object assigned to its .hoverLangaugeTextMeshController field.", new object[]
				{
					Util.GetGameObjectHierarchyName(base.gameObject)
				});
			}
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00138AB0 File Offset: 0x00136CB0
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			if (this.previousState != state)
			{
				switch (state)
				{
				case Selectable.SelectionState.Normal:
					this.hovering = false;
					break;
				case Selectable.SelectionState.Highlighted:
					Util.PlaySound("Play_UI_menuHover", RoR2Application.instance.gameObject);
					this.hovering = true;
					break;
				case Selectable.SelectionState.Pressed:
					this.hovering = true;
					break;
				case Selectable.SelectionState.Disabled:
					this.hovering = false;
					break;
				}
				this.previousState = state;
			}
			this.originalColor = base.targetGraphic.color;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x00138B39 File Offset: 0x00136D39
		public void OnClickCustom()
		{
			Util.PlaySound((!string.IsNullOrEmpty(this.uiClickSoundOverride)) ? this.uiClickSoundOverride : "Play_UI_menuClick", RoR2Application.instance.gameObject);
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00138B68 File Offset: 0x00136D68
		private void LateUpdate()
		{
			this.stopwatch += Time.deltaTime;
			if (Application.isPlaying)
			{
				if (this.showImageOnHover)
				{
					float target = this.hovering ? 1f : 0f;
					float target2 = this.hovering ? 1f : 1.1f;
					Color color = this.imageOnHover.color;
					float x = this.imageOnHover.transform.localScale.x;
					float a = Mathf.SmoothDamp(color.a, target, ref this.imageOnHoverAlphaVelocity, 0.03f * base.colors.fadeDuration, float.PositiveInfinity, Time.unscaledDeltaTime);
					float num = Mathf.SmoothDamp(x, target2, ref this.imageOnHoverScaleVelocity, 0.03f, float.PositiveInfinity, Time.unscaledDeltaTime);
					Color color2 = new Color(color.r, color.g, color.g, a);
					Vector3 localScale = new Vector3(num, num, num);
					this.imageOnHover.color = color2;
					this.imageOnHover.transform.localScale = localScale;
				}
				if (this.imageOnInteractable)
				{
					this.imageOnInteractable.enabled = base.interactable;
				}
			}
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x00138C9A File Offset: 0x00136E9A
		public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			if (this.updateTextOnHover && this.hoverLanguageTextMeshController)
			{
				this.hoverLanguageTextMeshController.token = this.hoverToken;
			}
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00138CC9 File Offset: 0x00136EC9
		public override void OnPointerExit(PointerEventData eventData)
		{
			if (this.updateTextOnHover && this.hoverLanguageTextMeshController)
			{
				this.hoverLanguageTextMeshController.token = "";
			}
			base.OnPointerExit(eventData);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00138CF7 File Offset: 0x00136EF7
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			if (this.updateTextOnHover && this.hoverLanguageTextMeshController)
			{
				this.hoverLanguageTextMeshController.token = this.hoverToken;
			}
		}

		// Token: 0x040048B4 RID: 18612
		private TextMeshProUGUI textMeshProUGui;

		// Token: 0x040048B5 RID: 18613
		private float stopwatch;

		// Token: 0x040048B6 RID: 18614
		private Color originalColor;

		// Token: 0x040048B7 RID: 18615
		public bool showImageOnHover;

		// Token: 0x040048B8 RID: 18616
		public Image imageOnHover;

		// Token: 0x040048B9 RID: 18617
		public Image imageOnInteractable;

		// Token: 0x040048BA RID: 18618
		public bool updateTextOnHover;

		// Token: 0x040048BB RID: 18619
		public LanguageTextMeshController hoverLanguageTextMeshController;

		// Token: 0x040048BC RID: 18620
		public string hoverToken;

		// Token: 0x040048BD RID: 18621
		private bool hovering;

		// Token: 0x040048BE RID: 18622
		private Selectable.SelectionState previousState = Selectable.SelectionState.Disabled;

		// Token: 0x040048BF RID: 18623
		public string uiClickSoundOverride;

		// Token: 0x040048C0 RID: 18624
		private float buttonScaleVelocity;

		// Token: 0x040048C1 RID: 18625
		private float imageOnHoverAlphaVelocity;

		// Token: 0x040048C2 RID: 18626
		private float imageOnHoverScaleVelocity;
	}
}
