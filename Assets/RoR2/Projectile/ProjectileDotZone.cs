using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B8D RID: 2957
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(HitBoxGroup))]
	public class ProjectileDotZone : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x06004353 RID: 17235 RVA: 0x00117538 File Offset: 0x00115738
		private void Start()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			this.ResetOverlap();
			this.onBegin.Invoke();
			if (!string.IsNullOrEmpty(this.soundLoopString))
			{
				Util.PlaySound(this.soundLoopString, base.gameObject);
			}
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x00117590 File Offset: 0x00115790
		private void ResetOverlap()
		{
			this.attack = new OverlapAttack();
			this.attack.procChainMask = this.projectileController.procChainMask;
			this.attack.procCoefficient = this.projectileController.procCoefficient * this.overlapProcCoefficient;
			this.attack.attacker = this.projectileController.owner;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = this.projectileController.teamFilter.teamIndex;
			this.attack.attackerFiltering = this.attackerFiltering;
			this.attack.damage = this.damageCoefficient * this.projectileDamage.damage;
			this.attack.forceVector = this.forceVector + this.projectileDamage.force * base.transform.forward;
			this.attack.hitEffectPrefab = this.impactEffect;
			this.attack.isCrit = this.projectileDamage.crit;
			this.attack.damageColorIndex = this.projectileDamage.damageColorIndex;
			this.attack.damageType = this.projectileDamage.damageType;
			this.attack.hitBoxGroup = base.GetComponent<HitBoxGroup>();
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x001176E0 File Offset: 0x001158E0
		public void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.totalStopwatch += Time.fixedDeltaTime;
				this.resetStopwatch += Time.fixedDeltaTime;
				this.fireStopwatch += Time.fixedDeltaTime;
				if (this.resetStopwatch >= 1f / this.resetFrequency)
				{
					this.ResetOverlap();
					this.resetStopwatch -= 1f / this.resetFrequency;
				}
				if (this.fireStopwatch >= 1f / this.fireFrequency)
				{
					this.attack.Fire(null);
					this.fireStopwatch -= 1f / this.fireFrequency;
				}
				if (this.lifetime > 0f && this.totalStopwatch >= this.lifetime)
				{
					this.onEnd.Invoke();
					if (!string.IsNullOrEmpty(this.soundLoopStopString))
					{
						Util.PlaySound(this.soundLoopStopString, base.gameObject);
					}
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x0400417B RID: 16763
		private ProjectileController projectileController;

		// Token: 0x0400417C RID: 16764
		private ProjectileDamage projectileDamage;

		// Token: 0x0400417D RID: 16765
		public float damageCoefficient;

		// Token: 0x0400417E RID: 16766
		public AttackerFiltering attackerFiltering = AttackerFiltering.NeverHitSelf;

		// Token: 0x0400417F RID: 16767
		public GameObject impactEffect;

		// Token: 0x04004180 RID: 16768
		public Vector3 forceVector;

		// Token: 0x04004181 RID: 16769
		public float overlapProcCoefficient = 1f;

		// Token: 0x04004182 RID: 16770
		[Tooltip("The frequency (1/time) at which the overlap attack is tested. Higher values are more accurate but more expensive.")]
		public float fireFrequency = 1f;

		// Token: 0x04004183 RID: 16771
		[Tooltip("The frequency  (1/time) at which the overlap attack is reset. Higher values means more frequent ticks of damage.")]
		public float resetFrequency = 20f;

		// Token: 0x04004184 RID: 16772
		public float lifetime = 30f;

		// Token: 0x04004185 RID: 16773
		[Tooltip("The event that runs at the start.")]
		public UnityEvent onBegin;

		// Token: 0x04004186 RID: 16774
		[Tooltip("The event that runs at the start.")]
		public UnityEvent onEnd;

		// Token: 0x04004187 RID: 16775
		private OverlapAttack attack;

		// Token: 0x04004188 RID: 16776
		private float fireStopwatch;

		// Token: 0x04004189 RID: 16777
		private float resetStopwatch;

		// Token: 0x0400418A RID: 16778
		private float totalStopwatch;

		// Token: 0x0400418B RID: 16779
		public string soundLoopString = "";

		// Token: 0x0400418C RID: 16780
		public string soundLoopStopString = "";
	}
}
