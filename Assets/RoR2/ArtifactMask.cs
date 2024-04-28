using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004CA RID: 1226
	[Serializable]
	public struct ArtifactMask
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x000623F4 File Offset: 0x000605F4
		public bool HasArtifact(ArtifactIndex artifactIndex)
		{
			return artifactIndex >= (ArtifactIndex)0 && artifactIndex < (ArtifactIndex)ArtifactCatalog.artifactCount && ((int)this.a & 1 << (int)artifactIndex) != 0;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00062414 File Offset: 0x00060614
		public void AddArtifact(ArtifactIndex artifactIndex)
		{
			if (artifactIndex < (ArtifactIndex)0 || artifactIndex >= (ArtifactIndex)ArtifactCatalog.artifactCount)
			{
				return;
			}
			this.a |= (ushort)(1 << (int)artifactIndex);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00062438 File Offset: 0x00060638
		public void ToggleArtifact(ArtifactIndex artifactIndex)
		{
			if (artifactIndex < (ArtifactIndex)0 || artifactIndex >= (ArtifactIndex)ArtifactCatalog.artifactCount)
			{
				return;
			}
			this.a ^= (ushort)(1 << (int)artifactIndex);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0006245C File Offset: 0x0006065C
		public void RemoveArtifact(ArtifactIndex artifactIndex)
		{
			if (artifactIndex < (ArtifactIndex)0 || artifactIndex >= (ArtifactIndex)ArtifactCatalog.artifactCount)
			{
				return;
			}
			this.a &= (ushort)(~(ushort)(1 << (int)artifactIndex));
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00062484 File Offset: 0x00060684
		public static ArtifactMask operator &(ArtifactMask mask1, ArtifactMask mask2)
		{
			return new ArtifactMask
			{
				a = (mask1.a & mask2.a)
			};
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000624B0 File Offset: 0x000606B0
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			ArtifactMask.all = default(ArtifactMask);
			for (ArtifactIndex artifactIndex = (ArtifactIndex)0; artifactIndex < (ArtifactIndex)ArtifactCatalog.artifactCount; artifactIndex++)
			{
				ArtifactMask.all.AddArtifact(artifactIndex);
			}
		}

		// Token: 0x04001BFE RID: 7166
		[SerializeField]
		public ushort a;

		// Token: 0x04001BFF RID: 7167
		public static readonly ArtifactMask none;

		// Token: 0x04001C00 RID: 7168
		public static ArtifactMask all;
	}
}
