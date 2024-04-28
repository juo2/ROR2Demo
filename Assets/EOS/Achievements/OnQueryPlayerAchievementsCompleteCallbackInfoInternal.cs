using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200061F RID: 1567
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryPlayerAchievementsCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00028CEC File Offset: 0x00026EEC
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x00028CF4 File Offset: 0x00026EF4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00028D10 File Offset: 0x00026F10
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00028D18 File Offset: 0x00026F18
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out result);
				return result;
			}
		}

		// Token: 0x0400121F RID: 4639
		private Result m_ResultCode;

		// Token: 0x04001220 RID: 4640
		private IntPtr m_ClientData;

		// Token: 0x04001221 RID: 4641
		private IntPtr m_UserId;
	}
}
