using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Fauna
{
	// Token: 0x02000387 RID: 903
	public class BirdsharkDeathState : BaseState
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x000472E0 File Offset: 0x000454E0
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.deathSoundString, base.gameObject);
			if (base.modelLocator)
			{
				if (base.modelLocator.modelBaseTransform)
				{
					EntityState.Destroy(base.modelLocator.modelBaseTransform.gameObject);
				}
				if (base.modelLocator.modelTransform)
				{
					EntityState.Destroy(base.modelLocator.modelTransform.gameObject);
				}
			}
			if (this.initialExplosion && NetworkServer.active)
			{
				EffectManager.SimpleImpactEffect(this.initialExplosion, base.transform.position, Vector3.up, true);
			}
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001499 RID: 5273
		[SerializeField]
		public GameObject initialExplosion;

		// Token: 0x0400149A RID: 5274
		[SerializeField]
		public string deathSoundString;
	}
}
