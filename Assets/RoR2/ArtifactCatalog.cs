using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x020004CB RID: 1227
	public static class ArtifactCatalog
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000624E3 File Offset: 0x000606E3
		public static int artifactCount
		{
			get
			{
				return ArtifactCatalog.artifactDefs.Length;
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000624EC File Offset: 0x000606EC
		private static void RegisterArtifact(ArtifactIndex artifactIndex, ArtifactDef artifactDef)
		{
			ArtifactCatalog.artifactDefs[(int)artifactIndex] = artifactDef;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000624F6 File Offset: 0x000606F6
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ArtifactCatalog.SetArtifactDefs(ContentManager.artifactDefs);
			ArtifactCatalog.availability.MakeAvailable();
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0006250C File Offset: 0x0006070C
		private static void SetArtifactDefs([NotNull] ArtifactDef[] newArtifactDefs)
		{
			ArtifactCatalog.artifactDefs = ArrayUtils.Clone<ArtifactDef>(newArtifactDefs);
			Array.Sort<ArtifactDef>(ArtifactCatalog.artifactDefs, (ArtifactDef a, ArtifactDef b) => string.CompareOrdinal(a.cachedName, b.cachedName));
			for (int i = 0; i < ArtifactCatalog.artifactDefs.Length; i++)
			{
				ArtifactCatalog.artifactDefs[i].artifactIndex = (ArtifactIndex)i;
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0006256C File Offset: 0x0006076C
		public static ArtifactIndex FindArtifactIndex(string artifactName)
		{
			for (int i = 0; i < ArtifactCatalog.artifactDefs.Length; i++)
			{
				if (string.CompareOrdinal(artifactName, ArtifactCatalog.artifactDefs[i].cachedName) == 0)
				{
					return (ArtifactIndex)i;
				}
			}
			return ArtifactIndex.None;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000625A2 File Offset: 0x000607A2
		public static ArtifactDef FindArtifactDef(string artifactName)
		{
			return ArtifactCatalog.GetArtifactDef(ArtifactCatalog.FindArtifactIndex(artifactName));
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000625B0 File Offset: 0x000607B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ArtifactDef GetArtifactDef(ArtifactIndex artifactIndex)
		{
			ArtifactDef[] array = ArtifactCatalog.artifactDefs;
			ArtifactDef artifactDef = null;
			return ArrayUtils.GetSafe<ArtifactDef>(array, (int)artifactIndex, artifactDef);
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001643 RID: 5699 RVA: 0x000625CC File Offset: 0x000607CC
		// (remove) Token: 0x06001644 RID: 5700 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<ArtifactDef>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<ArtifactDef>("RoR2.ArtifactCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.artifactDefs);
			}
			remove
			{
			}
		}

		// Token: 0x04001C01 RID: 7169
		private static ArtifactDef[] artifactDefs = Array.Empty<ArtifactDef>();

		// Token: 0x04001C02 RID: 7170
		public static ResourceAvailability availability;
	}
}
