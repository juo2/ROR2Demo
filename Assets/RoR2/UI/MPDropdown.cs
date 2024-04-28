using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D40 RID: 3392
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class MPDropdown : TMP_Dropdown
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x0013FB16 File Offset: 0x0013DD16
		protected MPEventSystem eventSystem
		{
			get
			{
				return this.eventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0013FB23 File Offset: 0x0013DD23
		protected override void Awake()
		{
			base.Awake();
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x0013FB38 File Offset: 0x0013DD38
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

		// Token: 0x06004D75 RID: 19829 RVA: 0x0013FB78 File Offset: 0x0013DD78
		public override Selectable FindSelectableOnDown()
		{
			return this.FilterSelectable(base.FindSelectableOnDown());
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x0013FB86 File Offset: 0x0013DD86
		public override Selectable FindSelectableOnLeft()
		{
			return this.FilterSelectable(base.FindSelectableOnLeft());
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x0013FB94 File Offset: 0x0013DD94
		public override Selectable FindSelectableOnRight()
		{
			return this.FilterSelectable(base.FindSelectableOnRight());
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x0013FBA2 File Offset: 0x0013DDA2
		public override Selectable FindSelectableOnUp()
		{
			return this.FilterSelectable(base.FindSelectableOnUp());
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0013FBB0 File Offset: 0x0013DDB0
		public bool InputModuleIsAllowed(BaseInputModule inputModule)
		{
			if (this.allowAllEventSystems)
			{
				return true;
			}
			EventSystem eventSystem = this.eventSystem;
			return eventSystem && inputModule == eventSystem.currentInputModule;
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0013FBE4 File Offset: 0x0013DDE4
		private void AttemptSelection(PointerEventData eventData)
		{
			if (this.eventSystem && this.eventSystem.currentInputModule == eventData.currentInputModule)
			{
				this.eventSystem.SetSelectedGameObject(base.gameObject, eventData);
			}
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x0013FC1D File Offset: 0x0013DE1D
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			this.isPointerInside = true;
			base.OnPointerEnter(eventData);
			this.AttemptSelection(eventData);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0013FC44 File Offset: 0x0013DE44
		public override void OnPointerExit(PointerEventData eventData)
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
			this.isPointerInside = false;
			base.OnPointerExit(eventData);
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x0013FCA1 File Offset: 0x0013DEA1
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnPointerClick(eventData);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x0013FCB9 File Offset: 0x0013DEB9
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnPointerUp(eventData);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x0013FCD4 File Offset: 0x0013DED4
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			if (this.IsInteractable() && base.navigation.mode != Navigation.Mode.None)
			{
				this.AttemptSelection(eventData);
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0013FD16 File Offset: 0x0013DF16
		protected override void OnDisable()
		{
			base.OnDisable();
			this.isPointerInside = false;
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x0013FD25 File Offset: 0x0013DF25
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
			if (this.previousState != state)
			{
				if (state == Selectable.SelectionState.Highlighted)
				{
					Util.PlaySound("Play_UI_menuHover", RoR2Application.instance.gameObject);
				}
				this.previousState = state;
			}
		}

		// Token: 0x04004A5E RID: 19038
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004A5F RID: 19039
		protected bool isPointerInside;

		// Token: 0x04004A60 RID: 19040
		public bool allowAllEventSystems;

		// Token: 0x04004A61 RID: 19041
		private Selectable.SelectionState previousState = Selectable.SelectionState.Disabled;
	}
}
