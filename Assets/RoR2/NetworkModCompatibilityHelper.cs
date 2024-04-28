using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using HG;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x02000974 RID: 2420
	public static class NetworkModCompatibilityHelper
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000E7971 File Offset: 0x000E5B71
		// (set) Token: 0x060036EB RID: 14059 RVA: 0x000E7978 File Offset: 0x000E5B78
		public static string networkModHash { get; private set; } = string.Empty;

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000E7980 File Offset: 0x000E5B80
		// (set) Token: 0x060036ED RID: 14061 RVA: 0x000E7987 File Offset: 0x000E5B87
		public static string steamworksGameserverGameDataValue { get; private set; } = string.Empty;

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000E798F File Offset: 0x000E5B8F
		// (set) Token: 0x060036EF RID: 14063 RVA: 0x000E7996 File Offset: 0x000E5B96
		public static string steamworksGameserverGameRulesValue { get; private set; } = string.Empty;

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000E799E File Offset: 0x000E5B9E
		// (set) Token: 0x060036F1 RID: 14065 RVA: 0x000E79A8 File Offset: 0x000E5BA8
		public static IEnumerable<string> networkModList
		{
			get
			{
				return NetworkModCompatibilityHelper._networkModList;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				List<string> list = CollectionPool<string, List<string>>.RentCollection();
				foreach (string text in value)
				{
					if (text == null)
					{
						throw new ArgumentException("Argument cannot contain null entries.", "value");
					}
					list.Add(text);
				}
				NetworkModCompatibilityHelper._networkModList = list.ToArray();
				CollectionPool<string, List<string>>.ReturnCollection(list);
				NetworkModCompatibilityHelper.Rebuild();
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000E7A30 File Offset: 0x000E5C30
		// (set) Token: 0x060036F3 RID: 14067 RVA: 0x000E7A37 File Offset: 0x000E5C37
		public static NetworkModCompatibilityHelper.ModListHasherDelegate modListHasher
		{
			get
			{
				return NetworkModCompatibilityHelper._modListHasher;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				NetworkModCompatibilityHelper._modListHasher = value;
				NetworkModCompatibilityHelper.Rebuild();
			}
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000E7A54 File Offset: 0x000E5C54
		public static byte[] DefaultModListHasher([NotNull] IEnumerable<string> networkModList)
		{
			HashAlgorithm hashAlgorithm = MD5.Create();
			byte[] result = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(string.Join(",", networkModList)));
			hashAlgorithm.Dispose();
			return result;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000E7A88 File Offset: 0x000E5C88
		private static void Rebuild()
		{
			byte[] array = NetworkModCompatibilityHelper.modListHasher(NetworkModCompatibilityHelper.networkModList);
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.AppendByteHexValue(array[i]);
			}
			NetworkModCompatibilityHelper.networkModHash = stringBuilder.ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			NetworkModCompatibilityHelper.steamworksGameserverGameDataValue = NetworkModCompatibilityHelper.steamworksGameserverGameDataValuePrefix + NetworkModCompatibilityHelper.networkModHash;
			NetworkModCompatibilityHelper.steamworksGameserverGameRulesValue = string.Join(",", NetworkModCompatibilityHelper._networkModList);
			Action action = NetworkModCompatibilityHelper.onUpdated;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000E7B0C File Offset: 0x000E5D0C
		static NetworkModCompatibilityHelper()
		{
			NetworkModCompatibilityHelper.Rebuild();
		}

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x060036F7 RID: 14071 RVA: 0x000E7B6C File Offset: 0x000E5D6C
		// (remove) Token: 0x060036F8 RID: 14072 RVA: 0x000E7BA0 File Offset: 0x000E5DA0
		public static event Action onUpdated;

		// Token: 0x04003747 RID: 14151
		public static readonly string steamworksGameserverGameDataValuePrefix = "modHash=";

		// Token: 0x04003749 RID: 14153
		public static readonly string steamworksGameserverRulesBaseName = "mods";

		// Token: 0x0400374B RID: 14155
		private static string[] _networkModList = Array.Empty<string>();

		// Token: 0x0400374C RID: 14156
		private static NetworkModCompatibilityHelper.ModListHasherDelegate _modListHasher = new NetworkModCompatibilityHelper.ModListHasherDelegate(NetworkModCompatibilityHelper.DefaultModListHasher);

		// Token: 0x02000975 RID: 2421
		// (Invoke) Token: 0x060036FA RID: 14074
		[NotNull]
		public delegate byte[] ModListHasherDelegate([NotNull] IEnumerable<string> modList);
	}
}
