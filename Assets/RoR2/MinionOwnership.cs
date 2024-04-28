using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007B1 RID: 1969
	[DisallowMultipleComponent]
	public class MinionOwnership : NetworkBehaviour
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x000B3C23 File Offset: 0x000B1E23
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x000B3C2B File Offset: 0x000B1E2B
		public CharacterMaster ownerMaster { get; private set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x000B3C34 File Offset: 0x000B1E34
		// (set) Token: 0x0600298E RID: 10638 RVA: 0x000B3C3C File Offset: 0x000B1E3C
		public MinionOwnership.MinionGroup group { get; private set; }

		// Token: 0x0600298F RID: 10639 RVA: 0x000B3C45 File Offset: 0x000B1E45
		[Server]
		public void SetOwner(CharacterMaster newOwnerMaster)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.MinionOwnership::SetOwner(RoR2.CharacterMaster)' called on client");
				return;
			}
			this.NetworkownerMasterId = (newOwnerMaster ? newOwnerMaster.netId : NetworkInstanceId.Invalid);
			MinionOwnership.MinionGroup.SetMinionOwner(this, this.ownerMasterId);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000B3C83 File Offset: 0x000B1E83
		private void OnSyncOwnerMasterId(NetworkInstanceId newOwnerMasterId)
		{
			MinionOwnership.MinionGroup.SetMinionOwner(this, this.ownerMasterId);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000B3C91 File Offset: 0x000B1E91
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (!NetworkServer.active)
			{
				MinionOwnership.MinionGroup.SetMinionOwner(this, this.ownerMasterId);
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000B3CAC File Offset: 0x000B1EAC
		private void HandleGroupDiscovery(MinionOwnership.MinionGroup newGroup)
		{
			this.group = newGroup;
			Action<MinionOwnership> action = MinionOwnership.onMinionGroupChangedGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000B3CC8 File Offset: 0x000B1EC8
		private void HandleOwnerDiscovery(CharacterMaster newOwner)
		{
			if (this.ownerMaster != null)
			{
				Action<CharacterMaster> action = this.onOwnerLost;
				if (action != null)
				{
					action(this.ownerMaster);
				}
			}
			this.ownerMaster = newOwner;
			if (this.ownerMaster != null)
			{
				Action<CharacterMaster> action2 = this.onOwnerDiscovered;
				if (action2 != null)
				{
					action2(this.ownerMaster);
				}
			}
			Action<MinionOwnership> action3 = MinionOwnership.onMinionOwnerChangedGlobal;
			if (action3 == null)
			{
				return;
			}
			action3(this);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000B3D2A File Offset: 0x000B1F2A
		private void OnDestroy()
		{
			MinionOwnership.MinionGroup.SetMinionOwner(this, NetworkInstanceId.Invalid);
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06002995 RID: 10645 RVA: 0x000B3D38 File Offset: 0x000B1F38
		// (remove) Token: 0x06002996 RID: 10646 RVA: 0x000B3D70 File Offset: 0x000B1F70
		public event Action<CharacterMaster> onOwnerDiscovered;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06002997 RID: 10647 RVA: 0x000B3DA8 File Offset: 0x000B1FA8
		// (remove) Token: 0x06002998 RID: 10648 RVA: 0x000B3DE0 File Offset: 0x000B1FE0
		public event Action<CharacterMaster> onOwnerLost;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06002999 RID: 10649 RVA: 0x000B3E18 File Offset: 0x000B2018
		// (remove) Token: 0x0600299A RID: 10650 RVA: 0x000B3E4C File Offset: 0x000B204C
		public static event Action<MinionOwnership> onMinionGroupChangedGlobal;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x0600299B RID: 10651 RVA: 0x000B3E80 File Offset: 0x000B2080
		// (remove) Token: 0x0600299C RID: 10652 RVA: 0x000B3EB4 File Offset: 0x000B20B4
		public static event Action<MinionOwnership> onMinionOwnerChangedGlobal;

		// Token: 0x0600299D RID: 10653 RVA: 0x000B3EE8 File Offset: 0x000B20E8
		[AssetCheck(typeof(CharacterMaster))]
		private static void AddMinionOwnershipComponent(AssetCheckArgs args)
		{
			CharacterMaster characterMaster = args.asset as CharacterMaster;
			if (!characterMaster.GetComponent<MinionOwnership>())
			{
				characterMaster.gameObject.AddComponent<MinionOwnership>();
				args.UpdatePrefab();
			}
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000B3F20 File Offset: 0x000B2120
		private void OnValidate()
		{
			if (base.GetComponents<MinionOwnership>().Length > 1)
			{
				Debug.LogError("Only one MinionOwnership is allowed per object!", this);
			}
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000B3F4C File Offset: 0x000B214C
		// (set) Token: 0x060029A2 RID: 10658 RVA: 0x000B3F5F File Offset: 0x000B215F
		public NetworkInstanceId NetworkownerMasterId
		{
			get
			{
				return this.ownerMasterId;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncOwnerMasterId(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkInstanceId>(value, ref this.ownerMasterId, 1U);
			}
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000B3FA0 File Offset: 0x000B21A0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.ownerMasterId);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.ownerMasterId);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000B400C File Offset: 0x000B220C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.ownerMasterId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncOwnerMasterId(reader.ReadNetworkId());
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002CF9 RID: 11513
		[SyncVar(hook = "OnSyncOwnerMasterId")]
		private NetworkInstanceId ownerMasterId = NetworkInstanceId.Invalid;

		// Token: 0x020007B2 RID: 1970
		public class MinionGroup : IDisposable
		{
			// Token: 0x060029A6 RID: 10662 RVA: 0x000B4050 File Offset: 0x000B2250
			public static MinionOwnership.MinionGroup FindGroup(NetworkInstanceId ownerId)
			{
				foreach (MinionOwnership.MinionGroup minionGroup in MinionOwnership.MinionGroup.instancesList)
				{
					if (minionGroup.ownerId == ownerId)
					{
						return minionGroup;
					}
				}
				return null;
			}

			// Token: 0x060029A7 RID: 10663 RVA: 0x000B40B0 File Offset: 0x000B22B0
			public static void SetMinionOwner(MinionOwnership minion, NetworkInstanceId ownerId)
			{
				if (minion.group != null)
				{
					if (minion.group.ownerId == ownerId)
					{
						return;
					}
					MinionOwnership.MinionGroup.RemoveMinion(minion.group.ownerId, minion);
				}
				if (ownerId != NetworkInstanceId.Invalid)
				{
					MinionOwnership.MinionGroup.AddMinion(ownerId, minion);
				}
			}

			// Token: 0x060029A8 RID: 10664 RVA: 0x000B4100 File Offset: 0x000B2300
			private static void AddMinion(NetworkInstanceId ownerId, MinionOwnership minion)
			{
				MinionOwnership.MinionGroup minionGroup = null;
				for (int i = 0; i < MinionOwnership.MinionGroup.instancesList.Count; i++)
				{
					MinionOwnership.MinionGroup minionGroup2 = MinionOwnership.MinionGroup.instancesList[i];
					if (MinionOwnership.MinionGroup.instancesList[i].ownerId == ownerId)
					{
						minionGroup = minionGroup2;
						break;
					}
				}
				if (minionGroup == null)
				{
					minionGroup = new MinionOwnership.MinionGroup(ownerId);
				}
				minionGroup.AddMember(minion);
				minionGroup.AttemptToResolveOwner();
				CharacterMaster component = minion.GetComponent<CharacterMaster>();
				if (component)
				{
					component.inventory.GiveItem(RoR2Content.Items.MinionLeash, 1);
				}
			}

			// Token: 0x060029A9 RID: 10665 RVA: 0x000B4184 File Offset: 0x000B2384
			private static void RemoveMinion(NetworkInstanceId ownerId, MinionOwnership minion)
			{
				CharacterMaster component = minion.GetComponent<CharacterMaster>();
				if (component)
				{
					component.inventory.RemoveItem(RoR2Content.Items.MinionLeash, 1);
				}
				MinionOwnership.MinionGroup minionGroup = null;
				for (int i = 0; i < MinionOwnership.MinionGroup.instancesList.Count; i++)
				{
					MinionOwnership.MinionGroup minionGroup2 = MinionOwnership.MinionGroup.instancesList[i];
					if (MinionOwnership.MinionGroup.instancesList[i].ownerId == ownerId)
					{
						minionGroup = minionGroup2;
						break;
					}
				}
				if (minionGroup == null)
				{
					throw new InvalidOperationException(string.Format("{0}.{1} Could not find group to which {2} belongs", "MinionGroup", "RemoveMinion", minion));
				}
				minionGroup.RemoveMember(minion);
				if (minionGroup.refCount == 0 && !minionGroup.resolvedOwnerGameObject)
				{
					minionGroup.Dispose();
				}
			}

			// Token: 0x060029AA RID: 10666 RVA: 0x000B4230 File Offset: 0x000B2430
			private MinionGroup(NetworkInstanceId ownerId)
			{
				this.ownerId = ownerId;
				this._members = new MinionOwnership[4];
				this._memberCount = 0;
				MinionOwnership.MinionGroup.instancesList.Add(this);
			}

			// Token: 0x060029AB RID: 10667 RVA: 0x000B4260 File Offset: 0x000B2460
			public void Dispose()
			{
				for (int i = this._memberCount - 1; i >= 0; i--)
				{
					this.RemoveMemberAt(i);
				}
				MinionOwnership.MinionGroup.instancesList.Remove(this);
			}

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x060029AC RID: 10668 RVA: 0x000B4293 File Offset: 0x000B2493
			public MinionOwnership[] members
			{
				get
				{
					return this._members;
				}
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x060029AD RID: 10669 RVA: 0x000B429B File Offset: 0x000B249B
			public int memberCount
			{
				get
				{
					return this._memberCount;
				}
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x060029AE RID: 10670 RVA: 0x000B42A3 File Offset: 0x000B24A3
			public bool isMinion
			{
				get
				{
					return this.ownerId != NetworkInstanceId.Invalid;
				}
			}

			// Token: 0x060029AF RID: 10671 RVA: 0x000B42B8 File Offset: 0x000B24B8
			private void AttemptToResolveOwner()
			{
				if (this.resolved)
				{
					return;
				}
				this.resolvedOwnerGameObject = Util.FindNetworkObject(this.ownerId);
				if (!this.resolvedOwnerGameObject)
				{
					return;
				}
				this.resolved = true;
				this.resolvedOwnerMaster = this.resolvedOwnerGameObject.GetComponent<CharacterMaster>();
				this.resolvedOwnerGameObject.AddComponent<MinionOwnership.MinionGroup.MinionGroupDestroyer>().group = this;
				this.refCount++;
				for (int i = 0; i < this._memberCount; i++)
				{
					this._members[i].HandleOwnerDiscovery(this.resolvedOwnerMaster);
				}
			}

			// Token: 0x060029B0 RID: 10672 RVA: 0x000B4348 File Offset: 0x000B2548
			public void AddMember(MinionOwnership minion)
			{
				ArrayUtils.ArrayAppend<MinionOwnership>(ref this._members, ref this._memberCount, minion);
				this.refCount++;
				minion.HandleGroupDiscovery(this);
				if (this.resolvedOwnerMaster)
				{
					minion.HandleOwnerDiscovery(this.resolvedOwnerMaster);
				}
			}

			// Token: 0x060029B1 RID: 10673 RVA: 0x000B4396 File Offset: 0x000B2596
			public void RemoveMember(MinionOwnership minion)
			{
				this.RemoveMemberAt(Array.IndexOf<MinionOwnership>(this._members, minion));
				this.refCount--;
			}

			// Token: 0x060029B2 RID: 10674 RVA: 0x000B43B8 File Offset: 0x000B25B8
			private void RemoveMemberAt(int i)
			{
				MinionOwnership minionOwnership = this._members[i];
				ArrayUtils.ArrayRemoveAt<MinionOwnership>(this._members, ref this._memberCount, i, 1);
				minionOwnership.HandleOwnerDiscovery(null);
				minionOwnership.HandleGroupDiscovery(null);
			}

			// Token: 0x060029B3 RID: 10675 RVA: 0x000B43E4 File Offset: 0x000B25E4
			[ConCommand(commandName = "minion_dump", flags = ConVarFlags.None, helpText = "Prints debug information about all active minion groups.")]
			private static void CCMinionPrint(ConCommandArgs args)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < MinionOwnership.MinionGroup.instancesList.Count; i++)
				{
					MinionOwnership.MinionGroup minionGroup = MinionOwnership.MinionGroup.instancesList[i];
					stringBuilder.Append("group [").Append(i).Append("] size=").Append(minionGroup._memberCount).Append(" id=").Append(minionGroup.ownerId).Append(" resolvedOwnerGameObject=").Append(minionGroup.resolvedOwnerGameObject).AppendLine();
					for (int j = 0; j < minionGroup._memberCount; j++)
					{
						stringBuilder.Append("  ").Append("[").Append(j).Append("] member.name=").Append(minionGroup._members[j].name).AppendLine();
					}
				}
				Debug.Log(stringBuilder.ToString());
			}

			// Token: 0x04002D00 RID: 11520
			private static readonly List<MinionOwnership.MinionGroup> instancesList = new List<MinionOwnership.MinionGroup>();

			// Token: 0x04002D01 RID: 11521
			public readonly NetworkInstanceId ownerId;

			// Token: 0x04002D02 RID: 11522
			private MinionOwnership[] _members;

			// Token: 0x04002D03 RID: 11523
			private int _memberCount;

			// Token: 0x04002D04 RID: 11524
			private int refCount;

			// Token: 0x04002D05 RID: 11525
			private bool resolved;

			// Token: 0x04002D06 RID: 11526
			private GameObject resolvedOwnerGameObject;

			// Token: 0x04002D07 RID: 11527
			private CharacterMaster resolvedOwnerMaster;

			// Token: 0x020007B3 RID: 1971
			private class MinionGroupDestroyer : MonoBehaviour
			{
				// Token: 0x060029B5 RID: 10677 RVA: 0x000B44E3 File Offset: 0x000B26E3
				private void OnDestroy()
				{
					this.group.Dispose();
					this.group.refCount--;
				}

				// Token: 0x04002D08 RID: 11528
				public MinionOwnership.MinionGroup group;
			}
		}
	}
}
