using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200041E RID: 1054
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnFriendsUpdateInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0001AB80 File Offset: 0x00018D80
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public FriendsStatus PreviousStatus
		{
			get
			{
				return this.m_PreviousStatus;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		public FriendsStatus CurrentStatus
		{
			get
			{
				return this.m_CurrentStatus;
			}
		}

		// Token: 0x04000BCC RID: 3020
		private IntPtr m_ClientData;

		// Token: 0x04000BCD RID: 3021
		private IntPtr m_LocalUserId;

		// Token: 0x04000BCE RID: 3022
		private IntPtr m_TargetUserId;

		// Token: 0x04000BCF RID: 3023
		private FriendsStatus m_PreviousStatus;

		// Token: 0x04000BD0 RID: 3024
		private FriendsStatus m_CurrentStatus;
	}
}
