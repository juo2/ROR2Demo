using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200008C RID: 140
	public sealed class TitleStorageFileTransferRequest : Handle
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x000036D3 File Offset: 0x000018D3
		public TitleStorageFileTransferRequest()
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000036DB File Offset: 0x000018DB
		public TitleStorageFileTransferRequest(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00005FFD File Offset: 0x000041FD
		public Result CancelRequest()
		{
			return Bindings.EOS_TitleStorageFileTransferRequest_CancelRequest(base.InnerHandle);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000600A File Offset: 0x0000420A
		public Result GetFileRequestState()
		{
			return Bindings.EOS_TitleStorageFileTransferRequest_GetFileRequestState(base.InnerHandle);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00006018 File Offset: 0x00004218
		public Result GetFilename(out string outStringBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 64;
			Helper.TryMarshalAllocate(ref zero, num);
			Result result = Bindings.EOS_TitleStorageFileTransferRequest_GetFilename(base.InnerHandle, (uint)num, zero, ref num);
			Helper.TryMarshalGet(zero, out outStringBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00006056 File Offset: 0x00004256
		public void Release()
		{
			Bindings.EOS_TitleStorageFileTransferRequest_Release(base.InnerHandle);
		}
	}
}
