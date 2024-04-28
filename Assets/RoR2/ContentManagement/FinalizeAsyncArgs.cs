using System;
using HG;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E2F RID: 3631
	public class FinalizeAsyncArgs
	{
		// Token: 0x06005354 RID: 21332 RVA: 0x00158E99 File Offset: 0x00157099
		public FinalizeAsyncArgs(IProgress<float> progressReceiver, ReadOnlyArray<ContentPackLoadInfo> peerLoadInfos, ReadOnlyContentPack finalContentPack)
		{
			this.progressReceiver = progressReceiver;
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x00158EA8 File Offset: 0x001570A8
		public void ReportProgress(float progress)
		{
			this.progressReceiver.Report(progress);
		}

		// Token: 0x04004F7A RID: 20346
		private readonly IProgress<float> progressReceiver;

		// Token: 0x04004F7B RID: 20347
		public readonly ReadOnlyArray<ContentPackLoadInfo> peerLoadInfos;

		// Token: 0x04004F7C RID: 20348
		public readonly ReadOnlyContentPack finalContentPack;
	}
}
