using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x02000496 RID: 1174
	public class ChannelRain : BaseState
	{
		// Token: 0x06001505 RID: 5381 RVA: 0x0005D27C File Offset: 0x0005B47C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChannelRain.baseDuration;
			this.durationBetweenCast = ChannelRain.baseDuration / (float)ChannelRain.explosionCount / this.attackSpeedStat;
			base.PlayCrossfade("Body", "ChannelRain", 0.3f);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
		private void PlaceRain()
		{
			Vector3 vector = Vector3.zero;
			Ray aimRay = base.GetAimRay();
			aimRay.origin += UnityEngine.Random.insideUnitSphere * ChannelRain.randomRadius;
			RaycastHit raycastHit;
			if (Physics.Raycast(aimRay, out raycastHit, (float)LayerIndex.world.mask))
			{
				vector = raycastHit.point;
			}
			if (vector != Vector3.zero)
			{
				TeamIndex teamIndex = base.characterBody.GetComponent<TeamComponent>().teamIndex;
				TeamIndex enemyTeam;
				if (teamIndex != TeamIndex.Player)
				{
					if (teamIndex == TeamIndex.Monster)
					{
						enemyTeam = TeamIndex.Player;
					}
					else
					{
						enemyTeam = TeamIndex.Neutral;
					}
				}
				else
				{
					enemyTeam = TeamIndex.Monster;
				}
				Transform transform = this.FindTargetClosest(vector, enemyTeam);
				Vector3 a = vector;
				if (transform)
				{
					a = transform.transform.position;
				}
				a += UnityEngine.Random.insideUnitSphere * ChannelRain.randomRadius;
				if (Physics.Raycast(new Ray
				{
					origin = a + Vector3.up * ChannelRain.randomRadius,
					direction = Vector3.down
				}, out raycastHit, 500f, LayerIndex.world.mask))
				{
					Vector3 point = raycastHit.point;
					Quaternion rotation = Util.QuaternionSafeLookRotation(raycastHit.normal);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ChannelRain.delayPrefab, point, rotation);
					DelayBlast component = gameObject.GetComponent<DelayBlast>();
					component.position = point;
					component.baseDamage = base.characterBody.damage * ChannelRain.damageCoefficient;
					component.baseForce = 2000f;
					component.bonusForce = Vector3.up * 1000f;
					component.radius = ChannelRain.radius;
					component.attacker = base.gameObject;
					component.inflictor = null;
					component.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
					component.maxTimer = ChannelRain.explosionDelay;
					gameObject.GetComponent<TeamFilter>().teamIndex = TeamComponent.GetObjectTeam(component.attacker);
					gameObject.transform.localScale = new Vector3(ChannelRain.radius, ChannelRain.radius, 1f);
					ScaleParticleSystemDuration component2 = gameObject.GetComponent<ScaleParticleSystemDuration>();
					if (component2)
					{
						component2.newDuration = ChannelRain.explosionDelay;
					}
				}
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0005D500 File Offset: 0x0005B700
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.castTimer += Time.fixedDeltaTime;
			if (this.castTimer >= this.durationBetweenCast)
			{
				this.PlaceRain();
				this.castTimer -= this.durationBetweenCast;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new EndRain());
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0005D574 File Offset: 0x0005B774
		private Transform FindTargetClosest(Vector3 point, TeamIndex enemyTeam)
		{
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(enemyTeam);
			float num = 99999f;
			Transform result = null;
			for (int i = 0; i < teamMembers.Count; i++)
			{
				float num2 = Vector3.SqrMagnitude(teamMembers[i].transform.position - point);
				if (num2 < num)
				{
					num = num2;
					result = teamMembers[i].transform;
				}
			}
			return result;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04001AD5 RID: 6869
		private float castTimer;

		// Token: 0x04001AD6 RID: 6870
		public static float baseDuration = 4f;

		// Token: 0x04001AD7 RID: 6871
		public static float explosionDelay = 2f;

		// Token: 0x04001AD8 RID: 6872
		public static int explosionCount = 10;

		// Token: 0x04001AD9 RID: 6873
		public static float damageCoefficient;

		// Token: 0x04001ADA RID: 6874
		public static float randomRadius;

		// Token: 0x04001ADB RID: 6875
		public static float radius;

		// Token: 0x04001ADC RID: 6876
		public static GameObject delayPrefab;

		// Token: 0x04001ADD RID: 6877
		private float duration;

		// Token: 0x04001ADE RID: 6878
		private float durationBetweenCast;
	}
}
