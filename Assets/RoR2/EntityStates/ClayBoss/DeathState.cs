using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ClayBoss
{
	// Token: 0x02000405 RID: 1029
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x0600127E RID: 4734 RVA: 0x00052908 File Offset: 0x00050B08
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

		// Token: 0x0600127F RID: 4735 RVA: 0x000529B4 File Offset: 0x00050BB4
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

		// Token: 0x06001280 RID: 4736 RVA: 0x00052A48 File Offset: 0x00050C48
		public override void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= DeathState.duration)
			{
				this.AttemptDeathBehavior();
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00052A6F File Offset: 0x00050C6F
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				this.AttemptDeathBehavior();
			}
			base.OnExit();
		}

		// Token: 0x040017D2 RID: 6098
		public static GameObject initialEffect;

		// Token: 0x040017D3 RID: 6099
		public static GameObject deathEffect;

		// Token: 0x040017D4 RID: 6100
		public static float duration = 2f;

		// Token: 0x040017D5 RID: 6101
		private float stopwatch;

		// Token: 0x040017D6 RID: 6102
		private Transform modelBaseTransform;

		// Token: 0x040017D7 RID: 6103
		private Transform centerTransform;

		// Token: 0x040017D8 RID: 6104
		private bool attemptedDeathBehavior;
	}
}
