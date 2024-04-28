using System;
using RoR2;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

namespace EntityStates.ArtifactShell
{
	// Token: 0x0200048F RID: 1167
	public class ArtifactShellBaseState : BaseState
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0005CAE1 File Offset: 0x0005ACE1
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x0005CAE9 File Offset: 0x0005ACE9
		private protected PurchaseInteraction purchaseInteraction { protected get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual CostTypeIndex interactionCostType
		{
			get
			{
				return CostTypeIndex.None;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool interactionAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual int interactionCost
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0005CAF2 File Offset: 0x0005ACF2
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x0005CAFA File Offset: 0x0005ACFA
		private protected ParticleSystem rayParticleSystem { protected get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0005CB03 File Offset: 0x0005AD03
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0005CB0B File Offset: 0x0005AD0B
		private protected PostProcessVolume postProcessVolume { protected get; private set; }

		// Token: 0x060014E2 RID: 5346 RVA: 0x0005CB14 File Offset: 0x0005AD14
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			if (this.purchaseInteraction != null)
			{
				this.purchaseInteraction.costType = this.interactionCostType;
				this.purchaseInteraction.Networkcost = this.interactionCost;
				this.purchaseInteraction.Networkavailable = this.interactionAvailable;
				this.purchaseInteraction.onPurchase.AddListener(new UnityAction<Interactor>(this.DoOnPurchase));
			}
			Transform transform = base.FindModelChild("RayParticles");
			this.rayParticleSystem = ((transform != null) ? transform.GetComponent<ParticleSystem>() : null);
			Transform transform2 = base.FindModelChild("PP");
			this.postProcessVolume = ((transform2 != null) ? transform2.GetComponent<PostProcessVolume>() : null);
			this.light = base.FindModelChild("Light").GetComponent<Light>();
			string soundString;
			ArtifactShellBaseState.CalcLoopSounds(base.healthComponent.combinedHealthFraction, out soundString, out this.stopLoopSound);
			Util.PlaySound(soundString, base.gameObject);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0005CC00 File Offset: 0x0005AE00
		private static void CalcLoopSounds(float currentHealthFraction, out string startLoopSound, out string stopLoopSound)
		{
			startLoopSound = null;
			stopLoopSound = null;
			float num = 0.05f;
			if (currentHealthFraction > 0.75f + num)
			{
				startLoopSound = "Play_artifactBoss_loop_level1";
				stopLoopSound = "Stop_artifactBoss_loop_level1";
				return;
			}
			if (currentHealthFraction > 0.25f + num)
			{
				startLoopSound = "Play_artifactBoss_loop_level2";
				stopLoopSound = "Stop_artifactBoss_loop_level2";
				return;
			}
			if (currentHealthFraction > 0f + num)
			{
				startLoopSound = "Play_artifactBoss_loop_level2";
				stopLoopSound = "Stop_artifactBoss_loop_level2";
				return;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0005CC64 File Offset: 0x0005AE64
		public override void OnExit()
		{
			Util.PlaySound(this.stopLoopSound, base.gameObject);
			if (this.purchaseInteraction != null)
			{
				this.purchaseInteraction.onPurchase.RemoveListener(new UnityAction<Interactor>(this.DoOnPurchase));
				this.purchaseInteraction = null;
			}
			base.OnExit();
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0005CCB4 File Offset: 0x0005AEB4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.UpdateVisuals();
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0005CCC2 File Offset: 0x0005AEC2
		private void DoOnPurchase(Interactor activator)
		{
			this.OnPurchase(activator);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnPurchase(Interactor activator)
		{
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0005CCCC File Offset: 0x0005AECC
		protected void UpdateVisuals()
		{
			float num = 1f - base.healthComponent.combinedHealthFraction;
			if (this.rayParticleSystem)
			{
				this.rayParticleSystem.emission.rateOverTime = Util.Remap(num, 0f, 1f, 0f, 10f);
				this.rayParticleSystem.main.simulationSpeed = Util.Remap(num, 0f, 1f, 1f, 5f);
			}
			if (this.postProcessVolume)
			{
				this.postProcessVolume.weight = num;
			}
			if (this.light)
			{
				this.light.range = 10f + num * 150f;
			}
		}

		// Token: 0x04001AB8 RID: 6840
		protected Light light;

		// Token: 0x04001AB9 RID: 6841
		private string stopLoopSound;
	}
}
