using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000D0 RID: 208
	public class BaseMelee : BaseState
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000F898 File Offset: 0x0000DA98
		public RootMotionAccumulator InitMeleeRootMotion()
		{
			this.rootMotionAccumulator = base.GetModelRootMotionAccumulator();
			if (this.rootMotionAccumulator)
			{
				this.rootMotionAccumulator.ExtractRootMotion();
			}
			if (base.characterDirection)
			{
				base.characterDirection.forward = base.inputBank.aimDirection;
			}
			if (base.characterMotor)
			{
				base.characterMotor.moveDirection = Vector3.zero;
			}
			return this.rootMotionAccumulator;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000F910 File Offset: 0x0000DB10
		public void UpdateMeleeRootMotion(float scale)
		{
			if (this.rootMotionAccumulator)
			{
				Vector3 a = this.rootMotionAccumulator.ExtractRootMotion();
				if (base.characterMotor)
				{
					base.characterMotor.rootMotion = a * scale;
				}
			}
		}

		// Token: 0x040003CA RID: 970
		protected RootMotionAccumulator rootMotionAccumulator;
	}
}
