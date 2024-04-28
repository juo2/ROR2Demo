using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidInfestor
{
	// Token: 0x0200015C RID: 348
	public class Spawn : BaseState
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x0001A4C8 File Offset: 0x000186C8
		public override void OnEnter()
		{
			base.OnEnter();
			if (Spawn.spawnEffectPrefab)
			{
				EffectManager.SimpleImpactEffect(Spawn.spawnEffectPrefab, base.characterBody.corePosition, Vector3.up, false);
			}
			if (base.characterMotor)
			{
				Vector3 vector = (Vector3.up + UnityEngine.Random.onUnitSphere * Spawn.spread) * Spawn.velocityStrength;
				base.characterMotor.ApplyForce(vector, true, true);
				base.characterDirection.forward = vector;
			}
			this.PlayAnimation("Base", "Spawn");
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001A560 File Offset: 0x00018760
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.characterMotor && base.characterMotor.isGrounded && base.fixedAge > 0.1f)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0400076D RID: 1901
		public static GameObject spawnEffectPrefab;

		// Token: 0x0400076E RID: 1902
		public static float velocityStrength;

		// Token: 0x0400076F RID: 1903
		public static float spread;
	}
}
