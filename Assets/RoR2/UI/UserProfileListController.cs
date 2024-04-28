using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoR2.UI
{
	// Token: 0x02000DA9 RID: 3497
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class UserProfileListController : MonoBehaviour
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600501E RID: 20510 RVA: 0x0014BBC5 File Offset: 0x00149DC5
		private EventSystem eventSystem
		{
			get
			{
				return this.eventSystemLocator.eventSystem;
			}
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0014BBD2 File Offset: 0x00149DD2
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0014BBE0 File Offset: 0x00149DE0
		private void OnEnable()
		{
			this.RebuildElements();
			SaveSystem.onAvailableUserProfilesChanged += this.RebuildElements;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0014BBF9 File Offset: 0x00149DF9
		private void OnDisable()
		{
			SaveSystem.onAvailableUserProfilesChanged -= this.RebuildElements;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0014BC0C File Offset: 0x00149E0C
		private void RebuildElements()
		{
			foreach (object obj in this.contentRect)
			{
				UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
			this.elementsList.Clear();
			List<string> availableProfileNames = PlatformSystems.saveSystem.GetAvailableProfileNames();
			for (int i = 0; i < availableProfileNames.Count; i++)
			{
				if (this.allowDefault || !availableProfileNames[i].Equals("default", StringComparison.OrdinalIgnoreCase))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.elementPrefab, this.contentRect);
					UserProfileListElementController component = gameObject.GetComponent<UserProfileListElementController>();
					MPButton component2 = gameObject.GetComponent<MPButton>();
					component.listController = this;
					component.userProfile = PlatformSystems.saveSystem.GetProfile(availableProfileNames[i]);
					this.elementsList.Add(component);
					gameObject.SetActive(true);
					if (i == 0)
					{
						component2.defaultFallbackButton = true;
					}
				}
			}
			if (this.onListRebuilt != null)
			{
				this.onListRebuilt();
			}
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0014BD20 File Offset: 0x00149F20
		public ReadOnlyCollection<UserProfileListElementController> GetReadOnlyElementsList()
		{
			return new ReadOnlyCollection<UserProfileListElementController>(this.elementsList);
		}

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x06005024 RID: 20516 RVA: 0x0014BD30 File Offset: 0x00149F30
		// (remove) Token: 0x06005025 RID: 20517 RVA: 0x0014BD68 File Offset: 0x00149F68
		public event UserProfileListController.ProfileSelectedDelegate onProfileSelected;

		// Token: 0x06005026 RID: 20518 RVA: 0x0014BD9D File Offset: 0x00149F9D
		public void SendProfileSelection(UserProfile userProfile)
		{
			UserProfileListController.ProfileSelectedDelegate profileSelectedDelegate = this.onProfileSelected;
			if (profileSelectedDelegate == null)
			{
				return;
			}
			profileSelectedDelegate(userProfile);
		}

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x06005027 RID: 20519 RVA: 0x0014BDB0 File Offset: 0x00149FB0
		// (remove) Token: 0x06005028 RID: 20520 RVA: 0x0014BDE8 File Offset: 0x00149FE8
		public event Action onListRebuilt;

		// Token: 0x04004CD1 RID: 19665
		public GameObject elementPrefab;

		// Token: 0x04004CD2 RID: 19666
		public RectTransform contentRect;

		// Token: 0x04004CD3 RID: 19667
		[Tooltip("Whether or not \"default\" profile appears as a selectable option.")]
		public bool allowDefault = true;

		// Token: 0x04004CD4 RID: 19668
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004CD5 RID: 19669
		private readonly List<UserProfileListElementController> elementsList = new List<UserProfileListElementController>();

		// Token: 0x02000DAA RID: 3498
		// (Invoke) Token: 0x0600502B RID: 20523
		public delegate void ProfileSelectedDelegate(UserProfile userProfile);
	}
}
