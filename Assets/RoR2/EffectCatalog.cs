using System;
using HG;
using RoR2.ContentManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000598 RID: 1432
	public static class EffectCatalog
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x0006F9C3 File Offset: 0x0006DBC3
		public static int effectCount
		{
			get
			{
				return EffectCatalog.entries.Length;
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006F9CC File Offset: 0x0006DBCC
		[SystemInitializer(new Type[]
		{

		})]
		public static void Init()
		{
			EffectCatalog.SetEntries(ContentManager.effectDefs);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006F9D8 File Offset: 0x0006DBD8
		public static void SetEntries(EffectDef[] newEntries)
		{
			foreach (EffectDef effectDef in EffectCatalog.entries)
			{
				effectDef.index = EffectIndex.Invalid;
				effectDef.prefabEffectComponent.effectIndex = EffectIndex.Invalid;
			}
			ArrayUtils.CloneTo<EffectDef>(newEntries, ref EffectCatalog.entries);
			Array.Sort<EffectDef>(EffectCatalog.entries, (EffectDef a, EffectDef b) => string.CompareOrdinal(a.prefabName, b.prefabName));
			for (int j = 0; j < EffectCatalog.entries.Length; j++)
			{
				ref EffectDef ptr = ref EffectCatalog.entries[j];
				ptr.index = (EffectIndex)j;
				ptr.prefabEffectComponent.effectIndex = ptr.index;
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0006FA7C File Offset: 0x0006DC7C
		public static EffectIndex FindEffectIndexFromPrefab(GameObject effectPrefab)
		{
			if (effectPrefab)
			{
				EffectComponent component = effectPrefab.GetComponent<EffectComponent>();
				if (component)
				{
					return component.effectIndex;
				}
			}
			return EffectIndex.Invalid;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006FAA8 File Offset: 0x0006DCA8
		public static EffectDef GetEffectDef(EffectIndex effectIndex)
		{
			EffectDef[] array = EffectCatalog.entries;
			EffectDef effectDef = null;
			return ArrayUtils.GetSafe<EffectDef>(array, (int)effectIndex, effectDef);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0006FAC4 File Offset: 0x0006DCC4
		[ConCommand(commandName = "effects_reload", flags = ConVarFlags.Cheat, helpText = "Reloads the effect catalog.")]
		private static void CCEffectsReload(ConCommandArgs args)
		{
			throw new ConCommandException("Command unavailable outside editor until asset reloading is implemented at the ContentPack level.");
		}

		// Token: 0x0400201C RID: 8220
		private static EffectDef[] entries = Array.Empty<EffectDef>();
	}
}
