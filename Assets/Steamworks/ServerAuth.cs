using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200017B RID: 379
	public class ServerAuth
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x00039E8B File Offset: 0x0003808B
		internal ServerAuth(Server s)
		{
			this.server = s;
			this.server.RegisterCallback<ValidateAuthTicketResponse_t>(new Action<ValidateAuthTicketResponse_t>(this.OnAuthTicketValidate));
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00039EB1 File Offset: 0x000380B1
		private void OnAuthTicketValidate(ValidateAuthTicketResponse_t data)
		{
			if (this.OnAuthChange != null)
			{
				this.OnAuthChange(data.SteamID, data.OwnerSteamID, (ServerAuth.Status)data.AuthSessionResponse);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00039ED8 File Offset: 0x000380D8
		public unsafe bool StartSession(byte[] data, ulong steamid)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return this.server.native.gameServer.BeginAuthSession((IntPtr)((void*)value), data.Length, steamid) == BeginAuthSessionResult.OK;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00039F26 File Offset: 0x00038126
		public void EndSession(ulong steamid)
		{
			this.server.native.gameServer.EndAuthSession(steamid);
		}

		// Token: 0x0400087D RID: 2173
		internal Server server;

		// Token: 0x0400087E RID: 2174
		public Action<ulong, ulong, ServerAuth.Status> OnAuthChange;

		// Token: 0x0200029E RID: 670
		public enum Status
		{
			// Token: 0x04000D0E RID: 3342
			OK,
			// Token: 0x04000D0F RID: 3343
			UserNotConnectedToSteam,
			// Token: 0x04000D10 RID: 3344
			NoLicenseOrExpired,
			// Token: 0x04000D11 RID: 3345
			VACBanned,
			// Token: 0x04000D12 RID: 3346
			LoggedInElseWhere,
			// Token: 0x04000D13 RID: 3347
			VACCheckTimedOut,
			// Token: 0x04000D14 RID: 3348
			AuthTicketCanceled,
			// Token: 0x04000D15 RID: 3349
			AuthTicketInvalidAlreadyUsed,
			// Token: 0x04000D16 RID: 3350
			AuthTicketInvalid,
			// Token: 0x04000D17 RID: 3351
			PublisherIssuedBan
		}
	}
}
