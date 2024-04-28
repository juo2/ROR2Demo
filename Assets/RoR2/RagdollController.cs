using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200083C RID: 2108
	public class RagdollController : MonoBehaviour
	{
		// Token: 0x06002E05 RID: 11781 RVA: 0x000C3CBC File Offset: 0x000C1EBC
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
			foreach (Transform transform in this.bones)
			{
				Collider component = transform.GetComponent<Collider>();
				Rigidbody component2 = transform.GetComponent<Rigidbody>();
				if (!component)
				{
					Debug.LogWarningFormat("Bone {0} is missing a collider!", new object[]
					{
						transform
					});
				}
				else
				{
					component.enabled = false;
					component2.interpolation = RigidbodyInterpolation.None;
					component2.isKinematic = true;
				}
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000C3D34 File Offset: 0x000C1F34
		public void BeginRagdoll(Vector3 force)
		{
			if (this.animator)
			{
				this.animator.enabled = false;
			}
			foreach (Transform transform in this.bones)
			{
				if (transform.gameObject.layer == LayerIndex.ragdoll.intVal)
				{
					transform.parent = base.transform;
					Rigidbody component = transform.GetComponent<Rigidbody>();
					transform.GetComponent<Collider>().enabled = true;
					component.isKinematic = false;
					component.interpolation = RigidbodyInterpolation.Interpolate;
					component.collisionDetectionMode = CollisionDetectionMode.Continuous;
					component.AddForce(force * UnityEngine.Random.Range(0.9f, 1.2f), ForceMode.VelocityChange);
				}
			}
			MonoBehaviour[] array2 = this.componentsToDisableOnRagdoll;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
		}

		// Token: 0x04002FE6 RID: 12262
		public Transform[] bones;

		// Token: 0x04002FE7 RID: 12263
		public MonoBehaviour[] componentsToDisableOnRagdoll;

		// Token: 0x04002FE8 RID: 12264
		private Animator animator;
	}
}
