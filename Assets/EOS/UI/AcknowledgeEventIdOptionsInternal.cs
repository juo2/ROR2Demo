using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000041 RID: 65
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcknowledgeEventIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700006A RID: 106
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00004A7E File Offset: 0x00002C7E
		public ulong UiEventId
		{
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00004A87 File Offset: 0x00002C87
		public Result Result
		{
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00004A90 File Offset: 0x00002C90
		public void Set(AcknowledgeEventIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UiEventId = other.UiEventId;
				this.Result = other.Result;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public void Set(object other)
		{
			this.Set(other as AcknowledgeEventIdOptions);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400018C RID: 396
		private int m_ApiVersion;

		// Token: 0x0400018D RID: 397
		private ulong m_UiEventId;

		// Token: 0x0400018E RID: 398
		private Result m_Result;
	}
}
