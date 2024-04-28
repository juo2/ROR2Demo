using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000B8 RID: 184
	public class CloakTest : BaseState
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000CDD9 File Offset: 0x0000AFD9
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.Cloak);
				base.characterBody.AddBuff(RoR2Content.Buffs.CloakSpeed);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000CE15 File Offset: 0x0000B015
		public override void OnExit()
		{
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.Cloak);
				base.characterBody.RemoveBuff(RoR2Content.Buffs.CloakSpeed);
			}
			base.OnExit();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000CE51 File Offset: 0x0000B051
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0400034C RID: 844
		private float duration = 3f;
	}
}
