using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B87 RID: 2951
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileCharacterController : MonoBehaviour
	{
		// Token: 0x0600431C RID: 17180 RVA: 0x00116814 File Offset: 0x00114A14
		private void Awake()
		{
			this.downVector = Vector3.down * 3f;
			this.projectileController = base.GetComponent<ProjectileController>();
			this.characterController = base.GetComponent<CharacterController>();
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x00116844 File Offset: 0x00114A44
		private void FixedUpdate()
		{
			if (NetworkServer.active || this.projectileController.isPrediction)
			{
				this.characterController.Move((base.transform.forward + this.downVector) * (this.velocity * Time.fixedDeltaTime));
			}
			if (NetworkServer.active)
			{
				this.timer += Time.fixedDeltaTime;
				if (this.timer > this.lifetime)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x0400413F RID: 16703
		private Vector3 downVector;

		// Token: 0x04004140 RID: 16704
		public float velocity;

		// Token: 0x04004141 RID: 16705
		public float lifetime = 5f;

		// Token: 0x04004142 RID: 16706
		private float timer;

		// Token: 0x04004143 RID: 16707
		private ProjectileController projectileController;

		// Token: 0x04004144 RID: 16708
		private CharacterController characterController;
	}
}
