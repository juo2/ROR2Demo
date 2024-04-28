using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000442 RID: 1090
	public class SpellBaseState : BaseState
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool DisplayWeapon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00056DF8 File Offset: 0x00054FF8
		public override void OnEnter()
		{
			base.OnEnter();
			this.FindItemStealer();
			if (NetworkServer.active && !this.itemStealController)
			{
				this.InitItemStealer();
			}
			this.hammerRendererObject = base.FindModelChild("HammerRenderer").gameObject;
			if (this.hammerRendererObject)
			{
				this.hammerRendererObject.SetActive(this.DisplayWeapon);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00056E5F File Offset: 0x0005505F
		public override void OnExit()
		{
			if (this.hammerRendererObject)
			{
				this.hammerRendererObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00056E80 File Offset: 0x00055080
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.FindItemStealer();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00056E90 File Offset: 0x00055090
		private void FindItemStealer()
		{
			if (!this.itemStealController)
			{
				List<NetworkedBodyAttachment> list = new List<NetworkedBodyAttachment>();
				NetworkedBodyAttachment.FindBodyAttachments(base.characterBody, list);
				foreach (NetworkedBodyAttachment networkedBodyAttachment in list)
				{
					this.itemStealController = networkedBodyAttachment.GetComponent<ItemStealController>();
					if (this.itemStealController)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00056F14 File Offset: 0x00055114
		private void InitItemStealer()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (this.itemStealController == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/ItemStealController"), base.transform.position, Quaternion.identity);
				this.itemStealController = gameObject.GetComponent<ItemStealController>();
				this.itemStealController.itemLendFilter = new Func<ItemIndex, bool>(ItemStealController.BrotherItemFilter);
				gameObject.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
				base.gameObject.GetComponent<ReturnStolenItemsOnGettingHit>().itemStealController = this.itemStealController;
				NetworkServer.Spawn(gameObject);
			}
		}

		// Token: 0x040018F8 RID: 6392
		protected ItemStealController itemStealController;

		// Token: 0x040018F9 RID: 6393
		protected GameObject hammerRendererObject;
	}
}
