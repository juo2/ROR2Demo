using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000452 RID: 1106
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyTransactionByIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700082B RID: 2091
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0001C605 File Offset: 0x0001A805
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (set) Token: 0x06001AF7 RID: 6903 RVA: 0x0001C614 File Offset: 0x0001A814
		public string TransactionId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TransactionId, value);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0001C623 File Offset: 0x0001A823
		public void Set(CopyTransactionByIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TransactionId = other.TransactionId;
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0001C647 File Offset: 0x0001A847
		public void Set(object other)
		{
			this.Set(other as CopyTransactionByIdOptions);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0001C655 File Offset: 0x0001A855
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TransactionId);
		}

		// Token: 0x04000C8A RID: 3210
		private int m_ApiVersion;

		// Token: 0x04000C8B RID: 3211
		private IntPtr m_LocalUserId;

		// Token: 0x04000C8C RID: 3212
		private IntPtr m_TransactionId;
	}
}
