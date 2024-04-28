using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200066A RID: 1642
	public class CombatSquad : NetworkBehaviour
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0008927A File Offset: 0x0008747A
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00089282 File Offset: 0x00087482
		public ReadOnlyCollection<CharacterMaster> readOnlyMembersList { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0008928B File Offset: 0x0008748B
		public int memberCount
		{
			get
			{
				return this.membersList.Count;
			}
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x00089298 File Offset: 0x00087498
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.onDestroyCallbacksServer = new List<OnDestroyCallback>();
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathCallback;
				MasterSummon.onServerMasterSummonGlobal += this.OnServerMasterSummonGlobal;
			}
			this.readOnlyMembersList = new ReadOnlyCollection<CharacterMaster>(this.membersList);
			this.awakeTime = Run.FixedTimeStamp.now;
			InstanceTracker.Add<CombatSquad>(this);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000892FB File Offset: 0x000874FB
		private void OnEnable()
		{
			if (NetworkServer.active)
			{
				base.SetDirtyBit(1U);
			}
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0008930C File Offset: 0x0008750C
		private void OnDestroy()
		{
			InstanceTracker.Remove<CombatSquad>(this);
			if (NetworkServer.active)
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathCallback;
				MasterSummon.onServerMasterSummonGlobal -= this.OnServerMasterSummonGlobal;
			}
			for (int i = this.membersList.Count - 1; i >= 0; i--)
			{
				this.RemoveMemberAt(i);
			}
			this.onDestroyCallbacksServer = null;
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00089370 File Offset: 0x00087570
		[Server]
		private void OnServerMasterSummonGlobal(MasterSummon.MasterSummonReport report)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CombatSquad::OnServerMasterSummonGlobal(RoR2.MasterSummon/MasterSummonReport)' called on client");
				return;
			}
			if (this.propagateMembershipToSummons && report.leaderMasterInstance && this.HasContainedMember(report.leaderMasterInstance.netId))
			{
				this.AddMember(report.summonMasterInstance);
			}
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000893C8 File Offset: 0x000875C8
		[Server]
		public void AddMember(CharacterMaster memberMaster)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CombatSquad::AddMember(RoR2.CharacterMaster)' called on client");
				return;
			}
			if (this.membersList.Count >= 255)
			{
				Debug.LogFormat("Cannot add character {0} to CombatGroup! Limit of {1} members already reached.", new object[]
				{
					memberMaster,
					byte.MaxValue
				});
				return;
			}
			this.membersList.Add(memberMaster);
			this.memberHistory.Add(memberMaster.netId);
			base.SetDirtyBit(1U);
			this.onDestroyCallbacksServer.Add(OnDestroyCallback.AddCallback(memberMaster.gameObject, new Action<OnDestroyCallback>(this.OnMemberDestroyedServer)));
			Action<CharacterMaster> action = this.onMemberAddedServer;
			if (action != null)
			{
				action(memberMaster);
			}
			Action<CharacterMaster> action2 = this.onMemberDiscovered;
			if (action2 == null)
			{
				return;
			}
			action2(memberMaster);
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001FE5 RID: 8165 RVA: 0x00089488 File Offset: 0x00087688
		// (remove) Token: 0x06001FE6 RID: 8166 RVA: 0x000894C0 File Offset: 0x000876C0
		public event Action onDefeatedServer;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001FE7 RID: 8167 RVA: 0x000894F8 File Offset: 0x000876F8
		// (remove) Token: 0x06001FE8 RID: 8168 RVA: 0x00089530 File Offset: 0x00087730
		public event Action<CharacterMaster, DamageReport> onMemberDeathServer;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001FE9 RID: 8169 RVA: 0x00089568 File Offset: 0x00087768
		// (remove) Token: 0x06001FEA RID: 8170 RVA: 0x000895A0 File Offset: 0x000877A0
		public event Action<CharacterMaster, DamageReport> onMemberDefeatedServer;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001FEB RID: 8171 RVA: 0x000895D8 File Offset: 0x000877D8
		// (remove) Token: 0x06001FEC RID: 8172 RVA: 0x00089610 File Offset: 0x00087810
		public event Action<CharacterMaster> onMemberAddedServer;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06001FED RID: 8173 RVA: 0x00089648 File Offset: 0x00087848
		// (remove) Token: 0x06001FEE RID: 8174 RVA: 0x00089680 File Offset: 0x00087880
		public event Action<CharacterMaster> onMemberDiscovered;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001FEF RID: 8175 RVA: 0x000896B8 File Offset: 0x000878B8
		// (remove) Token: 0x06001FF0 RID: 8176 RVA: 0x000896F0 File Offset: 0x000878F0
		public event Action<CharacterMaster> onMemberLost;

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00089728 File Offset: 0x00087928
		[Server]
		private void OnCharacterDeathCallback(DamageReport damageReport)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CombatSquad::OnCharacterDeathCallback(RoR2.DamageReport)' called on client");
				return;
			}
			CharacterMaster victimMaster = damageReport.victimMaster;
			if (!victimMaster)
			{
				return;
			}
			int num = this.membersList.IndexOf(victimMaster);
			if (num >= 0)
			{
				Action<CharacterMaster, DamageReport> action = this.onMemberDeathServer;
				if (action != null)
				{
					action(victimMaster, damageReport);
				}
				if (victimMaster.IsDeadAndOutOfLivesServer())
				{
					Action<CharacterMaster, DamageReport> action2 = this.onMemberDefeatedServer;
					if (action2 != null)
					{
						action2(victimMaster, damageReport);
					}
					this.RemoveMemberAt(num);
					if (!this.defeatedServer && this.membersList.Count == 0)
					{
						this.TriggerDefeat();
					}
				}
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000897BC File Offset: 0x000879BC
		[Server]
		private void RemoveMember(CharacterMaster memberMaster)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CombatSquad::RemoveMember(RoR2.CharacterMaster)' called on client");
				return;
			}
			int num = this.membersList.IndexOf(memberMaster);
			if (num != -1)
			{
				this.RemoveMemberAt(num);
			}
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000897F8 File Offset: 0x000879F8
		private void RemoveMemberAt(int memberIndex)
		{
			CharacterMaster obj = this.membersList[memberIndex];
			this.membersList.RemoveAt(memberIndex);
			if (this.onDestroyCallbacksServer != null)
			{
				this.onDestroyCallbacksServer.RemoveAt(memberIndex);
			}
			base.SetDirtyBit(1U);
			Action<CharacterMaster> action = this.onMemberLost;
			if (action == null)
			{
				return;
			}
			action(obj);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0008984C File Offset: 0x00087A4C
		[Server]
		public void OnMemberDestroyedServer(OnDestroyCallback onDestroyCallback)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CombatSquad::OnMemberDestroyedServer(RoR2.OnDestroyCallback)' called on client");
				return;
			}
			if (onDestroyCallback)
			{
				GameObject gameObject = onDestroyCallback.gameObject;
				CharacterMaster characterMaster = gameObject ? gameObject.GetComponent<CharacterMaster>() : null;
				for (int i = 0; i < this.membersList.Count; i++)
				{
					if (this.membersList[i] == characterMaster)
					{
						this.RemoveMemberAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000898BC File Offset: 0x00087ABC
		public bool ContainsMember(CharacterMaster master)
		{
			for (int i = 0; i < this.membersList.Count; i++)
			{
				if (this.membersList[i] == master)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000898F1 File Offset: 0x00087AF1
		public bool HasContainedMember(NetworkInstanceId id)
		{
			return this.memberHistory.Contains(id);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00089900 File Offset: 0x00087B00
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 1U;
			}
			bool flag = (num & 1U) > 0U;
			writer.Write((byte)num);
			if (flag)
			{
				writer.Write((byte)this.membersList.Count);
				for (int i = 0; i < this.membersList.Count; i++)
				{
					CharacterMaster characterMaster = this.membersList[i];
					GameObject value = characterMaster ? characterMaster.gameObject : null;
					writer.Write(value);
				}
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00089980 File Offset: 0x00087B80
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadByte() & 1) > 0)
			{
				List<CharacterMaster> list = CollectionPool<CharacterMaster, List<CharacterMaster>>.RentCollection();
				List<CharacterMaster> list2 = CollectionPool<CharacterMaster, List<CharacterMaster>>.RentCollection();
				List<CharacterMaster> list3 = CollectionPool<CharacterMaster, List<CharacterMaster>>.RentCollection();
				byte b = reader.ReadByte();
				for (byte b2 = 0; b2 < b; b2 += 1)
				{
					GameObject gameObject = reader.ReadGameObject();
					CharacterMaster item = gameObject ? gameObject.GetComponent<CharacterMaster>() : null;
					list3.Add(item);
				}
				ListUtils.FindExclusiveEntriesByReference<CharacterMaster>(this.membersList, list3, list, list2);
				Util.Swap<List<CharacterMaster>>(ref list3, ref this.membersList);
				CollectionPool<CharacterMaster, List<CharacterMaster>>.ReturnCollection(list3);
				for (int i = 0; i < list.Count; i++)
				{
					CharacterMaster characterMaster = list[i];
					if (characterMaster)
					{
						try
						{
							Action<CharacterMaster> action = this.onMemberLost;
							if (action != null)
							{
								action(characterMaster);
							}
						}
						catch (Exception message)
						{
							Debug.LogError(message);
						}
					}
				}
				for (int j = 0; j < list2.Count; j++)
				{
					CharacterMaster characterMaster2 = list2[j];
					if (characterMaster2)
					{
						try
						{
							Action<CharacterMaster> action2 = this.onMemberDiscovered;
							if (action2 != null)
							{
								action2(characterMaster2);
							}
						}
						catch (Exception message2)
						{
							Debug.LogError(message2);
						}
					}
				}
				CollectionPool<CharacterMaster, List<CharacterMaster>>.ReturnCollection(list2);
				CollectionPool<CharacterMaster, List<CharacterMaster>>.ReturnCollection(list);
			}
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00089AC0 File Offset: 0x00087CC0
		private void FixedUpdate()
		{
			if (NetworkServer.active && !this.defeatedServer && this.memberHistory.Count > 0)
			{
				bool flag = false;
				foreach (CharacterMaster characterMaster in this.membersList)
				{
					if (characterMaster.hasBody || characterMaster.IsExtraLifePendingServer())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Debug.LogError("CombatSquad has no living members.  Triggering defeat...");
					while (this.membersList.Count > 0)
					{
						this.RemoveMember(this.membersList[0]);
					}
					this.TriggerDefeat();
				}
			}
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00089B7C File Offset: 0x00087D7C
		private void TriggerDefeat()
		{
			this.defeatedServer = true;
			Action action = this.onDefeatedServer;
			if (action != null)
			{
				action();
			}
			UnityEvent unityEvent = this.onDefeatedServerLogicEvent;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			List<CharacterMaster> list = this.membersList;
			if (list != null)
			{
				list.Clear();
			}
			this.CallRpcOnDefeatedClient();
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00089BC9 File Offset: 0x00087DC9
		[ClientRpc]
		private void RpcOnDefeatedClient()
		{
			UnityEvent unityEvent = this.onDefeatedClientLogicEvent;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00089C00 File Offset: 0x00087E00
		protected static void InvokeRpcRpcOnDefeatedClient(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcOnDefeatedClient called on server.");
				return;
			}
			((CombatSquad)obj).RpcOnDefeatedClient();
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00089C24 File Offset: 0x00087E24
		public void CallRpcOnDefeatedClient()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcOnDefeatedClient called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)CombatSquad.kRpcRpcOnDefeatedClient);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcOnDefeatedClient");
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00089C8D File Offset: 0x00087E8D
		static CombatSquad()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(CombatSquad), CombatSquad.kRpcRpcOnDefeatedClient, new NetworkBehaviour.CmdDelegate(CombatSquad.InvokeRpcRpcOnDefeatedClient));
			NetworkCRC.RegisterBehaviour("CombatSquad", 0);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400256C RID: 9580
		private List<CharacterMaster> membersList = new List<CharacterMaster>();

		// Token: 0x0400256D RID: 9581
		private List<NetworkInstanceId> memberHistory = new List<NetworkInstanceId>();

		// Token: 0x0400256F RID: 9583
		public bool propagateMembershipToSummons;

		// Token: 0x04002570 RID: 9584
		[Tooltip("Grants a bonus health boost to members of the combat squad, depending on the number of players in the game.")]
		public bool grantBonusHealthInMultiplayer = true;

		// Token: 0x04002571 RID: 9585
		private List<OnDestroyCallback> onDestroyCallbacksServer;

		// Token: 0x04002572 RID: 9586
		private bool defeatedServer;

		// Token: 0x04002573 RID: 9587
		private const uint membersListDirtyBit = 1U;

		// Token: 0x04002574 RID: 9588
		private const uint allDirtyBits = 1U;

		// Token: 0x04002575 RID: 9589
		[NonSerialized]
		public Run.FixedTimeStamp awakeTime;

		// Token: 0x04002577 RID: 9591
		public UnityEvent onDefeatedClientLogicEvent;

		// Token: 0x04002578 RID: 9592
		public UnityEvent onDefeatedServerLogicEvent;

		// Token: 0x0400257E RID: 9598
		private static int kRpcRpcOnDefeatedClient = -1235734536;
	}
}
