using System;
using System.Runtime.InteropServices;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006F9 RID: 1785
	public class GenericEnergyComponent : NetworkBehaviour
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x0009B978 File Offset: 0x00099B78
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x0009B980 File Offset: 0x00099B80
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x0600245D RID: 9309 RVA: 0x0009B989 File Offset: 0x00099B89
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0009B99C File Offset: 0x00099B9C
		private void Awake()
		{
			this.UpdateAuthority();
			this.internalChargeRate = ((this.capacity != 0f) ? (this.chargeRate / this.capacity) : 0f);
			if (NetworkServer.active)
			{
				this.energy = this.capacity;
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0009B9E9 File Offset: 0x00099BE9
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0009B9F7 File Offset: 0x00099BF7
		public override void OnStopAuthority()
		{
			base.OnStopAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0009BA05 File Offset: 0x00099C05
		private void OnEnable()
		{
			this.internalChargeRate = ((this.capacity != 0f) ? (this.chargeRate / this.capacity) : 0f);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0009BA2E File Offset: 0x00099C2E
		private void OnDisable()
		{
			this.internalChargeRate = 0f;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x0009BA3C File Offset: 0x00099C3C
		// (set) Token: 0x06002464 RID: 9316 RVA: 0x0009BA8C File Offset: 0x00099C8C
		public float energy
		{
			get
			{
				if (this.internalChargeRate == 0f)
				{
					return (this.referenceTime - Run.FixedTimeStamp.zero) * this.capacity;
				}
				return Mathf.Clamp01(this.referenceTime.timeSince * this.internalChargeRate) * this.capacity;
			}
			set
			{
				float num = this.capacity.Equals(0f) ? 0f : Mathf.Clamp01(value / this.capacity);
				if (this.internalChargeRate == 0f)
				{
					this.NetworkreferenceTime = Run.FixedTimeStamp.zero + num;
					return;
				}
				this.NetworkreferenceTime = Run.FixedTimeStamp.now - num / this.internalChargeRate;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x0009BAFA File Offset: 0x00099CFA
		public float normalizedEnergy
		{
			get
			{
				if (this.capacity == 0f)
				{
					return 0f;
				}
				return Mathf.Clamp01(this.energy / this.capacity);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x0009BB21 File Offset: 0x00099D21
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x0009BB2C File Offset: 0x00099D2C
		private float internalChargeRate
		{
			get
			{
				return this._internalChargeRate;
			}
			set
			{
				if (this._internalChargeRate == value)
				{
					return;
				}
				float energy = this.energy;
				this._internalChargeRate = value;
				this.energy = energy;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x0009BB58 File Offset: 0x00099D58
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x0009BB60 File Offset: 0x00099D60
		public float chargeRate
		{
			get
			{
				return this._chargeRate;
			}
			set
			{
				if (this._chargeRate == value)
				{
					return;
				}
				float energy = this.energy;
				this._chargeRate = value;
				this.internalChargeRate = ((this.capacity != 0f) ? (this._chargeRate / this.capacity) : 0f);
				this.energy = energy;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x0009BBB3 File Offset: 0x00099DB3
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x0009BBD5 File Offset: 0x00099DD5
		public float normalizedChargeRate
		{
			get
			{
				if (this.capacity == 0f)
				{
					return 0f;
				}
				return this.chargeRate / this.capacity;
			}
			set
			{
				this.chargeRate = ((this.capacity != 0f) ? (value * this.capacity) : 0f);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0009BBF9 File Offset: 0x00099DF9
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x0009BC04 File Offset: 0x00099E04
		public float capacity
		{
			get
			{
				return this._capacity;
			}
			set
			{
				if (value == this._capacity)
				{
					return;
				}
				float energy = this.energy;
				this._capacity = value;
				this.energy = energy;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0009BC30 File Offset: 0x00099E30
		[Server]
		public bool TakeEnergy(float amount)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.GenericEnergyComponent::TakeEnergy(System.Single)' called on client");
				return false;
			}
			if (amount > this.energy)
			{
				return false;
			}
			this.energy -= amount;
			return true;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x0009BC80 File Offset: 0x00099E80
		// (set) Token: 0x06002472 RID: 9330 RVA: 0x0009BC93 File Offset: 0x00099E93
		public Run.FixedTimeStamp NetworkreferenceTime
		{
			get
			{
				return this.referenceTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Run.FixedTimeStamp>(value, ref this.referenceTime, 1U);
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0009BCA8 File Offset: 0x00099EA8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteFixedTimeStamp_Run(writer, this.referenceTime);
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
				GeneratedNetworkCode._WriteFixedTimeStamp_Run(writer, this.referenceTime);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x0009BD14 File Offset: 0x00099F14
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.referenceTime = GeneratedNetworkCode._ReadFixedTimeStamp_Run(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.referenceTime = GeneratedNetworkCode._ReadFixedTimeStamp_Run(reader);
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040028A4 RID: 10404
		[SerializeField]
		private float _capacity = 1f;

		// Token: 0x040028A5 RID: 10405
		[SerializeField]
		private float _chargeRate = 1f;

		// Token: 0x040028A6 RID: 10406
		[SyncVar]
		private Run.FixedTimeStamp referenceTime;

		// Token: 0x040028A8 RID: 10408
		private float _internalChargeRate;
	}
}
