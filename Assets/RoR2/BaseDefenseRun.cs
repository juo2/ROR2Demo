using System;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005EB RID: 1515
	public class BaseDefenseRun : Run
	{
		// Token: 0x06001B8C RID: 7052 RVA: 0x00075945 File Offset: 0x00073B45
		protected new void Awake()
		{
			base.Awake();
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00075958 File Offset: 0x00073B58
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			bool flag2;
			return flag2 || flag;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00075971 File Offset: 0x00073B71
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0007597B File Offset: 0x00073B7B
		public override void PreStartClient()
		{
			base.PreStartClient();
		}
	}
}
