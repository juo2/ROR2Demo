using System;

namespace RoR2
{
	// Token: 0x0200077C RID: 1916
	public class BearVoidBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x0600281B RID: 10267 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnEnable()
		{
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000ADFA4 File Offset: 0x000AC1A4
		private void OnDisable()
		{
			if (this.body)
			{
				if (this.body.HasBuff(DLC1Content.Buffs.BearVoidReady))
				{
					this.body.RemoveBuff(DLC1Content.Buffs.BearVoidReady);
				}
				if (this.body.HasBuff(DLC1Content.Buffs.BearVoidCooldown))
				{
					this.body.RemoveBuff(DLC1Content.Buffs.BearVoidCooldown);
				}
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000AE004 File Offset: 0x000AC204
		private void FixedUpdate()
		{
			if (this.body && !this.body.HasBuff(DLC1Content.Buffs.BearVoidReady) && !this.body.HasBuff(DLC1Content.Buffs.BearVoidCooldown))
			{
				this.body.AddBuff(DLC1Content.Buffs.BearVoidReady);
			}
		}
	}
}
