using System;

namespace Facepunch.Steamworks
{
	// Token: 0x0200016F RID: 367
	public class Screenshots
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x0003743E File Offset: 0x0003563E
		internal Screenshots(Client c)
		{
			this.client = c;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0003744D File Offset: 0x0003564D
		public void Trigger()
		{
			this.client.native.screenshots.TriggerScreenshot();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00037464 File Offset: 0x00035664
		public unsafe void Write(byte[] rgbData, int width, int height)
		{
			if (rgbData == null)
			{
				throw new ArgumentNullException("rgbData");
			}
			if (width < 1)
			{
				throw new ArgumentOutOfRangeException("width", width, "Expected width to be at least 1.");
			}
			if (height < 1)
			{
				throw new ArgumentOutOfRangeException("height", height, "Expected height to be at least 1.");
			}
			int num = width * height * 3;
			if (rgbData.Length < num)
			{
				throw new ArgumentException("rgbData", string.Format("Expected {0} to contain at least {1} elements (actual size: {2}).", "rgbData", num, rgbData.Length));
			}
			fixed (byte[] array = rgbData)
			{
				byte* value;
				if (rgbData == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				this.client.native.screenshots.WriteScreenshot((IntPtr)((void*)value), (uint)rgbData.Length, width, height);
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00037522 File Offset: 0x00035722
		public void AddScreenshotToLibrary(string filename, string thumbnailFilename, int width, int height)
		{
			this.client.native.screenshots.AddScreenshotToLibrary(filename, thumbnailFilename, width, height);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0003753F File Offset: 0x0003573F
		public void AddScreenshotToLibrary(string filename, int width, int height)
		{
			this.client.native.screenshots.AddScreenshotToLibrary(filename, null, width, height);
		}

		// Token: 0x0400082E RID: 2094
		internal Client client;
	}
}
