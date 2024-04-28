using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020009EE RID: 2542
	public class PortalDialerButtonController : NetworkBehaviour
	{
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000F3F08 File Offset: 0x000F2108
		public ArtifactCompoundDef currentDigitDef
		{
			get
			{
				return this.digitDefs[(int)this.currentDigitIndex];
			}
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000F3F18 File Offset: 0x000F2118
		private void OnSyncDigitIndex(byte newDigitIndex)
		{
			this.NetworkcurrentDigitIndex = newDigitIndex;
			if (this.modelInstance)
			{
				UnityEngine.Object.Destroy(this.modelInstance);
			}
			if (this.swapEffect)
			{
				this.swapEffect.SetActive(false);
				this.swapEffect.SetActive(true);
			}
			if (this.currentDigitDef.modelPrefab)
			{
				this.modelInstance = UnityEngine.Object.Instantiate<GameObject>(this.currentDigitDef.modelPrefab, this.holderObject.transform);
			}
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000F3F9C File Offset: 0x000F219C
		public override void OnStartClient()
		{
			base.OnStartClient();
			this.OnSyncDigitIndex(this.currentDigitIndex);
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000F3FB0 File Offset: 0x000F21B0
		[Server]
		public void CycleDigitServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PortalDialerButtonController::CycleDigitServer()' called on client");
				return;
			}
			byte b = this.currentDigitIndex + 1;
			if ((int)b >= this.digitDefs.Length)
			{
				b = 1;
			}
			this.NetworkcurrentDigitIndex = b;
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000F4000 File Offset: 0x000F2200
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x000F4013 File Offset: 0x000F2213
		public byte NetworkcurrentDigitIndex
		{
			get
			{
				return this.currentDigitIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncDigitIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<byte>(value, ref this.currentDigitIndex, 1U);
			}
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000F4054 File Offset: 0x000F2254
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.currentDigitIndex);
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
				writer.WritePackedUInt32((uint)this.currentDigitIndex);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000F40C0 File Offset: 0x000F22C0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.currentDigitIndex = (byte)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncDigitIndex((byte)reader.ReadPackedUInt32());
			}
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003995 RID: 14741
		[SyncVar(hook = "OnSyncDigitIndex")]
		public byte currentDigitIndex = 1;

		// Token: 0x04003996 RID: 14742
		public GameObject holderObject;

		// Token: 0x04003997 RID: 14743
		public GameObject swapEffect;

		// Token: 0x04003998 RID: 14744
		public ArtifactCompoundDef[] digitDefs;

		// Token: 0x04003999 RID: 14745
		private GameObject modelInstance;
	}
}
