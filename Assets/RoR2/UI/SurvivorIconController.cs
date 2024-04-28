using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D9B RID: 3483
	[RequireComponent(typeof(HGButton))]
	public class SurvivorIconController : MonoBehaviour
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x00149D1B File Offset: 0x00147F1B
		// (set) Token: 0x06004FB5 RID: 20405 RVA: 0x00149D23 File Offset: 0x00147F23
		public CharacterSelectBarController characterSelectBarController { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x00149D2C File Offset: 0x00147F2C
		// (set) Token: 0x06004FB7 RID: 20407 RVA: 0x00149D34 File Offset: 0x00147F34
		public SurvivorIndex survivorIndex
		{
			get
			{
				return this._survivorIndex;
			}
			set
			{
				this.survivorDef = SurvivorCatalog.GetSurvivorDef(value);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x00149D42 File Offset: 0x00147F42
		// (set) Token: 0x06004FB9 RID: 20409 RVA: 0x00149D4C File Offset: 0x00147F4C
		public SurvivorDef survivorDef
		{
			get
			{
				return this._survivorDef;
			}
			set
			{
				if (this._survivorDef == value)
				{
					return;
				}
				this._survivorDef = value;
				this._survivorIndex = (this._survivorDef ? this._survivorDef.survivorIndex : SurvivorIndex.None);
				this.survivorBodyIndex = (this._survivorDef ? BodyCatalog.FindBodyIndex(this._survivorDef.bodyPrefab) : BodyIndex.None);
				this.survivorBodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(this.survivorBodyIndex);
				this.shouldRebuild = true;
				this.UpdateAvailability();
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x00149DCF File Offset: 0x00147FCF
		// (set) Token: 0x06004FBB RID: 20411 RVA: 0x00149DD7 File Offset: 0x00147FD7
		public bool survivorIsAvailable { get; private set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x00149DE0 File Offset: 0x00147FE0
		// (set) Token: 0x06004FBD RID: 20413 RVA: 0x00149DE8 File Offset: 0x00147FE8
		public HGButton hgButton { get; private set; }

		// Token: 0x06004FBE RID: 20414 RVA: 0x00149DF1 File Offset: 0x00147FF1
		private void Awake()
		{
			this.hgButton = base.GetComponent<HGButton>();
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00149E00 File Offset: 0x00148000
		private void Update()
		{
			this.isCurrentChoice = (this.characterSelectBarController.pickedIcon == this);
			this.UpdateAvailability();
			if (this.shouldRebuild)
			{
				this.shouldRebuild = false;
				this.Rebuild();
			}
			this.hgButton.interactable = true;
			this.survivorIsSelectedEffect.SetActive(this.isCurrentChoice);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x00149E59 File Offset: 0x00148059
		private LocalUser GetLocalUser()
		{
			return GameObject.Find("MPEventSystem Player0").GetComponent<MPEventSystem>().localUser;
			// return ((MPEventSystem)EventSystem.current).localUser;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00149E6A File Offset: 0x0014806A
		private void SetBoolAndMarkDirtyIfChanged(ref bool oldValue, bool newValue)
		{
			if (oldValue == newValue)
			{
				return;
			}
			oldValue = newValue;
			this.shouldRebuild = true;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00149E7C File Offset: 0x0014807C
		private void UpdateAvailability()
		{
			this.SetBoolAndMarkDirtyIfChanged(ref this.survivorIsUnlocked, SurvivorCatalog.SurvivorIsUnlockedOnThisClient(this.survivorIndex));
			this.SetBoolAndMarkDirtyIfChanged(ref this.survivorRequiredExpansionEnabled, this.survivorDef.CheckRequiredExpansionEnabled(null));
			this.SetBoolAndMarkDirtyIfChanged(ref this.survivorRequiredEntitlementAvailable, this.survivorDef.CheckUserHasRequiredEntitlement(this.GetLocalUser()));
			this.survivorIsAvailable = (this.survivorIsUnlocked && this.survivorRequiredExpansionEnabled && this.survivorRequiredEntitlementAvailable);
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00149EF4 File Offset: 0x001480F4
		private void Rebuild()
		{
			string viewableName = "";
			string viewableName2 = "";
			Texture texture = null;
			string text = "";
			Color titleColor = Color.clear;
			string overrideBodyText = "";
			if (this.survivorDef != null)
			{
				UnlockableDef unlockableDef = this.survivorDef.unlockableDef;
				if (this.survivorBodyPrefabBodyComponent)
				{
					texture = this.survivorBodyPrefabBodyComponent.portraitIcon;
					viewableName = string.Format(CultureInfo.InvariantCulture, "/Survivors/{0}", this.survivorDef.cachedName);
					viewableName2 = string.Format(CultureInfo.InvariantCulture, "/Loadout/Bodies/{0}/", BodyCatalog.GetBodyName(this.survivorBodyPrefabBodyComponent.bodyIndex));
					if (!this.survivorIsAvailable)
					{
						text = "UNIDENTIFIED";
						titleColor = Color.gray;
						if (!this.survivorRequiredEntitlementAvailable)
						{
							overrideBodyText = Language.GetStringFormatted("EXPANSION_PURCHASED_REQUIRED_FORMAT", new object[]
							{
								Language.GetString(this.survivorDef.GetRequiredEntitlement().nameToken)
							});
						}
						else if (!this.survivorRequiredExpansionEnabled)
						{
							overrideBodyText = Language.GetStringFormatted("EXPANSION_ENABLED_REQUIRED_FORMAT", new object[]
							{
								Language.GetString(this.survivorDef.GetRequiredExpansion().nameToken)
							});
						}
						else if (!this.survivorIsUnlocked && unlockableDef)
						{
							overrideBodyText = unlockableDef.getHowToUnlockString();
						}
					}
				}
			}
			if (this.survivorIcon)
			{
				this.survivorIcon.texture = texture;
				this.survivorIcon.color = (this.survivorIsAvailable ? Color.white : Color.black);
			}
			if (this.viewableTag)
			{
				this.viewableTag.viewableName = viewableName;
				this.viewableTag.Refresh();
			}
			if (this.loadoutViewableTag)
			{
				this.loadoutViewableTag.viewableName = viewableName2;
				this.loadoutViewableTag.Refresh();
			}
			if (this.viewableTrigger)
			{
				this.viewableTrigger.viewableName = viewableName;
			}
			if (this.tooltipProvider)
			{
				this.tooltipProvider.enabled = (text != "");
				this.tooltipProvider.titleToken = text;
				this.tooltipProvider.titleColor = titleColor;
				this.tooltipProvider.overrideBodyText = overrideBodyText;
			}
			this.hgButton.disableGamepadClick = !this.survivorIsAvailable;
			this.hgButton.disablePointerClick = !this.survivorIsAvailable;
		}

		// Token: 0x04004C51 RID: 19537
		public EclipseRunScreenController eclipseRunScreenController;

		// Token: 0x04004C52 RID: 19538
		private SurvivorIndex _survivorIndex = SurvivorIndex.None;

		// Token: 0x04004C53 RID: 19539
		private SurvivorDef _survivorDef;

		// Token: 0x04004C54 RID: 19540
		private BodyIndex survivorBodyIndex = BodyIndex.None;

		// Token: 0x04004C55 RID: 19541
		private CharacterBody survivorBodyPrefabBodyComponent;

		// Token: 0x04004C56 RID: 19542
		private bool survivorIsUnlocked;

		// Token: 0x04004C57 RID: 19543
		private bool survivorRequiredExpansionEnabled;

		// Token: 0x04004C58 RID: 19544
		private bool survivorRequiredEntitlementAvailable;

		// Token: 0x04004C5A RID: 19546
		private bool shouldRebuild = true;

		// Token: 0x04004C5B RID: 19547
		private bool isCurrentChoice;

		// Token: 0x04004C5C RID: 19548
		public RawImage survivorIcon;

		// Token: 0x04004C5D RID: 19549
		public GameObject survivorIsSelectedEffect;

		// Token: 0x04004C5F RID: 19551
		public ViewableTag viewableTag;

		// Token: 0x04004C60 RID: 19552
		public ViewableTag loadoutViewableTag;

		// Token: 0x04004C61 RID: 19553
		public ViewableTrigger viewableTrigger;

		// Token: 0x04004C62 RID: 19554
		public TooltipProvider tooltipProvider;
	}
}
