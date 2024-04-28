using System;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200043B RID: 1083
	public class SlideIntroState : BaseState
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x00056A90 File Offset: 0x00054C90
		public override void OnEnter()
		{
			base.OnEnter();
			bool flag = false;
			if (base.inputBank && base.isAuthority)
			{
				Vector3 normalized = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
				Vector3 forward = base.characterDirection.forward;
				Vector3 rhs = Vector3.Cross(Vector3.up, forward);
				float num = Vector3.Dot(normalized, forward);
				float num2 = Vector3.Dot(normalized, rhs);
				if (base.characterDirection)
				{
					base.characterDirection.moveVector = base.inputBank.aimDirection;
				}
				if (Mathf.Abs(num2) > Mathf.Abs(num))
				{
					if (num2 <= 0f)
					{
						flag = true;
						this.outer.SetNextState(new SlideLeftState());
					}
					else
					{
						flag = true;
						this.outer.SetNextState(new SlideRightState());
					}
				}
				else if (num <= 0f)
				{
					flag = true;
					this.outer.SetNextState(new SlideBackwardState());
				}
				else
				{
					flag = true;
					this.outer.SetNextState(new SlideForwardState());
				}
			}
			if (!flag)
			{
				this.outer.SetNextState(new SlideForwardState());
			}
		}
	}
}
