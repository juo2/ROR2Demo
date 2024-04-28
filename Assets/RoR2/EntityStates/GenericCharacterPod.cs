using System;

namespace EntityStates
{
	// Token: 0x020000C2 RID: 194
	public class GenericCharacterPod : BaseState
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0000EB00 File Offset: 0x0000CD00
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.enabled = false;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000EB3A File Offset: 0x0000CD3A
		public override void OnExit()
		{
			if (base.characterMotor)
			{
				base.characterMotor.enabled = true;
			}
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.enabled = true;
			}
			base.OnExit();
		}
	}
}
