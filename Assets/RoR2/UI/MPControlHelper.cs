using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D3F RID: 3391
	public struct MPControlHelper
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x0013F8E8 File Offset: 0x0013DAE8
		public MPEventSystem eventSystem
		{
			get
			{
				return this.eventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x0013F8F5 File Offset: 0x0013DAF5
		public MPControlHelper(Selectable owner, MPEventSystemLocator eventSystemLocator, bool allowAllEventSystems, bool disablePointerClick)
		{
			this.owner = owner;
			this.eventSystemLocator = eventSystemLocator;
			this.gameObject = this.owner.gameObject;
			this.allowAllEventSystems = allowAllEventSystems;
			this.disablePointerClick = disablePointerClick;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0013F928 File Offset: 0x0013DB28
		public Selectable FilterSelectable(Selectable selectable)
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

		// Token: 0x06004D66 RID: 19814 RVA: 0x0013F968 File Offset: 0x0013DB68
		public bool InputModuleIsAllowed(BaseInputModule inputModule)
		{
			if (this.allowAllEventSystems)
			{
				return true;
			}
			EventSystem eventSystem = this.eventSystem;
			return eventSystem && inputModule == eventSystem.currentInputModule;
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x0013F99C File Offset: 0x0013DB9C
		private void AttemptSelection(PointerEventData eventData)
		{
			if (this.eventSystem && this.eventSystem.currentInputModule == eventData.currentInputModule)
			{
				this.eventSystem.SetSelectedGameObject(this.gameObject, eventData);
			}
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x0013F9D5 File Offset: 0x0013DBD5
		public Selectable FindSelectableOnDown(Func<Selectable> baseMethod)
		{
			return this.FilterSelectable(baseMethod());
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x0013F9D5 File Offset: 0x0013DBD5
		public Selectable FindSelectableOnLeft(Func<Selectable> baseMethod)
		{
			return this.FilterSelectable(baseMethod());
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x0013F9D5 File Offset: 0x0013DBD5
		public Selectable FindSelectableOnRight(Func<Selectable> baseMethod)
		{
			return this.FilterSelectable(baseMethod());
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x0013F9D5 File Offset: 0x0013DBD5
		public Selectable FindSelectableOnUp(Func<Selectable> baseMethod)
		{
			return this.FilterSelectable(baseMethod());
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x0013F9E3 File Offset: 0x0013DBE3
		public void OnPointerEnter(PointerEventData eventData, Action<PointerEventData> baseMethod)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			baseMethod(eventData);
			this.AttemptSelection(eventData);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x0013FA04 File Offset: 0x0013DC04
		public void OnPointerExit(PointerEventData eventData, Action<PointerEventData> baseMethod)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			if (this.eventSystem && this.owner.gameObject == this.eventSystem.currentSelectedGameObject)
			{
				this.owner.enabled = false;
				this.owner.enabled = true;
			}
			baseMethod(eventData);
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0013FA69 File Offset: 0x0013DC69
		public void OnPointerClick(PointerEventData eventData, Action<PointerEventData> baseMethod)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule) || this.disablePointerClick)
			{
				return;
			}
			baseMethod(eventData);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0013FA89 File Offset: 0x0013DC89
		public void OnPointerUp(PointerEventData eventData, Action<PointerEventData> baseMethod, ref bool inPointerUp)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule) || this.disablePointerClick)
			{
				return;
			}
			inPointerUp = true;
			baseMethod(eventData);
			inPointerUp = false;
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0013FAB0 File Offset: 0x0013DCB0
		public void OnPointerDown(PointerEventData eventData, Action<PointerEventData> baseMethod, ref bool inPointerUp)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule) || this.disablePointerClick)
			{
				return;
			}
			if (this.owner.IsInteractable() && this.owner.navigation.mode != Navigation.Mode.None)
			{
				this.AttemptSelection(eventData);
			}
			baseMethod(eventData);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0013FB04 File Offset: 0x0013DD04
		public void OnSubmit(BaseEventData eventData, Action<BaseEventData> baseMethod, bool submitOnPointerUp, ref bool inPointerUp)
		{
			if (submitOnPointerUp && !inPointerUp)
			{
				return;
			}
			baseMethod(eventData);
		}

		// Token: 0x04004A59 RID: 19033
		private readonly MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004A5A RID: 19034
		private readonly Selectable owner;

		// Token: 0x04004A5B RID: 19035
		private readonly GameObject gameObject;

		// Token: 0x04004A5C RID: 19036
		private readonly bool allowAllEventSystems;

		// Token: 0x04004A5D RID: 19037
		private readonly bool disablePointerClick;
	}
}
