using System;
using EntityStates.SurvivorPod;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivorPod
{
	// Token: 0x020000E7 RID: 231
	public class Landed : SurvivorPodBaseState
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00011220 File Offset: 0x0000F420
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.survivorPodController.exitAllowed = true;
			base.vehicleSeat.handleVehicleExitRequestServer.AddCallback(new CallbackCheck<bool, GameObject>.CallbackDelegate(this.HandleVehicleExitRequest));
			ModelLocator component = base.GetComponent<ModelLocator>();
			if (component && component.modelTransform)
			{
				ChildLocator component2 = component.modelTransform.GetComponent<ChildLocator>();
				if (component2)
				{
					Transform transform = component2.FindChild(this.particleChildName);
					if (transform)
					{
						this.particles = transform.GetComponentsInChildren<ParticleSystem>();
						transform.gameObject.SetActive(true);
					}
				}
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000112DC File Offset: 0x0000F4DC
		private void HandleVehicleExitRequest(GameObject gameObject, ref bool? result)
		{
			base.survivorPodController.exitAllowed = false;
			this.outer.SetNextState(new Release());
			result = new bool?(true);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00011308 File Offset: 0x0000F508
		public override void OnExit()
		{
			base.vehicleSeat.handleVehicleExitRequestServer.RemoveCallback(new CallbackCheck<bool, GameObject>.CallbackDelegate(this.HandleVehicleExitRequest));
			base.survivorPodController.exitAllowed = false;
			EffectManager.SimpleMuzzleFlash(this.openEffect, base.gameObject, this.effectMuzzle, false);
			ParticleSystem[] array = this.particles;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Stop();
			}
		}

		// Token: 0x0400042C RID: 1068
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400042D RID: 1069
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400042E RID: 1070
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400042F RID: 1071
		[SerializeField]
		public string effectMuzzle;

		// Token: 0x04000430 RID: 1072
		[SerializeField]
		public GameObject openEffect;

		// Token: 0x04000431 RID: 1073
		[SerializeField]
		public string particleChildName;

		// Token: 0x04000432 RID: 1074
		private ParticleSystem[] particles;
	}
}
