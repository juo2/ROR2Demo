using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.FlyingVermin
{
	// Token: 0x02000383 RID: 899
	public class FallingDeath : GenericCharacterDeath
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x0004711E File Offset: 0x0004531E
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterMotor)
			{
				base.characterMotor.velocity.y = FallingDeath.initialVerticalVelocity;
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00047148 File Offset: 0x00045348
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > FallingDeath.deathDelay && NetworkServer.active && !this.hasDied)
			{
				this.hasDied = true;
				EffectManager.SimpleImpactEffect(FallingDeath.deathEffectPrefab, base.characterBody.corePosition, Vector3.up, true);
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x04001493 RID: 5267
		public static float initialVerticalVelocity;

		// Token: 0x04001494 RID: 5268
		public static float deathDelay;

		// Token: 0x04001495 RID: 5269
		public static GameObject deathEffectPrefab;

		// Token: 0x04001496 RID: 5270
		private bool hasDied;
	}
}
