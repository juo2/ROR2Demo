using System;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A9 RID: 425
	public class ToolbotStanceBase : BaseState
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x00020D8A File Offset: 0x0001EF8A
		protected void SetPrimarySkill()
		{
			base.skillLocator.primary = this.GetCurrentPrimarySkill();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00020D9D File Offset: 0x0001EF9D
		protected void SetSecondarySkill(string skillName)
		{
			if (base.skillLocator)
			{
				base.skillLocator.secondary = base.skillLocator.FindSkill(skillName);
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00020DC3 File Offset: 0x0001EFC3
		protected string GetSkillSlotStance(GenericSkill skillSlot)
		{
			ToolbotWeaponSkillDef toolbotWeaponSkillDef = skillSlot.skillDef as ToolbotWeaponSkillDef;
			return ((toolbotWeaponSkillDef != null) ? toolbotWeaponSkillDef.stanceName : null) ?? string.Empty;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00020DE5 File Offset: 0x0001EFE5
		protected GenericSkill GetPrimarySkill1()
		{
			return base.skillLocator.FindSkillByFamilyName("ToolbotBodyPrimary1");
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020DF7 File Offset: 0x0001EFF7
		protected GenericSkill GetPrimarySkill2()
		{
			return base.skillLocator.FindSkillByFamilyName("ToolbotBodyPrimary2");
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual GenericSkill GetCurrentPrimarySkill()
		{
			return null;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00020E0C File Offset: 0x0001F00C
		protected void UpdateCrosshairParameters(ToolbotWeaponSkillDef weaponSkillDef)
		{
			GameObject crosshairPrefab = ToolbotStanceBase.emptyCrosshairPrefab;
			AnimationCurve crosshairSpreadCurve = ToolbotStanceBase.emptyCrosshairSpreadCurve;
			if (weaponSkillDef)
			{
				crosshairPrefab = weaponSkillDef.crosshairPrefab;
				crosshairSpreadCurve = weaponSkillDef.crosshairSpreadCurve;
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, crosshairPrefab, CrosshairUtils.OverridePriority.Skill);
			base.characterBody.spreadBloomCurve = crosshairSpreadCurve;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00020E6B File Offset: 0x0001F06B
		protected void SetEquipmentSlot(byte i)
		{
			if (this.inventory)
			{
				this.inventory.SetActiveEquipmentSlot(i);
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00020E88 File Offset: 0x0001F088
		public override void OnEnter()
		{
			base.OnEnter();
			this.inventory = (base.characterBody ? base.characterBody.inventory : null);
			this.SetPrimarySkill();
			GenericSkill currentPrimarySkill = this.GetCurrentPrimarySkill();
			ToolbotWeaponSkillDef toolbotWeaponSkillDef = ((currentPrimarySkill != null) ? currentPrimarySkill.skillDef : null) as ToolbotWeaponSkillDef;
			if (toolbotWeaponSkillDef)
			{
				this.SendWeaponStanceToAnimator(toolbotWeaponSkillDef);
				base.PlayCrossfade("Gesture, Additive", toolbotWeaponSkillDef.enterGestureAnimState, 0.2f);
			}
			this.UpdateCrosshairParameters(toolbotWeaponSkillDef);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00020F06 File Offset: 0x0001F106
		public override void OnExit()
		{
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00020F1F File Offset: 0x0001F11F
		protected void SendWeaponStanceToAnimator(ToolbotWeaponSkillDef weaponSkillDef)
		{
			if (weaponSkillDef)
			{
				base.GetModelAnimator().SetInteger("weaponStance", weaponSkillDef.animatorWeaponIndex);
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020F40 File Offset: 0x0001F140
		protected static ToolbotStanceBase.WeaponStance GetSkillStance(GenericSkill skillSlot)
		{
			ToolbotWeaponSkillDef toolbotWeaponSkillDef = ((skillSlot != null) ? skillSlot.skillDef : null) as ToolbotWeaponSkillDef;
			string a = (toolbotWeaponSkillDef != null) ? toolbotWeaponSkillDef.stanceName : null;
			if (a == "Nailgun")
			{
				return ToolbotStanceBase.WeaponStance.Nailgun;
			}
			if (a == "Spear")
			{
				return ToolbotStanceBase.WeaponStance.Spear;
			}
			if (a == "Grenade")
			{
				return ToolbotStanceBase.WeaponStance.Grenade;
			}
			if (!(a == "Buzzsaw"))
			{
				return ToolbotStanceBase.WeaponStance.None;
			}
			return ToolbotStanceBase.WeaponStance.Buzzsaw;
		}

		// Token: 0x04000941 RID: 2369
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000942 RID: 2370
		public static GameObject emptyCrosshairPrefab;

		// Token: 0x04000943 RID: 2371
		public static AnimationCurve emptyCrosshairSpreadCurve;

		// Token: 0x04000944 RID: 2372
		protected Inventory inventory;

		// Token: 0x04000945 RID: 2373
		public Type swapStateType;

		// Token: 0x020001AA RID: 426
		protected enum WeaponStance
		{
			// Token: 0x04000947 RID: 2375
			None = -1,
			// Token: 0x04000948 RID: 2376
			Nailgun,
			// Token: 0x04000949 RID: 2377
			Spear,
			// Token: 0x0400094A RID: 2378
			Grenade,
			// Token: 0x0400094B RID: 2379
			Buzzsaw
		}
	}
}
