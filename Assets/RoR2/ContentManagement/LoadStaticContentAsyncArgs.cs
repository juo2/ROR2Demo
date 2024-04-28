using System;
using HG;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E2D RID: 3629
	public class LoadStaticContentAsyncArgs
	{
		// Token: 0x06005350 RID: 21328 RVA: 0x00158E42 File Offset: 0x00157042
		public LoadStaticContentAsyncArgs(IProgress<float> progressReceiver, ReadOnlyArray<ContentPackLoadInfo> peerLoadInfos)
		{
			this.progressReceiver = progressReceiver;
			this.peerLoadInfos = peerLoadInfos;
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x00158E58 File Offset: 0x00157058
		public void ReportProgress(float progress)
		{
			this.progressReceiver.Report(progress);
		}

		// Token: 0x04004F74 RID: 20340
		private readonly IProgress<float> progressReceiver;

		// Token: 0x04004F75 RID: 20341
		public readonly ReadOnlyArray<ContentPackLoadInfo> peerLoadInfos;
	}
}
