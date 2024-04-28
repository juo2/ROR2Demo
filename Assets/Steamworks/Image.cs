using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000165 RID: 357
	public class Image
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00034F80 File Offset: 0x00033180
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x00034F88 File Offset: 0x00033188
		public int Id { get; internal set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00034F91 File Offset: 0x00033191
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x00034F99 File Offset: 0x00033199
		public int Width { get; internal set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00034FA2 File Offset: 0x000331A2
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x00034FAA File Offset: 0x000331AA
		public int Height { get; internal set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00034FB3 File Offset: 0x000331B3
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00034FBB File Offset: 0x000331BB
		public byte[] Data { get; internal set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00034FC4 File Offset: 0x000331C4
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00034FCC File Offset: 0x000331CC
		public bool IsLoaded { get; internal set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00034FD5 File Offset: 0x000331D5
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00034FDD File Offset: 0x000331DD
		public bool IsError { get; internal set; }

		// Token: 0x06000A98 RID: 2712 RVA: 0x00034FE8 File Offset: 0x000331E8
		internal unsafe bool TryLoad(SteamUtils utils)
		{
			if (this.IsLoaded)
			{
				return true;
			}
			uint num = 0U;
			uint num2 = 0U;
			if (!utils.GetImageSize(this.Id, out num, out num2))
			{
				this.IsError = true;
				return false;
			}
			byte[] array = new byte[num * num2 * 4U];
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			if (!utils.GetImageRGBA(this.Id, (IntPtr)((void*)value), array.Length))
			{
				this.IsError = true;
				return false;
			}
			array2 = null;
			this.Width = (int)num;
			this.Height = (int)num2;
			this.Data = array;
			this.IsLoaded = true;
			this.IsError = false;
			return true;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0003508C File Offset: 0x0003328C
		public Color GetPixel(int x, int y)
		{
			if (!this.IsLoaded)
			{
				throw new Exception("Image not loaded");
			}
			if (x < 0 || x >= this.Width)
			{
				throw new Exception("x out of bounds");
			}
			if (y < 0 || y >= this.Height)
			{
				throw new Exception("y out of bounds");
			}
			Color result = default(Color);
			int num = (y * this.Width + x) * 4;
			result.r = this.Data[num];
			result.g = this.Data[num + 1];
			result.b = this.Data[num + 2];
			result.a = this.Data[num + 3];
			return result;
		}
	}
}
