using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Fauna
{
	// Token: 0x02000388 RID: 904
	public class VultureEggDeathState : BirdsharkDeathState
	{
		// Token: 0x0600102E RID: 4142 RVA: 0x0004739C File Offset: 0x0004559C
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				for (int i = 0; i < VultureEggDeathState.healPackCount; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/HealPack"), base.transform.position, UnityEngine.Random.rotation);
					gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
					gameObject.GetComponentInChildren<HealthPickup>().fractionalHealing = VultureEggDeathState.fractionalHealing;
					gameObject.transform.localScale = new Vector3(VultureEggDeathState.scale, VultureEggDeathState.scale, VultureEggDeathState.scale);
					gameObject.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere * VultureEggDeathState.healPackMaxVelocity, ForceMode.VelocityChange);
					NetworkServer.Spawn(gameObject);
				}
			}
		}

		// Token: 0x0400149B RID: 5275
		public static int healPackCount;

		// Token: 0x0400149C RID: 5276
		public static float healPackMaxVelocity;

		// Token: 0x0400149D RID: 5277
		public static float fractionalHealing;

		// Token: 0x0400149E RID: 5278
		public static float scale;
	}
}
