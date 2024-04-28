using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000613 RID: 1555
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnAchievementsUnlockedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000289DC File Offset: 0x00026BDC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000289F8 File Offset: 0x00026BF8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x00028A00 File Offset: 0x00026C00
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out result);
				return result;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x00028A1C File Offset: 0x00026C1C
		public string[] AchievementIds
		{
			get
			{
				string[] result;
				Helper.TryMarshalGet<string>(this.m_AchievementIds, out result, this.m_AchievementsCount, true);
				return result;
			}
		}

		// Token: 0x0400120C RID: 4620
		private IntPtr m_ClientData;

		// Token: 0x0400120D RID: 4621
		private IntPtr m_UserId;

		// Token: 0x0400120E RID: 4622
		private uint m_AchievementsCount;

		// Token: 0x0400120F RID: 4623
		private IntPtr m_AchievementIds;
	}
}
