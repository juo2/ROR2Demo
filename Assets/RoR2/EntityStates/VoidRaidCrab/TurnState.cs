using System;
using RoR2.VoidRaidCrab;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000126 RID: 294
	public class TurnState : GenericCharacterMain
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x000163A0 File Offset: 0x000145A0
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.centralLegController = base.GetComponent<CentralLegController>();
				if (this.centralLegController)
				{
					this.suppressBreaksRequest = this.centralLegController.SuppressBreaks();
				}
				float dirToAimDegrees = this.GetDirToAimDegrees();
				if (dirToAimDegrees >= this.turnDegrees)
				{
					this.isClockwiseTurn = true;
					this.canTurn = true;
				}
				else if (dirToAimDegrees <= -this.turnDegrees)
				{
					this.isClockwiseTurn = false;
					this.canTurn = true;
				}
			}
			if (!this.canTurn)
			{
				if (base.isAuthority)
				{
					this.outer.SetNextStateToMain();
				}
				return;
			}
			if (this.isClockwiseTurn)
			{
				base.PlayAnimation(this.animationLayerName, this.clockwiseAnimationStateName, this.animationPlaybackRateParam, this.duration);
				return;
			}
			base.PlayAnimation(this.animationLayerName, this.counterClockwiseAnimationStateName, this.animationPlaybackRateParam, this.duration);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001647E File Offset: 0x0001467E
		public override void OnExit()
		{
			base.OnExit();
			CentralLegController.SuppressBreaksRequest suppressBreaksRequest = this.suppressBreaksRequest;
			if (suppressBreaksRequest == null)
			{
				return;
			}
			suppressBreaksRequest.Dispose();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00016498 File Offset: 0x00014698
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				if (Mathf.Abs(this.GetDirToAimDegrees()) >= this.turnDegrees && this.centralLegController && !this.centralLegController.AreAnyBreaksPending() && this.turnCount < this.maxNumConsecutiveTurns)
				{
					TurnState turnState = new TurnState();
					turnState.turnCount = this.turnCount + 1;
					this.outer.SetNextState(turnState);
					return;
				}
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00016528 File Offset: 0x00014728
		public float GetDirToAimDegrees()
		{
			float num = 0f;
			if (base.inputBank && base.characterDirection)
			{
				Vector3 aimDirection = base.inputBank.aimDirection;
				aimDirection.y = 0f;
				aimDirection.Normalize();
				Vector3 forward = base.characterDirection.forward;
				forward.y = 0f;
				forward.Normalize();
				num = Mathf.Acos(Vector3.Dot(forward, aimDirection)) * 57.29578f;
				if (Vector3.Dot(Vector3.Cross(forward, aimDirection), Vector3.up) < 0f)
				{
					num *= -1f;
				}
			}
			return num;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000165C7 File Offset: 0x000147C7
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.isClockwiseTurn);
			writer.Write(this.canTurn);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000165E8 File Offset: 0x000147E8
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.isClockwiseTurn = reader.ReadBoolean();
			this.canTurn = reader.ReadBoolean();
		}

		// Token: 0x04000614 RID: 1556
		[SerializeField]
		public float duration = 1f;

		// Token: 0x04000615 RID: 1557
		[SerializeField]
		public float turnDegrees = 22.5f;

		// Token: 0x04000616 RID: 1558
		[SerializeField]
		public int maxNumConsecutiveTurns;

		// Token: 0x04000617 RID: 1559
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000618 RID: 1560
		[SerializeField]
		public string clockwiseAnimationStateName;

		// Token: 0x04000619 RID: 1561
		[SerializeField]
		public string counterClockwiseAnimationStateName;

		// Token: 0x0400061A RID: 1562
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400061B RID: 1563
		private CentralLegController centralLegController;

		// Token: 0x0400061C RID: 1564
		private CentralLegController.SuppressBreaksRequest suppressBreaksRequest;

		// Token: 0x0400061D RID: 1565
		private int turnCount = 1;

		// Token: 0x0400061E RID: 1566
		private bool isClockwiseTurn;

		// Token: 0x0400061F RID: 1567
		private bool canTurn;
	}
}
