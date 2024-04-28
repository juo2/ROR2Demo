using System;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200077B RID: 1915
	public class AffixVoidBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002816 RID: 10262 RVA: 0x000ADE3C File Offset: 0x000AC03C
		private void Awake()
		{
			base.enabled = false;
			this.sdStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Weapon");
			this.healthComponent = base.GetComponent<HealthComponent>();
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
			this.hasBegunSelfDestruct = false;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000ADE8A File Offset: 0x000AC08A
		private void OnEnable()
		{
			if (this.body)
			{
				this.wasVoidBody = ((this.body.bodyFlags & CharacterBody.BodyFlags.Void) > CharacterBody.BodyFlags.None);
				this.body.bodyFlags |= CharacterBody.BodyFlags.Void;
			}
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000ADECC File Offset: 0x000AC0CC
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
				if (!this.wasVoidBody)
				{
					this.body.bodyFlags &= ~CharacterBody.BodyFlags.Void;
				}
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000ADF4C File Offset: 0x000AC14C
		private void FixedUpdate()
		{
			if (NetworkServer.active && this.body && !this.body.HasBuff(DLC1Content.Buffs.BearVoidReady) && !this.body.HasBuff(DLC1Content.Buffs.BearVoidCooldown))
			{
				this.body.AddBuff(DLC1Content.Buffs.BearVoidReady);
			}
		}

		// Token: 0x04002BC0 RID: 11200
		private const string sdStateMachineName = "Weapon";

		// Token: 0x04002BC1 RID: 11201
		private bool wasVoidBody;

		// Token: 0x04002BC2 RID: 11202
		private EntityStateMachine sdStateMachine;

		// Token: 0x04002BC3 RID: 11203
		private HealthComponent healthComponent;

		// Token: 0x04002BC4 RID: 11204
		private bool hasEffectiveAuthority;

		// Token: 0x04002BC5 RID: 11205
		private bool hasBegunSelfDestruct;
	}
}
