using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Audio;
using RoR2.HudOverlay;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Reload
{
	// Token: 0x0200020B RID: 523
	public class Reloading : BaseState
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x000260D8 File Offset: 0x000242D8
		public bool AttemptBoost()
		{
			if (this.IsInBoostWindow())
			{
				this.outer.SetNextState(new BoostConfirm());
				return true;
			}
			foreach (ActiveReloadBarController activeReloadBarController in this.reloadUiList)
			{
				activeReloadBarController.SetWasFailure(true);
			}
			return false;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00026144 File Offset: 0x00024344
		public override void OnEnter()
		{
			base.OnEnter();
			float num = this.boostWindowDelay + this.boostWindowDuration;
			float num2 = this.baseDuration - num;
			this.duration = num + num2 / this.attackSpeedStat;
			this.adjustedBoostWindowDelay = Mathf.Min(this.boostWindowDelay / this.baseDuration * this.duration, this.boostWindowDelay);
			this.adjustedBoostWindowDuration = Mathf.Max((this.boostWindowDelay + this.boostWindowDuration) / this.baseDuration * this.duration, num) - this.adjustedBoostWindowDelay;
			if (this.loopSoundDef)
			{
				this.loopPtr = LoopSoundManager.PlaySoundLoopLocalRtpc(base.gameObject, this.loopSoundDef, "attackSpeed", Util.CalculateAttackSpeedRtpcValue(this.attackSpeedStat));
			}
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			SkillLocator component = base.gameObject.GetComponent<SkillLocator>();
			if (component && component.primary)
			{
				this.primarySkill = component.primary;
				this.primarySkill.SetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
			}
			OverlayCreationParams overlayCreationParams = new OverlayCreationParams
			{
				prefab = this.overlayPrefab,
				childLocatorEntry = this.overlayChildLocatorEntry
			};
			this.overlayController = HudOverlayManager.AddOverlay(base.gameObject, overlayCreationParams);
			this.overlayController.onInstanceAdded += this.OnOverlayInstanceAdded;
			this.overlayController.onInstanceRemove += this.OnOverlayInstanceRemoved;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000262C8 File Offset: 0x000244C8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			float num = (this.duration > 0f) ? (base.fixedAge / this.duration) : 0f;
			foreach (ActiveReloadBarController activeReloadBarController in this.reloadUiList)
			{
				activeReloadBarController.SetTValue(num);
				activeReloadBarController.SetIsWindowActive(this.IsInBoostWindow());
			}
			if (this.duration <= 0f || num >= 1f)
			{
				Util.PlaySound(this.endNoBoostSoundString, base.gameObject);
				this.outer.SetNextState(new Waiting());
			}
			if (base.inputBank && base.inputBank.skill1.justPressed && !this.hasAttempted)
			{
				this.hasAttempted = true;
				if (!this.AttemptBoost())
				{
					Util.PlaySound(this.failSoundString, base.gameObject);
				}
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000263CC File Offset: 0x000245CC
		public override void OnExit()
		{
			if (this.overlayController != null)
			{
				this.overlayController.onInstanceAdded -= this.OnOverlayInstanceAdded;
				this.overlayController.onInstanceRemove -= this.OnOverlayInstanceRemoved;
				this.reloadUiList.Clear();
			}
			if (base.skillLocator)
			{
				this.primarySkill.UnsetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
			}
			for (int i = 0; i < base.skillLocator.skillSlotCount; i++)
			{
				GenericSkill skillAtIndex = base.skillLocator.GetSkillAtIndex(i);
				if (skillAtIndex)
				{
					RailgunSkillDef railgunSkillDef = skillAtIndex.skillDef as RailgunSkillDef;
					if (railgunSkillDef && railgunSkillDef.restockOnReload)
					{
						skillAtIndex.stock = skillAtIndex.maxStock;
					}
				}
			}
			if (this.loopPtr.isValid)
			{
				LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			}
			base.OnExit();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000264AB File Offset: 0x000246AB
		public bool IsInBoostWindow()
		{
			return this.adjustedBoostWindowDelay - base.fixedAge < this.boostGracePeriod && base.fixedAge - (this.adjustedBoostWindowDelay + this.adjustedBoostWindowDuration) < this.boostGracePeriod;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000264E0 File Offset: 0x000246E0
		public override void ModifyNextState(EntityState nextState)
		{
			BoostConfirm boostConfirm = nextState as BoostConfirm;
			if (boostConfirm != null)
			{
				boostConfirm.overlayController = this.overlayController;
				if (this.primarySkill)
				{
					boostConfirm.OverridePrimary(this.primarySkill, this.primaryOverride);
					return;
				}
			}
			else if (this.overlayController != null)
			{
				HudOverlayManager.RemoveOverlay(this.overlayController);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00026538 File Offset: 0x00024738
		private void OnOverlayInstanceAdded(OverlayController controller, GameObject instance)
		{
			ActiveReloadBarController component = instance.GetComponent<ActiveReloadBarController>();
			float num = (this.duration > 0f) ? (1f / this.duration) : 0f;
			component.SetWindowRange(num * this.adjustedBoostWindowDelay, num * (this.adjustedBoostWindowDelay + this.adjustedBoostWindowDuration));
			this.reloadUiList.Add(component);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00026598 File Offset: 0x00024798
		private void OnOverlayInstanceRemoved(OverlayController controller, GameObject instance)
		{
			ActiveReloadBarController component = instance.GetComponent<ActiveReloadBarController>();
			this.reloadUiList.Remove(component);
		}

		// Token: 0x04000AB7 RID: 2743
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000AB8 RID: 2744
		[SerializeField]
		public float boostWindowDelay;

		// Token: 0x04000AB9 RID: 2745
		[SerializeField]
		public float boostWindowDuration;

		// Token: 0x04000ABA RID: 2746
		[SerializeField]
		public float boostGracePeriod;

		// Token: 0x04000ABB RID: 2747
		[SerializeField]
		public SkillDef primaryOverride;

		// Token: 0x04000ABC RID: 2748
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x04000ABD RID: 2749
		[SerializeField]
		public string overlayChildLocatorEntry;

		// Token: 0x04000ABE RID: 2750
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000ABF RID: 2751
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000AC0 RID: 2752
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000AC1 RID: 2753
		[SerializeField]
		public LoopSoundDef loopSoundDef;

		// Token: 0x04000AC2 RID: 2754
		[SerializeField]
		public string endNoBoostSoundString;

		// Token: 0x04000AC3 RID: 2755
		[SerializeField]
		public string failSoundString;

		// Token: 0x04000AC4 RID: 2756
		private float duration;

		// Token: 0x04000AC5 RID: 2757
		private float adjustedBoostWindowDelay;

		// Token: 0x04000AC6 RID: 2758
		private float adjustedBoostWindowDuration;

		// Token: 0x04000AC7 RID: 2759
		private GenericSkill primarySkill;

		// Token: 0x04000AC8 RID: 2760
		private OverlayController overlayController;

		// Token: 0x04000AC9 RID: 2761
		private List<ActiveReloadBarController> reloadUiList = new List<ActiveReloadBarController>();

		// Token: 0x04000ACA RID: 2762
		private bool hasAttempted;

		// Token: 0x04000ACB RID: 2763
		private LoopSoundManager.SoundLoopPtr loopPtr;
	}
}
