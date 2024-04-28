using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidMegaCrab.Weapon
{
	// Token: 0x0200014C RID: 332
	public abstract class ChargeCrabCannonBase : BaseState
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex(this.animationLayerName);
				if (modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("Empty"))
				{
					base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration, this.animationCrossfadeDuration);
				}
				else
				{
					base.PlayCrossfade(this.animationLayerName, this.animationStateNamePreCharged, this.animationPlaybackRateParam, this.duration, this.animationCrossfadeDuration);
				}
			}
			Util.PlaySound(this.soundName, base.gameObject);
			Transform transform = base.FindModelChild(this.chargeMuzzleName);
			if (this.chargeEffectPrefab && transform)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeInstance.transform.parent = transform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
			base.characterBody.SetAimTimer(this.duration + 2f);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00018D26 File Offset: 0x00016F26
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018D46 File Offset: 0x00016F46
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(this.GetNextState());
				return;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x060005D5 RID: 1493
		protected abstract FireCrabCannonBase GetNextState();

		// Token: 0x040006E1 RID: 1761
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x040006E2 RID: 1762
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040006E3 RID: 1763
		[SerializeField]
		public string soundName;

		// Token: 0x040006E4 RID: 1764
		[SerializeField]
		public string chargeMuzzleName;

		// Token: 0x040006E5 RID: 1765
		[SerializeField]
		public string animationLayerName = "Gesture, Additive";

		// Token: 0x040006E6 RID: 1766
		[SerializeField]
		public string animationStateName = "FireCrabCannon";

		// Token: 0x040006E7 RID: 1767
		[SerializeField]
		public string animationStateNamePreCharged = "FireCrabCannon";

		// Token: 0x040006E8 RID: 1768
		[SerializeField]
		public string animationPlaybackRateParam = "FireCrabCannon.playbackRate";

		// Token: 0x040006E9 RID: 1769
		[SerializeField]
		public float animationCrossfadeDuration = 0.2f;

		// Token: 0x040006EA RID: 1770
		private GameObject chargeInstance;

		// Token: 0x040006EB RID: 1771
		private float duration;
	}
}
