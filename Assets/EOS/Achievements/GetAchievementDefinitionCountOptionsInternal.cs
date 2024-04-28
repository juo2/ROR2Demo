using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200060B RID: 1547
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAchievementDefinitionCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060025F1 RID: 9713 RVA: 0x00028865 File Offset: 0x00026A65
		public void Set(GetAchievementDefinitionCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00028871 File Offset: 0x00026A71
		public void Set(object other)
		{
			this.Set(other as GetAchievementDefinitionCountOptions);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04001202 RID: 4610
		private int m_ApiVersion;
	}
}
