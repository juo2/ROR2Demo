using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006BD RID: 1725
	[RequireComponent(typeof(TeamFilter))]
	[RequireComponent(typeof(Inventory))]
	public class EnemyInfoPanelInventoryProvider : MonoBehaviour
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x00090850 File Offset: 0x0008EA50
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x00090858 File Offset: 0x0008EA58
		public Inventory inventory { get; private set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x00090861 File Offset: 0x0008EA61
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x00090869 File Offset: 0x0008EA69
		public TeamFilter teamFilter { get; private set; }

		// Token: 0x06002190 RID: 8592 RVA: 0x00090872 File Offset: 0x0008EA72
		private void Awake()
		{
			this.inventory = base.GetComponent<Inventory>();
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.inventory.onInventoryChanged += this.OnInventoryChanged;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000908A3 File Offset: 0x0008EAA3
		private void OnInventoryChanged()
		{
			this.MarkAsDirty();
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000908AB File Offset: 0x0008EAAB
		private void OnEnable()
		{
			InstanceTracker.Add<EnemyInfoPanelInventoryProvider>(this);
			this.MarkAsDirty();
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000908B9 File Offset: 0x0008EAB9
		private void OnDisable()
		{
			this.MarkAsDirty();
			InstanceTracker.Remove<EnemyInfoPanelInventoryProvider>(this);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000908C7 File Offset: 0x0008EAC7
		private void MarkAsDirty()
		{
			if (EnemyInfoPanelInventoryProvider.isDirty)
			{
				return;
			}
			EnemyInfoPanelInventoryProvider.isDirty = true;
			RoR2Application.onNextUpdate += this.Refresh;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000908E8 File Offset: 0x0008EAE8
		private void Refresh()
		{
			Action action = EnemyInfoPanelInventoryProvider.onInventoriesChanged;
			if (action != null)
			{
				action();
			}
			EnemyInfoPanelInventoryProvider.isDirty = false;
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002196 RID: 8598 RVA: 0x00090900 File Offset: 0x0008EB00
		// (remove) Token: 0x06002197 RID: 8599 RVA: 0x00090934 File Offset: 0x0008EB34
		public static event Action onInventoriesChanged;

		// Token: 0x040026FC RID: 9980
		private static bool isDirty;
	}
}
