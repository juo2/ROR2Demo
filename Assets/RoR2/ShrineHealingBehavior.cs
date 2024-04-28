using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000894 RID: 2196
	[RequireComponent(typeof(PurchaseInteraction))]
	public class ShrineHealingBehavior : NetworkBehaviour
	{
		// Token: 0x0600306D RID: 12397 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000CDD26 File Offset: 0x000CBF26
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x000CDD2E File Offset: 0x000CBF2E
		public int purchaseCount { get; private set; }

		// Token: 0x06003070 RID: 12400 RVA: 0x000CDD37 File Offset: 0x000CBF37
		private void Awake()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000CDD48 File Offset: 0x000CBF48
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

		// Token: 0x06003072 RID: 12402 RVA: 0x000CDDBC File Offset: 0x000CBFBC
		[Server]
		private void SetWardEnabled(bool enableWard)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineHealingBehavior::SetWardEnabled(System.Boolean)' called on client");
				return;
			}
			if (enableWard != this.wardInstance)
			{
				if (enableWard)
				{
					this.wardInstance = UnityEngine.Object.Instantiate<GameObject>(this.wardPrefab, base.transform.position, base.transform.rotation);
					this.wardInstance.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
					this.healingWard = this.wardInstance.GetComponent<HealingWard>();
					NetworkServer.Spawn(this.wardInstance);
					return;
				}
				UnityEngine.Object.Destroy(this.wardInstance);
				this.wardInstance = null;
				this.healingWard = null;
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000CDE60 File Offset: 0x000CC060
		[Server]
		public void AddShrineStack(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineHealingBehavior::AddShrineStack(RoR2.Interactor)' called on client");
				return;
			}
			this.SetWardEnabled(true);
			Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
			{
				subjectAsCharacterBody = activator.gameObject.GetComponent<CharacterBody>(),
				baseToken = "SHRINE_HEALING_USE_MESSAGE"
			});
			this.waitingForRefresh = true;
			int purchaseCount = this.purchaseCount;
			this.purchaseCount = purchaseCount + 1;
			float networkradius = this.baseRadius + this.radiusBonusPerPurchase * (float)(this.purchaseCount - 1);
			this.healingWard.Networkradius = networkradius;
			this.refreshTimer = 2f;
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity,
				scale = 1f,
				color = Color.green
			}, true);
			if (this.purchaseCount >= this.maxPurchaseCount)
			{
				this.symbolTransform.gameObject.SetActive(false);
			}
			Action<ShrineHealingBehavior, Interactor> action = ShrineHealingBehavior.onActivated;
			if (action == null)
			{
				return;
			}
			action(this, activator);
		}

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06003074 RID: 12404 RVA: 0x000CDF74 File Offset: 0x000CC174
		// (remove) Token: 0x06003075 RID: 12405 RVA: 0x000CDFA8 File Offset: 0x000CC1A8
		public static event Action<ShrineHealingBehavior, Interactor> onActivated;

		// Token: 0x06003077 RID: 12407 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000CDFDC File Offset: 0x000CC1DC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003210 RID: 12816
		public GameObject wardPrefab;

		// Token: 0x04003211 RID: 12817
		private GameObject wardInstance;

		// Token: 0x04003212 RID: 12818
		public float baseRadius;

		// Token: 0x04003213 RID: 12819
		public float radiusBonusPerPurchase;

		// Token: 0x04003214 RID: 12820
		public int maxPurchaseCount;

		// Token: 0x04003215 RID: 12821
		public float costMultiplierPerPurchase;

		// Token: 0x04003216 RID: 12822
		public Transform symbolTransform;

		// Token: 0x04003217 RID: 12823
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x04003219 RID: 12825
		private float refreshTimer;

		// Token: 0x0400321A RID: 12826
		private const float refreshDuration = 2f;

		// Token: 0x0400321B RID: 12827
		private bool waitingForRefresh;

		// Token: 0x0400321C RID: 12828
		private HealingWard healingWard;
	}
}
