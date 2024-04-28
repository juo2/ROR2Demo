using System;
using System.Collections.Generic;
using EntityStates.Railgunner.Backpack;
using RoR2;
using RoR2.Skills;

namespace EntityStates.Railgunner.Reload
{
	// Token: 0x0200020C RID: 524
	public class Waiting : EntityState
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x000265CC File Offset: 0x000247CC
		public Waiting()
		{
			this.isReloadQueued = false;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000265E6 File Offset: 0x000247E6
		public Waiting(bool queueReload)
		{
			this.isReloadQueued = queueReload;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00026600 File Offset: 0x00024800
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.skillLocator)
			{
				for (int i = 0; i < base.skillLocator.skillSlotCount; i++)
				{
					GenericSkill skillAtIndex = base.skillLocator.GetSkillAtIndex(i);
					if (skillAtIndex)
					{
						skillAtIndex.onSkillChanged += this.OnSkillChanged;
					}
				}
				this.ReevaluateSkills();
			}
			this.backpackStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Backpack");
			this.scopeStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Scope");
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00026690 File Offset: 0x00024890
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.isReloadQueued)
			{
				using (List<GenericSkill>.Enumerator enumerator = this.restockOnReloadList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.stock == 0)
						{
							this.isReloadQueued = true;
							break;
						}
					}
				}
			}
			if (this.isReloadQueued && this.CanReload())
			{
				this.outer.SetNextState(new Reloading());
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002671C File Offset: 0x0002491C
		public override void OnExit()
		{
			if (base.skillLocator)
			{
				for (int i = 0; i < base.skillLocator.skillSlotCount; i++)
				{
					GenericSkill skillAtIndex = base.skillLocator.GetSkillAtIndex(i);
					if (skillAtIndex)
					{
						skillAtIndex.onSkillChanged -= this.OnSkillChanged;
					}
				}
			}
			base.OnExit();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00026779 File Offset: 0x00024979
		public bool CanReload()
		{
			return !(this.backpackStateMachine.state is Offline) && this.scopeStateMachine.IsInMainState();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002679A File Offset: 0x0002499A
		public void QueueReload()
		{
			this.isReloadQueued = true;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000267A3 File Offset: 0x000249A3
		private void OnSkillChanged(GenericSkill skill)
		{
			this.ReevaluateSkills();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000267AC File Offset: 0x000249AC
		private void ReevaluateSkills()
		{
			this.restockOnReloadList.Clear();
			for (int i = 0; i < base.skillLocator.skillSlotCount; i++)
			{
				GenericSkill skillAtIndex = base.skillLocator.GetSkillAtIndex(i);
				if (skillAtIndex)
				{
					RailgunSkillDef railgunSkillDef = skillAtIndex.skillDef as RailgunSkillDef;
					if (railgunSkillDef && railgunSkillDef.restockOnReload)
					{
						this.restockOnReloadList.Add(skillAtIndex);
					}
				}
			}
		}

		// Token: 0x04000ACC RID: 2764
		private const string backpackStateMachineName = "Backpack";

		// Token: 0x04000ACD RID: 2765
		private const string scopeStateMachineName = "Scope";

		// Token: 0x04000ACE RID: 2766
		private List<GenericSkill> restockOnReloadList = new List<GenericSkill>();

		// Token: 0x04000ACF RID: 2767
		private EntityStateMachine backpackStateMachine;

		// Token: 0x04000AD0 RID: 2768
		private EntityStateMachine scopeStateMachine;

		// Token: 0x04000AD1 RID: 2769
		private bool isReloadQueued;
	}
}
