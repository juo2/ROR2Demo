using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B83 RID: 2947
	[RequireComponent(typeof(ProjectileTargetComponent))]
	[RequireComponent(typeof(Rigidbody))]
	public class MissileController : MonoBehaviour
	{
		// Token: 0x06004313 RID: 17171 RVA: 0x00116480 File Offset: 0x00114680
		private void Awake()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
				return;
			}
			this.transform = base.transform;
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.torquePID = base.GetComponent<QuaternionPID>();
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.targetComponent = base.GetComponent<ProjectileTargetComponent>();
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x001164D8 File Offset: 0x001146D8
		private void FixedUpdate()
		{
			this.timer += Time.fixedDeltaTime;
			if (this.timer < this.giveupTimer)
			{
				this.rigidbody.velocity = this.transform.forward * this.maxVelocity;
				if (this.targetComponent.target && this.timer >= this.delayTimer)
				{
					this.rigidbody.velocity = this.transform.forward * (this.maxVelocity + this.timer * this.acceleration);
					Vector3 vector = this.targetComponent.target.position + UnityEngine.Random.insideUnitSphere * this.turbulence - this.transform.position;
					if (vector != Vector3.zero)
					{
						Quaternion rotation = this.transform.rotation;
						Quaternion targetQuat = Util.QuaternionSafeLookRotation(vector);
						this.torquePID.inputQuat = rotation;
						this.torquePID.targetQuat = targetQuat;
						this.rigidbody.angularVelocity = this.torquePID.UpdatePID();
					}
				}
			}
			if (!this.targetComponent.target)
			{
				this.targetComponent.target = this.FindTarget();
			}
			else
			{
				HealthComponent component = this.targetComponent.target.GetComponent<HealthComponent>();
				if (component && !component.alive)
				{
					this.targetComponent.target = this.FindTarget();
				}
			}
			if (this.timer > this.deathTimer)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x00116674 File Offset: 0x00114874
		private Transform FindTarget()
		{
			this.search.searchOrigin = this.transform.position;
			this.search.searchDirection = this.transform.forward;
			this.search.teamMaskFilter.RemoveTeam(this.teamFilter.teamIndex);
			this.search.RefreshCandidates();
			HurtBox hurtBox = this.search.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox == null)
			{
				return null;
			}
			return hurtBox.transform;
		}

		// Token: 0x04004122 RID: 16674
		private new Transform transform;

		// Token: 0x04004123 RID: 16675
		private Rigidbody rigidbody;

		// Token: 0x04004124 RID: 16676
		private TeamFilter teamFilter;

		// Token: 0x04004125 RID: 16677
		private ProjectileTargetComponent targetComponent;

		// Token: 0x04004126 RID: 16678
		public float maxVelocity;

		// Token: 0x04004127 RID: 16679
		public float rollVelocity;

		// Token: 0x04004128 RID: 16680
		public float acceleration;

		// Token: 0x04004129 RID: 16681
		public float delayTimer;

		// Token: 0x0400412A RID: 16682
		public float giveupTimer = 8f;

		// Token: 0x0400412B RID: 16683
		public float deathTimer = 10f;

		// Token: 0x0400412C RID: 16684
		private float timer;

		// Token: 0x0400412D RID: 16685
		private QuaternionPID torquePID;

		// Token: 0x0400412E RID: 16686
		public float turbulence;

		// Token: 0x0400412F RID: 16687
		public float maxSeekDistance = 40f;

		// Token: 0x04004130 RID: 16688
		private BullseyeSearch search = new BullseyeSearch();
	}
}
