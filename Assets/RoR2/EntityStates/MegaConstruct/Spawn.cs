using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MegaConstruct
{
	// Token: 0x02000288 RID: 648
	public class Spawn : BaseState
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x0002FC58 File Offset: 0x0002DE58
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002FCC0 File Offset: 0x0002DEC0
		private void CheckForDepleteStocks(SkillSlot slot, bool deplete)
		{
			GenericSkill skill = base.skillLocator.GetSkill(slot);
			if (deplete && skill)
			{
				skill.RemoveAllStocks();
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002FCEB File Offset: 0x0002DEEB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002FD14 File Offset: 0x0002DF14
		public override void OnExit()
		{
			if (base.isAuthority)
			{
				this.CheckForDepleteStocks(SkillSlot.Primary, this.depleteStocksPrimary);
				this.CheckForDepleteStocks(SkillSlot.Secondary, this.depleteStocksSecondary);
				this.CheckForDepleteStocks(SkillSlot.Utility, this.depleteStocksUtility);
				this.CheckForDepleteStocks(SkillSlot.Special, this.depleteStocksSpecial);
			}
			base.OnExit();
		}

		// Token: 0x04000D6E RID: 3438
		[SerializeField]
		public float duration;

		// Token: 0x04000D6F RID: 3439
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000D70 RID: 3440
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000D71 RID: 3441
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000D72 RID: 3442
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000D73 RID: 3443
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000D74 RID: 3444
		[SerializeField]
		public int numPads;

		// Token: 0x04000D75 RID: 3445
		[SerializeField]
		public string padChildLocatorName;

		// Token: 0x04000D76 RID: 3446
		[SerializeField]
		public GameObject padPrefab;

		// Token: 0x04000D77 RID: 3447
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000D78 RID: 3448
		[SerializeField]
		public bool depleteStocksPrimary;

		// Token: 0x04000D79 RID: 3449
		[SerializeField]
		public bool depleteStocksSecondary;

		// Token: 0x04000D7A RID: 3450
		[SerializeField]
		public bool depleteStocksUtility;

		// Token: 0x04000D7B RID: 3451
		[SerializeField]
		public bool depleteStocksSpecial;
	}
}
