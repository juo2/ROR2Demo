using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.MagmaWorm
{
	// Token: 0x02000304 RID: 772
	public class SwitchStance : BaseState
	{
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003A223 File Offset: 0x00038423
		public override void OnEnter()
		{
			base.OnEnter();
			this.SetStanceParameters(true);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003A232 File Offset: 0x00038432
		public override void OnExit()
		{
			base.OnExit();
			this.SetStanceParameters(false);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003A241 File Offset: 0x00038441
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SwitchStance.leapingDuration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003A264 File Offset: 0x00038464
		private void SetStanceParameters(bool leaping)
		{
			if (NetworkServer.active)
			{
				WormBodyPositions2 component = base.GetComponent<WormBodyPositions2>();
				WormBodyPositionsDriver component2 = base.GetComponent<WormBodyPositionsDriver>();
				if (!component)
				{
					return;
				}
				if (leaping)
				{
					component2.ySpringConstant = SwitchStance.leapStanceSpring;
					component2.yDamperConstant = SwitchStance.leapStanceDamping;
					component.speedMultiplier = SwitchStance.leapStanceSpeedMultiplier;
					component2.allowShoving = false;
				}
				else
				{
					component2.ySpringConstant = SwitchStance.groundStanceSpring;
					component2.yDamperConstant = SwitchStance.groundStanceDamping;
					component.speedMultiplier = SwitchStance.groundStanceSpeedMultiplier;
					component2.allowShoving = true;
				}
				component.shouldFireMeatballsOnImpact = leaping;
			}
		}

		// Token: 0x040010E6 RID: 4326
		public static float leapingDuration = 10f;

		// Token: 0x040010E7 RID: 4327
		public static float groundStanceSpring = 3f;

		// Token: 0x040010E8 RID: 4328
		public static float groundStanceDamping = 3f;

		// Token: 0x040010E9 RID: 4329
		public static float groundStanceSpeedMultiplier = 1.5f;

		// Token: 0x040010EA RID: 4330
		public static float leapStanceSpring = 3f;

		// Token: 0x040010EB RID: 4331
		public static float leapStanceDamping = 1f;

		// Token: 0x040010EC RID: 4332
		public static float leapStanceSpeedMultiplier = 1f;
	}
}
