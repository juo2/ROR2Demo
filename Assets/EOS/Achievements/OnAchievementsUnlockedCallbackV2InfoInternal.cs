using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000617 RID: 1559
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnAchievementsUnlockedCallbackV2InfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00028B1C File Offset: 0x00026D1C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00028B38 File Offset: 0x00026D38
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x00028B40 File Offset: 0x00026D40
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out result);
				return result;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x00028B5C File Offset: 0x00026D5C
		public string AchievementId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_AchievementId, out result);
				return result;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00028B78 File Offset: 0x00026D78
		public DateTimeOffset? UnlockTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_UnlockTime, out result);
				return result;
			}
		}

		// Token: 0x04001214 RID: 4628
		private IntPtr m_ClientData;

		// Token: 0x04001215 RID: 4629
		private IntPtr m_UserId;

		// Token: 0x04001216 RID: 4630
		private IntPtr m_AchievementId;

		// Token: 0x04001217 RID: 4631
		private long m_UnlockTime;
	}
}
