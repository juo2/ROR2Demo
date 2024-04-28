using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ParentMonster
{
	// Token: 0x02000228 RID: 552
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00027DC0 File Offset: 0x00025FC0
		protected override bool shouldAutoDestroy
		{
			get
			{
				return this.destealth && base.fixedAge > this.timeBeforeDestealth + this.destealthDuration;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00027DE1 File Offset: 0x00025FE1
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00027DEC File Offset: 0x00025FEC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.timeBeforeDestealth && !this.destealth)
			{
				this.DoDestealth();
			}
			if (this.destealth && base.fixedAge > this.timeBeforeDestealth + this.destealthDuration)
			{
				base.DestroyModel();
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00027E40 File Offset: 0x00026040
		private void DoDestealth()
		{
			this.destealth = true;
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, this.effectMuzzleString, false);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				CharacterModel component = modelTransform.gameObject.GetComponent<CharacterModel>();
				if (this.destealthMaterial)
				{
					TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = this.destealthDuration;
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = this.destealthMaterial;
					temporaryOverlay.inspectorCharacterModel = component;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.animateShaderAlpha = true;
					PrintController component2 = base.modelLocator.modelTransform.gameObject.GetComponent<PrintController>();
					component2.enabled = false;
					component2.printTime = this.destealthDuration;
					component2.startingPrintHeight = 0f;
					component2.maxPrintHeight = 20f;
					component2.startingPrintBias = 0f;
					component2.maxPrintBias = 2f;
					component2.disableWhenFinished = false;
					component2.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
					component2.enabled = true;
				}
				Transform transform = base.FindModelChild("CoreLight");
				if (transform)
				{
					transform.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x04000B33 RID: 2867
		[SerializeField]
		public float timeBeforeDestealth = 2.5f;

		// Token: 0x04000B34 RID: 2868
		[SerializeField]
		public float destealthDuration;

		// Token: 0x04000B35 RID: 2869
		[SerializeField]
		public Material destealthMaterial;

		// Token: 0x04000B36 RID: 2870
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x04000B37 RID: 2871
		[SerializeField]
		public string effectMuzzleString;

		// Token: 0x04000B38 RID: 2872
		private bool destealth;
	}
}
