using System;
using System.Runtime.InteropServices;
using Unity;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000802 RID: 2050
	public class PickupIndexNetworker : NetworkBehaviour
	{
		// Token: 0x06002C37 RID: 11319 RVA: 0x000BD1AD File Offset: 0x000BB3AD
		private void SyncPickupIndex(PickupIndex newPickupIndex)
		{
			this.NetworkpickupIndex = newPickupIndex;
			this.UpdatePickupDisplay();
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000BD1BC File Offset: 0x000BB3BC
		private void UpdatePickupDisplay()
		{
			if (this.pickupDisplay)
			{
				this.pickupDisplay.SetPickupIndex(this.pickupIndex, false);
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000BD1E0 File Offset: 0x000BB3E0
		// (set) Token: 0x06002C3C RID: 11324 RVA: 0x000BD1F3 File Offset: 0x000BB3F3
		public PickupIndex NetworkpickupIndex
		{
			get
			{
				return this.pickupIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SyncPickupIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<PickupIndex>(value, ref this.pickupIndex, 1U);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000BD234 File Offset: 0x000BB434
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
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
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000BD2A0 File Offset: 0x000BB4A0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.pickupIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SyncPickupIndex(GeneratedNetworkCode._ReadPickupIndex_None(reader));
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002EA5 RID: 11941
		[SyncVar(hook = "SyncPickupIndex")]
		public PickupIndex pickupIndex;

		// Token: 0x04002EA6 RID: 11942
		public PickupDisplay pickupDisplay;
	}
}
