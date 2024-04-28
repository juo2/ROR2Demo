using System;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000437 RID: 1079
	public class ExitSkyLeap : BaseState
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x0005641C File Offset: 0x0005461C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ExitSkyLeap.baseDuration / this.attackSpeedStat;
			Util.PlaySound(ExitSkyLeap.soundString, base.gameObject);
			base.PlayAnimation("Body", "ExitSkyLeap", "SkyLeap.playbackRate", this.duration);
			this.PlayAnimation("FullBody Override", "BufferEmpty");
			base.characterBody.AddTimedBuff(RoR2Content.Buffs.ArmorBoost, ExitSkyLeap.baseDuration);
			AimAnimator aimAnimator = base.GetAimAnimator();
			if (aimAnimator)
			{
				aimAnimator.enabled = true;
			}
			if (base.isAuthority)
			{
				this.FireRingAuthority();
			}
			if (PhaseCounter.instance && PhaseCounter.instance.phase == 3)
			{
				if (UnityEngine.Random.value < ExitSkyLeap.recastChance)
				{
					this.recast = true;
				}
				for (int i = 0; i < ExitSkyLeap.cloneCount; i++)
				{
					DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscBrotherGlass"), new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Approximate,
						minDistance = 3f,
						maxDistance = 20f,
						spawnOnTarget = base.gameObject.transform
					}, RoR2Application.rng);
					directorSpawnRequest.summonerBodyObject = base.gameObject;
					DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
					directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult spawnResult)
					{
						spawnResult.spawnedInstance.GetComponent<Inventory>().GiveItem(RoR2Content.Items.HealthDecay, ExitSkyLeap.cloneDuration);
					}));
					DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
				}
				GenericSkill genericSkill = base.skillLocator ? base.skillLocator.special : null;
				if (genericSkill)
				{
					genericSkill.SetSkillOverride(this.outer, UltChannelState.replacementSkillDef, GenericSkill.SkillOverridePriority.Contextual);
				}
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000565CC File Offset: 0x000547CC
		private void FireRingAuthority()
		{
			float num = 360f / (float)ExitSkyLeap.waveProjectileCount;
			Vector3 point = Vector3.ProjectOnPlane(base.inputBank.aimDirection, Vector3.up);
			Vector3 footPosition = base.characterBody.footPosition;
			for (int i = 0; i < ExitSkyLeap.waveProjectileCount; i++)
			{
				Vector3 forward = Quaternion.AngleAxis(num * (float)i, Vector3.up) * point;
				if (base.isAuthority)
				{
					ProjectileManager.instance.FireProjectile(ExitSkyLeap.waveProjectilePrefab, footPosition, Util.QuaternionSafeLookRotation(forward), base.gameObject, base.characterBody.damage * ExitSkyLeap.waveProjectileDamageCoefficient, ExitSkyLeap.waveProjectileForce, Util.CheckRoll(base.characterBody.crit, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0005668C File Offset: 0x0005488C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (this.recast && base.fixedAge > ExitSkyLeap.baseDurationUntilRecastInterrupt)
				{
					this.outer.SetNextState(new EnterSkyLeap());
				}
				if (base.fixedAge > this.duration)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x040018D6 RID: 6358
		public static float baseDuration;

		// Token: 0x040018D7 RID: 6359
		public static float baseDurationUntilRecastInterrupt;

		// Token: 0x040018D8 RID: 6360
		public static string soundString;

		// Token: 0x040018D9 RID: 6361
		public static GameObject waveProjectilePrefab;

		// Token: 0x040018DA RID: 6362
		public static int waveProjectileCount;

		// Token: 0x040018DB RID: 6363
		public static float waveProjectileDamageCoefficient;

		// Token: 0x040018DC RID: 6364
		public static float waveProjectileForce;

		// Token: 0x040018DD RID: 6365
		public static float recastChance;

		// Token: 0x040018DE RID: 6366
		public static int cloneCount;

		// Token: 0x040018DF RID: 6367
		public static int cloneDuration;

		// Token: 0x040018E0 RID: 6368
		public static SkillDef replacementSkillDef;

		// Token: 0x040018E1 RID: 6369
		private float duration;

		// Token: 0x040018E2 RID: 6370
		private float durationUntilRecastInterrupt;

		// Token: 0x040018E3 RID: 6371
		private bool recast;
	}
}
