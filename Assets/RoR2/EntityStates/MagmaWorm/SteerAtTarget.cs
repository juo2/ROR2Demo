using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MagmaWorm
{
	// Token: 0x02000303 RID: 771
	public class SteerAtTarget : BaseSkillState
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003A028 File Offset: 0x00038228
		public override void OnEnter()
		{
			base.OnEnter();
			this.wormBodyPositionsDriver = base.GetComponent<WormBodyPositionsDriver>();
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				RaycastHit raycastHit;
				if (Util.CharacterRaycast(base.gameObject, aimRay, out raycastHit, 1000f, LayerIndex.CommonMasks.bullet, QueryTriggerInteraction.UseGlobal))
				{
					this.targetPosition = new Vector3?(raycastHit.point);
				}
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003A084 File Offset: 0x00038284
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.targetPosition != null)
			{
				Vector3 vector = this.targetPosition.Value - this.wormBodyPositionsDriver.chaserPosition;
				if (vector != Vector3.zero && this.wormBodyPositionsDriver.chaserVelocity != Vector3.zero)
				{
					Vector3 normalized = vector.normalized;
					Vector3 normalized2 = this.wormBodyPositionsDriver.chaserVelocity.normalized;
					float num = Vector3.Dot(normalized, normalized2);
					float num2 = 0f;
					if (num >= SteerAtTarget.slowTurnThreshold)
					{
						num2 = SteerAtTarget.slowTurnRate;
						if (num >= SteerAtTarget.fastTurnThreshold)
						{
							num2 = SteerAtTarget.fastTurnRate;
						}
					}
					if (num2 != 0f)
					{
						this.wormBodyPositionsDriver.chaserVelocity = Vector3.RotateTowards(this.wormBodyPositionsDriver.chaserVelocity, vector, 0.017453292f * num2 * Time.fixedDeltaTime, 0f);
					}
				}
			}
			if (base.isAuthority && !base.IsKeyDownAuthority())
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003A18A File Offset: 0x0003838A
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			if (this.targetPosition != null)
			{
				writer.Write(true);
				writer.Write(this.targetPosition.Value);
				return;
			}
			writer.Write(false);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003A1C0 File Offset: 0x000383C0
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			if (reader.ReadBoolean())
			{
				this.targetPosition = new Vector3?(reader.ReadVector3());
				return;
			}
			this.targetPosition = null;
		}

		// Token: 0x040010E0 RID: 4320
		private WormBodyPositionsDriver wormBodyPositionsDriver;

		// Token: 0x040010E1 RID: 4321
		private Vector3? targetPosition;

		// Token: 0x040010E2 RID: 4322
		private static readonly float fastTurnThreshold = Mathf.Cos(0.5235988f);

		// Token: 0x040010E3 RID: 4323
		private static readonly float slowTurnThreshold = Mathf.Cos(1.0471976f);

		// Token: 0x040010E4 RID: 4324
		private static readonly float fastTurnRate = 180f;

		// Token: 0x040010E5 RID: 4325
		private static readonly float slowTurnRate = 90f;
	}
}
