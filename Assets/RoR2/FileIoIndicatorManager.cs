using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020005B6 RID: 1462
	public static class FileIoIndicatorManager
	{
		// Token: 0x06001A7B RID: 6779 RVA: 0x00071C24 File Offset: 0x0006FE24
		public static void IncrementActiveWriteCount()
		{
			Interlocked.Increment(ref FileIoIndicatorManager.activeWriteCount);
			FileIoIndicatorManager.saveIconAlpha = 2f;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00071C3D File Offset: 0x0006FE3D
		public static void DecrementActiveWriteCount()
		{
			Interlocked.Decrement(ref FileIoIndicatorManager.activeWriteCount);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00071C4A File Offset: 0x0006FE4A
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			Image saveImage = RoR2Application.instance.mainCanvas.transform.Find("SafeArea/SaveIcon").GetComponent<Image>();
			RoR2Application.onUpdate += delegate()
			{
				Color color = saveImage.color;
				if (FileIoIndicatorManager.activeWriteCount <= 0)
				{
					color.a = (FileIoIndicatorManager.saveIconAlpha = Mathf.Max(FileIoIndicatorManager.saveIconAlpha - 4f * Time.unscaledDeltaTime, 0f));
				}
				saveImage.color = color;
			};
		}

		// Token: 0x04002092 RID: 8338
		private static int activeWriteCount;

		// Token: 0x04002093 RID: 8339
		private static volatile float saveIconAlpha;
	}
}
