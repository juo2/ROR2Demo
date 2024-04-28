using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005AC RID: 1452
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct Vec3fInternal : ISettable, IDisposable
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x000254C0 File Offset: 0x000236C0
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x000254C8 File Offset: 0x000236C8
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

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000254D1 File Offset: 0x000236D1
		// (set) Token: 0x06002351 RID: 9041 RVA: 0x000254D9 File Offset: 0x000236D9
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

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000254E2 File Offset: 0x000236E2
		// (set) Token: 0x06002353 RID: 9043 RVA: 0x000254EA File Offset: 0x000236EA
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

		// Token: 0x06002354 RID: 9044 RVA: 0x000254F3 File Offset: 0x000236F3
		public void Set(Vec3f other)
		{
			if (other != null)
			{
				this.x = other.x;
				this.y = other.y;
				this.z = other.z;
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0002551C File Offset: 0x0002371C
		public void Set(object other)
		{
			this.Set(other as Vec3f);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010A7 RID: 4263
		private float m_x;

		// Token: 0x040010A8 RID: 4264
		private float m_y;

		// Token: 0x040010A9 RID: 4265
		private float m_z;
	}
}
