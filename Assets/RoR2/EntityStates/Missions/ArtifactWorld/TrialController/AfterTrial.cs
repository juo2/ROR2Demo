using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x02000260 RID: 608
	public class AfterTrial : ArtifactTrialControllerBaseState
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002BD4D File Offset: 0x00029F4D
		public virtual Type GetNextStateType()
		{
			return typeof(FinishTrial);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002BD5C File Offset: 0x00029F5C
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction.enabled = true;
			this.childLocator.FindChild("AfterTrial").gameObject.SetActive(true);
			this.outer.mainStateType = new SerializableEntityStateType(this.GetNextStateType());
			Highlight component = base.GetComponent<Highlight>();
			Transform transform = this.childLocator.FindChild("CompletedArtifactMesh");
			if (component && transform)
			{
				component.targetRenderer = transform.GetComponent<MeshRenderer>();
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public override void OnExit()
		{
			this.childLocator.FindChild("AfterTrial").gameObject.SetActive(false);
			base.OnExit();
		}
	}
}
