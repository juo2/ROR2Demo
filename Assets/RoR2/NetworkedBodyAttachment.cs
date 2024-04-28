using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HG;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007DA RID: 2010
	public sealed class NetworkedBodyAttachment : NetworkBehaviour
	{
		// Token: 0x06002B65 RID: 11109 RVA: 0x000BA3B4 File Offset: 0x000B85B4
		private void OnSyncAttachedBodyObject(GameObject value)
		{
			if (NetworkServer.active)
			{
				return;
			}
			this.Network_attachedBodyObject = value;
			this.OnAttachedBodyObjectAssigned();
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000BA3CB File Offset: 0x000B85CB
		private void OnSyncAttachedBodyChildName(string newName)
		{
			this.NetworkattachedBodyChildName = newName;
			if (this.shouldParentToAttachedBody)
			{
				this.ParentToBody();
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000BA3E2 File Offset: 0x000B85E2
		public GameObject attachedBodyObject
		{
			get
			{
				return this._attachedBodyObject;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000BA3EA File Offset: 0x000B85EA
		// (set) Token: 0x06002B69 RID: 11113 RVA: 0x000BA3F2 File Offset: 0x000B85F2
		public CharacterBody attachedBody { get; private set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000BA3FB File Offset: 0x000B85FB
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000BA403 File Offset: 0x000B8603
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x06002B6C RID: 11116 RVA: 0x000BA40C File Offset: 0x000B860C
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.attachmentBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000BA428 File Offset: 0x000B8628
		[Server]
		public void AttachToGameObjectAndSpawn([NotNull] GameObject newAttachedBodyObject, string attachedBodyChildName = null)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkedBodyAttachment::AttachToGameObjectAndSpawn(UnityEngine.GameObject,System.String)' called on client");
				return;
			}
			if (this.attached)
			{
				Debug.LogErrorFormat("Can't attach object '{0}' to object '{1}', it's already been assigned to object '{2}'.", new object[]
				{
					base.gameObject,
					newAttachedBodyObject,
					this.attachedBodyObject
				});
				return;
			}
			if (!newAttachedBodyObject)
			{
				return;
			}
			NetworkIdentity component = newAttachedBodyObject.GetComponent<NetworkIdentity>();
			if (component.netId.Value == 0U)
			{
				Debug.LogWarningFormat("Network Identity for object {0} has a zero netID. Attachment will fail over the network.", new object[]
				{
					newAttachedBodyObject
				});
			}
			this.NetworkattachedBodyChildName = attachedBodyChildName;
			this.Network_attachedBodyObject = newAttachedBodyObject;
			this.OnAttachedBodyObjectAssigned();
			NetworkConnection clientAuthorityOwner = component.clientAuthorityOwner;
			if (clientAuthorityOwner == null || this.forceHostAuthority)
			{
				NetworkServer.Spawn(base.gameObject);
				return;
			}
			NetworkServer.SpawnWithClientAuthority(base.gameObject, clientAuthorityOwner);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000BA4F0 File Offset: 0x000B86F0
		private void OnAttachedBodyObjectAssigned()
		{
			if (this.attached)
			{
				return;
			}
			this.attached = true;
			if (this._attachedBodyObject)
			{
				this.attachedBody = this._attachedBodyObject.GetComponent<CharacterBody>();
				if (this.shouldParentToAttachedBody)
				{
					this.ParentToBody();
				}
			}
			if (this.attachedBody)
			{
				List<INetworkedBodyAttachmentListener> list = CollectionPool<INetworkedBodyAttachmentListener, List<INetworkedBodyAttachmentListener>>.RentCollection();
				base.GetComponents<INetworkedBodyAttachmentListener>(list);
				foreach (INetworkedBodyAttachmentListener networkedBodyAttachmentListener in list)
				{
					networkedBodyAttachmentListener.OnAttachedBodyDiscovered(this, this.attachedBody);
				}
				list = CollectionPool<INetworkedBodyAttachmentListener, List<INetworkedBodyAttachmentListener>>.ReturnCollection(list);
			}
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000BA5A0 File Offset: 0x000B87A0
		private void ParentToBody()
		{
			if (this._attachedBodyObject)
			{
				Transform parent = this._attachedBodyObject.transform;
				if (!string.IsNullOrEmpty(this.attachedBodyChildName))
				{
					ModelLocator component = this._attachedBodyObject.GetComponent<ModelLocator>();
					if (component && component.modelTransform)
					{
						ChildLocator component2 = component.modelTransform.GetComponent<ChildLocator>();
						if (component2)
						{
							Transform transform = component2.FindChild(this.attachedBodyChildName);
							if (transform)
							{
								parent = transform;
							}
						}
					}
				}
				base.transform.SetParent(parent, false);
				base.transform.localPosition = Vector3.zero;
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000BA63F File Offset: 0x000B883F
		public override void OnStartClient()
		{
			base.OnStartClient();
			this.OnSyncAttachedBodyObject(this.attachedBodyObject);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000BA654 File Offset: 0x000B8854
		private void FixedUpdate()
		{
			if (!this.attachedBodyObject && NetworkServer.active)
			{
				if (this.attachmentBody && this.attachmentBody.healthComponent)
				{
					this.attachmentBody.healthComponent.Suicide(null, null, DamageType.Generic);
					return;
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000BA6B3 File Offset: 0x000B88B3
		private void OnValidate()
		{
			if (!base.GetComponent<NetworkIdentity>().localPlayerAuthority && !this.forceHostAuthority)
			{
				Debug.LogWarningFormat("NetworkedBodyAttachment: Object {0} NetworkIdentity needs localPlayerAuthority=true", new object[]
				{
					base.gameObject.name
				});
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000BA6E8 File Offset: 0x000B88E8
		private void OnEnable()
		{
			InstanceTracker.Add<NetworkedBodyAttachment>(this);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000BA6F0 File Offset: 0x000B88F0
		private void OnDisable()
		{
			InstanceTracker.Remove<NetworkedBodyAttachment>(this);
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000BA6F8 File Offset: 0x000B88F8
		public static void FindBodyAttachments(CharacterBody body, List<NetworkedBodyAttachment> output)
		{
			foreach (NetworkedBodyAttachment networkedBodyAttachment in InstanceTracker.GetInstancesList<NetworkedBodyAttachment>())
			{
				if (networkedBodyAttachment.attachedBody == body)
				{
					output.Add(networkedBodyAttachment);
				}
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000BA754 File Offset: 0x000B8954
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.networkIdentity);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000BA76D File Offset: 0x000B896D
		public override void OnStopAuthority()
		{
			base.OnStopAuthority();
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.networkIdentity);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000BA798 File Offset: 0x000B8998
		// (set) Token: 0x06002B7B RID: 11131 RVA: 0x000BA7AC File Offset: 0x000B89AC
		public GameObject Network_attachedBodyObject
		{
			get
			{
				return this._attachedBodyObject;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncAttachedBodyObject(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVarGameObject(value, ref this._attachedBodyObject, 1U, ref this.____attachedBodyObjectNetId);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000BA7FC File Offset: 0x000B89FC
		// (set) Token: 0x06002B7D RID: 11133 RVA: 0x000BA80F File Offset: 0x000B8A0F
		public string NetworkattachedBodyChildName
		{
			get
			{
				return this.attachedBodyChildName;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncAttachedBodyChildName(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<string>(value, ref this.attachedBodyChildName, 2U);
			}
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000BA850 File Offset: 0x000B8A50
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._attachedBodyObject);
				writer.Write(this.attachedBodyChildName);
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
				writer.Write(this._attachedBodyObject);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.attachedBodyChildName);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000BA8FC File Offset: 0x000B8AFC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.____attachedBodyObjectNetId = reader.ReadNetworkId();
				this.attachedBodyChildName = reader.ReadString();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncAttachedBodyObject(reader.ReadGameObject());
			}
			if ((num & 2) != 0)
			{
				this.OnSyncAttachedBodyChildName(reader.ReadString());
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000BA962 File Offset: 0x000B8B62
		public override void PreStartClient()
		{
			if (!this.____attachedBodyObjectNetId.IsEmpty())
			{
				this.Network_attachedBodyObject = ClientScene.FindLocalObject(this.____attachedBodyObjectNetId);
			}
		}

		// Token: 0x04002DE9 RID: 11753
		[SyncVar(hook = "OnSyncAttachedBodyObject")]
		private GameObject _attachedBodyObject;

		// Token: 0x04002DEA RID: 11754
		[SyncVar(hook = "OnSyncAttachedBodyChildName")]
		private string attachedBodyChildName;

		// Token: 0x04002DEC RID: 11756
		public bool shouldParentToAttachedBody = true;

		// Token: 0x04002DED RID: 11757
		public bool forceHostAuthority;

		// Token: 0x04002DEF RID: 11759
		private NetworkIdentity networkIdentity;

		// Token: 0x04002DF0 RID: 11760
		private CharacterBody attachmentBody;

		// Token: 0x04002DF1 RID: 11761
		private bool attached;

		// Token: 0x04002DF2 RID: 11762
		private NetworkInstanceId ____attachedBodyObjectNetId;
	}
}
