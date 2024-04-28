using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000119 RID: 281
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000153FC File Offset: 0x000135FC
		protected override void PlayDeathAnimation(float crossfadeDuration)
		{
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration, crossfadeDuration);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015420 File Offset: 0x00013620
		public override void OnEnter()
		{
			base.OnEnter();
			if (VoidRaidGauntletController.instance)
			{
				VoidRaidGauntletController.instance.SetCurrentDonutCombatDirectorEnabled(false);
			}
			this.modelTransform = base.GetModelTransform();
			Transform transform = base.FindModelChild("StandableSurface");
			if (transform)
			{
				transform.gameObject.SetActive(false);
			}
			if (this.explosionEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.initialEffectPrefab, base.gameObject, this.initialEffectMuzzle, false);
			}
			if (this.addPrintController)
			{
				PrintController printController = this.modelTransform.gameObject.AddComponent<PrintController>();
				printController.printTime = this.printDuration;
				printController.enabled = true;
				printController.startingPrintHeight = 99f;
				printController.maxPrintHeight = 99f;
				printController.startingPrintBias = this.startingPrintBias;
				printController.maxPrintBias = this.maxPrintBias;
				printController.disableWhenFinished = false;
				printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			}
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = Vector3.zero;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015538 File Offset: 0x00013738
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				if (this.explosionEffectPrefab)
				{
					EffectManager.SimpleMuzzleFlash(this.explosionEffectPrefab, base.gameObject, this.explosionEffectMuzzle, true);
				}
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001558C File Offset: 0x0001378C
		public override void OnExit()
		{
			if (this.modelTransform)
			{
				RagdollController component = this.modelTransform.GetComponent<RagdollController>();
				Rigidbody component2 = base.GetComponent<Rigidbody>();
				if (component && component2)
				{
					component.BeginRagdoll(this.ragdollForce);
				}
				ExplodeRigidbodiesOnStart component3 = this.modelTransform.GetComponent<ExplodeRigidbodiesOnStart>();
				if (component3)
				{
					component3.force = this.explosionForce;
					component3.enabled = true;
				}
			}
		}

		// Token: 0x040005AE RID: 1454
		[SerializeField]
		public float duration;

		// Token: 0x040005AF RID: 1455
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005B0 RID: 1456
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005B1 RID: 1457
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040005B2 RID: 1458
		[SerializeField]
		public GameObject initialEffectPrefab;

		// Token: 0x040005B3 RID: 1459
		[SerializeField]
		public string initialEffectMuzzle;

		// Token: 0x040005B4 RID: 1460
		[SerializeField]
		public GameObject explosionEffectPrefab;

		// Token: 0x040005B5 RID: 1461
		[SerializeField]
		public string explosionEffectMuzzle;

		// Token: 0x040005B6 RID: 1462
		[SerializeField]
		public Vector3 ragdollForce;

		// Token: 0x040005B7 RID: 1463
		[SerializeField]
		public float explosionForce;

		// Token: 0x040005B8 RID: 1464
		[SerializeField]
		public bool addPrintController;

		// Token: 0x040005B9 RID: 1465
		[SerializeField]
		public float printDuration;

		// Token: 0x040005BA RID: 1466
		[SerializeField]
		public float startingPrintBias;

		// Token: 0x040005BB RID: 1467
		[SerializeField]
		public float maxPrintBias;

		// Token: 0x040005BC RID: 1468
		private Transform modelTransform;
	}
}
