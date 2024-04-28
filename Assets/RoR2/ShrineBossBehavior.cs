using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000890 RID: 2192
	[RequireComponent(typeof(PurchaseInteraction))]
	public class ShrineBossBehavior : NetworkBehaviour
	{
		// Token: 0x06003040 RID: 12352 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000CD38E File Offset: 0x000CB58E
		private void Start()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000CD39C File Offset: 0x000CB59C
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

		// Token: 0x06003043 RID: 12355 RVA: 0x000CD410 File Offset: 0x000CB610
		[Server]
		public void AddShrineStack(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineBossBehavior::AddShrineStack(RoR2.Interactor)' called on client");
				return;
			}
			this.waitingForRefresh = true;
			if (TeleporterInteraction.instance)
			{
				TeleporterInteraction.instance.AddShrineStack();
			}
			CharacterBody component = interactor.GetComponent<CharacterBody>();
			Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
			{
				subjectAsCharacterBody = component,
				baseToken = "SHRINE_BOSS_USE_MESSAGE"
			});
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity,
				scale = 1f,
				color = new Color(0.7372549f, 0.90588236f, 0.94509804f)
			}, true);
			this.purchaseCount++;
			this.refreshTimer = 2f;
			if (this.purchaseCount >= this.maxPurchaseCount)
			{
				this.symbolTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000CD508 File Offset: 0x000CB708
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040031E3 RID: 12771
		public int maxPurchaseCount;

		// Token: 0x040031E4 RID: 12772
		public float costMultiplierPerPurchase;

		// Token: 0x040031E5 RID: 12773
		public Transform symbolTransform;

		// Token: 0x040031E6 RID: 12774
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x040031E7 RID: 12775
		private int purchaseCount;

		// Token: 0x040031E8 RID: 12776
		private float refreshTimer;

		// Token: 0x040031E9 RID: 12777
		private const float refreshDuration = 2f;

		// Token: 0x040031EA RID: 12778
		private bool waitingForRefresh;
	}
}
