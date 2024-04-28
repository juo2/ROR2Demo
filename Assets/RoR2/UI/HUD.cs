using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D0B RID: 3339
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(MPEventSystemProvider))]
	public class HUD : MonoBehaviour
	{
		// Token: 0x06004C12 RID: 19474 RVA: 0x00139648 File Offset: 0x00137848
		private static void OnUICameraPreRender(UICamera uiCamera)
		{
			CameraRigController cameraRigController = uiCamera.cameraRigController;
			if (cameraRigController)
			{
				LocalUser localUser = cameraRigController.viewer ? cameraRigController.viewer.localUser : null;
				if (localUser != null)
				{
					HUD.lockInstancesList = true;
					for (int i = 0; i < HUD.instancesList.Count; i++)
					{
						HUD hud = HUD.instancesList[i];
						if (hud.localUserViewer == localUser)
						{
							hud.canvas.worldCamera = uiCamera.camera;
						}
						else
						{
							GameObject gameObject = hud.gameObject;
							HUD.instancesToReenableList.Add(gameObject);
							gameObject.SetActive(false);
						}
					}
					HUD.lockInstancesList = false;
				}
			}
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x001396EC File Offset: 0x001378EC
		private static void OnUICameraPostRender(UICamera uiCamera)
		{
			HUD.lockInstancesList = true;
			for (int i = 0; i < HUD.instancesToReenableList.Count; i++)
			{
				HUD.instancesToReenableList[i].SetActive(true);
			}
			HUD.instancesToReenableList.Clear();
			HUD.lockInstancesList = false;
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x00139735 File Offset: 0x00137935
		public void OnEnable()
		{
			if (!HUD.lockInstancesList)
			{
				HUD.instancesList.Add(this);
				this.UpdateHudVisibility();
			}
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x0013974F File Offset: 0x0013794F
		public void OnDisable()
		{
			if (!HUD.lockInstancesList)
			{
				HUD.instancesList.Remove(this);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x00139764 File Offset: 0x00137964
		public GameObject targetBodyObject
		{
			get
			{
				if (!this.targetMaster)
				{
					return null;
				}
				return this.targetMaster.GetBodyObject();
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x00139780 File Offset: 0x00137980
		// (set) Token: 0x06004C18 RID: 19480 RVA: 0x00139788 File Offset: 0x00137988
		public CharacterMaster targetMaster { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x00139791 File Offset: 0x00137991
		// (set) Token: 0x06004C1A RID: 19482 RVA: 0x00139799 File Offset: 0x00137999
		public LocalUser localUserViewer
		{
			get
			{
				return this._localUserViewer;
			}
			set
			{
				if (this._localUserViewer != value)
				{
					this._localUserViewer = value;
					this.eventSystemProvider.eventSystem = this._localUserViewer.eventSystem;
				}
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x001397C1 File Offset: 0x001379C1
		// (set) Token: 0x06004C1C RID: 19484 RVA: 0x001397C9 File Offset: 0x001379C9
		public GameObject gameModeUiInstance { get; private set; }

		// Token: 0x06004C1D RID: 19485 RVA: 0x001397D4 File Offset: 0x001379D4
		private void Awake()
		{
			this.eventSystemProvider = base.GetComponent<MPEventSystemProvider>();
			this.canvas = base.GetComponent<Canvas>();
			if (this.scoreboardPanel)
			{
				this.scoreboardPanel.SetActive(false);
			}
			this.mainUIPanel.SetActive(false);
			this.gameModeUiInstance = Run.instance.InstantiateUi(this.gameModeUiRoot);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00139834 File Offset: 0x00137A34
		public void Update()
		{
			NetworkUser networkUser;
			if (!this.targetMaster)
			{
				networkUser = null;
			}
			else
			{
				PlayerCharacterMasterController component = this.targetMaster.GetComponent<PlayerCharacterMasterController>();
				networkUser = ((component != null) ? component.networkUser : null);
			}
			NetworkUser networkUser2 = networkUser;
			PlayerCharacterMasterController playerCharacterMasterController = this.targetMaster ? this.targetMaster.GetComponent<PlayerCharacterMasterController>() : null;
			Inventory inventory = this.targetMaster ? this.targetMaster.inventory : null;
			CharacterBody characterBody = this.targetBodyObject ? this.targetBodyObject.GetComponent<CharacterBody>() : null;
			if (this.healthBar && this.targetBodyObject)
			{
				this.healthBar.source = this.targetBodyObject.GetComponent<HealthComponent>();
			}
			if (this.expBar)
			{
				this.expBar.source = this.targetMaster;
			}
			if (this.levelText)
			{
				this.levelText.source = characterBody;
			}
			if (this.moneyText)
			{
				this.moneyText.targetValue = (int)(this.targetMaster ? this.targetMaster.money : 0U);
			}
			if (this.lunarCoinContainer)
			{
				bool flag = this.localUserViewer != null && this.localUserViewer.userProfile.totalCollectedCoins > 0U;
				uint targetValue = networkUser2 ? networkUser2.lunarCoins : 0U;
				this.lunarCoinContainer.SetActive(flag);
				if (flag && this.lunarCoinText)
				{
					this.lunarCoinText.targetValue = (int)targetValue;
				}
			}
			if (this.voidCoinContainer)
			{
				int num = (int)(this.targetMaster ? this.targetMaster.voidCoins : 0U);
				bool flag2 = num > 0;
				this.voidCoinContainer.SetActive(flag2);
				if (flag2 && this.voidCoinText)
				{
					this.voidCoinText.targetValue = num;
				}
			}
			if (this.itemInventoryDisplay)
			{
				this.itemInventoryDisplay.SetSubscribedInventory(inventory);
			}
			if (this.targetBodyObject)
			{
				SkillLocator component2 = this.targetBodyObject.GetComponent<SkillLocator>();
				if (component2)
				{
					if (this.skillIcons.Length != 0 && this.skillIcons[0])
					{
						this.skillIcons[0].targetSkillSlot = SkillSlot.Primary;
						this.skillIcons[0].targetSkill = component2.primary;
						this.skillIcons[0].playerCharacterMasterController = playerCharacterMasterController;
					}
					if (this.skillIcons.Length > 1 && this.skillIcons[1])
					{
						this.skillIcons[1].targetSkillSlot = SkillSlot.Secondary;
						this.skillIcons[1].targetSkill = component2.secondary;
						this.skillIcons[1].playerCharacterMasterController = playerCharacterMasterController;
					}
					if (this.skillIcons.Length > 2 && this.skillIcons[2])
					{
						this.skillIcons[2].targetSkillSlot = SkillSlot.Utility;
						this.skillIcons[2].targetSkill = component2.utility;
						this.skillIcons[2].playerCharacterMasterController = playerCharacterMasterController;
					}
					if (this.skillIcons.Length > 3 && this.skillIcons[3])
					{
						this.skillIcons[3].targetSkillSlot = SkillSlot.Special;
						this.skillIcons[3].targetSkill = component2.special;
						this.skillIcons[3].playerCharacterMasterController = playerCharacterMasterController;
					}
				}
			}
			foreach (EquipmentIcon equipmentIcon in this.equipmentIcons)
			{
				equipmentIcon.targetInventory = inventory;
				equipmentIcon.targetEquipmentSlot = (this.targetBodyObject ? this.targetBodyObject.GetComponent<EquipmentSlot>() : null);
				equipmentIcon.playerCharacterMasterController = (this.targetMaster ? this.targetMaster.GetComponent<PlayerCharacterMasterController>() : null);
			}
			if (this.buffDisplay)
			{
				this.buffDisplay.source = characterBody;
			}
			if (this.allyCardManager)
			{
				this.allyCardManager.sourceGameObject = this.targetBodyObject;
			}
			if (this.scoreboardPanel)
			{
				bool active = this.localUserViewer != null && this.localUserViewer.inputPlayer != null && this.localUserViewer.inputPlayer.GetButton("info");
				this.scoreboardPanel.SetActive(active);
			}
			if (this.speedometer)
			{
				this.speedometer.targetTransform = (this.targetBodyObject ? this.targetBodyObject.transform : null);
			}
			if (this.combatHealthBarViewer)
			{
				this.combatHealthBarViewer.crosshairTarget = (this.cameraRigController.lastCrosshairHurtBox ? this.cameraRigController.lastCrosshairHurtBox.healthComponent : null);
				this.combatHealthBarViewer.viewerBody = characterBody;
				this.combatHealthBarViewer.viewerBodyObject = this.targetBodyObject;
				this.combatHealthBarViewer.viewerTeamIndex = TeamComponent.GetObjectTeam(this.targetBodyObject);
			}
			if (this.targetBodyObject != this.previousTargetBodyObject)
			{
				this.previousTargetBodyObject = this.targetBodyObject;
				Action<HUD> action = HUD.onHudTargetChangedGlobal;
				if (action != null)
				{
					action(this);
				}
			}
			int j = 0;
			int num2 = base.transform.childCount - 1;
			while (j < num2)
			{
				base.transform.GetChild(j).gameObject.SetActive(false);
				j++;
			}
			if (base.transform.childCount > 0)
			{
				base.transform.GetChild(base.transform.childCount - 1).gameObject.SetActive(true);
			}
			this.UpdateHudVisibility();
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x00139DAC File Offset: 0x00137FAC
		private void UpdateHudVisibility()
		{
			if (this.mainContainer.activeInHierarchy)
			{
				CameraRigController cameraRigController = this.cameraRigController;
				bool flag = cameraRigController != null && cameraRigController.isHudAllowed;
				HUD.ShouldHudDisplayDelegate shouldHudDisplayDelegate = HUD.shouldHudDisplay;
				if (shouldHudDisplayDelegate != null)
				{
					shouldHudDisplayDelegate(this, ref flag);
				}
				this.mainUIPanel.SetActive(HUD.cvHudEnable.value && flag);
			}
		}

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x06004C20 RID: 19488 RVA: 0x00139E04 File Offset: 0x00138004
		// (remove) Token: 0x06004C21 RID: 19489 RVA: 0x00139E38 File Offset: 0x00138038
		public static event Action<HUD> onHudTargetChangedGlobal;

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06004C22 RID: 19490 RVA: 0x00139E6C File Offset: 0x0013806C
		// (remove) Token: 0x06004C23 RID: 19491 RVA: 0x00139EA0 File Offset: 0x001380A0
		public static event HUD.ShouldHudDisplayDelegate shouldHudDisplay;

		// Token: 0x040048EA RID: 18666
		private static List<HUD> instancesList = new List<HUD>();

		// Token: 0x040048EB RID: 18667
		private static bool lockInstancesList = false;

		// Token: 0x040048EC RID: 18668
		private static List<GameObject> instancesToReenableList = new List<GameObject>();

		// Token: 0x040048ED RID: 18669
		public static readonly ReadOnlyCollection<HUD> readOnlyInstanceList = HUD.instancesList.AsReadOnly();

		// Token: 0x040048EF RID: 18671
		private LocalUser _localUserViewer;

		// Token: 0x040048F0 RID: 18672
		[Header("Main")]
		[Tooltip("Immediate child of this object which contains all other UI.")]
		public GameObject mainContainer;

		// Token: 0x040048F1 RID: 18673
		[NonSerialized]
		public CameraRigController cameraRigController;

		// Token: 0x040048F2 RID: 18674
		public GameObject scoreboardPanel;

		// Token: 0x040048F3 RID: 18675
		public GameObject mainUIPanel;

		// Token: 0x040048F4 RID: 18676
		public GameObject cinematicPanel;

		// Token: 0x040048F5 RID: 18677
		public CombatHealthBarViewer combatHealthBarViewer;

		// Token: 0x040048F6 RID: 18678
		public ContextManager contextManager;

		// Token: 0x040048F7 RID: 18679
		public AllyCardManager allyCardManager;

		// Token: 0x040048F8 RID: 18680
		public Transform gameModeUiRoot;

		// Token: 0x040048F9 RID: 18681
		[Header("Character")]
		public HealthBar healthBar;

		// Token: 0x040048FA RID: 18682
		public ExpBar expBar;

		// Token: 0x040048FB RID: 18683
		public LevelText levelText;

		// Token: 0x040048FC RID: 18684
		public BuffDisplay buffDisplay;

		// Token: 0x040048FD RID: 18685
		public MoneyText moneyText;

		// Token: 0x040048FE RID: 18686
		public GameObject lunarCoinContainer;

		// Token: 0x040048FF RID: 18687
		public MoneyText lunarCoinText;

		// Token: 0x04004900 RID: 18688
		public GameObject voidCoinContainer;

		// Token: 0x04004901 RID: 18689
		public MoneyText voidCoinText;

		// Token: 0x04004902 RID: 18690
		public SkillIcon[] skillIcons;

		// Token: 0x04004903 RID: 18691
		public EquipmentIcon[] equipmentIcons;

		// Token: 0x04004904 RID: 18692
		public ItemInventoryDisplay itemInventoryDisplay;

		// Token: 0x04004905 RID: 18693
		[Header("Debug")]
		public HUDSpeedometer speedometer;

		// Token: 0x04004906 RID: 18694
		private MPEventSystemProvider eventSystemProvider;

		// Token: 0x04004907 RID: 18695
		private Canvas canvas;

		// Token: 0x04004909 RID: 18697
		private GameObject previousTargetBodyObject;

		// Token: 0x0400490B RID: 18699
		public static readonly BoolConVar cvHudEnable = new BoolConVar("hud_enable", ConVarFlags.Archive, "1", "Enable/disable the HUD.");

		// Token: 0x02000D0C RID: 3340
		// (Invoke) Token: 0x06004C26 RID: 19494
		public delegate void ShouldHudDisplayDelegate(HUD hud, ref bool shouldDisplay);
	}
}
