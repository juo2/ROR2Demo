using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using UnityEngine;

namespace RoR2.SurvivorMannequins
{
	// Token: 0x02000B70 RID: 2928
	public class SurvivorMannequinDioramaController : MonoBehaviour
	{
		// Token: 0x060042AB RID: 17067 RVA: 0x00114357 File Offset: 0x00112557
		private void OnEnable()
		{
			NetworkUser.onNetworkUserDiscovered += this.OnNetworkUserDiscovered;
			NetworkUser.onNetworkUserLost += this.OnNetworkUserLost;
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0011437B File Offset: 0x0011257B
		private void OnDisable()
		{
			NetworkUser.onNetworkUserLost -= this.OnNetworkUserLost;
			NetworkUser.onNetworkUserDiscovered -= this.OnNetworkUserDiscovered;
			this.sortedNetworkUsers.Clear();
			this.UpdateMannequins();
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x001143B0 File Offset: 0x001125B0
		private void Update()
		{
			if (this.sortedNetworkUsersDirty)
			{
				this.sortedNetworkUsersDirty = false;
				this.UpdateSortedNetworkUsersList();
			}
			this.UpdateMannequins();
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x001143D0 File Offset: 0x001125D0
		private void UpdateSortedNetworkUsersList()
		{
			this.sortedNetworkUsers.Clear();
			if (this.showLocalPlayersFirst)
			{
				ListUtils.AddRange<NetworkUser, ReadOnlyCollection<NetworkUser>>(this.sortedNetworkUsers, NetworkUser.readOnlyLocalPlayersList);
				for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
				{
					ListUtils.AddIfUnique<NetworkUser>(this.sortedNetworkUsers, NetworkUser.readOnlyInstancesList[i]);
				}
			}
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x0011442B File Offset: 0x0011262B
		private void OnNetworkUserLost(NetworkUser networkUser)
		{
			this.sortedNetworkUsersDirty = true;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0011442B File Offset: 0x0011262B
		private void OnNetworkUserDiscovered(NetworkUser networkUser)
		{
			this.sortedNetworkUsersDirty = true;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x00114434 File Offset: 0x00112634
		public void GetSlots(List<SurvivorMannequinSlotController> dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			ListUtils.AddRange<SurvivorMannequinSlotController>(dest, this.mannequinSlots);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x00114450 File Offset: 0x00112650
		public void SetSlots(SurvivorMannequinSlotController[] newMannequinSlots)
		{
			if (newMannequinSlots == null)
			{
				throw new ArgumentNullException("newMannequinSlots");
			}
			for (int i = 0; i < this.mannequinSlots.Length; i++)
			{
				SurvivorMannequinSlotController survivorMannequinSlotController = this.mannequinSlots[i];
				if (survivorMannequinSlotController && Array.IndexOf<SurvivorMannequinSlotController>(newMannequinSlots, survivorMannequinSlotController) == -1)
				{
					survivorMannequinSlotController.networkUser = null;
				}
			}
			ArrayUtils.CloneTo<SurvivorMannequinSlotController>(newMannequinSlots, ref this.mannequinSlots);
			this.UpdateMannequins();
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x001144B4 File Offset: 0x001126B4
		private void UpdateMannequins()
		{
			this.AssignNetworkUsersToSlots(this.sortedNetworkUsers);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x001144C4 File Offset: 0x001126C4
		private void AssignNetworkUsersToSlots(List<NetworkUser> networkUsers)
		{
			int i = 0;
			int num = Math.Min(networkUsers.Count, this.mannequinSlots.Length);
			while (i < num)
			{
				NetworkUser networkUser = networkUsers[i];
				SurvivorMannequinSlotController survivorMannequinSlotController = this.mannequinSlots[i];
				if (survivorMannequinSlotController && survivorMannequinSlotController.networkUser != networkUser)
				{
					for (int j = i + 1; j < this.mannequinSlots.Length; j++)
					{
						SurvivorMannequinSlotController survivorMannequinSlotController2 = this.mannequinSlots[j];
						if (survivorMannequinSlotController2)
						{
							SurvivorMannequinSlotController.Swap(survivorMannequinSlotController, survivorMannequinSlotController2);
							break;
						}
					}
					survivorMannequinSlotController.networkUser = networkUser;
				}
				i++;
			}
			for (int k = networkUsers.Count; k < this.mannequinSlots.Length; k++)
			{
				this.mannequinSlots[k].networkUser = null;
			}
		}

		// Token: 0x040040A4 RID: 16548
		public bool showLocalPlayersFirst = true;

		// Token: 0x040040A5 RID: 16549
		[SerializeField]
		private SurvivorMannequinSlotController[] mannequinSlots = Array.Empty<SurvivorMannequinSlotController>();

		// Token: 0x040040A6 RID: 16550
		private bool sortedNetworkUsersDirty = true;

		// Token: 0x040040A7 RID: 16551
		private List<NetworkUser> sortedNetworkUsers = new List<NetworkUser>();
	}
}
