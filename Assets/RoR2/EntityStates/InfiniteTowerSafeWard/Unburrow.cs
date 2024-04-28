using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002FE RID: 766
	public class Unburrow : BaseSafeWardState
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00039145 File Offset: 0x00037345
		public Unburrow()
		{
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00039ADD File Offset: 0x00037CDD
		public Unburrow(Xoroshiro128Plus rng)
		{
			this.rng = rng;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00039AEC File Offset: 0x00037CEC
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.safeWardController)
			{
				this.safeWardController.SetIndicatorEnabled(true);
			}
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.purchaseInteraction)
			{
				this.purchaseInteraction.SetAvailable(false);
			}
			if (this.zone)
			{
				this.zone.Networkradius = this.radius;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00039B73 File Offset: 0x00037D73
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new Travelling(this.rng));
			}
		}

		// Token: 0x040010C9 RID: 4297
		[SerializeField]
		public float duration;

		// Token: 0x040010CA RID: 4298
		[SerializeField]
		public float radius;

		// Token: 0x040010CB RID: 4299
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040010CC RID: 4300
		[SerializeField]
		public string animationStateName;

		// Token: 0x040010CD RID: 4301
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040010CE RID: 4302
		private Xoroshiro128Plus rng;
	}
}
