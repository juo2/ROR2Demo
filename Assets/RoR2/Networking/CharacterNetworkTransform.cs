using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C30 RID: 3120
	public class CharacterNetworkTransform : NetworkBehaviour
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x001239C4 File Offset: 0x00121BC4
		public static ReadOnlyCollection<CharacterNetworkTransform> readOnlyInstancesList
		{
			get
			{
				return CharacterNetworkTransform._readOnlyInstancesList;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x001239D4 File Offset: 0x00121BD4
		// (set) Token: 0x06004680 RID: 18048 RVA: 0x001239CB File Offset: 0x00121BCB
		public new Transform transform { get; private set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x001239E5 File Offset: 0x00121BE5
		// (set) Token: 0x06004682 RID: 18050 RVA: 0x001239DC File Offset: 0x00121BDC
		public InputBankTest inputBank { get; private set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x001239F6 File Offset: 0x00121BF6
		// (set) Token: 0x06004684 RID: 18052 RVA: 0x001239ED File Offset: 0x00121BED
		public CharacterMotor characterMotor { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x00123A07 File Offset: 0x00121C07
		// (set) Token: 0x06004686 RID: 18054 RVA: 0x001239FE File Offset: 0x00121BFE
		public CharacterDirection characterDirection { get; private set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x00123A18 File Offset: 0x00121C18
		// (set) Token: 0x06004688 RID: 18056 RVA: 0x00123A0F File Offset: 0x00121C0F
		private Rigidbody rigidbody { get; set; }

		// Token: 0x0600468A RID: 18058 RVA: 0x00123A20 File Offset: 0x00121C20
		private CharacterNetworkTransform.Snapshot CalcCurrentSnapshot(float time, float interpolationDelay)
		{
			float num = time - interpolationDelay;
			if (this.snapshots.Count < 2)
			{
				CharacterNetworkTransform.Snapshot result = (this.snapshots.Count == 0) ? this.BuildSnapshot() : this.snapshots[0];
				result.serverTime = num;
				return result;
			}
			int num2 = 0;
			while (num2 < this.snapshots.Count - 2 && (this.snapshots[num2].serverTime > num || this.snapshots[num2 + 1].serverTime < num))
			{
				num2++;
			}
			return CharacterNetworkTransform.Snapshot.Interpolate(this.snapshots[num2], this.snapshots[num2 + 1], num);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00123AD0 File Offset: 0x00121CD0
		private CharacterNetworkTransform.Snapshot BuildSnapshot()
		{
			return new CharacterNetworkTransform.Snapshot
			{
				serverTime = PlatformSystems.networkManager.serverFixedTime,
				position = this.transform.position,
				moveVector = (this.inputBank ? this.inputBank.moveVector : Vector3.zero),
				aimDirection = (this.inputBank ? this.inputBank.aimDirection : Vector3.zero),
				rotation = (this.characterDirection ? Quaternion.Euler(0f, this.characterDirection.yaw, 0f) : this.transform.rotation),
				isGrounded = (this.characterMotor && this.characterMotor.isGrounded),
				groundNormal = (this.characterMotor ? this.characterMotor.estimatedGroundNormal : Vector3.zero)
			};
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00123BD8 File Offset: 0x00121DD8
		public void PushSnapshot(CharacterNetworkTransform.Snapshot newSnapshot)
		{
			if (this.debugSnapshotReceived)
			{
				Debug.LogFormat("{0} CharacterNetworkTransform snapshot received.", new object[]
				{
					base.gameObject
				});
			}
			if (this.snapshots.Count > 0 && newSnapshot.serverTime == this.snapshots[this.snapshots.Count - 1].serverTime)
			{
				Debug.Log("Received duplicate time!");
			}
			if (this.debugDuplicatePositions && this.snapshots.Count > 0 && newSnapshot.position == this.snapshots[this.snapshots.Count - 1].position)
			{
				Debug.Log("Received duplicate position!");
			}
			if (((this.snapshots.Count > 0) ? this.snapshots[this.snapshots.Count - 1].serverTime : float.NegativeInfinity) < newSnapshot.serverTime)
			{
				this.snapshots.Add(newSnapshot);
				this.newestNetSnapshot = newSnapshot;
				Debug.DrawLine(newSnapshot.position + Vector3.up, newSnapshot.position + Vector3.down, Color.white, 0.25f);
			}
			float num = PlatformSystems.networkManager.serverFixedTime - this.interpolationDelay * 3f;
			while (this.snapshots.Count > 2 && this.snapshots[1].serverTime < num)
			{
				this.snapshots.RemoveAt(0);
			}
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00123D54 File Offset: 0x00121F54
		private void Awake()
		{
			this.transform = base.transform;
			this.inputBank = base.GetComponent<InputBankTest>();
			this.characterMotor = base.GetComponent<CharacterMotor>();
			this.characterDirection = base.GetComponent<CharacterDirection>();
			this.rigidbody = base.GetComponent<Rigidbody>();
			if (this.rigidbody)
			{
				this.rigidbodyStartedKinematic = this.rigidbody.isKinematic;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x00123DBB File Offset: 0x00121FBB
		public float interpolationDelay
		{
			get
			{
				return this.positionTransmitInterval * this.interpolationFactor;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06004690 RID: 18064 RVA: 0x00123DD3 File Offset: 0x00121FD3
		// (set) Token: 0x0600468F RID: 18063 RVA: 0x00123DCA File Offset: 0x00121FCA
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x06004691 RID: 18065 RVA: 0x00123DDB File Offset: 0x00121FDB
		private void Start()
		{
			this.newestNetSnapshot = this.BuildSnapshot();
			this.UpdateAuthority();
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00123DEF File Offset: 0x00121FEF
		private void OnEnable()
		{
			bool flag = CharacterNetworkTransform.instancesList.Contains(this);
			CharacterNetworkTransform.instancesList.Add(this);
			if (flag)
			{
				Debug.LogError("Instance already in list!");
			}
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00123E13 File Offset: 0x00122013
		private void OnDisable()
		{
			CharacterNetworkTransform.instancesList.Remove(this);
			if (CharacterNetworkTransform.instancesList.Contains(this))
			{
				Debug.LogError("Instance was not fully removed from list!");
			}
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x00123E38 File Offset: 0x00122038
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
			if (this.rigidbody)
			{
				this.rigidbody.isKinematic = (!this.hasEffectiveAuthority || this.rigidbodyStartedKinematic);
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00123E74 File Offset: 0x00122074
		public override void OnStartAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00123E74 File Offset: 0x00122074
		public override void OnStopAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x00123E7C File Offset: 0x0012207C
		private void ApplyCurrentSnapshot(float currentTime)
		{
			CharacterNetworkTransform.Snapshot snapshot = this.CalcCurrentSnapshot(currentTime, this.interpolationDelay);
			if (!this.characterMotor)
			{
				if (this.rigidbodyStartedKinematic)
				{
					this.transform.position = snapshot.position;
				}
				else
				{
					this.rigidbody.MovePosition(snapshot.position);
				}
			}
			if (this.inputBank)
			{
				this.inputBank.moveVector = snapshot.moveVector;
				this.inputBank.aimDirection = snapshot.aimDirection;
			}
			if (this.characterMotor)
			{
				this.characterMotor.netIsGrounded = snapshot.isGrounded;
				this.characterMotor.netGroundNormal = snapshot.groundNormal;
				if (this.characterMotor.Motor.enabled)
				{
					this.characterMotor.Motor.MoveCharacter(snapshot.position);
				}
				else
				{
					this.characterMotor.Motor.SetPosition(snapshot.position, true);
				}
			}
			if (this.characterDirection)
			{
				this.characterDirection.yaw = snapshot.rotation.eulerAngles.y;
				return;
			}
			if (this.rigidbodyStartedKinematic)
			{
				this.transform.rotation = snapshot.rotation;
				return;
			}
			this.rigidbody.MoveRotation(snapshot.rotation);
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x00123FC4 File Offset: 0x001221C4
		private void FixedUpdate()
		{
			if (!this.hasEffectiveAuthority)
			{
				this.ApplyCurrentSnapshot(PlatformSystems.networkManager.serverFixedTime);
				return;
			}
			this.newestNetSnapshot = this.BuildSnapshot();
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x00124044 File Offset: 0x00122244
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04004458 RID: 17496
		private static List<CharacterNetworkTransform> instancesList = new List<CharacterNetworkTransform>();

		// Token: 0x04004459 RID: 17497
		private static ReadOnlyCollection<CharacterNetworkTransform> _readOnlyInstancesList = new ReadOnlyCollection<CharacterNetworkTransform>(CharacterNetworkTransform.instancesList);

		// Token: 0x0400445F RID: 17503
		[Tooltip("The delay in seconds between position network updates.")]
		public float positionTransmitInterval = 0.1f;

		// Token: 0x04004460 RID: 17504
		[HideInInspector]
		public float lastPositionTransmitTime = float.NegativeInfinity;

		// Token: 0x04004461 RID: 17505
		[Tooltip("The number of packets of buffers to have.")]
		public float interpolationFactor = 2f;

		// Token: 0x04004462 RID: 17506
		public CharacterNetworkTransform.Snapshot newestNetSnapshot;

		// Token: 0x04004463 RID: 17507
		private List<CharacterNetworkTransform.Snapshot> snapshots = new List<CharacterNetworkTransform.Snapshot>();

		// Token: 0x04004464 RID: 17508
		public bool debugDuplicatePositions;

		// Token: 0x04004465 RID: 17509
		public bool debugSnapshotReceived;

		// Token: 0x04004466 RID: 17510
		private bool rigidbodyStartedKinematic = true;

		// Token: 0x02000C31 RID: 3121
		public struct Snapshot
		{
			// Token: 0x0600469F RID: 18079 RVA: 0x00124054 File Offset: 0x00122254
			private static bool LerpGroundNormal(ref CharacterNetworkTransform.Snapshot a, ref CharacterNetworkTransform.Snapshot b, float t, out Vector3 groundNormal)
			{
				groundNormal = Vector3.zero;
				bool flag = (t > 0f) ? b.isGrounded : a.isGrounded;
				if (flag)
				{
					if (b.isGrounded)
					{
						if (a.isGrounded)
						{
							groundNormal = Vector3.Slerp(a.groundNormal, b.groundNormal, t);
							return flag;
						}
						groundNormal = b.groundNormal;
						return flag;
					}
					else
					{
						groundNormal = a.groundNormal;
					}
				}
				return flag;
			}

			// Token: 0x060046A0 RID: 18080 RVA: 0x001240C8 File Offset: 0x001222C8
			public static CharacterNetworkTransform.Snapshot Lerp(CharacterNetworkTransform.Snapshot a, CharacterNetworkTransform.Snapshot b, float t)
			{
				Vector3 vector;
				bool flag = CharacterNetworkTransform.Snapshot.LerpGroundNormal(ref a, ref b, t, out vector);
				return new CharacterNetworkTransform.Snapshot
				{
					position = Vector3.Lerp(a.position, b.position, t),
					moveVector = Vector3.Lerp(a.moveVector, b.moveVector, t),
					aimDirection = Vector3.Slerp(a.aimDirection, b.moveVector, t),
					rotation = Quaternion.Lerp(a.rotation, b.rotation, t),
					isGrounded = flag,
					groundNormal = vector
				};
			}

			// Token: 0x060046A1 RID: 18081 RVA: 0x00124160 File Offset: 0x00122360
			public static CharacterNetworkTransform.Snapshot Interpolate(CharacterNetworkTransform.Snapshot a, CharacterNetworkTransform.Snapshot b, float serverTime)
			{
				float t = (serverTime - a.serverTime) / (b.serverTime - a.serverTime);
				Vector3 vector;
				bool flag = CharacterNetworkTransform.Snapshot.LerpGroundNormal(ref a, ref b, t, out vector);
				return new CharacterNetworkTransform.Snapshot
				{
					serverTime = serverTime,
					position = Vector3.LerpUnclamped(a.position, b.position, t),
					moveVector = Vector3.Lerp(a.moveVector, b.moveVector, t),
					aimDirection = Vector3.Slerp(a.aimDirection, b.aimDirection, t),
					rotation = Quaternion.Lerp(a.rotation, b.rotation, t),
					isGrounded = flag,
					groundNormal = vector
				};
			}

			// Token: 0x04004468 RID: 17512
			public float serverTime;

			// Token: 0x04004469 RID: 17513
			public Vector3 position;

			// Token: 0x0400446A RID: 17514
			public Vector3 moveVector;

			// Token: 0x0400446B RID: 17515
			public Vector3 aimDirection;

			// Token: 0x0400446C RID: 17516
			public Quaternion rotation;

			// Token: 0x0400446D RID: 17517
			public bool isGrounded;

			// Token: 0x0400446E RID: 17518
			public Vector3 groundNormal;
		}
	}
}
