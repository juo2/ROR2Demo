using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.TitanMonster
{
	// Token: 0x0200035E RID: 862
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x0004408C File Offset: 0x0004228C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.modelLocator)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.centerTransform = component.FindChild("Center");
					if (DeathState.initialEffect)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(DeathState.initialEffect, this.centerTransform.position, this.centerTransform.rotation);
						gameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = DeathState.duration + 2f;
						gameObject.transform.parent = this.centerTransform;
					}
				}
				this.modelBaseTransform = base.modelLocator.modelBaseTransform;
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0004413C File Offset: 0x0004233C
		private void AttemptDeathBehavior()
		{
			if (this.attemptedDeathBehavior)
			{
				return;
			}
			this.attemptedDeathBehavior = true;
			if (this.deathEffect && NetworkServer.active && this.centerTransform)
			{
				EffectManager.SpawnEffect(this.deathEffect, new EffectData
				{
					origin = this.centerTransform.position
				}, true);
			}
			if (this.modelBaseTransform)
			{
				EntityState.Destroy(this.modelBaseTransform.gameObject);
				this.modelBaseTransform = null;
			}
			if (NetworkServer.active)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000441D2 File Offset: 0x000423D2
		public override void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= DeathState.duration)
			{
				this.AttemptDeathBehavior();
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000441F9 File Offset: 0x000423F9
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				this.AttemptDeathBehavior();
			}
			base.OnExit();
		}

		// Token: 0x040013AA RID: 5034
		public static GameObject initialEffect;

		// Token: 0x040013AB RID: 5035
		[SerializeField]
		public GameObject deathEffect;

		// Token: 0x040013AC RID: 5036
		public static float duration = 2f;

		// Token: 0x040013AD RID: 5037
		private float stopwatch;

		// Token: 0x040013AE RID: 5038
		private Transform centerTransform;

		// Token: 0x040013AF RID: 5039
		private Transform modelBaseTransform;

		// Token: 0x040013B0 RID: 5040
		private bool attemptedDeathBehavior;
	}
}
