using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x020002EA RID: 746
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogMessageInternal
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00014190 File Offset: 0x00012390
		public string Category
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Category, out result);
				return result;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000141AC File Offset: 0x000123AC
		public string Message
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Message, out result);
				return result;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000141C8 File Offset: 0x000123C8
		public LogLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		// Token: 0x04000904 RID: 2308
		private IntPtr m_Category;

		// Token: 0x04000905 RID: 2309
		private IntPtr m_Message;

		// Token: 0x04000906 RID: 2310
		private LogLevel m_Level;
	}
}
