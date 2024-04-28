using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidJailer.Weapon
{
	// Token: 0x02000155 RID: 341
	public class ChargeCapture : BaseState
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public override void OnEnter()
		{
			base.OnEnter();
			ChargeCapture.duration /= this.attackSpeedStat;
			this._crossFadeDuration = ChargeCapture.duration * 0.25f;
			base.PlayCrossfade(ChargeCapture.animationLayerName, ChargeCapture.animationStateName, ChargeCapture.animationPlaybackRateName, ChargeCapture.duration, this._crossFadeDuration);
			this.soundID = Util.PlayAttackSpeedSound(ChargeCapture.enterSoundString, base.gameObject, this.attackSpeedStat);
			if (ChargeCapture.chargeEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(ChargeCapture.chargeEffectPrefab, base.gameObject, ChargeCapture.muzzleString, false);
			}
			if (ChargeCapture.attackIndicatorPrefab)
			{
				Transform coreTransform = base.characterBody.coreTransform;
				if (coreTransform)
				{
					this.attackIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeCapture.attackIndicatorPrefab, coreTransform);
				}
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00019BAC File Offset: 0x00017DAC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= ChargeCapture.duration)
			{
				this.outer.SetNextState(new Capture2());
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00019BDC File Offset: 0x00017DDC
		public override void Update()
		{
			if (this.attackIndicatorInstance)
			{
				this.attackIndicatorInstance.transform.forward = base.GetAimRay().direction;
			}
			base.Update();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00019C1A File Offset: 0x00017E1A
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.soundID);
			if (this.attackIndicatorInstance)
			{
				EntityState.Destroy(this.attackIndicatorInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000739 RID: 1849
		public static string animationLayerName;

		// Token: 0x0400073A RID: 1850
		public static string animationStateName;

		// Token: 0x0400073B RID: 1851
		public static string animationPlaybackRateName;

		// Token: 0x0400073C RID: 1852
		public static float duration;

		// Token: 0x0400073D RID: 1853
		public static string enterSoundString;

		// Token: 0x0400073E RID: 1854
		public static GameObject chargeEffectPrefab;

		// Token: 0x0400073F RID: 1855
		public static GameObject attackIndicatorPrefab;

		// Token: 0x04000740 RID: 1856
		public static string muzzleString;

		// Token: 0x04000741 RID: 1857
		private float _crossFadeDuration;

		// Token: 0x04000742 RID: 1858
		private uint soundID;

		// Token: 0x04000743 RID: 1859
		private GameObject attackIndicatorInstance;
	}
}
