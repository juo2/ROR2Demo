using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005DB RID: 1499
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AndroidInitializeOptionsSystemInitializeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x000262B4 File Offset: 0x000244B4
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x000262BC File Offset: 0x000244BC
		public IntPtr Reserved
		{
			get
			{
				return this.m_Reserved;
			}
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x000262C8 File Offset: 0x000244C8
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x000262E4 File Offset: 0x000244E4
		public string OptionalInternalDirectory
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_OptionalInternalDirectory, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_OptionalInternalDirectory, value);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x000262F4 File Offset: 0x000244F4
		// (set) Token: 0x06002437 RID: 9271 RVA: 0x00026310 File Offset: 0x00024510
		public string OptionalExternalDirectory
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_OptionalExternalDirectory, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_OptionalExternalDirectory, value);
			}
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0002631F File Offset: 0x0002451F
		public void Set(AndroidInitializeOptionsSystemInitializeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.Reserved = other.Reserved;
				this.OptionalInternalDirectory = other.OptionalInternalDirectory;
				this.OptionalExternalDirectory = other.OptionalExternalDirectory;
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0002634F File Offset: 0x0002454F
		public void Set(object other)
		{
			this.Set(other as AndroidInitializeOptionsSystemInitializeOptions);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0002635D File Offset: 0x0002455D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Reserved);
			Helper.TryMarshalDispose(ref this.m_OptionalInternalDirectory);
			Helper.TryMarshalDispose(ref this.m_OptionalExternalDirectory);
		}

		// Token: 0x0400111F RID: 4383
		private int m_ApiVersion;

		// Token: 0x04001120 RID: 4384
		private IntPtr m_Reserved;

		// Token: 0x04001121 RID: 4385
		private IntPtr m_OptionalInternalDirectory;

		// Token: 0x04001122 RID: 4386
		private IntPtr m_OptionalExternalDirectory;
	}
}
