using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPlayerBehaviorReportOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000249 RID: 585
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0000AB45 File Offset: 0x00008D45
		public ProductUserId ReporterUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ReporterUserId, value);
			}
		}

		// Token: 0x1700024A RID: 586
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0000AB54 File Offset: 0x00008D54
		public ProductUserId ReportedUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ReportedUserId, value);
			}
		}

		// Token: 0x1700024B RID: 587
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0000AB63 File Offset: 0x00008D63
		public PlayerReportsCategory Category
		{
			set
			{
				this.m_Category = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public string Message
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Message, value);
			}
		}

		// Token: 0x1700024D RID: 589
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0000AB7B File Offset: 0x00008D7B
		public string Context
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Context, value);
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public void Set(SendPlayerBehaviorReportOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.ReporterUserId = other.ReporterUserId;
				this.ReportedUserId = other.ReportedUserId;
				this.Category = other.Category;
				this.Message = other.Message;
				this.Context = other.Context;
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000ABDF File Offset: 0x00008DDF
		public void Set(object other)
		{
			this.Set(other as SendPlayerBehaviorReportOptions);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000ABED File Offset: 0x00008DED
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ReporterUserId);
			Helper.TryMarshalDispose(ref this.m_ReportedUserId);
			Helper.TryMarshalDispose(ref this.m_Message);
			Helper.TryMarshalDispose(ref this.m_Context);
		}

		// Token: 0x040004A7 RID: 1191
		private int m_ApiVersion;

		// Token: 0x040004A8 RID: 1192
		private IntPtr m_ReporterUserId;

		// Token: 0x040004A9 RID: 1193
		private IntPtr m_ReportedUserId;

		// Token: 0x040004AA RID: 1194
		private PlayerReportsCategory m_Category;

		// Token: 0x040004AB RID: 1195
		private IntPtr m_Message;

		// Token: 0x040004AC RID: 1196
		private IntPtr m_Context;
	}
}
