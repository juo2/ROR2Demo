using System;
using UnityEngine;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000250 RID: 592
	public class PreEncounter : BaseState
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x0002B907 File Offset: 0x00029B07
		public override void OnEnter()
		{
			base.OnEnter();
			this.childLocator = base.GetComponent<ChildLocator>();
			this.childLocator.FindChild("PreEncounter").gameObject.SetActive(true);
			Debug.Log("Entering pre-encounter");
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002B940 File Offset: 0x00029B40
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= PreEncounter.duration)
			{
				this.outer.SetNextState(new Phase1());
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002B965 File Offset: 0x00029B65
		public override void OnExit()
		{
			this.childLocator.FindChild("PreEncounter").gameObject.SetActive(false);
			base.OnExit();
		}

		// Token: 0x04000C34 RID: 3124
		public static float duration;

		// Token: 0x04000C35 RID: 3125
		private ChildLocator childLocator;
	}
}
