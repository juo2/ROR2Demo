using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x02000277 RID: 631
	public class Evis : BaseState
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0002D864 File Offset: 0x0002BA64
		public override void OnEnter()
		{
			base.OnEnter();
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
			Util.PlayAttackSpeedSound(Evis.beginSoundString, base.gameObject, 1.2f);
			this.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
			}
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount++;
			}
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002D94C File Offset: 0x0002BB4C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.attackStopwatch += Time.fixedDeltaTime;
			float num = 1f / Evis.damageFrequency / this.attackSpeedStat;
			if (this.attackStopwatch >= num)
			{
				this.attackStopwatch -= num;
				HurtBox hurtBox = this.SearchForTarget();
				if (hurtBox)
				{
					Util.PlayAttackSpeedSound(Evis.slashSoundString, base.gameObject, Evis.slashPitch);
					Util.PlaySound(Evis.dashSoundString, base.gameObject);
					Util.PlaySound(Evis.impactSoundString, base.gameObject);
					HurtBoxGroup hurtBoxGroup = hurtBox.hurtBoxGroup;
					HurtBox hurtBox2 = hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, hurtBoxGroup.hurtBoxes.Length - 1)];
					if (hurtBox2)
					{
						Vector3 position = hurtBox2.transform.position;
						Vector2 normalized = UnityEngine.Random.insideUnitCircle.normalized;
						Vector3 normal = new Vector3(normalized.x, 0f, normalized.y);
						EffectManager.SimpleImpactEffect(Evis.hitEffectPrefab, position, normal, false);
						Transform transform = hurtBox.hurtBoxGroup.transform;
						TemporaryOverlay temporaryOverlay = transform.gameObject.AddComponent<TemporaryOverlay>();
						temporaryOverlay.duration = num;
						temporaryOverlay.animateShaderAlpha = true;
						temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
						temporaryOverlay.destroyComponentOnEnd = true;
						temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matMercEvisTarget");
						temporaryOverlay.AddToCharacerModel(transform.GetComponent<CharacterModel>());
						if (NetworkServer.active)
						{
							DamageInfo damageInfo = new DamageInfo();
							damageInfo.damage = Evis.damageCoefficient * this.damageStat;
							damageInfo.attacker = base.gameObject;
							damageInfo.procCoefficient = Evis.procCoefficient;
							damageInfo.position = hurtBox2.transform.position;
							damageInfo.crit = this.crit;
							hurtBox2.healthComponent.TakeDamage(damageInfo);
							GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox2.healthComponent.gameObject);
							GlobalEventManager.instance.OnHitAll(damageInfo, hurtBox2.healthComponent.gameObject);
						}
					}
				}
				else if (base.isAuthority && this.stopwatch > Evis.minimumDuration)
				{
					this.outer.SetNextStateToMain();
				}
			}
			if (base.characterMotor)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			if (this.stopwatch >= Evis.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		private HurtBox SearchForTarget()
		{
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = base.transform.position;
			bullseyeSearch.searchDirection = UnityEngine.Random.onUnitSphere;
			bullseyeSearch.maxDistanceFilter = Evis.maxRadius;
			bullseyeSearch.teamMaskFilter = TeamMask.GetUnprotectedTeams(base.GetTeam());
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.RefreshCandidates();
			bullseyeSearch.FilterOutGameObject(base.gameObject);
			return bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002DC3C File Offset: 0x0002BE3C
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(Vector3.up);
			effectData.origin = origin;
			EffectManager.SpawnEffect(Evis.blinkPrefab, effectData, false);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002DC74 File Offset: 0x0002BE74
		public override void OnExit()
		{
			Util.PlaySound(Evis.endSoundString, base.gameObject);
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 0.6f;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matMercEvisTarget");
				temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
				TemporaryOverlay temporaryOverlay2 = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay2.duration = 0.7f;
				temporaryOverlay2.animateShaderAlpha = true;
				temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay2.destroyComponentOnEnd = true;
				temporaryOverlay2.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
				temporaryOverlay2.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
			}
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount--;
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, Evis.lingeringInvincibilityDuration);
			}
			Util.PlaySound(Evis.endSoundString, base.gameObject);
			base.SmallHop(base.characterMotor, Evis.smallHopVelocity);
			base.OnExit();
		}

		// Token: 0x04000CBB RID: 3259
		private Transform modelTransform;

		// Token: 0x04000CBC RID: 3260
		public static GameObject blinkPrefab;

		// Token: 0x04000CBD RID: 3261
		public static float duration = 2f;

		// Token: 0x04000CBE RID: 3262
		public static float damageCoefficient;

		// Token: 0x04000CBF RID: 3263
		public static float damageFrequency;

		// Token: 0x04000CC0 RID: 3264
		public static float procCoefficient;

		// Token: 0x04000CC1 RID: 3265
		public static string beginSoundString;

		// Token: 0x04000CC2 RID: 3266
		public static string endSoundString;

		// Token: 0x04000CC3 RID: 3267
		public static float maxRadius;

		// Token: 0x04000CC4 RID: 3268
		public static GameObject hitEffectPrefab;

		// Token: 0x04000CC5 RID: 3269
		public static string slashSoundString;

		// Token: 0x04000CC6 RID: 3270
		public static string impactSoundString;

		// Token: 0x04000CC7 RID: 3271
		public static string dashSoundString;

		// Token: 0x04000CC8 RID: 3272
		public static float slashPitch;

		// Token: 0x04000CC9 RID: 3273
		public static float smallHopVelocity;

		// Token: 0x04000CCA RID: 3274
		public static float lingeringInvincibilityDuration;

		// Token: 0x04000CCB RID: 3275
		private Animator animator;

		// Token: 0x04000CCC RID: 3276
		private CharacterModel characterModel;

		// Token: 0x04000CCD RID: 3277
		private float stopwatch;

		// Token: 0x04000CCE RID: 3278
		private float attackStopwatch;

		// Token: 0x04000CCF RID: 3279
		private bool crit;

		// Token: 0x04000CD0 RID: 3280
		private static float minimumDuration = 0.5f;

		// Token: 0x04000CD1 RID: 3281
		private CameraTargetParams.AimRequest aimRequest;
	}
}
