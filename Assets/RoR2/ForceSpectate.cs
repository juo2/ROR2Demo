using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006E1 RID: 1761
	public class ForceSpectate : NetworkBehaviour
	{
		// Token: 0x060022CB RID: 8907 RVA: 0x00096724 File Offset: 0x00094924
		private void OnEnable()
		{
			InstanceTracker.Add<ForceSpectate>(this);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0009672C File Offset: 0x0009492C
		private void OnDisable()
		{
			InstanceTracker.Remove<ForceSpectate>(this);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x00096734 File Offset: 0x00094934
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x00096747 File Offset: 0x00094947
		public GameObject Networktarget
		{
			get
			{
				return this.target;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.target, 1U, ref this.___targetNetId);
			}
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00096764 File Offset: 0x00094964
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.target);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.target);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000967D0 File Offset: 0x000949D0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___targetNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.target = reader.ReadGameObject();
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00096811 File Offset: 0x00094A11
		public override void PreStartClient()
		{
			if (!this.___targetNetId.IsEmpty())
			{
				this.Networktarget = ClientScene.FindLocalObject(this.___targetNetId);
			}
		}

		// Token: 0x040027ED RID: 10221
		[SyncVar]
		public GameObject target;

		// Token: 0x040027EE RID: 10222
		private NetworkInstanceId ___targetNetId;
	}
}
