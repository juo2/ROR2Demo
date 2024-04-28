using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidInfestor
{
	// Token: 0x0200015A RID: 346
	public class Infest : BaseState
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00019F94 File Offset: 0x00018194
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Infest");
			Util.PlaySound(Infest.enterSoundString, base.gameObject);
			if (Infest.enterEffectPrefab)
			{
				EffectManager.SimpleImpactEffect(Infest.enterEffectPrefab, base.characterBody.corePosition, Vector3.up, false);
			}
			if (Infest.infestVfxPrefab)
			{
				this.infestVfxInstance = UnityEngine.Object.Instantiate<GameObject>(Infest.infestVfxPrefab, base.transform.position, Quaternion.identity);
				this.infestVfxInstance.transform.parent = base.transform;
			}
			HitBoxGroup hitBoxGroup = null;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Infest");
			}
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = base.GetTeam();
			this.attack.damage = Infest.infestDamageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = null;
			this.attack.hitBoxGroup = hitBoxGroup;
			this.attack.isCrit = base.RollCrit();
			this.attack.damageType = DamageType.Stun1s;
			this.attack.damageColorIndex = DamageColorIndex.Void;
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.viewer = base.characterBody;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(base.characterBody.teamComponent.teamIndex);
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.minDistanceFilter = 0f;
			bullseyeSearch.maxDistanceFilter = Infest.searchMaxDistance;
			bullseyeSearch.searchOrigin = base.inputBank.aimOrigin;
			bullseyeSearch.searchDirection = base.inputBank.aimDirection;
			bullseyeSearch.maxAngleFilter = Infest.searchMaxAngle;
			bullseyeSearch.filterByLoS = true;
			bullseyeSearch.RefreshCandidates();
			HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox)
			{
				this.targetTransform = hurtBox.transform;
				if (base.characterMotor)
				{
					Vector3 position = this.targetTransform.position;
					float num = Infest.velocityInitialSpeed;
					Vector3 vector = position - base.transform.position;
					Vector2 vector2 = new Vector2(vector.x, vector.z);
					float magnitude = vector2.magnitude;
					float y = Trajectory.CalculateInitialYSpeed(magnitude / num, vector.y);
					Vector3 vector3 = new Vector3(vector2.x / magnitude * num, y, vector2.y / magnitude * num);
					base.characterMotor.velocity = vector3;
					base.characterMotor.disableAirControlUntilCollision = true;
					base.characterMotor.Motor.ForceUnground();
					base.characterDirection.forward = vector3;
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001A270 File Offset: 0x00018470
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.targetTransform && base.characterMotor)
			{
				Vector3 target = this.targetTransform.position - base.transform.position;
				Vector3 vector = base.characterMotor.velocity;
				vector = Vector3.RotateTowards(vector, target, Infest.velocityTurnRate * Time.fixedDeltaTime * 0.017453292f, 0f);
				base.characterMotor.velocity = vector;
				if (NetworkServer.active && this.attack.Fire(this.victimsStruck))
				{
					int i = 0;
					while (i < this.victimsStruck.Count)
					{
						HealthComponent healthComponent = this.victimsStruck[i].healthComponent;
						CharacterBody body = healthComponent.body;
						CharacterMaster master = body.master;
						if (healthComponent.alive && master != null && !body.isPlayerControlled && !body.bodyFlags.HasFlag(CharacterBody.BodyFlags.Mechanical))
						{
							master.teamIndex = TeamIndex.Void;
							body.teamComponent.teamIndex = TeamIndex.Void;
							body.inventory.SetEquipmentIndex(DLC1Content.Elites.Void.eliteEquipmentDef.equipmentIndex);
							BaseAI component = master.GetComponent<BaseAI>();
							if (component)
							{
								component.enemyAttention = 0f;
								component.ForceAcquireNearestEnemyIfNoCurrentEnemy();
							}
							base.healthComponent.Suicide(null, null, DamageType.Generic);
							if (Infest.successfulInfestEffectPrefab)
							{
								EffectManager.SimpleImpactEffect(Infest.successfulInfestEffectPrefab, base.transform.position, Vector3.up, false);
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
			}
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterMotor.velocity;
			}
			if (base.isAuthority && base.characterMotor && base.characterMotor.isGrounded && base.fixedAge > 0.1f)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001A475 File Offset: 0x00018675
		public override void OnExit()
		{
			if (this.infestVfxInstance)
			{
				EntityState.Destroy(this.infestVfxInstance);
			}
			base.OnExit();
		}

		// Token: 0x0400075E RID: 1886
		public static GameObject enterEffectPrefab;

		// Token: 0x0400075F RID: 1887
		public static GameObject successfulInfestEffectPrefab;

		// Token: 0x04000760 RID: 1888
		public static GameObject infestVfxPrefab;

		// Token: 0x04000761 RID: 1889
		public static string enterSoundString;

		// Token: 0x04000762 RID: 1890
		public static float searchMaxDistance;

		// Token: 0x04000763 RID: 1891
		public static float searchMaxAngle;

		// Token: 0x04000764 RID: 1892
		public static float velocityInitialSpeed;

		// Token: 0x04000765 RID: 1893
		public static float velocityTurnRate;

		// Token: 0x04000766 RID: 1894
		public static float infestDamageCoefficient;

		// Token: 0x04000767 RID: 1895
		private Transform targetTransform;

		// Token: 0x04000768 RID: 1896
		private GameObject infestVfxInstance;

		// Token: 0x04000769 RID: 1897
		private OverlapAttack attack;

		// Token: 0x0400076A RID: 1898
		private List<HurtBox> victimsStruck = new List<HurtBox>();
	}
}
