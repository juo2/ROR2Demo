using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000004 RID: 4
	internal abstract class CallResult : CallbackHandle
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002136 File Offset: 0x00000336
		public override bool IsValid
		{
			get
			{
				return this.Call > 0UL;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002147 File Offset: 0x00000347
		internal CallResult(BaseSteamworks steamworks, SteamAPICall_t call) : base(steamworks)
		{
			this.Call = call;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		internal void Try()
		{
			bool flag = false;
			if (!this.Steamworks.native.utils.IsAPICallCompleted(this.Call, ref flag))
			{
				return;
			}
			this.Steamworks.UnregisterCallResult(this);
			this.RunCallback();
		}

		// Token: 0x06000009 RID: 9
		internal abstract void RunCallback();

		// Token: 0x0400000A RID: 10
		internal SteamAPICall_t Call;
	}
}
