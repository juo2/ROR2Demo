using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BA3 RID: 2979
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileInstantiateDeployable : MonoBehaviour
	{
		// Token: 0x060043B5 RID: 17333 RVA: 0x001196DF File Offset: 0x001178DF
		public void Start()
		{
			if (this.instantiateOnStart)
			{
				this.InstantiateDeployable();
			}
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x001196F0 File Offset: 0x001178F0
		public void InstantiateDeployable()
		{
			if (NetworkServer.active)
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
							Vector3 position = this.targetTransform ? this.targetTransform.position : Vector3.zero;
							Quaternion rotation = this.copyTargetRotation ? this.targetTransform.rotation : Quaternion.identity;
							Transform parent = this.parentToTarget ? this.targetTransform : null;
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation, parent);
							NetworkServer.Spawn(gameObject);
							master.AddDeployable(gameObject.GetComponent<Deployable>(), this.deployableSlot);
						}
					}
				}
			}
		}

		// Token: 0x04004219 RID: 16921
		[Tooltip("The deployable slot to use.")]
		[SerializeField]
		private DeployableSlot deployableSlot;

		// Token: 0x0400421A RID: 16922
		[SerializeField]
		[Tooltip("The prefab to instantiate.")]
		private GameObject prefab;

		// Token: 0x0400421B RID: 16923
		[Tooltip("The object upon which the prefab will be positioned.")]
		[SerializeField]
		private Transform targetTransform;

		// Token: 0x0400421C RID: 16924
		[Tooltip("The transform upon which to instantiate the prefab.")]
		[SerializeField]
		private bool copyTargetRotation;

		// Token: 0x0400421D RID: 16925
		[SerializeField]
		[Tooltip("Whether or not to parent the instantiated prefab to the specified transform.")]
		private bool parentToTarget;

		// Token: 0x0400421E RID: 16926
		[Tooltip("Whether or not to instantiate this prefab. If so, this will only run on the server, and will be spawned over the network.")]
		[SerializeField]
		private bool instantiateOnStart = true;
	}
}
