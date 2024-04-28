using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.QuestVolatileBattery
{
	// Token: 0x02000220 RID: 544
	public class CountDown : QuestVolatileBatteryBaseState
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x000273EC File Offset: 0x000255EC
		public override void OnEnter()
		{
			base.OnEnter();
			if (CountDown.vfxPrefab && base.attachedCharacterModel)
			{
				List<GameObject> equipmentDisplayObjects = base.attachedCharacterModel.GetEquipmentDisplayObjects(RoR2Content.Equipment.QuestVolatileBattery.equipmentIndex);
				if (equipmentDisplayObjects.Count > 0)
				{
					this.vfxInstances = new GameObject[equipmentDisplayObjects.Count];
					for (int i = 0; i < this.vfxInstances.Length; i++)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(CountDown.vfxPrefab, equipmentDisplayObjects[i].transform);
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localRotation = Quaternion.identity;
						this.vfxInstances[i] = gameObject;
					}
				}
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000274A4 File Offset: 0x000256A4
		public override void OnExit()
		{
			GameObject[] array = this.vfxInstances;
			for (int i = 0; i < array.Length; i++)
			{
				EntityState.Destroy(array[i]);
			}
			this.vfxInstances = Array.Empty<GameObject>();
			base.OnExit();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000274DF File Offset: 0x000256DF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000274F4 File Offset: 0x000256F4
		private void FixedUpdateServer()
		{
			if (base.fixedAge >= CountDown.duration && !this.detonated)
			{
				this.detonated = true;
				this.Detonate();
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00027518 File Offset: 0x00025718
		public void Detonate()
		{
			if (!base.networkedBodyAttachment.attachedBody)
			{
				return;
			}
			Vector3 corePosition = base.networkedBodyAttachment.attachedBody.corePosition;
			float baseDamage = 0f;
			if (base.attachedHealthComponent)
			{
				baseDamage = base.attachedHealthComponent.fullCombinedHealth * 3f;
			}
			EffectManager.SpawnEffect(CountDown.explosionEffectPrefab, new EffectData
			{
				origin = corePosition,
				scale = CountDown.explosionRadius
			}, true);
			new BlastAttack
			{
				position = corePosition + UnityEngine.Random.onUnitSphere,
				radius = CountDown.explosionRadius,
				falloffModel = BlastAttack.FalloffModel.None,
				attacker = base.networkedBodyAttachment.attachedBodyObject,
				inflictor = base.networkedBodyAttachment.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				baseDamage = baseDamage,
				baseForce = 5000f,
				bonusForce = Vector3.zero,
				attackerFiltering = AttackerFiltering.AlwaysHit,
				crit = false,
				procChainMask = default(ProcChainMask),
				procCoefficient = 0f,
				teamIndex = base.networkedBodyAttachment.attachedBody.teamComponent.teamIndex
			}.Fire();
			base.networkedBodyAttachment.attachedBody.inventory.SetEquipmentIndex(EquipmentIndex.None);
			this.outer.SetNextState(new Idle());
		}

		// Token: 0x04000B15 RID: 2837
		public static float duration;

		// Token: 0x04000B16 RID: 2838
		public static GameObject vfxPrefab;

		// Token: 0x04000B17 RID: 2839
		public static float explosionRadius;

		// Token: 0x04000B18 RID: 2840
		public static GameObject explosionEffectPrefab;

		// Token: 0x04000B19 RID: 2841
		private GameObject[] vfxInstances = Array.Empty<GameObject>();

		// Token: 0x04000B1A RID: 2842
		private bool detonated;
	}
}
