using System;
using HG;
using JetBrains.Annotations;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200080D RID: 2061
	[RequireComponent(typeof(PlayerCharacterMasterController))]
	public class PlayerCharacterMasterControllerEntitlementTracker : NetworkBehaviour
	{
		// Token: 0x06002CBA RID: 11450 RVA: 0x000BF0C4 File Offset: 0x000BD2C4
		private void Awake()
		{
			this.playerCharacterMasterController = base.GetComponent<PlayerCharacterMasterController>();
			this.entitlementsSet = new bool[EntitlementCatalog.entitlementDefs.Length];
			this.playerCharacterMasterController.onLinkedToNetworkUserServer += this.OnLinkedToNetworkUserServer;
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000BF10C File Offset: 0x000BD30C
		private void OnLinkedToNetworkUserServer()
		{
			this.UpdateEntitlementsServer();
			GameObject bodyPrefab = this.playerCharacterMasterController.master.bodyPrefab;
			if (bodyPrefab)
			{
				ExpansionRequirementComponent component = bodyPrefab.GetComponent<ExpansionRequirementComponent>();
				if (component && !component.PlayerCanUseBody(this.playerCharacterMasterController))
				{
					Debug.LogWarning("Player can't use body " + bodyPrefab.name + "; defaulting to default survivor.");
					this.playerCharacterMasterController.master.bodyPrefab = SurvivorCatalog.defaultSurvivor.bodyPrefab;
				}
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000BF18C File Offset: 0x000BD38C
		[Server]
		private unsafe void UpdateEntitlementsServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PlayerCharacterMasterControllerEntitlementTracker::UpdateEntitlementsServer()' called on client");
				return;
			}
			NetworkUser networkUser = this.playerCharacterMasterController.networkUser;
			if (!networkUser)
			{
				return;
			}
			bool flag = false;
			NetworkUserServerEntitlementTracker networkUserEntitlementTracker = EntitlementManager.networkUserEntitlementTracker;
			ReadOnlyArray<EntitlementDef> entitlementDefs = EntitlementCatalog.entitlementDefs;
			for (int i = 0; i < entitlementDefs.Length; i++)
			{
				EntitlementDef entitlementDef = *entitlementDefs[i];
				if (!this.entitlementsSet[i] && networkUserEntitlementTracker.UserHasEntitlement(networkUser, entitlementDef))
				{
					this.entitlementsSet[i] = true;
					flag = true;
				}
			}
			if (flag)
			{
				base.SetDirtyBit(PlayerCharacterMasterControllerEntitlementTracker.entitlementsSetDirtyBit);
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000BF224 File Offset: 0x000BD424
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = initialState ? PlayerCharacterMasterControllerEntitlementTracker.allDirtyBits : base.syncVarDirtyBits;
			writer.WritePackedUInt32(num);
			if ((num & PlayerCharacterMasterControllerEntitlementTracker.entitlementsSetDirtyBit) != 0U)
			{
				writer.WriteBitArray(this.entitlementsSet);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000BF267 File Offset: 0x000BD467
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadPackedUInt32() & PlayerCharacterMasterControllerEntitlementTracker.entitlementsSetDirtyBit) != 0U)
			{
				reader.ReadBitArray(this.entitlementsSet);
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000BF284 File Offset: 0x000BD484
		public bool HasEntitlement([NotNull] EntitlementDef entitlementDef)
		{
			if (entitlementDef == null)
			{
				throw new ArgumentNullException("entitlementDef");
			}
			bool[] array = this.entitlementsSet;
			int entitlementIndex = (int)entitlementDef.entitlementIndex;
			bool flag = false;
			return ArrayUtils.GetSafe<bool>(array, entitlementIndex, flag);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002EF0 RID: 12016
		private PlayerCharacterMasterController playerCharacterMasterController;

		// Token: 0x04002EF1 RID: 12017
		private static readonly uint entitlementsSetDirtyBit = 1U;

		// Token: 0x04002EF2 RID: 12018
		private static readonly uint allDirtyBits = PlayerCharacterMasterControllerEntitlementTracker.entitlementsSetDirtyBit;

		// Token: 0x04002EF3 RID: 12019
		private bool[] entitlementsSet;
	}
}
