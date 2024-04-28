using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006BF RID: 1727
	[DisallowMultipleComponent]
	public class EntityLocator : MonoBehaviour
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x00090A04 File Offset: 0x0008EC04
		public static GameObject GetEntity(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return null;
			}
			EntityLocator component = gameObject.GetComponent<EntityLocator>();
			if (!component)
			{
				return null;
			}
			return component.entity;
		}

		// Token: 0x04002701 RID: 9985
		[Tooltip("The root gameobject of the entity.")]
		public GameObject entity;
	}
}
