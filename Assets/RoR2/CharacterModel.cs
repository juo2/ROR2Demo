using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RoR2.UI;
using RoR2.WwiseUtils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x0200063E RID: 1598
	public class CharacterModel : MonoBehaviour
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x000836A7 File Offset: 0x000818A7
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x000836AF File Offset: 0x000818AF
		public VisibilityLevel visibility
		{
			get
			{
				return this._visibility;
			}
			set
			{
				if (this._visibility != value)
				{
					this._visibility = value;
					this.materialsDirty = true;
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000836C8 File Offset: 0x000818C8
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x000836D0 File Offset: 0x000818D0
		public bool isGhost
		{
			get
			{
				return this._isGhost;
			}
			set
			{
				if (this._isGhost != value)
				{
					this._isGhost = value;
					this.materialsDirty = true;
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000836E9 File Offset: 0x000818E9
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x000836F1 File Offset: 0x000818F1
		public bool isDoppelganger
		{
			get
			{
				return this._isDoppelganger;
			}
			set
			{
				if (this._isDoppelganger != value)
				{
					this._isDoppelganger = value;
					this.materialsDirty = true;
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0008370A File Offset: 0x0008190A
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x00083712 File Offset: 0x00081912
		public bool isEcho
		{
			get
			{
				return this._isEcho;
			}
			set
			{
				if (this._isEcho != value)
				{
					this._isEcho = value;
					this.materialsDirty = true;
				}
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0008372C File Offset: 0x0008192C
		private void Awake()
		{
			this.enabledItemDisplays = ItemMask.Rent();
			this.childLocator = base.GetComponent<ChildLocator>();
			HurtBoxGroup component = base.GetComponent<HurtBoxGroup>();
			this.coreTransform = base.transform;
			if (component)
			{
				HurtBox mainHurtBox = component.mainHurtBox;
				this.coreTransform = (((mainHurtBox != null) ? mainHurtBox.transform : null) ?? this.coreTransform);
				HurtBox[] hurtBoxes = component.hurtBoxes;
				if (hurtBoxes.Length != 0)
				{
					this.hurtBoxInfos = new CharacterModel.HurtBoxInfo[hurtBoxes.Length];
					for (int i = 0; i < hurtBoxes.Length; i++)
					{
						this.hurtBoxInfos[i] = new CharacterModel.HurtBoxInfo(hurtBoxes[i]);
					}
				}
			}
			this.propertyStorage = new MaterialPropertyBlock();
			foreach (CharacterModel.RendererInfo rendererInfo in this.baseRendererInfos)
			{
				if (rendererInfo.renderer is SkinnedMeshRenderer)
				{
					this.mainSkinnedMeshRenderer = (SkinnedMeshRenderer)rendererInfo.renderer;
					break;
				}
			}
			if (this.body && Util.IsPrefab(this.body.gameObject) && !Util.IsPrefab(base.gameObject))
			{
				this.body = null;
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0008384B File Offset: 0x00081A4B
		private void Start()
		{
			this.visibility = VisibilityLevel.Invisible;
			this.UpdateMaterials();
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0008385C File Offset: 0x00081A5C
		private void OnEnable()
		{
			InstanceTracker.Add<CharacterModel>(this);
			if (this.body != null)
			{
				this.rtpcEliteEnemy = new RtpcSetter("eliteEnemy", this.body.gameObject);
				this.body.onInventoryChanged += this.OnInventoryChanged;
			}
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000838A9 File Offset: 0x00081AA9
		private void OnDisable()
		{
			this.InstanceUpdate();
			if (this.body != null)
			{
				this.body.onInventoryChanged -= this.OnInventoryChanged;
			}
			InstanceTracker.Remove<CharacterModel>(this);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000838D6 File Offset: 0x00081AD6
		private void OnDestroy()
		{
			ItemMask.Return(this.enabledItemDisplays);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000838E4 File Offset: 0x00081AE4
		private void OnInventoryChanged()
		{
			if (this.body)
			{
				Inventory inventory = this.body.inventory;
				if (inventory)
				{
					this.UpdateItemDisplay(inventory);
					this.inventoryEquipmentIndex = inventory.GetEquipmentIndex();
					this.SetEquipmentDisplay(this.inventoryEquipmentIndex);
				}
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00083934 File Offset: 0x00081B34
		private void InstanceUpdate()
		{
			if (this.isGhost)
			{
				this.particleMaterialOverride = CharacterModel.ghostParticleReplacementMaterial;
			}
			else if (this.myEliteIndex == RoR2Content.Elites.Poison.eliteIndex)
			{
				this.lightColorOverride = new Color?(CharacterModel.poisonEliteLightColor);
				this.particleMaterialOverride = CharacterModel.elitePoisonParticleReplacementMaterial;
			}
			else if (this.myEliteIndex == RoR2Content.Elites.Haunted.eliteIndex)
			{
				this.lightColorOverride = new Color?(CharacterModel.hauntedEliteLightColor);
				this.particleMaterialOverride = CharacterModel.eliteHauntedParticleReplacementMaterial;
			}
			else if (this.myEliteIndex == RoR2Content.Elites.Lunar.eliteIndex)
			{
				this.lightColorOverride = new Color?(CharacterModel.lunarEliteLightColor);
				this.particleMaterialOverride = CharacterModel.eliteLunarParticleReplacementMaterial;
			}
			else if (this.myEliteIndex == DLC1Content.Elites.Void.eliteIndex && this.body && this.body.healthComponent.alive)
			{
				this.lightColorOverride = new Color?(CharacterModel.voidEliteLightColor);
				this.particleMaterialOverride = CharacterModel.eliteVoidParticleReplacementMaterial;
			}
			else
			{
				this.lightColorOverride = null;
				this.particleMaterialOverride = null;
			}
			this.UpdateGoldAffix();
			this.UpdatePoisonAffix();
			this.UpdateHauntedAffix();
			this.UpdateVoidAffix();
			this.UpdateLights();
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00083A6C File Offset: 0x00081C6C
		private void UpdateLights()
		{
			CharacterModel.LightInfo[] array = this.baseLightInfos;
			if (array.Length == 0)
			{
				return;
			}
			if (this.lightColorOverride != null)
			{
				Color value = this.lightColorOverride.Value;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].light.color = value;
				}
				return;
			}
			for (int j = 0; j < array.Length; j++)
			{
				ref CharacterModel.LightInfo ptr = ref array[j];
				ptr.light.color = ptr.defaultColor;
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00083AE9 File Offset: 0x00081CE9
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			RoR2Application.onLateUpdate += CharacterModel.StaticUpdate;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00083AFC File Offset: 0x00081CFC
		private static void StaticUpdate()
		{
			foreach (CharacterModel characterModel in InstanceTracker.GetInstancesList<CharacterModel>())
			{
				characterModel.InstanceUpdate();
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00083B4C File Offset: 0x00081D4C
		private bool IsCurrentEliteType(EliteDef eliteDef)
		{
			return eliteDef != null && eliteDef.eliteIndex != EliteIndex.None && eliteDef.eliteIndex == this.myEliteIndex;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00083B6C File Offset: 0x00081D6C
		private void UpdateGoldAffix()
		{
			if (this.IsCurrentEliteType(JunkContent.Elites.Gold) != this.goldAffixEffect)
			{
				if (!this.goldAffixEffect)
				{
					this.goldAffixEffect = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/GoldAffixEffect"), base.transform);
					ParticleSystem.ShapeModule shape = this.goldAffixEffect.GetComponent<ParticleSystem>().shape;
					if (this.mainSkinnedMeshRenderer)
					{
						shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
						shape.skinnedMeshRenderer = this.mainSkinnedMeshRenderer;
						return;
					}
				}
				else
				{
					UnityEngine.Object.Destroy(this.goldAffixEffect);
					this.goldAffixEffect = null;
				}
			}
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00083C00 File Offset: 0x00081E00
		private void UpdatePoisonAffix()
		{
			if ((this.myEliteIndex == RoR2Content.Elites.Poison.eliteIndex && this.body.healthComponent.alive) != this.poisonAffixEffect)
			{
				if (!this.poisonAffixEffect)
				{
					this.poisonAffixEffect = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/PoisonAffixEffect"), base.transform);
					if (this.mainSkinnedMeshRenderer)
					{
						JitterBones[] components = this.poisonAffixEffect.GetComponents<JitterBones>();
						for (int i = 0; i < components.Length; i++)
						{
							components[i].skinnedMeshRenderer = this.mainSkinnedMeshRenderer;
						}
						return;
					}
				}
				else
				{
					UnityEngine.Object.Destroy(this.poisonAffixEffect);
					this.poisonAffixEffect = null;
				}
			}
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00083CB0 File Offset: 0x00081EB0
		private void UpdateHauntedAffix()
		{
			if ((this.myEliteIndex == RoR2Content.Elites.Haunted.eliteIndex && this.body.healthComponent.alive) != this.hauntedAffixEffect)
			{
				if (!this.hauntedAffixEffect)
				{
					this.hauntedAffixEffect = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/HauntedAffixEffect"), base.transform);
					if (this.mainSkinnedMeshRenderer)
					{
						JitterBones[] components = this.hauntedAffixEffect.GetComponents<JitterBones>();
						for (int i = 0; i < components.Length; i++)
						{
							components[i].skinnedMeshRenderer = this.mainSkinnedMeshRenderer;
						}
						return;
					}
				}
				else
				{
					UnityEngine.Object.Destroy(this.hauntedAffixEffect);
					this.hauntedAffixEffect = null;
				}
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00083D60 File Offset: 0x00081F60
		private void UpdateVoidAffix()
		{
			if ((this.myEliteIndex == DLC1Content.Elites.Void.eliteIndex && this.body.healthComponent.alive) != this.voidAffixEffect)
			{
				if (!this.voidAffixEffect)
				{
					this.voidAffixEffect = UnityEngine.Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/EliteVoid/VoidAffixEffect.prefab").WaitForCompletion(), base.transform);
					if (this.mainSkinnedMeshRenderer)
					{
						JitterBones[] components = this.voidAffixEffect.GetComponents<JitterBones>();
						for (int i = 0; i < components.Length; i++)
						{
							components[i].skinnedMeshRenderer = this.mainSkinnedMeshRenderer;
						}
						return;
					}
				}
				else
				{
					UnityEngine.Object.Destroy(this.voidAffixEffect);
					this.voidAffixEffect = null;
				}
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00083E18 File Offset: 0x00082018
		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				return;
			}
			for (int i = 0; i < this.baseLightInfos.Length; i++)
			{
				ref CharacterModel.LightInfo ptr = ref this.baseLightInfos[i];
				if (ptr.light)
				{
					ptr.defaultColor = ptr.light.color;
				}
			}
			if (!this.itemDisplayRuleSet)
			{
				Debug.LogErrorFormat("CharacterModel \"{0}\" does not have the itemDisplayRuleSet field assigned.", new object[]
				{
					base.gameObject
				});
			}
			if (this.autoPopulateLightInfos)
			{
				CharacterModel.LightInfo[] first = (from light in base.GetComponentsInChildren<Light>()
				select new CharacterModel.LightInfo(light)).ToArray<CharacterModel.LightInfo>();
				if (!first.SequenceEqual(this.baseLightInfos))
				{
					this.baseLightInfos = first;
				}
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00083EE0 File Offset: 0x000820E0
		private static void RefreshObstructorsForCamera(CameraRigController cameraRigController)
		{
			Vector3 position = cameraRigController.transform.position;
			foreach (CharacterModel characterModel in InstanceTracker.GetInstancesList<CharacterModel>())
			{
				if (cameraRigController.enableFading)
				{
					float nearestHurtBoxDistance = characterModel.GetNearestHurtBoxDistance(position);
					characterModel.fade = Mathf.Clamp01(Util.Remap(nearestHurtBoxDistance, cameraRigController.fadeStartDistance, cameraRigController.fadeEndDistance, 0f, 1f));
				}
				else
				{
					characterModel.fade = 1f;
				}
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00083F7C File Offset: 0x0008217C
		private float GetNearestHurtBoxDistance(Vector3 cameraPosition)
		{
			float num = float.PositiveInfinity;
			for (int i = 0; i < this.hurtBoxInfos.Length; i++)
			{
				float num2 = Vector3.Distance(this.hurtBoxInfos[i].transform.position, cameraPosition) - this.hurtBoxInfos[i].estimatedRadius;
				if (num2 < num)
				{
					num = Mathf.Min(num2, num);
				}
			}
			return num;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00083FE0 File Offset: 0x000821E0
		private void UpdateForCamera(CameraRigController cameraRigController)
		{
			this.visibility = VisibilityLevel.Visible;
			float target = 1f;
			if (this.body)
			{
				if (cameraRigController.firstPersonTarget == this.body.gameObject)
				{
					target = 0f;
				}
				this.visibility = this.body.GetVisibilityLevel(cameraRigController.targetTeamIndex);
			}
			this.firstPersonFade = Mathf.MoveTowards(this.firstPersonFade, target, Time.deltaTime / 0.25f);
			this.fade *= this.firstPersonFade;
			if (this.fade <= 0f || this.invisibilityCount > 0)
			{
				this.visibility = VisibilityLevel.Invisible;
			}
			this.UpdateOverlays();
			if (this.materialsDirty)
			{
				this.UpdateMaterials();
				this.materialsDirty = false;
			}
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000840A4 File Offset: 0x000822A4
		static CharacterModel()
		{
			SceneCamera.onSceneCameraPreRender += CharacterModel.OnSceneCameraPreRender;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000841C8 File Offset: 0x000823C8
		private static void OnSceneCameraPreRender(SceneCamera sceneCamera)
		{
			if (sceneCamera.cameraRigController)
			{
				CharacterModel.RefreshObstructorsForCamera(sceneCamera.cameraRigController);
			}
			if (sceneCamera.cameraRigController)
			{
				foreach (CharacterModel characterModel in InstanceTracker.GetInstancesList<CharacterModel>())
				{
					characterModel.UpdateForCamera(sceneCamera.cameraRigController);
				}
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00084244 File Offset: 0x00082444
		private void InstantiateDisplayRuleGroup(DisplayRuleGroup displayRuleGroup, ItemIndex itemIndex, EquipmentIndex equipmentIndex)
		{
			if (displayRuleGroup.rules != null)
			{
				for (int i = 0; i < displayRuleGroup.rules.Length; i++)
				{
					ItemDisplayRule itemDisplayRule = displayRuleGroup.rules[i];
					ItemDisplayRuleType ruleType = itemDisplayRule.ruleType;
					if (ruleType != ItemDisplayRuleType.ParentedPrefab)
					{
						if (ruleType == ItemDisplayRuleType.LimbMask)
						{
							CharacterModel.LimbMaskDisplay item = new CharacterModel.LimbMaskDisplay
							{
								itemIndex = itemIndex,
								equipmentIndex = equipmentIndex
							};
							item.Apply(this, itemDisplayRule.limbMask);
							this.limbMaskDisplays.Add(item);
						}
					}
					else if (this.childLocator)
					{
						Transform transform = this.childLocator.FindChild(itemDisplayRule.childName);
						if (transform)
						{
							CharacterModel.ParentedPrefabDisplay item2 = new CharacterModel.ParentedPrefabDisplay
							{
								itemIndex = itemIndex,
								equipmentIndex = equipmentIndex
							};
							item2.Apply(this, itemDisplayRule.followerPrefab, transform, itemDisplayRule.localPos, Quaternion.Euler(itemDisplayRule.localAngles), itemDisplayRule.localScale);
							this.parentedPrefabDisplays.Add(item2);
						}
					}
				}
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00084350 File Offset: 0x00082550
		private void SetEquipmentDisplay(EquipmentIndex newEquipmentIndex)
		{
			if (newEquipmentIndex == this.currentEquipmentDisplayIndex)
			{
				return;
			}
			for (int i = this.parentedPrefabDisplays.Count - 1; i >= 0; i--)
			{
				if (this.parentedPrefabDisplays[i].equipmentIndex != EquipmentIndex.None)
				{
					this.parentedPrefabDisplays[i].Undo();
					this.parentedPrefabDisplays.RemoveAt(i);
				}
			}
			for (int j = this.limbMaskDisplays.Count - 1; j >= 0; j--)
			{
				if (this.limbMaskDisplays[j].equipmentIndex != EquipmentIndex.None)
				{
					this.limbMaskDisplays[j].Undo(this);
					this.limbMaskDisplays.RemoveAt(j);
				}
			}
			this.currentEquipmentDisplayIndex = newEquipmentIndex;
			if (this.itemDisplayRuleSet)
			{
				DisplayRuleGroup equipmentDisplayRuleGroup = this.itemDisplayRuleSet.GetEquipmentDisplayRuleGroup(newEquipmentIndex);
				this.InstantiateDisplayRuleGroup(equipmentDisplayRuleGroup, ItemIndex.None, newEquipmentIndex);
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0008442C File Offset: 0x0008262C
		private void EnableItemDisplay(ItemIndex itemIndex)
		{
			if (this.enabledItemDisplays.Contains(itemIndex))
			{
				return;
			}
			this.enabledItemDisplays.Add(itemIndex);
			if (this.itemDisplayRuleSet)
			{
				DisplayRuleGroup itemDisplayRuleGroup = this.itemDisplayRuleSet.GetItemDisplayRuleGroup(itemIndex);
				this.InstantiateDisplayRuleGroup(itemDisplayRuleGroup, itemIndex, EquipmentIndex.None);
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00084478 File Offset: 0x00082678
		private void DisableItemDisplay(ItemIndex itemIndex)
		{
			if (!this.enabledItemDisplays.Contains(itemIndex))
			{
				return;
			}
			this.enabledItemDisplays.Remove(itemIndex);
			for (int i = this.parentedPrefabDisplays.Count - 1; i >= 0; i--)
			{
				if (this.parentedPrefabDisplays[i].itemIndex == itemIndex)
				{
					this.parentedPrefabDisplays[i].Undo();
					this.parentedPrefabDisplays.RemoveAt(i);
				}
			}
			for (int j = this.limbMaskDisplays.Count - 1; j >= 0; j--)
			{
				if (this.limbMaskDisplays[j].itemIndex == itemIndex)
				{
					this.limbMaskDisplays[j].Undo(this);
					this.limbMaskDisplays.RemoveAt(j);
				}
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0008453C File Offset: 0x0008273C
		public void UpdateItemDisplay(Inventory inventory)
		{
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				if (inventory.GetItemCount(itemIndex) > 0)
				{
					this.EnableItemDisplay(itemIndex);
				}
				else
				{
					this.DisableItemDisplay(itemIndex);
				}
				itemIndex++;
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00084578 File Offset: 0x00082778
		public void HighlightItemDisplay(ItemIndex itemIndex)
		{
			if (!this.enabledItemDisplays.Contains(itemIndex))
			{
				return;
			}
			ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(ItemCatalog.GetItemDef(itemIndex).tier);
			GameObject highlightPrefab;
			if (itemTierDef && itemTierDef.highlightPrefab)
			{
				highlightPrefab = itemTierDef.highlightPrefab;
			}
			else
			{
				highlightPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/HighlightTier1Item");
			}
			for (int i = this.parentedPrefabDisplays.Count - 1; i >= 0; i--)
			{
				if (this.parentedPrefabDisplays[i].itemIndex == itemIndex)
				{
					GameObject instance = this.parentedPrefabDisplays[i].instance;
					if (instance)
					{
						Renderer componentInChildren = instance.GetComponentInChildren<Renderer>();
						if (componentInChildren && this.body)
						{
							HighlightRect.CreateHighlight(this.body.gameObject, componentInChildren, highlightPrefab, -1f, false);
						}
					}
				}
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00084654 File Offset: 0x00082854
		public List<GameObject> GetEquipmentDisplayObjects(EquipmentIndex equipmentIndex)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = this.parentedPrefabDisplays.Count - 1; i >= 0; i--)
			{
				if (this.parentedPrefabDisplays[i].equipmentIndex == equipmentIndex)
				{
					GameObject instance = this.parentedPrefabDisplays[i].instance;
					list.Add(instance);
				}
			}
			return list;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000846B0 File Offset: 0x000828B0
		public List<GameObject> GetItemDisplayObjects(ItemIndex itemIndex)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = this.parentedPrefabDisplays.Count - 1; i >= 0; i--)
			{
				if (this.parentedPrefabDisplays[i].itemIndex == itemIndex)
				{
					GameObject instance = this.parentedPrefabDisplays[i].instance;
					list.Add(instance);
				}
			}
			return list;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0008470C File Offset: 0x0008290C
		[RuntimeInitializeOnLoadMethod]
		private static void InitMaterials()
		{
			CharacterModel.revealedMaterial = LegacyResourcesAPI.Load<Material>("Materials/matRevealedEffect");
			CharacterModel.cloakedMaterial = LegacyResourcesAPI.Load<Material>("Materials/matCloakedEffect");
			CharacterModel.ghostMaterial = LegacyResourcesAPI.Load<Material>("Materials/matGhostEffect");
			CharacterModel.ghostParticleReplacementMaterial = LegacyResourcesAPI.Load<Material>("Materials/matGhostParticleReplacement");
			CharacterModel.wolfhatMaterial = LegacyResourcesAPI.Load<Material>("Materials/matWolfhatOverlay");
			CharacterModel.energyShieldMaterial = LegacyResourcesAPI.Load<Material>("Materials/matEnergyShield");
			CharacterModel.beetleJuiceMaterial = LegacyResourcesAPI.Load<Material>("Materials/matBeetleJuice");
			CharacterModel.brittleMaterial = LegacyResourcesAPI.Load<Material>("Materials/matBrittle");
			CharacterModel.fullCritMaterial = LegacyResourcesAPI.Load<Material>("Materials/matFullCrit");
			CharacterModel.clayGooMaterial = LegacyResourcesAPI.Load<Material>("Materials/matClayGooDebuff");
			CharacterModel.slow80Material = LegacyResourcesAPI.Load<Material>("Materials/matSlow80Debuff");
			CharacterModel.immuneMaterial = LegacyResourcesAPI.Load<Material>("Materials/matImmune");
			CharacterModel.bellBuffMaterial = LegacyResourcesAPI.Load<Material>("Materials/matBellBuff");
			CharacterModel.elitePoisonOverlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matElitePoisonOverlay");
			CharacterModel.elitePoisonParticleReplacementMaterial = LegacyResourcesAPI.Load<Material>("Materials/matElitePoisonParticleReplacement");
			CharacterModel.eliteHauntedOverlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matEliteHauntedOverlay");
			CharacterModel.eliteHauntedParticleReplacementMaterial = LegacyResourcesAPI.Load<Material>("Materials/matEliteHauntedParticleReplacement");
			CharacterModel.eliteJustHauntedOverlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matEliteJustHauntedOverlay");
			CharacterModel.eliteLunarParticleReplacementMaterial = LegacyResourcesAPI.Load<Material>("Materials/matEliteLunarParticleReplacement");
			CharacterModel.doppelgangerMaterial = LegacyResourcesAPI.Load<Material>("Materials/matDoppelganger");
			CharacterModel.weakMaterial = LegacyResourcesAPI.Load<Material>("Materials/matWeakOverlay");
			CharacterModel.pulverizedMaterial = LegacyResourcesAPI.Load<Material>("Materials/matPulverizedOverlay");
			CharacterModel.lunarGolemShieldMaterial = LegacyResourcesAPI.Load<Material>("Materials/matLunarGolemShield");
			CharacterModel.echoMaterial = LegacyResourcesAPI.Load<Material>("Materials/matEcho");
			CharacterModel.gummyCloneMaterial = LegacyResourcesAPI.Load<Material>("Materials/matGummyClone");
			CharacterModel.eliteVoidParticleReplacementMaterial = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/EliteVoid/matEliteVoidParticleReplacement.mat").WaitForCompletion();
			CharacterModel.eliteVoidOverlayMaterial = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/EliteVoid/matEliteVoidOverlay.mat").WaitForCompletion();
			CharacterModel.voidSurvivorCorruptMaterial = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/VoidSurvivor/matVoidSurvivorCorruptOverlay.mat").WaitForCompletion();
			CharacterModel.voidShieldMaterial = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/MissileVoid/matEnergyShieldVoid.mat").WaitForCompletion();
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000848EC File Offset: 0x00082AEC
		private void UpdateOverlays()
		{
			for (int i = 0; i < this.activeOverlayCount; i++)
			{
				this.currentOverlays[i] = null;
			}
			this.activeOverlayCount = 0;
			if (this.visibility == VisibilityLevel.Invisible)
			{
				return;
			}
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(this.inventoryEquipmentIndex);
			EliteIndex? eliteIndex;
			if (equipmentDef == null)
			{
				eliteIndex = null;
			}
			else
			{
				BuffDef passiveBuffDef = equipmentDef.passiveBuffDef;
				if (passiveBuffDef == null)
				{
					eliteIndex = null;
				}
				else
				{
					EliteDef eliteDef = passiveBuffDef.eliteDef;
					eliteIndex = ((eliteDef != null) ? new EliteIndex?(eliteDef.eliteIndex) : null);
				}
			}
			this.myEliteIndex = (eliteIndex ?? EliteIndex.None);
			int? num;
			if (equipmentDef == null)
			{
				num = null;
			}
			else
			{
				BuffDef passiveBuffDef2 = equipmentDef.passiveBuffDef;
				if (passiveBuffDef2 == null)
				{
					num = null;
				}
				else
				{
					EliteDef eliteDef2 = passiveBuffDef2.eliteDef;
					num = ((eliteDef2 != null) ? new int?(eliteDef2.shaderEliteRampIndex) : null);
				}
			}
			this.shaderEliteRampIndex = (num ?? -1);
			bool flag = false;
			bool flag2 = false;
			if (this.body)
			{
				flag = this.body.HasBuff(RoR2Content.Buffs.ClayGoo);
				flag2 = this.body.HasBuff(RoR2Content.Buffs.AffixHauntedRecipient);
				this.rtpcEliteEnemy.value = ((this.myEliteIndex != EliteIndex.None) ? 1f : 0f);
				this.rtpcEliteEnemy.FlushIfChanged();
				Inventory inventory = this.body.inventory;
				this.isGhost = (inventory != null && inventory.GetItemCount(RoR2Content.Items.Ghost) > 0);
				Inventory inventory2 = this.body.inventory;
				this.isDoppelganger = (inventory2 != null && inventory2.GetItemCount(RoR2Content.Items.InvadingDoppelganger) > 0);
				Inventory inventory3 = this.body.inventory;
				this.isEcho = (inventory3 != null && inventory3.GetItemCount(RoR2Content.Items.SummonedEcho) > 0);
				Inventory inventory4 = this.body.inventory;
				bool flag3 = inventory4 != null && inventory4.GetItemCount(DLC1Content.Items.MissileVoid) > 0;
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.ghostMaterial, this.isGhost);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.doppelgangerMaterial, this.isDoppelganger);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.clayGooMaterial, flag);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.elitePoisonOverlayMaterial, this.myEliteIndex == RoR2Content.Elites.Poison.eliteIndex || this.body.HasBuff(RoR2Content.Buffs.HealingDisabled));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.eliteHauntedOverlayMaterial, this.body.HasBuff(RoR2Content.Buffs.AffixHaunted));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.eliteVoidOverlayMaterial, this.body.HasBuff(DLC1Content.Buffs.EliteVoid) && this.body.healthComponent && this.body.healthComponent.alive);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.pulverizedMaterial, this.body.HasBuff(RoR2Content.Buffs.Pulverized));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.weakMaterial, this.body.HasBuff(RoR2Content.Buffs.Weak));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.fullCritMaterial, this.body.HasBuff(RoR2Content.Buffs.FullCrit));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.wolfhatMaterial, this.body.HasBuff(RoR2Content.Buffs.AttackSpeedOnCrit));
				this.<UpdateOverlays>g__AddOverlay|126_0(flag3 ? CharacterModel.voidShieldMaterial : CharacterModel.energyShieldMaterial, this.body.healthComponent && this.body.healthComponent.shield > 0f);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.beetleJuiceMaterial, this.body.HasBuff(RoR2Content.Buffs.BeetleJuice));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.immuneMaterial, this.body.HasBuff(RoR2Content.Buffs.Immune));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.slow80Material, this.body.HasBuff(RoR2Content.Buffs.Slow80));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.brittleMaterial, this.body.inventory && this.body.inventory.GetItemCount(RoR2Content.Items.LunarDagger) > 0);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.lunarGolemShieldMaterial, this.body.HasBuff(RoR2Content.Buffs.LunarShell));
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.echoMaterial, this.isEcho);
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.gummyCloneMaterial, this.IsGummyClone());
				this.<UpdateOverlays>g__AddOverlay|126_0(CharacterModel.voidSurvivorCorruptMaterial, this.body.HasBuff(DLC1Content.Buffs.VoidSurvivorCorruptMode));
			}
			if (this.wasPreviouslyClayGooed && !flag)
			{
				TemporaryOverlay temporaryOverlay = base.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 0.6f;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = CharacterModel.clayGooMaterial;
				temporaryOverlay.AddToCharacerModel(this);
			}
			if (this.wasPreviouslyHaunted != flag2)
			{
				TemporaryOverlay temporaryOverlay2 = base.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay2.duration = 0.5f;
				temporaryOverlay2.animateShaderAlpha = true;
				temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay2.destroyComponentOnEnd = true;
				temporaryOverlay2.originalMaterial = CharacterModel.eliteJustHauntedOverlayMaterial;
				temporaryOverlay2.AddToCharacerModel(this);
			}
			this.wasPreviouslyClayGooed = flag;
			this.wasPreviouslyHaunted = flag2;
			int num2 = 0;
			while (num2 < this.temporaryOverlays.Count && this.activeOverlayCount < CharacterModel.maxOverlays)
			{
				Material[] array = this.currentOverlays;
				int num3 = this.activeOverlayCount;
				this.activeOverlayCount = num3 + 1;
				array[num3] = this.temporaryOverlays[num2].materialInstance;
				num2++;
			}
			this.wasPreviouslyClayGooed = flag;
			this.wasPreviouslyHaunted = flag2;
			this.materialsDirty = true;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00084E5C File Offset: 0x0008305C
		[RuntimeInitializeOnLoadMethod]
		private static void InitSharedMaterialsArrays()
		{
			CharacterModel.sharedMaterialArrays = new Material[CharacterModel.maxMaterials + 1][];
			if (CharacterModel.maxMaterials > 0)
			{
				CharacterModel.sharedMaterialArrays[0] = Array.Empty<Material>();
				for (int i = 1; i < CharacterModel.sharedMaterialArrays.Length; i++)
				{
					CharacterModel.sharedMaterialArrays[i] = new Material[i];
				}
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00084EB0 File Offset: 0x000830B0
		private void UpdateRendererMaterials(Renderer renderer, Material defaultMaterial, bool ignoreOverlays)
		{
			Material material = null;
			switch (this.visibility)
			{
			case VisibilityLevel.Invisible:
				renderer.sharedMaterial = null;
				return;
			case VisibilityLevel.Cloaked:
				if (!ignoreOverlays)
				{
					ignoreOverlays = true;
					material = CharacterModel.cloakedMaterial;
				}
				break;
			case VisibilityLevel.Revealed:
				if (!ignoreOverlays)
				{
					material = CharacterModel.revealedMaterial;
				}
				break;
			case VisibilityLevel.Visible:
				if (!ignoreOverlays)
				{
					if (this.isDoppelganger)
					{
						material = CharacterModel.doppelgangerMaterial;
					}
					else if (this.isGhost)
					{
						material = CharacterModel.ghostMaterial;
					}
					else if (this.IsGummyClone())
					{
						material = CharacterModel.gummyCloneMaterial;
					}
					else
					{
						material = defaultMaterial;
					}
				}
				else
				{
					material = (this.particleMaterialOverride ? this.particleMaterialOverride : defaultMaterial);
				}
				break;
			}
			int num = ignoreOverlays ? 0 : this.activeOverlayCount;
			if (material)
			{
				num++;
			}
			Material[] array = CharacterModel.sharedMaterialArrays[num];
			int num2 = 0;
			if (material)
			{
				array[num2++] = material;
			}
			if (!ignoreOverlays)
			{
				for (int i = 0; i < this.activeOverlayCount; i++)
				{
					array[num2++] = this.currentOverlays[i];
				}
			}
			renderer.sharedMaterials = array;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00084FB4 File Offset: 0x000831B4
		private void UpdateMaterials()
		{
			Color value = Color.black;
			if (this.body && this.body.healthComponent)
			{
				float num = Mathf.Clamp01(1f - this.body.healthComponent.timeSinceLastHit / CharacterModel.hitFlashDuration);
				float num2 = Mathf.Pow(Mathf.Clamp01(1f - this.body.healthComponent.timeSinceLastHeal / CharacterModel.healFlashDuration), 0.5f);
				if (num2 > num)
				{
					value = CharacterModel.healFlashColor * num2;
				}
				else
				{
					value = ((this.body.healthComponent.shield > 0f) ? CharacterModel.hitFlashShieldColor : CharacterModel.hitFlashBaseColor) * num;
				}
			}
			if (this.visibility == VisibilityLevel.Invisible)
			{
				for (int i = this.baseRendererInfos.Length - 1; i >= 0; i--)
				{
					CharacterModel.RendererInfo rendererInfo = this.baseRendererInfos[i];
					rendererInfo.renderer.shadowCastingMode = ShadowCastingMode.Off;
					rendererInfo.renderer.enabled = false;
				}
			}
			else
			{
				for (int j = this.baseRendererInfos.Length - 1; j >= 0; j--)
				{
					CharacterModel.RendererInfo rendererInfo2 = this.baseRendererInfos[j];
					Renderer renderer = rendererInfo2.renderer;
					this.UpdateRendererMaterials(renderer, this.baseRendererInfos[j].defaultMaterial, this.baseRendererInfos[j].ignoreOverlays);
					renderer.shadowCastingMode = rendererInfo2.defaultShadowCastingMode;
					renderer.enabled = true;
					renderer.GetPropertyBlock(this.propertyStorage);
					this.propertyStorage.SetColor(CommonShaderProperties._FlashColor, value);
					this.propertyStorage.SetFloat(CommonShaderProperties._EliteIndex, (float)(this.shaderEliteRampIndex + 1));
					this.propertyStorage.SetInt(CommonShaderProperties._LimbPrimeMask, this.limbFlagSet.materialMaskValue);
					this.propertyStorage.SetFloat(CommonShaderProperties._Fade, this.fade);
					renderer.SetPropertyBlock(this.propertyStorage);
				}
			}
			for (int k = 0; k < this.parentedPrefabDisplays.Count; k++)
			{
				ItemDisplay itemDisplay = this.parentedPrefabDisplays[k].itemDisplay;
				itemDisplay.SetVisibilityLevel(this.visibility);
				for (int l = 0; l < itemDisplay.rendererInfos.Length; l++)
				{
					Renderer renderer2 = itemDisplay.rendererInfos[l].renderer;
					renderer2.GetPropertyBlock(this.propertyStorage);
					this.propertyStorage.SetColor(CommonShaderProperties._FlashColor, value);
					this.propertyStorage.SetFloat(CommonShaderProperties._Fade, this.fade);
					renderer2.SetPropertyBlock(this.propertyStorage);
				}
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00085258 File Offset: 0x00083458
		private bool IsGummyClone()
		{
			CharacterBody characterBody = this.body;
			if (characterBody == null)
			{
				return false;
			}
			Inventory inventory = characterBody.inventory;
			int? num = (inventory != null) ? new int?(inventory.GetItemCount(DLC1Content.Items.GummyCloneIdentifier)) : null;
			int num2 = 0;
			return num.GetValueOrDefault() > num2 & num != null;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000852AC File Offset: 0x000834AC
		public void OnDeath()
		{
			for (int i = 0; i < this.parentedPrefabDisplays.Count; i++)
			{
				this.parentedPrefabDisplays[i].itemDisplay.OnDeath();
			}
			this.InstanceUpdate();
			this.UpdateOverlays();
			this.UpdateMaterials();
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000853C0 File Offset: 0x000835C0
		[CompilerGenerated]
		private void <UpdateOverlays>g__AddOverlay|126_0(Material overlayMaterial, bool condition)
		{
			if (this.activeOverlayCount >= CharacterModel.maxOverlays)
			{
				return;
			}
			if (condition)
			{
				Material[] array = this.currentOverlays;
				int num = this.activeOverlayCount;
				this.activeOverlayCount = num + 1;
				array[num] = overlayMaterial;
			}
		}

		// Token: 0x04002436 RID: 9270
		public CharacterBody body;

		// Token: 0x04002437 RID: 9271
		public ItemDisplayRuleSet itemDisplayRuleSet;

		// Token: 0x04002438 RID: 9272
		public bool autoPopulateLightInfos = true;

		// Token: 0x04002439 RID: 9273
		[FormerlySerializedAs("rendererInfos")]
		public CharacterModel.RendererInfo[] baseRendererInfos = Array.Empty<CharacterModel.RendererInfo>();

		// Token: 0x0400243A RID: 9274
		public CharacterModel.LightInfo[] baseLightInfos = Array.Empty<CharacterModel.LightInfo>();

		// Token: 0x0400243B RID: 9275
		private ChildLocator childLocator;

		// Token: 0x0400243C RID: 9276
		private GameObject goldAffixEffect;

		// Token: 0x0400243D RID: 9277
		private CharacterModel.HurtBoxInfo[] hurtBoxInfos = Array.Empty<CharacterModel.HurtBoxInfo>();

		// Token: 0x0400243E RID: 9278
		private Transform coreTransform;

		// Token: 0x0400243F RID: 9279
		private static readonly Color hitFlashBaseColor = new Color32(193, 108, 51, byte.MaxValue);

		// Token: 0x04002440 RID: 9280
		private static readonly Color hitFlashShieldColor = new Color32(132, 159, byte.MaxValue, byte.MaxValue);

		// Token: 0x04002441 RID: 9281
		private static readonly Color healFlashColor = new Color32(104, 196, 49, byte.MaxValue);

		// Token: 0x04002442 RID: 9282
		private static readonly float hitFlashDuration = 0.15f;

		// Token: 0x04002443 RID: 9283
		private static readonly float healFlashDuration = 0.35f;

		// Token: 0x04002444 RID: 9284
		private VisibilityLevel _visibility = VisibilityLevel.Visible;

		// Token: 0x04002445 RID: 9285
		private bool _isGhost;

		// Token: 0x04002446 RID: 9286
		private bool _isDoppelganger;

		// Token: 0x04002447 RID: 9287
		private bool _isEcho;

		// Token: 0x04002448 RID: 9288
		[HideInInspector]
		[NonSerialized]
		public int invisibilityCount;

		// Token: 0x04002449 RID: 9289
		[NonSerialized]
		public List<TemporaryOverlay> temporaryOverlays = new List<TemporaryOverlay>();

		// Token: 0x0400244A RID: 9290
		private bool materialsDirty = true;

		// Token: 0x0400244B RID: 9291
		private MaterialPropertyBlock propertyStorage;

		// Token: 0x0400244C RID: 9292
		private EquipmentIndex inventoryEquipmentIndex = EquipmentIndex.None;

		// Token: 0x0400244D RID: 9293
		private EliteIndex myEliteIndex = EliteIndex.None;

		// Token: 0x0400244E RID: 9294
		private float fade = 1f;

		// Token: 0x0400244F RID: 9295
		private float firstPersonFade = 1f;

		// Token: 0x04002450 RID: 9296
		private SkinnedMeshRenderer mainSkinnedMeshRenderer;

		// Token: 0x04002451 RID: 9297
		private static readonly Color poisonEliteLightColor = new Color32(90, byte.MaxValue, 193, 204);

		// Token: 0x04002452 RID: 9298
		private static readonly Color hauntedEliteLightColor = new Color32(152, 228, 217, 204);

		// Token: 0x04002453 RID: 9299
		private static readonly Color lunarEliteLightColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 127);

		// Token: 0x04002454 RID: 9300
		private static readonly Color voidEliteLightColor = new Color32(151, 78, 132, 204);

		// Token: 0x04002455 RID: 9301
		private Color? lightColorOverride;

		// Token: 0x04002456 RID: 9302
		private Material particleMaterialOverride;

		// Token: 0x04002457 RID: 9303
		private GameObject poisonAffixEffect;

		// Token: 0x04002458 RID: 9304
		private GameObject hauntedAffixEffect;

		// Token: 0x04002459 RID: 9305
		private GameObject voidAffixEffect;

		// Token: 0x0400245A RID: 9306
		private float affixHauntedCloakLockoutDuration = 3f;

		// Token: 0x0400245B RID: 9307
		private EquipmentIndex currentEquipmentDisplayIndex = EquipmentIndex.None;

		// Token: 0x0400245C RID: 9308
		private ItemMask enabledItemDisplays;

		// Token: 0x0400245D RID: 9309
		private List<CharacterModel.ParentedPrefabDisplay> parentedPrefabDisplays = new List<CharacterModel.ParentedPrefabDisplay>();

		// Token: 0x0400245E RID: 9310
		private List<CharacterModel.LimbMaskDisplay> limbMaskDisplays = new List<CharacterModel.LimbMaskDisplay>();

		// Token: 0x0400245F RID: 9311
		private CharacterModel.LimbFlagSet limbFlagSet = new CharacterModel.LimbFlagSet();

		// Token: 0x04002460 RID: 9312
		public static Material revealedMaterial;

		// Token: 0x04002461 RID: 9313
		public static Material cloakedMaterial;

		// Token: 0x04002462 RID: 9314
		public static Material ghostMaterial;

		// Token: 0x04002463 RID: 9315
		public static Material bellBuffMaterial;

		// Token: 0x04002464 RID: 9316
		public static Material wolfhatMaterial;

		// Token: 0x04002465 RID: 9317
		public static Material energyShieldMaterial;

		// Token: 0x04002466 RID: 9318
		public static Material fullCritMaterial;

		// Token: 0x04002467 RID: 9319
		public static Material beetleJuiceMaterial;

		// Token: 0x04002468 RID: 9320
		public static Material brittleMaterial;

		// Token: 0x04002469 RID: 9321
		public static Material clayGooMaterial;

		// Token: 0x0400246A RID: 9322
		public static Material slow80Material;

		// Token: 0x0400246B RID: 9323
		public static Material immuneMaterial;

		// Token: 0x0400246C RID: 9324
		public static Material elitePoisonOverlayMaterial;

		// Token: 0x0400246D RID: 9325
		public static Material elitePoisonParticleReplacementMaterial;

		// Token: 0x0400246E RID: 9326
		public static Material eliteHauntedOverlayMaterial;

		// Token: 0x0400246F RID: 9327
		public static Material eliteJustHauntedOverlayMaterial;

		// Token: 0x04002470 RID: 9328
		public static Material eliteHauntedParticleReplacementMaterial;

		// Token: 0x04002471 RID: 9329
		public static Material eliteLunarParticleReplacementMaterial;

		// Token: 0x04002472 RID: 9330
		public static Material eliteVoidParticleReplacementMaterial;

		// Token: 0x04002473 RID: 9331
		public static Material eliteVoidOverlayMaterial;

		// Token: 0x04002474 RID: 9332
		public static Material weakMaterial;

		// Token: 0x04002475 RID: 9333
		public static Material pulverizedMaterial;

		// Token: 0x04002476 RID: 9334
		public static Material doppelgangerMaterial;

		// Token: 0x04002477 RID: 9335
		public static Material ghostParticleReplacementMaterial;

		// Token: 0x04002478 RID: 9336
		public static Material lunarGolemShieldMaterial;

		// Token: 0x04002479 RID: 9337
		public static Material echoMaterial;

		// Token: 0x0400247A RID: 9338
		public static Material gummyCloneMaterial;

		// Token: 0x0400247B RID: 9339
		public static Material voidSurvivorCorruptMaterial;

		// Token: 0x0400247C RID: 9340
		public static Material voidShieldMaterial;

		// Token: 0x0400247D RID: 9341
		private static readonly int maxOverlays = 6;

		// Token: 0x0400247E RID: 9342
		private Material[] currentOverlays = new Material[CharacterModel.maxOverlays];

		// Token: 0x0400247F RID: 9343
		private int activeOverlayCount;

		// Token: 0x04002480 RID: 9344
		private bool wasPreviouslyClayGooed;

		// Token: 0x04002481 RID: 9345
		private bool wasPreviouslyHaunted;

		// Token: 0x04002482 RID: 9346
		private RtpcSetter rtpcEliteEnemy;

		// Token: 0x04002483 RID: 9347
		private int shaderEliteRampIndex = -1;

		// Token: 0x04002484 RID: 9348
		private static Material[][] sharedMaterialArrays;

		// Token: 0x04002485 RID: 9349
		private static readonly int maxMaterials = 1 + CharacterModel.maxOverlays;

		// Token: 0x0200063F RID: 1599
		[Serializable]
		public struct RendererInfo : IEquatable<CharacterModel.RendererInfo>
		{
			// Token: 0x06001ECB RID: 7883 RVA: 0x000853F8 File Offset: 0x000835F8
			public bool Equals(CharacterModel.RendererInfo other)
			{
				return this.renderer == other.renderer && this.defaultMaterial == other.defaultMaterial && object.Equals(this.defaultShadowCastingMode, other.defaultShadowCastingMode) && object.Equals(this.ignoreOverlays, other.ignoreOverlays) && object.Equals(this.hideOnDeath, other.hideOnDeath);
			}

			// Token: 0x04002486 RID: 9350
			[PrefabReference]
			public Renderer renderer;

			// Token: 0x04002487 RID: 9351
			public Material defaultMaterial;

			// Token: 0x04002488 RID: 9352
			public ShadowCastingMode defaultShadowCastingMode;

			// Token: 0x04002489 RID: 9353
			public bool ignoreOverlays;

			// Token: 0x0400248A RID: 9354
			public bool hideOnDeath;
		}

		// Token: 0x02000640 RID: 1600
		[Serializable]
		public struct LightInfo
		{
			// Token: 0x06001ECC RID: 7884 RVA: 0x00085478 File Offset: 0x00083678
			public LightInfo(Light light)
			{
				this.light = light;
				this.defaultColor = light.color;
			}

			// Token: 0x0400248B RID: 9355
			public Light light;

			// Token: 0x0400248C RID: 9356
			public Color defaultColor;
		}

		// Token: 0x02000641 RID: 1601
		private struct HurtBoxInfo
		{
			// Token: 0x06001ECD RID: 7885 RVA: 0x0008548D File Offset: 0x0008368D
			public HurtBoxInfo(HurtBox hurtBox)
			{
				this.transform = hurtBox.transform;
				this.estimatedRadius = Util.SphereVolumeToRadius(hurtBox.volume);
			}

			// Token: 0x0400248D RID: 9357
			public readonly Transform transform;

			// Token: 0x0400248E RID: 9358
			public readonly float estimatedRadius;
		}

		// Token: 0x02000642 RID: 1602
		private struct ParentedPrefabDisplay
		{
			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000854AC File Offset: 0x000836AC
			// (set) Token: 0x06001ECF RID: 7887 RVA: 0x000854B4 File Offset: 0x000836B4
			public GameObject instance { get; private set; }

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000854BD File Offset: 0x000836BD
			// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x000854C5 File Offset: 0x000836C5
			public ItemDisplay itemDisplay { get; private set; }

			// Token: 0x06001ED2 RID: 7890 RVA: 0x000854D0 File Offset: 0x000836D0
			public void Apply(CharacterModel characterModel, GameObject prefab, Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
			{
				this.instance = UnityEngine.Object.Instantiate<GameObject>(prefab.gameObject, parent);
				this.instance.transform.localPosition = localPosition;
				this.instance.transform.localRotation = localRotation;
				this.instance.transform.localScale = localScale;
				LimbMatcher component = this.instance.GetComponent<LimbMatcher>();
				if (component && characterModel.childLocator)
				{
					component.SetChildLocator(characterModel.childLocator);
				}
				this.itemDisplay = this.instance.GetComponent<ItemDisplay>();
			}

			// Token: 0x06001ED3 RID: 7891 RVA: 0x00085563 File Offset: 0x00083763
			public void Undo()
			{
				if (this.instance)
				{
					UnityEngine.Object.Destroy(this.instance);
					this.instance = null;
				}
			}

			// Token: 0x0400248F RID: 9359
			public ItemIndex itemIndex;

			// Token: 0x04002490 RID: 9360
			public EquipmentIndex equipmentIndex;
		}

		// Token: 0x02000643 RID: 1603
		private struct LimbMaskDisplay
		{
			// Token: 0x06001ED4 RID: 7892 RVA: 0x00085584 File Offset: 0x00083784
			public void Apply(CharacterModel characterModel, LimbFlags mask)
			{
				this.maskValue = mask;
				characterModel.limbFlagSet.AddFlags(mask);
			}

			// Token: 0x06001ED5 RID: 7893 RVA: 0x00085599 File Offset: 0x00083799
			public void Undo(CharacterModel characterModel)
			{
				characterModel.limbFlagSet.RemoveFlags(this.maskValue);
			}

			// Token: 0x04002493 RID: 9363
			public ItemIndex itemIndex;

			// Token: 0x04002494 RID: 9364
			public EquipmentIndex equipmentIndex;

			// Token: 0x04002495 RID: 9365
			public LimbFlags maskValue;
		}

		// Token: 0x02000644 RID: 1604
		[Serializable]
		private class LimbFlagSet
		{
			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000855AC File Offset: 0x000837AC
			// (set) Token: 0x06001ED7 RID: 7895 RVA: 0x000855B4 File Offset: 0x000837B4
			public int materialMaskValue { get; private set; }

			// Token: 0x06001ED8 RID: 7896 RVA: 0x000855BD File Offset: 0x000837BD
			public LimbFlagSet()
			{
				this.materialMaskValue = 1;
			}

			// Token: 0x06001ED9 RID: 7897 RVA: 0x000855D8 File Offset: 0x000837D8
			static LimbFlagSet()
			{
				int[] array = new int[]
				{
					2,
					3,
					5,
					11,
					17
				};
				CharacterModel.LimbFlagSet.primeConversionTable = new int[31];
				for (int i = 0; i < CharacterModel.LimbFlagSet.primeConversionTable.Length; i++)
				{
					int num = 1;
					for (int j = 0; j < 5; j++)
					{
						if ((i & 1 << j) != 0)
						{
							num *= array[j];
						}
					}
					CharacterModel.LimbFlagSet.primeConversionTable[i] = num;
				}
			}

			// Token: 0x06001EDA RID: 7898 RVA: 0x0008563B File Offset: 0x0008383B
			private static int ConvertLimbFlagsToMaterialMask(LimbFlags limbFlags)
			{
				return CharacterModel.LimbFlagSet.primeConversionTable[(int)limbFlags];
			}

			// Token: 0x06001EDB RID: 7899 RVA: 0x00085644 File Offset: 0x00083844
			public void AddFlags(LimbFlags addedFlags)
			{
				LimbFlags limbFlags = this.flags;
				this.flags |= addedFlags;
				for (int i = 0; i < 5; i++)
				{
					if ((addedFlags & (LimbFlags)(1 << i)) != LimbFlags.None)
					{
						byte[] array = this.flagCounts;
						int num = i;
						array[num] += 1;
					}
				}
				if (this.flags != limbFlags)
				{
					this.materialMaskValue = CharacterModel.LimbFlagSet.ConvertLimbFlagsToMaterialMask(this.flags);
				}
			}

			// Token: 0x06001EDC RID: 7900 RVA: 0x000856A8 File Offset: 0x000838A8
			public void RemoveFlags(LimbFlags removedFlags)
			{
				LimbFlags limbFlags = this.flags;
				for (int i = 0; i < 5; i++)
				{
					if ((removedFlags & (LimbFlags)(1 << i)) != LimbFlags.None)
					{
						byte[] array = this.flagCounts;
						int num = i;
						byte b = array[num] - 1;
						array[num] = b;
						if (b == 0)
						{
							this.flags &= (LimbFlags)(~(LimbFlags)(1 << i));
						}
					}
				}
				if (this.flags != limbFlags)
				{
					this.materialMaskValue = CharacterModel.LimbFlagSet.ConvertLimbFlagsToMaterialMask(this.flags);
				}
			}

			// Token: 0x04002496 RID: 9366
			private readonly byte[] flagCounts = new byte[5];

			// Token: 0x04002497 RID: 9367
			private LimbFlags flags;

			// Token: 0x04002499 RID: 9369
			private static readonly int[] primeConversionTable;
		}
	}
}
