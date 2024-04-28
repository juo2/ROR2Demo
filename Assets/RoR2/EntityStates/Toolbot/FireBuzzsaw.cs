using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000195 RID: 405
	public class FireBuzzsaw : BaseToolbotPrimarySkillState
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001EA11 File Offset: 0x0001CC11
		public override string baseMuzzleName
		{
			get
			{
				return "MuzzleBuzzsaw";
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001EA18 File Offset: 0x0001CC18
		public override void OnEnter()
		{
			base.OnEnter();
			this.fireFrequency = FireBuzzsaw.baseFireFrequency * this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			Util.PlaySound(FireBuzzsaw.spinUpSoundString, base.gameObject);
			Util.PlaySound(FireBuzzsaw.fireSoundString, base.gameObject);
			if (!base.isInDualWield)
			{
				this.PlayAnimation("Gesture, Additive Gun", "SpinBuzzsaw");
				this.PlayAnimation("Gesture, Additive", "EnterBuzzsaw");
			}
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = FireBuzzsaw.damageCoefficientPerSecond * this.damageStat / FireBuzzsaw.baseFireFrequency;
			this.attack.procCoefficient = FireBuzzsaw.procCoefficientPerSecond / FireBuzzsaw.baseFireFrequency;
			if (FireBuzzsaw.impactEffectPrefab)
			{
				this.attack.hitEffectPrefab = FireBuzzsaw.impactEffectPrefab;
			}
			if (modelTransform)
			{
				string groupName = "Buzzsaw";
				if (base.isInDualWield)
				{
					if (base.currentHand == -1)
					{
						groupName = "BuzzsawL";
					}
					else if (base.currentHand == 1)
					{
						groupName = "BuzzsawR";
					}
				}
				this.attack.hitBoxGroup = HitBoxGroup.FindByGroupName(modelTransform.gameObject, groupName);
			}
			if (base.muzzleTransform)
			{
				if (FireBuzzsaw.spinEffectPrefab)
				{
					this.spinEffectInstance = UnityEngine.Object.Instantiate<GameObject>(FireBuzzsaw.spinEffectPrefab, base.muzzleTransform.position, base.muzzleTransform.rotation);
					this.spinEffectInstance.transform.parent = base.muzzleTransform;
					this.spinEffectInstance.transform.localScale = Vector3.one;
				}
				if (FireBuzzsaw.spinImpactEffectPrefab)
				{
					this.spinImpactEffectInstance = UnityEngine.Object.Instantiate<GameObject>(FireBuzzsaw.spinImpactEffectPrefab, base.muzzleTransform.position, base.muzzleTransform.rotation);
					this.spinImpactEffectInstance.transform.parent = base.muzzleTransform;
					this.spinImpactEffectInstance.transform.localScale = Vector3.one;
					this.spinImpactEffectInstance.gameObject.SetActive(false);
				}
			}
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001EC70 File Offset: 0x0001CE70
		public override void OnExit()
		{
			base.OnExit();
			Util.PlaySound(FireBuzzsaw.spinDownSoundString, base.gameObject);
			if (!base.isInDualWield)
			{
				this.PlayAnimation("Gesture, Additive Gun", "Empty");
				this.PlayAnimation("Gesture, Additive", "ExitBuzzsaw");
			}
			if (this.spinEffectInstance)
			{
				EntityState.Destroy(this.spinEffectInstance);
			}
			if (this.spinImpactEffectInstance)
			{
				EntityState.Destroy(this.spinImpactEffectInstance);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireAge += Time.fixedDeltaTime;
			base.characterBody.SetAimTimer(2f);
			this.attackSpeedStat = base.characterBody.attackSpeed;
			this.fireFrequency = FireBuzzsaw.baseFireFrequency * this.attackSpeedStat;
			if (this.fireAge >= 1f / this.fireFrequency && base.isAuthority)
			{
				this.fireAge = 0f;
				this.attack.ResetIgnoredHealthComponents();
				this.attack.isCrit = base.characterBody.RollCrit();
				this.hitOverlapLastTick = this.attack.Fire(null);
				if (this.hitOverlapLastTick)
				{
					Vector3 normalized = (this.attack.lastFireAverageHitPosition - base.GetAimRay().origin).normalized;
					if (base.characterMotor)
					{
						base.characterMotor.ApplyForce(normalized * FireBuzzsaw.selfForceMagnitude, false, false);
					}
					Util.PlaySound(FireBuzzsaw.impactSoundString, base.gameObject);
					if (!base.isInDualWield)
					{
						this.PlayAnimation("Gesture, Additive", "ImpactBuzzsaw");
					}
				}
				base.characterBody.AddSpreadBloom(FireBuzzsaw.spreadBloomValue);
				if (!base.IsKeyDownAuthority() || base.skillDef != base.activatorSkillSlot.skillDef)
				{
					this.outer.SetNextStateToMain();
				}
			}
			this.spinImpactEffectInstance.gameObject.SetActive(this.hitOverlapLastTick);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040008B3 RID: 2227
		public static float damageCoefficientPerSecond;

		// Token: 0x040008B4 RID: 2228
		public static float procCoefficientPerSecond = 1f;

		// Token: 0x040008B5 RID: 2229
		public static string fireSoundString;

		// Token: 0x040008B6 RID: 2230
		public static string impactSoundString;

		// Token: 0x040008B7 RID: 2231
		public static string spinUpSoundString;

		// Token: 0x040008B8 RID: 2232
		public static string spinDownSoundString;

		// Token: 0x040008B9 RID: 2233
		public static float spreadBloomValue = 0.2f;

		// Token: 0x040008BA RID: 2234
		public static float baseFireFrequency;

		// Token: 0x040008BB RID: 2235
		public static GameObject spinEffectPrefab;

		// Token: 0x040008BC RID: 2236
		public static GameObject spinImpactEffectPrefab;

		// Token: 0x040008BD RID: 2237
		public static GameObject impactEffectPrefab;

		// Token: 0x040008BE RID: 2238
		public static float selfForceMagnitude;

		// Token: 0x040008BF RID: 2239
		private OverlapAttack attack;

		// Token: 0x040008C0 RID: 2240
		private float fireFrequency;

		// Token: 0x040008C1 RID: 2241
		private float fireAge;

		// Token: 0x040008C2 RID: 2242
		private GameObject spinEffectInstance;

		// Token: 0x040008C3 RID: 2243
		private GameObject spinImpactEffectInstance;

		// Token: 0x040008C4 RID: 2244
		private bool hitOverlapLastTick;
	}
}
