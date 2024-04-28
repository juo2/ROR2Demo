using System;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000534 RID: 1332
	[CreateAssetMenu(menuName = "RoR2/EquipmentDef")]
	public class EquipmentDef : ScriptableObject
	{
		// Token: 0x0600183C RID: 6204 RVA: 0x0006A2F4 File Offset: 0x000684F4
		public static void AttemptGrant(ref PickupDef.GrantContext context)
		{
			context.controller.StartWaitTime();
			Inventory inventory = context.body.inventory;
			EquipmentIndex currentEquipmentIndex = inventory.currentEquipmentIndex;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(context.controller.pickupIndex);
			EquipmentIndex equipmentIndex = (pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None;
			inventory.SetEquipmentIndex(equipmentIndex);
			context.controller.NetworkpickupIndex = PickupCatalog.FindPickupIndex(currentEquipmentIndex);
			context.shouldDestroy = false;
			context.shouldNotify = true;
			if (context.controller.pickupIndex == PickupIndex.none)
			{
				context.shouldDestroy = true;
			}
			if (context.controller.selfDestructIfPickupIndexIsNotIdeal && context.controller.pickupIndex != PickupCatalog.FindPickupIndex(context.controller.idealPickupIndex.pickupName))
			{
				PickupDropletController.CreatePickupDroplet(context.controller.pickupIndex, context.controller.transform.position, new Vector3(UnityEngine.Random.Range(-4f, 4f), 20f, UnityEngine.Random.Range(-4f, 4f)));
				context.shouldDestroy = true;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0006A404 File Offset: 0x00068604
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x0006A474 File Offset: 0x00068674
		public EquipmentIndex equipmentIndex
		{
			get
			{
				if (this._equipmentIndex == EquipmentIndex.None)
				{
					Debug.LogError("EquipmentDef '" + base.name + "' has an equipment index of 'None'.  Attempting to fix...");
					this._equipmentIndex = EquipmentCatalog.FindEquipmentIndex(base.name);
					if (this._equipmentIndex != EquipmentIndex.None)
					{
						Debug.LogError(string.Format("Able to fix EquipmentDef '{0}' (equipment index = {1}).  This is probably because the asset is being duplicated across bundles.", base.name, this._equipmentIndex));
					}
				}
				return this._equipmentIndex;
			}
			set
			{
				this._equipmentIndex = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0006A47D File Offset: 0x0006867D
		public Texture pickupIconTexture
		{
			get
			{
				if (!this.pickupIconSprite)
				{
					return null;
				}
				return this.pickupIconSprite.texture;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0006A499 File Offset: 0x00068699
		public Texture bgIconTexture
		{
			get
			{
				if (this.isLunar)
				{
					return LegacyResourcesAPI.Load<Texture>("Textures/ItemIcons/BG/texLunarBGIcon");
				}
				return LegacyResourcesAPI.Load<Texture>("Textures/ItemIcons/BG/texEquipmentBGIcon");
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0006A4B8 File Offset: 0x000686B8
		[ContextMenu("Auto Populate Tokens")]
		public void AutoPopulateTokens()
		{
			string arg = base.name.ToUpperInvariant();
			this.nameToken = string.Format("EQUIPMENT_{0}_NAME", arg);
			this.pickupToken = string.Format("EQUIPMENT_{0}_PICKUP", arg);
			this.descriptionToken = string.Format("EQUIPMENT_{0}_DESC", arg);
			this.loreToken = string.Format("EQUIPMENT_{0}_LORE", arg);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0006A518 File Offset: 0x00068718
		public virtual PickupDef CreatePickupDef()
		{
			PickupDef pickupDef = new PickupDef();
			pickupDef.internalName = "EquipmentIndex." + base.name;
			pickupDef.equipmentIndex = this.equipmentIndex;
			pickupDef.displayPrefab = this.pickupModelPrefab;
			pickupDef.dropletDisplayPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/ItemPickups/EquipmentOrb");
			pickupDef.nameToken = this.nameToken;
			pickupDef.baseColor = ColorCatalog.GetColor(this.colorIndex);
			pickupDef.darkColor = pickupDef.baseColor;
			pickupDef.unlockableDef = this.unlockableDef;
			pickupDef.interactContextToken = "EQUIPMENT_PICKUP_CONTEXT";
			pickupDef.isLunar = this.isLunar;
			pickupDef.isBoss = this.isBoss;
			pickupDef.iconTexture = this.pickupIconTexture;
			pickupDef.iconSprite = this.pickupIconSprite;
			pickupDef.attemptGrant = new PickupDef.AttemptGrantDelegate(EquipmentDef.AttemptGrant);
			return pickupDef;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0006A5F0 File Offset: 0x000687F0
		[ConCommand(commandName = "equipment_migrate", flags = ConVarFlags.None, helpText = "Generates EquipmentDef assets from the existing catalog entries.")]
		private static void CCEquipmentMigrate(ConCommandArgs args)
		{
			for (EquipmentIndex equipmentIndex = (EquipmentIndex)0; equipmentIndex < (EquipmentIndex)EquipmentCatalog.equipmentCount; equipmentIndex++)
			{
				EditorUtil.CopyToScriptableObject<EquipmentDef, EquipmentDef>(EquipmentCatalog.GetEquipmentDef(equipmentIndex), "Assets/RoR2/Resources/EquipmentDefs/");
			}
		}

		// Token: 0x04001DD2 RID: 7634
		private EquipmentIndex _equipmentIndex = EquipmentIndex.None;

		// Token: 0x04001DD3 RID: 7635
		public GameObject pickupModelPrefab;

		// Token: 0x04001DD4 RID: 7636
		public float cooldown;

		// Token: 0x04001DD5 RID: 7637
		public string nameToken;

		// Token: 0x04001DD6 RID: 7638
		public string pickupToken;

		// Token: 0x04001DD7 RID: 7639
		public string descriptionToken;

		// Token: 0x04001DD8 RID: 7640
		public string loreToken;

		// Token: 0x04001DD9 RID: 7641
		public Sprite pickupIconSprite;

		// Token: 0x04001DDA RID: 7642
		public UnlockableDef unlockableDef;

		// Token: 0x04001DDB RID: 7643
		public ColorCatalog.ColorIndex colorIndex = ColorCatalog.ColorIndex.Equipment;

		// Token: 0x04001DDC RID: 7644
		public bool canDrop;

		// Token: 0x04001DDD RID: 7645
		[Range(0f, 1f)]
		public float dropOnDeathChance;

		// Token: 0x04001DDE RID: 7646
		public bool canBeRandomlyTriggered = true;

		// Token: 0x04001DDF RID: 7647
		public bool enigmaCompatible;

		// Token: 0x04001DE0 RID: 7648
		public bool isLunar;

		// Token: 0x04001DE1 RID: 7649
		public bool isBoss;

		// Token: 0x04001DE2 RID: 7650
		public BuffDef passiveBuffDef;

		// Token: 0x04001DE3 RID: 7651
		public bool appearsInSinglePlayer = true;

		// Token: 0x04001DE4 RID: 7652
		public bool appearsInMultiPlayer = true;

		// Token: 0x04001DE5 RID: 7653
		public ExpansionDef requiredExpansion;

		// Token: 0x04001DE6 RID: 7654
		[Obsolete]
		[HideInInspector]
		public MageElement mageElement;
	}
}
