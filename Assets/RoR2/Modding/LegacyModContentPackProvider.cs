using System;
using System.Collections;
using System.Collections.Generic;
using RoR2.ContentManagement;
using UnityEngine;

namespace RoR2.Modding
{
	// Token: 0x02000B5D RID: 2909
	public class LegacyModContentPackProvider : IContentPackProvider
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x001124C5 File Offset: 0x001106C5
		// (set) Token: 0x0600421C RID: 16924 RVA: 0x001124CC File Offset: 0x001106CC
		public static LegacyModContentPackProvider instance { get; private set; } = new LegacyModContentPackProvider();

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x001124D4 File Offset: 0x001106D4
		// (set) Token: 0x0600421E RID: 16926 RVA: 0x001124DC File Offset: 0x001106DC
		public ContentPack registrationContentPack { get; private set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x001124E5 File Offset: 0x001106E5
		// (set) Token: 0x06004220 RID: 16928 RVA: 0x001124ED File Offset: 0x001106ED
		public bool cutoffReached { get; private set; }

		// Token: 0x06004221 RID: 16929 RVA: 0x001124F6 File Offset: 0x001106F6
		private LegacyModContentPackProvider()
		{
			this.registrationContentPack = new ContentPack();
			this.finalizedContentPack = new ContentPack();
			ContentManager.collectContentPackProviders += delegate(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
			{
				addContentPackProvider(this);
			};
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x00112525 File Offset: 0x00110725
		public string identifier
		{
			get
			{
				return "RoR2.LegacyModContent";
			}
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0011252C File Offset: 0x0011072C
		public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
		{
			ContentPack.Copy(this.registrationContentPack, this.finalizedContentPack);
			yield break;
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x0011253B File Offset: 0x0011073B
		public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
		{
			ContentPack.Copy(this.finalizedContentPack, args.output);
			yield break;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x00112551 File Offset: 0x00110751
		public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
		{
			ContentPack.Copy(new ContentPack(), this.registrationContentPack);
			this.finalizedContentPack = null;
			yield break;
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x00112560 File Offset: 0x00110760
		public void HandleLegacyGetAdditionalEntries<TAsset>(string eventName, Action<List<TAsset>> callback, NamedAssetCollection<TAsset> dest)
		{
			if (this.cutoffReached)
			{
				throw new InvalidOperationException("Legacy mod ContentPack has been finalized. It is too late to add additional entries via " + eventName + ".");
			}
			List<TAsset> list = new List<TAsset>();
			try
			{
				if (callback != null)
				{
					callback(list);
				}
				dest.Add(list.ToArray());
				Debug.LogWarning("Added content to legacy mod ContentPack via " + eventName + " succeeded. Do not use this code path; it will be removed in a future update. Use IContentPackProvider instead.");
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Adding content to legacy mod ContentPack via {0} failed: {1}.", eventName, arg));
			}
		}

		// Token: 0x04004045 RID: 16453
		private ContentPack finalizedContentPack;
	}
}
