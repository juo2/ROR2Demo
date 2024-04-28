using System;
using HG;

namespace RoR2
{
	// Token: 0x0200058B RID: 1419
	public static class DifficultyCatalog
	{
		// Token: 0x06001971 RID: 6513 RVA: 0x0006DF98 File Offset: 0x0006C198
		static DifficultyCatalog()
		{
			DifficultyCatalog.difficultyDefs = new DifficultyDef[11];
			DifficultyCatalog.difficultyDefs[0] = new DifficultyDef(1f, "DIFFICULTY_EASY_NAME", "Textures/DifficultyIcons/texDifficultyEasyIcon", "DIFFICULTY_EASY_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.EasyDifficulty), "dz", false);
			DifficultyCatalog.difficultyDefs[1] = new DifficultyDef(2f, "DIFFICULTY_NORMAL_NAME", "Textures/DifficultyIcons/texDifficultyNormalIcon", "DIFFICULTY_NORMAL_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.NormalDifficulty), "rs", false);
			DifficultyCatalog.difficultyDefs[2] = new DifficultyDef(3f, "DIFFICULTY_HARD_NAME", "Textures/DifficultyIcons/texDifficultyHardIcon", "DIFFICULTY_HARD_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[3] = new DifficultyDef(3f, "ECLIPSE_1_NAME", "Textures/DifficultyIcons/texDifficultyEclipse1Icon", "ECLIPSE_1_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[4] = new DifficultyDef(3f, "ECLIPSE_2_NAME", "Textures/DifficultyIcons/texDifficultyEclipse2Icon", "ECLIPSE_2_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[5] = new DifficultyDef(3f, "ECLIPSE_3_NAME", "Textures/DifficultyIcons/texDifficultyEclipse3Icon", "ECLIPSE_3_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[6] = new DifficultyDef(3f, "ECLIPSE_4_NAME", "Textures/DifficultyIcons/texDifficultyEclipse4Icon", "ECLIPSE_4_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[7] = new DifficultyDef(3f, "ECLIPSE_5_NAME", "Textures/DifficultyIcons/texDifficultyEclipse5Icon", "ECLIPSE_5_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[8] = new DifficultyDef(3f, "ECLIPSE_6_NAME", "Textures/DifficultyIcons/texDifficultyEclipse6Icon", "ECLIPSE_6_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[9] = new DifficultyDef(3f, "ECLIPSE_7_NAME", "Textures/DifficultyIcons/texDifficultyEclipse7Icon", "ECLIPSE_7_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
			DifficultyCatalog.difficultyDefs[10] = new DifficultyDef(3f, "ECLIPSE_8_NAME", "Textures/DifficultyIcons/texDifficultyEclipse8Icon", "ECLIPSE_8_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty), "mn", true);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006E1DF File Offset: 0x0006C3DF
		public static DifficultyDef GetDifficultyDef(DifficultyIndex difficultyIndex)
		{
			return ArrayUtils.GetSafe<DifficultyDef>(DifficultyCatalog.difficultyDefs, (int)difficultyIndex);
		}

		// Token: 0x04001FDF RID: 8159
		private static readonly DifficultyDef[] difficultyDefs;

		// Token: 0x04001FE0 RID: 8160
		public static int standardDifficultyCount = 3;
	}
}
