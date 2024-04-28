using System;
using HG;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E2E RID: 3630
	public class GetContentPackAsyncArgs
	{
		// Token: 0x06005352 RID: 21330 RVA: 0x00158E66 File Offset: 0x00157066
		public GetContentPackAsyncArgs(IProgress<float> progressReceiver, ContentPack output, ReadOnlyArray<ContentPackLoadInfo> peerLoadInfos, int retriesRemaining)
		{
			this.progressReceiver = progressReceiver;
			this.output = output;
			this.peerLoadInfos = peerLoadInfos;
			this.retriesRemaining = retriesRemaining;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x00158E8B File Offset: 0x0015708B
		public void ReportProgress(float progress)
		{
			this.progressReceiver.Report(progress);
		}

		// Token: 0x04004F76 RID: 20342
		private readonly IProgress<float> progressReceiver;

		// Token: 0x04004F77 RID: 20343
		public readonly ReadOnlyArray<ContentPackLoadInfo> peerLoadInfos;

		// Token: 0x04004F78 RID: 20344
		public readonly ContentPack output;

		// Token: 0x04004F79 RID: 20345
		public readonly int retriesRemaining;
	}
}
