using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200011A RID: 282
	public class EscapeDeath : GenericCharacterDeath
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000155FC File Offset: 0x000137FC
		protected override void PlayDeathAnimation(float crossfadeDuration)
		{
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration, crossfadeDuration);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00015620 File Offset: 0x00013820
		public override void OnEnter()
		{
			base.OnEnter();
			this.netId = NetworkInstanceId.Invalid;
			if (base.characterBody && base.characterBody.master)
			{
				this.netId = base.characterBody.master.netId;
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.addPrintController)
			{
				Transform modelTransform = base.GetModelTransform();
				if (modelTransform)
				{
					PrintController printController = modelTransform.gameObject.AddComponent<PrintController>();
					printController.printTime = this.printDuration;
					printController.enabled = true;
					printController.startingPrintHeight = this.startingPrintHeight;
					printController.maxPrintHeight = this.maxPrintHeight;
					printController.startingPrintBias = 0f;
					printController.maxPrintBias = 0f;
					printController.disableWhenFinished = false;
					printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
				}
			}
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = Vector3.zero;
			}
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				this.gauntletEntranceTransform = modelChildLocator.FindChild(this.gauntletEntranceChildName);
				this.RefreshGauntletEntrancePosition();
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00015754 File Offset: 0x00013954
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				this.RefreshGauntletEntrancePosition();
				VoidRaidGauntletController.instance.TryOpenGauntlet(this.gauntletEntrancePosition, this.netId);
			}
			base.OnExit();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00015780 File Offset: 0x00013980
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.RefreshGauntletEntrancePosition();
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000157B6 File Offset: 0x000139B6
		private void RefreshGauntletEntrancePosition()
		{
			if (this.gauntletEntranceTransform)
			{
				this.gauntletEntrancePosition = this.gauntletEntranceTransform.position;
			}
		}

		// Token: 0x040005BD RID: 1469
		[SerializeField]
		public float duration;

		// Token: 0x040005BE RID: 1470
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005BF RID: 1471
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005C0 RID: 1472
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040005C1 RID: 1473
		[SerializeField]
		public bool addPrintController;

		// Token: 0x040005C2 RID: 1474
		[SerializeField]
		public float printDuration;

		// Token: 0x040005C3 RID: 1475
		[SerializeField]
		public float startingPrintHeight;

		// Token: 0x040005C4 RID: 1476
		[SerializeField]
		public float maxPrintHeight;

		// Token: 0x040005C5 RID: 1477
		[SerializeField]
		public string gauntletEntranceChildName;

		// Token: 0x040005C6 RID: 1478
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040005C7 RID: 1479
		private Vector3 gauntletEntrancePosition;

		// Token: 0x040005C8 RID: 1480
		private Transform gauntletEntranceTransform;

		// Token: 0x040005C9 RID: 1481
		private NetworkInstanceId netId;
	}
}
