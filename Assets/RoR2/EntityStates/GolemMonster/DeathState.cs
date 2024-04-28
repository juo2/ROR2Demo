using System;
using UnityEngine;

namespace EntityStates.GolemMonster
{
	// Token: 0x0200036B RID: 875
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x00045AC0 File Offset: 0x00043CC0
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("Head");
					if (transform && DeathState.initialDeathExplosionPrefab)
					{
						UnityEngine.Object.Instantiate<GameObject>(DeathState.initialDeathExplosionPrefab, transform.position, Quaternion.identity).transform.parent = transform;
					}
				}
			}
		}

		// Token: 0x04001420 RID: 5152
		public static GameObject initialDeathExplosionPrefab;
	}
}
