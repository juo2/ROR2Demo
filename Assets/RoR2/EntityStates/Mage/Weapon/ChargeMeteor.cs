using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x02000298 RID: 664
	public class ChargeMeteor : BaseState
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x00030C32 File Offset: 0x0002EE32
		public override void OnEnter()
		{
			base.OnEnter();
			this.chargeDuration = ChargeMeteor.baseChargeDuration / this.attackSpeedStat;
			this.duration = ChargeMeteor.baseDuration / this.attackSpeedStat;
			this.UpdateAreaIndicator();
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00030C64 File Offset: 0x0002EE64
		private void UpdateAreaIndicator()
		{
			if (this.areaIndicatorInstance)
			{
				float maxDistance = 1000f;
				RaycastHit raycastHit;
				if (Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.world.mask))
				{
					this.areaIndicatorInstance.transform.position = raycastHit.point;
					this.areaIndicatorInstance.transform.up = raycastHit.normal;
				}
			}
			else
			{
				this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMeteor.areaIndicatorPrefab);
			}
			this.radius = Util.Remap(Mathf.Clamp01(this.stopwatch / this.chargeDuration), 0f, 1f, ChargeMeteor.minMeteorRadius, ChargeMeteor.maxMeteorRadius);
			this.areaIndicatorInstance.transform.localScale = new Vector3(this.radius, this.radius, this.radius);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00030D3E File Offset: 0x0002EF3E
		public override void Update()
		{
			base.Update();
			this.UpdateAreaIndicator();
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00030D4C File Offset: 0x0002EF4C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if ((this.stopwatch >= this.duration || base.inputBank.skill2.justReleased) && base.isAuthority)
			{
				this.fireMeteor = true;
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00030DAC File Offset: 0x0002EFAC
		public override void OnExit()
		{
			EffectManager.SimpleMuzzleFlash(ChargeMeteor.muzzleflashEffect, base.gameObject, "Muzzle", false);
			if (this.areaIndicatorInstance)
			{
				if (this.fireMeteor)
				{
					float num = Util.Remap(Mathf.Clamp01(this.stopwatch / this.chargeDuration), 0f, 1f, ChargeMeteor.minDamageCoefficient, ChargeMeteor.maxDamageCoefficient);
					EffectManager.SpawnEffect(ChargeMeteor.meteorEffect, new EffectData
					{
						origin = this.areaIndicatorInstance.transform.position,
						scale = this.radius
					}, true);
					BlastAttack blastAttack = new BlastAttack();
					blastAttack.radius = this.radius;
					blastAttack.procCoefficient = ChargeMeteor.procCoefficient;
					blastAttack.position = this.areaIndicatorInstance.transform.position;
					blastAttack.attacker = base.gameObject;
					blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
					blastAttack.baseDamage = base.characterBody.damage * num;
					blastAttack.falloffModel = BlastAttack.FalloffModel.SweetSpot;
					blastAttack.baseForce = ChargeMeteor.force;
					blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
					blastAttack.Fire();
				}
				EntityState.Destroy(this.areaIndicatorInstance.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x04000DE6 RID: 3558
		public static float baseChargeDuration;

		// Token: 0x04000DE7 RID: 3559
		public static float baseDuration;

		// Token: 0x04000DE8 RID: 3560
		public static GameObject areaIndicatorPrefab;

		// Token: 0x04000DE9 RID: 3561
		public static float minMeteorRadius = 0f;

		// Token: 0x04000DEA RID: 3562
		public static float maxMeteorRadius = 10f;

		// Token: 0x04000DEB RID: 3563
		public static GameObject meteorEffect;

		// Token: 0x04000DEC RID: 3564
		public static float minDamageCoefficient;

		// Token: 0x04000DED RID: 3565
		public static float maxDamageCoefficient;

		// Token: 0x04000DEE RID: 3566
		public static float procCoefficient;

		// Token: 0x04000DEF RID: 3567
		public static float force;

		// Token: 0x04000DF0 RID: 3568
		public static GameObject muzzleflashEffect;

		// Token: 0x04000DF1 RID: 3569
		private float stopwatch;

		// Token: 0x04000DF2 RID: 3570
		private GameObject areaIndicatorInstance;

		// Token: 0x04000DF3 RID: 3571
		private bool fireMeteor;

		// Token: 0x04000DF4 RID: 3572
		private float radius;

		// Token: 0x04000DF5 RID: 3573
		private float chargeDuration;

		// Token: 0x04000DF6 RID: 3574
		private float duration;
	}
}
