using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200062E RID: 1582
	public class CharacterDirection : NetworkBehaviour, ILifeBehavior
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00080519 File Offset: 0x0007E719
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x000804E3 File Offset: 0x0007E6E3
		public float yaw
		{
			get
			{
				return this._yaw;
			}
			set
			{
				this._yaw = value;
				if (this.targetTransform)
				{
					this.targetTransform.rotation = Quaternion.Euler(0f, this._yaw, 0f);
				}
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00080524 File Offset: 0x0007E724
		public Vector3 animatorForward
		{
			get
			{
				if (!this.overrideAnimatorForwardTransform)
				{
					return this.forward;
				}
				float y = this.overrideAnimatorForwardTransform.eulerAngles.y;
				return Quaternion.Euler(0f, y, 0f) * Vector3.forward;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000805A7 File Offset: 0x0007E7A7
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x00080570 File Offset: 0x0007E770
		public Vector3 forward
		{
			get
			{
				return Quaternion.Euler(0f, this.yaw, 0f) * Vector3.forward;
			}
			set
			{
				value.y = 0f;
				this.yaw = Util.QuaternionSafeLookRotation(value, Vector3.up).eulerAngles.y;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x000805D1 File Offset: 0x0007E7D1
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x000805C8 File Offset: 0x0007E7C8
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x06001DED RID: 7661 RVA: 0x000805D9 File Offset: 0x0007E7D9
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000805EC File Offset: 0x0007E7EC
		public override void OnStartAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000805EC File Offset: 0x0007E7EC
		public override void OnStopAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000805F4 File Offset: 0x0007E7F4
		private void Start()
		{
			this.UpdateAuthority();
			ModelLocator component = base.GetComponent<ModelLocator>();
			if (component)
			{
				this.modelAnimator = component.modelTransform.GetComponent<Animator>();
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00080627 File Offset: 0x0007E827
		private void Update()
		{
			this.Simulate(Time.deltaTime);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		public void OnDeathStart()
		{
			base.enabled = false;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00080634 File Offset: 0x0007E834
		private static int PickIndex(float angle)
		{
			float num = Mathf.Sign(angle);
			int num2 = Mathf.CeilToInt((angle * num - 22.5f) * 0.022222223f);
			return Mathf.Clamp(CharacterDirection.paramsMidIndex + num2 * (int)num, 0, CharacterDirection.turnAnimatorParamsSets.Length - 1);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00080678 File Offset: 0x0007E878
		private void Simulate(float deltaTime)
		{
			Quaternion quaternion = Quaternion.Euler(0f, this.yaw, 0f);
			if (this.hasEffectiveAuthority)
			{
				if (this.driveFromRootRotation)
				{
					Quaternion rhs = this.rootMotionAccumulator.ExtractRootRotation();
					if (this.targetTransform)
					{
						this.targetTransform.rotation = quaternion * rhs;
						float y = this.targetTransform.rotation.eulerAngles.y;
						this.yaw = y;
						float angle = 0f;
						if (this.moveVector.sqrMagnitude > 0f)
						{
							angle = Util.AngleSigned(Vector3.ProjectOnPlane(this.moveVector, Vector3.up), this.targetTransform.forward, -Vector3.up);
						}
						int num = CharacterDirection.PickIndex(angle);
						if (this.turnSoundName != null && num != this.previousParamsIndex)
						{
							Util.PlaySound(this.turnSoundName, base.gameObject);
						}
						this.previousParamsIndex = num;
						CharacterDirection.turnAnimatorParamsSets[num].Apply(this.modelAnimator);
					}
				}
				this.targetVector = this.moveVector;
				this.targetVector.y = 0f;
				if (this.targetVector != Vector3.zero && deltaTime != 0f)
				{
					this.targetVector.Normalize();
					Quaternion quaternion2 = Util.QuaternionSafeLookRotation(this.targetVector, Vector3.up);
					float num2 = Mathf.SmoothDampAngle(this.yaw, quaternion2.eulerAngles.y, ref this.yRotationVelocity, 360f / this.turnSpeed * 0.25f, float.PositiveInfinity, deltaTime);
					quaternion = Quaternion.Euler(0f, num2, 0f);
					this.yaw = num2;
				}
				if (this.targetTransform)
				{
					this.targetTransform.rotation = quaternion;
				}
			}
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00080AE8 File Offset: 0x0007ECE8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040023BE RID: 9150
		[HideInInspector]
		public Vector3 moveVector;

		// Token: 0x040023BF RID: 9151
		[Tooltip("The transform to rotate.")]
		public Transform targetTransform;

		// Token: 0x040023C0 RID: 9152
		[Tooltip("The transform to take the rotation from for animator purposes. Commonly the root node.")]
		public Transform overrideAnimatorForwardTransform;

		// Token: 0x040023C1 RID: 9153
		public RootMotionAccumulator rootMotionAccumulator;

		// Token: 0x040023C2 RID: 9154
		public Animator modelAnimator;

		// Token: 0x040023C3 RID: 9155
		[Tooltip("The character direction is set by root rotation, rather than moveVector.")]
		public bool driveFromRootRotation;

		// Token: 0x040023C4 RID: 9156
		[Tooltip("The maximum turn rate in degrees/second.")]
		public float turnSpeed = 360f;

		// Token: 0x040023C5 RID: 9157
		[SerializeField]
		private string turnSoundName;

		// Token: 0x040023C6 RID: 9158
		private int previousParamsIndex = -1;

		// Token: 0x040023C7 RID: 9159
		private float yRotationVelocity;

		// Token: 0x040023C8 RID: 9160
		private float _yaw;

		// Token: 0x040023C9 RID: 9161
		private Vector3 targetVector = Vector3.zero;

		// Token: 0x040023CB RID: 9163
		private const float offset = 22.5f;

		// Token: 0x040023CC RID: 9164
		private static readonly CharacterDirection.TurnAnimatorParamsSet[] turnAnimatorParamsSets = new CharacterDirection.TurnAnimatorParamsSet[]
		{
			new CharacterDirection.TurnAnimatorParamsSet
			{
				angleMin = -180f,
				angleMax = -112.5f,
				turnRight45 = false,
				turnRight90 = false,
				turnRight135 = false,
				turnLeft45 = false,
				turnLeft90 = false,
				turnLeft135 = true
			},
			new CharacterDirection.TurnAnimatorParamsSet
			{
				angleMin = -112.5f,
				angleMax = -67.5f,
				turnRight45 = false,
				turnRight90 = false,
				turnRight135 = false,
				turnLeft45 = false,
				turnLeft90 = true,
				turnLeft135 = false
			},
			new CharacterDirection.TurnAnimatorParamsSet
			{
				angleMin = -67.5f,
				angleMax = -22.5f,
				turnRight45 = false,
				turnRight90 = false,
				turnRight135 = false,
				turnLeft45 = true,
				turnLeft90 = false,
				turnLeft135 = false
			},
			new CharacterDirection.TurnAnimatorParamsSet
			{
				turnRight45 = false,
				turnRight90 = false,
				turnRight135 = false,
				turnLeft45 = false,
				turnLeft90 = false,
				turnLeft135 = false
			},
			new CharacterDirection.TurnAnimatorParamsSet
			{
				angleMin = 22.5f,
				angleMax = 67.5f,
				turnRight45 = true,
				turnRight90 = false,
				turnRight135 = false,
				turnLeft45 = false,
				turnLeft90 = false,
				turnLeft135 = false
			},
			new CharacterDirection.TurnAnimatorParamsSet
			{
				angleMin = 67.5f,
				angleMax = 112.5f,
				turnRight45 = false,
				turnRight90 = true,
				turnRight135 = false,
				turnLeft45 = false,
				turnLeft90 = false,
				turnLeft135 = false
			},
			new CharacterDirection.TurnAnimatorParamsSet
			{
				angleMin = 112.5f,
				angleMax = 180f,
				turnRight45 = false,
				turnRight90 = false,
				turnRight135 = true,
				turnLeft45 = false,
				turnLeft90 = false,
				turnLeft135 = false
			}
		};

		// Token: 0x040023CD RID: 9165
		private static readonly int paramsMidIndex = CharacterDirection.turnAnimatorParamsSets.Length >> 1;

		// Token: 0x0200062F RID: 1583
		private struct TurnAnimatorParamsSet
		{
			// Token: 0x06001DFB RID: 7675 RVA: 0x00080AF8 File Offset: 0x0007ECF8
			public void Apply(Animator animator)
			{
				animator.SetBool(CharacterDirection.TurnAnimatorParamsSet.turnRight45ParamHash, this.turnRight45);
				animator.SetBool(CharacterDirection.TurnAnimatorParamsSet.turnRight90ParamHash, this.turnRight90);
				animator.SetBool(CharacterDirection.TurnAnimatorParamsSet.turnRight135ParamHash, this.turnRight135);
				animator.SetBool(CharacterDirection.TurnAnimatorParamsSet.turnLeft45ParamHash, this.turnLeft45);
				animator.SetBool(CharacterDirection.TurnAnimatorParamsSet.turnLeft90ParamHash, this.turnLeft90);
				animator.SetBool(CharacterDirection.TurnAnimatorParamsSet.turnLeft135ParamHash, this.turnLeft135);
			}

			// Token: 0x040023CE RID: 9166
			public float angleMin;

			// Token: 0x040023CF RID: 9167
			public float angleMax;

			// Token: 0x040023D0 RID: 9168
			public bool turnRight45;

			// Token: 0x040023D1 RID: 9169
			public bool turnRight90;

			// Token: 0x040023D2 RID: 9170
			public bool turnRight135;

			// Token: 0x040023D3 RID: 9171
			public bool turnLeft45;

			// Token: 0x040023D4 RID: 9172
			public bool turnLeft90;

			// Token: 0x040023D5 RID: 9173
			public bool turnLeft135;

			// Token: 0x040023D6 RID: 9174
			private static readonly int turnRight45ParamHash = Animator.StringToHash("turnRight45");

			// Token: 0x040023D7 RID: 9175
			private static readonly int turnRight90ParamHash = Animator.StringToHash("turnRight90");

			// Token: 0x040023D8 RID: 9176
			private static readonly int turnRight135ParamHash = Animator.StringToHash("turnRight135");

			// Token: 0x040023D9 RID: 9177
			private static readonly int turnLeft45ParamHash = Animator.StringToHash("turnLeft45");

			// Token: 0x040023DA RID: 9178
			private static readonly int turnLeft90ParamHash = Animator.StringToHash("turnLeft90");

			// Token: 0x040023DB RID: 9179
			private static readonly int turnLeft135ParamHash = Animator.StringToHash("turnLeft135");
		}
	}
}
