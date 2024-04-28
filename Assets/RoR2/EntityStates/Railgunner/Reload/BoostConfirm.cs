using System;
using RoR2;
using RoR2.HudOverlay;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Reload
{
	// Token: 0x02000209 RID: 521
	public class BoostConfirm : EntityState
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00025F24 File Offset: 0x00024124
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.overlayController != null)
			{
				foreach (GameObject gameObject in this.overlayController.instancesList)
				{
					ActiveReloadBarController component = gameObject.GetComponent<ActiveReloadBarController>();
					if (component)
					{
						component.SetWasWindowHit(true);
					}
				}
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00025FC4 File Offset: 0x000241C4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new Boosted());
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00025FEA File Offset: 0x000241EA
		public override void OnExit()
		{
			if (this.overlayController != null)
			{
				HudOverlayManager.RemoveOverlay(this.overlayController);
			}
			if (this.primarySkill)
			{
				this.primarySkill.UnsetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
			}
			base.OnExit();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00026025 File Offset: 0x00024225
		public void OverridePrimary(GenericSkill skill, SkillDef overrideDef)
		{
			this.primarySkill = skill;
			this.primaryOverride = overrideDef;
			this.primarySkill.SetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
		}

		// Token: 0x04000AAA RID: 2730
		[SerializeField]
		public float duration;

		// Token: 0x04000AAB RID: 2731
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000AAC RID: 2732
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000AAD RID: 2733
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000AAE RID: 2734
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000AAF RID: 2735
		public OverlayController overlayController;

		// Token: 0x04000AB0 RID: 2736
		private GenericSkill primarySkill;

		// Token: 0x04000AB1 RID: 2737
		public SkillDef primaryOverride;
	}
}
