using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x020005A8 RID: 1448
	[RequireComponent(typeof(CharacterBody))]
	public class SpawnerPodsController : MonoBehaviour
	{
		// Token: 0x06001A22 RID: 6690 RVA: 0x00070C5E File Offset: 0x0006EE5E
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00070C6C File Offset: 0x0006EE6C
		private void Start()
		{
			this.ownerMaster = this.characterBody.master.minionOwnership.ownerMaster;
			if (NetworkServer.active)
			{
				Deployable component = base.GetComponent<Deployable>();
				if (this.ownerMaster)
				{
					this.ownerMaster.AddDeployable(component, DeployableSlot.ParentPodAlly);
				}
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00070CBC File Offset: 0x0006EEBC
		public void UndeployKill()
		{
			this.characterBody.healthComponent.Suicide(null, null, DamageType.Generic);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00070CD4 File Offset: 0x0006EED4
		public void Dissolve()
		{
			PrintController printController = this.characterBody.modelLocator.modelTransform.gameObject.AddComponent<PrintController>();
			printController.printTime = this.dissolveDuration;
			printController.enabled = true;
			printController.startingPrintHeight = 99999f;
			printController.maxPrintHeight = 99999f;
			printController.startingPrintBias = 0.95f;
			printController.maxPrintBias = 1.95f;
			printController.animateFlowmapPower = true;
			printController.startingFlowmapPower = 1.14f;
			printController.maxFlowmapPower = 30f;
			printController.disableWhenFinished = false;
			printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}

		// Token: 0x04002058 RID: 8280
		[FormerlySerializedAs("maxSpawnTimer")]
		public float incubationDuration;

		// Token: 0x04002059 RID: 8281
		public GameObject spawnEffect;

		// Token: 0x0400205A RID: 8282
		public GameObject hatchEffect;

		// Token: 0x0400205B RID: 8283
		[FormerlySerializedAs("dissolveTime")]
		public float dissolveDuration;

		// Token: 0x0400205C RID: 8284
		public string podSpawnSound = "";

		// Token: 0x0400205D RID: 8285
		public string podHatchSound = "";

		// Token: 0x0400205E RID: 8286
		private CharacterMaster ownerMaster;

		// Token: 0x0400205F RID: 8287
		private CharacterBody characterBody;
	}
}
