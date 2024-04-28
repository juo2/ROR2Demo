using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200083B RID: 2107
	public class RadiotowerTerminal : NetworkBehaviour
	{
		// Token: 0x06002DFA RID: 11770 RVA: 0x000C3A91 File Offset: 0x000C1C91
		private void SetHasBeenPurchased(bool newHasBeenPurchased)
		{
			if (this.hasBeenPurchased != newHasBeenPurchased)
			{
				this.NetworkhasBeenPurchased = newHasBeenPurchased;
			}
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000C3AA3 File Offset: 0x000C1CA3
		public void Start()
		{
			if (NetworkServer.active)
			{
				this.FindStageLogUnlockable();
			}
			bool active = NetworkClient.active;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000C3AB8 File Offset: 0x000C1CB8
		private void FindStageLogUnlockable()
		{
			SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
			if (mostRecentSceneDef)
			{
				this.unlockableDef = SceneCatalog.GetUnlockableLogFromBaseSceneName(mostRecentSceneDef.baseSceneName);
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000C3AE4 File Offset: 0x000C1CE4
		[Server]
		public void GrantUnlock(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.RadiotowerTerminal::GrantUnlock(RoR2.Interactor)' called on client");
				return;
			}
			EffectManager.SpawnEffect(this.unlockEffect, new EffectData
			{
				origin = base.transform.position
			}, true);
			this.SetHasBeenPurchased(true);
			if (Run.instance)
			{
				Util.PlaySound(this.unlockSoundString, interactor.gameObject);
				Run.instance.GrantUnlockToAllParticipatingPlayers(this.unlockableDef);
				string pickupToken = "???";
				if (this.unlockableDef)
				{
					pickupToken = this.unlockableDef.nameToken;
				}
				Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
				{
					subjectAsCharacterBody = interactor.GetComponent<CharacterBody>(),
					baseToken = "PLAYER_PICKUP",
					pickupToken = pickupToken,
					pickupColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unlockable),
					pickupQuantity = 1U
				});
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000C3BB8 File Offset: 0x000C1DB8
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x000C3BCB File Offset: 0x000C1DCB
		public bool NetworkhasBeenPurchased
		{
			get
			{
				return this.hasBeenPurchased;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetHasBeenPurchased(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this.hasBeenPurchased, 1U);
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000C3C0C File Offset: 0x000C1E0C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.hasBeenPurchased);
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
				writer.Write(this.hasBeenPurchased);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000C3C78 File Offset: 0x000C1E78
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.hasBeenPurchased = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetHasBeenPurchased(reader.ReadBoolean());
			}
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002FE2 RID: 12258
		[SyncVar(hook = "SetHasBeenPurchased")]
		private bool hasBeenPurchased;

		// Token: 0x04002FE3 RID: 12259
		private UnlockableDef unlockableDef;

		// Token: 0x04002FE4 RID: 12260
		public string unlockSoundString;

		// Token: 0x04002FE5 RID: 12261
		public GameObject unlockEffect;
	}
}
