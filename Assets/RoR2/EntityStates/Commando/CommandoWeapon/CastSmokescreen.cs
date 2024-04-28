using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003E7 RID: 999
	public class CastSmokescreen : BaseState
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x0004F5DC File Offset: 0x0004D7DC
		private void CastSmoke()
		{
			if (!this.hasCastSmoke)
			{
				Util.PlaySound(CastSmokescreen.startCloakSoundString, base.gameObject);
			}
			else
			{
				Util.PlaySound(CastSmokescreen.stopCloakSoundString, base.gameObject);
			}
			EffectManager.SpawnEffect(CastSmokescreen.smokescreenEffectPrefab, new EffectData
			{
				origin = base.transform.position
			}, false);
			int layerIndex = this.animator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, 2f);
				this.animator.PlayInFixedTime("LightImpact", layerIndex, 0f);
			}
			if (NetworkServer.active)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * CastSmokescreen.damageCoefficient,
					baseForce = CastSmokescreen.forceMagnitude,
					position = base.transform.position,
					radius = CastSmokescreen.radius,
					falloffModel = BlastAttack.FalloffModel.None,
					damageType = DamageType.Stun1s,
					attackerFiltering = AttackerFiltering.NeverHitSelf
				}.Fire();
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004F700 File Offset: 0x0004D900
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = CastSmokescreen.baseDuration / this.attackSpeedStat;
			this.totalDuration = CastSmokescreen.stealthDuration + this.totalDuration;
			base.PlayCrossfade("Gesture, Smokescreen", "CastSmokescreen", "CastSmokescreen.playbackRate", this.duration, 0.2f);
			this.animator = base.GetModelAnimator();
			Util.PlaySound(CastSmokescreen.jumpSoundString, base.gameObject);
			EffectManager.SpawnEffect(CastSmokescreen.initialEffectPrefab, new EffectData
			{
				origin = base.transform.position
			}, true);
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.CloakSpeed.buffIndex);
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		public override void OnExit()
		{
			if (base.characterBody && NetworkServer.active)
			{
				if (base.characterBody.HasBuff(RoR2Content.Buffs.Cloak))
				{
					base.characterBody.RemoveBuff(RoR2Content.Buffs.Cloak);
				}
				if (base.characterBody.HasBuff(RoR2Content.Buffs.CloakSpeed))
				{
					base.characterBody.RemoveBuff(RoR2Content.Buffs.CloakSpeed);
				}
			}
			if (!this.outer.destroying)
			{
				this.CastSmoke();
			}
			base.OnExit();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0004F840 File Offset: 0x0004DA40
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && !this.hasCastSmoke)
			{
				this.CastSmoke();
				if (base.characterBody && NetworkServer.active)
				{
					base.characterBody.AddBuff(RoR2Content.Buffs.Cloak.buffIndex);
				}
				this.hasCastSmoke = true;
			}
			if (base.fixedAge >= this.totalDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004F8C1 File Offset: 0x0004DAC1
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (!this.hasCastSmoke)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Any;
		}

		// Token: 0x040016C3 RID: 5827
		public static float baseDuration;

		// Token: 0x040016C4 RID: 5828
		public static float stealthDuration = 3f;

		// Token: 0x040016C5 RID: 5829
		public static string jumpSoundString;

		// Token: 0x040016C6 RID: 5830
		public static string startCloakSoundString;

		// Token: 0x040016C7 RID: 5831
		public static string stopCloakSoundString;

		// Token: 0x040016C8 RID: 5832
		public static GameObject initialEffectPrefab;

		// Token: 0x040016C9 RID: 5833
		public static GameObject smokescreenEffectPrefab;

		// Token: 0x040016CA RID: 5834
		public static float damageCoefficient = 1.3f;

		// Token: 0x040016CB RID: 5835
		public static float radius = 4f;

		// Token: 0x040016CC RID: 5836
		public static float forceMagnitude = 100f;

		// Token: 0x040016CD RID: 5837
		private float duration;

		// Token: 0x040016CE RID: 5838
		private float totalDuration;

		// Token: 0x040016CF RID: 5839
		private bool hasCastSmoke;

		// Token: 0x040016D0 RID: 5840
		private Animator animator;
	}
}
