using System;
using System.Globalization;
using Facepunch.Steamworks;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D93 RID: 3475
	public class SteamJoinClipboardLobby : MonoBehaviour
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x00149105 File Offset: 0x00147305
		// (set) Token: 0x06004F8A RID: 20362 RVA: 0x0014910D File Offset: 0x0014730D
		public bool validClipboardLobbyID { get; private set; }

		// Token: 0x06004F8B RID: 20363 RVA: 0x00149116 File Offset: 0x00147316
		private void OnEnable()
		{
			SingletonHelper.Assign<SteamJoinClipboardLobby>(ref SteamJoinClipboardLobby.instance, this);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00149123 File Offset: 0x00147323
		private void OnDisable()
		{
			SingletonHelper.Unassign<SteamJoinClipboardLobby>(ref SteamJoinClipboardLobby.instance, this);
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x00149130 File Offset: 0x00147330
		[SystemInitializer(new Type[]
		{

		})]
		public void Init()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyJoined = (Action<bool>)Delegate.Combine(lobbyManager.onLobbyJoined, new Action<bool>(SteamJoinClipboardLobby.OnLobbyJoined));
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x00149158 File Offset: 0x00147358
		private static void OnLobbyJoined(bool success)
		{
			if (SteamJoinClipboardLobby.instance && SteamJoinClipboardLobby.instance.resultTextComponent)
			{
				SteamJoinClipboardLobby.instance.resultTextTimer = 4f;
				SteamJoinClipboardLobby.instance.resultTextComponent.enabled = true;
				SteamJoinClipboardLobby.instance.resultTextComponent.SetText(Language.GetString(success ? "STEAM_JOIN_LOBBY_CLIPBOARD_SUCCESS" : "STEAM_JOIN_LOBBY_CLIPBOARD_FAIL"), true);
			}
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x001491C5 File Offset: 0x001473C5
		private static bool IsLobbyIdValid(CSteamID lobbyId)
		{
			return lobbyId != CSteamID.nil;
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x001491D2 File Offset: 0x001473D2
		private static CSteamID FetchClipboardLobbyId()
		{
			if (PlatformSystems.EgsToggleConVar.value == 1)
			{
				return SteamJoinClipboardLobby.FetchClipboardLobbyIdEGS();
			}
			return SteamJoinClipboardLobby.FetchClipboardLobbyIdSTEAM();
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x001491EC File Offset: 0x001473EC
		private static CSteamID FetchClipboardLobbyIdSTEAM()
		{
			CSteamID csteamID;
			if (CSteamID.TryParse(GUIUtility.systemCopyBuffer, out csteamID))
			{
				Client client = Client.Instance;
				CSteamID a = new CSteamID((client != null) ? client.Lobby.CurrentLobby : CSteamID.nil.steamValue);
				if (csteamID.isLobby && a != csteamID)
				{
					return csteamID;
				}
			}
			return CSteamID.nil;
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x00149248 File Offset: 0x00147448
		private static CSteamID FetchClipboardLobbyIdEGS()
		{
			string systemCopyBuffer = GUIUtility.systemCopyBuffer;
			if ((PlatformSystems.lobbyManager as EOSLobbyManager).CurrentLobbyId != systemCopyBuffer && systemCopyBuffer != string.Empty)
			{
				return new CSteamID(systemCopyBuffer);
			}
			return CSteamID.nil;
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0014928C File Offset: 0x0014748C
		private void FixedUpdate()
		{
			this.clipboardLobbyID = SteamJoinClipboardLobby.FetchClipboardLobbyId();
			this.validClipboardLobbyID = SteamJoinClipboardLobby.IsLobbyIdValid(this.clipboardLobbyID);
			if (this.mpButton != null)
			{
				this.mpButton.interactable = this.validClipboardLobbyID;
			}
			if (this.resultTextComponent != null)
			{
				if (this.resultTextTimer > 0f)
				{
					this.resultTextTimer -= Time.fixedDeltaTime;
					this.resultTextComponent.enabled = true;
					return;
				}
				this.resultTextComponent.enabled = false;
			}
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0014931A File Offset: 0x0014751A
		public void TryToJoinClipboardLobby()
		{
			Console.instance.SubmitCmd(null, string.Format(CultureInfo.InvariantCulture, "steam_lobby_join {0}", this.clipboardLobbyID.ToString()), true);
		}

		// Token: 0x04004C32 RID: 19506
		public TextMeshProUGUI buttonText;

		// Token: 0x04004C33 RID: 19507
		public TextMeshProUGUI resultTextComponent;

		// Token: 0x04004C34 RID: 19508
		public MPButton mpButton;

		// Token: 0x04004C35 RID: 19509
		private CSteamID clipboardLobbyID;

		// Token: 0x04004C37 RID: 19511
		private const float resultTextDuration = 4f;

		// Token: 0x04004C38 RID: 19512
		protected float resultTextTimer;

		// Token: 0x04004C39 RID: 19513
		private static SteamJoinClipboardLobby instance;
	}
}
