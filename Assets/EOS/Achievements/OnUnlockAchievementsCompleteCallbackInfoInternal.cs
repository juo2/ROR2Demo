using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000623 RID: 1571
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnUnlockAchievementsCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x00028E05 File Offset: 0x00027005
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x00028E10 File Offset: 0x00027010
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x00028E2C File Offset: 0x0002702C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x00028E34 File Offset: 0x00027034
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out result);
				return result;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x00028E50 File Offset: 0x00027050
		public uint AchievementsCount
		{
			get
			{
				return this.m_AchievementsCount;
			}
		}

		// Token: 0x04001226 RID: 4646
		private Result m_ResultCode;

		// Token: 0x04001227 RID: 4647
		private IntPtr m_ClientData;

		// Token: 0x04001228 RID: 4648
		private IntPtr m_UserId;

		// Token: 0x04001229 RID: 4649
		private uint m_AchievementsCount;
	}
}
