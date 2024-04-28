using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.LunarWisp
{
	// Token: 0x020002AC RID: 684
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00032DE4 File Offset: 0x00030FE4
		public override void OnEnter()
		{
			base.OnEnter();
			LunarWispFXController component = base.characterBody.GetComponent<LunarWispFXController>();
			if (component)
			{
				component.TurnOffFX();
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				RagdollController component2 = modelTransform.GetComponent<RagdollController>();
				Rigidbody component3 = base.GetComponent<Rigidbody>();
				if (component2 && component3)
				{
					component2.BeginRagdoll(component3.velocity * DeathState.velocityMagnitude);
				}
				ExplodeRigidbodiesOnStart component4 = modelTransform.GetComponent<ExplodeRigidbodiesOnStart>();
				if (component4)
				{
					component4.force = DeathState.explosionForce;
					component4.enabled = true;
				}
			}
			if (base.modelLocator)
			{
				base.modelLocator.autoUpdateModelTransform = false;
			}
			base.FindModelChild("StandableSurface").gameObject.SetActive(false);
			if (NetworkServer.active)
			{
				EffectData effectData = new EffectData
				{
					origin = base.FindModelChild(DeathState.deathEffectMuzzleName).position
				};
				EffectManager.SpawnEffect(DeathState.deathEffectPrefab, effectData, true);
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x04000EAA RID: 3754
		public static GameObject deathEffectPrefab;

		// Token: 0x04000EAB RID: 3755
		public static string deathEffectMuzzleName;

		// Token: 0x04000EAC RID: 3756
		public static float velocityMagnitude;

		// Token: 0x04000EAD RID: 3757
		public static float explosionForce;

		// Token: 0x04000EAE RID: 3758
		private LunarWispFXController FXController;
	}
}
