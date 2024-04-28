using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000185 RID: 389
	public class FireSonicBoom : BaseState
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.PlayAnimation("Gesture, Additive", "FireSonicBoom");
			Util.PlaySound(this.sound, base.gameObject);
			Ray aimRay = base.GetAimRay();
			if (!string.IsNullOrEmpty(this.muzzle))
			{
				EffectManager.SimpleMuzzleFlash(this.fireEffectPrefab, base.gameObject, this.muzzle, false);
			}
			else
			{
				EffectManager.SpawnEffect(this.fireEffectPrefab, new EffectData
				{
					origin = aimRay.origin,
					rotation = Quaternion.LookRotation(aimRay.direction)
				}, false);
			}
			aimRay.origin -= aimRay.direction * this.backupDistance;
			if (NetworkServer.active)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.teamMaskFilter = TeamMask.all;
				bullseyeSearch.maxAngleFilter = this.fieldOfView * 0.5f;
				bullseyeSearch.maxDistanceFilter = this.maxDistance;
				bullseyeSearch.searchOrigin = aimRay.origin;
				bullseyeSearch.searchDirection = aimRay.direction;
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
				bullseyeSearch.filterByLoS = false;
				bullseyeSearch.RefreshCandidates();
				bullseyeSearch.FilterOutGameObject(base.gameObject);
				IEnumerable<HurtBox> enumerable = bullseyeSearch.GetResults().Where(new Func<HurtBox, bool>(Util.IsValid)).Distinct(default(HurtBox.EntityEqualityComparer));
				TeamIndex team = base.GetTeam();
				foreach (HurtBox hurtBox in enumerable)
				{
					if (FriendlyFireManager.ShouldSplashHitProceed(hurtBox.healthComponent, team))
					{
						Vector3 vector = hurtBox.transform.position - aimRay.origin;
						float magnitude = vector.magnitude;
						float magnitude2 = new Vector2(vector.x, vector.z).magnitude;
						Vector3 vector2 = vector / magnitude;
						float num = 1f;
						CharacterBody body = hurtBox.healthComponent.body;
						if (body.characterMotor)
						{
							num = body.characterMotor.mass;
						}
						else if (hurtBox.healthComponent.GetComponent<Rigidbody>())
						{
							num = base.rigidbody.mass;
						}
						float num2 = FireSonicBoom.shoveSuitabilityCurve.Evaluate(num);
						this.AddDebuff(body);
						body.RecalculateStats();
						float acceleration = body.acceleration;
						Vector3 a = vector2;
						float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(this.idealDistanceToPlaceTargets - magnitude), -acceleration) * Mathf.Sign(this.idealDistanceToPlaceTargets - magnitude);
						a *= d;
						a.y = this.liftVelocity;
						DamageInfo damageInfo = new DamageInfo
						{
							attacker = base.gameObject,
							damage = this.CalculateDamage(),
							position = hurtBox.transform.position,
							procCoefficient = this.CalculateProcCoefficient()
						};
						hurtBox.healthComponent.TakeDamageForce(a * (num * num2), true, true);
						hurtBox.healthComponent.TakeDamage(new DamageInfo
						{
							attacker = base.gameObject,
							damage = this.CalculateDamage(),
							position = hurtBox.transform.position,
							procCoefficient = this.CalculateProcCoefficient()
						});
						GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox.healthComponent.gameObject);
					}
				}
			}
			if (base.isAuthority && base.characterBody && base.characterBody.characterMotor)
			{
				float height = base.characterBody.characterMotor.isGrounded ? this.groundKnockbackDistance : this.airKnockbackDistance;
				float num3 = base.characterBody.characterMotor ? base.characterBody.characterMotor.mass : 1f;
				float acceleration2 = base.characterBody.acceleration;
				float num4 = Trajectory.CalculateInitialYSpeedForHeight(height, -acceleration2);
				base.characterBody.characterMotor.ApplyForce(-num4 * num3 * aimRay.direction, false, false);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		protected virtual void AddDebuff(CharacterBody body)
		{
			body.AddTimedBuff(RoR2Content.Buffs.Weak, this.slowDuration);
			SetStateOnHurt component = body.healthComponent.GetComponent<SetStateOnHurt>();
			if (component == null)
			{
				return;
			}
			component.SetStun(-1f);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float CalculateDamage()
		{
			return 0f;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float CalculateProcCoefficient()
		{
			return 0f;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001D901 File Offset: 0x0001BB01
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400085D RID: 2141
		[SerializeField]
		public string sound;

		// Token: 0x0400085E RID: 2142
		[SerializeField]
		public string muzzle;

		// Token: 0x0400085F RID: 2143
		[SerializeField]
		public GameObject fireEffectPrefab;

		// Token: 0x04000860 RID: 2144
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000861 RID: 2145
		[SerializeField]
		public float fieldOfView;

		// Token: 0x04000862 RID: 2146
		[SerializeField]
		public float backupDistance;

		// Token: 0x04000863 RID: 2147
		[SerializeField]
		public float maxDistance;

		// Token: 0x04000864 RID: 2148
		[SerializeField]
		public float idealDistanceToPlaceTargets;

		// Token: 0x04000865 RID: 2149
		[SerializeField]
		public float liftVelocity;

		// Token: 0x04000866 RID: 2150
		[SerializeField]
		public float slowDuration;

		// Token: 0x04000867 RID: 2151
		[SerializeField]
		public float groundKnockbackDistance;

		// Token: 0x04000868 RID: 2152
		[SerializeField]
		public float airKnockbackDistance;

		// Token: 0x04000869 RID: 2153
		public static AnimationCurve shoveSuitabilityCurve;

		// Token: 0x0400086A RID: 2154
		private float duration;
	}
}
