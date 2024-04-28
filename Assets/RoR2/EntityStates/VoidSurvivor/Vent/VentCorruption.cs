using System;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Vent
{
	// Token: 0x0200010E RID: 270
	public class VentCorruption : GenericCharacterMain
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x00014424 File Offset: 0x00012624
		public override void OnEnter()
		{
			base.OnEnter();
			this.voidSurvivorController = base.GetComponent<VoidSurvivorController>();
			this.voidSurvivorController = base.GetComponent<VoidSurvivorController>();
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.PlayCrossfade(this.animationLayerName, this.enterAnimationStateName, this.animationCrossfadeDuration);
			this.healPerTick = base.healthComponent.fullHealth * this.healingPercentagePerSecond / this.healingTickRate;
			this.corruptionReductionPerTick = this.corruptionReductionPerSecond / this.healingTickRate;
			Transform transform = base.FindModelChild(this.leftVentEffectChildLocatorEntry);
			if (transform != null)
			{
				transform.gameObject.SetActive(true);
			}
			Transform transform2 = base.FindModelChild(this.rightVentEffectChildLocatorEntry);
			if (transform2 != null)
			{
				transform2.gameObject.SetActive(true);
			}
			Transform transform3 = base.FindModelChild(this.miniVentEffectChildLocatorEntry);
			if (transform3 != null)
			{
				transform3.gameObject.SetActive(true);
			}
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			if (base.characterDirection)
			{
				this.previousTurnSpeed = base.characterDirection.turnSpeed;
				base.characterDirection.turnSpeed = this.turnSpeed;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00014558 File Offset: 0x00012758
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(1f);
			if (NetworkServer.active)
			{
				this.healTickStopwatch += Time.fixedDeltaTime;
				if (this.healTickStopwatch > 1f / this.healingTickRate)
				{
					this.healTickStopwatch -= 1f / this.healingTickRate;
					base.healthComponent.Heal(this.healPerTick, default(ProcChainMask), true);
					if (this.voidSurvivorController)
					{
						this.voidSurvivorController.AddCorruption(-this.corruptionReductionPerTick);
					}
				}
			}
			if (base.isAuthority)
			{
				if (base.characterMotor)
				{
					float num = base.characterMotor.velocity.y;
					if (num < this.hoverVelocity)
					{
						num = Mathf.MoveTowards(num, this.hoverVelocity, this.hoverAcceleration * Time.fixedDeltaTime);
						base.characterMotor.velocity = new Vector3(base.characterMotor.velocity.x, num, base.characterMotor.velocity.z);
					}
				}
				if (base.fixedAge >= this.maximumDuration || (base.fixedAge >= this.minimumDuration && this.voidSurvivorController && this.voidSurvivorController.corruption <= this.voidSurvivorController.minimumCorruption))
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool CanExecuteSkill(GenericSkill skillSlot)
		{
			return false;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000146C4 File Offset: 0x000128C4
		public override void OnExit()
		{
			if (base.characterDirection)
			{
				base.characterDirection.turnSpeed = this.previousTurnSpeed;
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			Transform transform = base.FindModelChild(this.leftVentEffectChildLocatorEntry);
			if (transform != null)
			{
				transform.gameObject.SetActive(false);
			}
			Transform transform2 = base.FindModelChild(this.rightVentEffectChildLocatorEntry);
			if (transform2 != null)
			{
				transform2.gameObject.SetActive(false);
			}
			Transform transform3 = base.FindModelChild(this.miniVentEffectChildLocatorEntry);
			if (transform3 != null)
			{
				transform3.gameObject.SetActive(false);
			}
			base.PlayCrossfade(this.animationLayerName, this.exitAnimationStateName, this.animationCrossfadeDuration);
			Util.PlaySound(this.exitSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400055B RID: 1371
		[SerializeField]
		public float minimumDuration;

		// Token: 0x0400055C RID: 1372
		[SerializeField]
		public float maximumDuration;

		// Token: 0x0400055D RID: 1373
		[SerializeField]
		public string leftVentEffectChildLocatorEntry;

		// Token: 0x0400055E RID: 1374
		[SerializeField]
		public string rightVentEffectChildLocatorEntry;

		// Token: 0x0400055F RID: 1375
		[SerializeField]
		public string miniVentEffectChildLocatorEntry;

		// Token: 0x04000560 RID: 1376
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000561 RID: 1377
		[SerializeField]
		public string exitSoundString;

		// Token: 0x04000562 RID: 1378
		[SerializeField]
		public float animationCrossfadeDuration;

		// Token: 0x04000563 RID: 1379
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000564 RID: 1380
		[SerializeField]
		public string enterAnimationStateName;

		// Token: 0x04000565 RID: 1381
		[SerializeField]
		public string exitAnimationStateName;

		// Token: 0x04000566 RID: 1382
		[SerializeField]
		public float hoverVelocity;

		// Token: 0x04000567 RID: 1383
		[SerializeField]
		public float hoverAcceleration;

		// Token: 0x04000568 RID: 1384
		[SerializeField]
		public float healingPercentagePerSecond;

		// Token: 0x04000569 RID: 1385
		[SerializeField]
		public float healingTickRate;

		// Token: 0x0400056A RID: 1386
		[SerializeField]
		public float corruptionReductionPerSecond;

		// Token: 0x0400056B RID: 1387
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x0400056C RID: 1388
		[SerializeField]
		public float turnSpeed;

		// Token: 0x0400056D RID: 1389
		private float healPerTick;

		// Token: 0x0400056E RID: 1390
		private float healTickStopwatch;

		// Token: 0x0400056F RID: 1391
		private float corruptionReductionPerTick;

		// Token: 0x04000570 RID: 1392
		private Vector3 liftVector = Vector3.up;

		// Token: 0x04000571 RID: 1393
		private VoidSurvivorController voidSurvivorController;

		// Token: 0x04000572 RID: 1394
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000573 RID: 1395
		private float previousTurnSpeed;
	}
}
