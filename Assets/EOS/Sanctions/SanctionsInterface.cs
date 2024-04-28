using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200015B RID: 347
	public sealed class SanctionsInterface : Handle
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x000036D3 File Offset: 0x000018D3
		public SanctionsInterface()
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000036DB File Offset: 0x000018DB
		public SanctionsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public Result CopyPlayerSanctionByIndex(CopyPlayerSanctionByIndexOptions options, out PlayerSanction outSanction)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyPlayerSanctionByIndexOptionsInternal, CopyPlayerSanctionByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sanctions_CopyPlayerSanctionByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<PlayerSanctionInternal, PlayerSanction>(zero2, out outSanction))
			{
				Bindings.EOS_Sanctions_PlayerSanction_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000A928 File Offset: 0x00008B28
		public uint GetPlayerSanctionCount(GetPlayerSanctionCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetPlayerSanctionCountOptionsInternal, GetPlayerSanctionCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Sanctions_GetPlayerSanctionCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0000A958 File Offset: 0x00008B58
		public void QueryActivePlayerSanctions(QueryActivePlayerSanctionsOptions options, object clientData, OnQueryActivePlayerSanctionsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryActivePlayerSanctionsOptionsInternal, QueryActivePlayerSanctionsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryActivePlayerSanctionsCallbackInternal onQueryActivePlayerSanctionsCallbackInternal = new OnQueryActivePlayerSanctionsCallbackInternal(SanctionsInterface.OnQueryActivePlayerSanctionsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryActivePlayerSanctionsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sanctions_QueryActivePlayerSanctions(base.InnerHandle, zero, zero2, onQueryActivePlayerSanctionsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000A9AC File Offset: 0x00008BAC
		[MonoPInvokeCallback(typeof(OnQueryActivePlayerSanctionsCallbackInternal))]
		internal static void OnQueryActivePlayerSanctionsCallbackInternalImplementation(IntPtr data)
		{
			OnQueryActivePlayerSanctionsCallback onQueryActivePlayerSanctionsCallback;
			QueryActivePlayerSanctionsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryActivePlayerSanctionsCallback, QueryActivePlayerSanctionsCallbackInfoInternal, QueryActivePlayerSanctionsCallbackInfo>(data, out onQueryActivePlayerSanctionsCallback, out data2))
			{
				onQueryActivePlayerSanctionsCallback(data2);
			}
		}

		// Token: 0x0400048E RID: 1166
		public const int CopyplayersanctionbyindexApiLatest = 1;

		// Token: 0x0400048F RID: 1167
		public const int GetplayersanctioncountApiLatest = 1;

		// Token: 0x04000490 RID: 1168
		public const int PlayersanctionApiLatest = 2;

		// Token: 0x04000491 RID: 1169
		public const int QueryactiveplayersanctionsApiLatest = 2;
	}
}
