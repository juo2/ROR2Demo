using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000695 RID: 1685
	public class Deployable : MonoBehaviour
	{
		// Token: 0x060020E7 RID: 8423 RVA: 0x0008D967 File Offset: 0x0008BB67
		private void OnDestroy()
		{
			if (NetworkServer.active && this.ownerMaster)
			{
				this.ownerMaster.RemoveDeployable(this);
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0008D989 File Offset: 0x0008BB89
		public void DestroyGameObject()
		{
			if (NetworkServer.active)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04002656 RID: 9814
		[NonSerialized]
		public CharacterMaster ownerMaster;

		// Token: 0x04002657 RID: 9815
		public UnityEvent onUndeploy;
	}
}
