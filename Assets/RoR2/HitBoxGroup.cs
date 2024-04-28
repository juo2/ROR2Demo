using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200072C RID: 1836
	public class HitBoxGroup : MonoBehaviour
	{
		// Token: 0x06002624 RID: 9764 RVA: 0x000A6660 File Offset: 0x000A4860
		public static HitBoxGroup FindByGroupName(GameObject gameObject, string groupName)
		{
			List<HitBoxGroup> gameObjectComponents = GetComponentsCache<HitBoxGroup>.GetGameObjectComponents(gameObject);
			HitBoxGroup result = null;
			int i = 0;
			int count = gameObjectComponents.Count;
			while (i < count)
			{
				if (string.CompareOrdinal(groupName, gameObjectComponents[i].groupName) == 0)
				{
					result = gameObjectComponents[i];
					break;
				}
				i++;
			}
			GetComponentsCache<HitBoxGroup>.ReturnBuffer(gameObjectComponents);
			return result;
		}

		// Token: 0x04002A02 RID: 10754
		[Tooltip("The name of this hitbox group.")]
		public string groupName;

		// Token: 0x04002A03 RID: 10755
		[Tooltip("The hitbox objects in this group.")]
		public HitBox[] hitBoxes;
	}
}
