using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000260 RID: 608
	public sealed class PlayerDataStorageFileTransferRequest : Handle
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x000036D3 File Offset: 0x000018D3
		public PlayerDataStorageFileTransferRequest()
		{
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000036DB File Offset: 0x000018DB
		public PlayerDataStorageFileTransferRequest(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0001059C File Offset: 0x0000E79C
		public Result CancelRequest()
		{
			return Bindings.EOS_PlayerDataStorageFileTransferRequest_CancelRequest(base.InnerHandle);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000105A9 File Offset: 0x0000E7A9
		public Result GetFileRequestState()
		{
			return Bindings.EOS_PlayerDataStorageFileTransferRequest_GetFileRequestState(base.InnerHandle);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x000105B8 File Offset: 0x0000E7B8
		public Result GetFilename(out string outStringBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 64;
			Helper.TryMarshalAllocate(ref zero, num);
			Result result = Bindings.EOS_PlayerDataStorageFileTransferRequest_GetFilename(base.InnerHandle, (uint)num, zero, ref num);
			Helper.TryMarshalGet(zero, out outStringBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000105F6 File Offset: 0x0000E7F6
		public void Release()
		{
			Bindings.EOS_PlayerDataStorageFileTransferRequest_Release(base.InnerHandle);
		}
	}
}
