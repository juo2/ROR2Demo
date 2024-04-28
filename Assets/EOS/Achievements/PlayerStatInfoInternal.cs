using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000627 RID: 1575
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerStatInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0002927C File Offset: 0x0002747C
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x00029298 File Offset: 0x00027498
		public string Name
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Name, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Name, value);
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000292A7 File Offset: 0x000274A7
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x000292AF File Offset: 0x000274AF
		public int CurrentValue
		{
			get
			{
				return this.m_CurrentValue;
			}
			set
			{
				this.m_CurrentValue = value;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000292B8 File Offset: 0x000274B8
		// (set) Token: 0x060026A7 RID: 9895 RVA: 0x000292C0 File Offset: 0x000274C0
		public int ThresholdValue
		{
			get
			{
				return this.m_ThresholdValue;
			}
			set
			{
				this.m_ThresholdValue = value;
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000292C9 File Offset: 0x000274C9
		public void Set(PlayerStatInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Name;
				this.CurrentValue = other.CurrentValue;
				this.ThresholdValue = other.ThresholdValue;
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000292F9 File Offset: 0x000274F9
		public void Set(object other)
		{
			this.Set(other as PlayerStatInfo);
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00029307 File Offset: 0x00027507
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Name);
		}

		// Token: 0x0400123F RID: 4671
		private int m_ApiVersion;

		// Token: 0x04001240 RID: 4672
		private IntPtr m_Name;

		// Token: 0x04001241 RID: 4673
		private int m_CurrentValue;

		// Token: 0x04001242 RID: 4674
		private int m_ThresholdValue;
	}
}
