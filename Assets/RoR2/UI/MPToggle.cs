using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D4A RID: 3402
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class MPToggle : Toggle
	{
		// Token: 0x06004DE9 RID: 19945 RVA: 0x00140F0C File Offset: 0x0013F10C
		protected override void Awake()
		{
			base.Awake();
			this.mpControlHelper = new MPControlHelper(this, base.GetComponent<MPEventSystemLocator>(), this.allowAllEventSystems, this.disablePointerClick);
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x00140F34 File Offset: 0x0013F134
		private void LateUpdate()
		{
			if (this.hoverGraphic)
			{
				bool flag = base.currentSelectionState == Selectable.SelectionState.Highlighted;
				if (this.hoverGraphic.enabled != flag)
				{
					this.hoverGraphic.enabled = flag;
				}
			}
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x00140F72 File Offset: 0x0013F172
		public override void OnPointerEnter(PointerEventData eventData)
		{
			this.mpControlHelper.OnPointerEnter(eventData, new Action<PointerEventData>(base.OnPointerEnter));
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x00140F8C File Offset: 0x0013F18C
		public override void OnPointerExit(PointerEventData eventData)
		{
			this.mpControlHelper.OnPointerExit(eventData, new Action<PointerEventData>(base.OnPointerExit));
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x00140FA6 File Offset: 0x0013F1A6
		public override void OnPointerClick(PointerEventData eventData)
		{
			this.mpControlHelper.OnPointerClick(eventData, new Action<PointerEventData>(base.OnPointerClick));
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x00140FC0 File Offset: 0x0013F1C0
		public override void OnPointerUp(PointerEventData eventData)
		{
			this.mpControlHelper.OnPointerUp(eventData, new Action<PointerEventData>(base.OnPointerUp), ref this.inPointerUp);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x00140FE0 File Offset: 0x0013F1E0
		public override void OnPointerDown(PointerEventData eventData)
		{
			this.mpControlHelper.OnPointerDown(eventData, new Action<PointerEventData>(base.OnPointerDown), ref this.inPointerUp);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x00141000 File Offset: 0x0013F200
		public override Selectable FindSelectableOnDown()
		{
			return this.mpControlHelper.FindSelectableOnDown(new Func<Selectable>(base.FindSelectableOnDown));
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x00141019 File Offset: 0x0013F219
		public override Selectable FindSelectableOnLeft()
		{
			return this.mpControlHelper.FindSelectableOnLeft(new Func<Selectable>(base.FindSelectableOnLeft));
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00141032 File Offset: 0x0013F232
		public override Selectable FindSelectableOnRight()
		{
			return this.mpControlHelper.FindSelectableOnRight(new Func<Selectable>(base.FindSelectableOnRight));
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0014104B File Offset: 0x0013F24B
		public override Selectable FindSelectableOnUp()
		{
			return this.mpControlHelper.FindSelectableOnUp(new Func<Selectable>(base.FindSelectableOnUp));
		}

		// Token: 0x04004A92 RID: 19090
		private MPControlHelper mpControlHelper;

		// Token: 0x04004A93 RID: 19091
		public bool allowAllEventSystems;

		// Token: 0x04004A94 RID: 19092
		public bool disablePointerClick;

		// Token: 0x04004A95 RID: 19093
		public Graphic hoverGraphic;

		// Token: 0x04004A96 RID: 19094
		private bool inPointerUp;
	}
}
