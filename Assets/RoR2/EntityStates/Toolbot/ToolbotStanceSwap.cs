using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x020001AD RID: 429
	public class ToolbotStanceSwap : ToolbotStanceBase
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00021010 File Offset: 0x0001F210
		public override void OnEnter()
		{
			base.OnEnter();
			float num = this.baseDuration / this.attackSpeedStat;
			this.endTime = Run.FixedTimeStamp.now + num;
			GenericSkill primarySkill = base.GetPrimarySkill1();
			GenericSkill primarySkill2 = base.GetPrimarySkill2();
			if (this.previousStanceState != typeof(ToolbotStanceA))
			{
				Util.Swap<GenericSkill>(ref primarySkill, ref primarySkill2);
			}
			ToolbotWeaponSkillDef toolbotWeaponSkillDef;
			if (primarySkill2 && (toolbotWeaponSkillDef = (primarySkill2.skillDef as ToolbotWeaponSkillDef)) != null)
			{
				base.SendWeaponStanceToAnimator(toolbotWeaponSkillDef);
				Util.PlaySound(toolbotWeaponSkillDef.entrySound, base.gameObject);
			}
			ToolbotWeaponSkillDef toolbotWeaponSkillDef2;
			if (primarySkill && (toolbotWeaponSkillDef2 = (primarySkill.skillDef as ToolbotWeaponSkillDef)) != null)
			{
				base.PlayAnimation("Stance, Additive", toolbotWeaponSkillDef2.exitAnimState, "StanceSwap.playbackRate", num * 0.5f);
				this.PlayAnimation("Gesture, Additive", toolbotWeaponSkillDef2.exitGestureAnimState);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.endTime.hasPassed)
			{
				this.outer.SetNextState(EntityStateCatalog.InstantiateState(this.nextStanceState));
				return;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002111C File Offset: 0x0001F31C
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			EntityStateIndex stateIndex = EntityStateCatalog.GetStateIndex(this.previousStanceState);
			EntityStateIndex stateIndex2 = EntityStateCatalog.GetStateIndex(this.nextStanceState);
			writer.Write(stateIndex);
			writer.Write(stateIndex2);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00021158 File Offset: 0x0001F358
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			EntityStateIndex entityStateIndex = reader.ReadEntityStateIndex();
			EntityStateIndex entityStateIndex2 = reader.ReadEntityStateIndex();
			this.previousStanceState = EntityStateCatalog.GetStateType(entityStateIndex);
			this.nextStanceState = EntityStateCatalog.GetStateType(entityStateIndex2);
		}

		// Token: 0x0400094C RID: 2380
		[SerializeField]
		private float baseDuration = 0.5f;

		// Token: 0x0400094D RID: 2381
		private Run.FixedTimeStamp endTime;

		// Token: 0x0400094E RID: 2382
		public Type nextStanceState;

		// Token: 0x0400094F RID: 2383
		public Type previousStanceState;
	}
}
