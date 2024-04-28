using System;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F2 RID: 498
	public abstract class BaseWindupSnipe : BaseState, IBaseWeaponState
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00025698 File Offset: 0x00023898
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			if (this.windupEffectPrefab)
			{
				Transform transform = base.FindModelChild(this.windupEffectMuzzle);
				if (transform)
				{
					this.windupEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.windupEffectPrefab, transform.position, transform.rotation);
					this.windupEffectInstance.transform.parent = transform;
				}
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00025750 File Offset: 0x00023950
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(base.age / this.duration, false);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00025771 File Offset: 0x00023971
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.InstantiateNextState());
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00025798 File Offset: 0x00023998
		public override void OnExit()
		{
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			if (this.windupEffectInstance)
			{
				EntityState.Destroy(this.windupEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x060008E7 RID: 2279
		protected abstract EntityState InstantiateNextState();

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool CanScope()
		{
			return true;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04000A7C RID: 2684
		[SerializeField]
		public float duration;

		// Token: 0x04000A7D RID: 2685
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000A7E RID: 2686
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000A7F RID: 2687
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000A80 RID: 2688
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000A81 RID: 2689
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000A82 RID: 2690
		[SerializeField]
		public GameObject windupEffectPrefab;

		// Token: 0x04000A83 RID: 2691
		[SerializeField]
		public string windupEffectMuzzle;

		// Token: 0x04000A84 RID: 2692
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000A85 RID: 2693
		private GameObject windupEffectInstance;
	}
}
