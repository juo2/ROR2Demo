using System;
using System.Collections.Generic;
using RoR2.Networking;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008EB RID: 2283
	[RequireComponent(typeof(PurchaseInteraction))]
	public class VendingMachineBehavior : NetworkBehaviour
	{
		// Token: 0x0600333B RID: 13115 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000D80C2 File Offset: 0x000D62C2
		// (set) Token: 0x0600333D RID: 13117 RVA: 0x000D80CA File Offset: 0x000D62CA
		public int purchaseCount { get; private set; }

		// Token: 0x0600333E RID: 13118 RVA: 0x000D80D3 File Offset: 0x000D62D3
		private void Awake()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000D80E4 File Offset: 0x000D62E4
		[Server]
		public void Vend(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VendingMachineBehavior::Vend(RoR2.Interactor)' called on client");
				return;
			}
			int purchaseCount = this.purchaseCount + 1;
			this.purchaseCount = purchaseCount;
			this.CallRpcTriggerVendAnimation(this.maxPurchases - this.purchaseCount);
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (component)
			{
				this.SendOrbToBody(component);
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.teamMaskFilter = TeamMask.none;
				if (component.teamComponent)
				{
					bullseyeSearch.teamMaskFilter.AddTeam(component.teamComponent.teamIndex);
				}
				bullseyeSearch.filterByLoS = false;
				bullseyeSearch.maxDistanceFilter = this.vendingRadius;
				bullseyeSearch.maxAngleFilter = 360f;
				bullseyeSearch.searchOrigin = base.transform.position;
				bullseyeSearch.searchDirection = Vector3.up;
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.None;
				bullseyeSearch.RefreshCandidates();
				bullseyeSearch.FilterOutGameObject(activator.gameObject);
				List<HurtBox> list = new List<HurtBox>(bullseyeSearch.GetResults());
				list.Sort(new Comparison<HurtBox>(VendingMachineBehavior.BonusOrbHurtBoxSort));
				int num = 0;
				while (num < this.numBonusOrbs && num < list.Count)
				{
					this.SendOrbToBody(list[num].healthComponent.body);
					num++;
				}
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000D821E File Offset: 0x000D641E
		private static int BonusOrbHurtBoxSort(HurtBox lhs, HurtBox rhs)
		{
			return (int)(100f * (lhs.healthComponent.combinedHealthFraction - rhs.healthComponent.combinedHealthFraction));
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000D8240 File Offset: 0x000D6440
		private void SendOrbToBody(CharacterBody body)
		{
			HealthComponent component = body.GetComponent<HealthComponent>();
			if (component)
			{
				VendingMachineOrb vendingMachineOrb = new VendingMachineOrb();
				vendingMachineOrb.origin = this.orbOrigin.position;
				vendingMachineOrb.target = component.body.mainHurtBox;
				vendingMachineOrb.healFraction = this.healFraction;
				OrbManager.instance.AddOrb(vendingMachineOrb);
			}
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000D829C File Offset: 0x000D649C
		[Server]
		public void Detonate()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VendingMachineBehavior::Detonate()' called on client");
				return;
			}
			EffectData effectData = new EffectData();
			effectData.origin = base.transform.position;
			EffectManager.SpawnEffect(this.detonateEffect, effectData, true);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000D82ED File Offset: 0x000D64ED
		[Server]
		public void RefreshPurchaseInteractionAvailability()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VendingMachineBehavior::RefreshPurchaseInteractionAvailability()' called on client");
				return;
			}
			this.purchaseInteraction.Networkavailable = (this.purchaseCount < this.maxPurchases);
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000D831D File Offset: 0x000D651D
		[ClientRpc]
		public void RpcTriggerVendAnimation(int vendsRemaining)
		{
			if (this.animator)
			{
				this.animator.SetTrigger(this.animatorTriggerNameVend);
				this.animator.SetInteger(this.animatorIntNameVendsRemaining, vendsRemaining);
			}
		}

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06003345 RID: 13125 RVA: 0x000D8350 File Offset: 0x000D6550
		// (remove) Token: 0x06003346 RID: 13126 RVA: 0x000D8384 File Offset: 0x000D6584
		public static event Action<ShrineHealingBehavior, Interactor> onActivated;

		// Token: 0x06003348 RID: 13128 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000D83B7 File Offset: 0x000D65B7
		protected static void InvokeRpcRpcTriggerVendAnimation(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcTriggerVendAnimation called on server.");
				return;
			}
			((VendingMachineBehavior)obj).RpcTriggerVendAnimation((int)reader.ReadPackedUInt32());
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000D83E0 File Offset: 0x000D65E0
		public void CallRpcTriggerVendAnimation(int vendsRemaining)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcTriggerVendAnimation called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)VendingMachineBehavior.kRpcRpcTriggerVendAnimation);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32((uint)vendsRemaining);
			this.SendRPCInternal(networkWriter, 0, "RpcTriggerVendAnimation");
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000D8453 File Offset: 0x000D6653
		static VendingMachineBehavior()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(VendingMachineBehavior), VendingMachineBehavior.kRpcRpcTriggerVendAnimation, new NetworkBehaviour.CmdDelegate(VendingMachineBehavior.InvokeRpcRpcTriggerVendAnimation));
			NetworkCRC.RegisterBehaviour("VendingMachineBehavior", 0);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000D8490 File Offset: 0x000D6690
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003453 RID: 13395
		[SerializeField]
		private int maxPurchases;

		// Token: 0x04003454 RID: 13396
		[SerializeField]
		private float healFraction;

		// Token: 0x04003455 RID: 13397
		[SerializeField]
		private GameObject detonateEffect;

		// Token: 0x04003456 RID: 13398
		[SerializeField]
		private float vendingRadius;

		// Token: 0x04003457 RID: 13399
		[SerializeField]
		private int numBonusOrbs;

		// Token: 0x04003458 RID: 13400
		[SerializeField]
		private Transform orbOrigin;

		// Token: 0x04003459 RID: 13401
		[SerializeField]
		private Animator animator;

		// Token: 0x0400345A RID: 13402
		[SerializeField]
		private string animatorTriggerNameVend;

		// Token: 0x0400345B RID: 13403
		[SerializeField]
		private string animatorIntNameVendsRemaining;

		// Token: 0x0400345C RID: 13404
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x0400345F RID: 13407
		private static int kRpcRpcTriggerVendAnimation = 2105333433;
	}
}
