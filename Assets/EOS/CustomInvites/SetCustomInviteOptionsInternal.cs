using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A9 RID: 1193
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetCustomInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008D3 RID: 2259
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x0001E94E File Offset: 0x0001CB4E
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0001E95D File Offset: 0x0001CB5D
		public string Payload
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Payload, value);
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0001E96C File Offset: 0x0001CB6C
		public void Set(SetCustomInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Payload = other.Payload;
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0001E990 File Offset: 0x0001CB90
		public void Set(object other)
		{
			this.Set(other as SetCustomInviteOptions);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0001E99E File Offset: 0x0001CB9E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Payload);
		}

		// Token: 0x04000D82 RID: 3458
		private int m_ApiVersion;

		// Token: 0x04000D83 RID: 3459
		private IntPtr m_LocalUserId;

		// Token: 0x04000D84 RID: 3460
		private IntPtr m_Payload;
	}
}
