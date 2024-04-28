using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008AE RID: 2222
	public class SummonMasterBehavior : NetworkBehaviour
	{
		// Token: 0x06003147 RID: 12615 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000D15E7 File Offset: 0x000CF7E7
		[Server]
		public void OpenSummon(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SummonMasterBehavior::OpenSummon(RoR2.Interactor)' called on client");
				return;
			}
			this.OpenSummonReturnMaster(activator);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000D1608 File Offset: 0x000CF808
		[Server]
		public CharacterMaster OpenSummonReturnMaster(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.CharacterMaster RoR2.SummonMasterBehavior::OpenSummonReturnMaster(RoR2.Interactor)' called on client");
				return null;
			}
			float d = 0f;
			CharacterMaster characterMaster = new MasterSummon
			{
				masterPrefab = this.masterPrefab,
				position = base.transform.position + Vector3.up * d,
				rotation = base.transform.rotation,
				summonerBodyObject = ((activator != null) ? activator.gameObject : null),
				ignoreTeamMemberLimit = true,
				useAmbientLevel = new bool?(true)
			}.Perform();
			if (characterMaster)
			{
				GameObject bodyObject = characterMaster.GetBodyObject();
				if (bodyObject)
				{
					ModelLocator component = bodyObject.GetComponent<ModelLocator>();
					if (component && component.modelTransform)
					{
						TemporaryOverlay temporaryOverlay = component.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
						temporaryOverlay.duration = 0.5f;
						temporaryOverlay.animateShaderAlpha = true;
						temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
						temporaryOverlay.destroyComponentOnEnd = true;
						temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matSummonDrone");
						temporaryOverlay.AddToCharacerModel(component.modelTransform.GetComponent<CharacterModel>());
					}
				}
			}
			if (this.destroyAfterSummoning)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return characterMaster;
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000D175F File Offset: 0x000CF95F
		public void OnEnable()
		{
			if (this.callOnEquipmentSpentOnPurchase)
			{
				PurchaseInteraction.onEquipmentSpentOnPurchase += this.OnEquipmentSpentOnPurchase;
			}
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000D177A File Offset: 0x000CF97A
		public void OnDisable()
		{
			if (this.callOnEquipmentSpentOnPurchase)
			{
				PurchaseInteraction.onEquipmentSpentOnPurchase -= this.OnEquipmentSpentOnPurchase;
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000D1798 File Offset: 0x000CF998
		[Server]
		private void OnEquipmentSpentOnPurchase(PurchaseInteraction purchaseInteraction, Interactor interactor, EquipmentIndex equipmentIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SummonMasterBehavior::OnEquipmentSpentOnPurchase(RoR2.PurchaseInteraction,RoR2.Interactor,RoR2.EquipmentIndex)' called on client");
				return;
			}
			if (purchaseInteraction == base.GetComponent<PurchaseInteraction>())
			{
				CharacterMaster characterMaster = this.OpenSummonReturnMaster(interactor);
				if (characterMaster)
				{
					characterMaster.inventory.SetEquipmentIndex(equipmentIndex);
				}
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000D17F4 File Offset: 0x000CF9F4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040032E5 RID: 13029
		[Tooltip("The master to spawn")]
		public GameObject masterPrefab;

		// Token: 0x040032E6 RID: 13030
		public bool callOnEquipmentSpentOnPurchase;

		// Token: 0x040032E7 RID: 13031
		public bool destroyAfterSummoning = true;
	}
}
