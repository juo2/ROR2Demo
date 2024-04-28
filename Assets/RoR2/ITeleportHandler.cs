using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoR2
{
	// Token: 0x02000763 RID: 1891
	public interface ITeleportHandler : IEventSystemHandler
	{
		// Token: 0x06002712 RID: 10002
		void OnTeleport(Vector3 oldPosition, Vector3 newPosition);
	}
}
