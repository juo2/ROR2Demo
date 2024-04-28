using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006FE RID: 1790
	public class GenericOwnership : NetworkBehaviour
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x0009C115 File Offset: 0x0009A315
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0009C070 File Offset: 0x0009A270
		public GameObject ownerObject
		{
			get
			{
				return this.cachedOwnerObject;
			}
			[Server]
			set
			{
				if (!NetworkServer.active)
				{
					Debug.LogWarning("[Server] function 'System.Void RoR2.GenericOwnership::set_ownerObject(UnityEngine.GameObject)' called on client");
					return;
				}
				if (!value)
				{
					value = null;
				}
				if (this.cachedOwnerObject == value)
				{
					return;
				}
				this.cachedOwnerObject = value;
				GameObject gameObject = this.cachedOwnerObject;
				NetworkInstanceId? networkInstanceId;
				if (gameObject == null)
				{
					networkInstanceId = null;
				}
				else
				{
					NetworkIdentity component = gameObject.GetComponent<NetworkIdentity>();
					networkInstanceId = ((component != null) ? new NetworkInstanceId?(component.netId) : null);
				}
				this.NetworkownerInstanceId = (networkInstanceId ?? NetworkInstanceId.Invalid);
				Action<GameObject> action = this.onOwnerChanged;
				if (action == null)
				{
					return;
				}
				action(this.cachedOwnerObject);
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002493 RID: 9363 RVA: 0x0009C120 File Offset: 0x0009A320
		// (remove) Token: 0x06002494 RID: 9364 RVA: 0x0009C158 File Offset: 0x0009A358
		public event Action<GameObject> onOwnerChanged;

		// Token: 0x06002495 RID: 9365 RVA: 0x0009C18D File Offset: 0x0009A38D
		private void SetOwnerClient(NetworkInstanceId id)
		{
			if (NetworkServer.active)
			{
				return;
			}
			this.cachedOwnerObject = ClientScene.FindLocalObject(id);
			Action<GameObject> action = this.onOwnerChanged;
			if (action == null)
			{
				return;
			}
			action(this.cachedOwnerObject);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x0009C1B9 File Offset: 0x0009A3B9
		public override void OnStartClient()
		{
			base.OnStartClient();
			this.SetOwnerClient(this.ownerInstanceId);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x0009C1D0 File Offset: 0x0009A3D0
		// (set) Token: 0x0600249A RID: 9370 RVA: 0x0009C1E3 File Offset: 0x0009A3E3
		public NetworkInstanceId NetworkownerInstanceId
		{
			get
			{
				return this.ownerInstanceId;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetOwnerClient(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkInstanceId>(value, ref this.ownerInstanceId, 1U);
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0009C224 File Offset: 0x0009A424
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.ownerInstanceId);
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
				writer.Write(this.ownerInstanceId);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0009C290 File Offset: 0x0009A490
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.ownerInstanceId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetOwnerClient(reader.ReadNetworkId());
			}
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040028B3 RID: 10419
		[SyncVar(hook = "SetOwnerClient")]
		private NetworkInstanceId ownerInstanceId;

		// Token: 0x040028B4 RID: 10420
		private GameObject cachedOwnerObject;
	}
}
