using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000893 RID: 2195
	[RequireComponent(typeof(PurchaseInteraction))]
	[RequireComponent(typeof(CombatDirector))]
	[RequireComponent(typeof(CombatSquad))]
	public class ShrineCombatBehavior : NetworkBehaviour
	{
		// Token: 0x0600305E RID: 12382 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000CDAB0 File Offset: 0x000CBCB0
		private float monsterCredit
		{
			get
			{
				return (float)this.baseMonsterCredit * Stage.instance.entryDifficultyCoefficient * (1f + (float)this.purchaseCount * (this.monsterCreditCoefficientPerPurchase - 1f));
			}
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000CDADF File Offset: 0x000CBCDF
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
				this.combatDirector = base.GetComponent<CombatDirector>();
				this.combatDirector.combatSquad.onDefeatedServer += this.OnDefeatedServer;
			}
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000CDB1C File Offset: 0x000CBD1C
		private void OnDefeatedServer()
		{
			Action<ShrineCombatBehavior> action = ShrineCombatBehavior.onDefeatedServerGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000CDB2E File Offset: 0x000CBD2E
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.chosenDirectorCard = this.combatDirector.SelectMonsterCardForCombatShrine(this.monsterCredit);
				if (this.chosenDirectorCard == null)
				{
					Debug.Log("Could not find appropriate spawn card for Combat Shrine");
					this.purchaseInteraction.SetAvailable(false);
				}
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000CDB6C File Offset: 0x000CBD6C
		public void FixedUpdate()
		{
			if (this.waitingForRefresh)
			{
				this.refreshTimer -= Time.fixedDeltaTime;
				if (this.refreshTimer <= 0f && this.purchaseCount < this.maxPurchaseCount)
				{
					this.purchaseInteraction.SetAvailable(true);
					this.waitingForRefresh = false;
				}
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000CDBC4 File Offset: 0x000CBDC4
		[Server]
		public void AddShrineStack(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShrineCombatBehavior::AddShrineStack(RoR2.Interactor)' called on client");
				return;
			}
			this.waitingForRefresh = true;
			this.combatDirector.CombatShrineActivation(interactor, this.monsterCredit, this.chosenDirectorCard);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity,
				scale = 1f,
				color = this.shrineEffectColor
			}, true);
			this.purchaseCount++;
			this.refreshTimer = 2f;
			if (this.purchaseCount >= this.maxPurchaseCount)
			{
				this.symbolTransform.gameObject.SetActive(false);
			}
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06003065 RID: 12389 RVA: 0x000CDC8C File Offset: 0x000CBE8C
		// (remove) Token: 0x06003066 RID: 12390 RVA: 0x000CDCC0 File Offset: 0x000CBEC0
		public static event Action<ShrineCombatBehavior> onDefeatedServerGlobal;

		// Token: 0x06003067 RID: 12391 RVA: 0x000CDCF3 File Offset: 0x000CBEF3
		private void OnValidate()
		{
			if (!base.GetComponent<CombatDirector>().combatSquad)
			{
				Debug.LogError("ShrineCombatBehavior's sibling CombatDirector must use a CombatSquad.", base.gameObject);
			}
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000CDD18 File Offset: 0x000CBF18
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003202 RID: 12802
		public Color shrineEffectColor;

		// Token: 0x04003203 RID: 12803
		public int maxPurchaseCount;

		// Token: 0x04003204 RID: 12804
		public int baseMonsterCredit;

		// Token: 0x04003205 RID: 12805
		public float monsterCreditCoefficientPerPurchase;

		// Token: 0x04003206 RID: 12806
		public Transform symbolTransform;

		// Token: 0x04003207 RID: 12807
		public GameObject spawnPositionEffectPrefab;

		// Token: 0x04003208 RID: 12808
		private CombatDirector combatDirector;

		// Token: 0x04003209 RID: 12809
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x0400320A RID: 12810
		private int purchaseCount;

		// Token: 0x0400320B RID: 12811
		private float refreshTimer;

		// Token: 0x0400320C RID: 12812
		private const float refreshDuration = 2f;

		// Token: 0x0400320D RID: 12813
		private bool waitingForRefresh;

		// Token: 0x0400320E RID: 12814
		private DirectorCard chosenDirectorCard;
	}
}
