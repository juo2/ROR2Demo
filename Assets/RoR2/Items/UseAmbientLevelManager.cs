using System;
using System.Collections.ObjectModel;

namespace RoR2.Items
{
	// Token: 0x02000BED RID: 3053
	public class UseAmbientLevelManager
	{
		// Token: 0x06004542 RID: 17730 RVA: 0x0012058F File Offset: 0x0011E78F
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			Run.onRunAmbientLevelUp += UseAmbientLevelManager.OnRunAmbientLevelUp;
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x001205A4 File Offset: 0x0011E7A4
		private static void OnRunAmbientLevelUp(Run run)
		{
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			int i = 0;
			int count = readOnlyInstancesList.Count;
			while (i < count)
			{
				CharacterBody characterBody = readOnlyInstancesList[i];
				Inventory inventory = characterBody.inventory;
				if (inventory && inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel) > 0)
				{
					characterBody.MarkAllStatsDirty();
				}
				i++;
			}
		}
	}
}
