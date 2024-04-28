using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Orbs
{
	// Token: 0x02000B07 RID: 2823
	public class BeetleWardOrb : Orb
	{
		// Token: 0x06004097 RID: 16535 RVA: 0x0010AE44 File Offset: 0x00109044
		public override void Begin()
		{
			base.duration = base.distanceToTarget / this.speed;
			EffectData effectData = new EffectData
			{
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/BeetleWardOrbEffect"), effectData, true);
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0010AEA0 File Offset: 0x001090A0
		public override void OnArrival()
		{
			if (this.target)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BeetleWard"), this.target.transform.position, Quaternion.identity);
				gameObject.GetComponent<TeamFilter>().teamIndex = this.target.teamIndex;
				NetworkServer.Spawn(gameObject);
			}
		}

		// Token: 0x04003EDE RID: 16094
		public float speed;
	}
}
