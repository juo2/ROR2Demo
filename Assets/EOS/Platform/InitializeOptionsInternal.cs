using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E1 RID: 1505
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InitializeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B5D RID: 2909
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x00026B1B File Offset: 0x00024D1B
		public IntPtr AllocateMemoryFunction
		{
			set
			{
				this.m_AllocateMemoryFunction = value;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x00026B24 File Offset: 0x00024D24
		public IntPtr ReallocateMemoryFunction
		{
			set
			{
				this.m_ReallocateMemoryFunction = value;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x00026B2D File Offset: 0x00024D2D
		public IntPtr ReleaseMemoryFunction
		{
			set
			{
				this.m_ReleaseMemoryFunction = value;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x00026B36 File Offset: 0x00024D36
		public string ProductName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductName, value);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x00026B45 File Offset: 0x00024D45
		public string ProductVersion
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductVersion, value);
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x00026B54 File Offset: 0x00024D54
		public IntPtr SystemInitializeOptions
		{
			set
			{
				this.m_SystemInitializeOptions = value;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x00026B5D File Offset: 0x00024D5D
		public InitializeThreadAffinity OverrideThreadAffinity
		{
			set
			{
				Helper.TryMarshalSet<InitializeThreadAffinityInternal, InitializeThreadAffinity>(ref this.m_OverrideThreadAffinity, value);
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00026B6C File Offset: 0x00024D6C
		public void Set(InitializeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 4;
				this.AllocateMemoryFunction = other.AllocateMemoryFunction;
				this.ReallocateMemoryFunction = other.ReallocateMemoryFunction;
				this.ReleaseMemoryFunction = other.ReleaseMemoryFunction;
				this.ProductName = other.ProductName;
				this.ProductVersion = other.ProductVersion;
				int[] source = new int[]
				{
					1,
					1
				};
				IntPtr zero = IntPtr.Zero;
				Helper.TryMarshalSet<int>(ref zero, source);
				this.m_Reserved = zero;
				this.SystemInitializeOptions = other.SystemInitializeOptions;
				this.OverrideThreadAffinity = other.OverrideThreadAffinity;
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00026BFF File Offset: 0x00024DFF
		public void Set(object other)
		{
			this.Set(other as InitializeOptions);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00026C10 File Offset: 0x00024E10
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AllocateMemoryFunction);
			Helper.TryMarshalDispose(ref this.m_ReallocateMemoryFunction);
			Helper.TryMarshalDispose(ref this.m_ReleaseMemoryFunction);
			Helper.TryMarshalDispose(ref this.m_ProductName);
			Helper.TryMarshalDispose(ref this.m_ProductVersion);
			Helper.TryMarshalDispose(ref this.m_Reserved);
			Helper.TryMarshalDispose(ref this.m_SystemInitializeOptions);
			Helper.TryMarshalDispose(ref this.m_OverrideThreadAffinity);
		}

		// Token: 0x04001138 RID: 4408
		private int m_ApiVersion;

		// Token: 0x04001139 RID: 4409
		private IntPtr m_AllocateMemoryFunction;

		// Token: 0x0400113A RID: 4410
		private IntPtr m_ReallocateMemoryFunction;

		// Token: 0x0400113B RID: 4411
		private IntPtr m_ReleaseMemoryFunction;

		// Token: 0x0400113C RID: 4412
		private IntPtr m_ProductName;

		// Token: 0x0400113D RID: 4413
		private IntPtr m_ProductVersion;

		// Token: 0x0400113E RID: 4414
		private IntPtr m_Reserved;

		// Token: 0x0400113F RID: 4415
		private IntPtr m_SystemInitializeOptions;

		// Token: 0x04001140 RID: 4416
		private IntPtr m_OverrideThreadAffinity;
	}
}
