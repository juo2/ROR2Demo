using System;
using System.Collections.Generic;
using EntityStates.Interactables.GoldBeacon;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000713 RID: 1811
	public class GoldshoresMissionController : MonoBehaviour
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000A19A9 File Offset: 0x0009FBA9
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x000A19B0 File Offset: 0x0009FBB0
		public static GoldshoresMissionController instance { get; private set; }

		// Token: 0x0600255B RID: 9563 RVA: 0x000A19B8 File Offset: 0x0009FBB8
		private void OnEnable()
		{
			GoldshoresMissionController.instance = SingletonHelper.Assign<GoldshoresMissionController>(GoldshoresMissionController.instance, this);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000A19CA File Offset: 0x0009FBCA
		private void OnDisable()
		{
			GoldshoresMissionController.instance = SingletonHelper.Unassign<GoldshoresMissionController>(GoldshoresMissionController.instance, this);
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000A19DC File Offset: 0x0009FBDC
		public int beaconsActive
		{
			get
			{
				return Ready.count;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000A19E3 File Offset: 0x0009FBE3
		public int beaconCount
		{
			get
			{
				return Ready.count + NotReady.count;
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000A19F0 File Offset: 0x0009FBF0
		private void Start()
		{
			this.rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
			this.beginTransitionIntoBossFightEffect.SetActive(false);
			this.exitTransitionIntoBossFightEffect.SetActive(false);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000A1A28 File Offset: 0x0009FC28
		public void SpawnBeacons()
		{
			if (NetworkServer.active)
			{
				for (int i = 0; i < this.beaconsToSpawnOnMap; i++)
				{
					GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.beaconSpawnCard, new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Random
					}, this.rng));
					if (gameObject)
					{
						this.beaconInstanceList.Add(gameObject);
					}
				}
				this.beaconsToSpawnOnMap = this.beaconInstanceList.Count;
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000A1A9A File Offset: 0x0009FC9A
		public void BeginTransitionIntoBossfight()
		{
			this.beginTransitionIntoBossFightEffect.SetActive(true);
			this.exitTransitionIntoBossFightEffect.SetActive(false);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000A1AB4 File Offset: 0x0009FCB4
		public void ExitTransitionIntoBossfight()
		{
			this.beginTransitionIntoBossFightEffect.SetActive(false);
			this.exitTransitionIntoBossFightEffect.SetActive(true);
		}

		// Token: 0x0400292F RID: 10543
		public Xoroshiro128Plus rng;

		// Token: 0x04002930 RID: 10544
		public EntityStateMachine entityStateMachine;

		// Token: 0x04002931 RID: 10545
		public GameObject beginTransitionIntoBossFightEffect;

		// Token: 0x04002932 RID: 10546
		public GameObject exitTransitionIntoBossFightEffect;

		// Token: 0x04002933 RID: 10547
		public Transform bossSpawnPosition;

		// Token: 0x04002934 RID: 10548
		public List<GameObject> beaconInstanceList = new List<GameObject>();

		// Token: 0x04002935 RID: 10549
		public int beaconsRequiredToSpawnBoss;

		// Token: 0x04002936 RID: 10550
		public int beaconsToSpawnOnMap;

		// Token: 0x04002937 RID: 10551
		public InteractableSpawnCard beaconSpawnCard;
	}
}
