using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C8 RID: 712
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ModIdentifierInternal : ISettable, IDisposable
	{
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00013358 File Offset: 0x00011558
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x00013374 File Offset: 0x00011574
		public string NamespaceId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_NamespaceId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_NamespaceId, value);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00013384 File Offset: 0x00011584
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x000133A0 File Offset: 0x000115A0
		public string ItemId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ItemId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ItemId, value);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000133B0 File Offset: 0x000115B0
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000133CC File Offset: 0x000115CC
		public string ArtifactId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ArtifactId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ArtifactId, value);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x000133DC File Offset: 0x000115DC
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x000133F8 File Offset: 0x000115F8
		public string Title
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Title, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Title, value);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00013408 File Offset: 0x00011608
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x00013424 File Offset: 0x00011624
		public string Version
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Version, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Version, value);
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00013434 File Offset: 0x00011634
		public void Set(ModIdentifier other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.NamespaceId = other.NamespaceId;
				this.ItemId = other.ItemId;
				this.ArtifactId = other.ArtifactId;
				this.Title = other.Title;
				this.Version = other.Version;
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00013487 File Offset: 0x00011687
		public void Set(object other)
		{
			this.Set(other as ModIdentifier);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00013495 File Offset: 0x00011695
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_NamespaceId);
			Helper.TryMarshalDispose(ref this.m_ItemId);
			Helper.TryMarshalDispose(ref this.m_ArtifactId);
			Helper.TryMarshalDispose(ref this.m_Title);
			Helper.TryMarshalDispose(ref this.m_Version);
		}

		// Token: 0x04000884 RID: 2180
		private int m_ApiVersion;

		// Token: 0x04000885 RID: 2181
		private IntPtr m_NamespaceId;

		// Token: 0x04000886 RID: 2182
		private IntPtr m_ItemId;

		// Token: 0x04000887 RID: 2183
		private IntPtr m_ArtifactId;

		// Token: 0x04000888 RID: 2184
		private IntPtr m_Title;

		// Token: 0x04000889 RID: 2185
		private IntPtr m_Version;
	}
}
