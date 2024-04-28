using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000897 RID: 2199
	[DefaultExecutionOrder(99999)]
	public class SimpleRotateToDirection : MonoBehaviour
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000CE321 File Offset: 0x000CC521
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x000CE329 File Offset: 0x000CC529
		public Quaternion targetRotation { get; set; }

		// Token: 0x0600308D RID: 12429 RVA: 0x000CE332 File Offset: 0x000CC532
		private void Awake()
		{
			this.transform = base.transform;
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000CE34C File Offset: 0x000CC54C
		private void OnEnable()
		{
			this.targetRotation = this.transform.rotation;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000CE35F File Offset: 0x000CC55F
		private void LateUpdate()
		{
			this.isNetworkControlled = (this.networkIdentity != null && !Util.HasEffectiveAuthority(this.networkIdentity));
			if (!this.isNetworkControlled)
			{
				this.Simulate(Time.deltaTime);
			}
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000CE393 File Offset: 0x000CC593
		private void Simulate(float deltaTime)
		{
			this.transform.rotation = Util.SmoothDampQuaternion(this.transform.rotation, this.targetRotation, ref this.velocity, this.smoothTime, this.maxRotationSpeed, deltaTime);
		}

		// Token: 0x04003230 RID: 12848
		public float smoothTime;

		// Token: 0x04003231 RID: 12849
		public float maxRotationSpeed;

		// Token: 0x04003232 RID: 12850
		private new Transform transform;

		// Token: 0x04003233 RID: 12851
		[CanBeNull]
		private NetworkIdentity networkIdentity;

		// Token: 0x04003235 RID: 12853
		private float velocity;

		// Token: 0x04003236 RID: 12854
		private bool isNetworkControlled;
	}
}
