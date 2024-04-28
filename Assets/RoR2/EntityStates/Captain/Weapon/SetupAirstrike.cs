using System;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x0200041C RID: 1052
	public class SetupAirstrike : BaseState
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000546EE File Offset: 0x000528EE
		private float exitDuration
		{
			get
			{
				return SetupAirstrike.baseExitDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000546FC File Offset: 0x000528FC
		public override void OnEnter()
		{
			base.OnEnter();
			this.primarySkillSlot = (base.skillLocator ? base.skillLocator.primary : null);
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.SetSkillOverride(this, SetupAirstrike.primarySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetBool("PrepAirstrike", true);
			}
			base.PlayCrossfade("Gesture, Override", "PrepAirstrike", 0.1f);
			base.PlayCrossfade("Gesture, Additive", "PrepAirstrike", 0.1f);
			Transform transform = base.FindModelChild(SetupAirstrike.effectMuzzleString);
			if (transform)
			{
				this.effectMuzzleInstance = UnityEngine.Object.Instantiate<GameObject>(SetupAirstrike.effectMuzzlePrefab, transform);
			}
			if (SetupAirstrike.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, SetupAirstrike.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			Util.PlaySound(SetupAirstrike.enterSoundString, base.gameObject);
			Util.PlaySound("Play_captain_shift_active_loop", base.gameObject);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00054810 File Offset: 0x00052A10
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

		// Token: 0x060012F6 RID: 4854 RVA: 0x000548A0 File Offset: 0x00052AA0
		public override void OnExit()
		{
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.UnsetSkillOverride(this, SetupAirstrike.primarySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			Util.PlaySound(SetupAirstrike.exitSoundString, base.gameObject);
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

		// Token: 0x04001851 RID: 6225
		public static SkillDef primarySkillDef;

		// Token: 0x04001852 RID: 6226
		public static GameObject crosshairOverridePrefab;

		// Token: 0x04001853 RID: 6227
		public static string enterSoundString;

		// Token: 0x04001854 RID: 6228
		public static string exitSoundString;

		// Token: 0x04001855 RID: 6229
		public static GameObject effectMuzzlePrefab;

		// Token: 0x04001856 RID: 6230
		public static string effectMuzzleString;

		// Token: 0x04001857 RID: 6231
		public static float baseExitDuration;

		// Token: 0x04001858 RID: 6232
		private GenericSkill primarySkillSlot;

		// Token: 0x04001859 RID: 6233
		private GameObject effectMuzzleInstance;

		// Token: 0x0400185A RID: 6234
		private Animator modelAnimator;

		// Token: 0x0400185B RID: 6235
		private float timerSinceComplete;

		// Token: 0x0400185C RID: 6236
		private bool beginExit;

		// Token: 0x0400185D RID: 6237
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
	}
}
