using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007FD RID: 2045
	public class PhasedInventorySetter : MonoBehaviour
	{
		// Token: 0x06002C14 RID: 11284 RVA: 0x000BC7DB File Offset: 0x000BA9DB
		public bool AdvancePhase()
		{
			if (this.phaseIndex < this.phases.Length - 1)
			{
				this.phaseIndex++;
				this.isPhaseDirty = true;
				this.TryUpdateInventory();
				return true;
			}
			return false;
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000BC80D File Offset: 0x000BAA0D
		private void FixedUpdate()
		{
			this.TryUpdateInventory();
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000BC818 File Offset: 0x000BAA18
		private void TryUpdateInventory()
		{
			if (NetworkServer.active && this.isPhaseDirty && this.phaseIndex < this.phases.Length && this.body && this.body.inventory)
			{
				foreach (ItemCountPair itemCountPair in this.phases[this.phaseIndex].itemCounts)
				{
					int itemCount = this.body.inventory.GetItemCount(itemCountPair.itemDef);
					this.body.inventory.GiveItem(itemCountPair.itemDef, itemCountPair.count - itemCount);
				}
				this.isPhaseDirty = false;
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000BC8D5 File Offset: 0x000BAAD5
		public int GetNumPhases()
		{
			return this.phases.Length;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000BC8E0 File Offset: 0x000BAAE0
		public int GetItemCountForPhase(int phaseIndex, ItemDef itemDef)
		{
			if (this.phases.Length > phaseIndex)
			{
				foreach (ItemCountPair itemCountPair in this.phases[phaseIndex].itemCounts)
				{
					if (itemDef == itemCountPair.itemDef)
					{
						return itemCountPair.count;
					}
				}
			}
			return 0;
		}

		// Token: 0x04002E86 RID: 11910
		[SerializeField]
		private CharacterBody body;

		// Token: 0x04002E87 RID: 11911
		[SerializeField]
		private PhasedInventorySetter.PhaseItems[] phases;

		// Token: 0x04002E88 RID: 11912
		private bool isPhaseDirty = true;

		// Token: 0x04002E89 RID: 11913
		private int phaseIndex;

		// Token: 0x020007FE RID: 2046
		[Serializable]
		public struct PhaseItems
		{
			// Token: 0x04002E8A RID: 11914
			public ItemCountPair[] itemCounts;
		}
	}
}
