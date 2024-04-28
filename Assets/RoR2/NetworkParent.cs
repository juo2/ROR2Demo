using System;
using System.Runtime.InteropServices;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007CC RID: 1996
	public class NetworkParent : NetworkBehaviour
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x000B6B93 File Offset: 0x000B4D93
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000B6BA1 File Offset: 0x000B4DA1
		public override void OnStartServer()
		{
			this.ServerUpdateParent();
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000B6BAC File Offset: 0x000B4DAC
		private void OnTransformParentChanged()
		{
			if (NetworkServer.active)
			{
				this.ServerUpdateParent();
			}
			if (this.transform.parent)
			{
				this.transform.localPosition = Vector3.zero;
				this.transform.localRotation = Quaternion.identity;
				this.transform.localScale = Vector3.one;
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000B6C08 File Offset: 0x000B4E08
		[Server]
		private void ServerUpdateParent()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkParent::ServerUpdateParent()' called on client");
				return;
			}
			Transform transform = this.transform.parent;
			if (transform == this.cachedServerParentTransform)
			{
				return;
			}
			if (!transform)
			{
				transform = null;
			}
			this.cachedServerParentTransform = transform;
			this.SetParentIdentifier(new NetworkParent.ParentIdentifier(transform));
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000B6C5D File Offset: 0x000B4E5D
		public override void OnStartClient()
		{
			base.OnStartClient();
			this.SetParentIdentifier(this.parentIdentifier);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000B6C71 File Offset: 0x000B4E71
		private void SetParentIdentifier(NetworkParent.ParentIdentifier newParentIdentifier)
		{
			this.NetworkparentIdentifier = newParentIdentifier;
			if (!NetworkServer.active)
			{
				this.transform.parent = this.parentIdentifier.Resolve();
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x000B6C98 File Offset: 0x000B4E98
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x000B6CAB File Offset: 0x000B4EAB
		public NetworkParent.ParentIdentifier NetworkparentIdentifier
		{
			get
			{
				return this.parentIdentifier;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetParentIdentifier(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkParent.ParentIdentifier>(value, ref this.parentIdentifier, 1U);
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000B6CEC File Offset: 0x000B4EEC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteParentIdentifier_NetworkParent(writer, this.parentIdentifier);
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
				GeneratedNetworkCode._WriteParentIdentifier_NetworkParent(writer, this.parentIdentifier);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000B6D58 File Offset: 0x000B4F58
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.parentIdentifier = GeneratedNetworkCode._ReadParentIdentifier_NetworkParent(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetParentIdentifier(GeneratedNetworkCode._ReadParentIdentifier_NetworkParent(reader));
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D91 RID: 11665
		private Transform cachedServerParentTransform;

		// Token: 0x04002D92 RID: 11666
		private new Transform transform;

		// Token: 0x04002D93 RID: 11667
		[SyncVar(hook = "SetParentIdentifier")]
		private NetworkParent.ParentIdentifier parentIdentifier;

		// Token: 0x020007CD RID: 1997
		[Serializable]
		private struct ParentIdentifier : IEquatable<NetworkParent.ParentIdentifier>
		{
			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000B6D99 File Offset: 0x000B4F99
			// (set) Token: 0x06002A5F RID: 10847 RVA: 0x000B6DA3 File Offset: 0x000B4FA3
			public int indexInParentChildLocator
			{
				get
				{
					return (int)(this.indexInParentChildLocatorPlusOne - 1);
				}
				set
				{
					this.indexInParentChildLocatorPlusOne = (byte)(value + 1);
				}
			}

			// Token: 0x06002A60 RID: 10848 RVA: 0x000B6DB0 File Offset: 0x000B4FB0
			private static ChildLocator LookUpChildLocator(Transform rootObject)
			{
				ModelLocator component = rootObject.GetComponent<ModelLocator>();
				if (!component)
				{
					return null;
				}
				Transform modelTransform = component.modelTransform;
				if (!modelTransform)
				{
					return null;
				}
				return modelTransform.GetComponent<ChildLocator>();
			}

			// Token: 0x06002A61 RID: 10849 RVA: 0x000B6DE8 File Offset: 0x000B4FE8
			public ParentIdentifier(Transform parent)
			{
				this.parentNetworkInstanceId = NetworkInstanceId.Invalid;
				this.indexInParentChildLocatorPlusOne = 0;
				if (!parent)
				{
					return;
				}
				NetworkIdentity componentInParent = parent.GetComponentInParent<NetworkIdentity>();
				if (!componentInParent)
				{
					Debug.LogWarningFormat("NetworkParent cannot accept a non-null parent without a NetworkIdentity! parent={0}", new object[]
					{
						parent
					});
					return;
				}
				this.parentNetworkInstanceId = componentInParent.netId;
				if (componentInParent.gameObject == parent.gameObject)
				{
					return;
				}
				ChildLocator childLocator = NetworkParent.ParentIdentifier.LookUpChildLocator(componentInParent.transform);
				if (!childLocator)
				{
					Debug.LogWarningFormat("NetworkParent can only be parented directly to another object with a NetworkIdentity or an object registered in the ChildLocator of a a model of an object with a NetworkIdentity. parent={0}", new object[]
					{
						parent
					});
					return;
				}
				this.indexInParentChildLocator = childLocator.FindChildIndex(parent);
				if (this.indexInParentChildLocatorPlusOne == 0)
				{
					Debug.LogWarningFormat("NetworkParent parent={0} is not registered in a ChildLocator.", new object[]
					{
						parent
					});
					return;
				}
			}

			// Token: 0x06002A62 RID: 10850 RVA: 0x000B6EA2 File Offset: 0x000B50A2
			public bool Equals(NetworkParent.ParentIdentifier other)
			{
				return this.indexInParentChildLocatorPlusOne == other.indexInParentChildLocatorPlusOne && this.parentNetworkInstanceId.Equals(other.parentNetworkInstanceId);
			}

			// Token: 0x06002A63 RID: 10851 RVA: 0x000B6EC8 File Offset: 0x000B50C8
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is NetworkParent.ParentIdentifier)
				{
					NetworkParent.ParentIdentifier other = (NetworkParent.ParentIdentifier)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06002A64 RID: 10852 RVA: 0x000B6EF4 File Offset: 0x000B50F4
			public override int GetHashCode()
			{
				return this.indexInParentChildLocatorPlusOne.GetHashCode() * 397 ^ this.parentNetworkInstanceId.GetHashCode();
			}

			// Token: 0x06002A65 RID: 10853 RVA: 0x000B6F1C File Offset: 0x000B511C
			public Transform Resolve()
			{
				GameObject gameObject = Util.FindNetworkObject(this.parentNetworkInstanceId);
				NetworkIdentity networkIdentity = gameObject ? gameObject.GetComponent<NetworkIdentity>() : null;
				if (!networkIdentity)
				{
					return null;
				}
				if (this.indexInParentChildLocatorPlusOne == 0)
				{
					return networkIdentity.transform;
				}
				ChildLocator childLocator = NetworkParent.ParentIdentifier.LookUpChildLocator(networkIdentity.transform);
				if (childLocator)
				{
					return childLocator.FindChild(this.indexInParentChildLocator);
				}
				return null;
			}

			// Token: 0x04002D94 RID: 11668
			public byte indexInParentChildLocatorPlusOne;

			// Token: 0x04002D95 RID: 11669
			public NetworkInstanceId parentNetworkInstanceId;
		}
	}
}
