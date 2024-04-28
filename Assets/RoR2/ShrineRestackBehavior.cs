using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000895 RID: 2197
	[RequireComponent(typeof(PurchaseInteraction))]
	public class ShrineRestackBehavior : NetworkBehaviour
	{
		// Token: 0x0600307B RID: 12411 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000CDFEA File Offset: 0x000CC1EA
		private void Start()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000CE01C File Offset: 0x000CC21C
		public void FixedUpdate()
		{
			if (this.waitingForRefresh)
			{
				this.refreshTimer -= Time.fixedDeltaTime;
				if (this.refreshTimer <= 0f && this.purchaseCount < this.maxPurchaseCount)
				{
					this.purchaseInteraction.SetAvailable(true);
					this.purchaseInteraction.Networkcost = (int)((float)this.purchaseInteraction.cost * this.costMultiplierPerPurchase);
					this.waitingForRefresh = false;
				}
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000CE090 File Offset: 0x000CC290
		[Server]
		public void AddShrineStack(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineRestackBehavior::AddShrineStack(RoR2.Interactor)' called on client");
				return;
			}
			this.waitingForRefresh = true;
			CharacterBody component = interactor.GetComponent<CharacterBody>();
			if (component && component.master)
			{
				Inventory inventory = component.master.inventory;
				if (inventory)
				{
					inventory.ShrineRestackInventory(this.rng);
					Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
					{
						subjectAsCharacterBody = component,
						baseToken = "SHRINE_RESTACK_USE_MESSAGE"
					});
				}
			}
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity,
				scale = 1f,
				color = new Color(1f, 0.23f, 0.6337214f)
			}, true);
			this.purchaseCount++;
			this.refreshTimer = 2f;
			if (this.purchaseCount >= this.maxPurchaseCount)
			{
				this.symbolTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000CE1A4 File Offset: 0x000CC3A4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400321E RID: 12830
		public int maxPurchaseCount;

		// Token: 0x0400321F RID: 12831
		public float costMultiplierPerPurchase;

		// Token: 0x04003220 RID: 12832
		public Transform symbolTransform;

		// Token: 0x04003221 RID: 12833
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x04003222 RID: 12834
		private int purchaseCount;

		// Token: 0x04003223 RID: 12835
		private float refreshTimer;

		// Token: 0x04003224 RID: 12836
		private const float refreshDuration = 2f;

		// Token: 0x04003225 RID: 12837
		private bool waitingForRefresh;

		// Token: 0x04003226 RID: 12838
		private Xoroshiro128Plus rng;
	}
}
