using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D49 RID: 3401
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class MPScrollbar : Scrollbar
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06004DD9 RID: 19929 RVA: 0x00140D15 File Offset: 0x0013EF15
		private EventSystem eventSystem
		{
			get
			{
				return this.eventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x00140D22 File Offset: 0x0013EF22
		protected override void Awake()
		{
			base.Awake();
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x00140D38 File Offset: 0x0013EF38
		private Selectable FilterSelectable(Selectable selectable)
		{
			if (selectable)
			{
				MPEventSystemLocator component = selectable.GetComponent<MPEventSystemLocator>();
				if (!component || component.eventSystem != this.eventSystemLocator.eventSystem)
				{
					selectable = null;
				}
			}
			return selectable;
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x00140D78 File Offset: 0x0013EF78
		public bool InputModuleIsAllowed(BaseInputModule inputModule)
		{
			if (this.allowAllEventSystems)
			{
				return true;
			}
			EventSystem eventSystem = this.eventSystem;
			return eventSystem && inputModule == eventSystem.currentInputModule;
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00140DAC File Offset: 0x0013EFAC
		public override Selectable FindSelectableOnDown()
		{
			return this.FilterSelectable(base.FindSelectableOnDown());
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00140DBA File Offset: 0x0013EFBA
		public override Selectable FindSelectableOnLeft()
		{
			return this.FilterSelectable(base.FindSelectableOnLeft());
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00140DC8 File Offset: 0x0013EFC8
		public override Selectable FindSelectableOnRight()
		{
			return this.FilterSelectable(base.FindSelectableOnRight());
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x00140DD6 File Offset: 0x0013EFD6
		public override Selectable FindSelectableOnUp()
		{
			return this.FilterSelectable(base.FindSelectableOnUp());
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00140DE4 File Offset: 0x0013EFE4
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			if (this.eventSystem && base.gameObject == this.eventSystem.currentSelectedGameObject)
			{
				base.enabled = false;
				base.enabled = true;
			}
			base.OnPointerUp(eventData);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x00140E3A File Offset: 0x0013F03A
		public override void OnSelect(BaseEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnSelect(eventData);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00140E52 File Offset: 0x0013F052
		public override void OnDeselect(BaseEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnDeselect(eventData);
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00140E6A File Offset: 0x0013F06A
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnPointerEnter(eventData);
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x00140E82 File Offset: 0x0013F082
		public override void OnPointerExit(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnPointerExit(eventData);
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00140E9C File Offset: 0x0013F09C
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && base.navigation.mode != Navigation.Mode.None)
			{
				this.eventSystem.SetSelectedGameObject(base.gameObject, eventData);
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00140EE3 File Offset: 0x0013F0E3
		public override void Select()
		{
			if (this.eventSystem.alreadySelecting)
			{
				return;
			}
			this.eventSystem.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x04004A8F RID: 19087
		public bool allowAllEventSystems;

		// Token: 0x04004A90 RID: 19088
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004A91 RID: 19089
		private RectTransform viewPortRectTransform;
	}
}
