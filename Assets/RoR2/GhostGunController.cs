using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000707 RID: 1799
	public class GhostGunController : MonoBehaviour
	{
		// Token: 0x0600251A RID: 9498 RVA: 0x0009D60F File Offset: 0x0009B80F
		private void Start()
		{
			this.fireTimer = 0f;
			this.ammo = 6;
			this.kills = 0;
			this.timeoutTimer = this.timeout;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0009D638 File Offset: 0x0009B838
		private void Fire(Vector3 origin, Vector3 aimDirection)
		{
			BulletAttack bulletAttack = new BulletAttack();
			bulletAttack.aimVector = aimDirection;
			bulletAttack.bulletCount = 1U;
			bulletAttack.damage = this.CalcDamage();
			bulletAttack.force = 2400f;
			bulletAttack.maxSpread = 0f;
			bulletAttack.minSpread = 0f;
			bulletAttack.muzzleName = "muzzle";
			bulletAttack.origin = origin;
			bulletAttack.owner = this.owner;
			bulletAttack.procCoefficient = 0f;
			bulletAttack.tracerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerSmokeChase");
			bulletAttack.hitEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/Hitspark1");
			bulletAttack.damageColorIndex = DamageColorIndex.Item;
			GlobalEventManager.onCharacterDeathGlobal += this.CheckForKill;
			bulletAttack.Fire();
			GlobalEventManager.onCharacterDeathGlobal -= this.CheckForKill;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0009D6FC File Offset: 0x0009B8FC
		private void CheckForKill(DamageReport damageReport)
		{
			if (damageReport.damageInfo.inflictor == base.gameObject)
			{
				this.kills++;
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0009D724 File Offset: 0x0009B924
		private float CalcDamage()
		{
			float damage = this.owner.GetComponent<CharacterBody>().damage;
			return 5f * Mathf.Pow(2f, (float)this.kills) * damage;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0009D75C File Offset: 0x0009B95C
		private bool HasLoS(GameObject target)
		{
			Ray ray = new Ray(base.transform.position, target.transform.position - base.transform.position);
			RaycastHit raycastHit = default(RaycastHit);
			return !Physics.Raycast(ray, out raycastHit, this.maxRange, LayerIndex.defaultLayer.mask | LayerIndex.world.mask, QueryTriggerInteraction.Ignore) || raycastHit.collider.gameObject == target;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0009D7E8 File Offset: 0x0009B9E8
		private bool WillHit(GameObject target)
		{
			Ray ray = new Ray(base.transform.position, base.transform.forward);
			RaycastHit raycastHit = default(RaycastHit);
			if (Physics.Raycast(ray, out raycastHit, this.maxRange, LayerIndex.entityPrecise.mask | LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
			{
				HurtBox component = raycastHit.collider.GetComponent<HurtBox>();
				if (component)
				{
					HealthComponent healthComponent = component.healthComponent;
					if (healthComponent)
					{
						return healthComponent.gameObject == target;
					}
				}
			}
			return false;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0009D880 File Offset: 0x0009BA80
		private GameObject FindTarget()
		{
			TeamIndex teamA = TeamIndex.Neutral;
			TeamComponent component = this.owner.GetComponent<TeamComponent>();
			if (component)
			{
				teamA = component.teamIndex;
			}
			Vector3 position = base.transform.position;
			float num = this.CalcDamage();
			float num2 = this.maxRange * this.maxRange;
			GameObject gameObject = null;
			GameObject result = null;
			float num3 = 0f;
			float num4 = float.PositiveInfinity;
			for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
			{
				if (TeamManager.IsTeamEnemy(teamA, teamIndex))
				{
					ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
					for (int i = 0; i < teamMembers.Count; i++)
					{
						GameObject gameObject2 = teamMembers[i].gameObject;
						if ((gameObject2.transform.position - position).sqrMagnitude <= num2)
						{
							HealthComponent component2 = teamMembers[i].GetComponent<HealthComponent>();
							if (component2)
							{
								if (component2.health <= num)
								{
									if (component2.health > num3 && this.HasLoS(gameObject2))
									{
										gameObject = gameObject2;
										num3 = component2.health;
									}
								}
								else if (component2.health < num4 && this.HasLoS(gameObject2))
								{
									result = gameObject2;
									num4 = component2.health;
								}
							}
						}
					}
				}
			}
			if (!gameObject)
			{
				return result;
			}
			return gameObject;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
		private void FixedUpdate()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (!this.owner)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			InputBankTest component = this.owner.GetComponent<InputBankTest>();
			Vector3 vector = component ? component.aimDirection : base.transform.forward;
			if (this.target)
			{
				vector = (this.target.transform.position - base.transform.position).normalized;
			}
			base.transform.forward = Vector3.RotateTowards(base.transform.forward, vector, 0.017453292f * this.turnSpeed * Time.fixedDeltaTime, 0f);
			Vector3 vector2 = this.owner.transform.position + base.transform.rotation * this.localOffset;
			base.transform.position = Vector3.SmoothDamp(base.transform.position, vector2, ref this.velocity, this.positionSmoothTime, float.PositiveInfinity, Time.fixedDeltaTime);
			this.fireTimer -= Time.fixedDeltaTime;
			this.timeoutTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				this.target = this.FindTarget();
				this.fireTimer = this.interval;
			}
			if (this.target && this.WillHit(this.target))
			{
				Vector3 normalized = (this.target.transform.position - base.transform.position).normalized;
				this.Fire(base.transform.position, normalized);
				this.ammo--;
				this.target = null;
				this.timeoutTimer = this.timeout;
			}
			if (this.ammo <= 0 || this.timeoutTimer <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x040028EE RID: 10478
		public GameObject owner;

		// Token: 0x040028EF RID: 10479
		public float interval;

		// Token: 0x040028F0 RID: 10480
		public float maxRange = 20f;

		// Token: 0x040028F1 RID: 10481
		public float turnSpeed = 180f;

		// Token: 0x040028F2 RID: 10482
		public Vector3 localOffset = Vector3.zero;

		// Token: 0x040028F3 RID: 10483
		public float positionSmoothTime = 0.05f;

		// Token: 0x040028F4 RID: 10484
		public float timeout = 2f;

		// Token: 0x040028F5 RID: 10485
		private float fireTimer;

		// Token: 0x040028F6 RID: 10486
		private float timeoutTimer;

		// Token: 0x040028F7 RID: 10487
		private int ammo;

		// Token: 0x040028F8 RID: 10488
		private int kills;

		// Token: 0x040028F9 RID: 10489
		private GameObject target;

		// Token: 0x040028FA RID: 10490
		private Vector3 velocity;
	}
}
