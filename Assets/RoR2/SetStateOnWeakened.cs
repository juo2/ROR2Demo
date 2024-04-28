using System;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000889 RID: 2185
	public class SetStateOnWeakened : NetworkBehaviour, IOnTakeDamageServerReceiver
	{
		// Token: 0x06003002 RID: 12290 RVA: 0x000CC44E File Offset: 0x000CA64E
		private void Start()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000CC45C File Offset: 0x000CA65C
		public void OnTakeDamageServer(DamageReport damageReport)
		{
			if (this.consumed)
			{
				return;
			}
			if (this.targetStateMachine && base.isServer && this.characterBody)
			{
				DamageInfo damageInfo = damageReport.damageInfo;
				float num = damageInfo.crit ? (damageInfo.damage * 2f) : damageInfo.damage;
				if ((damageInfo.damageType & DamageType.WeakPointHit) != DamageType.Generic)
				{
					this.accumulatedDamage += num;
					if (this.accumulatedDamage > this.characterBody.maxHealth * this.weakenPercentage)
					{
						this.consumed = true;
						this.SetWeak(damageInfo);
					}
				}
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000CC4FC File Offset: 0x000CA6FC
		public void SetWeak(DamageInfo damageInfo)
		{
			if (this.targetStateMachine)
			{
				this.targetStateMachine.SetInterruptState(EntityStateCatalog.InstantiateState(this.hurtState), InterruptPriority.Pain);
			}
			EntityStateMachine[] array = this.idleStateMachine;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetNextState(new Idle());
			}
			HurtBox[] array2 = this.weakHurtBox;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].gameObject.SetActive(false);
			}
			if (this.selfDamagePercentage > 0f)
			{
				DamageInfo damageInfo2 = new DamageInfo();
				damageInfo2.damage = this.characterBody.maxHealth * this.selfDamagePercentage / 3f;
				damageInfo2.attacker = damageInfo.attacker;
				damageInfo2.crit = true;
				damageInfo2.position = damageInfo.position;
				damageInfo2.damageType = (DamageType.NonLethal | DamageType.WeakPointHit);
				this.characterBody.healthComponent.TakeDamage(damageInfo2);
			}
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000CC5F0 File Offset: 0x000CA7F0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040031A9 RID: 12713
		[Tooltip("The percentage of their max HP they need to take to get weakened. Ranges from 0-1.")]
		public float weakenPercentage = 0.1f;

		// Token: 0x040031AA RID: 12714
		[Tooltip("The percentage of their max HP they deal to themselves once weakened. Ranges from 0-1.")]
		public float selfDamagePercentage;

		// Token: 0x040031AB RID: 12715
		[Tooltip("The state machine to set the state of when this character is hurt.")]
		public EntityStateMachine targetStateMachine;

		// Token: 0x040031AC RID: 12716
		[Tooltip("The state machine to set to idle when this character is hurt.")]
		public EntityStateMachine[] idleStateMachine;

		// Token: 0x040031AD RID: 12717
		[Tooltip("The hurtboxes to set to not a weak point once consumed")]
		public HurtBox[] weakHurtBox;

		// Token: 0x040031AE RID: 12718
		[Tooltip("The state to enter when this character is hurt.")]
		public SerializableEntityStateType hurtState;

		// Token: 0x040031AF RID: 12719
		private float accumulatedDamage;

		// Token: 0x040031B0 RID: 12720
		private bool consumed;

		// Token: 0x040031B1 RID: 12721
		private CharacterBody characterBody;
	}
}
