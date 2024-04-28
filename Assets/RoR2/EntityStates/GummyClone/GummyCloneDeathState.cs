using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GummyClone
{
	// Token: 0x0200033F RID: 831
	public class GummyCloneDeathState : BaseState
	{
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0004045C File Offset: 0x0003E65C
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.soundString, base.gameObject);
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
			if (base.isAuthority && this.effectPrefab)
			{
				EffectManager.SimpleImpactEffect(this.effectPrefab, base.transform.position, Vector3.up, true);
			}
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040012A7 RID: 4775
		[SerializeField]
		public string soundString;

		// Token: 0x040012A8 RID: 4776
		[SerializeField]
		public GameObject effectPrefab;
	}
}
