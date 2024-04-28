using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000601 RID: 1537
	public class BuffPassengerWhileSeated : MonoBehaviour
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x00077BC8 File Offset: 0x00075DC8
		private void OnEnable()
		{
			if (this.vehicleSeat)
			{
				if (this.vehicleSeat.currentPassengerBody)
				{
					this.AddBuff(this.vehicleSeat.currentPassengerBody);
				}
				this.vehicleSeat.onPassengerEnter += this.OnPassengerEnter;
				this.vehicleSeat.onPassengerExit += this.OnPassengerExit;
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00077C34 File Offset: 0x00075E34
		private void OnDisable()
		{
			if (this.vehicleSeat)
			{
				if (this.vehicleSeat.currentPassengerBody)
				{
					this.RemoveBuff(this.vehicleSeat.currentPassengerBody);
				}
				this.vehicleSeat.onPassengerEnter -= this.OnPassengerEnter;
				this.vehicleSeat.onPassengerExit -= this.OnPassengerExit;
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00077CA0 File Offset: 0x00075EA0
		private void OnPassengerEnter(GameObject passengerObject)
		{
			CharacterBody component = passengerObject.GetComponent<CharacterBody>();
			if (component)
			{
				this.AddBuff(component);
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00077CC4 File Offset: 0x00075EC4
		private void OnPassengerExit(GameObject passengerObject)
		{
			CharacterBody component = passengerObject.GetComponent<CharacterBody>();
			if (component)
			{
				this.RemoveBuff(component);
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00077CE7 File Offset: 0x00075EE7
		private void AddBuff(CharacterBody passengerBody)
		{
			passengerBody.AddBuff(this.buff);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00077CF5 File Offset: 0x00075EF5
		private void RemoveBuff(CharacterBody passengerBody)
		{
			passengerBody.RemoveBuff(this.buff);
		}

		// Token: 0x040021E5 RID: 8677
		[SerializeField]
		private BuffDef buff;

		// Token: 0x040021E6 RID: 8678
		[SerializeField]
		private VehicleSeat vehicleSeat;
	}
}
