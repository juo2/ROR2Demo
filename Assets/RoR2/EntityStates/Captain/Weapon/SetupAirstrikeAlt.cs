using System;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x0200041D RID: 1053
	public class SetupAirstrikeAlt : BaseState
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0005493B File Offset: 0x00052B3B
		private float exitDuration
		{
			get
			{
				return SetupAirstrikeAlt.baseExitDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0005494C File Offset: 0x00052B4C
		public override void OnEnter()
		{
			base.OnEnter();
			this.primarySkillSlot = (base.skillLocator ? base.skillLocator.primary : null);
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.SetSkillOverride(this, SetupAirstrikeAlt.primarySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetBool("PrepAirstrike", true);
			}
			base.PlayCrossfade("Gesture, Override", "PrepAirstrike", 0.1f);
			base.PlayCrossfade("Gesture, Additive", "PrepAirstrike", 0.1f);
			Transform transform = base.FindModelChild(SetupAirstrikeAlt.effectMuzzleString);
			if (transform)
			{
				this.effectMuzzleInstance = UnityEngine.Object.Instantiate<GameObject>(SetupAirstrikeAlt.effectMuzzlePrefab, transform);
			}
			if (SetupAirstrikeAlt.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, SetupAirstrikeAlt.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			Util.PlaySound(SetupAirstrikeAlt.enterSoundString, base.gameObject);
			Util.PlaySound("Play_captain_shift_active_loop", base.gameObject);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00054A60 File Offset: 0x00052C60
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (!this.primarySkillSlot || this.primarySkillSlot.stock == 0)
			{
				this.beginExit = true;
			}
			if (this.beginExit)
			{
				this.timerSinceComplete += Time.fixedDeltaTime;
				if (this.timerSinceComplete > this.exitDuration)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00054AF0 File Offset: 0x00052CF0
		public override void OnExit()
		{
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.UnsetSkillOverride(this, SetupAirstrikeAlt.primarySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			Util.PlaySound(SetupAirstrikeAlt.exitSoundString, base.gameObject);
			Util.PlaySound("Stop_captain_shift_active_loop", base.gameObject);
			if (this.effectMuzzleInstance)
			{
				EntityState.Destroy(this.effectMuzzleInstance);
			}
			if (this.modelAnimator)
			{
				this.modelAnimator.SetBool("PrepAirstrike", false);
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x0400185E RID: 6238
		public static SkillDef primarySkillDef;

		// Token: 0x0400185F RID: 6239
		public static GameObject crosshairOverridePrefab;

		// Token: 0x04001860 RID: 6240
		public static string enterSoundString;

		// Token: 0x04001861 RID: 6241
		public static string exitSoundString;

		// Token: 0x04001862 RID: 6242
		public static GameObject effectMuzzlePrefab;

		// Token: 0x04001863 RID: 6243
		public static string effectMuzzleString;

		// Token: 0x04001864 RID: 6244
		public static float baseExitDuration;

		// Token: 0x04001865 RID: 6245
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04001866 RID: 6246
		private GenericSkill primarySkillSlot;

		// Token: 0x04001867 RID: 6247
		private GameObject effectMuzzleInstance;

		// Token: 0x04001868 RID: 6248
		private Animator modelAnimator;

		// Token: 0x04001869 RID: 6249
		private float timerSinceComplete;

		// Token: 0x0400186A RID: 6250
		private bool beginExit;
	}
}
