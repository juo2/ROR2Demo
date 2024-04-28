using System;
using RoR2;
using UnityEngine;

namespace EntityStates.SurvivorPod
{
	// Token: 0x020001B8 RID: 440
	public class PreRelease : SurvivorPodBaseState
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00021B98 File Offset: 0x0001FD98
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "IdleToRelease");
			Util.PlaySound("Play_UI_podBlastDoorOpen", base.gameObject);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				modelTransform.GetComponent<ChildLocator>().FindChild("InitialExhaustFX").gameObject.SetActive(true);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Base");
				if (layerIndex != -1 && modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("IdleToReleaseFinished"))
				{
					this.outer.SetNextState(new Release());
				}
			}
		}
	}
}
