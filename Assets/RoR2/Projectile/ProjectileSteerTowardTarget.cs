using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BBC RID: 3004
	[RequireComponent(typeof(ProjectileTargetComponent))]
	public class ProjectileSteerTowardTarget : MonoBehaviour
	{
		// Token: 0x06004470 RID: 17520 RVA: 0x0011CF5D File Offset: 0x0011B15D
		private void Start()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
				return;
			}
			this.transform = base.transform;
			this.targetComponent = base.GetComponent<ProjectileTargetComponent>();
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x0011CF88 File Offset: 0x0011B188
		private void FixedUpdate()
		{
			if (this.targetComponent.target)
			{
				Vector3 vector = this.targetComponent.target.transform.position - this.transform.position;
				if (this.yAxisOnly)
				{
					vector.y = 0f;
				}
				if (vector != Vector3.zero)
				{
					this.transform.forward = Vector3.RotateTowards(this.transform.forward, vector, this.rotationSpeed * 0.017453292f * Time.fixedDeltaTime, 0f);
				}
			}
		}

		// Token: 0x040042E7 RID: 17127
		[Tooltip("Constrains rotation to the Y axis only.")]
		public bool yAxisOnly;

		// Token: 0x040042E8 RID: 17128
		[Tooltip("How fast to rotate in degrees per second. Rotation is linear.")]
		public float rotationSpeed;

		// Token: 0x040042E9 RID: 17129
		private new Transform transform;

		// Token: 0x040042EA RID: 17130
		private ProjectileTargetComponent targetComponent;
	}
}
