using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2.Projectile
{
	// Token: 0x02000BB1 RID: 2993
	public class ProjectileSimple : MonoBehaviour
	{
		// Token: 0x06004429 RID: 17449 RVA: 0x0011BB7D File Offset: 0x00119D7D
		protected void Awake()
		{
			this.transform = base.transform;
			this.rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x0011BB97 File Offset: 0x00119D97
		protected void OnEnable()
		{
			this.SetForwardSpeed(this.desiredForwardSpeed);
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x0011BB97 File Offset: 0x00119D97
		protected void Start()
		{
			this.SetForwardSpeed(this.desiredForwardSpeed);
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x0011BBA5 File Offset: 0x00119DA5
		protected void OnDisable()
		{
			this.SetForwardSpeed(0f);
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x0011BBB4 File Offset: 0x00119DB4
		protected void FixedUpdate()
		{
			if (this.oscillate)
			{
				this.deltaHeight = Mathf.Sin(this.oscillationStopwatch * this.oscillateSpeed);
			}
			if (this.updateAfterFiring || this.enableVelocityOverLifetime)
			{
				this.SetForwardSpeed(this.desiredForwardSpeed);
			}
			this.oscillationStopwatch += Time.deltaTime;
			this.stopwatch += Time.deltaTime;
			if (NetworkServer.active && this.stopwatch > this.lifetime)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				if (this.lifetimeExpiredEffect && NetworkServer.active)
				{
					EffectManager.SimpleEffect(this.lifetimeExpiredEffect, this.transform.position, this.transform.rotation, true);
				}
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x0011BC77 File Offset: 0x00119E77
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x0011BC7F File Offset: 0x00119E7F
		[Obsolete("Use 'desiredForwardSpeed' instead.", false)]
		public float velocity
		{
			get
			{
				return this.desiredForwardSpeed;
			}
			set
			{
				this.desiredForwardSpeed = value;
			}
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x0011BC88 File Offset: 0x00119E88
		public void SetLifetime(float newLifetime)
		{
			this.lifetime = newLifetime;
			this.stopwatch = 0f;
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x0011BC9C File Offset: 0x00119E9C
		protected void SetForwardSpeed(float speed)
		{
			if (this.rigidbody)
			{
				if (this.enableVelocityOverLifetime)
				{
					this.rigidbody.velocity = speed * this.velocityOverLifetime.Evaluate(this.stopwatch / this.lifetime) * this.transform.forward;
					return;
				}
				this.rigidbody.velocity = this.transform.forward * speed + this.transform.right * (this.deltaHeight * this.oscillateMagnitude);
			}
		}

		// Token: 0x0400429B RID: 17051
		[Header("Lifetime")]
		public float lifetime = 5f;

		// Token: 0x0400429C RID: 17052
		public GameObject lifetimeExpiredEffect;

		// Token: 0x0400429D RID: 17053
		[FormerlySerializedAs("velocity")]
		[Header("Velocity")]
		public float desiredForwardSpeed;

		// Token: 0x0400429E RID: 17054
		public bool updateAfterFiring;

		// Token: 0x0400429F RID: 17055
		public bool enableVelocityOverLifetime;

		// Token: 0x040042A0 RID: 17056
		public AnimationCurve velocityOverLifetime;

		// Token: 0x040042A1 RID: 17057
		[Header("Oscillation")]
		public bool oscillate;

		// Token: 0x040042A2 RID: 17058
		public float oscillateMagnitude = 20f;

		// Token: 0x040042A3 RID: 17059
		public float oscillateSpeed;

		// Token: 0x040042A4 RID: 17060
		private float deltaHeight;

		// Token: 0x040042A5 RID: 17061
		private float oscillationStopwatch;

		// Token: 0x040042A6 RID: 17062
		private float stopwatch;

		// Token: 0x040042A7 RID: 17063
		private Rigidbody rigidbody;

		// Token: 0x040042A8 RID: 17064
		private new Transform transform;
	}
}
