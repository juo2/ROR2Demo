using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B8B RID: 2955
	[RequireComponent(typeof(Deployable))]
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileDeployToOwner : MonoBehaviour
	{
		// Token: 0x06004349 RID: 17225 RVA: 0x00117137 File Offset: 0x00115337
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.DeployToOwner();
			}
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x00117148 File Offset: 0x00115348
		private void DeployToOwner()
		{
			GameObject owner = base.GetComponent<ProjectileController>().owner;
			if (owner)
			{
				CharacterBody component = owner.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						master.AddDeployable(base.GetComponent<Deployable>(), this.deployableSlot);
					}
				}
			}
		}

		// Token: 0x04004167 RID: 16743
		public DeployableSlot deployableSlot;
	}
}
