using System;
using System.Collections.Generic;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007EC RID: 2028
	public class OrbFireZone : MonoBehaviour
	{
		// Token: 0x06002BC8 RID: 11208 RVA: 0x000026ED File Offset: 0x000008ED
		private void Awake()
		{
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000BB5D4 File Offset: 0x000B97D4
		private void FixedUpdate()
		{
			if (this.previousColliderList.Count > 0)
			{
				this.resetStopwatch += Time.fixedDeltaTime;
				this.removeFromBottomOfListStopwatch += Time.fixedDeltaTime;
				if (this.removeFromBottomOfListStopwatch > 1f / this.orbRemoveFromBottomOfListFrequency)
				{
					this.removeFromBottomOfListStopwatch -= 1f / this.orbRemoveFromBottomOfListFrequency;
					this.previousColliderList.RemoveAt(this.previousColliderList.Count - 1);
				}
				if (this.resetStopwatch > 1f / this.orbResetListFrequency)
				{
					this.resetStopwatch -= 1f / this.orbResetListFrequency;
					this.previousColliderList.Clear();
				}
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000BB694 File Offset: 0x000B9894
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active)
			{
				if (this.previousColliderList.Contains(other))
				{
					return;
				}
				this.previousColliderList.Add(other);
				CharacterBody component = other.GetComponent<CharacterBody>();
				if (component && component.mainHurtBox)
				{
					DamageOrb damageOrb = new DamageOrb();
					damageOrb.attacker = null;
					damageOrb.damageOrbType = DamageOrb.DamageOrbType.ClayGooOrb;
					damageOrb.procCoefficient = this.procCoefficient;
					damageOrb.damageValue = this.baseDamage * Run.instance.teamlessDamageCoefficient;
					damageOrb.target = component.mainHurtBox;
					damageOrb.teamIndex = TeamIndex.None;
					RaycastHit raycastHit;
					if (Physics.Raycast(damageOrb.target.transform.position + UnityEngine.Random.insideUnitSphere * 3f, Vector3.down, out raycastHit, 1000f, LayerIndex.world.mask))
					{
						damageOrb.origin = raycastHit.point;
						OrbManager.instance.AddOrb(damageOrb);
					}
				}
			}
		}

		// Token: 0x04002E32 RID: 11826
		public float baseDamage;

		// Token: 0x04002E33 RID: 11827
		public float procCoefficient;

		// Token: 0x04002E34 RID: 11828
		public float orbRemoveFromBottomOfListFrequency;

		// Token: 0x04002E35 RID: 11829
		public float orbResetListFrequency;

		// Token: 0x04002E36 RID: 11830
		private List<Collider> previousColliderList = new List<Collider>();

		// Token: 0x04002E37 RID: 11831
		private float resetStopwatch;

		// Token: 0x04002E38 RID: 11832
		private float removeFromBottomOfListStopwatch;
	}
}
