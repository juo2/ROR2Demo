using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005EC RID: 1516
	public abstract class BaseZoneBehavior : NetworkBehaviour, IZone
	{
		// Token: 0x06001B92 RID: 7058
		public abstract bool IsInBounds(Vector3 position);

		// Token: 0x06001B94 RID: 7060 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00075984 File Offset: 0x00073B84
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}
	}
}
