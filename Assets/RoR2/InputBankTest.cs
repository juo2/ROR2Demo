using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200076E RID: 1902
	[RequireComponent(typeof(CharacterBody))]
	public class InputBankTest : MonoBehaviour
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000ABBD7 File Offset: 0x000A9DD7
		// (set) Token: 0x06002788 RID: 10120 RVA: 0x000ABBFD File Offset: 0x000A9DFD
		public Vector3 aimDirection
		{
			get
			{
				if (!(this._aimDirection != Vector3.zero))
				{
					return base.transform.forward;
				}
				return this._aimDirection;
			}
			set
			{
				this._aimDirection = value.normalized;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000ABC0C File Offset: 0x000A9E0C
		public Vector3 aimOrigin
		{
			get
			{
				if (!this.characterBody.aimOriginTransform)
				{
					return base.transform.position;
				}
				return this.characterBody.aimOriginTransform.position;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000ABC3C File Offset: 0x000A9E3C
		public Ray GetAimRay()
		{
			return new Ray(this.aimOrigin, this.aimDirection);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000ABC50 File Offset: 0x000A9E50
		public bool GetAimRaycast(float maxDistance, out RaycastHit hitInfo)
		{
			float time = Time.time;
			float fixedTime = Time.fixedTime;
			if (!this.cachedRaycast.time.Equals(time) || !this.cachedRaycast.fixedTime.Equals(fixedTime) || (this.cachedRaycast.maxDistance < maxDistance && !this.cachedRaycast.didHit))
			{
				float num = 0f;
				Ray ray = CameraRigController.ModifyAimRayIfApplicable(this.GetAimRay(), base.gameObject, out num);
				this.cachedRaycast = InputBankTest.CachedRaycastInfo.empty;
				this.cachedRaycast.time = time;
				this.cachedRaycast.fixedTime = fixedTime;
				this.cachedRaycast.maxDistance = maxDistance;
				GameObject gameObject = base.gameObject;
				Ray ray2 = ray;
				float maxDistance2 = maxDistance + num;
				LayerMask layerMask = LayerIndex.world.mask | LayerIndex.entityPrecise.mask;
				this.cachedRaycast.didHit = Util.CharacterRaycast(gameObject, ray2, out this.cachedRaycast.hitInfo, maxDistance2, layerMask, QueryTriggerInteraction.Ignore);
			}
			bool flag = this.cachedRaycast.didHit;
			hitInfo = this.cachedRaycast.hitInfo;
			if (flag && hitInfo.distance > maxDistance)
			{
				flag = false;
				hitInfo = default(RaycastHit);
			}
			return flag;
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x000ABD8B File Offset: 0x000A9F8B
		// (set) Token: 0x0600278D RID: 10125 RVA: 0x000ABD93 File Offset: 0x000A9F93
		public int emoteRequest { get; set; } = -1;

		// Token: 0x0600278E RID: 10126 RVA: 0x000ABD9C File Offset: 0x000A9F9C
		public bool CheckAnyButtonDown()
		{
			return this.skill1.down || this.skill2.down || this.skill3.down || this.skill4.down || this.interact.down || this.jump.down || this.sprint.down || this.activateEquipment.down || this.ping.down;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000ABE1E File Offset: 0x000AA01E
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000ABE2C File Offset: 0x000AA02C
		private void Start()
		{
			if (this.characterBody.hasEffectiveAuthority)
			{
				if (this.characterBody.characterDirection)
				{
					this.aimDirection = this.characterBody.characterDirection.forward;
					return;
				}
				this.aimDirection = base.transform.forward;
			}
		}

		// Token: 0x04002B6C RID: 11116
		private CharacterBody characterBody;

		// Token: 0x04002B6D RID: 11117
		private Vector3 _aimDirection;

		// Token: 0x04002B6E RID: 11118
		private float lastRaycastTime = float.NegativeInfinity;

		// Token: 0x04002B6F RID: 11119
		private float lastFixedRaycastTime = float.NegativeInfinity;

		// Token: 0x04002B70 RID: 11120
		private bool didLastRaycastHit;

		// Token: 0x04002B71 RID: 11121
		private RaycastHit lastHitInfo;

		// Token: 0x04002B72 RID: 11122
		private float lastMaxDistance;

		// Token: 0x04002B73 RID: 11123
		private InputBankTest.CachedRaycastInfo cachedRaycast = InputBankTest.CachedRaycastInfo.empty;

		// Token: 0x04002B74 RID: 11124
		public Vector3 moveVector;

		// Token: 0x04002B75 RID: 11125
		public InputBankTest.ButtonState skill1;

		// Token: 0x04002B76 RID: 11126
		public InputBankTest.ButtonState skill2;

		// Token: 0x04002B77 RID: 11127
		public InputBankTest.ButtonState skill3;

		// Token: 0x04002B78 RID: 11128
		public InputBankTest.ButtonState skill4;

		// Token: 0x04002B79 RID: 11129
		public InputBankTest.ButtonState interact;

		// Token: 0x04002B7A RID: 11130
		public InputBankTest.ButtonState jump;

		// Token: 0x04002B7B RID: 11131
		public InputBankTest.ButtonState sprint;

		// Token: 0x04002B7C RID: 11132
		public InputBankTest.ButtonState activateEquipment;

		// Token: 0x04002B7D RID: 11133
		public InputBankTest.ButtonState ping;

		// Token: 0x0200076F RID: 1903
		private struct CachedRaycastInfo
		{
			// Token: 0x04002B7F RID: 11135
			public float time;

			// Token: 0x04002B80 RID: 11136
			public float fixedTime;

			// Token: 0x04002B81 RID: 11137
			public bool didHit;

			// Token: 0x04002B82 RID: 11138
			public RaycastHit hitInfo;

			// Token: 0x04002B83 RID: 11139
			public float maxDistance;

			// Token: 0x04002B84 RID: 11140
			public static readonly InputBankTest.CachedRaycastInfo empty = new InputBankTest.CachedRaycastInfo
			{
				time = float.NegativeInfinity,
				fixedTime = float.NegativeInfinity,
				didHit = false,
				maxDistance = 0f
			};
		}

		// Token: 0x02000770 RID: 1904
		public struct ButtonState
		{
			// Token: 0x17000383 RID: 899
			// (get) Token: 0x06002793 RID: 10131 RVA: 0x000ABEF7 File Offset: 0x000AA0F7
			public bool justReleased
			{
				get
				{
					return !this.down && this.wasDown;
				}
			}

			// Token: 0x17000384 RID: 900
			// (get) Token: 0x06002794 RID: 10132 RVA: 0x000ABF09 File Offset: 0x000AA109
			public bool justPressed
			{
				get
				{
					return this.down && !this.wasDown;
				}
			}

			// Token: 0x06002795 RID: 10133 RVA: 0x000ABF1E File Offset: 0x000AA11E
			public void PushState(bool newState)
			{
				this.hasPressBeenClaimed = (this.hasPressBeenClaimed && newState);
				this.wasDown = this.down;
				this.down = newState;
			}

			// Token: 0x04002B85 RID: 11141
			public bool down;

			// Token: 0x04002B86 RID: 11142
			public bool wasDown;

			// Token: 0x04002B87 RID: 11143
			public bool hasPressBeenClaimed;
		}
	}
}
