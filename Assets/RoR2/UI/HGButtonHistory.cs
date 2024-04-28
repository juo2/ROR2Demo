using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D04 RID: 3332
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class HGButtonHistory : MonoBehaviour
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x00138D35 File Offset: 0x00136F35
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

		// Token: 0x06004BED RID: 19437 RVA: 0x00138D48 File Offset: 0x00136F48
		protected void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00138D56 File Offset: 0x00136F56
		private void OnEnable()
		{
			this.SelectRememberedButton();
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00138D60 File Offset: 0x00136F60
		private void Update()
		{
			bool flag = !this.requiredTopLayer || this.requiredTopLayer.representsTopLayer;
			if (flag && !this.topLayerWasOn)
			{
				this.SelectRememberedButton();
			}
			this.topLayerWasOn = flag;
			MPEventSystem eventSystem = this.eventSystem;
			if (!((eventSystem != null) ? eventSystem.currentSelectedGameObject : null) || !this.topLayerWasOn)
			{
				return;
			}
			MPEventSystem eventSystem2 = this.eventSystem;
			GameObject gameObject = (eventSystem2 != null) ? eventSystem2.currentSelectedGameObject : null;
			if (gameObject)
			{
				MPButton component = gameObject.GetComponent<MPButton>();
				if (component && component.navigation.mode != Navigation.Mode.None)
				{
					MPEventSystem eventSystem3 = this.eventSystem;
					this.lastRememberedGameObject = ((eventSystem3 != null) ? eventSystem3.currentSelectedGameObject : null);
				}
			}
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x00138E18 File Offset: 0x00137018
		private void SelectRememberedButton()
		{
			if (this.lastRememberedGameObject)
			{
				MPEventSystem eventSystem = this.eventSystem;
				if (eventSystem != null && eventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad)
				{
					MPButton component = this.lastRememberedGameObject.GetComponent<MPButton>();
					if (component)
					{
						Debug.LogFormat("Applying button history ({0})", new object[]
						{
							this.lastRememberedGameObject
						});
						component.Select();
						component.OnSelect(null);
					}
					return;
				}
			}
		}

		// Token: 0x040048C3 RID: 18627
		public UILayerKey requiredTopLayer;

		// Token: 0x040048C4 RID: 18628
		public GameObject lastRememberedGameObject;

		// Token: 0x040048C5 RID: 18629
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040048C6 RID: 18630
		private bool topLayerWasOn;
	}
}
