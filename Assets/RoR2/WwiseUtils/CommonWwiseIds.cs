using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RoR2.WwiseUtils
{
	// Token: 0x02000AAC RID: 2732
	public static class CommonWwiseIds
	{
		// Token: 0x06003ECF RID: 16079 RVA: 0x00103178 File Offset: 0x00101378
		[RuntimeInitializeOnLoadMethod]
		public static void Init()
		{
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.none, "None");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.alive, "alive");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.dead, "dead");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.bossfight, "Bossfight");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.gameplay, "Gameplay");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.menu, "Menu");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.main, "Main");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.logbook, "Logbook");
			CommonWwiseIds.<Init>g__Assign|9_0(ref CommonWwiseIds.secretLevel, "SecretLevel");
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0010320C File Offset: 0x0010140C
		[CompilerGenerated]
		internal static void <Init>g__Assign|9_0(ref uint field, string name)
		{
			field = AkSoundEngine.GetIDFromString(name);
		}

		// Token: 0x04003D11 RID: 15633
		public static uint none;

		// Token: 0x04003D12 RID: 15634
		public static uint alive;

		// Token: 0x04003D13 RID: 15635
		public static uint bossfight;

		// Token: 0x04003D14 RID: 15636
		public static uint dead;

		// Token: 0x04003D15 RID: 15637
		public static uint gameplay;

		// Token: 0x04003D16 RID: 15638
		public static uint menu;

		// Token: 0x04003D17 RID: 15639
		public static uint main;

		// Token: 0x04003D18 RID: 15640
		public static uint logbook;

		// Token: 0x04003D19 RID: 15641
		public static uint secretLevel;
	}
}
