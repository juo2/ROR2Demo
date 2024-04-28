using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200088F RID: 2191
	[RequireComponent(typeof(PurchaseInteraction))]
	public class ShrineBloodBehavior : NetworkBehaviour
	{
		// Token: 0x06003037 RID: 12343 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000CD19B File Offset: 0x000CB39B
		private void Start()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000CD1AC File Offset: 0x000CB3AC
		public void FixedUpdate()
		{
			if (this.waitingForRefresh)
			{
				this.refreshTimer -= Time.fixedDeltaTime;
				if (this.refreshTimer <= 0f && this.purchaseCount < this.maxPurchaseCount)
				{
					this.purchaseInteraction.SetAvailable(true);
					this.purchaseInteraction.Networkcost = (int)(100f * (1f - Mathf.Pow(1f - (float)this.purchaseInteraction.cost / 100f, this.costMultiplierPerPurchase)));
					this.waitingForRefresh = false;
				}
			}
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000CD23C File Offset: 0x000CB43C
		[Server]
		public void AddShrineStack(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineBloodBehavior::AddShrineStack(RoR2.Interactor)' called on client");
				return;
			}
			this.waitingForRefresh = true;
			CharacterBody component = interactor.GetComponent<CharacterBody>();
			if (component)
			{
				uint amount = (uint)(component.healthComponent.fullCombinedHealth * (float)this.purchaseInteraction.cost / 100f * this.goldToPaidHpRatio);
				if (component.master)
				{
					component.master.GiveMoney(amount);
					Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
					{
						subjectAsCharacterBody = component,
						baseToken = "SHRINE_BLOOD_USE_MESSAGE",
						paramTokens = new string[]
						{
							amount.ToString()
						}
					});
				}
			}
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity,
				scale = 1f,
				color = Color.red
			}, true);
			this.purchaseCount++;
			this.refreshTimer = 2f;
			if (this.purchaseCount >= this.maxPurchaseCount)
			{
				this.symbolTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000CD380 File Offset: 0x000CB580
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040031DA RID: 12762
		public int maxPurchaseCount;

		// Token: 0x040031DB RID: 12763
		public float goldToPaidHpRatio = 0.5f;

		// Token: 0x040031DC RID: 12764
		public float costMultiplierPerPurchase;

		// Token: 0x040031DD RID: 12765
		public Transform symbolTransform;

		// Token: 0x040031DE RID: 12766
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x040031DF RID: 12767
		private int purchaseCount;

		// Token: 0x040031E0 RID: 12768
		private float refreshTimer;

		// Token: 0x040031E1 RID: 12769
		private const float refreshDuration = 2f;

		// Token: 0x040031E2 RID: 12770
		private bool waitingForRefresh;
	}
}
