using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A4 RID: 1444
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterEventOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000AFD RID: 2813
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x000251B0 File Offset: 0x000233B0
		public uint EventId
		{
			set
			{
				this.m_EventId = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x000251B9 File Offset: 0x000233B9
		public string EventName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EventName, value);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x000251C8 File Offset: 0x000233C8
		public AntiCheatCommonEventType EventType
		{
			set
			{
				this.m_EventType = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (set) Token: 0x0600231F RID: 8991 RVA: 0x000251D1 File Offset: 0x000233D1
		public RegisterEventParamDef[] ParamDefs
		{
			set
			{
				Helper.TryMarshalSet<RegisterEventParamDefInternal, RegisterEventParamDef>(ref this.m_ParamDefs, value, out this.m_ParamDefsCount);
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000251E6 File Offset: 0x000233E6
		public void Set(RegisterEventOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.EventId = other.EventId;
				this.EventName = other.EventName;
				this.EventType = other.EventType;
				this.ParamDefs = other.ParamDefs;
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00025222 File Offset: 0x00023422
		public void Set(object other)
		{
			this.Set(other as RegisterEventOptions);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x00025230 File Offset: 0x00023430
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_EventName);
			Helper.TryMarshalDispose(ref this.m_ParamDefs);
		}

		// Token: 0x04001090 RID: 4240
		private int m_ApiVersion;

		// Token: 0x04001091 RID: 4241
		private uint m_EventId;

		// Token: 0x04001092 RID: 4242
		private IntPtr m_EventName;

		// Token: 0x04001093 RID: 4243
		private AntiCheatCommonEventType m_EventType;

		// Token: 0x04001094 RID: 4244
		private uint m_ParamDefsCount;

		// Token: 0x04001095 RID: 4245
		private IntPtr m_ParamDefs;
	}
}
