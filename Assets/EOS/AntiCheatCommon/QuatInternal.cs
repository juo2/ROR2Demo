using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A2 RID: 1442
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QuatInternal : ISettable, IDisposable
	{
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x000250E5 File Offset: 0x000232E5
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x000250ED File Offset: 0x000232ED
		public float w
		{
			get
			{
				return this.m_w;
			}
			set
			{
				this.m_w = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000250F6 File Offset: 0x000232F6
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x000250FE File Offset: 0x000232FE
		public float x
		{
			get
			{
				return this.m_x;
			}
			set
			{
				this.m_x = value;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x00025107 File Offset: 0x00023307
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x0002510F File Offset: 0x0002330F
		public float y
		{
			get
			{
				return this.m_y;
			}
			set
			{
				this.m_y = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x00025118 File Offset: 0x00023318
		// (set) Token: 0x0600230F RID: 8975 RVA: 0x00025120 File Offset: 0x00023320
		public float z
		{
			get
			{
				return this.m_z;
			}
			set
			{
				this.m_z = value;
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00025129 File Offset: 0x00023329
		public void Set(Quat other)
		{
			if (other != null)
			{
				this.w = other.w;
				this.x = other.x;
				this.y = other.y;
				this.z = other.z;
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0002515E File Offset: 0x0002335E
		public void Set(object other)
		{
			this.Set(other as Quat);
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04001088 RID: 4232
		private float m_w;

		// Token: 0x04001089 RID: 4233
		private float m_x;

		// Token: 0x0400108A RID: 4234
		private float m_y;

		// Token: 0x0400108B RID: 4235
		private float m_z;
	}
}
