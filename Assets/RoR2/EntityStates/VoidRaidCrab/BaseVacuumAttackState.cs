using System;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000127 RID: 295
	public abstract class BaseVacuumAttackState : BaseState
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001662E File Offset: 0x0001482E
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00016636 File Offset: 0x00014836
		private protected float duration { protected get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001663F File Offset: 0x0001483F
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00016647 File Offset: 0x00014847
		private protected Transform vacuumOrigin { protected get; private set; }

		// Token: 0x0600053F RID: 1343 RVA: 0x00016650 File Offset: 0x00014850
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			if (!string.IsNullOrEmpty(this.animLayerName) && !string.IsNullOrEmpty(this.animStateName) && !string.IsNullOrEmpty(this.animPlaybackRateParamName))
			{
				base.PlayAnimation(this.animLayerName, this.animStateName, this.animPlaybackRateParamName, this.duration);
			}
			if (!string.IsNullOrEmpty(BaseVacuumAttackState.vacuumOriginChildLocatorName))
			{
				this.vacuumOrigin = base.FindModelChild(BaseVacuumAttackState.vacuumOriginChildLocatorName);
				return;
			}
			this.vacuumOrigin = base.transform;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000166E5 File Offset: 0x000148E5
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.OnLifetimeExpiredAuthority();
			}
		}

		// Token: 0x06000541 RID: 1345
		protected abstract void OnLifetimeExpiredAuthority();

		// Token: 0x06000542 RID: 1346 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04000620 RID: 1568
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000621 RID: 1569
		[SerializeField]
		public string animLayerName;

		// Token: 0x04000622 RID: 1570
		[SerializeField]
		public string animStateName;

		// Token: 0x04000623 RID: 1571
		[SerializeField]
		public string animPlaybackRateParamName;

		// Token: 0x04000624 RID: 1572
		public static string vacuumOriginChildLocatorName;
	}
}
