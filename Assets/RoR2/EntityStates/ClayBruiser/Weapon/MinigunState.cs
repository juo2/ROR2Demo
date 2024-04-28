using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ClayBruiser.Weapon
{
	// Token: 0x020003FF RID: 1023
	public class MinigunState : BaseState
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x00052277 File Offset: 0x00050477
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(MinigunState.muzzleName);
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.AddBuff(MinigunState.slowBuff);
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0001B6A7 File Offset: 0x000198A7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.StartAimMode(2f, false);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000522B4 File Offset: 0x000504B4
		public override void OnExit()
		{
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.RemoveBuff(MinigunState.slowBuff);
			}
			base.OnExit();
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0001B6BB File Offset: 0x000198BB
		protected ref InputBankTest.ButtonState skillButtonState
		{
			get
			{
				return ref base.inputBank.skill1;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040017AB RID: 6059
		public static string muzzleName;

		// Token: 0x040017AC RID: 6060
		private static readonly BuffDef slowBuff = RoR2Content.Buffs.Slow50;

		// Token: 0x040017AD RID: 6061
		protected Transform muzzleTransform;
	}
}
