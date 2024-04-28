using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E4 RID: 740
	public class ExplosionAttack : BaseState
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x000378A9 File Offset: 0x00035AA9
		public override void OnEnter()
		{
			base.OnEnter();
			this.explosionInterval = ExplosionAttack.baseDuration / (float)ExplosionAttack.explosionCount;
			this.explosionIndex = 0;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000378CC File Offset: 0x00035ACC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.explosionTimer -= Time.fixedDeltaTime;
			if (this.explosionTimer <= 0f)
			{
				if (this.explosionIndex >= ExplosionAttack.explosionCount)
				{
					if (base.isAuthority)
					{
						this.outer.SetNextStateToMain();
						return;
					}
				}
				else
				{
					this.explosionTimer += this.explosionInterval;
					this.Explode();
					this.explosionIndex++;
				}
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00037948 File Offset: 0x00035B48
		private void Explode()
		{
			float t = (float)this.explosionIndex / (float)(ExplosionAttack.explosionCount - 1);
			float num = Mathf.Lerp(ExplosionAttack.minRadius, ExplosionAttack.maxRadius, t);
			EffectManager.SpawnEffect(ExplosionAttack.novaEffectPrefab, new EffectData
			{
				origin = base.transform.position,
				scale = num
			}, false);
			if (NetworkServer.active)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * ExplosionAttack.damageCoefficient * Mathf.Pow(ExplosionAttack.damageScaling, (float)this.explosionIndex),
					baseForce = ExplosionAttack.force,
					position = base.transform.position,
					radius = num,
					falloffModel = BlastAttack.FalloffModel.None,
					attackerFiltering = AttackerFiltering.NeverHitSelf
				}.Fire();
			}
		}

		// Token: 0x04001016 RID: 4118
		public static float minRadius;

		// Token: 0x04001017 RID: 4119
		public static float maxRadius;

		// Token: 0x04001018 RID: 4120
		public static int explosionCount;

		// Token: 0x04001019 RID: 4121
		public static float baseDuration;

		// Token: 0x0400101A RID: 4122
		public static float damageCoefficient;

		// Token: 0x0400101B RID: 4123
		public static float force;

		// Token: 0x0400101C RID: 4124
		public static float damageScaling;

		// Token: 0x0400101D RID: 4125
		public static GameObject novaEffectPrefab;

		// Token: 0x0400101E RID: 4126
		private float explosionTimer;

		// Token: 0x0400101F RID: 4127
		private float explosionInterval;

		// Token: 0x04001020 RID: 4128
		private int explosionIndex;
	}
}
