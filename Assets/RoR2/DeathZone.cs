using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200068E RID: 1678
	internal class DeathZone : MonoBehaviour
	{
		// Token: 0x060020D0 RID: 8400 RVA: 0x0008D4CC File Offset: 0x0008B6CC
		public void OnTriggerEnter(Collider other)
		{
			if (NetworkServer.active)
			{
				HealthComponent component = other.GetComponent<HealthComponent>();
				if (component)
				{
					component.TakeDamage(new DamageInfo
					{
						position = other.transform.position,
						attacker = null,
						inflictor = base.gameObject,
						damage = 999999f
					});
				}
			}
		}
	}
}
