using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000212 RID: 530
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InfoInternal : ISettable, IDisposable
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public Status Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public EpicAccountId UserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0000EE08 File Offset: 0x0000D008
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0000EE24 File Offset: 0x0000D024
		public string ProductId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ProductId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductId, value);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0000EE34 File Offset: 0x0000D034
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0000EE50 File Offset: 0x0000D050
		public string ProductVersion
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ProductVersion, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductVersion, value);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0000EE60 File Offset: 0x0000D060
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public string Platform
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Platform, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Platform, value);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0000EE8C File Offset: 0x0000D08C
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public string RichText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RichText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_RichText, value);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0000EEDA File Offset: 0x0000D0DA
		public DataRecord[] Records
		{
			get
			{
				DataRecord[] result;
				Helper.TryMarshalGet<DataRecordInternal, DataRecord>(this.m_Records, out result, this.m_RecordsCount);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<DataRecordInternal, DataRecord>(ref this.m_Records, value, out this.m_RecordsCount);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public string ProductName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ProductName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductName, value);
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public void Set(Info other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.Status = other.Status;
				this.UserId = other.UserId;
				this.ProductId = other.ProductId;
				this.ProductVersion = other.ProductVersion;
				this.Platform = other.Platform;
				this.RichText = other.RichText;
				this.Records = other.Records;
				this.ProductName = other.ProductName;
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0000EF93 File Offset: 0x0000D193
		public void Set(object other)
		{
			this.Set(other as Info);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
			Helper.TryMarshalDispose(ref this.m_ProductId);
			Helper.TryMarshalDispose(ref this.m_ProductVersion);
			Helper.TryMarshalDispose(ref this.m_Platform);
			Helper.TryMarshalDispose(ref this.m_RichText);
			Helper.TryMarshalDispose(ref this.m_Records);
			Helper.TryMarshalDispose(ref this.m_ProductName);
		}

		// Token: 0x04000680 RID: 1664
		private int m_ApiVersion;

		// Token: 0x04000681 RID: 1665
		private Status m_Status;

		// Token: 0x04000682 RID: 1666
		private IntPtr m_UserId;

		// Token: 0x04000683 RID: 1667
		private IntPtr m_ProductId;

		// Token: 0x04000684 RID: 1668
		private IntPtr m_ProductVersion;

		// Token: 0x04000685 RID: 1669
		private IntPtr m_Platform;

		// Token: 0x04000686 RID: 1670
		private IntPtr m_RichText;

		// Token: 0x04000687 RID: 1671
		private int m_RecordsCount;

		// Token: 0x04000688 RID: 1672
		private IntPtr m_Records;

		// Token: 0x04000689 RID: 1673
		private IntPtr m_ProductName;
	}
}
