using System;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B8E RID: 2958
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileExplosion : MonoBehaviour
	{
		// Token: 0x06004358 RID: 17240 RVA: 0x00117848 File Offset: 0x00115A48
		protected virtual void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x00117862 File Offset: 0x00115A62
		public void Detonate()
		{
			if (NetworkServer.active)
			{
				this.DetonateServer();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x0011787C File Offset: 0x00115A7C
		protected void DetonateServer()
		{
			if (this.explosionEffect)
			{
				EffectManager.SpawnEffect(this.explosionEffect, new EffectData
				{
					origin = base.transform.position,
					scale = this.blastRadius
				}, true);
			}
			if (this.projectileDamage)
			{
				BlastAttack blastAttack = new BlastAttack();
				blastAttack.position = base.transform.position;
				blastAttack.baseDamage = this.projectileDamage.damage * this.blastDamageCoefficient;
				blastAttack.baseForce = this.projectileDamage.force * this.blastDamageCoefficient;
				blastAttack.radius = this.blastRadius;
				blastAttack.attacker = (this.projectileController.owner ? this.projectileController.owner.gameObject : null);
				blastAttack.inflictor = base.gameObject;
				blastAttack.teamIndex = this.projectileController.teamFilter.teamIndex;
				blastAttack.crit = this.projectileDamage.crit;
				blastAttack.procChainMask = this.projectileController.procChainMask;
				blastAttack.procCoefficient = this.projectileController.procCoefficient * this.blastProcCoefficient;
				blastAttack.bonusForce = this.bonusBlastForce;
				blastAttack.falloffModel = this.falloffModel;
				blastAttack.damageColorIndex = this.projectileDamage.damageColorIndex;
				blastAttack.damageType = this.projectileDamage.damageType;
				blastAttack.attackerFiltering = this.blastAttackerFiltering;
				blastAttack.canRejectForce = this.canRejectForce;
				BlastAttack.Result result = blastAttack.Fire();
				this.OnBlastAttackResult(blastAttack, result);
			}
			if (this.explosionSoundString.Length > 0)
			{
				Util.PlaySound(this.explosionSoundString, base.gameObject);
			}
			if (this.fireChildren)
			{
				for (int i = 0; i < this.childrenCount; i++)
				{
					this.FireChild();
				}
			}
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x00117A50 File Offset: 0x00115C50
		protected Quaternion GetRandomChildRollPitch()
		{
			Quaternion lhs = Quaternion.AngleAxis(this.minRollDegrees + UnityEngine.Random.Range(0f, this.rangeRollDegrees), Vector3.forward);
			Quaternion rhs = Quaternion.AngleAxis(this.minPitchDegrees + UnityEngine.Random.Range(0f, this.rangePitchDegrees), Vector3.left);
			return lhs * rhs;
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x00117AA8 File Offset: 0x00115CA8
		protected virtual Quaternion GetRandomDirectionForChild()
		{
			Quaternion randomChildRollPitch = this.GetRandomChildRollPitch();
			if (this.useLocalSpaceForChildren)
			{
				return base.transform.rotation * randomChildRollPitch;
			}
			return randomChildRollPitch;
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x00117AD8 File Offset: 0x00115CD8
		protected void FireChild()
		{
			Quaternion randomDirectionForChild = this.GetRandomDirectionForChild();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.childrenProjectilePrefab, base.transform.position, randomDirectionForChild);
			ProjectileController component = gameObject.GetComponent<ProjectileController>();
			if (component)
			{
				component.procChainMask = this.projectileController.procChainMask;
				component.procCoefficient = this.projectileController.procCoefficient;
				component.Networkowner = this.projectileController.owner;
			}
			gameObject.GetComponent<TeamFilter>().teamIndex = base.GetComponent<TeamFilter>().teamIndex;
			ProjectileDamage component2 = gameObject.GetComponent<ProjectileDamage>();
			if (component2)
			{
				component2.damage = this.projectileDamage.damage * this.childrenDamageCoefficient;
				component2.crit = this.projectileDamage.crit;
				component2.force = this.projectileDamage.force;
				component2.damageColorIndex = this.projectileDamage.damageColorIndex;
			}
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x00117BBA File Offset: 0x00115DBA
		public void SetExplosionRadius(float newRadius)
		{
			this.blastRadius = newRadius;
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x00117BC3 File Offset: 0x00115DC3
		public void SetAlive(bool newAlive)
		{
			this.alive = newAlive;
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x00117BCC File Offset: 0x00115DCC
		public bool GetAlive()
		{
			if (!NetworkServer.active)
			{
				Debug.Log("Cannot get alive state. Returning false.");
				return false;
			}
			return this.alive;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x00117BE7 File Offset: 0x00115DE7
		protected virtual void OnValidate()
		{
			if (Application.IsPlaying(this))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.explosionSoundString))
			{
				Debug.LogWarningFormat(base.gameObject, "{0} ProjectileImpactExplosion component supplies a value in the explosionSoundString field. This will not play correctly over the network. Please move the sound to the explosion effect.", new object[]
				{
					Util.GetGameObjectHierarchyName(base.gameObject)
				});
			}
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x00117C24 File Offset: 0x00115E24
		protected virtual void OnBlastAttackResult(BlastAttack blastAttack, BlastAttack.Result result)
		{
			if (this.applyDot)
			{
				GameObject attacker = blastAttack.attacker;
				CharacterBody characterBody = (attacker != null) ? attacker.GetComponent<CharacterBody>() : null;
				foreach (BlastAttack.HitPoint hitPoint in result.hitPoints)
				{
					if (hitPoint.hurtBox && hitPoint.hurtBox.healthComponent)
					{
						InflictDotInfo inflictDotInfo = new InflictDotInfo
						{
							victimObject = hitPoint.hurtBox.healthComponent.gameObject,
							attackerObject = blastAttack.attacker,
							dotIndex = this.dotIndex,
							damageMultiplier = this.dotDamageMultiplier
						};
						if (this.calculateTotalDamage && characterBody)
						{
							inflictDotInfo.totalDamage = new float?(characterBody.damage * this.totalDamageMultiplier);
						}
						else
						{
							inflictDotInfo.duration = this.dotDuration;
						}
						if (this.applyMaxStacksFromAttacker)
						{
							inflictDotInfo.maxStacksFromAttacker = new uint?(this.maxStacksFromAttacker);
						}
						if (characterBody && characterBody.inventory)
						{
							StrengthenBurnUtils.CheckDotForUpgrade(characterBody.inventory, ref inflictDotInfo);
						}
						DotController.InflictDot(ref inflictDotInfo);
					}
				}
			}
		}

		// Token: 0x0400418D RID: 16781
		protected ProjectileController projectileController;

		// Token: 0x0400418E RID: 16782
		protected ProjectileDamage projectileDamage;

		// Token: 0x0400418F RID: 16783
		protected bool alive = true;

		// Token: 0x04004190 RID: 16784
		[Header("Main Properties")]
		public BlastAttack.FalloffModel falloffModel = BlastAttack.FalloffModel.Linear;

		// Token: 0x04004191 RID: 16785
		public float blastRadius;

		// Token: 0x04004192 RID: 16786
		[Tooltip("The percentage of the damage, proc coefficient, and force of the initial projectile. Ranges from 0-1")]
		public float blastDamageCoefficient;

		// Token: 0x04004193 RID: 16787
		public float blastProcCoefficient = 1f;

		// Token: 0x04004194 RID: 16788
		public AttackerFiltering blastAttackerFiltering;

		// Token: 0x04004195 RID: 16789
		public Vector3 bonusBlastForce;

		// Token: 0x04004196 RID: 16790
		public bool canRejectForce = true;

		// Token: 0x04004197 RID: 16791
		public HealthComponent projectileHealthComponent;

		// Token: 0x04004198 RID: 16792
		public GameObject explosionEffect;

		// Token: 0x04004199 RID: 16793
		[Obsolete("This sound will not play over the network. Provide the sound via the prefab referenced by explosionEffect instead.", false)]
		[ShowFieldObsolete]
		[Tooltip("This sound will not play over the network. Provide the sound via the prefab referenced by explosionEffect instead.")]
		public string explosionSoundString;

		// Token: 0x0400419A RID: 16794
		[Header("Child Properties")]
		[Tooltip("Does this projectile release children on death?")]
		public bool fireChildren;

		// Token: 0x0400419B RID: 16795
		public GameObject childrenProjectilePrefab;

		// Token: 0x0400419C RID: 16796
		public int childrenCount;

		// Token: 0x0400419D RID: 16797
		[Tooltip("What percentage of our damage does the children get?")]
		public float childrenDamageCoefficient;

		// Token: 0x0400419E RID: 16798
		[ShowFieldObsolete]
		[Tooltip("How to randomize the orientation of children")]
		public Vector3 minAngleOffset;

		// Token: 0x0400419F RID: 16799
		[ShowFieldObsolete]
		public Vector3 maxAngleOffset;

		// Token: 0x040041A0 RID: 16800
		public float minRollDegrees;

		// Token: 0x040041A1 RID: 16801
		public float rangeRollDegrees;

		// Token: 0x040041A2 RID: 16802
		public float minPitchDegrees;

		// Token: 0x040041A3 RID: 16803
		public float rangePitchDegrees;

		// Token: 0x040041A4 RID: 16804
		[Tooltip("useLocalSpaceForChildren is unused by ProjectileImpactExplosion")]
		public bool useLocalSpaceForChildren;

		// Token: 0x040041A5 RID: 16805
		[Header("DoT Properties")]
		[Tooltip("If true, applies a DoT given the following properties")]
		public bool applyDot;

		// Token: 0x040041A6 RID: 16806
		public DotController.DotIndex dotIndex = DotController.DotIndex.None;

		// Token: 0x040041A7 RID: 16807
		[Tooltip("Duration in seconds of the DoT.  Unused if calculateTotalDamage is true.")]
		public float dotDuration;

		// Token: 0x040041A8 RID: 16808
		[Tooltip("Multiplier on the per-tick damage")]
		public float dotDamageMultiplier = 1f;

		// Token: 0x040041A9 RID: 16809
		[Tooltip("If true, we cap the numer of DoT stacks for this attacker.")]
		public bool applyMaxStacksFromAttacker;

		// Token: 0x040041AA RID: 16810
		[Tooltip("The maximum number of stacks that we can apply for this attacker")]
		public uint maxStacksFromAttacker = uint.MaxValue;

		// Token: 0x040041AB RID: 16811
		[Tooltip("If true, we disregard the duration and instead specify the total damage.")]
		public bool calculateTotalDamage;

		// Token: 0x040041AC RID: 16812
		[Tooltip("totalDamage = totalDamageMultiplier * attacker's damage")]
		public float totalDamageMultiplier;
	}
}
