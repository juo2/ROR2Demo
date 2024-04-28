using System;
using UnityEngine;

namespace RoR2.Artifacts
{
	// Token: 0x02000E67 RID: 3687
	public class DoppelgangerSpawnCard : MasterCopySpawnCard
	{
		// Token: 0x06005469 RID: 21609 RVA: 0x0015C2D8 File Offset: 0x0015A4D8
		public static DoppelgangerSpawnCard FromMaster(CharacterMaster srcCharacterMaster)
		{
			DoppelgangerSpawnCard.<>c__DisplayClass0_0 CS$<>8__locals1 = new DoppelgangerSpawnCard.<>c__DisplayClass0_0();
			CS$<>8__locals1.srcCharacterMaster = srcCharacterMaster;
			if (!CS$<>8__locals1.srcCharacterMaster || !CS$<>8__locals1.srcCharacterMaster.GetBody())
			{
				return null;
			}
			DoppelgangerSpawnCard doppelgangerSpawnCard = ScriptableObject.CreateInstance<DoppelgangerSpawnCard>();
			MasterCopySpawnCard.CopyDataFromMaster(doppelgangerSpawnCard, CS$<>8__locals1.srcCharacterMaster, true, true);
			doppelgangerSpawnCard.GiveItem(RoR2Content.Items.InvadingDoppelganger, 1);
			doppelgangerSpawnCard.onPreSpawnSetup = new Action<CharacterMaster>(CS$<>8__locals1.<FromMaster>g__OnPreSpawnSetup|0);
			return doppelgangerSpawnCard;
		}
	}
}
