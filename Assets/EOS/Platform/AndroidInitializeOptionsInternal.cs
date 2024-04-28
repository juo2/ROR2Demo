using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005D9 RID: 1497
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AndroidInitializeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B44 RID: 2884
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x000260C1 File Offset: 0x000242C1
		public IntPtr AllocateMemoryFunction
		{
			set
			{
				this.m_AllocateMemoryFunction = value;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x000260CA File Offset: 0x000242CA
		public IntPtr ReallocateMemoryFunction
		{
			set
			{
				this.m_ReallocateMemoryFunction = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x000260D3 File Offset: 0x000242D3
		public IntPtr ReleaseMemoryFunction
		{
			set
			{
				this.m_ReleaseMemoryFunction = value;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (set) Token: 0x06002421 RID: 9249 RVA: 0x000260DC File Offset: 0x000242DC
		public string ProductName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductName, value);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x000260EB File Offset: 0x000242EB
		public string ProductVersion
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductVersion, value);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x000260FA File Offset: 0x000242FA
		public IntPtr Reserved
		{
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x00026103 File Offset: 0x00024303
		public AndroidInitializeOptionsSystemInitializeOptions SystemInitializeOptions
		{
			set
			{
				Helper.TryMarshalSet<AndroidInitializeOptionsSystemInitializeOptionsInternal, AndroidInitializeOptionsSystemInitializeOptions>(ref this.m_SystemInitializeOptions, value);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (set) Token: 0x06002425 RID: 9253 RVA: 0x00026112 File Offset: 0x00024312
		public InitializeThreadAffinity OverrideThreadAffinity
		{
			set
			{
				Helper.TryMarshalSet<InitializeThreadAffinityInternal, InitializeThreadAffinity>(ref this.m_OverrideThreadAffinity, value);
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00026124 File Offset: 0x00024324
		public void Set(AndroidInitializeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 4;
				this.AllocateMemoryFunction = other.AllocateMemoryFunction;
				this.ReallocateMemoryFunction = other.ReallocateMemoryFunction;
				this.ReleaseMemoryFunction = other.ReleaseMemoryFunction;
				this.ProductName = other.ProductName;
				this.ProductVersion = other.ProductVersion;
				this.Reserved = other.Reserved;
				this.SystemInitializeOptions = other.SystemInitializeOptions;
				this.OverrideThreadAffinity = other.OverrideThreadAffinity;
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0002619B File Offset: 0x0002439B
		public void Set(object other)
		{
			this.Set(other as AndroidInitializeOptions);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000261AC File Offset: 0x000243AC
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

		// Token: 0x04001113 RID: 4371
		private int m_ApiVersion;

		// Token: 0x04001114 RID: 4372
		private IntPtr m_AllocateMemoryFunction;

		// Token: 0x04001115 RID: 4373
		private IntPtr m_ReallocateMemoryFunction;

		// Token: 0x04001116 RID: 4374
		private IntPtr m_ReleaseMemoryFunction;

		// Token: 0x04001117 RID: 4375
		private IntPtr m_ProductName;

		// Token: 0x04001118 RID: 4376
		private IntPtr m_ProductVersion;

		// Token: 0x04001119 RID: 4377
		private IntPtr m_Reserved;

		// Token: 0x0400111A RID: 4378
		private IntPtr m_SystemInitializeOptions;

		// Token: 0x0400111B RID: 4379
		private IntPtr m_OverrideThreadAffinity;
	}
}
