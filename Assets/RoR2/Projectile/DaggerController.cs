using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B75 RID: 2933
	[RequireComponent(typeof(Rigidbody))]
	public class DaggerController : MonoBehaviour
	{
		// Token: 0x060042D3 RID: 17107 RVA: 0x00114D95 File Offset: 0x00112F95
		private void Awake()
		{
			this.transform = base.transform;
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.rigidbody.AddRelativeForce(UnityEngine.Random.insideUnitSphere * 50f);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x00114DCC File Offset: 0x00112FCC
		private void FixedUpdate()
		{
			this.timer += Time.fixedDeltaTime;
			if (this.timer < this.giveupTimer)
			{
				if (this.target)
				{
					Vector3 vector = this.target.transform.position - this.transform.position;
					if (vector != Vector3.zero)
					{
						this.transform.rotation = Util.QuaternionSafeLookRotation(vector);
					}
					if (this.timer >= this.delayTimer)
					{
						this.rigidbody.AddForce(this.transform.forward * this.acceleration);
						if (!this.hasPlayedSound)
						{
							Util.PlaySound("Play_item_proc_dagger_fly", base.gameObject);
							this.hasPlayedSound = true;
						}
					}
				}
			}
			else
			{
				this.rigidbody.useGravity = true;
			}
			if (!this.target)
			{
				this.target = this.FindTarget();
			}
			else
			{
				HealthComponent component = this.target.GetComponent<HealthComponent>();
				if (component && !component.alive)
				{
					this.target = this.FindTarget();
				}
			}
			if (this.timer > this.deathTimer)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x00114F04 File Offset: 0x00113104
		private Transform FindTarget()
		{
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Monster);
			float num = 99999f;
			Transform result = null;
			for (int i = 0; i < teamMembers.Count; i++)
			{
				float num2 = Vector3.SqrMagnitude(teamMembers[i].transform.position - this.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = teamMembers[i].transform;
				}
			}
			return result;
		}

		// Token: 0x040040C6 RID: 16582
		private new Transform transform;

		// Token: 0x040040C7 RID: 16583
		private Rigidbody rigidbody;

		// Token: 0x040040C8 RID: 16584
		public Transform target;

		// Token: 0x040040C9 RID: 16585
		public float acceleration;

		// Token: 0x040040CA RID: 16586
		public float delayTimer;

		// Token: 0x040040CB RID: 16587
		public float giveupTimer = 8f;

		// Token: 0x040040CC RID: 16588
		public float deathTimer = 10f;

		// Token: 0x040040CD RID: 16589
		private float timer;

		// Token: 0x040040CE RID: 16590
		public float turbulence;

		// Token: 0x040040CF RID: 16591
		private bool hasPlayedSound;
	}
}
