using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x02000971 RID: 2417
	public static class MusicTrackCatalog
	{
		// Token: 0x060036D3 RID: 14035 RVA: 0x000E778B File Offset: 0x000E598B
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			MusicTrackCatalog.SetEntries(ContentManager.musicTrackDefs);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000E7798 File Offset: 0x000E5998
		private static void SetEntries(MusicTrackDef[] newMusicTrackDefs)
		{
			MusicTrackDef[] array = MusicTrackCatalog.musicTrackDefs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].catalogIndex = MusicTrackIndex.Invalid;
			}
			ArrayUtils.CloneTo<MusicTrackDef>(newMusicTrackDefs, ref MusicTrackCatalog.musicTrackDefs);
			Array.Sort<MusicTrackDef>(MusicTrackCatalog.musicTrackDefs, (MusicTrackDef a, MusicTrackDef b) => string.CompareOrdinal(a.cachedName, b.cachedName));
			for (int j = 0; j < MusicTrackCatalog.musicTrackDefs.Length; j++)
			{
				MusicTrackCatalog.musicTrackDefs[j].catalogIndex = (MusicTrackIndex)j;
			}
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000E7815 File Offset: 0x000E5A15
		public static MusicTrackDef GetMusicTrackDef(MusicTrackIndex musicTrackIndex)
		{
			return ArrayUtils.GetSafe<MusicTrackDef>(MusicTrackCatalog.musicTrackDefs, (int)musicTrackIndex);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000E7822 File Offset: 0x000E5A22
		public static MusicTrackDef FindMusicTrackDef(string name)
		{
			return MusicTrackCatalog.GetMusicTrackDef(MusicTrackCatalog.FindMusicTrackIndex(name));
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000E7830 File Offset: 0x000E5A30
		public static MusicTrackIndex FindMusicTrackIndex(string name)
		{
			for (int i = 0; i < MusicTrackCatalog.musicTrackDefs.Length; i++)
			{
				if (string.CompareOrdinal(MusicTrackCatalog.musicTrackDefs[i].cachedName, name) == 0)
				{
					return (MusicTrackIndex)i;
				}
			}
			return MusicTrackIndex.Invalid;
		}

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x060036D8 RID: 14040 RVA: 0x000E7866 File Offset: 0x000E5A66
		// (remove) Token: 0x060036D9 RID: 14041 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<MusicTrackDef>> collectEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<MusicTrackDef>("RoR2.MusicTrackCatalog.collectEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.musicTrackDefs);
			}
			remove
			{
			}
		}

		// Token: 0x0400373E RID: 14142
		private static MusicTrackDef[] musicTrackDefs = Array.Empty<MusicTrackDef>();
	}
}
