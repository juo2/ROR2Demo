using System;
using UnityEngine;

namespace RoR2.Achievements.Merc
{
	// Token: 0x02000EDF RID: 3807
	[RegisterAchievement("MercDontTouchGround", "Skills.Merc.Uppercut", "CompleteUnknownEnding", null)]
	public class MercDontTouchGroundAchievement : BaseAchievement
	{
		// Token: 0x060056AE RID: 22190 RVA: 0x001602A3 File Offset: 0x0015E4A3
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MercBody");
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x001604D5 File Offset: 0x0015E6D5
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			RoR2Application.onFixedUpdate += this.MercFixedUpdate;
			base.localUser.onBodyChanged += this.OnBodyChanged;
			this.OnBodyChanged();
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x0016050B File Offset: 0x0015E70B
		protected override void OnBodyRequirementBroken()
		{
			base.localUser.onBodyChanged -= this.OnBodyChanged;
			RoR2Application.onFixedUpdate -= this.MercFixedUpdate;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x0016053B File Offset: 0x0015E73B
		private void OnBodyChanged()
		{
			this.body = base.localUser.cachedBody;
			this.motor = (this.body ? this.body.characterMotor : null);
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00160570 File Offset: 0x0015E770
		private void MercFixedUpdate()
		{
			this.stopwatch = ((this.motor && !this.motor.isGrounded && !this.body.currentVehicle) ? (this.stopwatch + Time.fixedDeltaTime) : 0f);
			if (MercDontTouchGroundAchievement.requirement <= this.stopwatch)
			{
				base.Grant();
			}
		}

		// Token: 0x0400509E RID: 20638
		private static readonly float requirement = 30f;

		// Token: 0x0400509F RID: 20639
		private CharacterMotor motor;

		// Token: 0x040050A0 RID: 20640
		private CharacterBody body;

		// Token: 0x040050A1 RID: 20641
		private float stopwatch;
	}
}
