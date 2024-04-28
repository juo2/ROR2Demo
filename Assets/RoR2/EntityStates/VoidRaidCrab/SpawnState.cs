using System;
using RoR2;
using RoR2.VoidRaidCrab;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200011F RID: 287
	public class SpawnState : BaseState
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00015B34 File Offset: 0x00013D34
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.spawnSoundString, base.gameObject);
			if (this.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.spawnEffectPrefab, base.gameObject, this.spawnMuzzleName, false);
			}
			if (this.doLegs && NetworkServer.active)
			{
				ChildLocator modelChildLocator = base.GetModelChildLocator();
				if (this.jointSpawnCard && modelChildLocator)
				{
					DirectorPlacementRule placementRule = new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Direct,
						spawnOnTarget = base.transform
					};
					this.SpawnJointBodyForLegServer(this.leg1Name, modelChildLocator, placementRule);
					this.SpawnJointBodyForLegServer(this.leg2Name, modelChildLocator, placementRule);
					this.SpawnJointBodyForLegServer(this.leg3Name, modelChildLocator, placementRule);
					this.SpawnJointBodyForLegServer(this.leg4Name, modelChildLocator, placementRule);
					this.SpawnJointBodyForLegServer(this.leg5Name, modelChildLocator, placementRule);
					this.SpawnJointBodyForLegServer(this.leg6Name, modelChildLocator, placementRule);
				}
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015C3C File Offset: 0x00013E3C
		private void SpawnJointBodyForLegServer(string legName, ChildLocator childLocator, DirectorPlacementRule placementRule)
		{
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.jointSpawnCard, placementRule, Run.instance.stageRng);
			directorSpawnRequest.summonerBodyObject = base.gameObject;
			DirectorCore instance = DirectorCore.instance;
			GameObject gameObject = (instance != null) ? instance.TrySpawnObject(directorSpawnRequest) : null;
			Transform transform = childLocator.FindChild(legName);
			if (gameObject && transform)
			{
				CharacterMaster component = gameObject.GetComponent<CharacterMaster>();
				if (component)
				{
					LegController component2 = transform.GetComponent<LegController>();
					if (component2)
					{
						component2.SetJointMaster(component, transform.GetComponent<ChildLocator>());
					}
				}
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00015CC6 File Offset: 0x00013EC6
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040005E5 RID: 1509
		[SerializeField]
		public float duration = 4f;

		// Token: 0x040005E6 RID: 1510
		[SerializeField]
		public string spawnSoundString;

		// Token: 0x040005E7 RID: 1511
		[SerializeField]
		public GameObject spawnEffectPrefab;

		// Token: 0x040005E8 RID: 1512
		[SerializeField]
		public string spawnMuzzleName;

		// Token: 0x040005E9 RID: 1513
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005EA RID: 1514
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005EB RID: 1515
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040005EC RID: 1516
		[SerializeField]
		public bool doLegs;

		// Token: 0x040005ED RID: 1517
		[SerializeField]
		public CharacterSpawnCard jointSpawnCard;

		// Token: 0x040005EE RID: 1518
		[SerializeField]
		public string leg1Name;

		// Token: 0x040005EF RID: 1519
		[SerializeField]
		public string leg2Name;

		// Token: 0x040005F0 RID: 1520
		[SerializeField]
		public string leg3Name;

		// Token: 0x040005F1 RID: 1521
		[SerializeField]
		public string leg4Name;

		// Token: 0x040005F2 RID: 1522
		[SerializeField]
		public string leg5Name;

		// Token: 0x040005F3 RID: 1523
		[SerializeField]
		public string leg6Name;
	}
}
