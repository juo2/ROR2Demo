using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000896 RID: 2198
	[DefaultExecutionOrder(99999)]
	public class SimpleLeash : MonoBehaviour
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000CE1B2 File Offset: 0x000CC3B2
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000CE1BA File Offset: 0x000CC3BA
		public Vector3 leashOrigin { get; set; }

		// Token: 0x06003086 RID: 12422 RVA: 0x000CE1C3 File Offset: 0x000CC3C3
		private void Awake()
		{
			this.transform = base.transform;
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000CE1DD File Offset: 0x000CC3DD
		private void OnEnable()
		{
			this.leashOrigin = this.transform.position;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000CE1F0 File Offset: 0x000CC3F0
		private void LateUpdate()
		{
			this.isNetworkControlled = (this.networkIdentity != null && !Util.HasEffectiveAuthority(this.networkIdentity));
			if (!this.isNetworkControlled)
			{
				this.Simulate(Time.deltaTime);
			}
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000CE224 File Offset: 0x000CC424
		private void Simulate(float deltaTime)
		{
			Vector3 position = this.transform.position;
			Vector3 leashOrigin = this.leashOrigin;
			Vector3 a = position - leashOrigin;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > this.minLeashRadius * this.minLeashRadius)
			{
				float num = Mathf.Sqrt(sqrMagnitude);
				Vector3 a2 = a / num;
				Vector3 target = leashOrigin + a2 * this.minLeashRadius;
				Vector3 vector = position;
				if (num > this.maxLeashRadius)
				{
					vector = leashOrigin + a2 * this.maxLeashRadius;
				}
				vector = Vector3.SmoothDamp(vector, target, ref this.velocity, this.smoothTime, this.maxFollowSpeed, deltaTime);
				if (vector != position)
				{
					this.transform.position = vector;
				}
			}
		}

		// Token: 0x04003227 RID: 12839
		public float minLeashRadius = 1f;

		// Token: 0x04003228 RID: 12840
		public float maxLeashRadius = 20f;

		// Token: 0x04003229 RID: 12841
		public float maxFollowSpeed = 40f;

		// Token: 0x0400322A RID: 12842
		public float smoothTime = 0.15f;

		// Token: 0x0400322B RID: 12843
		private new Transform transform;

		// Token: 0x0400322C RID: 12844
		[CanBeNull]
		private NetworkIdentity networkIdentity;

		// Token: 0x0400322E RID: 12846
		private Vector3 velocity = Vector3.zero;

		// Token: 0x0400322F RID: 12847
		private bool isNetworkControlled;
	}
}
