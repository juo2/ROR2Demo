using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000191 RID: 401
	public static class BaseToolbotPrimarySkillStateMethods
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0001E5AC File Offset: 0x0001C7AC
		public static void OnEnter<T>(T state, GameObject gameObject, SkillLocator skillLocator, ChildLocator modelChildLocator) where T : BaseState, IToolbotPrimarySkillState
		{
			state.currentHand = 0;
			state.isInDualWield = (EntityStateMachine.FindByCustomName(gameObject, "Body").state is ToolbotDualWield);
			state.muzzleName = state.baseMuzzleName;
			state.skillDef = state.activatorSkillSlot.skillDef;
			if (state.isInDualWield)
			{
				if (state.activatorSkillSlot == skillLocator.primary)
				{
					state.currentHand = -1;
					state.muzzleName = "DualWieldMuzzleL";
				}
				else if (state.activatorSkillSlot == skillLocator.secondary)
				{
					state.currentHand = 1;
					state.muzzleName = "DualWieldMuzzleR";
				}
			}
			if (state.muzzleName != null)
			{
				state.muzzleTransform = modelChildLocator.FindChild(state.muzzleName);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		public static void PlayGenericFireAnim<T>(T state, GameObject gameObject, SkillLocator skillLocator, float duration) where T : BaseState, IToolbotPrimarySkillState
		{
			state.currentHand = 0;
			if (state.activatorSkillSlot == skillLocator.primary)
			{
				state.currentHand = -1;
			}
			else if (state.activatorSkillSlot == skillLocator.secondary)
			{
				state.currentHand = 1;
			}
			int currentHand = state.currentHand;
			if (currentHand == -1)
			{
				state.PlayAnimation("Gesture, Additive", "DualWieldFire, Left");
				return;
			}
			if (currentHand != 1)
			{
				return;
			}
			state.PlayAnimation("Gesture, Additive Bonus", "DualWieldFire, Right");
		}
	}
}
