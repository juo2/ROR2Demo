using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000173 RID: 371
	public class User : IDisposable
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x0003807D File Offset: 0x0003627D
		internal User(Client c)
		{
			this.client = c;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00038097 File Offset: 0x00036297
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000380A0 File Offset: 0x000362A0
		public string GetRichPresence(string key)
		{
			string result;
			if (this.richPresence.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000380C0 File Offset: 0x000362C0
		public bool SetRichPresence(string key, string value)
		{
			this.richPresence[key] = value;
			return this.client.native.friends.SetRichPresence(key, value);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000380E6 File Offset: 0x000362E6
		public void ClearRichPresence()
		{
			this.richPresence.Clear();
			this.client.native.friends.ClearRichPresence();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00038108 File Offset: 0x00036308
		public User.UserHasLicenseForAppResult UserHasLicenseForApp(ulong steamId, uint appId)
		{
			return (User.UserHasLicenseForAppResult)this.client.native.user.UserHasLicenseForApp(steamId, appId);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003812C File Offset: 0x0003632C
		public void RequestEncryptedAppTicketAsync(byte[] dataToInclude)
		{
			this.ClearRequestNativeDataPointer();
			this.requestDataPtr = Marshal.AllocHGlobal(dataToInclude.Length);
			Marshal.Copy(dataToInclude, 0, this.requestDataPtr, dataToInclude.Length);
			try
			{
				this.client.native.user.RequestEncryptedAppTicket(this.requestDataPtr, dataToInclude.Length, delegate(EncryptedAppTicketResponse_t callback, bool error)
				{
					if (error || callback.Result != Result.OK)
					{
						this.ClearRequestNativeDataPointer();
						if (this.OnEncryptedAppTicketRequestComplete != null)
						{
							this.OnEncryptedAppTicketRequestComplete(false, null);
						}
						return;
					}
					IntPtr intPtr = Marshal.AllocHGlobal(1024);
					uint num = 0U;
					byte[] array = null;
					if (this.client.native.user.GetEncryptedAppTicket(intPtr, 1024, out num))
					{
						array = new byte[num];
						Marshal.Copy(intPtr, array, 0, (int)num);
					}
					Marshal.FreeHGlobal(intPtr);
					this.ClearRequestNativeDataPointer();
					if (this.OnEncryptedAppTicketRequestComplete != null)
					{
						this.OnEncryptedAppTicketRequestComplete(true, array);
					}
				});
			}
			catch (Exception)
			{
				this.ClearRequestNativeDataPointer();
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000381A4 File Offset: 0x000363A4
		internal void ClearRequestNativeDataPointer()
		{
			if (this.requestDataPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.requestDataPtr);
				this.requestDataPtr = IntPtr.Zero;
			}
		}

		// Token: 0x0400083E RID: 2110
		internal Client client;

		// Token: 0x0400083F RID: 2111
		internal Dictionary<string, string> richPresence = new Dictionary<string, string>();

		// Token: 0x04000840 RID: 2112
		public Action<bool, byte[]> OnEncryptedAppTicketRequestComplete;

		// Token: 0x04000841 RID: 2113
		internal CallbackHandle Callback;

		// Token: 0x04000842 RID: 2114
		internal IntPtr requestDataPtr;

		// Token: 0x02000287 RID: 647
		public enum UserHasLicenseForAppResult
		{
			// Token: 0x04000C39 RID: 3129
			HasLicense,
			// Token: 0x04000C3A RID: 3130
			DoesNotHaveLicense,
			// Token: 0x04000C3B RID: 3131
			NoAuth
		}
	}
}
