using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.RoboBallBoss
{
	// Token: 0x020001E1 RID: 481
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x00024444 File Offset: 0x00022644
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
						gameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = DeathState.duration;
						gameObject.transform.parent = this.centerTransform;
					}
				}
				this.modelBaseTransform = base.modelLocator.modelBaseTransform;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000244F0 File Offset: 0x000226F0
		private void AttemptDeathBehavior()
		{
			if (this.attemptedDeathBehavior)
			{
				return;
			}
			this.attemptedDeathBehavior = true;
			if (DeathState.deathEffect && NetworkServer.active && this.centerTransform)
			{
				EffectManager.SpawnEffect(DeathState.deathEffect, new EffectData
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

		// Token: 0x06000898 RID: 2200 RVA: 0x00024584 File Offset: 0x00022784
		public override void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= DeathState.duration)
			{
				this.AttemptDeathBehavior();
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000245AB File Offset: 0x000227AB
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				this.AttemptDeathBehavior();
			}
			base.OnExit();
		}

		// Token: 0x04000A17 RID: 2583
		public static GameObject initialEffect;

		// Token: 0x04000A18 RID: 2584
		public static GameObject deathEffect;

		// Token: 0x04000A19 RID: 2585
		public static float duration = 2f;

		// Token: 0x04000A1A RID: 2586
		private float stopwatch;

		// Token: 0x04000A1B RID: 2587
		private Transform modelBaseTransform;

		// Token: 0x04000A1C RID: 2588
		private Transform centerTransform;

		// Token: 0x04000A1D RID: 2589
		private bool attemptedDeathBehavior;
	}
}
