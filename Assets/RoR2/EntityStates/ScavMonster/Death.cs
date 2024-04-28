using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001D3 RID: 467
	public class Death : GenericCharacterDeath
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002367C File Offset: 0x0002187C
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				CharacterMaster characterMaster = base.characterBody ? base.characterBody.master : null;
				if (characterMaster)
				{
					bool flag = characterMaster.IsExtraLifePendingServer();
					bool flag2 = characterMaster.inventory.GetItemCount(RoR2Content.Items.Ghost) > 0;
					bool flag3 = !Stage.instance || Stage.instance.scavPackDroppedServer;
					this.shouldDropPack = (!flag && !flag2 && !flag3);
					if (this.shouldDropPack)
					{
						Stage.instance.scavPackDroppedServer = true;
					}
				}
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00023715 File Offset: 0x00021915
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= Death.duration)
			{
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00023737 File Offset: 0x00021937
		protected override void OnPreDestroyBodyServer()
		{
			base.OnPreDestroyBodyServer();
			if (this.shouldDropPack)
			{
				this.AttemptDropPack();
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00023750 File Offset: 0x00021950
		private void AttemptDropPack()
		{
			DirectorCore instance = DirectorCore.instance;
			if (instance)
			{
				Xoroshiro128Plus rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
				DirectorPlacementRule placementRule = new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
					position = base.characterBody.footPosition,
					minDistance = 0f,
					maxDistance = float.PositiveInfinity
				};
				instance.TrySpawnObject(new DirectorSpawnRequest(this.spawnCard, placementRule, rng));
			}
		}

		// Token: 0x040009D1 RID: 2513
		[SerializeField]
		public SpawnCard spawnCard;

		// Token: 0x040009D2 RID: 2514
		public static float duration;

		// Token: 0x040009D3 RID: 2515
		private bool shouldDropPack;
	}
}
