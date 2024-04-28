using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000108 RID: 264
	public class Suppress : BaseSkillState
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x00013D78 File Offset: 0x00011F78
		public override void OnEnter()
		{
			base.OnEnter();
			this.targetBody = null;
			this.voidSurvivorController = base.GetComponent<VoidSurvivorController>();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.muzzleTransform = component.FindChild(this.muzzle);
					if (this.muzzleTransform && this.suppressEffectPrefab)
					{
						this.suppressEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.suppressEffectPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation);
						this.suppressEffectInstance.transform.parent = base.characterBody.transform;
						ChildLocator component2 = this.suppressEffectInstance.GetComponent<ChildLocator>();
						if (component2)
						{
							this.idealFXTransform = component2.FindChild("IdealFX");
							this.targetFXTransform = component2.FindChild("TargetFX");
						}
					}
				}
			}
			Ray aimRay = base.GetAimRay();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
			bullseyeSearch.maxAngleFilter = this.maxSearchAngleFilter;
			bullseyeSearch.maxDistanceFilter = this.maxSearchDistance;
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
			bullseyeSearch.filterByLoS = true;
			bullseyeSearch.RefreshCandidates();
			bullseyeSearch.FilterOutGameObject(base.gameObject);
			HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox)
			{
				Debug.LogFormat("Found target {0}", new object[]
				{
					this.targetBody
				});
				this.targetBody = hurtBox.healthComponent.body;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00013F2B File Offset: 0x0001212B
		public override void OnExit()
		{
			if (this.suppressEffectInstance)
			{
				EntityState.Destroy(this.suppressEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00013F4C File Offset: 0x0001214C
		public override void Update()
		{
			base.Update();
			if (this.targetBody)
			{
				if (this.muzzleTransform)
				{
					this.suppressEffectInstance.transform.position = this.muzzleTransform.position;
				}
				if (this.idealFXTransform)
				{
					Ray aimRay = base.GetAimRay();
					Vector3 corePosition = this.targetBody.corePosition;
					Vector3 position = aimRay.origin + aimRay.direction * this.idealDistance;
					this.idealFXTransform.position = position;
				}
				if (this.targetFXTransform)
				{
					this.targetFXTransform.position = this.targetBody.corePosition;
				}
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00014008 File Offset: 0x00012208
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(3f);
			if (NetworkServer.active && this.targetBody)
			{
				Ray aimRay = base.GetAimRay();
				Vector3 corePosition = this.targetBody.corePosition;
				Vector3 a = aimRay.origin + aimRay.direction * this.idealDistance;
				if (this.applyForces)
				{
					Vector3 vector = a - corePosition;
					float magnitude = vector.magnitude;
					Mathf.Clamp01(magnitude / this.suppressedTargetForceRadius);
					Vector3 velocity;
					float mass;
					bool flag;
					if (this.targetBody.characterMotor)
					{
						velocity = this.targetBody.characterMotor.velocity;
						mass = this.targetBody.characterMotor.mass;
						flag = !this.targetBody.characterMotor.isFlying;
					}
					else
					{
						Rigidbody rigidbody = this.targetBody.rigidbody;
						velocity = rigidbody.velocity;
						mass = rigidbody.mass;
						flag = rigidbody.useGravity;
					}
					Vector3 a2 = vector.normalized * Mathf.Min(this.springMaxLength, magnitude) * this.springConstant * Time.fixedDeltaTime;
					Vector3 b = -velocity * this.damperConstant * Time.fixedDeltaTime;
					Vector3 b2 = flag ? (Physics.gravity * Time.fixedDeltaTime * mass) : Vector3.zero;
					float d = this.forceSuitabilityCurve.Evaluate(mass);
					this.targetBody.healthComponent.TakeDamageForce((a2 + b) * d - b2, true, true);
				}
				this.damageTickCountdown -= Time.fixedDeltaTime;
				if (this.damageTickCountdown <= 0f)
				{
					this.damageTickCountdown = 1f / this.tickRate;
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.attacker = base.gameObject;
					damageInfo.procCoefficient = this.procCoefficientPerSecond / this.tickRate;
					damageInfo.damage = this.damageCoefficientPerSecond * this.damageStat / this.tickRate;
					damageInfo.crit = base.RollCrit();
					this.targetBody.healthComponent.TakeDamage(damageInfo);
					this.voidSurvivorController.AddCorruption(this.corruptionPerSecond / this.tickRate);
				}
			}
			if (base.isAuthority && (!this.targetBody || !this.targetBody.healthComponent.alive))
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400053B RID: 1339
		[SerializeField]
		public float minimumDuration;

		// Token: 0x0400053C RID: 1340
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400053D RID: 1341
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400053E RID: 1342
		[SerializeField]
		public float maxSearchAngleFilter;

		// Token: 0x0400053F RID: 1343
		[SerializeField]
		public float maxSearchDistance;

		// Token: 0x04000540 RID: 1344
		[SerializeField]
		public float idealDistance;

		// Token: 0x04000541 RID: 1345
		[SerializeField]
		public float springConstant;

		// Token: 0x04000542 RID: 1346
		[SerializeField]
		public float springMaxLength;

		// Token: 0x04000543 RID: 1347
		[SerializeField]
		public float damperConstant;

		// Token: 0x04000544 RID: 1348
		[SerializeField]
		public float suppressedTargetForceRadius;

		// Token: 0x04000545 RID: 1349
		[SerializeField]
		public GameObject suppressEffectPrefab;

		// Token: 0x04000546 RID: 1350
		[SerializeField]
		public string muzzle;

		// Token: 0x04000547 RID: 1351
		[SerializeField]
		public AnimationCurve forceSuitabilityCurve;

		// Token: 0x04000548 RID: 1352
		[SerializeField]
		public float damageCoefficientPerSecond;

		// Token: 0x04000549 RID: 1353
		[SerializeField]
		public float procCoefficientPerSecond;

		// Token: 0x0400054A RID: 1354
		[SerializeField]
		public float corruptionPerSecond;

		// Token: 0x0400054B RID: 1355
		[SerializeField]
		public float maxBreakDistance;

		// Token: 0x0400054C RID: 1356
		[SerializeField]
		public float tickRate;

		// Token: 0x0400054D RID: 1357
		[SerializeField]
		public bool applyForces;

		// Token: 0x0400054E RID: 1358
		private GameObject suppressEffectInstance;

		// Token: 0x0400054F RID: 1359
		private Transform idealFXTransform;

		// Token: 0x04000550 RID: 1360
		private Transform targetFXTransform;

		// Token: 0x04000551 RID: 1361
		private Transform muzzleTransform;

		// Token: 0x04000552 RID: 1362
		private VoidSurvivorController voidSurvivorController;

		// Token: 0x04000553 RID: 1363
		private CharacterBody targetBody;

		// Token: 0x04000554 RID: 1364
		private float damageTickCountdown;
	}
}
