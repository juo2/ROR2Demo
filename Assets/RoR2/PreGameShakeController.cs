using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200082B RID: 2091
	public class PreGameShakeController : MonoBehaviour
	{
		// Token: 0x06002D79 RID: 11641 RVA: 0x000C1CD3 File Offset: 0x000BFED3
		private void ResetTimer()
		{
			this.timer = UnityEngine.Random.Range(this.minInterval, this.maxInterval);
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000C1CEC File Offset: 0x000BFEEC
		private void DoShake()
		{
			this.shakeEmitter.StartShake();
			Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
			foreach (Rigidbody rigidbody in this.physicsBodies)
			{
				if (rigidbody)
				{
					Vector3 force = onUnitSphere * ((0.75f + UnityEngine.Random.value * 0.25f) * this.physicsForce);
					float y = rigidbody.GetComponent<Collider>().bounds.min.y;
					Vector3 centerOfMass = rigidbody.centerOfMass;
					centerOfMass.y = y;
					rigidbody.AddForceAtPosition(force, centerOfMass);
				}
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000C1D81 File Offset: 0x000BFF81
		private void Awake()
		{
			this.ResetTimer();
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000C1D89 File Offset: 0x000BFF89
		private void Update()
		{
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.ResetTimer();
				this.DoShake();
			}
		}

		// Token: 0x04002F70 RID: 12144
		public ShakeEmitter shakeEmitter;

		// Token: 0x04002F71 RID: 12145
		public float minInterval = 0.5f;

		// Token: 0x04002F72 RID: 12146
		public float maxInterval = 7f;

		// Token: 0x04002F73 RID: 12147
		public Rigidbody[] physicsBodies;

		// Token: 0x04002F74 RID: 12148
		public float physicsForce;

		// Token: 0x04002F75 RID: 12149
		private float timer;
	}
}
