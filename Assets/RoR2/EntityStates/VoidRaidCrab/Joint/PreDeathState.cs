using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Joint
{
	// Token: 0x02000146 RID: 326
	public class PreDeathState : BaseState
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0001860C File Offset: 0x0001680C
		public override void OnEnter()
		{
			base.OnEnter();
			this.canProceed = false;
			this.jointEffects.Clear();
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				this.SpawnJointEffect(this.joint1Name, modelChildLocator);
				this.SpawnJointEffect(this.joint2Name, modelChildLocator);
				this.SpawnJointEffect(this.joint3Name, modelChildLocator);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00018668 File Offset: 0x00016868
		public override void OnExit()
		{
			base.OnExit();
			foreach (GameObject obj in this.jointEffects)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.jointEffects.Clear();
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000186CC File Offset: 0x000168CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.minDuration && this.canProceed)
			{
				this.outer.SetNextState(new DeathState());
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018704 File Offset: 0x00016904
		private void SpawnJointEffect(string jointName, ChildLocator childLocator)
		{
			Transform transform = childLocator.FindChild(jointName);
			if (transform)
			{
				GameObject item = UnityEngine.Object.Instantiate<GameObject>(this.jointEffectPrefab, transform);
				this.jointEffects.Add(item);
			}
		}

		// Token: 0x040006BC RID: 1724
		[SerializeField]
		public float minDuration;

		// Token: 0x040006BD RID: 1725
		[SerializeField]
		public string joint1Name;

		// Token: 0x040006BE RID: 1726
		[SerializeField]
		public string joint2Name;

		// Token: 0x040006BF RID: 1727
		[SerializeField]
		public string joint3Name;

		// Token: 0x040006C0 RID: 1728
		[SerializeField]
		public GameObject jointEffectPrefab;

		// Token: 0x040006C1 RID: 1729
		public bool canProceed;

		// Token: 0x040006C2 RID: 1730
		private List<GameObject> jointEffects = new List<GameObject>();
	}
}
