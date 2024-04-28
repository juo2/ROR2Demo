using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200076A RID: 1898
	public class InfiniteTowerExplicitSpawnWaveController : InfiniteTowerWaveController
	{
		// Token: 0x0600273F RID: 10047 RVA: 0x000AA908 File Offset: 0x000A8B08
		[Server]
		public override void Initialize(int waveIndex, Inventory enemyInventory, GameObject spawnTargetObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerExplicitSpawnWaveController::Initialize(System.Int32,RoR2.Inventory,UnityEngine.GameObject)' called on client");
				return;
			}
			base.Initialize(waveIndex, enemyInventory, spawnTargetObject);
			foreach (InfiniteTowerExplicitSpawnWaveController.SpawnInfo spawnInfo in this.spawnList)
			{
				for (int j = 0; j < spawnInfo.count; j++)
				{
					CombatDirector combatDirector = this.combatDirector;
					SpawnCard spawnCard = spawnInfo.spawnCard;
					EliteDef eliteDef = spawnInfo.eliteDef;
					Transform transform = this.spawnTarget.transform;
					bool preventOverhead = spawnInfo.preventOverhead;
					combatDirector.Spawn(spawnCard, eliteDef, transform, spawnInfo.spawnDistance, preventOverhead, 1f, DirectorPlacementRule.PlacementMode.Approximate);
				}
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000AA99C File Offset: 0x000A8B9C
		protected override void OnAllEnemiesDefeatedServer()
		{
			base.OnAllEnemiesDefeatedServer();
			InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
			if (infiniteTowerRun && !infiniteTowerRun.IsStageTransitionWave())
			{
				infiniteTowerRun.MoveSafeWard();
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void OnTimerExpire()
		{
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000AA9D0 File Offset: 0x000A8BD0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			bool flag2;
			return flag2 || flag;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000AA8F5 File Offset: 0x000A8AF5
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000AA8FF File Offset: 0x000A8AFF
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x04002B31 RID: 11057
		[Tooltip("The information for all of the characters to spawn.")]
		[SerializeField]
		private InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] spawnList;

		// Token: 0x0200076B RID: 1899
		[Serializable]
		private struct SpawnInfo
		{
			// Token: 0x04002B32 RID: 11058
			[SerializeField]
			public int count;

			// Token: 0x04002B33 RID: 11059
			[SerializeField]
			public CharacterSpawnCard spawnCard;

			// Token: 0x04002B34 RID: 11060
			[SerializeField]
			public EliteDef eliteDef;

			// Token: 0x04002B35 RID: 11061
			[SerializeField]
			public bool preventOverhead;

			// Token: 0x04002B36 RID: 11062
			[SerializeField]
			public DirectorCore.MonsterSpawnDistance spawnDistance;
		}
	}
}
