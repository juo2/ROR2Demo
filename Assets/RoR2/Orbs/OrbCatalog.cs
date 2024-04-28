using System;
using System.Collections.Generic;
using System.Linq;

namespace RoR2.Orbs
{
	// Token: 0x02000B1D RID: 2845
	public static class OrbCatalog
	{
		// Token: 0x060040E0 RID: 16608 RVA: 0x0010C8B4 File Offset: 0x0010AAB4
		private static void GenerateCatalog()
		{
			OrbCatalog.indexToType = (from t in typeof(Orb).Assembly.GetTypes()
			where t.IsSubclassOf(typeof(Orb))
			orderby t.Name
			select t).ToArray<Type>();
			OrbCatalog.typeToIndex.Clear();
			foreach (Type key in OrbCatalog.indexToType)
			{
				OrbCatalog.typeToIndex[key] = OrbCatalog.typeToIndex.Count;
			}
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x0010C95E File Offset: 0x0010AB5E
		static OrbCatalog()
		{
			OrbCatalog.GenerateCatalog();
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x0010C97C File Offset: 0x0010AB7C
		public static int FindIndex(Type type)
		{
			int result;
			if (OrbCatalog.typeToIndex.TryGetValue(type, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x0010C99B File Offset: 0x0010AB9B
		public static Type FindType(int index)
		{
			if (index < 0 || index >= OrbCatalog.indexToType.Length)
			{
				return null;
			}
			return OrbCatalog.indexToType[index];
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0010C9B4 File Offset: 0x0010ABB4
		public static Orb Instantiate(int index)
		{
			return OrbCatalog.Instantiate(OrbCatalog.FindType(index));
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0010C9C1 File Offset: 0x0010ABC1
		public static Orb Instantiate(Type type)
		{
			if (type == null)
			{
				return null;
			}
			return (Orb)Activator.CreateInstance(type);
		}

		// Token: 0x04003F53 RID: 16211
		private static readonly Dictionary<Type, int> typeToIndex = new Dictionary<Type, int>();

		// Token: 0x04003F54 RID: 16212
		private static Type[] indexToType = Array.Empty<Type>();
	}
}
