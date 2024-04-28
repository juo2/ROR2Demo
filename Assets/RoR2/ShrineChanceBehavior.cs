using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000891 RID: 2193
	[RequireComponent(typeof(PurchaseInteraction))]
	public class ShrineChanceBehavior : NetworkBehaviour
	{
		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06003049 RID: 12361 RVA: 0x000CD518 File Offset: 0x000CB718
		// (remove) Token: 0x0600304A RID: 12362 RVA: 0x000CD54C File Offset: 0x000CB74C
		public static event Action<bool, Interactor> onShrineChancePurchaseGlobal;

		// Token: 0x0600304B RID: 12363 RVA: 0x000CD57F File Offset: 0x000CB77F
		private void Awake()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000CD58D File Offset: 0x000CB78D
		public void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000CD5B0 File Offset: 0x000CB7B0
		public void FixedUpdate()
		{
			if (this.waitingForRefresh)
			{
				this.refreshTimer -= Time.fixedDeltaTime;
				if (this.refreshTimer <= 0f && this.successfulPurchaseCount < this.maxPurchaseCount)
				{
					this.purchaseInteraction.SetAvailable(true);
					this.purchaseInteraction.Networkcost = (int)((float)this.purchaseInteraction.cost * this.costMultiplierPerPurchase);
					this.waitingForRefresh = false;
				}
			}
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000CD624 File Offset: 0x000CB824
		[Server]
		public void AddShrineStack(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineChanceBehavior::AddShrineStack(RoR2.Interactor)' called on client");
				return;
			}
			PickupIndex pickupIndex = PickupIndex.none;
			if (this.dropTable)
			{
				if (this.rng.nextNormalizedFloat > this.failureChance)
				{
					pickupIndex = this.dropTable.GenerateDrop(this.rng);
				}
			}
			else
			{
				PickupIndex none = PickupIndex.none;
				PickupIndex value = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableTier1DropList);
				PickupIndex value2 = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableTier2DropList);
				PickupIndex value3 = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableTier3DropList);
				PickupIndex value4 = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableEquipmentDropList);
				WeightedSelection<PickupIndex> weightedSelection = new WeightedSelection<PickupIndex>(8);
				weightedSelection.AddChoice(none, this.failureWeight);
				weightedSelection.AddChoice(value, this.tier1Weight);
				weightedSelection.AddChoice(value2, this.tier2Weight);
				weightedSelection.AddChoice(value3, this.tier3Weight);
				weightedSelection.AddChoice(value4, this.equipmentWeight);
				pickupIndex = weightedSelection.Evaluate(this.rng.nextNormalizedFloat);
			}
			bool flag = pickupIndex == PickupIndex.none;
			string baseToken;
			if (flag)
			{
				baseToken = "SHRINE_CHANCE_FAIL_MESSAGE";
			}
			else
			{
				baseToken = "SHRINE_CHANCE_SUCCESS_MESSAGE";
				this.successfulPurchaseCount++;
				PickupDropletController.CreatePickupDroplet(pickupIndex, this.dropletOrigin.position, this.dropletOrigin.forward * 20f);
			}
			Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
			{
				subjectAsCharacterBody = activator.GetComponent<CharacterBody>(),
				baseToken = baseToken
			});
			Action<bool, Interactor> action = ShrineChanceBehavior.onShrineChancePurchaseGlobal;
			if (action != null)
			{
				action(flag, activator);
			}
			this.waitingForRefresh = true;
			this.refreshTimer = 2f;
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity,
				scale = 1f,
				color = this.shrineColor
			}, true);
			if (this.successfulPurchaseCount >= this.maxPurchaseCount)
			{
				this.symbolTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000CD844 File Offset: 0x000CBA44
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040031EB RID: 12779
		public int maxPurchaseCount;

		// Token: 0x040031EC RID: 12780
		public float costMultiplierPerPurchase;

		// Token: 0x040031ED RID: 12781
		public float failureChance;

		// Token: 0x040031EE RID: 12782
		public PickupDropTable dropTable;

		// Token: 0x040031EF RID: 12783
		public Transform symbolTransform;

		// Token: 0x040031F0 RID: 12784
		public Transform dropletOrigin;

		// Token: 0x040031F1 RID: 12785
		public Color shrineColor;

		// Token: 0x040031F2 RID: 12786
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x040031F3 RID: 12787
		private int successfulPurchaseCount;

		// Token: 0x040031F4 RID: 12788
		private float refreshTimer;

		// Token: 0x040031F5 RID: 12789
		private const float refreshDuration = 2f;

		// Token: 0x040031F6 RID: 12790
		private bool waitingForRefresh;

		// Token: 0x040031F8 RID: 12792
		private Xoroshiro128Plus rng;

		// Token: 0x040031F9 RID: 12793
		[Header("Deprecated")]
		public float failureWeight;

		// Token: 0x040031FA RID: 12794
		public float equipmentWeight;

		// Token: 0x040031FB RID: 12795
		public float tier1Weight;

		// Token: 0x040031FC RID: 12796
		public float tier2Weight;

		// Token: 0x040031FD RID: 12797
		public float tier3Weight;
	}
}
