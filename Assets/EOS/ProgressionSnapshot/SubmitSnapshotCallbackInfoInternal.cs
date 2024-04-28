using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000200 RID: 512
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SubmitSnapshotCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		public uint SnapshotId
		{
			get
			{
				return this.m_SnapshotId;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0000E8DC File Offset: 0x0000CADC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000659 RID: 1625
		private Result m_ResultCode;

		// Token: 0x0400065A RID: 1626
		private uint m_SnapshotId;

		// Token: 0x0400065B RID: 1627
		private IntPtr m_ClientData;
	}
}
