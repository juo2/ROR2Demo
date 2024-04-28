using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Gup
{
	// Token: 0x0200033B RID: 827
	public class BaseSplitDeath : GenericCharacterDeath
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x00027DE1 File Offset: 0x00025FE1
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0004032C File Offset: 0x0003E52C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.deathDelay && !this.hasDied)
			{
				this.hasDied = true;
				if (NetworkServer.active)
				{
					EffectManager.SpawnEffect(BaseSplitDeath.deathEffectPrefab, new EffectData
					{
						origin = base.characterBody.corePosition,
						scale = base.characterBody.radius
					}, true);
					if (this.characterSpawnCard && this.spawnCount > 0 && (base.healthComponent.killingDamageType & (DamageType.VoidDeath | DamageType.OutOfBounds)) == DamageType.Generic)
					{
						new BodySplitter
						{
							body = base.characterBody,
							masterSummon = 
							{
								masterPrefab = this.characterSpawnCard.prefab
							},
							count = this.spawnCount,
							splinterInitialVelocityLocal = new Vector3(0f, 20f, 10f),
							minSpawnCircleRadius = base.characterBody.radius * BaseSplitDeath.spawnRadiusCoefficient,
							moneyMultiplier = this.moneyMultiplier
						}.Perform();
					}
					base.DestroyBodyAsapServer();
				}
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x040012A0 RID: 4768
		[SerializeField]
		public CharacterSpawnCard characterSpawnCard;

		// Token: 0x040012A1 RID: 4769
		[SerializeField]
		public int spawnCount;

		// Token: 0x040012A2 RID: 4770
		[SerializeField]
		public float deathDelay;

		// Token: 0x040012A3 RID: 4771
		[SerializeField]
		public float moneyMultiplier;

		// Token: 0x040012A4 RID: 4772
		public static float spawnRadiusCoefficient = 0.5f;

		// Token: 0x040012A5 RID: 4773
		public static GameObject deathEffectPrefab;

		// Token: 0x040012A6 RID: 4774
		private bool hasDied;
	}
}
