using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008A3 RID: 2211
	[RequireComponent(typeof(DelayBlast))]
	public class SpiteBombController : NetworkBehaviour
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000CFEBD File Offset: 0x000CE0BD
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x000CFEC5 File Offset: 0x000CE0C5
		public DelayBlast delayBlast { get; private set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000CFECE File Offset: 0x000CE0CE
		// (set) Token: 0x060030EF RID: 12527 RVA: 0x000CFED6 File Offset: 0x000CE0D6
		public Vector3 bouncePosition
		{
			get
			{
				return this._bouncePosition;
			}
			set
			{
				this.Network_bouncePosition = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000CFEDF File Offset: 0x000CE0DF
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x000CFEE7 File Offset: 0x000CE0E7
		public float initialVelocityY
		{
			get
			{
				return this._initialVelocityY;
			}
			set
			{
				this.Network_initialVelocityY = value;
			}
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000CFEF0 File Offset: 0x000CE0F0
		private void Awake()
		{
			this.transform = base.transform;
			this.rb = base.GetComponent<Rigidbody>();
			this.delayBlast = base.GetComponent<DelayBlast>();
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000CFF18 File Offset: 0x000CE118
		private void Start()
		{
			this.startPosition = this.transform.position;
			float time = Trajectory.CalculateFlightDuration(this.startPosition.y, this.bouncePosition.y, this.initialVelocityY);
			Vector3 a = this.bouncePosition - this.startPosition;
			a.y = 0f;
			float magnitude = a.magnitude;
			float d = Trajectory.CalculateGroundSpeed(time, magnitude);
			this.velocity = a / magnitude * d;
			this.velocity.y = this.initialVelocityY;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000CFFA8 File Offset: 0x000CE1A8
		private void FixedUpdate()
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			this.velocity.y = this.velocity.y + fixedDeltaTime * Physics.gravity.y;
			Vector3 vector = this.transform.position;
			vector += this.velocity * fixedDeltaTime;
			if (vector.y < this.bouncePosition.y + this.radius)
			{
				this.velocity.y = Mathf.Max(this.velocity.y * -this.bounce, this.minimumBounceVelocity);
				this.velocity.x = 0f;
				this.velocity.z = 0f;
				vector.y = this.bouncePosition.y + this.radius;
				this.OnBounce();
			}
			this.rb.MovePosition(vector);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000D0084 File Offset: 0x000CE284
		private void OnBounce()
		{
			this.meshVisuals[this.bounces].SetActive(false);
			Util.PlaySound(this.bounceSoundStrings[this.bounces], base.gameObject);
			this.bounces++;
			if (this.bounces > SpiteBombController.maxBounces)
			{
				this.OnFinalBounce();
				return;
			}
			this.meshVisuals[this.bounces].SetActive(true);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000D00F2 File Offset: 0x000CE2F2
		private void OnFinalBounce()
		{
			if (NetworkServer.active)
			{
				this.delayBlast.position = this.transform.position;
				this.delayBlast.Detonate();
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000D0138 File Offset: 0x000CE338
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x000D014B File Offset: 0x000CE34B
		public Vector3 Network_bouncePosition
		{
			get
			{
				return this._bouncePosition;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Vector3>(value, ref this._bouncePosition, 1U);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000D0160 File Offset: 0x000CE360
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x000D0173 File Offset: 0x000CE373
		public float Network_initialVelocityY
		{
			get
			{
				return this._initialVelocityY;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._initialVelocityY, 2U);
			}
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000D0188 File Offset: 0x000CE388
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._bouncePosition);
				writer.Write(this._initialVelocityY);
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
				writer.Write(this._bouncePosition);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._initialVelocityY);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000D0234 File Offset: 0x000CE434
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._bouncePosition = reader.ReadVector3();
				this._initialVelocityY = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._bouncePosition = reader.ReadVector3();
			}
			if ((num & 2) != 0)
			{
				this._initialVelocityY = reader.ReadSingle();
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003292 RID: 12946
		public float radius;

		// Token: 0x04003293 RID: 12947
		public float bounce = 0.8f;

		// Token: 0x04003294 RID: 12948
		public float minimumBounceVelocity;

		// Token: 0x04003295 RID: 12949
		public GameObject[] meshVisuals;

		// Token: 0x04003296 RID: 12950
		public string[] bounceSoundStrings;

		// Token: 0x04003297 RID: 12951
		private new Transform transform;

		// Token: 0x04003298 RID: 12952
		private Rigidbody rb;

		// Token: 0x0400329A RID: 12954
		[SyncVar]
		private Vector3 _bouncePosition;

		// Token: 0x0400329B RID: 12955
		[SyncVar]
		private float _initialVelocityY;

		// Token: 0x0400329C RID: 12956
		private static readonly int maxBounces = 2;

		// Token: 0x0400329D RID: 12957
		private Vector3 startPosition;

		// Token: 0x0400329E RID: 12958
		private Vector3 velocity;

		// Token: 0x0400329F RID: 12959
		private int bounces;
	}
}
