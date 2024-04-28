using System;
using UnityEngine;

namespace EntityStates.Mage
{
	// Token: 0x02000297 RID: 663
	public class JetpackOn : BaseState
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x00030B60 File Offset: 0x0002ED60
		public override void OnEnter()
		{
			base.OnEnter();
			this.jetOnEffect = base.FindModelChild("JetOn");
			if (this.jetOnEffect)
			{
				this.jetOnEffect.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00030B98 File Offset: 0x0002ED98
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				float num = base.characterMotor.velocity.y;
				num = Mathf.MoveTowards(num, JetpackOn.hoverVelocity, JetpackOn.hoverAcceleration * Time.fixedDeltaTime);
				base.characterMotor.velocity = new Vector3(base.characterMotor.velocity.x, num, base.characterMotor.velocity.z);
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00030C0C File Offset: 0x0002EE0C
		public override void OnExit()
		{
			base.OnExit();
			if (this.jetOnEffect)
			{
				this.jetOnEffect.gameObject.SetActive(false);
			}
		}

		// Token: 0x04000DE3 RID: 3555
		public static float hoverVelocity;

		// Token: 0x04000DE4 RID: 3556
		public static float hoverAcceleration;

		// Token: 0x04000DE5 RID: 3557
		private Transform jetOnEffect;
	}
}
