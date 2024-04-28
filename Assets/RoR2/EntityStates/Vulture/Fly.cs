using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Vulture
{
	// Token: 0x020000E1 RID: 225
	public class Fly : VultureModeState
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.characterGravityParameterProvider != null)
			{
				CharacterGravityParameters gravityParameters = this.characterGravityParameterProvider.gravityParameters;
				gravityParameters.channeledAntiGravityGranterCount++;
				this.characterGravityParameterProvider.gravityParameters = gravityParameters;
			}
			if (this.characterFlightParameterProvider != null)
			{
				CharacterFlightParameters flightParameters = this.characterFlightParameterProvider.flightParameters;
				flightParameters.channeledFlightGranterCount++;
				this.characterFlightParameterProvider.flightParameters = flightParameters;
			}
			if (base.characterMotor)
			{
				base.characterMotor.velocity.y = Fly.launchSpeed;
				base.characterMotor.Motor.ForceUnground();
			}
			this.PlayAnimation("Body", "Jump");
			if (base.activatorSkillSlot)
			{
				base.activatorSkillSlot.SetSkillOverride(this, Fly.landingSkill, GenericSkill.SkillOverridePriority.Contextual);
			}
			if (Fly.jumpEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(Fly.jumpEffectPrefab, base.gameObject, Fly.jumpEffectMuzzleString, false);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public override void OnExit()
		{
			if (base.activatorSkillSlot)
			{
				base.activatorSkillSlot.UnsetSkillOverride(this, Fly.landingSkill, GenericSkill.SkillOverridePriority.Contextual);
			}
			if (this.characterFlightParameterProvider != null)
			{
				CharacterFlightParameters flightParameters = this.characterFlightParameterProvider.flightParameters;
				flightParameters.channeledFlightGranterCount--;
				this.characterFlightParameterProvider.flightParameters = flightParameters;
			}
			if (this.characterGravityParameterProvider != null)
			{
				CharacterGravityParameters gravityParameters = this.characterGravityParameterProvider.gravityParameters;
				gravityParameters.channeledAntiGravityGranterCount--;
				this.characterGravityParameterProvider.gravityParameters = gravityParameters;
			}
			if (base.modelLocator)
			{
				base.modelLocator.normalizeToFloor = true;
			}
			base.OnExit();
		}

		// Token: 0x0400040F RID: 1039
		public static SkillDef landingSkill;

		// Token: 0x04000410 RID: 1040
		public static float launchSpeed;

		// Token: 0x04000411 RID: 1041
		public static GameObject jumpEffectPrefab;

		// Token: 0x04000412 RID: 1042
		public static string jumpEffectMuzzleString;
	}
}
