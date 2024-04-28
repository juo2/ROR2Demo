using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000134 RID: 308
	public class FireGravityBump : BaseGravityBumpState
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x000177C0 File Offset: 0x000159C0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			if (this.nextSkillDef)
			{
				GenericSkill genericSkill = base.skillLocator.FindSkillByDef(this.skillDefToReplaceAtStocksEmpty);
				if (genericSkill && genericSkill.stock == 0)
				{
					genericSkill.SetBaseSkill(this.nextSkillDef);
				}
			}
			if (this.isLeft)
			{
				base.PlayAnimation(this.leftAnimationLayerName, this.leftAnimationStateName, this.leftAnimationPlaybackRateParam, this.duration);
			}
			else
			{
				base.PlayAnimation(this.rightAnimationLayerName, this.rightAnimationStateName, this.rightAnimationPlaybackRateParam, this.duration);
			}
			if (this.muzzleFlashPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
			}
			if (NetworkServer.active)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.viewer = base.characterBody;
				bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.characterBody.teamComponent.teamIndex);
				bullseyeSearch.minDistanceFilter = 0f;
				bullseyeSearch.maxDistanceFilter = this.maxDistance;
				bullseyeSearch.searchOrigin = base.inputBank.aimOrigin;
				bullseyeSearch.searchDirection = base.inputBank.aimDirection;
				bullseyeSearch.maxAngleFilter = 360f;
				bullseyeSearch.filterByLoS = false;
				bullseyeSearch.filterByDistinctEntity = true;
				bullseyeSearch.RefreshCandidates();
				foreach (HurtBox hurtBox in bullseyeSearch.GetResults())
				{
					GameObject gameObject = hurtBox.healthComponent.gameObject;
					if (gameObject)
					{
						CharacterMotor component = gameObject.GetComponent<CharacterMotor>();
						if (component)
						{
							EffectData effectData = new EffectData
							{
								origin = gameObject.transform.position
							};
							GameObject effectPrefab;
							if (component.isGrounded)
							{
								component.ApplyForce(this.groundedForce, true, this.disableAirControlUntilCollision);
								effectPrefab = this.groundedEffectPrefab;
								effectData.rotation = Util.QuaternionSafeLookRotation(this.groundedForce);
							}
							else
							{
								component.ApplyForce(this.airborneForce, true, this.disableAirControlUntilCollision);
								effectPrefab = this.airborneEffectPrefab;
								effectData.rotation = Util.QuaternionSafeLookRotation(this.airborneForce);
							}
							EffectManager.SpawnEffect(effectPrefab, effectData, true);
						}
					}
				}
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00017A08 File Offset: 0x00015C08
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000675 RID: 1653
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000676 RID: 1654
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000677 RID: 1655
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x04000678 RID: 1656
		[SerializeField]
		public bool disableAirControlUntilCollision;

		// Token: 0x04000679 RID: 1657
		[SerializeField]
		public GameObject airborneEffectPrefab;

		// Token: 0x0400067A RID: 1658
		[SerializeField]
		public GameObject groundedEffectPrefab;

		// Token: 0x0400067B RID: 1659
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400067C RID: 1660
		[SerializeField]
		public bool isSoundScaledByAttackSpeed;

		// Token: 0x0400067D RID: 1661
		[SerializeField]
		public string leftAnimationLayerName;

		// Token: 0x0400067E RID: 1662
		[SerializeField]
		public string leftAnimationStateName;

		// Token: 0x0400067F RID: 1663
		[SerializeField]
		public string leftAnimationPlaybackRateParam;

		// Token: 0x04000680 RID: 1664
		[SerializeField]
		public string rightAnimationLayerName;

		// Token: 0x04000681 RID: 1665
		[SerializeField]
		public string rightAnimationStateName;

		// Token: 0x04000682 RID: 1666
		[SerializeField]
		public string rightAnimationPlaybackRateParam;

		// Token: 0x04000683 RID: 1667
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x04000684 RID: 1668
		[SerializeField]
		public SkillDef nextSkillDef;

		// Token: 0x04000685 RID: 1669
		private float duration;
	}
}
