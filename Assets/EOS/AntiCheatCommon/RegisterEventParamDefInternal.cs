using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A6 RID: 1446
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterEventParamDefInternal : ISettable, IDisposable
	{
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000252C0 File Offset: 0x000234C0
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x000252DC File Offset: 0x000234DC
		public string ParamName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ParamName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ParamName, value);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000252EB File Offset: 0x000234EB
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x000252F3 File Offset: 0x000234F3
		public AntiCheatCommonEventParamType ParamType
		{
			get
			{
				return this.m_ParamType;
			}
			set
			{
				this.m_ParamType = value;
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000252FC File Offset: 0x000234FC
		public void Set(RegisterEventParamDef other)
		{
			if (other != null)
			{
				this.ParamName = other.ParamName;
				this.ParamType = other.ParamType;
			}
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00025319 File Offset: 0x00023519
		public void Set(object other)
		{
			this.Set(other as RegisterEventParamDef);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00025327 File Offset: 0x00023527
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ParamName);
		}

		// Token: 0x04001098 RID: 4248
		private IntPtr m_ParamName;

		// Token: 0x04001099 RID: 4249
		private AntiCheatCommonEventParamType m_ParamType;
	}
}
