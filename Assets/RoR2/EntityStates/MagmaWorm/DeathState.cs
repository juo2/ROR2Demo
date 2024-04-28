using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MagmaWorm
{
	// Token: 0x02000300 RID: 768
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00039BB0 File Offset: 0x00037DB0
		public override void OnEnter()
		{
			base.OnEnter();
			WormBodyPositions2 component = base.GetComponent<WormBodyPositions2>();
			WormBodyPositionsDriver component2 = base.GetComponent<WormBodyPositionsDriver>();
			if (component)
			{
				component2.yDamperConstant = 0f;
				component2.ySpringConstant = 0f;
				component2.maxTurnSpeed = 0f;
				component.meatballCount = 0;
				Util.PlaySound(DeathState.deathSoundString, component.bones[0].gameObject);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				PrintController printController = modelTransform.gameObject.AddComponent<PrintController>();
				printController.printTime = DeathState.duration;
				printController.enabled = true;
				printController.startingPrintHeight = 99999f;
				printController.maxPrintHeight = 99999f;
				printController.startingPrintBias = 1f;
				printController.maxPrintBias = 3.5f;
				printController.animateFlowmapPower = true;
				printController.startingFlowmapPower = 1.14f;
				printController.maxFlowmapPower = 30f;
				printController.disableWhenFinished = false;
				printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
				ParticleSystem[] componentsInChildren = modelTransform.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Stop();
				}
				ChildLocator component3 = modelTransform.GetComponent<ChildLocator>();
				if (component3)
				{
					Transform transform = component3.FindChild("PP");
					if (transform)
					{
						PostProcessDuration component4 = transform.GetComponent<PostProcessDuration>();
						if (component4)
						{
							component4.enabled = true;
							component4.maxDuration = DeathState.duration;
						}
					}
				}
				if (NetworkServer.active)
				{
					EffectManager.SimpleMuzzleFlash(DeathState.initialDeathExplosionEffect, base.gameObject, "HeadCenter", true);
				}
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00039D43 File Offset: 0x00037F43
		public override void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (NetworkServer.active && this.stopwatch > DeathState.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x040010CF RID: 4303
		public static GameObject initialDeathExplosionEffect;

		// Token: 0x040010D0 RID: 4304
		public static string deathSoundString;

		// Token: 0x040010D1 RID: 4305
		public static float duration;

		// Token: 0x040010D2 RID: 4306
		private float stopwatch;
	}
}
