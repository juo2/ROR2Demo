using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.UI
{
	// Token: 0x02000D60 RID: 3424
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(MPEventSystemProvider))]
	public class PauseScreenController : MonoBehaviour
	{
		// Token: 0x06004E74 RID: 20084 RVA: 0x00143DFC File Offset: 0x00141FFC
		private void Awake()
		{
			this.eventSystemProvider = base.GetComponent<MPEventSystemProvider>();
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x00143E0C File Offset: 0x0014200C
		private void OnEnable()
		{
			if (PauseScreenController.instancesList.Count == 0)
			{
				PauseScreenController.paused = NetworkServer.dontListen;
				if (PauseScreenController.paused)
				{
					if (PauseManager.onPauseStartGlobal != null)
					{
						PauseManager.onPauseStartGlobal();
					}
					PauseScreenController.oldTimeScale = Time.timeScale;
					Time.timeScale = 0f;
				}
			}
			PauseScreenController.instancesList.Add(this);
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x00143E68 File Offset: 0x00142068
		private void OnDisable()
		{
			PauseScreenController.instancesList.Remove(this);
			if (PauseScreenController.instancesList.Count == 0 && PauseScreenController.paused)
			{
				Time.timeScale = PauseScreenController.oldTimeScale;
				PauseScreenController.paused = false;
				if (PauseManager.onPauseEndGlobal != null)
				{
					PauseManager.onPauseEndGlobal();
				}
			}
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x00143EB5 File Offset: 0x001420B5
		public void OpenSettingsMenu()
		{
			UnityEngine.Object.Destroy(this.submenuObject);
			this.submenuObject = UnityEngine.Object.Instantiate<GameObject>(this.settingsPanelPrefab, this.rootPanel);
			this.mainPanel.gameObject.SetActive(false);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x00143EEA File Offset: 0x001420EA
		public void Update()
		{
			if (!this.submenuObject)
			{
				this.mainPanel.gameObject.SetActive(true);
			}
			if (!NetworkManager.singleton.isNetworkActive)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04004B19 RID: 19225
		public static readonly List<PauseScreenController> instancesList = new List<PauseScreenController>();

		// Token: 0x04004B1A RID: 19226
		private MPEventSystemProvider eventSystemProvider;

		// Token: 0x04004B1B RID: 19227
		[Tooltip("The main panel to which any submenus will be parented.")]
		public RectTransform rootPanel;

		// Token: 0x04004B1C RID: 19228
		[Tooltip("The panel which contains the main controls. This will be disabled while in a submenu.")]
		public RectTransform mainPanel;

		// Token: 0x04004B1D RID: 19229
		public GameObject settingsPanelPrefab;

		// Token: 0x04004B1E RID: 19230
		private GameObject submenuObject;

		// Token: 0x04004B1F RID: 19231
		public GameObject exitGameButton;

		// Token: 0x04004B20 RID: 19232
		private static float oldTimeScale;

		// Token: 0x04004B21 RID: 19233
		private static bool paused = false;
	}
}
