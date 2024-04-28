using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007AA RID: 1962
	public class MasterSuicideOnTimer : MonoBehaviour
	{
		// Token: 0x06002977 RID: 10615 RVA: 0x000B3474 File Offset: 0x000B1674
		private void Start()
		{
			this.characterMaster = base.GetComponent<CharacterMaster>();
			if (this.minionOwnership && this.minionOwnership.ownerMaster)
			{
				this.ownerBody = this.minionOwnership.ownerMaster.GetBody();
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000B34C4 File Offset: 0x000B16C4
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				if (this.ownerBody)
				{
					if (!this.body)
					{
						this.body = this.characterMaster.GetBody();
					}
					if (this.body && (this.ownerBody.transform.position - this.body.transform.position).sqrMagnitude < this.timerResetDistanceToOwner * this.timerResetDistanceToOwner)
					{
						this.timer = 0f;
					}
				}
				this.timer += Time.fixedDeltaTime;
				if (this.timer >= this.lifeTimer && !this.hasDied)
				{
					GameObject bodyObject = this.characterMaster.GetBodyObject();
					if (bodyObject)
					{
						HealthComponent component = bodyObject.GetComponent<HealthComponent>();
						if (component)
						{
							component.Suicide(null, null, DamageType.Generic);
							this.hasDied = true;
						}
					}
				}
			}
		}

		// Token: 0x04002CCC RID: 11468
		public float lifeTimer;

		// Token: 0x04002CCD RID: 11469
		[Tooltip("Reset the timer if we're within this radius of the owner")]
		public float timerResetDistanceToOwner;

		// Token: 0x04002CCE RID: 11470
		public MinionOwnership minionOwnership;

		// Token: 0x04002CCF RID: 11471
		private CharacterBody body;

		// Token: 0x04002CD0 RID: 11472
		private CharacterBody ownerBody;

		// Token: 0x04002CD1 RID: 11473
		private float timer;

		// Token: 0x04002CD2 RID: 11474
		private bool hasDied;

		// Token: 0x04002CD3 RID: 11475
		private CharacterMaster characterMaster;
	}
}
