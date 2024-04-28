using System;
using EntityStates;
using EntityStates.Merc;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C0E RID: 3086
	[CreateAssetMenu(menuName = "RoR2/SkillDef/MercDashSkillDef")]
	public class MercDashSkillDef : SkillDef
	{
		// Token: 0x060045D3 RID: 17875 RVA: 0x00121DC5 File Offset: 0x0011FFC5
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new MercDashSkillDef.InstanceData();
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x00121DCC File Offset: 0x0011FFCC
		public override void OnUnassigned([NotNull] GenericSkill skillSlot)
		{
			base.OnUnassigned(skillSlot);
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x00121DD8 File Offset: 0x0011FFD8
		public override void OnFixedUpdate([NotNull] GenericSkill skillSlot)
		{
			base.OnFixedUpdate(skillSlot);
			MercDashSkillDef.InstanceData instanceData = (MercDashSkillDef.InstanceData)skillSlot.skillInstanceData;
			Assaulter2 assaulter;
			if (instanceData.waitingForHit && (assaulter = (skillSlot.stateMachine.state as Assaulter2)) != null && assaulter.grantAnotherDash)
			{
				instanceData.waitingForHit = false;
				this.AddHit(skillSlot);
			}
			instanceData.timeoutTimer -= Time.fixedDeltaTime;
			if (instanceData.timeoutTimer <= 0f && instanceData.currentDashIndex != 0)
			{
				if (instanceData.hasExtraStock)
				{
					skillSlot.stock--;
					instanceData.hasExtraStock = false;
				}
				instanceData.currentDashIndex = 0;
			}
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x00121E78 File Offset: 0x00120078
		protected override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
		{
			EntityState entityState = base.InstantiateNextState(skillSlot);
			PrepAssaulter2 prepAssaulter;
			if ((prepAssaulter = (entityState as PrepAssaulter2)) != null)
			{
				prepAssaulter.dashIndex = ((MercDashSkillDef.InstanceData)skillSlot.skillInstanceData).currentDashIndex;
			}
			return entityState;
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x00121EAC File Offset: 0x001200AC
		public override void OnExecute([NotNull] GenericSkill skillSlot)
		{
			base.OnExecute(skillSlot);
			MercDashSkillDef.InstanceData instanceData = (MercDashSkillDef.InstanceData)skillSlot.skillInstanceData;
			if (!instanceData.hasExtraStock)
			{
				instanceData.currentDashIndex = 0;
			}
			instanceData.waitingForHit = true;
			instanceData.hasExtraStock = false;
			instanceData.timeoutTimer = this.timeoutDuration;
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x00121EF8 File Offset: 0x001200F8
		protected void AddHit([NotNull] GenericSkill skillSlot)
		{
			MercDashSkillDef.InstanceData instanceData = (MercDashSkillDef.InstanceData)skillSlot.skillInstanceData;
			if (instanceData.currentDashIndex < this.maxDashes - 1)
			{
				instanceData.currentDashIndex++;
				int stock = skillSlot.stock + 1;
				skillSlot.stock = stock;
				instanceData.hasExtraStock = true;
				return;
			}
			instanceData.currentDashIndex = 0;
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x00121F50 File Offset: 0x00120150
		public override Sprite GetCurrentIcon([NotNull] GenericSkill skillSlot)
		{
			MercDashSkillDef.InstanceData instanceData = (MercDashSkillDef.InstanceData)skillSlot.skillInstanceData;
			int index = (instanceData != null) ? instanceData.currentDashIndex : 0;
			return ArrayUtils.GetSafe<Sprite>(this.icons, index);
		}

		// Token: 0x040043D8 RID: 17368
		public int maxDashes;

		// Token: 0x040043D9 RID: 17369
		public float timeoutDuration;

		// Token: 0x040043DA RID: 17370
		public Sprite[] icons;

		// Token: 0x02000C0F RID: 3087
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043DB RID: 17371
			public int currentDashIndex;

			// Token: 0x040043DC RID: 17372
			public float timeoutTimer;

			// Token: 0x040043DD RID: 17373
			public bool waitingForHit;

			// Token: 0x040043DE RID: 17374
			public bool hasExtraStock;
		}
	}
}
