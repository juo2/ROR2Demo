using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CFB RID: 3323
	[RequireComponent(typeof(HudElement))]
	public class EnemyInfoPanel : MonoBehaviour
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x00136E48 File Offset: 0x00135048
		private void Awake()
		{
			this.monsterBodyIconAllocator = new UIElementAllocator<RawImage>(this.monsterBodyIconContainer, this.iconPrefab, true, false);
			this.itemStacks = ItemCatalog.RequestItemStackArray();
			this.itemAcquisitionOrder = ItemCatalog.RequestItemOrderBuffer();
			this.monsterBodiesContainer.SetActive(false);
			this.inventoryContainer.SetActive(false);
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00136E9C File Offset: 0x0013509C
		private void OnDestroy()
		{
			ItemCatalog.ReturnItemOrderBuffer(this.itemAcquisitionOrder);
			this.itemAcquisitionOrder = null;
			ItemCatalog.ReturnItemStackArray(this.itemStacks);
			this.itemStacks = null;
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x00136EC2 File Offset: 0x001350C2
		private void OnEnable()
		{
			InstanceTracker.Add<EnemyInfoPanel>(this);
			EnemyInfoPanel.MarkDirty();
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00136ECF File Offset: 0x001350CF
		private void OnDisable()
		{
			EnemyInfoPanel.MarkDirty();
			InstanceTracker.Remove<EnemyInfoPanel>(this);
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00136EDC File Offset: 0x001350DC
		private void TrySetMonsterBodies<T>([NotNull] T newBodyIndices) where T : IList<BodyIndex>
		{
			bool flag = false;
			if (this.currentMonsterBodyIndices.Length != newBodyIndices.Count)
			{
				Array.Resize<BodyIndex>(ref this.currentMonsterBodyIndices, newBodyIndices.Count);
				flag = true;
			}
			for (int i = 0; i < newBodyIndices.Count; i++)
			{
				if (this.currentMonsterBodyIndices[i] != newBodyIndices[i])
				{
					this.currentMonsterBodyIndices[i] = newBodyIndices[i];
					flag = true;
				}
			}
			if (flag)
			{
				this.SetMonsterBodies(this.currentMonsterBodyIndices);
			}
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00136F74 File Offset: 0x00135174
		private void SetMonsterBodies([NotNull] BodyIndex[] bodyIndices)
		{
			this.monsterBodyIconAllocator.AllocateElements(bodyIndices.Length);
			for (int i = 0; i < bodyIndices.Length; i++)
			{
				CharacterBody bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(bodyIndices[i]);
				this.monsterBodyIconAllocator.elements[i].texture = ((bodyPrefabBodyComponent != null) ? bodyPrefabBodyComponent.portraitIcon : null);
			}
			this.monsterBodiesContainer.SetActive(bodyIndices.Length != 0);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00136FD8 File Offset: 0x001351D8
		private void TrySetItems<T1, T2>([NotNull] T1 newItemAcquisitionOrder, int newItemAcquisitionOrderLength, [NotNull] T2 newItemStacks) where T1 : IList<ItemIndex> where T2 : IList<int>
		{
			bool flag = false;
			bool flag2 = false;
			if (this.itemAcquisitionOrderLength != newItemAcquisitionOrderLength)
			{
				flag = true;
			}
			else
			{
				for (int i = 0; i < this.itemAcquisitionOrderLength; i++)
				{
					if (this.itemAcquisitionOrder[i] != newItemAcquisitionOrder[i])
					{
						flag = true;
						break;
					}
				}
			}
			for (int j = 0; j < this.itemStacks.Length; j++)
			{
				if (this.itemStacks[j] != newItemStacks[j])
				{
					flag2 = true;
					break;
				}
			}
			if (flag)
			{
				this.itemAcquisitionOrderLength = newItemAcquisitionOrderLength;
				for (int k = 0; k < this.itemAcquisitionOrderLength; k++)
				{
					this.itemAcquisitionOrder[k] = newItemAcquisitionOrder[k];
				}
			}
			if (flag2)
			{
				for (int l = 0; l < this.itemStacks.Length; l++)
				{
					this.itemStacks[l] = newItemStacks[l];
				}
			}
			if (flag || flag2)
			{
				this.SetItems(this.itemAcquisitionOrder, this.itemAcquisitionOrderLength, this.itemStacks);
			}
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x001370D8 File Offset: 0x001352D8
		private void SetItems([NotNull] ItemIndex[] newItemAcquisitionOrder, int newItemAcquisitionOrderLength, [NotNull] int[] newItemStacks)
		{
			this.inventoryContainer.SetActive(newItemAcquisitionOrderLength > 0);
			this.inventoryDisplay.SetItems(newItemAcquisitionOrder, newItemAcquisitionOrderLength, newItemStacks);
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x001370F8 File Offset: 0x001352F8
		private static EnemyInfoPanel SetDisplayingOnHud([NotNull] HUD hud, bool shouldDisplay)
		{
			List<EnemyInfoPanel> instancesList = InstanceTracker.GetInstancesList<EnemyInfoPanel>();
			EnemyInfoPanel enemyInfoPanel = null;
			for (int i = 0; i < instancesList.Count; i++)
			{
				EnemyInfoPanel enemyInfoPanel2 = instancesList[i];
				if (enemyInfoPanel2.hud == hud)
				{
					enemyInfoPanel = enemyInfoPanel2;
					break;
				}
			}
			if (enemyInfoPanel != shouldDisplay)
			{
				if (!enemyInfoPanel)
				{
					Transform transform = null;
					if (hud.gameModeUiInstance)
					{
						ChildLocator component = hud.gameModeUiInstance.GetComponent<ChildLocator>();
						if (component)
						{
							Transform transform2 = component.FindChild("RightInfoBar");
							if (transform2)
							{
								transform = transform2.GetComponent<RectTransform>();
							}
						}
					}
					if (transform)
					{
						EnemyInfoPanel component2 = UnityEngine.Object.Instantiate<GameObject>(EnemyInfoPanel.panelPrefab, transform).GetComponent<EnemyInfoPanel>();
						component2.hud = hud;
						enemyInfoPanel = component2;
					}
				}
				else
				{
					UnityEngine.Object.Destroy(enemyInfoPanel.gameObject);
					enemyInfoPanel = null;
				}
			}
			return enemyInfoPanel;
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x001371BC File Offset: 0x001353BC
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ArenaMissionController.onInstanceChangedGlobal += EnemyInfoPanel.ArenaMissionControllerOnOnInstanceChangedGlobal;
			EnemyInfoPanel.panelPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/EnemyInfoPanel");
			HUD.onHudTargetChangedGlobal += EnemyInfoPanel.OnHudTargetChangedGlobal;
			EnemyInfoPanelInventoryProvider.onInventoriesChanged += EnemyInfoPanel.RefreshAll;
			RoR2Application.onFixedUpdate += EnemyInfoPanel.StaticFixedUpdate;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0013721C File Offset: 0x0013541C
		private static void ArenaMissionControllerOnOnInstanceChangedGlobal()
		{
			EnemyInfoPanel.MarkDirty();
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0013721C File Offset: 0x0013541C
		private static void OnHudTargetChangedGlobal([NotNull] HUD hud)
		{
			EnemyInfoPanel.MarkDirty();
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x00137223 File Offset: 0x00135423
		private static void MarkDirty()
		{
			if (EnemyInfoPanel.isDirty)
			{
				return;
			}
			RoR2Application.onNextUpdate += EnemyInfoPanel.RefreshAll;
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00137240 File Offset: 0x00135440
		private static void RefreshAll()
		{
			for (int i = 0; i < HUD.readOnlyInstanceList.Count; i++)
			{
				EnemyInfoPanel.RefreshHUD(HUD.readOnlyInstanceList[i]);
			}
			EnemyInfoPanel.isDirty = false;
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x00137278 File Offset: 0x00135478
		private static void RefreshHUD(HUD hud)
		{
			if (!hud.targetMaster)
			{
				return;
			}
			TeamIndex teamIndex = hud.targetMaster.teamIndex;
			ItemIndex[] array = ItemCatalog.RequestItemOrderBuffer();
			int itemAcquisitonOrderLength = 0;
			int[] array2 = ItemCatalog.RequestItemStackArray();
			int[] array3 = ItemCatalog.RequestItemStackArray();
			List<EnemyInfoPanelInventoryProvider> instancesList = InstanceTracker.GetInstancesList<EnemyInfoPanelInventoryProvider>();
			int i = 0;
			int count = instancesList.Count;
			while (i < count)
			{
				EnemyInfoPanelInventoryProvider enemyInfoPanelInventoryProvider = instancesList[i];
				if (enemyInfoPanelInventoryProvider.teamFilter.teamIndex != teamIndex)
				{
					List<ItemIndex> list = enemyInfoPanelInventoryProvider.inventory.itemAcquisitionOrder;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						ItemIndex itemIndex = list[j];
						ref int ptr = ref array3[(int)itemIndex];
						if (ptr == 0)
						{
							ptr = 1;
							array[itemAcquisitonOrderLength++] = itemIndex;
						}
						j++;
					}
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k] += enemyInfoPanelInventoryProvider.inventory.GetItemCount((ItemIndex)k);
					}
				}
				i++;
			}
			EnemyInfoPanel.bodyIndexList.Clear();
			if (ArenaMissionController.instance)
			{
				SyncListInt syncActiveMonsterBodies = ArenaMissionController.instance.syncActiveMonsterBodies;
				for (int l = 0; l < syncActiveMonsterBodies.Count; l++)
				{
					EnemyInfoPanel.bodyIndexList.Add((BodyIndex)syncActiveMonsterBodies[l]);
				}
			}
			else
			{
				BodyIndex bodyIndex = Stage.instance ? Stage.instance.singleMonsterTypeBodyIndex : BodyIndex.None;
				if (bodyIndex != BodyIndex.None)
				{
					EnemyInfoPanel.bodyIndexList.Add(bodyIndex);
				}
			}
			EnemyInfoPanel.SetDisplayDataForViewer(hud, EnemyInfoPanel.bodyIndexList, array, itemAcquisitonOrderLength, array2);
			ItemCatalog.ReturnItemStackArray(array3);
			ItemCatalog.ReturnItemStackArray(array2);
			ItemCatalog.ReturnItemOrderBuffer(array);
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0013740C File Offset: 0x0013560C
		private static void SetDisplayDataForViewer([NotNull] HUD hud, [NotNull] List<BodyIndex> bodyIndices, [NotNull] ItemIndex[] itemAcquisitionOrderBuffer, int itemAcquisitonOrderLength, int[] itemStacks)
		{
			bool shouldDisplay = bodyIndices.Count > 0 || itemAcquisitonOrderLength > 0;
			EnemyInfoPanel enemyInfoPanel = EnemyInfoPanel.SetDisplayingOnHud(hud, shouldDisplay);
			if (enemyInfoPanel && enemyInfoPanel.isActiveAndEnabled)
			{
				enemyInfoPanel.TrySetMonsterBodies<List<BodyIndex>>(bodyIndices);
				enemyInfoPanel.TrySetItems<ItemIndex[], int[]>(itemAcquisitionOrderBuffer, itemAcquisitonOrderLength, itemStacks);
			}
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x00137454 File Offset: 0x00135654
		private static void StaticFixedUpdate()
		{
			if (ArenaMissionController.instance)
			{
				bool flag = false;
				SyncListInt syncActiveMonsterBodies = ArenaMissionController.instance.syncActiveMonsterBodies;
				if (EnemyInfoPanel.cachedArenaMonsterBodiesList.Count == syncActiveMonsterBodies.Count)
				{
					int i = 0;
					int count = syncActiveMonsterBodies.Count;
					while (i < count)
					{
						if (EnemyInfoPanel.cachedArenaMonsterBodiesList[i] != syncActiveMonsterBodies[i])
						{
							flag = true;
							break;
						}
						i++;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					EnemyInfoPanel.cachedArenaMonsterBodiesList.Clear();
					int j = 0;
					int count2 = syncActiveMonsterBodies.Count;
					while (j < count2)
					{
						EnemyInfoPanel.cachedArenaMonsterBodiesList.Add(syncActiveMonsterBodies[j]);
						j++;
					}
					EnemyInfoPanel.MarkDirty();
				}
			}
		}

		// Token: 0x04004860 RID: 18528
		public GameObject monsterBodiesContainer;

		// Token: 0x04004861 RID: 18529
		public RectTransform monsterBodyIconContainer;

		// Token: 0x04004862 RID: 18530
		public GameObject inventoryContainer;

		// Token: 0x04004863 RID: 18531
		public ItemInventoryDisplay inventoryDisplay;

		// Token: 0x04004864 RID: 18532
		public GameObject iconPrefab;

		// Token: 0x04004865 RID: 18533
		private UIElementAllocator<RawImage> monsterBodyIconAllocator;

		// Token: 0x04004866 RID: 18534
		private BodyIndex[] currentMonsterBodyIndices = Array.Empty<BodyIndex>();

		// Token: 0x04004867 RID: 18535
		private HUD hud;

		// Token: 0x04004868 RID: 18536
		private ItemIndex[] itemAcquisitionOrder;

		// Token: 0x04004869 RID: 18537
		private int itemAcquisitionOrderLength;

		// Token: 0x0400486A RID: 18538
		private int[] itemStacks;

		// Token: 0x0400486B RID: 18539
		private static GameObject panelPrefab = null;

		// Token: 0x0400486C RID: 18540
		private static readonly List<BodyIndex> bodyIndexList = new List<BodyIndex>();

		// Token: 0x0400486D RID: 18541
		private static bool isDirty = false;

		// Token: 0x0400486E RID: 18542
		private static readonly List<int> cachedArenaMonsterBodiesList = new List<int>();
	}
}
