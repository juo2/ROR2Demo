using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Modding;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A7C RID: 2684
	public static class SurvivorCatalog
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06003DAE RID: 15790 RVA: 0x000FEC86 File Offset: 0x000FCE86
		public static int survivorCount
		{
			get
			{
				return SurvivorCatalog.survivorDefs.Length;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x000FEC86 File Offset: 0x000FCE86
		public static SurvivorIndex endIndex
		{
			get
			{
				return (SurvivorIndex)SurvivorCatalog.survivorDefs.Length;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06003DB0 RID: 15792 RVA: 0x000FEC8F File Offset: 0x000FCE8F
		public static IEnumerable<SurvivorDef> allSurvivorDefs
		{
			get
			{
				return SurvivorCatalog.survivorDefs;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x000FEC96 File Offset: 0x000FCE96
		public static IEnumerable<SurvivorDef> orderedSurvivorDefs
		{
			get
			{
				return SurvivorCatalog._orderedSurvivorDefs;
			}
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000FECA0 File Offset: 0x000FCEA0
		private static void ValidateEntry(SurvivorDef survivorDef)
		{
			if (survivorDef.bodyPrefab)
			{
				CharacterBody component = survivorDef.bodyPrefab.GetComponent<CharacterBody>();
				if (component)
				{
					if (string.IsNullOrEmpty(survivorDef.cachedName))
					{
						string text = BodyCatalog.GetBodyName(component.bodyIndex);
						string text2 = "Body";
						if (text.EndsWith(text2))
						{
							text = text.Substring(0, text.Length - text2.Length);
						}
						survivorDef.cachedName = text;
					}
					if (survivorDef.displayNameToken == null)
					{
						survivorDef.displayNameToken = component.baseNameToken;
					}
				}
			}
			survivorDef.displayNameToken = (survivorDef.displayNameToken ?? "");
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000FED3B File Offset: 0x000FCF3B
		[CanBeNull]
		public static SurvivorDef GetSurvivorDef(SurvivorIndex survivorIndex)
		{
			return ArrayUtils.GetSafe<SurvivorDef>(SurvivorCatalog.survivorDefs, (int)survivorIndex);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000FED48 File Offset: 0x000FCF48
		public static SurvivorIndex GetSurvivorIndexFromBodyIndex(BodyIndex bodyIndex)
		{
			SurvivorIndex[] array = SurvivorCatalog.bodyIndexToSurvivorIndex;
			SurvivorIndex survivorIndex = SurvivorIndex.None;
			return ArrayUtils.GetSafe<SurvivorIndex>(array, (int)bodyIndex, survivorIndex);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000FED64 File Offset: 0x000FCF64
		public static BodyIndex GetBodyIndexFromSurvivorIndex(SurvivorIndex survivorIndex)
		{
			BodyIndex[] array = SurvivorCatalog.survivorIndexToBodyIndex;
			BodyIndex bodyIndex = BodyIndex.None;
			return ArrayUtils.GetSafe<BodyIndex>(array, (int)survivorIndex, bodyIndex);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000FED80 File Offset: 0x000FCF80
		public static bool SurvivorIsUnlockedOnThisClient(SurvivorIndex survivorIndex)
		{
			return LocalUserManager.readOnlyLocalUsersList.Any((LocalUser localUser) => localUser.userProfile.HasSurvivorUnlocked(survivorIndex));
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000FEDB0 File Offset: 0x000FCFB0
		[CanBeNull]
		public static SurvivorDef FindSurvivorDefFromBody([CanBeNull] GameObject characterBodyPrefab)
		{
			for (int i = 0; i < SurvivorCatalog.survivorDefs.Length; i++)
			{
				SurvivorDef survivorDef = SurvivorCatalog.survivorDefs[i];
				GameObject y = (survivorDef != null) ? survivorDef.bodyPrefab : null;
				if (characterBodyPrefab == y)
				{
					return survivorDef;
				}
			}
			return null;
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000FEDF8 File Offset: 0x000FCFF8
		[CanBeNull]
		public static Texture GetSurvivorPortrait(SurvivorIndex survivorIndex)
		{
			SurvivorDef survivorDef = SurvivorCatalog.GetSurvivorDef(survivorIndex);
			if (survivorDef.bodyPrefab != null)
			{
				CharacterBody component = survivorDef.bodyPrefab.GetComponent<CharacterBody>();
				if (component)
				{
					return component.portraitIcon;
				}
			}
			return null;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000FEE36 File Offset: 0x000FD036
		public static SurvivorIndex FindSurvivorIndex([CanBeNull] string survivorName)
		{
			return (SurvivorIndex)Array.IndexOf<string>(SurvivorCatalog.cachedSurvivorNames, survivorName);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000FEE43 File Offset: 0x000FD043
		public static SurvivorDef FindSurvivorDef([CanBeNull] string survivorName)
		{
			return SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.FindSurvivorIndex(survivorName));
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000FEE50 File Offset: 0x000FD050
		public static int GetSurvivorOrderedPosition(SurvivorIndex survivorIndex)
		{
			int[] array = SurvivorCatalog.survivorOrderPositions;
			int num = -1;
			return ArrayUtils.GetSafe<int>(array, (int)survivorIndex, num);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x000FEE6C File Offset: 0x000FD06C
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog)
		})]
		private static void Init()
		{
			SurvivorCatalog.SetSurvivorDefs(ContentManager.survivorDefs);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000FEE78 File Offset: 0x000FD078
		private static void SetSurvivorDefs(SurvivorDef[] newSurvivorDefs)
		{
			SurvivorDef[] array = SurvivorCatalog.survivorDefs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].survivorIndex = SurvivorIndex.None;
			}
			ArrayUtils.CloneTo<SurvivorDef>(newSurvivorDefs, ref SurvivorCatalog.survivorDefs);
			Array.Resize<BodyIndex>(ref SurvivorCatalog.survivorIndexToBodyIndex, SurvivorCatalog.survivorDefs.Length);
			Array.Resize<string>(ref SurvivorCatalog.cachedSurvivorNames, SurvivorCatalog.survivorDefs.Length);
			Array.Resize<SurvivorDef>(ref SurvivorCatalog._orderedSurvivorDefs, SurvivorCatalog.survivorDefs.Length);
			Array.Resize<int>(ref SurvivorCatalog.survivorOrderPositions, SurvivorCatalog.survivorDefs.Length);
			Array.Resize<SurvivorIndex>(ref SurvivorCatalog.bodyIndexToSurvivorIndex, BodyCatalog.bodyCount);
			SurvivorIndex[] array2 = SurvivorCatalog.bodyIndexToSurvivorIndex;
			SurvivorIndex survivorIndex = SurvivorIndex.None;
			ArrayUtils.SetAll<SurvivorIndex>(array2, survivorIndex);
			for (SurvivorIndex survivorIndex2 = (SurvivorIndex)0; survivorIndex2 < (SurvivorIndex)SurvivorCatalog.survivorDefs.Length; survivorIndex2++)
			{
				SurvivorDef survivorDef = SurvivorCatalog.survivorDefs[(int)survivorIndex2];
				survivorDef.survivorIndex = survivorIndex2;
				SurvivorCatalog.ValidateEntry(survivorDef);
				SurvivorCatalog.cachedSurvivorNames[(int)survivorIndex2] = survivorDef.cachedName;
				BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(survivorDef.bodyPrefab);
				SurvivorCatalog.survivorIndexToBodyIndex[(int)survivorIndex2] = bodyIndex;
				SurvivorCatalog.bodyIndexToSurvivorIndex[(int)bodyIndex] = survivorIndex2;
			}
			ArrayUtils.CloneTo<SurvivorDef>(SurvivorCatalog.survivorDefs, ref SurvivorCatalog._orderedSurvivorDefs);
			Array.Sort<SurvivorDef>(SurvivorCatalog._orderedSurvivorDefs, (SurvivorDef a, SurvivorDef b) => a.desiredSortPosition.CompareTo(b.desiredSortPosition));
			for (int j = 0; j < SurvivorCatalog._orderedSurvivorDefs.Length; j++)
			{
				SurvivorIndex survivorIndex3 = SurvivorCatalog._orderedSurvivorDefs[j].survivorIndex;
				SurvivorCatalog.survivorOrderPositions[(int)survivorIndex3] = j;
			}
			ViewablesCatalog.Node node = new ViewablesCatalog.Node("Survivors", true, null);
			using (IEnumerator<SurvivorDef> enumerator = SurvivorCatalog.allSurvivorDefs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SurvivorCatalog.<>c__DisplayClass27_0 CS$<>8__locals1 = new SurvivorCatalog.<>c__DisplayClass27_0();
					CS$<>8__locals1.survivorDef = enumerator.Current;
					ViewablesCatalog.Node survivorEntryNode = new ViewablesCatalog.Node(CS$<>8__locals1.survivorDef.cachedName, false, node);
					survivorEntryNode.shouldShowUnviewed = ((UserProfile userProfile) => !userProfile.HasViewedViewable(survivorEntryNode.fullName) && CS$<>8__locals1.survivorDef.unlockableDef && userProfile.HasUnlockable(CS$<>8__locals1.survivorDef.unlockableDef));
				}
			}
			ViewablesCatalog.AddNodeToRoot(node);
			SurvivorCatalog.defaultSurvivor = SurvivorCatalog.FindSurvivorDef("Commando");
		}

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06003DBE RID: 15806 RVA: 0x000FF084 File Offset: 0x000FD284
		// (remove) Token: 0x06003DBF RID: 15807 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<SurvivorDef>> getAdditionalSurvivorDefs
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<SurvivorDef>("RoR2.SurvivorCatalog.getAdditionalSurvivorDefs", value, LegacyModContentPackProvider.instance.registrationContentPack.survivorDefs);
			}
			remove
			{
			}
		}

		// Token: 0x04003C85 RID: 15493
		public static int survivorMaxCount = 10;

		// Token: 0x04003C86 RID: 15494
		private static SurvivorDef[] survivorDefs = Array.Empty<SurvivorDef>();

		// Token: 0x04003C87 RID: 15495
		private static SurvivorDef[] _orderedSurvivorDefs = Array.Empty<SurvivorDef>();

		// Token: 0x04003C88 RID: 15496
		private static SurvivorIndex[] bodyIndexToSurvivorIndex = Array.Empty<SurvivorIndex>();

		// Token: 0x04003C89 RID: 15497
		private static BodyIndex[] survivorIndexToBodyIndex = Array.Empty<BodyIndex>();

		// Token: 0x04003C8A RID: 15498
		private static int[] survivorOrderPositions = Array.Empty<int>();

		// Token: 0x04003C8B RID: 15499
		private static string[] cachedSurvivorNames = Array.Empty<string>();

		// Token: 0x04003C8C RID: 15500
		public static SurvivorDef defaultSurvivor;
	}
}
