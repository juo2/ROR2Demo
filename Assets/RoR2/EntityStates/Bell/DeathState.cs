using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bell
{
	// Token: 0x02000459 RID: 1113
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000589DC File Offset: 0x00056BDC
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				if (modelTransform.GetComponent<ChildLocator>() && DeathState.initialEffect)
				{
					EffectManager.SpawnEffect(DeathState.initialEffect, new EffectData
					{
						origin = base.transform.position,
						scale = DeathState.initialEffectScale
					}, false);
				}
				RagdollController component = modelTransform.GetComponent<RagdollController>();
				Rigidbody component2 = base.GetComponent<Rigidbody>();
				if (component && component2)
				{
					component.BeginRagdoll(component2.velocity * DeathState.velocityMagnitude);
				}
				ExplodeRigidbodiesOnStart component3 = modelTransform.GetComponent<ExplodeRigidbodiesOnStart>();
				if (component3)
				{
					component3.force = DeathState.explosionForce;
					component3.enabled = true;
				}
			}
			if (base.modelLocator)
			{
				base.modelLocator.autoUpdateModelTransform = false;
			}
			base.DestroyBodyAsapServer();
		}

		// Token: 0x0400197F RID: 6527
		public static GameObject initialEffect;

		// Token: 0x04001980 RID: 6528
		public static float initialEffectScale;

		// Token: 0x04001981 RID: 6529
		public static float velocityMagnitude;

		// Token: 0x04001982 RID: 6530
		public static float explosionForce;
	}
}
