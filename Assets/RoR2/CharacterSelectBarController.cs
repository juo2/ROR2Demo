using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020008CF RID: 2255
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class CharacterSelectBarController : MonoBehaviour
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000D54F1 File Offset: 0x000D36F1
		[Obsolete("Use iconContainerGrid instead", false)]
		public ref GridLayoutGroup gridLayoutGroup
		{
			get
			{
				return ref this.iconContainerGrid;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000D54F9 File Offset: 0x000D36F9
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000D5501 File Offset: 0x000D3701
		public SurvivorIconController pickedIcon { get; private set; }

		// Token: 0x06003281 RID: 12929 RVA: 0x000D550C File Offset: 0x000D370C
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.survivorIconControllers = new UIElementAllocator<SurvivorIconController>((RectTransform)this.iconContainerGrid.transform, this.choiceButtonPrefab, true, false);
			this.survivorIconControllers.onCreateElement = new UIElementAllocator<SurvivorIconController>.ElementOperationDelegate(this.OnCreateSurvivorIcon);
			this.fillerIcons = new UIElementAllocator<RectTransform>((RectTransform)this.iconContainerGrid.transform, this.fillButtonPrefab, true, false);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000D5582 File Offset: 0x000D3782
		private void LateUpdate()
		{
			this.EnforceValidChoice();
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000D558A File Offset: 0x000D378A
		private void OnEnable()
		{
			this.eventSystemLocator.onEventSystemDiscovered += this.OnEventSystemDiscovered;
			if (this.eventSystemLocator.eventSystem)
			{
				this.OnEventSystemDiscovered(this.eventSystemLocator.eventSystem);
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000D55C6 File Offset: 0x000D37C6
		private void OnDisable()
		{
			this.eventSystemLocator.onEventSystemDiscovered -= this.OnEventSystemDiscovered;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000D55DF File Offset: 0x000D37DF
		public void ApplyPickToPickerSurvivorPreference(CharacterSelectBarController.SurvivorPickInfo survivorPickInfo)
		{
			if (survivorPickInfo.localUser == null)
			{
				return;
			}
			survivorPickInfo.localUser.userProfile.SetSurvivorPreference(survivorPickInfo.pickedSurvivor);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000026ED File Offset: 0x000008ED
		public void ApplyPickToEclipseSurvivorPreference(CharacterSelectBarController.SurvivorPickInfo survivorPickInfo)
		{
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000D5600 File Offset: 0x000D3800
		private void OnEventSystemDiscovered(MPEventSystem eventSystem)
		{
			this.currentLocalUser = eventSystem.localUser;
			this.Build();
			SurvivorDef localUserExistingSurvivorPreference = this.GetLocalUserExistingSurvivorPreference();
			this.PickIconBySurvivorDef(localUserExistingSurvivorPreference);
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000D5630 File Offset: 0x000D3830
		private void OnCreateSurvivorIcon(int index, SurvivorIconController survivorIconController)
		{
			survivorIconController.characterSelectBarController = this;
			survivorIconController.hgButton.onClick.AddListener(delegate()
			{
				this.PickIcon(survivorIconController);
			});
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000D5680 File Offset: 0x000D3880
		private int FindIconIndexForSurvivorDef(SurvivorDef survivorDef)
		{
			ReadOnlyCollection<SurvivorIconController> elements = this.survivorIconControllers.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i].survivorDef == survivorDef)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000D56BC File Offset: 0x000D38BC
		private int FindIconIndex(SurvivorIconController survivorIconController)
		{
			ReadOnlyCollection<SurvivorIconController> elements = this.survivorIconControllers.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i] == survivorIconController)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000D56F4 File Offset: 0x000D38F4
		private void PickIconBySurvivorDef(SurvivorDef survivorDef)
		{
			int num = this.FindIconIndexForSurvivorDef(survivorDef);
			if (num < 0)
			{
				return;
			}
			this.PickIcon(this.survivorIconControllers.elements[num]);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000D5728 File Offset: 0x000D3928
		private void PickIcon(SurvivorIconController newPickedIcon)
		{
			if (this.pickedIcon == newPickedIcon)
			{
				return;
			}
			this.pickedIcon = newPickedIcon;
			CharacterSelectBarController.SurvivorPickInfoUnityEvent survivorPickInfoUnityEvent = this.onSurvivorPicked;
			if (survivorPickInfoUnityEvent == null)
			{
				return;
			}
			survivorPickInfoUnityEvent.Invoke(new CharacterSelectBarController.SurvivorPickInfo
			{
				localUser = this.currentLocalUser,
				pickedSurvivor = newPickedIcon.survivorDef
			});
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000D5779 File Offset: 0x000D3979
		private bool isEclipseRun
		{
			get
			{
				return PreGameController.instance && PreGameController.instance.gameModeIndex == GameModeCatalog.FindGameModeIndex("EclipseRun");
			}
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000D579F File Offset: 0x000D399F
		private bool ShouldDisplaySurvivor(SurvivorDef survivorDef)
		{
			return survivorDef && !survivorDef.hidden && survivorDef.CheckUserHasRequiredEntitlement(this.currentLocalUser);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000D57BF File Offset: 0x000D39BF
		private SurvivorDef GetLocalUserExistingSurvivorPreference()
		{
			LocalUser localUser = this.currentLocalUser;
			if (localUser == null)
			{
				return null;
			}
			return localUser.userProfile.GetSurvivorPreference();
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000D57D8 File Offset: 0x000D39D8
		private void Build()
		{
			List<SurvivorDef> list = new List<SurvivorDef>();
			foreach (SurvivorDef survivorDef in SurvivorCatalog.orderedSurvivorDefs)
			{
				if (this.ShouldDisplaySurvivor(survivorDef))
				{
					list.Add(survivorDef);
				}
			}
			int count = list.Count;
			int desiredCount = Math.Max(CharacterSelectBarController.CalcGridCellCount(count, this.iconContainerGrid.constraintCount) - count, 0);
			this.survivorIconControllers.AllocateElements(count);
			this.fillerIcons.AllocateElements(desiredCount);
			this.fillerIcons.MoveElementsToContainerEnd();
			ReadOnlyCollection<SurvivorIconController> elements = this.survivorIconControllers.elements;
			for (int i = 0; i < count; i++)
			{
				SurvivorDef survivorDef2 = list[i];
				SurvivorIconController survivorIconController = elements[i];
				survivorIconController.survivorDef = survivorDef2;
				survivorIconController.hgButton.defaultFallbackButton = (i == 0);
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000D58C4 File Offset: 0x000D3AC4
		private static int CalcGridCellCount(int elementCount, int gridFixedDimensionLength)
		{
			return (int)Mathf.Ceil((float)elementCount / (float)gridFixedDimensionLength) * gridFixedDimensionLength;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000D58D4 File Offset: 0x000D3AD4
		private void EnforceValidChoice()
		{
			int num = this.FindIconIndex(this.pickedIcon);
			if (this.pickedIcon && this.pickedIcon.survivorIsAvailable)
			{
				return;
			}
			int i = -1;
			ReadOnlyCollection<SurvivorIconController> elements = this.survivorIconControllers.elements;
			while (i < elements.Count)
			{
				int num2 = num + i;
				if (0 <= num2 && num2 < elements.Count)
				{
					SurvivorIconController survivorIconController = elements[num2];
					if (survivorIconController.survivorIsAvailable)
					{
						this.PickIcon(survivorIconController);
						return;
					}
				}
				if (i >= 0)
				{
					i++;
				}
				i = -i;
			}
		}

		// Token: 0x040033AF RID: 13231
		[Header("Prefabs")]
		public GameObject choiceButtonPrefab;

		// Token: 0x040033B0 RID: 13232
		public GameObject fillButtonPrefab;

		// Token: 0x040033B1 RID: 13233
		[ShowFieldObsolete]
		[Obsolete("No longer used.", false)]
		public GameObject WIPButtonPrefab;

		// Token: 0x040033B2 RID: 13234
		[Header("Required References")]
		[FormerlySerializedAs("gridLayoutGroup")]
		public GridLayoutGroup iconContainerGrid;

		// Token: 0x040033B3 RID: 13235
		[Header("Events")]
		public CharacterSelectBarController.SurvivorPickInfoUnityEvent onSurvivorPicked;

		// Token: 0x040033B4 RID: 13236
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040033B5 RID: 13237
		private UIElementAllocator<SurvivorIconController> survivorIconControllers;

		// Token: 0x040033B6 RID: 13238
		private UIElementAllocator<RectTransform> fillerIcons;

		// Token: 0x040033B8 RID: 13240
		private LocalUser currentLocalUser;

		// Token: 0x020008D0 RID: 2256
		public struct SurvivorPickInfo
		{
			// Token: 0x040033B9 RID: 13241
			public LocalUser localUser;

			// Token: 0x040033BA RID: 13242
			public SurvivorDef pickedSurvivor;
		}

		// Token: 0x020008D1 RID: 2257
		[Serializable]
		public class SurvivorPickInfoUnityEvent : UnityEvent<CharacterSelectBarController.SurvivorPickInfo>
		{
		}
	}
}
