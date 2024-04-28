using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates.Headstompers
{
	// Token: 0x0200032D RID: 813
	public class BaseHeadstompersState : EntityState
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0003EF78 File Offset: 0x0003D178
		public static BaseHeadstompersState FindForBody(CharacterBody body)
		{
			for (int i = 0; i < BaseHeadstompersState.instancesList.Count; i++)
			{
				if (BaseHeadstompersState.instancesList[i].body == body)
				{
					return BaseHeadstompersState.instancesList[i];
				}
			}
			return null;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003EFBA File Offset: 0x0003D1BA
		protected bool jumpButtonDown
		{
			get
			{
				return this.bodyInputBank && this.bodyInputBank.jump.down;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0003EFDB File Offset: 0x0003D1DB
		protected bool slamButtonDown
		{
			get
			{
				return this.bodyInputBank && this.bodyInputBank.interact.down;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0003EFFC File Offset: 0x0003D1FC
		protected bool isGrounded
		{
			get
			{
				return this.bodyMotor && this.bodyMotor.isGrounded;
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003F018 File Offset: 0x0003D218
		public override void OnEnter()
		{
			base.OnEnter();
			BaseHeadstompersState.instancesList.Add(this);
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			if (this.networkedBodyAttachment)
			{
				this.bodyGameObject = this.networkedBodyAttachment.attachedBodyObject;
				this.body = this.networkedBodyAttachment.attachedBody;
				if (this.bodyGameObject)
				{
					this.bodyMotor = this.bodyGameObject.GetComponent<CharacterMotor>();
					this.bodyInputBank = this.bodyGameObject.GetComponent<InputBankTest>();
				}
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003F0A0 File Offset: 0x0003D2A0
		public override void OnExit()
		{
			BaseHeadstompersState.instancesList.Remove(this);
			base.OnExit();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003F0B4 File Offset: 0x0003D2B4
		protected bool ReturnToIdleIfGroundedAuthority()
		{
			if (this.bodyMotor && this.bodyMotor.isGrounded)
			{
				this.outer.SetNextState(new HeadstompersIdle());
				return true;
			}
			return false;
		}

		// Token: 0x0400124D RID: 4685
		private static readonly List<BaseHeadstompersState> instancesList = new List<BaseHeadstompersState>();

		// Token: 0x0400124E RID: 4686
		protected NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x0400124F RID: 4687
		protected GameObject bodyGameObject;

		// Token: 0x04001250 RID: 4688
		protected CharacterBody body;

		// Token: 0x04001251 RID: 4689
		protected CharacterMotor bodyMotor;

		// Token: 0x04001252 RID: 4690
		protected InputBankTest bodyInputBank;
	}
}
