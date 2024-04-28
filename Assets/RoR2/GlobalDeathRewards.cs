using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200070C RID: 1804
	public class GlobalDeathRewards : MonoBehaviour
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x0009DE46 File Offset: 0x0009C046
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.runRNG.nextUlong);
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x0009DE69 File Offset: 0x0009C069
		private void OnEnable()
		{
			GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x0009DE7C File Offset: 0x0009C07C
		private void OnDisable()
		{
			GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0009DE90 File Offset: 0x0009C090
		private void OnCharacterDeathGlobal(DamageReport damageReport)
		{
			if (NetworkServer.active)
			{
				bool flag = true;
				if (this.requirePlayerAttacker)
				{
					flag = (damageReport.attackerMaster && damageReport.attackerMaster.GetComponent<PlayerCharacterMasterController>());
				}
				if (flag)
				{
					foreach (GlobalDeathRewards.PickupReward pickupReward in this.pickupRewards)
					{
						if ((damageReport.victimBody.bodyFlags & pickupReward.requiredBodyFlags) == pickupReward.requiredBodyFlags && pickupReward.dropTable && this.rng.nextNormalizedFloat <= pickupReward.chance)
						{
							PickupDropletController.CreatePickupDroplet(pickupReward.dropTable.GenerateDrop(this.rng), damageReport.victim.transform.position, pickupReward.velocity);
						}
					}
				}
			}
		}

		// Token: 0x0400290B RID: 10507
		[SerializeField]
		private bool requirePlayerAttacker;

		// Token: 0x0400290C RID: 10508
		[SerializeField]
		private GlobalDeathRewards.PickupReward[] pickupRewards;

		// Token: 0x0400290D RID: 10509
		private Xoroshiro128Plus rng;

		// Token: 0x0200070D RID: 1805
		[Serializable]
		public struct PickupReward
		{
			// Token: 0x0400290E RID: 10510
			[SerializeField]
			public PickupDropTable dropTable;

			// Token: 0x0400290F RID: 10511
			[EnumMask(typeof(CharacterBody.BodyFlags))]
			[SerializeField]
			public CharacterBody.BodyFlags requiredBodyFlags;

			// Token: 0x04002910 RID: 10512
			[Range(0f, 1f)]
			[SerializeField]
			public float chance;

			// Token: 0x04002911 RID: 10513
			[SerializeField]
			public Vector3 velocity;
		}
	}
}
