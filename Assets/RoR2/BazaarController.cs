using System;
using System.Collections.Generic;
using RoR2.ExpansionManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005F0 RID: 1520
	public class BazaarController : MonoBehaviour
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00075E1D File Offset: 0x0007401D
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x00075E24 File Offset: 0x00074024
		public static BazaarController instance { get; private set; }

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00075E2C File Offset: 0x0007402C
		private void Awake()
		{
			BazaarController.instance = SingletonHelper.Assign<BazaarController>(BazaarController.instance, this);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00075E3E File Offset: 0x0007403E
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.OnStartServer();
			}
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00075E4D File Offset: 0x0007404D
		private void OnStartServer()
		{
			this.rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
			this.SetUpSeerStations();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00075E70 File Offset: 0x00074070
		private void SetUpSeerStations()
		{
			SceneDef nextStageScene = Run.instance.nextStageScene;
			List<SceneDef> list = new List<SceneDef>();
			if (nextStageScene != null)
			{
				int stageOrder = nextStageScene.stageOrder;
				foreach (SceneDef sceneDef in SceneCatalog.allSceneDefs)
				{
					if (sceneDef.stageOrder == stageOrder && (sceneDef.requiredExpansion == null || Run.instance.IsExpansionEnabled(sceneDef.requiredExpansion)))
					{
						list.Add(sceneDef);
					}
				}
			}
			WeightedSelection<SceneDef> weightedSelection = new WeightedSelection<SceneDef>(8);
			float num = 0f;
			foreach (BazaarController.SeerSceneOverride seerSceneOverride in this.seerSceneOverrides)
			{
				bool flag = Run.instance.stageClearCount >= seerSceneOverride.minStagesCleared;
				bool flag2 = seerSceneOverride.requiredExpasion == null || Run.instance.IsExpansionEnabled(seerSceneOverride.requiredExpasion);
				bool flag3 = string.IsNullOrEmpty(seerSceneOverride.bannedEventFlag) || !Run.instance.GetEventFlag(seerSceneOverride.bannedEventFlag);
				if (flag && flag2 && flag3)
				{
					weightedSelection.AddChoice(seerSceneOverride.sceneDef, seerSceneOverride.overrideChance);
					num += seerSceneOverride.overrideChance;
				}
			}
			foreach (SeerStationController seerStationController in this.seerStations)
			{
				if (list.Count == 0)
				{
					seerStationController.GetComponent<PurchaseInteraction>().SetAvailable(false);
				}
				else
				{
					Util.ShuffleList<SceneDef>(list, this.rng);
					int index = list.Count - 1;
					SceneDef targetScene = list[index];
					list.RemoveAt(index);
					if (this.rng.nextNormalizedFloat < num)
					{
						targetScene = weightedSelection.Evaluate(this.rng.nextNormalizedFloat);
					}
					seerStationController.SetTargetScene(targetScene);
				}
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00076064 File Offset: 0x00074264
		private void OnDestroy()
		{
			BazaarController.instance = SingletonHelper.Unassign<BazaarController>(BazaarController.instance, this);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00076078 File Offset: 0x00074278
		public void CommentOnAnnoy()
		{
			float percentChance = 20f;
			int max = 6;
			if (Util.CheckRoll(percentChance, 0f, null))
			{
				Chat.SendBroadcastChat(new Chat.NpcChatMessage
				{
					sender = this.shopkeeper,
					baseToken = "NEWT_ANNOY_" + UnityEngine.Random.Range(1, max)
				});
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000026ED File Offset: 0x000008ED
		public void CommentOnEnter()
		{
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000026ED File Offset: 0x000008ED
		public void CommentOnLeaving()
		{
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000760CC File Offset: 0x000742CC
		public void CommentOnLunarPurchase()
		{
			float percentChance = 20f;
			int max = 8;
			if (Util.CheckRoll(percentChance, 0f, null))
			{
				Chat.SendBroadcastChat(new Chat.NpcChatMessage
				{
					sender = this.shopkeeper,
					baseToken = "NEWT_LUNAR_PURCHASE_" + UnityEngine.Random.Range(1, max)
				});
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000026ED File Offset: 0x000008ED
		public void CommentOnBlueprintPurchase()
		{
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000026ED File Offset: 0x000008ED
		public void CommentOnDronePurchase()
		{
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00076120 File Offset: 0x00074320
		public void CommentOnUpgrade()
		{
			float percentChance = 100f;
			int max = 3;
			if (Util.CheckRoll(percentChance, 0f, null))
			{
				Chat.SendBroadcastChat(new Chat.NpcChatMessage
				{
					sender = this.shopkeeper,
					baseToken = "NEWT_UPGRADE_" + UnityEngine.Random.Range(1, max)
				});
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00076174 File Offset: 0x00074374
		private void Update()
		{
			if (this.shopkeeper)
			{
				if (!this.shopkeeperInputBank)
				{
					this.shopkeeperInputBank = this.shopkeeper.GetComponent<InputBankTest>();
					return;
				}
				Ray aimRay = new Ray(this.shopkeeperInputBank.aimOrigin, this.shopkeeper.transform.forward);
				this.shopkeeperTargetBody = Util.GetEnemyEasyTarget(this.shopkeeper.GetComponent<CharacterBody>(), aimRay, this.shopkeeperTrackDistance, this.shopkeeperTrackAngle);
				if (this.shopkeeperTargetBody)
				{
					Vector3 direction = this.shopkeeperTargetBody.mainHurtBox.transform.position - aimRay.origin;
					aimRay.direction = direction;
				}
				this.shopkeeperInputBank.aimDirection = aimRay.direction;
			}
		}

		// Token: 0x04002180 RID: 8576
		public GameObject shopkeeper;

		// Token: 0x04002181 RID: 8577
		public TextMeshPro shopkeeperChat;

		// Token: 0x04002182 RID: 8578
		public float shopkeeperTrackDistance = 250f;

		// Token: 0x04002183 RID: 8579
		public float shopkeeperTrackAngle = 120f;

		// Token: 0x04002184 RID: 8580
		[Tooltip("Any PurchaseInteraction objects here will have their activation state set based on whether or not the specified unlockable is available.")]
		public PurchaseInteraction[] unlockableTerminals;

		// Token: 0x04002185 RID: 8581
		public SeerStationController[] seerStations;

		// Token: 0x04002186 RID: 8582
		public BazaarController.SeerSceneOverride[] seerSceneOverrides;

		// Token: 0x04002187 RID: 8583
		private InputBankTest shopkeeperInputBank;

		// Token: 0x04002188 RID: 8584
		private CharacterBody shopkeeperTargetBody;

		// Token: 0x04002189 RID: 8585
		private Xoroshiro128Plus rng;

		// Token: 0x020005F1 RID: 1521
		[Serializable]
		public struct SeerSceneOverride
		{
			// Token: 0x0400218A RID: 8586
			[SerializeField]
			public SceneDef sceneDef;

			// Token: 0x0400218B RID: 8587
			[SerializeField]
			public ExpansionDef requiredExpasion;

			// Token: 0x0400218C RID: 8588
			[Range(0f, 1f)]
			[SerializeField]
			public float overrideChance;

			// Token: 0x0400218D RID: 8589
			[SerializeField]
			public int minStagesCleared;

			// Token: 0x0400218E RID: 8590
			[SerializeField]
			public string bannedEventFlag;
		}
	}
}
