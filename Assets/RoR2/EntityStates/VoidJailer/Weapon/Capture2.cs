using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidJailer.Weapon
{
	// Token: 0x02000154 RID: 340
	public class Capture2 : BaseState
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x000197E0 File Offset: 0x000179E0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateName, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzleString, false);
			}
			Ray aimRay = base.GetAimRay();
			if (NetworkServer.active)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.teamMaskFilter = TeamMask.all;
				bullseyeSearch.maxAngleFilter = this.pullFieldOfView * 0.5f;
				bullseyeSearch.maxDistanceFilter = this.pullMaxDistance;
				bullseyeSearch.searchOrigin = aimRay.origin;
				bullseyeSearch.searchDirection = aimRay.direction;
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
				bullseyeSearch.filterByLoS = true;
				bullseyeSearch.RefreshCandidates();
				bullseyeSearch.FilterOutGameObject(base.gameObject);
				HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
				base.GetTeam();
				if (hurtBox)
				{
					Vector3 a = hurtBox.transform.position - aimRay.origin;
					float magnitude = a.magnitude;
					Vector3 vector = a / magnitude;
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
					if (this.debuffDef)
					{
						body.AddTimedBuff(this.debuffDef, this.debuffDuration);
					}
					float num2 = this.pullSuitabilityCurve.Evaluate(num);
					Vector3 a2 = vector;
					float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(this.pullMinDistance - magnitude)) * Mathf.Sign(this.pullMinDistance - magnitude);
					a2 *= d;
					a2.y = this.pullLiftVelocity;
					DamageInfo damageInfo = new DamageInfo
					{
						attacker = base.gameObject,
						damage = this.damageStat * this.damageCoefficient,
						position = hurtBox.transform.position,
						procCoefficient = this.procCoefficient
					};
					hurtBox.healthComponent.TakeDamageForce(a2 * (num * num2), true, true);
					hurtBox.healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox.healthComponent.gameObject);
					if (this.pullTracerPrefab)
					{
						Vector3 position = hurtBox.transform.position;
						Vector3 start = base.characterBody.corePosition;
						Transform transform = base.FindModelChild(this.muzzleString);
						if (transform)
						{
							start = transform.position;
						}
						EffectData effectData = new EffectData
						{
							origin = position,
							start = start
						};
						EffectManager.SpawnEffect(this.pullTracerPrefab, effectData, true);
					}
				}
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019AC0 File Offset: 0x00017CC0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new ExitCapture());
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000727 RID: 1831
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000728 RID: 1832
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000729 RID: 1833
		[SerializeField]
		public string animationPlaybackRateName;

		// Token: 0x0400072A RID: 1834
		[SerializeField]
		public float baseDuration;

		// Token: 0x0400072B RID: 1835
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400072C RID: 1836
		[SerializeField]
		public float pullFieldOfView;

		// Token: 0x0400072D RID: 1837
		[SerializeField]
		public float pullMinDistance;

		// Token: 0x0400072E RID: 1838
		[SerializeField]
		public float pullMaxDistance;

		// Token: 0x0400072F RID: 1839
		[SerializeField]
		public AnimationCurve pullSuitabilityCurve;

		// Token: 0x04000730 RID: 1840
		[SerializeField]
		public GameObject pullTracerPrefab;

		// Token: 0x04000731 RID: 1841
		[SerializeField]
		public float pullLiftVelocity;

		// Token: 0x04000732 RID: 1842
		[SerializeField]
		public BuffDef debuffDef;

		// Token: 0x04000733 RID: 1843
		[SerializeField]
		public float debuffDuration;

		// Token: 0x04000734 RID: 1844
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04000735 RID: 1845
		[SerializeField]
		public float procCoefficient;

		// Token: 0x04000736 RID: 1846
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x04000737 RID: 1847
		[SerializeField]
		public string muzzleString;

		// Token: 0x04000738 RID: 1848
		private float duration;
	}
}
