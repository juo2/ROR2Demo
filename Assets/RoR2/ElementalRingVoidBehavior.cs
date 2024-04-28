using System;

namespace RoR2
{
	// Token: 0x0200077F RID: 1919
	public class ElementalRingVoidBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x0600282D RID: 10285 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnEnable()
		{
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000AE69C File Offset: 0x000AC89C
		private void OnDisable()
		{
			if (this.body)
			{
				if (this.body.HasBuff(DLC1Content.Buffs.ElementalRingVoidReady))
				{
					this.body.RemoveBuff(DLC1Content.Buffs.ElementalRingVoidReady);
				}
				if (this.body.HasBuff(DLC1Content.Buffs.ElementalRingVoidCooldown))
				{
					this.body.RemoveBuff(DLC1Content.Buffs.ElementalRingVoidCooldown);
				}
			}
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000AE6FC File Offset: 0x000AC8FC
		private void FixedUpdate()
		{
			if (this.body && !this.body.HasBuff(DLC1Content.Buffs.ElementalRingVoidReady) && !this.body.HasBuff(DLC1Content.Buffs.ElementalRingVoidCooldown))
			{
				this.body.AddBuff(DLC1Content.Buffs.ElementalRingVoidReady);
			}
		}
	}
}
