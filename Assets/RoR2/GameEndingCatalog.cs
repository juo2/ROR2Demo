using System;
using System.Collections.Generic;
using System.Xml.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x02000538 RID: 1336
	public static class GameEndingCatalog
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0006A6FB File Offset: 0x000688FB
		public static int endingCount
		{
			get
			{
				return GameEndingCatalog.gameEndingDefs.Length;
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0006A704 File Offset: 0x00068904
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			HGXml.Register<GameEndingDef>(delegate(XElement element, GameEndingDef contents)
			{
				element.Value = (((contents != null) ? contents.cachedName : null) ?? "");
			}, delegate(XElement element, ref GameEndingDef contents)
			{
				contents = GameEndingCatalog.FindGameEndingDef(element.Value);
				return true;
			});
			GameEndingCatalog.SetGameEndingDefs(ContentManager.gameEndingDefs);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0006A760 File Offset: 0x00068960
		private static void SetGameEndingDefs(GameEndingDef[] newGameEndingDefs)
		{
			GameEndingDef[] array = GameEndingCatalog.gameEndingDefs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameEndingIndex = GameEndingIndex.Invalid;
			}
			ArrayUtils.CloneTo<GameEndingDef>(newGameEndingDefs, ref GameEndingCatalog.gameEndingDefs);
			Array.Sort<GameEndingDef>(GameEndingCatalog.gameEndingDefs, (GameEndingDef a, GameEndingDef b) => string.CompareOrdinal(a.cachedName, b.cachedName));
			for (int j = 0; j < GameEndingCatalog.gameEndingDefs.Length; j++)
			{
				GameEndingCatalog.gameEndingDefs[j].gameEndingIndex = (GameEndingIndex)j;
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0006A7DD File Offset: 0x000689DD
		public static GameEndingDef GetGameEndingDef(GameEndingIndex gameEndingIndex)
		{
			return ArrayUtils.GetSafe<GameEndingDef>(GameEndingCatalog.gameEndingDefs, (int)gameEndingIndex);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0006A7EC File Offset: 0x000689EC
		public static GameEndingIndex FindGameEndingIndex(string gameEndingName)
		{
			for (int i = 0; i < GameEndingCatalog.gameEndingDefs.Length; i++)
			{
				if (string.CompareOrdinal(gameEndingName, GameEndingCatalog.gameEndingDefs[i].cachedName) == 0)
				{
					return (GameEndingIndex)i;
				}
			}
			return GameEndingIndex.Invalid;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0006A822 File Offset: 0x00068A22
		public static GameEndingDef FindGameEndingDef(string gameEndingName)
		{
			return GameEndingCatalog.GetGameEndingDef(GameEndingCatalog.FindGameEndingIndex(gameEndingName));
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0006A82F File Offset: 0x00068A2F
		public static GameEndingIndex GetGameEndingIndex([CanBeNull] GameEndingDef gameEndingDef)
		{
			if (!gameEndingDef)
			{
				return GameEndingIndex.Invalid;
			}
			return gameEndingDef.gameEndingIndex;
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001858 RID: 6232 RVA: 0x0006A841 File Offset: 0x00068A41
		// (remove) Token: 0x06001859 RID: 6233 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<GameEndingDef>> collectEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<GameEndingDef>("RoR2.GameEndingCatalog.collectEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.gameEndingDefs);
			}
			remove
			{
			}
		}

		// Token: 0x04001DF9 RID: 7673
		private static GameEndingDef[] gameEndingDefs = Array.Empty<GameEndingDef>();
	}
}
