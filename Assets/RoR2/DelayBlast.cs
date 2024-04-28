using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000692 RID: 1682
	[RequireComponent(typeof(TeamFilter))]
	public class DelayBlast : MonoBehaviour
	{
		// Token: 0x060020E3 RID: 8419 RVA: 0x0008D77A File Offset: 0x0008B97A
		private void Awake()
		{
			this.teamFilter = base.GetComponent<TeamFilter>();
			if (!NetworkServer.active)
			{
				base.enabled = false;
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0008D798 File Offset: 0x0008B998
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.timer += Time.fixedDeltaTime;
				if (this.delayEffect && !this.hasSpawnedDelayEffect && this.timer > this.timerStagger)
				{
					this.hasSpawnedDelayEffect = true;
					EffectManager.SpawnEffect(this.delayEffect, new EffectData
					{
						origin = base.transform.position,
						rotation = Util.QuaternionSafeLookRotation(base.transform.forward),
						scale = this.radius
					}, true);
				}
				if (this.timer >= this.maxTimer + this.timerStagger)
				{
					this.Detonate();
				}
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0008D84C File Offset: 0x0008BA4C
		public void Detonate()
		{
			EffectManager.SpawnEffect(this.explosionEffect, new EffectData
			{
				origin = base.transform.position,
				rotation = Util.QuaternionSafeLookRotation(base.transform.forward),
				scale = this.radius
			}, true);
			new BlastAttack
			{
				position = this.position,
				baseDamage = this.baseDamage,
				baseForce = this.baseForce,
				bonusForce = this.bonusForce,
				radius = this.radius,
				attacker = this.attacker,
				inflictor = this.inflictor,
				teamIndex = this.teamFilter.teamIndex,
				crit = this.crit,
				damageColorIndex = this.damageColorIndex,
				damageType = this.damageType,
				falloffModel = this.falloffModel,
				procCoefficient = this.procCoefficient
			}.Fire();
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04002629 RID: 9769
		[HideInInspector]
		public Vector3 position;

		// Token: 0x0400262A RID: 9770
		[HideInInspector]
		public GameObject attacker;

		// Token: 0x0400262B RID: 9771
		[HideInInspector]
		public GameObject inflictor;

		// Token: 0x0400262C RID: 9772
		[HideInInspector]
		public float baseDamage;

		// Token: 0x0400262D RID: 9773
		[HideInInspector]
		public bool crit;

		// Token: 0x0400262E RID: 9774
		[HideInInspector]
		public float baseForce;

		// Token: 0x0400262F RID: 9775
		[HideInInspector]
		public float radius;

		// Token: 0x04002630 RID: 9776
		[HideInInspector]
		public Vector3 bonusForce;

		// Token: 0x04002631 RID: 9777
		[HideInInspector]
		public float maxTimer;

		// Token: 0x04002632 RID: 9778
		[HideInInspector]
		public DamageColorIndex damageColorIndex;

		// Token: 0x04002633 RID: 9779
		[HideInInspector]
		public BlastAttack.FalloffModel falloffModel;

		// Token: 0x04002634 RID: 9780
		[HideInInspector]
		public DamageType damageType;

		// Token: 0x04002635 RID: 9781
		[HideInInspector]
		public float procCoefficient = 1f;

		// Token: 0x04002636 RID: 9782
		public GameObject explosionEffect;

		// Token: 0x04002637 RID: 9783
		public GameObject delayEffect;

		// Token: 0x04002638 RID: 9784
		public float timerStagger;

		// Token: 0x04002639 RID: 9785
		private float timer;

		// Token: 0x0400263A RID: 9786
		private bool hasSpawnedDelayEffect;

		// Token: 0x0400263B RID: 9787
		private TeamFilter teamFilter;
	}
}
