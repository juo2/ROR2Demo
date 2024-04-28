using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x020004ED RID: 1261
	public static class BuffCatalog
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x000651FF File Offset: 0x000633FF
		public static int buffCount
		{
			get
			{
				return BuffCatalog.buffDefs.Length;
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00065208 File Offset: 0x00063408
		private static void RegisterBuff(BuffIndex buffIndex, BuffDef buffDef)
		{
			buffDef.buffIndex = buffIndex;
			BuffCatalog.nameToBuffIndex[buffDef.name] = buffIndex;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00065222 File Offset: 0x00063422
		public static BuffDef GetBuffDef(BuffIndex buffIndex)
		{
			return ArrayUtils.GetSafe<BuffDef>(BuffCatalog.buffDefs, (int)buffIndex);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00065230 File Offset: 0x00063430
		public static BuffIndex FindBuffIndex(string buffName)
		{
			BuffIndex result;
			if (BuffCatalog.nameToBuffIndex.TryGetValue(buffName, out result))
			{
				return result;
			}
			return BuffIndex.None;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0006524F File Offset: 0x0006344F
		public static T[] GetPerBuffBuffer<T>()
		{
			return new T[BuffCatalog.buffCount];
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0006525B File Offset: 0x0006345B
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			BuffCatalog.SetBuffDefs(ContentManager.buffDefs);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00065268 File Offset: 0x00063468
		private static void SetBuffDefs(BuffDef[] newBuffDefs)
		{
			BuffCatalog.nameToBuffIndex.Clear();
			BuffCatalog.buffDefs = ArrayUtils.Clone<BuffDef>(newBuffDefs);
			for (BuffIndex buffIndex = (BuffIndex)0; buffIndex < (BuffIndex)BuffCatalog.buffDefs.Length; buffIndex++)
			{
				BuffCatalog.RegisterBuff(buffIndex, BuffCatalog.buffDefs[(int)buffIndex]);
			}
			BuffCatalog.eliteBuffIndices = (from buffDef in BuffCatalog.buffDefs
			where buffDef.isElite
			select buffDef.buffIndex).ToArray<BuffIndex>();
			BuffCatalog.debuffBuffIndices = (from buffDef in BuffCatalog.buffDefs
			where buffDef.isDebuff
			select buffDef.buffIndex).ToArray<BuffIndex>();
			BuffCatalog.nonHiddenBuffIndices = (from buffDef in BuffCatalog.buffDefs
			where !buffDef.isHidden
			select buffDef.buffIndex).ToArray<BuffIndex>();
		}

		// Token: 0x04001C94 RID: 7316
		private static BuffDef[] buffDefs;

		// Token: 0x04001C95 RID: 7317
		public static BuffIndex[] eliteBuffIndices;

		// Token: 0x04001C96 RID: 7318
		public static BuffIndex[] debuffBuffIndices;

		// Token: 0x04001C97 RID: 7319
		public static BuffIndex[] nonHiddenBuffIndices;

		// Token: 0x04001C98 RID: 7320
		private static readonly Dictionary<string, BuffIndex> nameToBuffIndex = new Dictionary<string, BuffIndex>();

		// Token: 0x04001C99 RID: 7321
		[Obsolete("Use IContentPackProvider instead.")]
		public static readonly CatalogModHelperProxy<BuffDef> modHelper = new CatalogModHelperProxy<BuffDef>("RoR2.BuffCatalog.modHelper", LegacyModContentPackProvider.instance.registrationContentPack.buffDefs);
	}
}
