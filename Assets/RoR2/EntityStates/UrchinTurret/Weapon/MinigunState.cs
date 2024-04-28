using System;
using RoR2;
using UnityEngine;

namespace EntityStates.UrchinTurret.Weapon
{
	// Token: 0x0200016E RID: 366
	public class MinigunState : BaseState
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0001B68E File Offset: 0x0001988E
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(MinigunState.muzzleName);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001B6A7 File Offset: 0x000198A7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.StartAimMode(2f, false);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001B6BB File Offset: 0x000198BB
		protected ref InputBankTest.ButtonState skillButtonState
		{
			get
			{
				return ref base.inputBank.skill1;
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040007BF RID: 1983
		public static string muzzleName;

		// Token: 0x040007C0 RID: 1984
		protected Transform muzzleTransform;
	}
}
