using System;
using System.Collections.Generic;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Engi.MineDeployer
{
	// Token: 0x02000395 RID: 917
	public class BaseMineDeployerState : BaseState
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004826D File Offset: 0x0004646D
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x00048275 File Offset: 0x00046475
		public GameObject owner { get; private set; }

		// Token: 0x06001078 RID: 4216 RVA: 0x0004827E File Offset: 0x0004647E
		public override void OnEnter()
		{
			base.OnEnter();
			ProjectileController projectileController = base.projectileController;
			this.owner = ((projectileController != null) ? projectileController.owner : null);
			BaseMineDeployerState.instancesList.Add(this);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000482A9 File Offset: 0x000464A9
		public override void OnExit()
		{
			BaseMineDeployerState.instancesList.Remove(this);
			base.OnExit();
		}

		// Token: 0x040014D6 RID: 5334
		public static List<BaseMineDeployerState> instancesList = new List<BaseMineDeployerState>();
	}
}
