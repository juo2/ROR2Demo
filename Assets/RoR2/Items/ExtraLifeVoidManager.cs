using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BD7 RID: 3031
	public static class ExtraLifeVoidManager
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x0011EB60 File Offset: 0x0011CD60
		// (set) Token: 0x060044D7 RID: 17623 RVA: 0x0011EB67 File Offset: 0x0011CD67
		public static GameObject rezEffectPrefab { get; private set; }

		// Token: 0x060044D8 RID: 17624 RVA: 0x0011EB6F File Offset: 0x0011CD6F
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			ExtraLifeVoidManager.rezEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/VoidRezEffect");
			ExtraLifeVoidManager.voidBodyNames = new string[]
			{
				"NullifierBody"
			};
			Run.onRunStartGlobal += ExtraLifeVoidManager.OnRunStart;
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x0011EBA4 File Offset: 0x0011CDA4
		private static void OnRunStart(Run run)
		{
			ExtraLifeVoidManager.rng = new Xoroshiro128Plus(run.seed ^ 733UL);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x0011EBBD File Offset: 0x0011CDBD
		public static GameObject GetNextBodyPrefab()
		{
			return BodyCatalog.FindBodyPrefab(ExtraLifeVoidManager.voidBodyNames[ExtraLifeVoidManager.rng.RangeInt(0, ExtraLifeVoidManager.voidBodyNames.Length)]);
		}

		// Token: 0x04004350 RID: 17232
		private const ulong seedSalt = 733UL;

		// Token: 0x04004351 RID: 17233
		private static Xoroshiro128Plus rng;

		// Token: 0x04004352 RID: 17234
		private static string[] voidBodyNames;
	}
}
