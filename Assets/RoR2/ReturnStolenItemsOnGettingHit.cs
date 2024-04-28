using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000848 RID: 2120
	public class ReturnStolenItemsOnGettingHit : MonoBehaviour, IOnTakeDamageServerReceiver, IOnKilledServerReceiver
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000C4BE2 File Offset: 0x000C2DE2
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x000C4BEC File Offset: 0x000C2DEC
		public ItemStealController itemStealController
		{
			get
			{
				return this._itemStealController;
			}
			set
			{
				if (this._itemStealController != null)
				{
					this._itemStealController.onLendingFinishServer.RemoveListener(new UnityAction(this.InitializeDamageTracking));
				}
				if (value)
				{
					value.onLendingFinishServer.AddListener(new UnityAction(this.InitializeDamageTracking));
					this._itemStealController = value;
					return;
				}
				this._itemStealController = null;
			}
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000C4C4B File Offset: 0x000C2E4B
		public void OnTakeDamageServer(DamageReport damageReport)
		{
			if (this.itemStealController && this.itemStealController.hasStolen && !damageReport.damageInfo.rejected)
			{
				this.accumulatedDamage += damageReport.damageDealt;
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000C4C87 File Offset: 0x000C2E87
		private void Awake()
		{
			this.maxPercentagePerItem = Mathf.Max(this.minPercentagePerItem, this.maxPercentagePerItem);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000C4CA0 File Offset: 0x000C2EA0
		private void Update()
		{
			if (NetworkServer.active)
			{
				if (!this.damageTrackingInitialized)
				{
					return;
				}
				if (this.damagePerItem <= 0f)
				{
					this.damageTrackingInitialized = false;
					Debug.LogError("ReturnStolenItemsOnGettingHit.damagePerItem is 0!");
					return;
				}
				while (this.accumulatedDamage > this.damagePerItem)
				{
					this.accumulatedDamage -= this.damagePerItem;
					bool flag = this.itemStealController.ReclaimItemForInventory(this.returnOrder[this.nextReturnIndex], int.MaxValue);
					this.nextReturnIndex = (this.nextReturnIndex + 1) % this.returnOrder.Count;
					int num = 0;
					while (!flag && num < this.returnOrder.Count - 1)
					{
						flag = this.itemStealController.ReclaimItemForInventory(this.returnOrder[this.nextReturnIndex], int.MaxValue);
						num++;
						this.nextReturnIndex = (this.nextReturnIndex + 1) % this.returnOrder.Count;
					}
					if (!flag)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000C4D9C File Offset: 0x000C2F9C
		private void OnDestroy()
		{
			if (this._itemStealController != null)
			{
				this._itemStealController.onLendingFinishServer.RemoveListener(new UnityAction(this.InitializeDamageTracking));
				this._itemStealController = null;
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000C4DC9 File Offset: 0x000C2FC9
		public void OnKilledServer(DamageReport damageReport)
		{
			this.itemStealController;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000C4DD8 File Offset: 0x000C2FD8
		private void InitializeDamageTracking()
		{
			this.returnOrder = new List<Inventory>();
			if (this.itemStealController)
			{
				int num = 0;
				List<Inventory> list = new List<Inventory>();
				this.itemStealController.AddValidStolenInventoriesToList(list);
				foreach (Inventory inventory in list)
				{
					if (!inventory.GetComponent<CharacterMaster>().minionOwnership.ownerMaster)
					{
						this.returnOrder.Add(inventory);
						num += this.itemStealController.GetStolenItemCount(inventory);
					}
				}
				float num2 = Mathf.Clamp(100f / (float)Math.Max(num, 1), this.minPercentagePerItem, this.maxPercentagePerItem) / 100f;
				this.damagePerItem = this.healthComponent.fullCombinedHealth * num2;
				this.accumulatedDamage += this.damagePerItem * this.initialPercentageToFirstItem / 100f;
				this._itemStealController.onLendingFinishServer.RemoveListener(new UnityAction(this.InitializeDamageTracking));
			}
			this.damageTrackingInitialized = true;
		}

		// Token: 0x0400302E RID: 12334
		public HealthComponent healthComponent;

		// Token: 0x0400302F RID: 12335
		[SerializeField]
		[Range(0.01f, 100f)]
		private float minPercentagePerItem;

		// Token: 0x04003030 RID: 12336
		[Range(0.01f, 100f)]
		[SerializeField]
		private float maxPercentagePerItem;

		// Token: 0x04003031 RID: 12337
		[SerializeField]
		[Range(0f, 100f)]
		private float initialPercentageToFirstItem;

		// Token: 0x04003032 RID: 12338
		private List<Inventory> returnOrder;

		// Token: 0x04003033 RID: 12339
		private int nextReturnIndex;

		// Token: 0x04003034 RID: 12340
		private float damagePerItem;

		// Token: 0x04003035 RID: 12341
		private float accumulatedDamage;

		// Token: 0x04003036 RID: 12342
		private ItemStealController _itemStealController;

		// Token: 0x04003037 RID: 12343
		private bool damageTrackingInitialized;
	}
}
