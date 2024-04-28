using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D3E RID: 3390
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class MPButton : Button
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06004D4E RID: 19790 RVA: 0x0013F54B File Offset: 0x0013D74B
		public MPEventSystem eventSystem
		{
			get
			{
				return this.eventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0013F558 File Offset: 0x0013D758
		protected override void Awake()
		{
			base.Awake();
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0013F56C File Offset: 0x0013D76C
		public void Update()
		{
			if (!this.eventSystem || this.eventSystem.player == null)
			{
				return;
			}
			if (!this.disableGamepadClick && this.eventSystem.player.GetButtonDown(14) && this.eventSystem.currentSelectedGameObject == base.gameObject)
			{
				this.InvokeClick();
			}
			if (this.defaultFallbackButton && this.eventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad && this.eventSystem.currentSelectedGameObject == null && this.CanBeSelected())
			{
				Debug.LogFormat("Setting {0} as fallback button", new object[]
				{
					base.gameObject
				});
				this.Select();
			}
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0013F61E File Offset: 0x0013D81E
		public void InvokeClick()
		{
			Button.ButtonClickedEvent onClick = base.onClick;
			if (onClick == null)
			{
				return;
			}
			onClick.Invoke();
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0013F630 File Offset: 0x0013D830
		private Selectable FilterSelectable(Selectable selectable)
		{
			if (selectable)
			{
				MPEventSystemLocator component = selectable.GetComponent<MPEventSystemLocator>();
				if (!component || component.eventSystem != this.eventSystemLocator.eventSystem)
				{
					selectable = null;
				}
				MPButton mpbutton = (selectable != null) ? selectable.GetComponent<MPButton>() : null;
				if (mpbutton && !mpbutton.CanBeSelected())
				{
					selectable = null;
				}
			}
			return selectable;
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0013F690 File Offset: 0x0013D890
		public bool CanBeSelected()
		{
			return base.gameObject.activeInHierarchy && (!this.requiredTopLayer || this.requiredTopLayer.representsTopLayer);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0013F6BC File Offset: 0x0013D8BC
		public bool InputModuleIsAllowed(BaseInputModule inputModule)
		{
			if (this.allowAllEventSystems)
			{
				return true;
			}
			EventSystem eventSystem = this.eventSystem;
			return eventSystem && inputModule == eventSystem.currentInputModule;
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0013F6F0 File Offset: 0x0013D8F0
		private void AttemptSelection(PointerEventData eventData)
		{
			if (this.eventSystem && this.eventSystem.currentInputModule == eventData.currentInputModule)
			{
				this.eventSystem.SetSelectedGameObject(base.gameObject, eventData);
			}
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0013F729 File Offset: 0x0013D929
		public override void OnSelect(BaseEventData eventData)
		{
			UnityEvent unityEvent = this.onSelect;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			base.OnSelect(eventData);
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0013F743 File Offset: 0x0013D943
		public override void OnDeselect(BaseEventData eventData)
		{
			UnityEvent unityEvent = this.onDeselect;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			base.OnDeselect(eventData);
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0013F75D File Offset: 0x0013D95D
		public override Selectable FindSelectableOnDown()
		{
			return this.FilterSelectable(base.FindSelectableOnDown());
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0013F76B File Offset: 0x0013D96B
		public override Selectable FindSelectableOnLeft()
		{
			UnityEvent unityEvent = this.onFindSelectableLeft;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			return this.FilterSelectable(base.FindSelectableOnLeft());
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0013F78A File Offset: 0x0013D98A
		public override Selectable FindSelectableOnRight()
		{
			UnityEvent unityEvent = this.onFindSelectableRight;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			return this.FilterSelectable(base.FindSelectableOnRight());
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0013F7A9 File Offset: 0x0013D9A9
		public override Selectable FindSelectableOnUp()
		{
			return this.FilterSelectable(base.FindSelectableOnUp());
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0013F7B7 File Offset: 0x0013D9B7
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule))
			{
				return;
			}
			base.OnPointerEnter(eventData);
			this.AttemptSelection(eventData);
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0013F7D8 File Offset: 0x0013D9D8
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
			base.OnPointerExit(eventData);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0013F82E File Offset: 0x0013DA2E
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule) || this.disablePointerClick)
			{
				return;
			}
			base.OnPointerClick(eventData);
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0013F84E File Offset: 0x0013DA4E
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule) || this.disablePointerClick)
			{
				return;
			}
			this.inPointerUp = true;
			base.OnPointerUp(eventData);
			this.inPointerUp = false;
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x0013F87C File Offset: 0x0013DA7C
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.InputModuleIsAllowed(eventData.currentInputModule) || this.disablePointerClick)
			{
				return;
			}
			if (this.IsInteractable() && base.navigation.mode != Navigation.Mode.None)
			{
				this.AttemptSelection(eventData);
			}
			base.OnPointerDown(eventData);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x0013F8C6 File Offset: 0x0013DAC6
		public override void OnSubmit(BaseEventData eventData)
		{
			if (this.submitOnPointerUp && !this.inPointerUp)
			{
				return;
			}
			base.OnSubmit(eventData);
		}

		// Token: 0x04004A4D RID: 19021
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004A4E RID: 19022
		public bool allowAllEventSystems;

		// Token: 0x04004A4F RID: 19023
		[FormerlySerializedAs("pointerClickOnly")]
		public bool submitOnPointerUp;

		// Token: 0x04004A50 RID: 19024
		public bool disablePointerClick;

		// Token: 0x04004A51 RID: 19025
		public bool disableGamepadClick;

		// Token: 0x04004A52 RID: 19026
		public UILayerKey requiredTopLayer;

		// Token: 0x04004A53 RID: 19027
		public UnityEvent onFindSelectableLeft;

		// Token: 0x04004A54 RID: 19028
		public UnityEvent onFindSelectableRight;

		// Token: 0x04004A55 RID: 19029
		public UnityEvent onSelect;

		// Token: 0x04004A56 RID: 19030
		public UnityEvent onDeselect;

		// Token: 0x04004A57 RID: 19031
		public bool defaultFallbackButton;

		// Token: 0x04004A58 RID: 19032
		private bool inPointerUp;
	}
}
