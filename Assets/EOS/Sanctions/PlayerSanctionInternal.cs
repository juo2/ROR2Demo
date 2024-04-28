using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000156 RID: 342
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerSanctionInternal : ISettable, IDisposable
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0000A641 File Offset: 0x00008841
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0000A649 File Offset: 0x00008849
		public long TimePlaced
		{
			get
			{
				return this.m_TimePlaced;
			}
			set
			{
				this.m_TimePlaced = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0000A654 File Offset: 0x00008854
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0000A670 File Offset: 0x00008870
		public string Action
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Action, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Action, value);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0000A67F File Offset: 0x0000887F
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0000A687 File Offset: 0x00008887
		public long TimeExpires
		{
			get
			{
				return this.m_TimeExpires;
			}
			set
			{
				this.m_TimeExpires = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0000A690 File Offset: 0x00008890
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0000A6AC File Offset: 0x000088AC
		public string ReferenceId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ReferenceId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ReferenceId, value);
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0000A6BB File Offset: 0x000088BB
		public void Set(PlayerSanction other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.TimePlaced = other.TimePlaced;
				this.Action = other.Action;
				this.TimeExpires = other.TimeExpires;
				this.ReferenceId = other.ReferenceId;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000A6F7 File Offset: 0x000088F7
		public void Set(object other)
		{
			this.Set(other as PlayerSanction);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000A705 File Offset: 0x00008905
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Action);
			Helper.TryMarshalDispose(ref this.m_ReferenceId);
		}

		// Token: 0x0400047C RID: 1148
		private int m_ApiVersion;

		// Token: 0x0400047D RID: 1149
		private long m_TimePlaced;

		// Token: 0x0400047E RID: 1150
		private IntPtr m_Action;

		// Token: 0x0400047F RID: 1151
		private long m_TimeExpires;

		// Token: 0x04000480 RID: 1152
		private IntPtr m_ReferenceId;
	}
}
