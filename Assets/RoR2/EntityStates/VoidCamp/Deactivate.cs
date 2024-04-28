using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidCamp
{
	// Token: 0x0200015D RID: 349
	public class Deactivate : EntityState
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x0001A5B0 File Offset: 0x000187B0
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.additiveAnimationLayerName, this.additiveAnimationStateName);
			FogDamageController componentInChildren = this.outer.GetComponentInChildren<FogDamageController>();
			if (componentInChildren)
			{
				componentInChildren.enabled = false;
			}
			Util.PlaySound(this.onEnterSoundString, base.gameObject);
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				modelChildLocator.FindChild("ActiveFX").gameObject.SetActive(false);
				modelChildLocator.FindChild("RangeFX").GetComponent<AnimateShaderAlpha>().enabled = true;
			}
			Chat.SendBroadcastChat(new Chat.SimpleChatMessage
			{
				baseToken = this.completeObjectiveChatMessageToken
			});
			if (NetworkServer.active && this.dropItem)
			{
				Transform transform = modelChildLocator.FindChild("RewardSpawnTarget");
				int participatingPlayerCount = Run.instance.participatingPlayerCount;
				if (participatingPlayerCount > 0 && transform && this.rewardDropTable)
				{
					int num = participatingPlayerCount;
					float angle = 360f / (float)num;
					Vector3 vector = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.up) * (Vector3.up * 40f + Vector3.forward * 5f);
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
					Vector3 position = transform.transform.position;
					PickupPickerController.Option[] pickerOptions = PickupPickerController.GenerateOptionsFromDropTable(3, this.rewardDropTable, Run.instance.treasureRng);
					int i = 0;
					while (i < num)
					{
						PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
						{
							pickupIndex = PickupCatalog.FindPickupIndex(ItemTier.VoidTier2),
							pickerOptions = pickerOptions,
							rotation = Quaternion.identity,
							prefabOverride = this.rewardPickupPrefab
						}, position, vector);
						i++;
						vector = rotation * vector;
					}
				}
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001A77C File Offset: 0x0001897C
		public override void OnExit()
		{
			this.PlayAnimation(this.baseAnimationLayerName, this.baseAnimationStateName);
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				modelChildLocator.FindChildGameObject(this.deactivateChildName).gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001A7C7 File Offset: 0x000189C7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new Idle());
			}
		}

		// Token: 0x04000770 RID: 1904
		[SerializeField]
		public float duration;

		// Token: 0x04000771 RID: 1905
		[SerializeField]
		public string onEnterSoundString;

		// Token: 0x04000772 RID: 1906
		[SerializeField]
		public string baseAnimationLayerName;

		// Token: 0x04000773 RID: 1907
		[SerializeField]
		public string baseAnimationStateName;

		// Token: 0x04000774 RID: 1908
		[SerializeField]
		public string deactivateChildName;

		// Token: 0x04000775 RID: 1909
		[SerializeField]
		public string additiveAnimationLayerName;

		// Token: 0x04000776 RID: 1910
		[SerializeField]
		public string additiveAnimationStateName;

		// Token: 0x04000777 RID: 1911
		[SerializeField]
		public string completeObjectiveChatMessageToken;

		// Token: 0x04000778 RID: 1912
		[SerializeField]
		public PickupDropTable rewardDropTable;

		// Token: 0x04000779 RID: 1913
		[SerializeField]
		public GameObject rewardPickupPrefab;

		// Token: 0x0400077A RID: 1914
		[SerializeField]
		public bool dropItem;
	}
}
