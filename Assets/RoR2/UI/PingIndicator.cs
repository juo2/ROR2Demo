using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D66 RID: 3430
	public class PingIndicator : MonoBehaviour
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x00144970 File Offset: 0x00142B70
		// (set) Token: 0x06004EA1 RID: 20129 RVA: 0x00144978 File Offset: 0x00142B78
		public Vector3 pingOrigin { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x00144981 File Offset: 0x00142B81
		// (set) Token: 0x06004EA3 RID: 20131 RVA: 0x00144989 File Offset: 0x00142B89
		public Vector3 pingNormal { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x00144992 File Offset: 0x00142B92
		// (set) Token: 0x06004EA5 RID: 20133 RVA: 0x0014499A File Offset: 0x00142B9A
		public GameObject pingOwner { get; set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x001449A3 File Offset: 0x00142BA3
		// (set) Token: 0x06004EA7 RID: 20135 RVA: 0x001449AB File Offset: 0x00142BAB
		public GameObject pingTarget { get; set; }

		// Token: 0x06004EA8 RID: 20136 RVA: 0x001449B4 File Offset: 0x00142BB4
		public static Sprite GetInteractableIcon(GameObject gameObject)
		{
			PingInfoProvider component = gameObject.GetComponent<PingInfoProvider>();
			if (component && component.pingIconOverride)
			{
				return component.pingIconOverride;
			}
			string path = "Textures/MiscIcons/texMysteryIcon";
			if (gameObject.GetComponent<BarrelInteraction>())
			{
				path = "Textures/MiscIcons/texBarrelIcon";
			}
			else if (gameObject.name.Contains("Shrine"))
			{
				path = "Textures/MiscIcons/texShrineIconOutlined";
			}
			else if (gameObject.GetComponent<GenericPickupController>() || gameObject.GetComponent<PickupPickerController>())
			{
				path = "Textures/MiscIcons/texLootIconOutlined";
			}
			else if (gameObject.GetComponent<SummonMasterBehavior>())
			{
				path = "Textures/MiscIcons/texDroneIconOutlined";
			}
			else if (gameObject.GetComponent<TeleporterInteraction>())
			{
				path = "Textures/MiscIcons/texTeleporterIconOutlined";
			}
			else
			{
				PortalStatueBehavior component2 = gameObject.GetComponent<PortalStatueBehavior>();
				if (component2 && component2.portalType == PortalStatueBehavior.PortalType.Shop)
				{
					path = "Textures/MiscIcons/texMysteryIcon";
				}
				else
				{
					PurchaseInteraction component3 = gameObject.GetComponent<PurchaseInteraction>();
					if (component3 && component3.costType == CostTypeIndex.LunarCoin)
					{
						path = "Textures/MiscIcons/texMysteryIcon";
					}
					else if (component3 || gameObject.GetComponent<TimedChestController>())
					{
						path = "Textures/MiscIcons/texInventoryIconOutlined";
					}
				}
			}
			return LegacyResourcesAPI.Load<Sprite>(path);
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00144ACE File Offset: 0x00142CCE
		private void OnEnable()
		{
			PingIndicator.instancesList.Add(this);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x00144ADB File Offset: 0x00142CDB
		private void OnDisable()
		{
			PingIndicator.instancesList.Remove(this);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00144AEC File Offset: 0x00142CEC
		public void RebuildPing()
		{
			base.transform.rotation = Util.QuaternionSafeLookRotation(this.pingNormal);
			base.transform.position = (this.pingTarget ? this.pingTarget.transform.position : this.pingOrigin);
			base.transform.localScale = Vector3.one;
			this.positionIndicator.targetTransform = (this.pingTarget ? this.pingTarget.transform : null);
			this.positionIndicator.defaultPosition = base.transform.position;
			IDisplayNameProvider displayNameProvider = this.pingTarget ? this.pingTarget.GetComponentInParent<IDisplayNameProvider>() : null;
			ModelLocator modelLocator = null;
			this.pingType = PingIndicator.PingType.Default;
			this.pingObjectScaleCurve.enabled = false;
			this.pingObjectScaleCurve.enabled = true;
			GameObject[] array = this.defaultPingGameObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			array = this.enemyPingGameObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			array = this.interactablePingGameObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			if (this.pingTarget)
			{
				Debug.LogFormat("Ping target {0}", new object[]
				{
					this.pingTarget
				});
				modelLocator = this.pingTarget.GetComponent<ModelLocator>();
				if (displayNameProvider != null)
				{
					CharacterBody component = this.pingTarget.GetComponent<CharacterBody>();
					if (component)
					{
						this.pingType = PingIndicator.PingType.Enemy;
						this.targetTransformToFollow = component.coreTransform;
					}
					else
					{
						this.pingType = PingIndicator.PingType.Interactable;
					}
				}
			}
			string bestMasterName = Util.GetBestMasterName(this.pingOwner.GetComponent<CharacterMaster>());
			string text = ((MonoBehaviour)displayNameProvider) ? Util.GetBestBodyName(((MonoBehaviour)displayNameProvider).gameObject) : "";
			this.pingText.enabled = true;
			this.pingText.text = bestMasterName;
			switch (this.pingType)
			{
			case PingIndicator.PingType.Default:
				this.pingColor = this.defaultPingColor;
				this.pingDuration = this.defaultPingDuration;
				this.pingHighlight.isOn = false;
				array = this.defaultPingGameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(true);
				}
				Chat.AddMessage(string.Format(Language.GetString("PLAYER_PING_DEFAULT"), bestMasterName));
				break;
			case PingIndicator.PingType.Enemy:
				this.pingColor = this.enemyPingColor;
				this.pingDuration = this.enemyPingDuration;
				array = this.enemyPingGameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(true);
				}
				if (modelLocator)
				{
					Transform modelTransform = modelLocator.modelTransform;
					if (modelTransform)
					{
						CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
						if (component2)
						{
							bool flag = false;
							foreach (CharacterModel.RendererInfo rendererInfo in component2.baseRendererInfos)
							{
								if (!rendererInfo.ignoreOverlays && !flag)
								{
									this.pingHighlight.highlightColor = Highlight.HighlightColor.teleporter;
									this.pingHighlight.targetRenderer = rendererInfo.renderer;
									this.pingHighlight.strength = 1f;
									this.pingHighlight.isOn = true;
									flag = true;
								}
							}
						}
					}
					Chat.AddMessage(string.Format(Language.GetString("PLAYER_PING_ENEMY"), bestMasterName, text));
				}
				break;
			case PingIndicator.PingType.Interactable:
			{
				this.pingColor = this.interactablePingColor;
				this.pingDuration = this.interactablePingDuration;
				this.pingTargetPurchaseInteraction = this.pingTarget.GetComponent<PurchaseInteraction>();
				Sprite interactableIcon = PingIndicator.GetInteractableIcon(this.pingTarget);
				SpriteRenderer component3 = this.interactablePingGameObjects[0].GetComponent<SpriteRenderer>();
				ShopTerminalBehavior component4 = this.pingTarget.GetComponent<ShopTerminalBehavior>();
				if (component4)
				{
					PickupIndex pickupIndex = component4.CurrentPickupIndex();
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string format = "{0} ({1})";
					object arg = text;
					object arg2;
					if (!component4.pickupIndexIsHidden && component4.pickupDisplay)
					{
						PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
						arg2 = Language.GetString(((pickupDef != null) ? pickupDef.nameToken : null) ?? PickupCatalog.invalidPickupToken);
					}
					else
					{
						arg2 = "?";
					}
					text = string.Format(invariantCulture, format, arg, arg2);
				}
				else if (!this.pingTarget.gameObject.name.Contains("Shrine") && (this.pingTarget.GetComponent<GenericPickupController>() || this.pingTarget.GetComponent<PickupPickerController>() || this.pingTarget.GetComponent<TeleporterInteraction>()))
				{
					this.pingDuration = 60f;
				}
				array = this.interactablePingGameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(true);
				}
				Renderer componentInChildren;
				if (modelLocator)
				{
					componentInChildren = modelLocator.modelTransform.GetComponentInChildren<Renderer>();
				}
				else
				{
					componentInChildren = this.pingTarget.GetComponentInChildren<Renderer>();
				}
				if (componentInChildren)
				{
					this.pingHighlight.highlightColor = Highlight.HighlightColor.interactive;
					this.pingHighlight.targetRenderer = componentInChildren;
					this.pingHighlight.strength = 1f;
					this.pingHighlight.isOn = true;
				}
				component3.sprite = interactableIcon;
				if (this.pingTargetPurchaseInteraction && this.pingTargetPurchaseInteraction.costType != CostTypeIndex.None)
				{
					PingIndicator.sharedStringBuilder.Clear();
					CostTypeCatalog.GetCostTypeDef(this.pingTargetPurchaseInteraction.costType).BuildCostStringStyled(this.pingTargetPurchaseInteraction.cost, PingIndicator.sharedStringBuilder, false, true);
					Chat.AddMessage(string.Format(Language.GetString("PLAYER_PING_INTERACTABLE_WITH_COST"), bestMasterName, text, PingIndicator.sharedStringBuilder.ToString()));
				}
				else
				{
					Chat.AddMessage(string.Format(Language.GetString("PLAYER_PING_INTERACTABLE"), bestMasterName, text));
				}
				break;
			}
			}
			this.pingText.color = this.textBaseColor * this.pingColor;
			this.fixedTimer = this.pingDuration;
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x001450CC File Offset: 0x001432CC
		private void Update()
		{
			if (this.pingType == PingIndicator.PingType.Interactable && this.pingTargetPurchaseInteraction && !this.pingTargetPurchaseInteraction.available)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			this.fixedTimer -= Time.deltaTime;
			if (this.fixedTimer <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00145134 File Offset: 0x00143334
		private void LateUpdate()
		{
			if (!this.pingTarget)
			{
				if (this.pingTarget != null)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				return;
			}
			if (this.targetTransformToFollow)
			{
				base.transform.SetPositionAndRotation(this.targetTransformToFollow.position, this.targetTransformToFollow.rotation);
			}
		}

		// Token: 0x04004B43 RID: 19267
		public PositionIndicator positionIndicator;

		// Token: 0x04004B44 RID: 19268
		public TextMeshPro pingText;

		// Token: 0x04004B45 RID: 19269
		public Highlight pingHighlight;

		// Token: 0x04004B46 RID: 19270
		public ObjectScaleCurve pingObjectScaleCurve;

		// Token: 0x04004B47 RID: 19271
		public GameObject positionIndicatorRoot;

		// Token: 0x04004B48 RID: 19272
		public Color textBaseColor;

		// Token: 0x04004B49 RID: 19273
		public GameObject[] defaultPingGameObjects;

		// Token: 0x04004B4A RID: 19274
		public Color defaultPingColor;

		// Token: 0x04004B4B RID: 19275
		public float defaultPingDuration;

		// Token: 0x04004B4C RID: 19276
		public GameObject[] enemyPingGameObjects;

		// Token: 0x04004B4D RID: 19277
		public Color enemyPingColor;

		// Token: 0x04004B4E RID: 19278
		public float enemyPingDuration;

		// Token: 0x04004B4F RID: 19279
		public GameObject[] interactablePingGameObjects;

		// Token: 0x04004B50 RID: 19280
		public Color interactablePingColor;

		// Token: 0x04004B51 RID: 19281
		public float interactablePingDuration;

		// Token: 0x04004B52 RID: 19282
		public static List<PingIndicator> instancesList = new List<PingIndicator>();

		// Token: 0x04004B53 RID: 19283
		private PingIndicator.PingType pingType;

		// Token: 0x04004B54 RID: 19284
		private Color pingColor;

		// Token: 0x04004B55 RID: 19285
		private float pingDuration;

		// Token: 0x04004B56 RID: 19286
		private PurchaseInteraction pingTargetPurchaseInteraction;

		// Token: 0x04004B5B RID: 19291
		private Transform targetTransformToFollow;

		// Token: 0x04004B5C RID: 19292
		private float fixedTimer;

		// Token: 0x04004B5D RID: 19293
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x02000D67 RID: 3431
		public enum PingType
		{
			// Token: 0x04004B5F RID: 19295
			Default,
			// Token: 0x04004B60 RID: 19296
			Enemy,
			// Token: 0x04004B61 RID: 19297
			Interactable,
			// Token: 0x04004B62 RID: 19298
			Count
		}
	}
}
