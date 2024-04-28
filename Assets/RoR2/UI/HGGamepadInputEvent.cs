using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D05 RID: 3333
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class HGGamepadInputEvent : MonoBehaviour
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x00138E87 File Offset: 0x00137087
		protected MPEventSystem eventSystem
		{
			get
			{
				MPEventSystemLocator mpeventSystemLocator = this.eventSystemLocator;
				if (mpeventSystemLocator == null)
				{
					return null;
				}
				return mpeventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00138E9A File Offset: 0x0013709A
		protected void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x00138EA8 File Offset: 0x001370A8
		protected void Update()
		{
			bool flag = this.CanAcceptInput();
			if (this.couldAcceptInput != flag)
			{
				GameObject[] array = this.enabledObjectsIfActive;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(flag);
				}
			}
			if (this.CanAcceptInput() && this.eventSystem.player.GetButtonDown(this.actionName))
			{
				this.actionEvent.Invoke();
			}
			this.couldAcceptInput = flag;
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x00138F15 File Offset: 0x00137115
		protected bool CanAcceptInput()
		{
			if (base.gameObject.activeInHierarchy && (!this.requiredTopLayer || this.requiredTopLayer.representsTopLayer))
			{
				MPEventSystem eventSystem = this.eventSystem;
				return eventSystem != null && eventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad;
			}
			return false;
		}

		// Token: 0x040048C7 RID: 18631
		public string actionName;

		// Token: 0x040048C8 RID: 18632
		public UnityEvent actionEvent;

		// Token: 0x040048C9 RID: 18633
		public UILayerKey requiredTopLayer;

		// Token: 0x040048CA RID: 18634
		public GameObject[] enabledObjectsIfActive;

		// Token: 0x040048CB RID: 18635
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040048CC RID: 18636
		private bool couldAcceptInput;
	}
}
