using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000202 RID: 514
	public class BaseWindUp : BaseScopeState
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00025BDB File Offset: 0x00023DDB
		public override void OnEnter()
		{
			this.duration = this.baseDuration;
			base.OnEnter();
			base.SetScopeAlpha(0f);
			base.StartScopeParamsOverride(this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00025C18 File Offset: 0x00023E18
		public override void OnExit()
		{
			base.EndScopeParamsOverride(0f);
			base.OnExit();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00025C2C File Offset: 0x00023E2C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				BaseActive nextState = this.GetNextState();
				nextState.activatorSkillSlot = base.activatorSkillSlot;
				this.outer.SetNextState(nextState);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00025C74 File Offset: 0x00023E74
		public override void Update()
		{
			base.Update();
			base.SetScopeAlpha(Mathf.Clamp01(base.age / this.duration));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00025C94 File Offset: 0x00023E94
		protected virtual BaseActive GetNextState()
		{
			return new BaseActive();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00025C9B File Offset: 0x00023E9B
		protected override float GetScopeEntryDuration()
		{
			return this.duration;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000026ED File Offset: 0x000008ED
		public override void ModifyNextState(EntityState nextState)
		{
		}

		// Token: 0x04000AA0 RID: 2720
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000AA1 RID: 2721
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000AA2 RID: 2722
		private float duration;
	}
}
