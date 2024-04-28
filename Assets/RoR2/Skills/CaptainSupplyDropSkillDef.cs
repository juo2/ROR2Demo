using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000BFA RID: 3066
	public class CaptainSupplyDropSkillDef : CaptainOrbitalSkillDef
	{
		// Token: 0x06004591 RID: 17809 RVA: 0x001216B8 File Offset: 0x0011F8B8
		public override Sprite GetCurrentIcon(GenericSkill skillSlot)
		{
			if (!((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).anySupplyDropsAvailable)
			{
				return this.exhaustedIcon;
			}
			return base.GetCurrentIcon(skillSlot);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x001216DA File Offset: 0x0011F8DA
		public override string GetCurrentNameToken(GenericSkill skillSlot)
		{
			if (!((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).anySupplyDropsAvailable)
			{
				return this.exhaustedNameToken;
			}
			return base.GetCurrentNameToken(skillSlot);
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x001216FC File Offset: 0x0011F8FC
		public override string GetCurrentDescriptionToken(GenericSkill skillSlot)
		{
			if (!((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).anySupplyDropsAvailable)
			{
				return this.exhaustedDescriptionToken;
			}
			return base.GetCurrentDescriptionToken(skillSlot);
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x0012171E File Offset: 0x0011F91E
		public override bool CanExecute(GenericSkill skillSlot)
		{
			return ((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).anySupplyDropsAvailable && base.CanExecute(skillSlot);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x0012173B File Offset: 0x0011F93B
		public override bool IsReady(GenericSkill skillSlot)
		{
			return ((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).anySupplyDropsAvailable && base.IsReady(skillSlot);
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x00121758 File Offset: 0x0011F958
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			return new CaptainSupplyDropSkillDef.InstanceData(skillSlot, this.supplyDropSkillSlotNames);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x00121766 File Offset: 0x0011F966
		public override void OnUnassigned(GenericSkill skillSlot)
		{
			((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).Dispose();
			base.OnUnassigned(skillSlot);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x0012177F File Offset: 0x0011F97F
		public override void OnFixedUpdate(GenericSkill skillSlot)
		{
			base.OnFixedUpdate(skillSlot);
			((CaptainSupplyDropSkillDef.InstanceData)skillSlot.skillInstanceData).FixedUpdate();
		}

		// Token: 0x040043C1 RID: 17345
		public string[] supplyDropSkillSlotNames = Array.Empty<string>();

		// Token: 0x040043C2 RID: 17346
		public Sprite exhaustedIcon;

		// Token: 0x040043C3 RID: 17347
		public string exhaustedNameToken;

		// Token: 0x040043C4 RID: 17348
		public string exhaustedDescriptionToken;

		// Token: 0x02000BFB RID: 3067
		protected class InstanceData : SkillDef.BaseSkillInstanceData, IDisposable
		{
			// Token: 0x0600459A RID: 17818 RVA: 0x001217AB File Offset: 0x0011F9AB
			public InstanceData(GenericSkill skillSlot, string[] supplyDropSkillSlotNames)
			{
				this.supplySkillSlots = CollectionPool<GenericSkill, List<GenericSkill>>.RentCollection();
				this.skillSlot = skillSlot;
				this.supplyDropSkillSlotNames = supplyDropSkillSlotNames;
			}

			// Token: 0x0600459B RID: 17819 RVA: 0x001217CC File Offset: 0x0011F9CC
			public void Dispose()
			{
				this.supplySkillSlots = CollectionPool<GenericSkill, List<GenericSkill>>.ReturnCollection(this.supplySkillSlots);
				this.skillSlot = null;
				this.supplyDropSkillSlotNames = null;
			}

			// Token: 0x0600459C RID: 17820 RVA: 0x001217F0 File Offset: 0x0011F9F0
			private bool CheckForSupplyDropAvailable()
			{
				for (int i = 0; i < this.supplySkillSlots.Count; i++)
				{
					if (this.supplySkillSlots[i].IsReady())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600459D RID: 17821 RVA: 0x0012182C File Offset: 0x0011FA2C
			private void FindSupplyDropSkillSlots()
			{
				for (int i = 0; i < this.supplyDropSkillSlotNames.Length; i++)
				{
					GenericSkill genericSkill = this.skillSlot.GetComponent<SkillLocator>().FindSkill(this.supplyDropSkillSlotNames[i]);
					if (genericSkill)
					{
						this.supplySkillSlots.Add(genericSkill);
					}
				}
			}

			// Token: 0x0600459E RID: 17822 RVA: 0x00121879 File Offset: 0x0011FA79
			public void FixedUpdate()
			{
				if (this.supplySkillSlots.Count == 0)
				{
					this.FindSupplyDropSkillSlots();
				}
				this.anySupplyDropsAvailable = this.CheckForSupplyDropAvailable();
			}

			// Token: 0x040043C5 RID: 17349
			public List<GenericSkill> supplySkillSlots;

			// Token: 0x040043C6 RID: 17350
			public bool anySupplyDropsAvailable;

			// Token: 0x040043C7 RID: 17351
			private GenericSkill skillSlot;

			// Token: 0x040043C8 RID: 17352
			private string[] supplyDropSkillSlotNames;
		}
	}
}
