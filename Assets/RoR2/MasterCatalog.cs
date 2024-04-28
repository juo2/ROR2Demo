using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.CharacterAI;
using RoR2.ContentManagement;
using RoR2.Modding;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000963 RID: 2403
	public static class MasterCatalog
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000E6C2E File Offset: 0x000E4E2E
		public static IEnumerable<CharacterMaster> allMasters
		{
			get
			{
				return MasterCatalog.masterPrefabMasterComponents;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000E6C35 File Offset: 0x000E4E35
		public static IEnumerable<CharacterMaster> allAiMasters
		{
			get
			{
				return MasterCatalog.aiMasterPrefabs;
			}
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000E6C3C File Offset: 0x000E4E3C
		public static GameObject GetMasterPrefab(MasterCatalog.MasterIndex masterIndex)
		{
			return ArrayUtils.GetSafe<GameObject>(MasterCatalog.masterPrefabs, (int)masterIndex);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000E6C50 File Offset: 0x000E4E50
		public static MasterCatalog.MasterIndex FindMasterIndex([NotNull] string masterName)
		{
			MasterCatalog.MasterIndex result;
			if (MasterCatalog.nameToIndexMap.TryGetValue(masterName, out result))
			{
				return result;
			}
			return MasterCatalog.MasterIndex.none;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000E6C73 File Offset: 0x000E4E73
		public static MasterCatalog.MasterIndex FindMasterIndex(GameObject masterObject)
		{
			if (!masterObject)
			{
				return MasterCatalog.MasterIndex.none;
			}
			return MasterCatalog.FindMasterIndex(masterObject.name);
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000E6C90 File Offset: 0x000E4E90
		public static GameObject FindMasterPrefab([NotNull] string bodyName)
		{
			MasterCatalog.MasterIndex masterIndex = MasterCatalog.FindMasterIndex(bodyName);
			if (masterIndex.isValid)
			{
				return MasterCatalog.GetMasterPrefab(masterIndex);
			}
			return null;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000E6CB8 File Offset: 0x000E4EB8
		public static MasterCatalog.MasterIndex FindAiMasterIndexForBody(BodyIndex bodyIndex)
		{
			foreach (CharacterMaster characterMaster in MasterCatalog.aiMasterPrefabs)
			{
				if (characterMaster.bodyPrefab.GetComponent<CharacterBody>().bodyIndex == bodyIndex)
				{
					return characterMaster.masterIndex;
				}
			}
			return MasterCatalog.MasterIndex.none;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000E6CFC File Offset: 0x000E4EFC
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			MasterCatalog.SetEntries(ContentManager.masterPrefabs);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000E6D08 File Offset: 0x000E4F08
		private static void SetEntries(GameObject[] newEntries)
		{
			MasterCatalog.masterPrefabs = ArrayUtils.Clone<GameObject>(newEntries);
			Array.Sort<GameObject>(MasterCatalog.masterPrefabs, (GameObject a, GameObject b) => string.CompareOrdinal(a.name, b.name));
			MasterCatalog.masterPrefabMasterComponents = new CharacterMaster[MasterCatalog.masterPrefabs.Length];
			for (int i = 0; i < MasterCatalog.masterPrefabs.Length; i++)
			{
				MasterCatalog.MasterIndex masterIndex = new MasterCatalog.MasterIndex(i);
				GameObject gameObject = MasterCatalog.masterPrefabs[i];
				string name = gameObject.name;
				CharacterMaster component = gameObject.GetComponent<CharacterMaster>();
				MasterCatalog.nameToIndexMap.Add(name, masterIndex);
				MasterCatalog.nameToIndexMap.Add(name + "(Clone)", masterIndex);
				MasterCatalog.masterPrefabMasterComponents[i] = component;
				component.masterIndex = masterIndex;
			}
			MasterCatalog.aiMasterPrefabs = (from master in MasterCatalog.masterPrefabMasterComponents
			where master.GetComponent<BaseAI>()
			select master).ToArray<CharacterMaster>();
		}

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x060036A4 RID: 13988 RVA: 0x000E6DEC File Offset: 0x000E4FEC
		// (remove) Token: 0x060036A5 RID: 13989 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<GameObject>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<GameObject>("RoR2.MasterCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.masterPrefabs);
			}
			remove
			{
			}
		}

		// Token: 0x04003717 RID: 14103
		private static GameObject[] masterPrefabs;

		// Token: 0x04003718 RID: 14104
		private static CharacterMaster[] masterPrefabMasterComponents;

		// Token: 0x04003719 RID: 14105
		private static CharacterMaster[] aiMasterPrefabs;

		// Token: 0x0400371A RID: 14106
		private static readonly Dictionary<string, MasterCatalog.MasterIndex> nameToIndexMap = new Dictionary<string, MasterCatalog.MasterIndex>();

		// Token: 0x02000964 RID: 2404
		public struct MasterIndex : IEquatable<MasterCatalog.MasterIndex>
		{
			// Token: 0x060036A7 RID: 13991 RVA: 0x000E6E19 File Offset: 0x000E5019
			public MasterIndex(int i)
			{
				this.i = i;
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000E6E22 File Offset: 0x000E5022
			public bool isValid
			{
				get
				{
					return this.i >= 0;
				}
			}

			// Token: 0x060036A9 RID: 13993 RVA: 0x000E6E30 File Offset: 0x000E5030
			public static explicit operator int(MasterCatalog.MasterIndex masterIndex)
			{
				return masterIndex.i;
			}

			// Token: 0x060036AA RID: 13994 RVA: 0x000E6E38 File Offset: 0x000E5038
			public static explicit operator MasterCatalog.MasterIndex(int value)
			{
				return new MasterCatalog.MasterIndex(value);
			}

			// Token: 0x060036AB RID: 13995 RVA: 0x000E6E40 File Offset: 0x000E5040
			public bool Equals(MasterCatalog.MasterIndex other)
			{
				return this.i == other.i;
			}

			// Token: 0x060036AC RID: 13996 RVA: 0x000E6E50 File Offset: 0x000E5050
			public override bool Equals(object obj)
			{
				if (obj is MasterCatalog.MasterIndex)
				{
					MasterCatalog.MasterIndex other = (MasterCatalog.MasterIndex)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x060036AD RID: 13997 RVA: 0x000E6E30 File Offset: 0x000E5030
			public override int GetHashCode()
			{
				return this.i;
			}

			// Token: 0x060036AE RID: 13998 RVA: 0x000E6E40 File Offset: 0x000E5040
			public static bool operator ==(MasterCatalog.MasterIndex a, MasterCatalog.MasterIndex b)
			{
				return a.i == b.i;
			}

			// Token: 0x060036AF RID: 13999 RVA: 0x000E6E77 File Offset: 0x000E5077
			public static bool operator !=(MasterCatalog.MasterIndex a, MasterCatalog.MasterIndex b)
			{
				return a.i != b.i;
			}

			// Token: 0x0400371B RID: 14107
			private readonly int i;

			// Token: 0x0400371C RID: 14108
			public static readonly MasterCatalog.MasterIndex none = new MasterCatalog.MasterIndex(-1);
		}

		// Token: 0x02000965 RID: 2405
		[Serializable]
		public struct NetworkMasterIndex : IEquatable<MasterCatalog.NetworkMasterIndex>
		{
			// Token: 0x060036B1 RID: 14001 RVA: 0x000E6E98 File Offset: 0x000E5098
			public static implicit operator MasterCatalog.NetworkMasterIndex(MasterCatalog.MasterIndex masterIndex)
			{
				return new MasterCatalog.NetworkMasterIndex
				{
					i = (uint)((int)masterIndex + 1)
				};
			}

			// Token: 0x060036B2 RID: 14002 RVA: 0x000E6EBD File Offset: 0x000E50BD
			public static implicit operator MasterCatalog.MasterIndex(MasterCatalog.NetworkMasterIndex networkMasterIndex)
			{
				return new MasterCatalog.MasterIndex((int)(networkMasterIndex.i - 1U));
			}

			// Token: 0x060036B3 RID: 14003 RVA: 0x000E6ECC File Offset: 0x000E50CC
			public bool Equals(MasterCatalog.NetworkMasterIndex other)
			{
				return this.i == other.i;
			}

			// Token: 0x060036B4 RID: 14004 RVA: 0x000E6EDC File Offset: 0x000E50DC
			public override bool Equals(object obj)
			{
				if (obj is MasterCatalog.MasterIndex)
				{
					MasterCatalog.MasterIndex masterIndex = (MasterCatalog.MasterIndex)obj;
					return this.Equals(masterIndex);
				}
				return false;
			}

			// Token: 0x060036B5 RID: 14005 RVA: 0x000E6F08 File Offset: 0x000E5108
			public override int GetHashCode()
			{
				return (int)this.i;
			}

			// Token: 0x0400371D RID: 14109
			public uint i;
		}
	}
}
