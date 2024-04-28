using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000912 RID: 2322
	public class ZiplineController : NetworkBehaviour
	{
		// Token: 0x0600347F RID: 13439 RVA: 0x000DDC45 File Offset: 0x000DBE45
		public void SetPointAPosition(Vector3 position)
		{
			this.NetworkpointAPosition = position;
			this.pointATransform.position = this.pointAPosition;
			this.pointATransform.LookAt(this.pointBTransform);
			this.pointBTransform.LookAt(this.pointATransform);
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000DDC81 File Offset: 0x000DBE81
		public void SetPointBPosition(Vector3 position)
		{
			this.NetworkpointBPosition = position;
			this.pointBTransform.position = this.pointBPosition;
			this.pointATransform.LookAt(this.pointBTransform);
			this.pointBTransform.LookAt(this.pointATransform);
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000DDCC0 File Offset: 0x000DBEC0
		private void RebuildZiplineVehicle(ref ZiplineVehicle ziplineVehicle, Vector3 startPos, Vector3 endPos)
		{
			if (ziplineVehicle && ziplineVehicle.vehicleSeat.hasPassenger)
			{
				ziplineVehicle = null;
			}
			if (!ziplineVehicle)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ziplineVehiclePrefab, startPos, Quaternion.identity);
				ziplineVehicle = gameObject.GetComponent<ZiplineVehicle>();
				ziplineVehicle.NetworkendPoint = endPos;
				NetworkServer.Spawn(gameObject);
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000DDD18 File Offset: 0x000DBF18
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.RebuildZiplineVehicle(ref this.currentZiplineA, this.pointAPosition, this.pointBPosition);
				this.RebuildZiplineVehicle(ref this.currentZiplineB, this.pointBPosition, this.pointAPosition);
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000DDD54 File Offset: 0x000DBF54
		private void OnDestroy()
		{
			if (NetworkServer.active)
			{
				if (this.currentZiplineA)
				{
					UnityEngine.Object.Destroy(this.currentZiplineA.gameObject);
				}
				if (this.currentZiplineB)
				{
					UnityEngine.Object.Destroy(this.currentZiplineB.gameObject);
				}
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000DDDA4 File Offset: 0x000DBFA4
		// (set) Token: 0x06003487 RID: 13447 RVA: 0x000DDDB7 File Offset: 0x000DBFB7
		public Vector3 NetworkpointAPosition
		{
			get
			{
				return this.pointAPosition;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetPointAPosition(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<Vector3>(value, ref this.pointAPosition, 1U);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000DDDF8 File Offset: 0x000DBFF8
		// (set) Token: 0x06003489 RID: 13449 RVA: 0x000DDE0B File Offset: 0x000DC00B
		public Vector3 NetworkpointBPosition
		{
			get
			{
				return this.pointBPosition;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetPointBPosition(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<Vector3>(value, ref this.pointBPosition, 2U);
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000DDE4C File Offset: 0x000DC04C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.pointAPosition);
				writer.Write(this.pointBPosition);
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
				writer.Write(this.pointAPosition);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.pointBPosition);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000DDEF8 File Offset: 0x000DC0F8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.pointAPosition = reader.ReadVector3();
				this.pointBPosition = reader.ReadVector3();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetPointAPosition(reader.ReadVector3());
			}
			if ((num & 2) != 0)
			{
				this.SetPointBPosition(reader.ReadVector3());
			}
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400358B RID: 13707
		[SyncVar(hook = "SetPointAPosition")]
		private Vector3 pointAPosition;

		// Token: 0x0400358C RID: 13708
		[SyncVar(hook = "SetPointBPosition")]
		private Vector3 pointBPosition;

		// Token: 0x0400358D RID: 13709
		[SerializeField]
		private Transform pointATransform;

		// Token: 0x0400358E RID: 13710
		[SerializeField]
		private Transform pointBTransform;

		// Token: 0x0400358F RID: 13711
		public GameObject ziplineVehiclePrefab;

		// Token: 0x04003590 RID: 13712
		private ZiplineVehicle currentZiplineA;

		// Token: 0x04003591 RID: 13713
		private ZiplineVehicle currentZiplineB;
	}
}
