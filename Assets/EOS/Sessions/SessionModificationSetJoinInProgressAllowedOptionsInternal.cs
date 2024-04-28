using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000126 RID: 294
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetJoinInProgressAllowedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001E9 RID: 489
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0000917D File Offset: 0x0000737D
		public bool AllowJoinInProgress
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowJoinInProgress, value);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000918C File Offset: 0x0000738C
		public void Set(SessionModificationSetJoinInProgressAllowedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AllowJoinInProgress = other.AllowJoinInProgress;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000091A4 File Offset: 0x000073A4
		public void Set(object other)
		{
			this.Set(other as SessionModificationSetJoinInProgressAllowedOptions);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040003FE RID: 1022
		private int m_ApiVersion;

		// Token: 0x040003FF RID: 1023
		private int m_AllowJoinInProgress;
	}
}
