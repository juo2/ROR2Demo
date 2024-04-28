using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000356 RID: 854
	public class SpawnState : GenericCharacterSpawnState
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x0004298C File Offset: 0x00040B8C
		public override void OnEnter()
		{
			base.OnEnter();
			this.characterModel = base.modelLocator.modelTransform.GetComponent<CharacterModel>();
			this.hurtboxGroup = base.modelLocator.modelTransform.GetComponent<HurtBoxGroup>();
			this.ToggleInvisibility(true);
			if (SpawnState.preSpawnEffect)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.preSpawnEffect, base.gameObject, SpawnState.preSpawnEffectMuzzle, false);
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x000429F4 File Offset: 0x00040BF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.preSpawnIsDone && base.fixedAge > SpawnState.preSpawnDuration)
			{
				this.preSpawnIsDone = true;
				this.ToggleInvisibility(false);
				base.PlayAnimation("Body", "Spawn1", "Spawn1.playbackRate", this.duration - SpawnState.preSpawnDuration);
				if (SpawnState.spawnEffect)
				{
					EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffect, base.gameObject, SpawnState.spawnEffectMuzzle, false);
				}
				if (base.isAuthority)
				{
					BlastAttack blastAttack = new BlastAttack();
					blastAttack.attacker = base.gameObject;
					blastAttack.inflictor = base.gameObject;
					blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
					blastAttack.position = base.characterBody.corePosition;
					blastAttack.procCoefficient = 1f;
					blastAttack.radius = SpawnState.blastAttackRadius;
					blastAttack.baseForce = SpawnState.blastAttackForce;
					blastAttack.bonusForce = Vector3.up * SpawnState.blastAttackBonusForce;
					blastAttack.baseDamage = 0f;
					blastAttack.falloffModel = BlastAttack.FalloffModel.Linear;
					blastAttack.damageColorIndex = DamageColorIndex.Item;
					blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
					blastAttack.Fire();
				}
				if (this.characterModel && SpawnState.spawnOverlayMaterial)
				{
					TemporaryOverlay temporaryOverlay = this.characterModel.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = SpawnState.spawnOverlayDuration;
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = SpawnState.spawnOverlayMaterial;
					temporaryOverlay.inspectorCharacterModel = this.characterModel;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.animateShaderAlpha = true;
				}
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00042B90 File Offset: 0x00040D90
		private void ToggleInvisibility(bool newInvisible)
		{
			if (this.isInvisible == newInvisible)
			{
				return;
			}
			this.isInvisible = newInvisible;
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount += (this.isInvisible ? 1 : -1);
			}
			if (this.hurtboxGroup)
			{
				this.hurtboxGroup.hurtBoxesDeactivatorCounter += (this.isInvisible ? 1 : -1);
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00042C04 File Offset: 0x00040E04
		public override void OnExit()
		{
			this.ToggleInvisibility(false);
			base.OnExit();
		}

		// Token: 0x04001353 RID: 4947
		public static GameObject preSpawnEffect;

		// Token: 0x04001354 RID: 4948
		public static string preSpawnEffectMuzzle;

		// Token: 0x04001355 RID: 4949
		public static float preSpawnDuration;

		// Token: 0x04001356 RID: 4950
		public static GameObject spawnEffect;

		// Token: 0x04001357 RID: 4951
		public static string spawnEffectMuzzle;

		// Token: 0x04001358 RID: 4952
		public static Material spawnOverlayMaterial;

		// Token: 0x04001359 RID: 4953
		public static float spawnOverlayDuration;

		// Token: 0x0400135A RID: 4954
		public static float blastAttackRadius;

		// Token: 0x0400135B RID: 4955
		public static float blastAttackForce;

		// Token: 0x0400135C RID: 4956
		public static float blastAttackBonusForce;

		// Token: 0x0400135D RID: 4957
		private bool hasSpawned;

		// Token: 0x0400135E RID: 4958
		private CharacterModel characterModel;

		// Token: 0x0400135F RID: 4959
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04001360 RID: 4960
		private bool preSpawnIsDone;

		// Token: 0x04001361 RID: 4961
		private bool isInvisible;
	}
}
