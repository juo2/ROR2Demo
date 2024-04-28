using System;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000135 RID: 309
	public class FireMissiles : BaseState
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00017A34 File Offset: 0x00015C34
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.nextSkillDef)
			{
				GenericSkill genericSkill = base.skillLocator.FindSkillByDef(this.skillDefToReplaceAtStocksEmpty);
				if (genericSkill && genericSkill.stock == 0)
				{
					genericSkill.SetBaseSkill(this.nextSkillDef);
				}
			}
			float num = this.baseInitialDelay + Mathf.Max(0f, this.baseDelayBetweenWaves * (float)(this.numWaves - 1)) + this.baseEndDelay;
			this.duration = num / this.attackSpeedStat;
			this.timeUntilNextWave = this.baseInitialDelay / this.attackSpeedStat;
			this.delayBetweenWaves = this.baseDelayBetweenWaves / this.attackSpeedStat;
			this.muzzleTransform = base.FindModelChild(this.muzzleName);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017AF4 File Offset: 0x00015CF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.timeUntilNextWave -= Time.fixedDeltaTime;
			while (this.timeUntilNextWave < 0f && this.numWavesFired < this.numWaves)
			{
				base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
				this.timeUntilNextWave += this.delayBetweenWaves;
				this.numWavesFired++;
				EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
				if (base.isAuthority)
				{
					Quaternion lhs = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);
					FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
					{
						projectilePrefab = this.projectilePrefab,
						position = this.muzzleTransform.position,
						owner = base.gameObject,
						damage = this.damageStat * this.damageCoefficient,
						force = this.force
					};
					for (int i = 0; i < this.numMissilesPerWave; i++)
					{
						fireProjectileInfo.rotation = lhs * this.GetRandomRollPitch();
						fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
						ProjectileManager.instance.FireProjectile(fireProjectileInfo);
					}
				}
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00017C7C File Offset: 0x00015E7C
		protected Quaternion GetRandomRollPitch()
		{
			Quaternion lhs = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.forward);
			Quaternion rhs = Quaternion.AngleAxis(this.minSpreadDegrees + UnityEngine.Random.Range(0f, this.rangeSpreadDegrees), Vector3.left);
			return lhs * rhs;
		}

		// Token: 0x04000686 RID: 1670
		[SerializeField]
		public float baseInitialDelay;

		// Token: 0x04000687 RID: 1671
		[SerializeField]
		public float baseDelayBetweenWaves;

		// Token: 0x04000688 RID: 1672
		[SerializeField]
		public float baseEndDelay;

		// Token: 0x04000689 RID: 1673
		[SerializeField]
		public int numWaves;

		// Token: 0x0400068A RID: 1674
		[SerializeField]
		public int numMissilesPerWave;

		// Token: 0x0400068B RID: 1675
		[SerializeField]
		public string muzzleName;

		// Token: 0x0400068C RID: 1676
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x0400068D RID: 1677
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x0400068E RID: 1678
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x0400068F RID: 1679
		[SerializeField]
		public float force;

		// Token: 0x04000690 RID: 1680
		[SerializeField]
		public float minSpreadDegrees;

		// Token: 0x04000691 RID: 1681
		[SerializeField]
		public float rangeSpreadDegrees;

		// Token: 0x04000692 RID: 1682
		[SerializeField]
		public string fireWaveSoundString;

		// Token: 0x04000693 RID: 1683
		[SerializeField]
		public bool isSoundScaledByAttackSpeed;

		// Token: 0x04000694 RID: 1684
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000695 RID: 1685
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000696 RID: 1686
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000697 RID: 1687
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x04000698 RID: 1688
		[SerializeField]
		public SkillDef nextSkillDef;

		// Token: 0x04000699 RID: 1689
		private float delayBetweenWaves;

		// Token: 0x0400069A RID: 1690
		private float duration;

		// Token: 0x0400069B RID: 1691
		private int numWavesFired;

		// Token: 0x0400069C RID: 1692
		private float timeUntilNextWave;

		// Token: 0x0400069D RID: 1693
		private Transform muzzleTransform;
	}
}
