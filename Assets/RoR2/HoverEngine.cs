using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000737 RID: 1847
	public class HoverEngine : MonoBehaviour
	{
		// Token: 0x06002672 RID: 9842 RVA: 0x000A75AC File Offset: 0x000A57AC
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(base.transform.TransformPoint(this.offsetVector), this.hoverRadius);
			Gizmos.DrawRay(this.castRay);
			if (this.isGrounded)
			{
				Gizmos.DrawSphere(this.raycastHit.point, this.hoverRadius);
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000A7608 File Offset: 0x000A5808
		private void FixedUpdate()
		{
			float num = Mathf.Clamp01(Vector3.Dot(-base.transform.up, Vector3.down));
			this.castPosition = base.transform.TransformPoint(this.offsetVector);
			this.castRay = new Ray(this.castPosition, -base.transform.up);
			this.isGrounded = false;
			this.forceStrength = 0f;
			this.compression = 0f;
			Vector3 position = this.castRay.origin + this.castRay.direction * this.hoverHeight;
			if (Physics.SphereCast(this.castRay, this.hoverRadius, out this.raycastHit, this.hoverHeight, LayerIndex.world.mask))
			{
				this.isGrounded = true;
				float num2 = (this.hoverHeight - this.raycastHit.distance) / this.hoverHeight;
				Vector3 a = Vector3.up * (num2 * this.hoverForce);
				Vector3 b = Vector3.Project(this.engineRigidbody.GetPointVelocity(this.castPosition), -base.transform.up) * this.hoverDamping;
				this.forceStrength = (a - b).magnitude;
				this.engineRigidbody.AddForceAtPosition(Vector3.Project(a - b, -base.transform.up), this.castPosition, ForceMode.Acceleration);
				this.compression = Mathf.Clamp01(num2 * num);
				position = this.raycastHit.point;
			}
			this.wheelVisual.position = position;
			bool flag = this.isGrounded;
		}

		// Token: 0x04002A3E RID: 10814
		public Rigidbody engineRigidbody;

		// Token: 0x04002A3F RID: 10815
		public Transform wheelVisual;

		// Token: 0x04002A40 RID: 10816
		public float hoverForce = 65f;

		// Token: 0x04002A41 RID: 10817
		public float hoverHeight = 3.5f;

		// Token: 0x04002A42 RID: 10818
		public float hoverDamping = 0.1f;

		// Token: 0x04002A43 RID: 10819
		public float hoverRadius;

		// Token: 0x04002A44 RID: 10820
		[HideInInspector]
		public float forceStrength;

		// Token: 0x04002A45 RID: 10821
		private Ray castRay;

		// Token: 0x04002A46 RID: 10822
		private Vector3 castPosition;

		// Token: 0x04002A47 RID: 10823
		[HideInInspector]
		public RaycastHit raycastHit;

		// Token: 0x04002A48 RID: 10824
		public float compression;

		// Token: 0x04002A49 RID: 10825
		public Vector3 offsetVector = Vector3.zero;

		// Token: 0x04002A4A RID: 10826
		public bool isGrounded;
	}
}
