using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BAE RID: 2990
	[DisallowMultipleComponent]
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileOwnerOrbiter : NetworkBehaviour
	{
		// Token: 0x06004400 RID: 17408 RVA: 0x0011AE2E File Offset: 0x0011902E
		public void Initialize(Vector3 planeNormal, float radius, float degreesPerSecond, float initialDegreesFromOwnerForward)
		{
			this.NetworkplaneNormal = planeNormal;
			this.Networkradius = radius;
			this.NetworkdegreesPerSecond = degreesPerSecond;
			this.NetworkinitialDegreesFromOwnerForward = initialDegreesFromOwnerForward;
			this.ResetState();
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x0011AE54 File Offset: 0x00119054
		private void OnEnable()
		{
			this.rigidBody = base.GetComponent<Rigidbody>();
			ProjectileController component = base.GetComponent<ProjectileController>();
			if (component.owner)
			{
				this.AcquireOwner(component);
				return;
			}
			component.onInitialized += this.AcquireOwner;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0011AE9B File Offset: 0x0011909B
		public void FixedUpdate()
		{
			this.UpdatePosition(false);
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x0011AEA4 File Offset: 0x001190A4
		private void ResetState()
		{
			this.NetworkinitialRunTime = Run.instance.GetRunStopwatch();
			this.planeNormal.Normalize();
			if (this.ownerTransform)
			{
				this.NetworkinitialRadialDirection = Quaternion.AngleAxis(this.initialDegreesFromOwnerForward, this.planeNormal) * this.ownerTransform.forward;
				this.resetOnAcquireOwner = false;
			}
			this.UpdatePosition(true);
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x0011AF10 File Offset: 0x00119110
		private void UpdatePosition(bool doSnap)
		{
			if (this.ownerTransform)
			{
				float angle = (Run.instance.GetRunStopwatch() - this.initialRunTime) * this.degreesPerSecond;
				Vector3 position = this.ownerTransform.position + this.offset + Quaternion.AngleAxis(angle, this.planeNormal) * this.initialRadialDirection * this.radius;
				if (!this.rigidBody || doSnap)
				{
					base.transform.position = position;
					return;
				}
				if (this.rigidBody)
				{
					this.rigidBody.MovePosition(position);
				}
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x0011AFBC File Offset: 0x001191BC
		private void AcquireOwner(ProjectileController controller)
		{
			this.ownerTransform = controller.owner.transform;
			controller.onInitialized -= this.AcquireOwner;
			if (this.resetOnAcquireOwner)
			{
				this.resetOnAcquireOwner = false;
				this.ResetState();
			}
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x0011B010 File Offset: 0x00119210
		// (set) Token: 0x06004409 RID: 17417 RVA: 0x0011B023 File Offset: 0x00119223
		public Vector3 Networkoffset
		{
			get
			{
				return this.offset;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Vector3>(value, ref this.offset, 1U);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0011B038 File Offset: 0x00119238
		// (set) Token: 0x0600440B RID: 17419 RVA: 0x0011B04B File Offset: 0x0011924B
		public float NetworkinitialDegreesFromOwnerForward
		{
			get
			{
				return this.initialDegreesFromOwnerForward;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.initialDegreesFromOwnerForward, 2U);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0011B060 File Offset: 0x00119260
		// (set) Token: 0x0600440D RID: 17421 RVA: 0x0011B073 File Offset: 0x00119273
		public float NetworkdegreesPerSecond
		{
			get
			{
				return this.degreesPerSecond;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.degreesPerSecond, 4U);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600440E RID: 17422 RVA: 0x0011B088 File Offset: 0x00119288
		// (set) Token: 0x0600440F RID: 17423 RVA: 0x0011B09B File Offset: 0x0011929B
		public float Networkradius
		{
			get
			{
				return this.radius;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.radius, 8U);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x0011B0B0 File Offset: 0x001192B0
		// (set) Token: 0x06004411 RID: 17425 RVA: 0x0011B0C3 File Offset: 0x001192C3
		public Vector3 NetworkplaneNormal
		{
			get
			{
				return this.planeNormal;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Vector3>(value, ref this.planeNormal, 16U);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x0011B0D8 File Offset: 0x001192D8
		// (set) Token: 0x06004413 RID: 17427 RVA: 0x0011B0EB File Offset: 0x001192EB
		public Vector3 NetworkinitialRadialDirection
		{
			get
			{
				return this.initialRadialDirection;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Vector3>(value, ref this.initialRadialDirection, 32U);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x0011B100 File Offset: 0x00119300
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x0011B113 File Offset: 0x00119313
		public float NetworkinitialRunTime
		{
			get
			{
				return this.initialRunTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.initialRunTime, 64U);
			}
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x0011B128 File Offset: 0x00119328
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.offset);
				writer.Write(this.initialDegreesFromOwnerForward);
				writer.Write(this.degreesPerSecond);
				writer.Write(this.radius);
				writer.Write(this.planeNormal);
				writer.Write(this.initialRadialDirection);
				writer.Write(this.initialRunTime);
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
				writer.Write(this.offset);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.initialDegreesFromOwnerForward);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.degreesPerSecond);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.radius);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.planeNormal);
			}
			if ((base.syncVarDirtyBits & 32U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.initialRadialDirection);
			}
			if ((base.syncVarDirtyBits & 64U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.initialRunTime);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x0011B310 File Offset: 0x00119510
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.offset = reader.ReadVector3();
				this.initialDegreesFromOwnerForward = reader.ReadSingle();
				this.degreesPerSecond = reader.ReadSingle();
				this.radius = reader.ReadSingle();
				this.planeNormal = reader.ReadVector3();
				this.initialRadialDirection = reader.ReadVector3();
				this.initialRunTime = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.offset = reader.ReadVector3();
			}
			if ((num & 2) != 0)
			{
				this.initialDegreesFromOwnerForward = reader.ReadSingle();
			}
			if ((num & 4) != 0)
			{
				this.degreesPerSecond = reader.ReadSingle();
			}
			if ((num & 8) != 0)
			{
				this.radius = reader.ReadSingle();
			}
			if ((num & 16) != 0)
			{
				this.planeNormal = reader.ReadVector3();
			}
			if ((num & 32) != 0)
			{
				this.initialRadialDirection = reader.ReadVector3();
			}
			if ((num & 64) != 0)
			{
				this.initialRunTime = reader.ReadSingle();
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400426F RID: 17007
		[SyncVar]
		[SerializeField]
		private Vector3 offset;

		// Token: 0x04004270 RID: 17008
		[SyncVar]
		[SerializeField]
		private float initialDegreesFromOwnerForward;

		// Token: 0x04004271 RID: 17009
		[SyncVar]
		[SerializeField]
		private float degreesPerSecond;

		// Token: 0x04004272 RID: 17010
		[SerializeField]
		[SyncVar]
		private float radius;

		// Token: 0x04004273 RID: 17011
		[SyncVar]
		[SerializeField]
		private Vector3 planeNormal = Vector3.up;

		// Token: 0x04004274 RID: 17012
		private Transform ownerTransform;

		// Token: 0x04004275 RID: 17013
		private Rigidbody rigidBody;

		// Token: 0x04004276 RID: 17014
		private bool resetOnAcquireOwner = true;

		// Token: 0x04004277 RID: 17015
		[SyncVar]
		private Vector3 initialRadialDirection;

		// Token: 0x04004278 RID: 17016
		[SyncVar]
		private float initialRunTime;
	}
}
