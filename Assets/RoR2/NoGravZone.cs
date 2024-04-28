using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004E0 RID: 1248
	public class NoGravZone : MonoBehaviour
	{
		// Token: 0x06001698 RID: 5784 RVA: 0x00063FB4 File Offset: 0x000621B4
		public void OnTriggerEnter(Collider other)
		{
			ICharacterGravityParameterProvider component = other.GetComponent<ICharacterGravityParameterProvider>();
			if (component != null)
			{
				CharacterGravityParameters gravityParameters = component.gravityParameters;
				gravityParameters.environmentalAntiGravityGranterCount++;
				component.gravityParameters = gravityParameters;
			}
			ICharacterFlightParameterProvider component2 = other.GetComponent<ICharacterFlightParameterProvider>();
			if (component2 != null)
			{
				CharacterFlightParameters flightParameters = component2.flightParameters;
				flightParameters.channeledFlightGranterCount++;
				component2.flightParameters = flightParameters;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0006400C File Offset: 0x0006220C
		public void OnTriggerExit(Collider other)
		{
			ICharacterFlightParameterProvider component = other.GetComponent<ICharacterFlightParameterProvider>();
			if (component != null)
			{
				CharacterFlightParameters flightParameters = component.flightParameters;
				flightParameters.channeledFlightGranterCount--;
				component.flightParameters = flightParameters;
			}
			ICharacterGravityParameterProvider component2 = other.GetComponent<ICharacterGravityParameterProvider>();
			if (component2 != null)
			{
				CharacterGravityParameters gravityParameters = component2.gravityParameters;
				gravityParameters.environmentalAntiGravityGranterCount--;
				component2.gravityParameters = gravityParameters;
			}
		}
	}
}
