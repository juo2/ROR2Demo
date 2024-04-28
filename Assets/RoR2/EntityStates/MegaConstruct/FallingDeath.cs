using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MegaConstruct
{
	// Token: 0x02000285 RID: 645
	public class FallingDeath : GenericCharacterDeath
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleImpactEffect(FallingDeath.enterEffectPrefab, base.characterBody.corePosition, Vector3.up, true);
			MasterSpawnSlotController component = base.GetComponent<MasterSpawnSlotController>();
			if (NetworkServer.active && component)
			{
				component.KillAll();
			}
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				Transform transform = modelChildLocator.FindChild(FallingDeath.standableSurfaceChildName);
				if (transform)
				{
					transform.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002FA30 File Offset: 0x0002DC30
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > FallingDeath.deathDelay && NetworkServer.active && !this.hasDied)
			{
				this.hasDied = true;
				EffectManager.SimpleImpactEffect(FallingDeath.deathEffectPrefab, base.characterBody.corePosition, Vector3.up, true);
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002FA88 File Offset: 0x0002DC88
		public override void OnExit()
		{
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				RagdollController component2 = modelTransform.GetComponent<RagdollController>();
				if (component2 && component)
				{
					component2.BeginRagdoll(component.velocity);
				}
				ExplodeRigidbodiesOnStart component3 = modelTransform.GetComponent<ExplodeRigidbodiesOnStart>();
				if (component3)
				{
					component3.force = FallingDeath.explosionForce;
					component3.enabled = true;
				}
			}
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.enabled = false;
			}
			base.OnExit();
		}

		// Token: 0x04000D5E RID: 3422
		public static float deathDelay;

		// Token: 0x04000D5F RID: 3423
		public static GameObject enterEffectPrefab;

		// Token: 0x04000D60 RID: 3424
		public static GameObject deathEffectPrefab;

		// Token: 0x04000D61 RID: 3425
		public static float explosionForce;

		// Token: 0x04000D62 RID: 3426
		public static string standableSurfaceChildName;

		// Token: 0x04000D63 RID: 3427
		private bool hasDied;
	}
}
