using System;

namespace RoR2
{
	// Token: 0x020009E3 RID: 2531
	public abstract class TextDataManager
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06003A5F RID: 14943
		public abstract bool InitializedConfigFiles { get; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06003A60 RID: 14944
		public abstract bool InitializedLocFiles { get; }

		// Token: 0x06003A61 RID: 14945
		public abstract string GetConfFile(string fileName, string path);

		// Token: 0x06003A62 RID: 14946
		public abstract void GetLocFiles(string folderPath, Action<string[]> callback);
	}
}
