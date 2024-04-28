using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E5 RID: 741
	public class FireMegaNova : BaseState
	{
		// Token: 0x06000D39 RID: 3385 RVA: 0x00037A34 File Offset: 0x00035C34
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = FireMegaNova.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Override", "FireMegaNova", "FireMegaNova.playbackRate", this.duration);
			this.Detonate();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00037A85 File Offset: 0x00035C85
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00037AC4 File Offset: 0x00035CC4
		private void Detonate()
		{
			Vector3 position = base.transform.position;
			Util.PlaySound(FireMegaNova.novaSoundString, base.gameObject);
			if (FireMegaNova.novaEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireMegaNova.novaEffectPrefab, base.gameObject, "NovaCenter", false);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 3f;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matVagrantEnergized");
				temporaryOverlay.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
			}
			if (NetworkServer.active)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					baseDamage = this.damageStat * FireMegaNova.novaDamageCoefficient,
					baseForce = FireMegaNova.novaForce,
					bonusForce = Vector3.zero,
					attackerFiltering = AttackerFiltering.NeverHitSelf,
					crit = base.characterBody.RollCrit(),
					damageColorIndex = DamageColorIndex.Default,
					damageType = DamageType.Generic,
					falloffModel = BlastAttack.FalloffModel.None,
					inflictor = base.gameObject,
					position = position,
					procChainMask = default(ProcChainMask),
					procCoefficient = 3f,
					radius = this.novaRadius,
					losType = BlastAttack.LoSType.NearestHit,
					teamIndex = base.teamComponent.teamIndex,
					impactEffect = EffectCatalog.FindEffectIndexFromPrefab(FireMegaNova.novaImpactEffectPrefab)
				}.Fire();
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04001021 RID: 4129
		public static float baseDuration = 3f;

		// Token: 0x04001022 RID: 4130
		public static GameObject novaEffectPrefab;

		// Token: 0x04001023 RID: 4131
		public static GameObject novaImpactEffectPrefab;

		// Token: 0x04001024 RID: 4132
		public static string novaSoundString;

		// Token: 0x04001025 RID: 4133
		public static float novaDamageCoefficient;

		// Token: 0x04001026 RID: 4134
		public static float novaForce;

		// Token: 0x04001027 RID: 4135
		public float novaRadius;

		// Token: 0x04001028 RID: 4136
		private float duration;

		// Token: 0x04001029 RID: 4137
		private float stopwatch;
	}
}
