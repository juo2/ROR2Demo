using System;
using RoR2;
using UnityEngine;

namespace EntityStates.SurvivorPod
{
	// Token: 0x020001B6 RID: 438
	public class Descent : SurvivorPodBaseState
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x0002197C File Offset: 0x0001FB7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "InitialSpawn");
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("Travel");
					if (transform)
					{
						this.shakeEmitter = transform.gameObject.AddComponent<ShakeEmitter>();
						this.shakeEmitter.wave = new Wave
						{
							amplitude = 1f,
							frequency = 180f,
							cycleOffset = 0f
						};
						this.shakeEmitter.duration = 10000f;
						this.shakeEmitter.radius = 400f;
						this.shakeEmitter.amplitudeTimeDecay = false;
					}
				}
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00021A4C File Offset: 0x0001FC4C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.AuthorityFixedUpdate();
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00021A64 File Offset: 0x0001FC64
		protected void AuthorityFixedUpdate()
		{
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Base");
				if (layerIndex != -1 && modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("Idle"))
				{
					this.TransitionIntoNextState();
				}
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00021AAC File Offset: 0x0001FCAC
		protected virtual void TransitionIntoNextState()
		{
			this.outer.SetNextState(new Landed());
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00021ABE File Offset: 0x0001FCBE
		public override void OnExit()
		{
			EntityState.Destroy(this.shakeEmitter);
			base.OnExit();
		}

		// Token: 0x0400096D RID: 2413
		private ShakeEmitter shakeEmitter;
	}
}
