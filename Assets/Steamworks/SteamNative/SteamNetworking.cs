using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000067 RID: 103
	internal class SteamNetworking : IDisposable
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00005908 File Offset: 0x00003B08
		internal SteamNetworking(BaseSteamworks steamworks, IntPtr pointer)
		{
			this.steamworks = steamworks;
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64(pointer);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32(pointer);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32(pointer);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64(pointer);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac(pointer);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00005985 File Offset: 0x00003B85
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000599C File Offset: 0x00003B9C
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000059B8 File Offset: 0x00003BB8
		public bool AcceptP2PSessionWithUser(CSteamID steamIDRemote)
		{
			return this.platform.ISteamNetworking_AcceptP2PSessionWithUser(steamIDRemote.Value);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000059CB File Offset: 0x00003BCB
		public bool AllowP2PPacketRelay(bool bAllow)
		{
			return this.platform.ISteamNetworking_AllowP2PPacketRelay(bAllow);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000059D9 File Offset: 0x00003BD9
		public bool CloseP2PChannelWithUser(CSteamID steamIDRemote, int nChannel)
		{
			return this.platform.ISteamNetworking_CloseP2PChannelWithUser(steamIDRemote.Value, nChannel);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000059ED File Offset: 0x00003BED
		public bool CloseP2PSessionWithUser(CSteamID steamIDRemote)
		{
			return this.platform.ISteamNetworking_CloseP2PSessionWithUser(steamIDRemote.Value);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005A00 File Offset: 0x00003C00
		public SNetSocket_t CreateConnectionSocket(uint nIP, ushort nPort, int nTimeoutSec)
		{
			return this.platform.ISteamNetworking_CreateConnectionSocket(nIP, nPort, nTimeoutSec);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005A10 File Offset: 0x00003C10
		public SNetListenSocket_t CreateListenSocket(int nVirtualP2PPort, uint nIP, ushort nPort, bool bAllowUseOfPacketRelay)
		{
			return this.platform.ISteamNetworking_CreateListenSocket(nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005A22 File Offset: 0x00003C22
		public SNetSocket_t CreateP2PConnectionSocket(CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay)
		{
			return this.platform.ISteamNetworking_CreateP2PConnectionSocket(steamIDTarget.Value, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005A39 File Offset: 0x00003C39
		public bool DestroyListenSocket(SNetListenSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			return this.platform.ISteamNetworking_DestroyListenSocket(hSocket.Value, bNotifyRemoteEnd);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005A4D File Offset: 0x00003C4D
		public bool DestroySocket(SNetSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			return this.platform.ISteamNetworking_DestroySocket(hSocket.Value, bNotifyRemoteEnd);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005A61 File Offset: 0x00003C61
		public bool GetListenSocketInfo(SNetListenSocket_t hListenSocket, out uint pnIP, out ushort pnPort)
		{
			return this.platform.ISteamNetworking_GetListenSocketInfo(hListenSocket.Value, out pnIP, out pnPort);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005A76 File Offset: 0x00003C76
		public int GetMaxPacketSize(SNetSocket_t hSocket)
		{
			return this.platform.ISteamNetworking_GetMaxPacketSize(hSocket.Value);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005A89 File Offset: 0x00003C89
		public bool GetP2PSessionState(CSteamID steamIDRemote, ref P2PSessionState_t pConnectionState)
		{
			return this.platform.ISteamNetworking_GetP2PSessionState(steamIDRemote.Value, ref pConnectionState);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005A9D File Offset: 0x00003C9D
		public SNetSocketConnectionType GetSocketConnectionType(SNetSocket_t hSocket)
		{
			return this.platform.ISteamNetworking_GetSocketConnectionType(hSocket.Value);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public bool GetSocketInfo(SNetSocket_t hSocket, out CSteamID pSteamIDRemote, IntPtr peSocketStatus, out uint punIPRemote, out ushort punPortRemote)
		{
			return this.platform.ISteamNetworking_GetSocketInfo(hSocket.Value, out pSteamIDRemote.Value, peSocketStatus, out punIPRemote, out punPortRemote);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005ACE File Offset: 0x00003CCE
		public bool IsDataAvailable(SNetListenSocket_t hListenSocket, out uint pcubMsgSize, ref SNetSocket_t phSocket)
		{
			return this.platform.ISteamNetworking_IsDataAvailable(hListenSocket.Value, out pcubMsgSize, ref phSocket.Value);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public bool IsDataAvailableOnSocket(SNetSocket_t hSocket, out uint pcubMsgSize)
		{
			return this.platform.ISteamNetworking_IsDataAvailableOnSocket(hSocket.Value, out pcubMsgSize);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005AFC File Offset: 0x00003CFC
		public bool IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel)
		{
			return this.platform.ISteamNetworking_IsP2PPacketAvailable(out pcubMsgSize, nChannel);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005B0B File Offset: 0x00003D0B
		public bool ReadP2PPacket(IntPtr pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel)
		{
			return this.platform.ISteamNetworking_ReadP2PPacket(pubDest, cubDest, out pcubMsgSize, out psteamIDRemote.Value, nChannel);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005B24 File Offset: 0x00003D24
		public bool RetrieveData(SNetListenSocket_t hListenSocket, IntPtr pubDest, uint cubDest, out uint pcubMsgSize, ref SNetSocket_t phSocket)
		{
			return this.platform.ISteamNetworking_RetrieveData(hListenSocket.Value, pubDest, cubDest, out pcubMsgSize, ref phSocket.Value);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005B42 File Offset: 0x00003D42
		public bool RetrieveDataFromSocket(SNetSocket_t hSocket, IntPtr pubDest, uint cubDest, out uint pcubMsgSize)
		{
			return this.platform.ISteamNetworking_RetrieveDataFromSocket(hSocket.Value, pubDest, cubDest, out pcubMsgSize);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005B59 File Offset: 0x00003D59
		public bool SendDataOnSocket(SNetSocket_t hSocket, IntPtr pubData, uint cubData, bool bReliable)
		{
			return this.platform.ISteamNetworking_SendDataOnSocket(hSocket.Value, pubData, cubData, bReliable);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00005B70 File Offset: 0x00003D70
		public bool SendP2PPacket(CSteamID steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel)
		{
			return this.platform.ISteamNetworking_SendP2PPacket(steamIDRemote.Value, pubData, cubData, eP2PSendType, nChannel);
		}

		// Token: 0x0400047E RID: 1150
		internal Platform.Interface platform;

		// Token: 0x0400047F RID: 1151
		internal BaseSteamworks steamworks;
	}
}
