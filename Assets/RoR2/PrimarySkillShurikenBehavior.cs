using System;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000784 RID: 1924
	public class PrimarySkillShurikenBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002846 RID: 10310 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000AECCB File Offset: 0x000ACECB
		private void Start()
		{
			this.projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/ShurikenProjectile");
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000AECE0 File Offset: 0x000ACEE0
		private void OnEnable()
		{
			if (this.body)
			{
				this.body.onSkillActivatedServer += this.OnSkillActivated;
				this.skillLocator = this.body.GetComponent<SkillLocator>();
				this.inputBank = this.body.GetComponent<InputBankTest>();
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000AED34 File Offset: 0x000ACF34
		private void OnDisable()
		{
			if (this.body)
			{
				this.body.onSkillActivatedServer -= this.OnSkillActivated;
				while (this.body.HasBuff(DLC1Content.Buffs.PrimarySkillShurikenBuff))
				{
					this.body.RemoveBuff(DLC1Content.Buffs.PrimarySkillShurikenBuff);
				}
			}
			this.inputBank = null;
			this.skillLocator = null;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000AED97 File Offset: 0x000ACF97
		private void OnSkillActivated(GenericSkill skill)
		{
			SkillLocator skillLocator = this.skillLocator;
			if (((skillLocator != null) ? skillLocator.primary : null) == skill && this.body.GetBuffCount(DLC1Content.Buffs.PrimarySkillShurikenBuff) > 0)
			{
				this.body.RemoveBuff(DLC1Content.Buffs.PrimarySkillShurikenBuff);
				this.FireShuriken();
			}
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000AEDD8 File Offset: 0x000ACFD8
		private void FixedUpdate()
		{
			int num = this.stack + 2;
			if (this.body.GetBuffCount(DLC1Content.Buffs.PrimarySkillShurikenBuff) < num)
			{
				float num2 = 10f / (float)num;
				this.reloadTimer += Time.fixedDeltaTime;
				while (this.reloadTimer > num2 && this.body.GetBuffCount(DLC1Content.Buffs.PrimarySkillShurikenBuff) < num)
				{
					this.body.AddBuff(DLC1Content.Buffs.PrimarySkillShurikenBuff);
					this.reloadTimer -= num2;
				}
			}
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000AEE58 File Offset: 0x000AD058
		private void FireShuriken()
		{
			Ray aimRay = this.GetAimRay();
			ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction) * this.GetRandomRollPitch(), base.gameObject, this.body.damage * (3f + 1f * (float)this.stack), 0f, Util.CheckRoll(this.body.crit, this.body.master), DamageColorIndex.Item, null, -1f);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000AEEE8 File Offset: 0x000AD0E8
		private Ray GetAimRay()
		{
			if (this.inputBank)
			{
				return new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
			}
			return new Ray(base.transform.position, base.transform.forward);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000AEF3C File Offset: 0x000AD13C
		protected Quaternion GetRandomRollPitch()
		{
			Quaternion lhs = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.forward);
			Quaternion rhs = Quaternion.AngleAxis(0f + UnityEngine.Random.Range(0f, 1f), Vector3.left);
			return lhs * rhs;
		}

		// Token: 0x04002BED RID: 11245
		private const float minSpreadDegrees = 0f;

		// Token: 0x04002BEE RID: 11246
		private const float rangeSpreadDegrees = 1f;

		// Token: 0x04002BEF RID: 11247
		private const int numShurikensPerStack = 1;

		// Token: 0x04002BF0 RID: 11248
		private const int numShurikensBase = 2;

		// Token: 0x04002BF1 RID: 11249
		private const string projectilePrefabPath = "Prefabs/Projectiles/ShurikenProjectile";

		// Token: 0x04002BF2 RID: 11250
		private const float totalReloadTime = 10f;

		// Token: 0x04002BF3 RID: 11251
		private const float damageCoefficientBase = 3f;

		// Token: 0x04002BF4 RID: 11252
		private const float damageCoefficientPerStack = 1f;

		// Token: 0x04002BF5 RID: 11253
		private const float force = 0f;

		// Token: 0x04002BF6 RID: 11254
		private SkillLocator skillLocator;

		// Token: 0x04002BF7 RID: 11255
		private float reloadTimer;

		// Token: 0x04002BF8 RID: 11256
		private GameObject projectilePrefab;

		// Token: 0x04002BF9 RID: 11257
		private InputBankTest inputBank;
	}
}
