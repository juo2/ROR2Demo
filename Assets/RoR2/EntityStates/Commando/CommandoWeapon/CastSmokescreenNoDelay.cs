using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003E8 RID: 1000
	public class CastSmokescreenNoDelay : BaseState
	{
		// Token: 0x060011F2 RID: 4594 RVA: 0x0004F8F8 File Offset: 0x0004DAF8
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.CastSmoke();
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.Cloak);
				base.characterBody.AddBuff(RoR2Content.Buffs.CloakSpeed);
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004F954 File Offset: 0x0004DB54
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
			if (CastSmokescreenNoDelay.destealthMaterial)
			{
				TemporaryOverlay temporaryOverlay = this.animator.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 1f;
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = CastSmokescreenNoDelay.destealthMaterial;
				temporaryOverlay.inspectorCharacterModel = this.animator.gameObject.GetComponent<CharacterModel>();
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.animateShaderAlpha = true;
			}
			base.OnExit();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004FA46 File Offset: 0x0004DC46
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= CastSmokescreenNoDelay.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004FA84 File Offset: 0x0004DC84
		private void CastSmoke()
		{
			if (!this.hasCastSmoke)
			{
				Util.PlaySound(CastSmokescreenNoDelay.startCloakSoundString, base.gameObject);
				this.hasCastSmoke = true;
			}
			else
			{
				Util.PlaySound(CastSmokescreenNoDelay.stopCloakSoundString, base.gameObject);
			}
			EffectManager.SpawnEffect(CastSmokescreenNoDelay.smokescreenEffectPrefab, new EffectData
			{
				origin = base.transform.position
			}, false);
			int layerIndex = this.animator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, 1f);
				this.animator.PlayInFixedTime("LightImpact", layerIndex, 0f);
			}
			if (NetworkServer.active)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * CastSmokescreenNoDelay.damageCoefficient,
					baseForce = CastSmokescreenNoDelay.forceMagnitude,
					position = base.transform.position,
					radius = CastSmokescreenNoDelay.radius,
					falloffModel = BlastAttack.FalloffModel.None,
					damageType = DamageType.Stun1s,
					attackerFiltering = AttackerFiltering.NeverHitSelf
				}.Fire();
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004FBAE File Offset: 0x0004DDAE
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (this.stopwatch <= CastSmokescreenNoDelay.minimumStateDuration)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Any;
		}

		// Token: 0x040016D1 RID: 5841
		public static float duration;

		// Token: 0x040016D2 RID: 5842
		public static float minimumStateDuration = 3f;

		// Token: 0x040016D3 RID: 5843
		public static string startCloakSoundString;

		// Token: 0x040016D4 RID: 5844
		public static string stopCloakSoundString;

		// Token: 0x040016D5 RID: 5845
		public static GameObject smokescreenEffectPrefab;

		// Token: 0x040016D6 RID: 5846
		public static Material destealthMaterial;

		// Token: 0x040016D7 RID: 5847
		public static float damageCoefficient = 1.3f;

		// Token: 0x040016D8 RID: 5848
		public static float radius = 4f;

		// Token: 0x040016D9 RID: 5849
		public static float forceMagnitude = 100f;

		// Token: 0x040016DA RID: 5850
		private float stopwatch;

		// Token: 0x040016DB RID: 5851
		private bool hasCastSmoke;

		// Token: 0x040016DC RID: 5852
		private Animator animator;
	}
}
