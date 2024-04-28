using System;
using System.Collections.Generic;
using System.Linq;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007AD RID: 1965
	public class MeteorStormController : MonoBehaviour
	{
		// Token: 0x06002980 RID: 10624 RVA: 0x000B36B3 File Offset: 0x000B18B3
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.meteorList = new List<MeteorStormController.Meteor>();
				this.waveList = new List<MeteorStormController.MeteorWave>();
			}
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000B36D4 File Offset: 0x000B18D4
		private void FixedUpdate()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.waveTimer -= Time.fixedDeltaTime;
			if (this.waveTimer <= 0f && this.wavesPerformed < this.waveCount)
			{
				this.wavesPerformed++;
				this.waveTimer = UnityEngine.Random.Range(this.waveMinInterval, this.waveMaxInterval);
				MeteorStormController.MeteorWave item = new MeteorStormController.MeteorWave(CharacterBody.readOnlyInstancesList.ToArray<CharacterBody>(), base.transform.position);
				this.waveList.Add(item);
			}
			for (int i = this.waveList.Count - 1; i >= 0; i--)
			{
				MeteorStormController.MeteorWave meteorWave = this.waveList[i];
				meteorWave.timer -= Time.fixedDeltaTime;
				if (meteorWave.timer <= 0f)
				{
					meteorWave.timer = UnityEngine.Random.Range(0.05f, 1f);
					MeteorStormController.Meteor nextMeteor = meteorWave.GetNextMeteor();
					if (nextMeteor == null)
					{
						this.waveList.RemoveAt(i);
					}
					else if (nextMeteor.valid)
					{
						this.meteorList.Add(nextMeteor);
						EffectManager.SpawnEffect(this.warningEffectPrefab, new EffectData
						{
							origin = nextMeteor.impactPosition,
							scale = this.blastRadius
						}, true);
					}
				}
			}
			float num = Run.instance.time - this.impactDelay;
			float num2 = num - this.travelEffectDuration;
			for (int j = this.meteorList.Count - 1; j >= 0; j--)
			{
				MeteorStormController.Meteor meteor = this.meteorList[j];
				if (meteor.startTime < num2 && !meteor.didTravelEffect)
				{
					this.DoMeteorEffect(meteor);
				}
				if (meteor.startTime < num)
				{
					this.meteorList.RemoveAt(j);
					this.DetonateMeteor(meteor);
				}
			}
			if (this.wavesPerformed == this.waveCount && this.meteorList.Count == 0)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000B38C1 File Offset: 0x000B1AC1
		private void OnDestroy()
		{
			this.onDestroyEvents.Invoke();
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000B38CE File Offset: 0x000B1ACE
		private void DoMeteorEffect(MeteorStormController.Meteor meteor)
		{
			meteor.didTravelEffect = true;
			if (this.travelEffectPrefab)
			{
				EffectManager.SpawnEffect(this.travelEffectPrefab, new EffectData
				{
					origin = meteor.impactPosition
				}, true);
			}
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000B3904 File Offset: 0x000B1B04
		private void DetonateMeteor(MeteorStormController.Meteor meteor)
		{
			EffectData effectData = new EffectData
			{
				origin = meteor.impactPosition
			};
			EffectManager.SpawnEffect(this.impactEffectPrefab, effectData, true);
			new BlastAttack
			{
				inflictor = base.gameObject,
				baseDamage = this.blastDamageCoefficient * this.ownerDamage,
				baseForce = this.blastForce,
				attackerFiltering = AttackerFiltering.AlwaysHit,
				crit = this.isCrit,
				falloffModel = BlastAttack.FalloffModel.Linear,
				attacker = this.owner,
				bonusForce = Vector3.zero,
				damageColorIndex = DamageColorIndex.Item,
				position = meteor.impactPosition,
				procChainMask = default(ProcChainMask),
				procCoefficient = 1f,
				teamIndex = TeamIndex.None,
				radius = this.blastRadius
			}.Fire();
		}

		// Token: 0x04002CDA RID: 11482
		public int waveCount;

		// Token: 0x04002CDB RID: 11483
		public float waveMinInterval;

		// Token: 0x04002CDC RID: 11484
		public float waveMaxInterval;

		// Token: 0x04002CDD RID: 11485
		public GameObject warningEffectPrefab;

		// Token: 0x04002CDE RID: 11486
		public GameObject travelEffectPrefab;

		// Token: 0x04002CDF RID: 11487
		public float travelEffectDuration;

		// Token: 0x04002CE0 RID: 11488
		public GameObject impactEffectPrefab;

		// Token: 0x04002CE1 RID: 11489
		public float impactDelay;

		// Token: 0x04002CE2 RID: 11490
		public float blastDamageCoefficient;

		// Token: 0x04002CE3 RID: 11491
		public float blastRadius;

		// Token: 0x04002CE4 RID: 11492
		public float blastForce;

		// Token: 0x04002CE5 RID: 11493
		[NonSerialized]
		public GameObject owner;

		// Token: 0x04002CE6 RID: 11494
		[NonSerialized]
		public float ownerDamage;

		// Token: 0x04002CE7 RID: 11495
		[NonSerialized]
		public bool isCrit;

		// Token: 0x04002CE8 RID: 11496
		public UnityEvent onDestroyEvents;

		// Token: 0x04002CE9 RID: 11497
		private List<MeteorStormController.Meteor> meteorList;

		// Token: 0x04002CEA RID: 11498
		private List<MeteorStormController.MeteorWave> waveList;

		// Token: 0x04002CEB RID: 11499
		private int wavesPerformed;

		// Token: 0x04002CEC RID: 11500
		private float waveTimer;

		// Token: 0x020007AE RID: 1966
		private class Meteor
		{
			// Token: 0x04002CED RID: 11501
			public Vector3 impactPosition;

			// Token: 0x04002CEE RID: 11502
			public float startTime;

			// Token: 0x04002CEF RID: 11503
			public bool didTravelEffect;

			// Token: 0x04002CF0 RID: 11504
			public bool valid = true;
		}

		// Token: 0x020007AF RID: 1967
		private class MeteorWave
		{
			// Token: 0x06002987 RID: 10631 RVA: 0x000B39E4 File Offset: 0x000B1BE4
			public MeteorWave(CharacterBody[] targets, Vector3 center)
			{
				this.targets = new CharacterBody[targets.Length];
				targets.CopyTo(this.targets, 0);
				Util.ShuffleArray<CharacterBody>(targets);
				this.center = center;
				this.nodeGraphSpider = new NodeGraphSpider(SceneInfo.instance.groundNodes, HullMask.Human);
				this.nodeGraphSpider.AddNodeForNextStep(SceneInfo.instance.groundNodes.FindClosestNode(center, HullClassification.Human, float.PositiveInfinity));
				int num = 0;
				int num2 = 20;
				while (num < num2 && this.nodeGraphSpider.PerformStep())
				{
					num++;
				}
			}

			// Token: 0x06002988 RID: 10632 RVA: 0x000B3A80 File Offset: 0x000B1C80
			public MeteorStormController.Meteor GetNextMeteor()
			{
				if (this.currentStep >= this.targets.Length)
				{
					return null;
				}
				CharacterBody characterBody = this.targets[this.currentStep];
				MeteorStormController.Meteor meteor = new MeteorStormController.Meteor();
				if (characterBody && UnityEngine.Random.value < this.hitChance)
				{
					meteor.impactPosition = characterBody.corePosition;
					Vector3 origin = meteor.impactPosition + Vector3.up * 6f;
					Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
					onUnitSphere.y = -1f;
					RaycastHit raycastHit;
					if (Physics.Raycast(origin, onUnitSphere, out raycastHit, 12f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
					{
						meteor.impactPosition = raycastHit.point;
					}
					else if (Physics.Raycast(meteor.impactPosition, Vector3.down, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
					{
						meteor.impactPosition = raycastHit.point;
					}
				}
				else if (this.nodeGraphSpider.collectedSteps.Count != 0)
				{
					int index = UnityEngine.Random.Range(0, this.nodeGraphSpider.collectedSteps.Count);
					SceneInfo.instance.groundNodes.GetNodePosition(this.nodeGraphSpider.collectedSteps[index].node, out meteor.impactPosition);
				}
				else
				{
					meteor.valid = false;
				}
				meteor.startTime = Run.instance.time;
				this.currentStep++;
				return meteor;
			}

			// Token: 0x04002CF1 RID: 11505
			private readonly CharacterBody[] targets;

			// Token: 0x04002CF2 RID: 11506
			private int currentStep;

			// Token: 0x04002CF3 RID: 11507
			private float hitChance = 0.4f;

			// Token: 0x04002CF4 RID: 11508
			private readonly Vector3 center;

			// Token: 0x04002CF5 RID: 11509
			public float timer;

			// Token: 0x04002CF6 RID: 11510
			private NodeGraphSpider nodeGraphSpider;
		}
	}
}
