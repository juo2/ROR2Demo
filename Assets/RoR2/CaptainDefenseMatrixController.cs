using System;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000619 RID: 1561
	public class CaptainDefenseMatrixController : MonoBehaviour
	{
		// Token: 0x06001CB7 RID: 7351 RVA: 0x0007A414 File Offset: 0x00078614
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0007A422 File Offset: 0x00078622
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.TryGrantItem();
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0007A431 File Offset: 0x00078631
		private void OnEnable()
		{
			if (NetworkServer.active)
			{
				MasterSummon.onServerMasterSummonGlobal += this.OnServerMasterSummonGlobal;
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0007A44B File Offset: 0x0007864B
		private void OnDisable()
		{
			if (NetworkServer.active)
			{
				MasterSummon.onServerMasterSummonGlobal -= this.OnServerMasterSummonGlobal;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0007A468 File Offset: 0x00078668
		private void TryGrantItem()
		{
			if (this.characterBody.master)
			{
				bool flag = false;
				if (this.characterBody.master.playerStatsComponent)
				{
					flag = (this.characterBody.master.playerStatsComponent.currentStats.GetStatValueDouble(PerBodyStatDef.totalTimeAlive, BodyCatalog.GetBodyName(this.characterBody.bodyIndex)) > 0.0);
				}
				if (!flag && this.characterBody.master.inventory.GetItemCount(RoR2Content.Items.CaptainDefenseMatrix) <= 0)
				{
					this.characterBody.master.inventory.GiveItem(RoR2Content.Items.CaptainDefenseMatrix, this.defenseMatrixToGrantPlayer);
				}
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0007A520 File Offset: 0x00078720
		private void OnServerMasterSummonGlobal(MasterSummon.MasterSummonReport summonReport)
		{
			if (this.characterBody.master && this.characterBody.master == summonReport.leaderMasterInstance)
			{
				CharacterMaster summonMasterInstance = summonReport.summonMasterInstance;
				if (summonMasterInstance)
				{
					CharacterBody body = summonMasterInstance.GetBody();
					if (body && (body.bodyFlags & CharacterBody.BodyFlags.Mechanical) > CharacterBody.BodyFlags.None)
					{
						summonMasterInstance.inventory.GiveItem(RoR2Content.Items.CaptainDefenseMatrix, this.defenseMatrixToGrantMechanicalAllies);
					}
				}
			}
		}

		// Token: 0x04002293 RID: 8851
		public int defenseMatrixToGrantPlayer = 1;

		// Token: 0x04002294 RID: 8852
		public int defenseMatrixToGrantMechanicalAllies = 1;

		// Token: 0x04002295 RID: 8853
		private CharacterBody characterBody;
	}
}
