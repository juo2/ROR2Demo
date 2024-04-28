using System;

namespace RoR2
{
	// Token: 0x02000783 RID: 1923
	internal class OutOfCombatArmorBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002842 RID: 10306 RVA: 0x000AEC73 File Offset: 0x000ACE73
		private void SetProvidingBuff(bool shouldProvideBuff)
		{
			if (shouldProvideBuff == this.providingBuff)
			{
				return;
			}
			this.providingBuff = shouldProvideBuff;
			if (this.providingBuff)
			{
				this.body.AddBuff(DLC1Content.Buffs.OutOfCombatArmorBuff);
				return;
			}
			this.body.RemoveBuff(DLC1Content.Buffs.OutOfCombatArmorBuff);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000AECAF File Offset: 0x000ACEAF
		private void OnDisable()
		{
			this.SetProvidingBuff(false);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000AECB8 File Offset: 0x000ACEB8
		private void FixedUpdate()
		{
			this.SetProvidingBuff(this.body.outOfDanger);
		}

		// Token: 0x04002BEC RID: 11244
		private bool providingBuff;
	}
}
