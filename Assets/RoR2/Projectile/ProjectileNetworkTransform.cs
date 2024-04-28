using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BAC RID: 2988
	public class ProjectileNetworkTransform : NetworkBehaviour
	{
		// Token: 0x060043E7 RID: 17383 RVA: 0x0011A746 File Offset: 0x00118946
		public void SetValuesFromTransform()
		{
			this.NetworkserverPosition = this.transform.position;
			this.NetworkserverRotation = this.transform.rotation;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x0011A76A File Offset: 0x0011896A
		private bool isPrediction
		{
			get
			{
				return this.projectileController && this.projectileController.isPrediction;
			}
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x0011A788 File Offset: 0x00118988
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.interpolatedTransform = base.GetComponent<InterpolatedTransform>();
			this.transform = base.transform;
			this.NetworkserverPosition = this.transform.position;
			this.NetworkserverRotation = this.transform.rotation;
			this.rb = base.GetComponent<Rigidbody>();
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x0011A7E8 File Offset: 0x001189E8
		private void Start()
		{
			this.interpolatedPosition.interpDelay = this.GetNetworkSendInterval() * this.interpolationFactor;
			this.interpolatedPosition.SetValueImmediate(this.serverPosition);
			this.interpolatedRotation.SetValueImmediate(this.serverRotation);
			if (this.isPrediction)
			{
				base.enabled = false;
			}
			if (this.rb && !this.isPrediction && !NetworkServer.active)
			{
				this.rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
				this.rb.detectCollisions = this.allowClientsideCollision;
				this.rb.isKinematic = true;
			}
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x0011A883 File Offset: 0x00118A83
		private void OnSyncPosition(Vector3 newPosition)
		{
			this.interpolatedPosition.PushValue(newPosition);
			this.NetworkserverPosition = newPosition;
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x0011A898 File Offset: 0x00118A98
		private void OnSyncRotation(Quaternion newRotation)
		{
			this.interpolatedRotation.PushValue(newRotation);
			this.NetworkserverRotation = newRotation;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x0011A8AD File Offset: 0x00118AAD
		public override float GetNetworkSendInterval()
		{
			return this.positionTransmitInterval;
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x0011A8B8 File Offset: 0x00118AB8
		private void FixedUpdate()
		{
			if (base.isServer)
			{
				this.interpolatedPosition.interpDelay = this.GetNetworkSendInterval() * this.interpolationFactor;
				this.NetworkserverPosition = this.transform.position;
				this.NetworkserverRotation = this.transform.rotation;
				this.interpolatedPosition.SetValueImmediate(this.serverPosition);
				this.interpolatedRotation.SetValueImmediate(this.serverRotation);
				return;
			}
			Vector3 currentValue = this.interpolatedPosition.GetCurrentValue(false);
			Quaternion currentValue2 = this.interpolatedRotation.GetCurrentValue(false);
			this.ApplyPositionAndRotation(currentValue, currentValue2);
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x0011A94C File Offset: 0x00118B4C
		private void ApplyPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			if (this.rb && !this.interpolatedTransform)
			{
				this.rb.MovePosition(position);
				this.rb.MoveRotation(rotation);
				return;
			}
			this.transform.position = position;
			this.transform.rotation = rotation;
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x0011A9C4 File Offset: 0x00118BC4
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x0011A9D7 File Offset: 0x00118BD7
		public Vector3 NetworkserverPosition
		{
			get
			{
				return this.serverPosition;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncPosition(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<Vector3>(value, ref this.serverPosition, 1U);
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x0011AA18 File Offset: 0x00118C18
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x0011AA2B File Offset: 0x00118C2B
		public Quaternion NetworkserverRotation
		{
			get
			{
				return this.serverRotation;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncRotation(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<Quaternion>(value, ref this.serverRotation, 2U);
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x0011AA6C File Offset: 0x00118C6C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.serverPosition);
				writer.Write(this.serverRotation);
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
				writer.Write(this.serverPosition);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.serverRotation);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x0011AB18 File Offset: 0x00118D18
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.serverPosition = reader.ReadVector3();
				this.serverRotation = reader.ReadQuaternion();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncPosition(reader.ReadVector3());
			}
			if ((num & 2) != 0)
			{
				this.OnSyncRotation(reader.ReadQuaternion());
			}
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04004257 RID: 16983
		private ProjectileController projectileController;

		// Token: 0x04004258 RID: 16984
		private new Transform transform;

		// Token: 0x04004259 RID: 16985
		private Rigidbody rb;

		// Token: 0x0400425A RID: 16986
		private InterpolatedTransform interpolatedTransform;

		// Token: 0x0400425B RID: 16987
		[Tooltip("The delay in seconds between position network updates.")]
		public float positionTransmitInterval = 0.033333335f;

		// Token: 0x0400425C RID: 16988
		[Tooltip("The number of packets of buffers to have.")]
		public float interpolationFactor = 1f;

		// Token: 0x0400425D RID: 16989
		public bool allowClientsideCollision;

		// Token: 0x0400425E RID: 16990
		[SyncVar(hook = "OnSyncPosition")]
		private Vector3 serverPosition;

		// Token: 0x0400425F RID: 16991
		[SyncVar(hook = "OnSyncRotation")]
		private Quaternion serverRotation;

		// Token: 0x04004260 RID: 16992
		private NetworkLerpedVector3 interpolatedPosition;

		// Token: 0x04004261 RID: 16993
		private NetworkLerpedQuaternion interpolatedRotation;
	}
}
