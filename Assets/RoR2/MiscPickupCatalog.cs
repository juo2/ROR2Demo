using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;

namespace RoR2
{
	// Token: 0x0200096C RID: 2412
	public static class MiscPickupCatalog
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000E7397 File Offset: 0x000E5597
		public static IReadOnlyList<MiscPickupDef> miscPickupDefs
		{
			get
			{
				return MiscPickupCatalog._miscPickupDefs;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000E739E File Offset: 0x000E559E
		public static int pickupCount
		{
			get
			{
				return MiscPickupCatalog._miscPickupDefs.Length;
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000E73A8 File Offset: 0x000E55A8
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ArrayUtils.CloneTo<MiscPickupDef>(ContentManager.miscPickupDefs, ref MiscPickupCatalog._miscPickupDefs);
			for (int i = 0; i < MiscPickupCatalog._miscPickupDefs.Length; i++)
			{
				MiscPickupCatalog._miscPickupDefs[i].miscPickupIndex = (MiscPickupIndex)i;
			}
			MiscPickupCatalog.availability.MakeAvailable();
		}

		// Token: 0x04003734 RID: 14132
		private static MiscPickupDef[] _miscPickupDefs = Array.Empty<MiscPickupDef>();

		// Token: 0x04003735 RID: 14133
		public static ResourceAvailability availability = default(ResourceAvailability);
	}
}
