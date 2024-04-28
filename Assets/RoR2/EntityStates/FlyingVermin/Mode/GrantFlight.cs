using System;
using RoR2;

namespace EntityStates.FlyingVermin.Mode
{
	// Token: 0x02000386 RID: 902
	public class GrantFlight : BaseState
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x000471E4 File Offset: 0x000453E4
		public override void OnEnter()
		{
			base.OnEnter();
			this.characterGravityParameterProvider = base.gameObject.GetComponent<ICharacterGravityParameterProvider>();
			this.characterFlightParameterProvider = base.gameObject.GetComponent<ICharacterFlightParameterProvider>();
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
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00047274 File Offset: 0x00045474
		public override void OnExit()
		{
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
			base.OnExit();
		}

		// Token: 0x04001497 RID: 5271
		protected ICharacterGravityParameterProvider characterGravityParameterProvider;

		// Token: 0x04001498 RID: 5272
		protected ICharacterFlightParameterProvider characterFlightParameterProvider;
	}
}
