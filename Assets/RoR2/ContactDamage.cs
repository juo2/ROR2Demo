using System;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000680 RID: 1664
	[RequireComponent(typeof(CharacterBody))]
	public class ContactDamage : MonoBehaviour
	{
		// Token: 0x06002082 RID: 8322 RVA: 0x0008BC9C File Offset: 0x00089E9C
		private void Start()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
			this.teamComponent = base.GetComponent<TeamComponent>();
			this.InitOverlapAttack();
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0008BCBC File Offset: 0x00089EBC
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.hasFiredThisUpdate = false;
				this.refreshTimer -= Time.fixedDeltaTime;
				this.fireTimer -= Time.fixedDeltaTime;
				if (this.refreshTimer <= 0f)
				{
					this.refreshTimer = this.damageInterval;
					this.overlapAttack.ResetIgnoredHealthComponents();
					this.overlapAttack.teamIndex = this.teamComponent.teamIndex;
					this.FireOverlaps();
				}
				if (this.fireTimer <= 0f)
				{
					this.FireOverlaps();
				}
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0008BD50 File Offset: 0x00089F50
		private void InitOverlapAttack()
		{
			this.overlapAttack = new OverlapAttack
			{
				attacker = base.gameObject,
				inflictor = base.gameObject,
				hitBoxGroup = this.hitBoxGroup,
				teamIndex = this.teamComponent.teamIndex
			};
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0008BDA0 File Offset: 0x00089FA0
		private void FireOverlaps()
		{
			if (this.hasFiredThisUpdate)
			{
				return;
			}
			if (this.overlapAttack == null)
			{
				this.InitOverlapAttack();
			}
			this.hasFiredThisUpdate = true;
			this.fireTimer = ContactDamage.cvContactDamageUpdateInterval.value;
			this.overlapAttack.damage = this.characterBody.damage * this.damagePerSecondCoefficient * this.damageInterval;
			this.overlapAttack.pushAwayForce = this.pushForcePerSecond * this.damageInterval;
			this.overlapAttack.damageType = this.damageType;
			this.overlapAttack.Fire(null);
		}

		// Token: 0x040025C1 RID: 9665
		protected static FloatConVar cvContactDamageUpdateInterval = new FloatConVar("contact_damage_update_interval", ConVarFlags.Cheat, "0.1", "Frequency that the contact damage fires.");

		// Token: 0x040025C2 RID: 9666
		public float damagePerSecondCoefficient = 2f;

		// Token: 0x040025C3 RID: 9667
		[Tooltip("The frequency that this can hurt the same target. Is NOT the frequency that it checks for targets!")]
		public float damageInterval = 0.25f;

		// Token: 0x040025C4 RID: 9668
		public float pushForcePerSecond = 4000f;

		// Token: 0x040025C5 RID: 9669
		public HitBoxGroup hitBoxGroup;

		// Token: 0x040025C6 RID: 9670
		public DamageType damageType;

		// Token: 0x040025C7 RID: 9671
		public bool doBlastAttackInstead;

		// Token: 0x040025C8 RID: 9672
		private OverlapAttack overlapAttack;

		// Token: 0x040025C9 RID: 9673
		private CharacterBody characterBody;

		// Token: 0x040025CA RID: 9674
		private TeamComponent teamComponent;

		// Token: 0x040025CB RID: 9675
		private float refreshTimer;

		// Token: 0x040025CC RID: 9676
		private float fireTimer;

		// Token: 0x040025CD RID: 9677
		private bool hasFiredThisUpdate;
	}
}
