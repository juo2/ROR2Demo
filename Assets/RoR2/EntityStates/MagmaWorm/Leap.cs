using System;
using UnityEngine;

namespace EntityStates.MagmaWorm
{
	// Token: 0x02000301 RID: 769
	public class Leap : BaseState
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x00039D76 File Offset: 0x00037F76
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelBaseTransform = base.GetModelBaseTransform();
			this.leapState = Leap.LeapState.Burrow;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00039D94 File Offset: 0x00037F94
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			switch (this.leapState)
			{
			case Leap.LeapState.Burrow:
				if (this.modelBaseTransform)
				{
					if (this.modelBaseTransform.position.y >= base.transform.position.y - this.diveDepth)
					{
						this.velocity = Vector3.MoveTowards(this.velocity, this.idealDiveVelocity, this.leapAcceleration * Time.fixedDeltaTime);
						this.modelBaseTransform.position += this.velocity * Time.fixedDeltaTime;
						return;
					}
					this.leapState = Leap.LeapState.Ascend;
					return;
				}
				break;
			case Leap.LeapState.Ascend:
				if (this.modelBaseTransform)
				{
					if (this.modelBaseTransform.position.y <= base.transform.position.y)
					{
						this.velocity = Vector3.MoveTowards(this.velocity, this.idealLeapVelocity, this.leapAcceleration * Time.fixedDeltaTime);
						this.modelBaseTransform.position += this.velocity * Time.fixedDeltaTime;
						return;
					}
					this.leapState = Leap.LeapState.Fall;
					return;
				}
				break;
			case Leap.LeapState.Fall:
				if (this.modelBaseTransform)
				{
					if (this.modelBaseTransform.position.y >= base.transform.position.y - this.diveDepth)
					{
						this.velocity += Physics.gravity * Time.fixedDeltaTime;
						this.modelBaseTransform.position += this.velocity * Time.fixedDeltaTime;
						return;
					}
					this.leapState = Leap.LeapState.Resurface;
					return;
				}
				break;
			case Leap.LeapState.Resurface:
				this.velocity = Vector3.zero;
				this.modelBaseTransform.position = Vector3.MoveTowards(this.modelBaseTransform.position, base.transform.position, this.resurfaceSpeed * Time.fixedDeltaTime);
				if (this.modelBaseTransform.position.y >= base.transform.position.y)
				{
					this.outer.SetNextStateToMain();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040010D3 RID: 4307
		private Transform modelBaseTransform;

		// Token: 0x040010D4 RID: 4308
		private readonly float diveDepth = 200f;

		// Token: 0x040010D5 RID: 4309
		private readonly Vector3 idealDiveVelocity = Vector3.down * 90f;

		// Token: 0x040010D6 RID: 4310
		private readonly Vector3 idealLeapVelocity = Vector3.up * 90f;

		// Token: 0x040010D7 RID: 4311
		private float leapAcceleration = 80f;

		// Token: 0x040010D8 RID: 4312
		private float resurfaceSpeed = 60f;

		// Token: 0x040010D9 RID: 4313
		private Vector3 velocity;

		// Token: 0x040010DA RID: 4314
		private Leap.LeapState leapState;

		// Token: 0x02000302 RID: 770
		private enum LeapState
		{
			// Token: 0x040010DC RID: 4316
			Burrow,
			// Token: 0x040010DD RID: 4317
			Ascend,
			// Token: 0x040010DE RID: 4318
			Fall,
			// Token: 0x040010DF RID: 4319
			Resurface
		}
	}
}
