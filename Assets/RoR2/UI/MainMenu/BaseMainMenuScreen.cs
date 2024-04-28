using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI.MainMenu
{
	// Token: 0x02000DC6 RID: 3526
	[RequireComponent(typeof(RectTransform))]
	public class BaseMainMenuScreen : MonoBehaviour
	{
		// Token: 0x060050A5 RID: 20645 RVA: 0x0014D68A File Offset: 0x0014B88A
		public void Awake()
		{
			this.firstSelectedObjectProvider = base.GetComponent<FirstSelectedObjectProvider>();
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public virtual bool IsReadyToLeave()
		{
			return true;
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x0014D698 File Offset: 0x0014B898
		public void Update()
		{
			if (SimpleDialogBox.instancesList.Count == 0)
			{
				FirstSelectedObjectProvider firstSelectedObjectProvider = this.firstSelectedObjectProvider;
				if (firstSelectedObjectProvider == null)
				{
					return;
				}
				firstSelectedObjectProvider.EnsureSelectedObject();
			}
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0014D6B6 File Offset: 0x0014B8B6
		public virtual void OnEnter(MainMenuController mainMenuController)
		{
			Debug.LogFormat("BaseMainMenuScreen: OnEnter()", Array.Empty<object>());
			this.myMainMenuController = mainMenuController;
			if (SimpleDialogBox.instancesList.Count == 0)
			{
				FirstSelectedObjectProvider firstSelectedObjectProvider = this.firstSelectedObjectProvider;
				if (firstSelectedObjectProvider != null)
				{
					firstSelectedObjectProvider.EnsureSelectedObject();
				}
			}
			this.onEnter.Invoke();
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0014D6F6 File Offset: 0x0014B8F6
		public virtual void OnExit(MainMenuController mainMenuController)
		{
			if (this.myMainMenuController == mainMenuController)
			{
				this.myMainMenuController = null;
			}
			this.onExit.Invoke();
		}

		// Token: 0x04004D38 RID: 19768
		public Transform desiredCameraTransform;

		// Token: 0x04004D39 RID: 19769
		[HideInInspector]
		public bool shouldDisplay;

		// Token: 0x04004D3A RID: 19770
		protected MainMenuController myMainMenuController;

		// Token: 0x04004D3B RID: 19771
		protected FirstSelectedObjectProvider firstSelectedObjectProvider;

		// Token: 0x04004D3C RID: 19772
		public UnityEvent onEnter;

		// Token: 0x04004D3D RID: 19773
		public UnityEvent onExit;
	}
}
