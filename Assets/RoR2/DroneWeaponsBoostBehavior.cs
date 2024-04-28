using System;
using System.Collections.Generic;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200077E RID: 1918
	public class DroneWeaponsBoostBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x000AE440 File Offset: 0x000AC640
		public void Start()
		{
			base.GetComponent<InputBankTest>();
			ModelLocator component = base.GetComponent<ModelLocator>();
			if (component)
			{
				Transform modelTransform = component.modelTransform;
				if (modelTransform)
				{
					CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
					if (component2)
					{
						List<GameObject> itemDisplayObjects = component2.GetItemDisplayObjects(DLC1Content.Items.DroneWeaponsDisplay1.itemIndex);
						itemDisplayObjects.AddRange(component2.GetItemDisplayObjects(DLC1Content.Items.DroneWeaponsDisplay2.itemIndex));
						foreach (GameObject gameObject in itemDisplayObjects)
						{
							ChildLocator component3 = gameObject.GetComponent<ChildLocator>();
							if (component3)
							{
								Transform exists = component3.FindChild("MissileMuzzle");
								if (exists)
								{
									this.missileMuzzleTransform = exists;
									break;
								}
							}
						}
					}
				}
			}
			this.chainGunController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/DroneWeaponsChainGunController"));
			this.chainGunController.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000AE544 File Offset: 0x000AC744
		public void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.chainGunController);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000AE554 File Offset: 0x000AC754
		public void OnEnemyHit(DamageInfo damageInfo, CharacterBody victimBody)
		{
			CharacterBody component = damageInfo.attacker.GetComponent<CharacterBody>();
			CharacterMaster characterMaster = (component != null) ? component.master : null;
			if (characterMaster && !damageInfo.procChainMask.HasProc(ProcType.MicroMissile) && Util.CheckRoll(10f, characterMaster))
			{
				ProcChainMask procChainMask = damageInfo.procChainMask;
				procChainMask.AddProc(ProcType.MicroMissile);
				HurtBox hurtBox = victimBody.mainHurtBox;
				if (victimBody.hurtBoxGroup)
				{
					hurtBox = victimBody.hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, victimBody.hurtBoxGroup.hurtBoxes.Length)];
				}
				if (hurtBox)
				{
					MicroMissileOrb microMissileOrb = new MicroMissileOrb();
					microMissileOrb.damageValue = component.damage * 3f;
					microMissileOrb.isCrit = Util.CheckRoll(component.crit, characterMaster);
					microMissileOrb.teamIndex = TeamComponent.GetObjectTeam(component.gameObject);
					microMissileOrb.attacker = component.gameObject;
					microMissileOrb.procCoefficient = 1f;
					microMissileOrb.procChainMask = procChainMask;
					microMissileOrb.origin = (this.missileMuzzleTransform ? this.missileMuzzleTransform.position : component.corePosition);
					microMissileOrb.target = hurtBox;
					microMissileOrb.damageColorIndex = DamageColorIndex.Item;
					OrbManager.instance.AddOrb(microMissileOrb);
				}
			}
		}

		// Token: 0x04002BD0 RID: 11216
		private const string controllerPrefabPath = "Prefabs/NetworkedObjects/DroneWeaponsChainGunController";

		// Token: 0x04002BD1 RID: 11217
		private const string missileMuzzleChildName = "MissileMuzzle";

		// Token: 0x04002BD2 RID: 11218
		private const float microMissileDamageCoefficient = 3f;

		// Token: 0x04002BD3 RID: 11219
		private const float microMissileProcCoefficient = 1f;

		// Token: 0x04002BD4 RID: 11220
		private Transform missileMuzzleTransform;

		// Token: 0x04002BD5 RID: 11221
		private GameObject chainGunController;
	}
}
