using System;
using RoR2;
using RoR2.Audio;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x0200020D RID: 525
	public abstract class BaseBackpack : BaseState
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x00026818 File Offset: 0x00024A18
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.loopSoundDef)
			{
				if (this.isSoundScaledByAttackSpeed)
				{
					this.loopPtr = LoopSoundManager.PlaySoundLoopLocalRtpc(base.gameObject, this.loopSoundDef, "attackSpeed", Util.CalculateAttackSpeedRtpcValue(this.attackSpeedStat));
				}
				else
				{
					this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
				}
			}
			if (!string.IsNullOrEmpty(this.enterSoundString))
			{
				if (this.isSoundScaledByAttackSpeed)
				{
					Util.PlayAttackSpeedSound(this.enterSoundString, base.gameObject, this.attackSpeedStat);
				}
				else
				{
					Util.PlaySound(this.enterSoundString, base.gameObject);
				}
			}
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator && !string.IsNullOrEmpty(this.mecanimBoolName))
			{
				modelAnimator.SetBool(this.mecanimBoolName, true);
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000268EC File Offset: 0x00024AEC
		public override void OnExit()
		{
			if (this.loopPtr.isValid)
			{
				LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			}
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator && !string.IsNullOrEmpty(this.mecanimBoolName))
			{
				modelAnimator.SetBool(this.mecanimBoolName, false);
			}
			base.OnExit();
		}

		// Token: 0x04000AD2 RID: 2770
		[SerializeField]
		public LoopSoundDef loopSoundDef;

		// Token: 0x04000AD3 RID: 2771
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000AD4 RID: 2772
		[SerializeField]
		public string mecanimBoolName;

		// Token: 0x04000AD5 RID: 2773
		private LoopSoundManager.SoundLoopPtr loopPtr;

		// Token: 0x04000AD6 RID: 2774
		protected bool isSoundScaledByAttackSpeed;
	}
}
