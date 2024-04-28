using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Drone
{
	// Token: 0x020003C5 RID: 965
	public class MegaDroneDeathState : GenericCharacterDeath
	{
		// Token: 0x06001136 RID: 4406 RVA: 0x0004BCB6 File Offset: 0x00049EB6
		public override void OnEnter()
		{
			if (NetworkServer.active)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004BCCC File Offset: 0x00049ECC
		public override void OnExit()
		{
			base.OnExit();
			Util.PlaySound(MegaDroneDeathState.initialSoundString, base.gameObject);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform && NetworkServer.active)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					component.FindChild("LeftJet").gameObject.SetActive(false);
					component.FindChild("RightJet").gameObject.SetActive(false);
					if (MegaDroneDeathState.initialEffect)
					{
						EffectManager.SpawnEffect(MegaDroneDeathState.initialEffect, new EffectData
						{
							origin = base.transform.position,
							scale = MegaDroneDeathState.initialEffectScale
						}, true);
					}
				}
			}
			Rigidbody component2 = base.GetComponent<Rigidbody>();
			RagdollController component3 = modelTransform.GetComponent<RagdollController>();
			if (component3 && component2)
			{
				component3.BeginRagdoll(component2.velocity * MegaDroneDeathState.velocityMagnitude);
			}
			ExplodeRigidbodiesOnStart component4 = modelTransform.GetComponent<ExplodeRigidbodiesOnStart>();
			if (component4)
			{
				component4.force = MegaDroneDeathState.explosionForce;
				component4.enabled = true;
			}
		}

		// Token: 0x040015C5 RID: 5573
		public static string initialSoundString;

		// Token: 0x040015C6 RID: 5574
		public static GameObject initialEffect;

		// Token: 0x040015C7 RID: 5575
		public static float initialEffectScale;

		// Token: 0x040015C8 RID: 5576
		public static float velocityMagnitude;

		// Token: 0x040015C9 RID: 5577
		public static float explosionForce;
	}
}
