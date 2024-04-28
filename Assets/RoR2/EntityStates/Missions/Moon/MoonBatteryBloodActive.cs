using System;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.Moon
{
	// Token: 0x02000248 RID: 584
	public class MoonBatteryBloodActive : MoonBatteryActive
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x0002AD8C File Offset: 0x00028F8C
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				Transform transform = base.FindModelChild(this.siphonRootName);
				if (!transform)
				{
					transform = base.transform;
				}
				this.siphonObject = UnityEngine.Object.Instantiate<GameObject>(this.siphonPrefab, transform.position, transform.rotation, transform);
				NetworkServer.Spawn(this.siphonObject);
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002ADEB File Offset: 0x00028FEB
		public override void OnExit()
		{
			if (NetworkServer.active && this.siphonObject)
			{
				NetworkServer.Destroy(this.siphonObject);
			}
			base.OnExit();
		}

		// Token: 0x04000C10 RID: 3088
		[SerializeField]
		public GameObject siphonPrefab;

		// Token: 0x04000C11 RID: 3089
		[SerializeField]
		public string siphonRootName;

		// Token: 0x04000C12 RID: 3090
		private GameObject siphonObject;
	}
}
