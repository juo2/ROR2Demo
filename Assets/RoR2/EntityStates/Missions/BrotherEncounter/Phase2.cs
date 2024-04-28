using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000252 RID: 594
	public class Phase2 : BrotherEncounterPhaseBaseState
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002BA5A File Offset: 0x00029C5A
		protected override string phaseControllerChildString
		{
			get
			{
				return "Phase2";
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002BA61 File Offset: 0x00029C61
		protected override EntityState nextState
		{
			get
			{
				return new Phase3();
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002BA68 File Offset: 0x00029C68
		public override void OnEnter()
		{
			base.OnEnter();
			GameObject gameObject = this.childLocator.FindChild("BlockingPillars").gameObject;
			if (gameObject)
			{
				gameObject.SetActive(true);
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					this.pillarsToActive.Add(gameObject.transform.GetChild(i).gameObject);
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.pillarActivationStopwatch += Time.fixedDeltaTime;
			if (this.pillarsToActive.Count > 0 && this.pillarActivationStopwatch > Phase2.delayBetweenPillarActivation)
			{
				this.pillarActivationStopwatch = 0f;
				this.pillarsToActive[0].SetActive(true);
				this.pillarsToActive.RemoveAt(0);
			}
		}

		// Token: 0x04000C3A RID: 3130
		public static float delayBetweenPillarActivation;

		// Token: 0x04000C3B RID: 3131
		private List<GameObject> pillarsToActive = new List<GameObject>();

		// Token: 0x04000C3C RID: 3132
		private float pillarActivationStopwatch;
	}
}
