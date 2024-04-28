using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;

namespace EntityStates.SpectatorSkills
{
	// Token: 0x020001C4 RID: 452
	public class LockOn : BaseSkillState
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x000223D8 File Offset: 0x000205D8
		public override void OnEnter()
		{
			base.OnEnter();
			RaycastHit raycastHit;
			if (base.inputBank.GetAimRaycast(float.PositiveInfinity, out raycastHit))
			{
				this.targetPoint = raycastHit.point;
			}
			else
			{
				this.outer.SetNextStateToMain();
			}
			RoR2Application.onUpdate += this.LookAtTarget;
			RoR2Application.onLateUpdate += this.LookAtTarget;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002243B File Offset: 0x0002063B
		public override void OnExit()
		{
			RoR2Application.onLateUpdate -= this.LookAtTarget;
			RoR2Application.onUpdate -= this.LookAtTarget;
			base.OnExit();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00022465 File Offset: 0x00020665
		public override void Update()
		{
			base.Update();
			if (base.isAuthority && !base.IsKeyDownAuthority())
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00022488 File Offset: 0x00020688
		private void LookAtTarget()
		{
			ReadOnlyCollection<CameraRigController> readOnlyInstancesList = CameraRigController.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				readOnlyInstancesList[i].target == base.gameObject;
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000986 RID: 2438
		private Vector3 targetPoint;
	}
}
