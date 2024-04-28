using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200062D RID: 1581
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StatThresholdsInternal : ISettable, IDisposable
	{
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000294E4 File Offset: 0x000276E4
		// (set) Token: 0x060026CA RID: 9930 RVA: 0x00029500 File Offset: 0x00027700
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

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x0002950F File Offset: 0x0002770F
		// (set) Token: 0x060026CC RID: 9932 RVA: 0x00029517 File Offset: 0x00027717
		public int Threshold
		{
			get
			{
				return this.m_Threshold;
			}
			set
			{
				this.m_Threshold = value;
			}
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00029520 File Offset: 0x00027720
		public void Set(StatThresholds other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Name;
				this.Threshold = other.Threshold;
			}
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00029544 File Offset: 0x00027744
		public void Set(object other)
		{
			this.Set(other as StatThresholds);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00029552 File Offset: 0x00027752
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Name);
		}

		// Token: 0x04001252 RID: 4690
		private int m_ApiVersion;

		// Token: 0x04001253 RID: 4691
		private IntPtr m_Name;

		// Token: 0x04001254 RID: 4692
		private int m_Threshold;
	}
}
