using System;
using UnityEngine;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x02000391 RID: 913
	public class ChaseTarget : BaseSpiderMineState
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00047F0B File Offset: 0x0004610B
		private Transform target
		{
			get
			{
				return base.projectileTargetComponent.target;
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00047F18 File Offset: 0x00046118
		public override void OnEnter()
		{
			base.OnEnter();
			this.passedDetonationRadius = false;
			this.bestDistance = float.PositiveInfinity;
			this.PlayAnimation("Base", "Chase");
			if (base.isAuthority)
			{
				this.orientationHelper = base.gameObject.AddComponent<ChaseTarget.OrientationHelper>();
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00047F68 File Offset: 0x00046168
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (!this.target)
				{
					base.rigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
					this.outer.SetNextState(new WaitForStick());
					return;
				}
				Vector3 position = this.target.position;
				Vector3 position2 = base.transform.position;
				Vector3 a = position - position2;
				float magnitude = a.magnitude;
				float y = base.rigidbody.velocity.y;
				Vector3 velocity = a * (ChaseTarget.speed / magnitude);
				velocity.y = y;
				base.rigidbody.velocity = velocity;
				if (!this.passedDetonationRadius && magnitude <= ChaseTarget.triggerRadius)
				{
					this.passedDetonationRadius = true;
				}
				if (magnitude < this.bestDistance)
				{
					this.bestDistance = magnitude;
					return;
				}
				if (this.passedDetonationRadius)
				{
					this.outer.SetNextState(new PreDetonate());
				}
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00048050 File Offset: 0x00046250
		public override void OnExit()
		{
			base.FindModelChild(this.childLocatorStringToEnable).gameObject.SetActive(false);
			if (this.orientationHelper != null)
			{
				EntityState.Destroy(this.orientationHelper);
				this.orientationHelper = null;
			}
			base.OnExit();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040014CC RID: 5324
		public static float speed;

		// Token: 0x040014CD RID: 5325
		public static float triggerRadius;

		// Token: 0x040014CE RID: 5326
		private bool passedDetonationRadius;

		// Token: 0x040014CF RID: 5327
		private float bestDistance;

		// Token: 0x040014D0 RID: 5328
		private ChaseTarget.OrientationHelper orientationHelper;

		// Token: 0x02000392 RID: 914
		private class OrientationHelper : MonoBehaviour
		{
			// Token: 0x0600106C RID: 4204 RVA: 0x00048089 File Offset: 0x00046289
			private void Awake()
			{
				this.rigidbody = base.GetComponent<Rigidbody>();
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x00048098 File Offset: 0x00046298
			private void OnCollisionStay(Collision collision)
			{
				int contactCount = collision.contactCount;
				if (contactCount == 0)
				{
					return;
				}
				Vector3 vector = collision.GetContact(0).normal;
				for (int i = 1; i < contactCount; i++)
				{
					Vector3 normal = collision.GetContact(i).normal;
					if (vector.y < normal.y)
					{
						vector = normal;
					}
				}
				this.rigidbody.MoveRotation(Quaternion.LookRotation(vector));
			}

			// Token: 0x040014D1 RID: 5329
			private Rigidbody rigidbody;
		}
	}
}
