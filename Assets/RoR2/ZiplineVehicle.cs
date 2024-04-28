using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000913 RID: 2323
	[RequireComponent(typeof(VehicleSeat))]
	[RequireComponent(typeof(Rigidbody))]
	public class ZiplineVehicle : NetworkBehaviour
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000DDF5E File Offset: 0x000DC15E
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x000DDF66 File Offset: 0x000DC166
		public VehicleSeat vehicleSeat { get; private set; }

		// Token: 0x0600348F RID: 13455 RVA: 0x000DDF70 File Offset: 0x000DC170
		private void Awake()
		{
			this.vehicleSeat = base.GetComponent<VehicleSeat>();
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.vehicleSeat.onPassengerEnter += this.OnPassengerEnter;
			this.vehicleSeat.onPassengerExit += this.OnPassengerExit;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000DDFC3 File Offset: 0x000DC1C3
		private void OnPassengerEnter(GameObject passenger)
		{
			this.currentPassenger = passenger;
			this.startPoint = base.transform.position;
			this.startTravelFixedTime = Run.FixedTimeStamp.now;
			this.startTravelTime = Run.TimeStamp.now;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000DDFF4 File Offset: 0x000DC1F4
		private void SetTravelDistance(float time)
		{
			Vector3 a = this.endPoint - this.startPoint;
			float magnitude = a.magnitude;
			Vector3 a2 = a / magnitude;
			float num = HGPhysics.CalculateDistance(0f, this.acceleration, time);
			bool flag = false;
			if (num > magnitude)
			{
				num = magnitude;
				flag = true;
			}
			this.rigidbody.MovePosition(this.startPoint + a2 * num);
			this.rigidbody.velocity = a2 * (this.acceleration * time);
			if (NetworkServer.active && flag)
			{
				this.vehicleSeat.EjectPassenger(this.currentPassenger);
				return;
			}
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000DE093 File Offset: 0x000DC293
		private void Update()
		{
			bool hasPassed = this.startTravelTime.hasPassed;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000DE0A4 File Offset: 0x000DC2A4
		private void FixedUpdate()
		{
			if (this.startTravelFixedTime.hasPassed)
			{
				this.SetTravelDistance(this.startTravelFixedTime.timeSince);
			}
			if (NetworkServer.active && this.currentPassenger)
			{
				Vector3 normalized = (this.endPoint - base.transform.position).normalized;
				if (Vector3.Dot(normalized, this.travelDirection) < 0f)
				{
					this.vehicleSeat.EjectPassenger(this.currentPassenger);
					return;
				}
				float fixedDeltaTime = Time.fixedDeltaTime;
				Vector3 vector = this.rigidbody.velocity;
				vector += this.travelDirection * (this.acceleration * fixedDeltaTime);
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude > this.maxSpeed * this.maxSpeed)
				{
					float num = Mathf.Sqrt(sqrMagnitude);
					vector *= this.maxSpeed / num;
				}
				this.rigidbody.velocity = vector;
				this.travelDirection = normalized;
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000DE19F File Offset: 0x000DC39F
		private void OnPassengerExit(GameObject passenger)
		{
			this.currentPassenger = null;
			this.vehicleSeat.enabled = false;
			if (NetworkServer.active)
			{
				base.gameObject.AddComponent<DestroyOnTimer>().duration = 0.1f;
			}
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000DE204 File Offset: 0x000DC404
		// (set) Token: 0x06003498 RID: 13464 RVA: 0x000DE217 File Offset: 0x000DC417
		public Vector3 NetworkendPoint
		{
			get
			{
				return this.endPoint;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Vector3>(value, ref this.endPoint, 1U);
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000DE22C File Offset: 0x000DC42C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.endPoint);
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
				writer.Write(this.endPoint);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000DE298 File Offset: 0x000DC498
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.endPoint = reader.ReadVector3();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.endPoint = reader.ReadVector3();
			}
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003592 RID: 13714
		public float maxSpeed = 30f;

		// Token: 0x04003593 RID: 13715
		public float acceleration = 2f;

		// Token: 0x04003595 RID: 13717
		private Rigidbody rigidbody;

		// Token: 0x04003596 RID: 13718
		private Vector3 startPoint;

		// Token: 0x04003597 RID: 13719
		[SyncVar]
		public Vector3 endPoint;

		// Token: 0x04003598 RID: 13720
		private Vector3 travelDirection;

		// Token: 0x04003599 RID: 13721
		private GameObject currentPassenger;

		// Token: 0x0400359A RID: 13722
		private Run.FixedTimeStamp startTravelFixedTime = Run.FixedTimeStamp.positiveInfinity;

		// Token: 0x0400359B RID: 13723
		private Run.TimeStamp startTravelTime = Run.TimeStamp.positiveInfinity;
	}
}
