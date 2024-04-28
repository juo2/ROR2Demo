using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.LaserTurbine
{
	// Token: 0x020002DA RID: 730
	public class RechargeState : LaserTurbineBaseState
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00036B00 File Offset: 0x00034D00
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00036B08 File Offset: 0x00034D08
		public Run.FixedTimeStamp startTime { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00036B11 File Offset: 0x00034D11
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00036B19 File Offset: 0x00034D19
		public Run.FixedTimeStamp readyTime { get; private set; }

		// Token: 0x06000D06 RID: 3334 RVA: 0x00036B22 File Offset: 0x00034D22
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.startTime = Run.FixedTimeStamp.now;
				this.readyTime = this.startTime + RechargeState.baseDuration;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00036B54 File Offset: 0x00034D54
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.ownerBody.GetBuffCount(RoR2Content.Buffs.LaserTurbineKillCharge) >= RechargeState.killChargesRequired)
			{
				if (NetworkServer.active)
				{
					base.laserTurbineController.ExpendCharge();
				}
				this.outer.SetNextState(new ReadyState());
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00036BA8 File Offset: 0x00034DA8
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.startTime);
			writer.Write(this.readyTime);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00036BC9 File Offset: 0x00034DC9
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.startTime = reader.ReadFixedTimeStamp();
			this.readyTime = reader.ReadFixedTimeStamp();
		}

		// Token: 0x04000FE2 RID: 4066
		public static float baseDuration = 60f;

		// Token: 0x04000FE3 RID: 4067
		public static int killChargesRequired = 4;

		// Token: 0x04000FE4 RID: 4068
		public static int killChargeDuration = 4;
	}
}
