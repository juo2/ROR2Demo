using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200046C RID: 1132
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KeyImageInfoInternal : ISettable, IDisposable
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0001D658 File Offset: 0x0001B858
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x0001D674 File Offset: 0x0001B874
		public string Type
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Type, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Type, value);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0001D684 File Offset: 0x0001B884
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		public string Url
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Url, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Url, value);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0001D6AF File Offset: 0x0001B8AF
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		public uint Width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0001D6C0 File Offset: 0x0001B8C0
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
		public uint Height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0001D6D1 File Offset: 0x0001B8D1
		public void Set(KeyImageInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Type = other.Type;
				this.Url = other.Url;
				this.Width = other.Width;
				this.Height = other.Height;
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0001D70D File Offset: 0x0001B90D
		public void Set(object other)
		{
			this.Set(other as KeyImageInfo);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0001D71B File Offset: 0x0001B91B
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Type);
			Helper.TryMarshalDispose(ref this.m_Url);
		}

		// Token: 0x04000CF8 RID: 3320
		private int m_ApiVersion;

		// Token: 0x04000CF9 RID: 3321
		private IntPtr m_Type;

		// Token: 0x04000CFA RID: 3322
		private IntPtr m_Url;

		// Token: 0x04000CFB RID: 3323
		private uint m_Width;

		// Token: 0x04000CFC RID: 3324
		private uint m_Height;
	}
}
