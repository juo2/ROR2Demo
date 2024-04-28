using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RoR2.UI;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000808 RID: 2056
	public class PingerController : NetworkBehaviour
	{
		// Token: 0x06002C6B RID: 11371 RVA: 0x000BDB90 File Offset: 0x000BBD90
		private void RebuildPing(PingerController.PingInfo pingInfo)
		{
			if (!pingInfo.active && this.pingIndicator != null)
			{
				if (this.pingIndicator)
				{
					UnityEngine.Object.Destroy(this.pingIndicator.gameObject);
				}
				this.pingIndicator = null;
				return;
			}
			if (!this.pingIndicator)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/PingIndicator"));
				this.pingIndicator = gameObject.GetComponent<PingIndicator>();
				this.pingIndicator.pingOwner = base.gameObject;
			}
			this.pingIndicator.pingOrigin = pingInfo.origin;
			this.pingIndicator.pingNormal = pingInfo.normal;
			this.pingIndicator.pingTarget = pingInfo.targetGameObject;
			this.pingIndicator.RebuildPing();
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000BDC4B File Offset: 0x000BBE4B
		private void OnDestroy()
		{
			if (this.pingIndicator)
			{
				UnityEngine.Object.Destroy(this.pingIndicator.gameObject);
			}
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000BDC6A File Offset: 0x000BBE6A
		private void OnSyncCurrentPing(PingerController.PingInfo newPingInfo)
		{
			if (base.hasAuthority)
			{
				return;
			}
			this.SetCurrentPing(newPingInfo);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000BDC7C File Offset: 0x000BBE7C
		private void SetCurrentPing(PingerController.PingInfo newPingInfo)
		{
			this.NetworkcurrentPing = newPingInfo;
			this.RebuildPing(this.currentPing);
			if (base.hasAuthority)
			{
				this.CallCmdPing(this.currentPing);
			}
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000BDCA5 File Offset: 0x000BBEA5
		[Command]
		private void CmdPing(PingerController.PingInfo incomingPing)
		{
			this.NetworkcurrentPing = incomingPing;
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000BDCB0 File Offset: 0x000BBEB0
		private void FixedUpdate()
		{
			if (base.hasAuthority)
			{
				this.pingRechargeStopwatch -= Time.fixedDeltaTime;
				if (this.pingRechargeStopwatch <= 0f)
				{
					this.pingStock = Mathf.Min(this.pingStock + 1, 3);
					this.pingRechargeStopwatch = 1.5f;
				}
			}
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000BDD04 File Offset: 0x000BBF04
		private static bool GeneratePingInfo(Ray aimRay, GameObject bodyObject, out PingerController.PingInfo result)
		{
			result = new PingerController.PingInfo
			{
				active = true,
				origin = Vector3.zero,
				normal = Vector3.zero,
				targetNetworkIdentity = null
			};
			float num;
			aimRay = CameraRigController.ModifyAimRayIfApplicable(aimRay, bodyObject, out num);
			float maxDistance = 1000f + num;
			RaycastHit raycastHit;
			if (Util.CharacterRaycast(bodyObject, aimRay, out raycastHit, maxDistance, LayerIndex.entityPrecise.mask | LayerIndex.world.mask, QueryTriggerInteraction.UseGlobal))
			{
				HurtBox component = raycastHit.collider.GetComponent<HurtBox>();
				if (component && component.healthComponent)
				{
					CharacterBody body = component.healthComponent.body;
					result.origin = body.corePosition;
					result.normal = Vector3.zero;
					result.targetNetworkIdentity = body.networkIdentity;
					return true;
				}
			}
			if (Util.CharacterRaycast(bodyObject, aimRay, out raycastHit, maxDistance, LayerIndex.world.mask | LayerIndex.defaultLayer.mask | LayerIndex.pickups.mask, QueryTriggerInteraction.Collide))
			{
				GameObject gameObject = raycastHit.collider.gameObject;
				NetworkIdentity networkIdentity = gameObject.GetComponentInParent<NetworkIdentity>();
				if (!networkIdentity)
				{
					Transform parent = gameObject.transform.parent;
					EntityLocator entityLocator = parent ? parent.GetComponentInChildren<EntityLocator>() : gameObject.GetComponent<EntityLocator>();
					if (entityLocator)
					{
						gameObject = entityLocator.entity;
						networkIdentity = gameObject.GetComponent<NetworkIdentity>();
					}
				}
				result.origin = raycastHit.point;
				result.normal = raycastHit.normal;
				result.targetNetworkIdentity = networkIdentity;
				return true;
			}
			return false;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000BDEC8 File Offset: 0x000BC0C8
		public void AttemptPing(Ray aimRay, GameObject bodyObject)
		{
			if (this.pingStock <= 0)
			{
				Chat.AddMessage(Language.GetString("PLAYER_PING_COOLDOWN"));
				return;
			}
			if (!RoR2Application.isInSinglePlayer)
			{
				this.pingStock--;
			}
			PingerController.PingInfo pingInfo;
			if (!PingerController.GeneratePingInfo(aimRay, bodyObject, out pingInfo))
			{
				return;
			}
			if (pingInfo.targetNetworkIdentity != null && pingInfo.targetNetworkIdentity == this.currentPing.targetNetworkIdentity)
			{
				pingInfo = PingerController.emptyPing;
				this.pingStock++;
			}
			this.SetCurrentPing(pingInfo);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000BDF54 File Offset: 0x000BC154
		static PingerController()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(PingerController), PingerController.kCmdCmdPing, new NetworkBehaviour.CmdDelegate(PingerController.InvokeCmdCmdPing));
			NetworkCRC.RegisterBehaviour("PingerController", 0);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000BDF90 File Offset: 0x000BC190
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x000BDFA3 File Offset: 0x000BC1A3
		public PingerController.PingInfo NetworkcurrentPing
		{
			get
			{
				return this.currentPing;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncCurrentPing(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<PingerController.PingInfo>(value, ref this.currentPing, 1U);
			}
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000BDFE2 File Offset: 0x000BC1E2
		protected static void InvokeCmdCmdPing(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdPing called on client.");
				return;
			}
			((PingerController)obj).CmdPing(GeneratedNetworkCode._ReadPingInfo_PingerController(reader));
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000BE00C File Offset: 0x000BC20C
		public void CallCmdPing(PingerController.PingInfo incomingPing)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdPing called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdPing(incomingPing);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)PingerController.kCmdCmdPing);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WritePingInfo_PingerController(networkWriter, incomingPing);
			base.SendCommandInternal(networkWriter, 0, "CmdPing");
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000BE098 File Offset: 0x000BC298
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WritePingInfo_PingerController(writer, this.currentPing);
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
				GeneratedNetworkCode._WritePingInfo_PingerController(writer, this.currentPing);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000BE104 File Offset: 0x000BC304
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.currentPing = GeneratedNetworkCode._ReadPingInfo_PingerController(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncCurrentPing(GeneratedNetworkCode._ReadPingInfo_PingerController(reader));
			}
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002EBC RID: 11964
		private int pingStock = 3;

		// Token: 0x04002EBD RID: 11965
		private float pingRechargeStopwatch;

		// Token: 0x04002EBE RID: 11966
		private const int maximumPingStock = 3;

		// Token: 0x04002EBF RID: 11967
		private const float pingRechargeInterval = 1.5f;

		// Token: 0x04002EC0 RID: 11968
		private static readonly PingerController.PingInfo emptyPing;

		// Token: 0x04002EC1 RID: 11969
		private PingIndicator pingIndicator;

		// Token: 0x04002EC2 RID: 11970
		[SyncVar(hook = "OnSyncCurrentPing")]
		public PingerController.PingInfo currentPing;

		// Token: 0x04002EC3 RID: 11971
		private static int kCmdCmdPing = 1170265357;

		// Token: 0x02000809 RID: 2057
		[Serializable]
		public struct PingInfo : IEquatable<PingerController.PingInfo>
		{
			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000BE145 File Offset: 0x000BC345
			public GameObject targetGameObject
			{
				get
				{
					if (!this.targetNetworkIdentity)
					{
						return null;
					}
					return this.targetNetworkIdentity.gameObject;
				}
			}

			// Token: 0x06002C7E RID: 11390 RVA: 0x000BE164 File Offset: 0x000BC364
			public bool Equals(PingerController.PingInfo other)
			{
				return this.active.Equals(other.active) && this.origin.Equals(other.origin) && this.normal.Equals(other.normal) && this.targetNetworkIdentity == other.targetNetworkIdentity;
			}

			// Token: 0x06002C7F RID: 11391 RVA: 0x000BE1BC File Offset: 0x000BC3BC
			public override bool Equals(object obj)
			{
				PingerController.PingInfo? pingInfo;
				return obj is PingerController.PingInfo? && pingInfo.GetValueOrDefault().Equals(this);
			}

			// Token: 0x06002C80 RID: 11392 RVA: 0x000BE1F8 File Offset: 0x000BC3F8
			public override int GetHashCode()
			{
				return (((-1814869148 * -1521134295 + this.active.GetHashCode()) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.origin)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.normal)) * -1521134295 + EqualityComparer<NetworkIdentity>.Default.GetHashCode(this.targetNetworkIdentity);
			}

			// Token: 0x04002EC4 RID: 11972
			public bool active;

			// Token: 0x04002EC5 RID: 11973
			public Vector3 origin;

			// Token: 0x04002EC6 RID: 11974
			public Vector3 normal;

			// Token: 0x04002EC7 RID: 11975
			public NetworkIdentity targetNetworkIdentity;
		}
	}
}
