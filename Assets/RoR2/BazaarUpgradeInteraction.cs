using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005F2 RID: 1522
	public sealed class BazaarUpgradeInteraction : NetworkBehaviour, IInteractable, IHologramContentProvider, IDisplayNameProvider
	{
		// Token: 0x06001BB3 RID: 7091 RVA: 0x0007625C File Offset: 0x0007445C
		private void Awake()
		{
			this.unlockableProgressionDefs = new UnlockableDef[this.unlockableProgression.Length];
			for (int i = 0; i < this.unlockableProgressionDefs.Length; i++)
			{
				this.unlockableProgressionDefs[i] = UnlockableCatalog.GetUnlockableDef(this.unlockableProgression[i]);
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000762A4 File Offset: 0x000744A4
		private void FixedUpdate()
		{
			if (NetworkServer.active && !this.available)
			{
				this.activationTimer -= Time.fixedDeltaTime;
				if (this.activationTimer <= 0f)
				{
					this.Networkavailable = true;
				}
			}
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000762DC File Offset: 0x000744DC
		private UnlockableDef GetInteractorNextUnlockable(GameObject activatorGameObject)
		{
			NetworkUser networkUser = Util.LookUpBodyNetworkUser(activatorGameObject);
			if (networkUser)
			{
				LocalUser localUser = networkUser.localUser;
				if (localUser != null)
				{
					for (int i = 0; i < this.unlockableProgressionDefs.Length; i++)
					{
						UnlockableDef unlockableDef = this.unlockableProgressionDefs[i];
						if (!localUser.userProfile.HasUnlockable(unlockableDef))
						{
							return unlockableDef;
						}
					}
				}
				else
				{
					for (int j = 0; j < this.unlockableProgressionDefs.Length; j++)
					{
						UnlockableDef unlockableDef2 = this.unlockableProgressionDefs[j];
						if (!networkUser.unlockables.Contains(unlockableDef2))
						{
							return unlockableDef2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00076368 File Offset: 0x00074568
		private static bool ActivatorHasUnlockable(Interactor activator, string unlockableName)
		{
			NetworkUser networkUser = Util.LookUpBodyNetworkUser(activator.gameObject);
			if (!networkUser)
			{
				return true;
			}
			LocalUser localUser = networkUser.localUser;
			if (localUser != null)
			{
				return localUser.userProfile.HasUnlockable(unlockableName);
			}
			return networkUser.unlockables.Contains(UnlockableCatalog.GetUnlockableDef(unlockableName));
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000763B3 File Offset: 0x000745B3
		public string GetDisplayName()
		{
			return Language.GetString(this.displayNameToken);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000763C0 File Offset: 0x000745C0
		private string GetCostString()
		{
			return string.Format(" (<color=#{1}>{0}</color>)", this.cost, BazaarUpgradeInteraction.lunarCoinColorString);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000763DC File Offset: 0x000745DC
		public string GetContextString(Interactor activator)
		{
			if (!this.CanBeAffordedByInteractor(activator))
			{
				return null;
			}
			return Language.GetString(this.contextToken) + this.GetCostString();
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000763FF File Offset: 0x000745FF
		public Interactability GetInteractability(Interactor activator)
		{
			if (this.GetInteractorNextUnlockable(activator.gameObject) == null || !this.available)
			{
				return Interactability.Disabled;
			}
			if (!this.CanBeAffordedByInteractor(activator))
			{
				return Interactability.ConditionsNotMet;
			}
			return Interactability.Available;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnInteractionBegin(Interactor activator)
		{
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0007642B File Offset: 0x0007462B
		private int GetCostForInteractor(Interactor activator)
		{
			return this.cost;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00076434 File Offset: 0x00074634
		public bool CanBeAffordedByInteractor(Interactor activator)
		{
			NetworkUser networkUser = Util.LookUpBodyNetworkUser(activator.gameObject);
			return networkUser && (ulong)networkUser.lunarCoins >= (ulong)((long)this.GetCostForInteractor(activator));
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0007646B File Offset: 0x0007466B
		public bool ShouldDisplayHologram(GameObject viewer)
		{
			return this.GetInteractorNextUnlockable(viewer) != null;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0007647A File Offset: 0x0007467A
		public GameObject GetHologramContentPrefab()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/CostHologramContent");
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00076488 File Offset: 0x00074688
		public void UpdateHologramContent(GameObject hologramContentObject)
		{
			CostHologramContent component = hologramContentObject.GetComponent<CostHologramContent>();
			if (component)
			{
				component.displayValue = this.cost;
				component.costType = CostTypeIndex.LunarCoin;
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000764B7 File Offset: 0x000746B7
		private void OnEnable()
		{
			InstanceTracker.Add<BazaarUpgradeInteraction>(this);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000764BF File Offset: 0x000746BF
		private void OnDisable()
		{
			InstanceTracker.Remove<BazaarUpgradeInteraction>(this);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000764C7 File Offset: 0x000746C7
		public bool ShouldShowOnScanner()
		{
			return this.available;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00076518 File Offset: 0x00074718
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0007652B File Offset: 0x0007472B
		public bool Networkavailable
		{
			get
			{
				return this.available;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.available, 1U);
			}
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00076540 File Offset: 0x00074740
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.available);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.available);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000765AC File Offset: 0x000747AC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.available = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.available = reader.ReadBoolean();
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400218F RID: 8591
		[SyncVar]
		public bool available = true;

		// Token: 0x04002190 RID: 8592
		public string displayNameToken;

		// Token: 0x04002191 RID: 8593
		public int cost;

		// Token: 0x04002192 RID: 8594
		public string contextToken;

		// Token: 0x04002193 RID: 8595
		public string[] unlockableProgression;

		// Token: 0x04002194 RID: 8596
		private UnlockableDef[] unlockableProgressionDefs;

		// Token: 0x04002195 RID: 8597
		public float activationCooldownDuration = 1f;

		// Token: 0x04002196 RID: 8598
		private float activationTimer;

		// Token: 0x04002197 RID: 8599
		public GameObject purchaseEffect;

		// Token: 0x04002198 RID: 8600
		private static readonly Color32 lunarCoinColor = new Color32(198, 173, 250, byte.MaxValue);

		// Token: 0x04002199 RID: 8601
		private static readonly string lunarCoinColorString = Util.RGBToHex(BazaarUpgradeInteraction.lunarCoinColor);
	}
}
