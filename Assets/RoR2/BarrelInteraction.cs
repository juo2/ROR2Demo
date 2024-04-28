using System;
using System.Runtime.InteropServices;
using EntityStates.Barrel;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005EA RID: 1514
	public sealed class BarrelInteraction : NetworkBehaviour, IInteractable, IDisplayNameProvider
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x000756CD File Offset: 0x000738CD
		public string GetContextString(Interactor activator)
		{
			return Language.GetString(this.contextToken);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000756E6 File Offset: 0x000738E6
		public Interactability GetInteractability(Interactor activator)
		{
			if (this.opened)
			{
				return Interactability.Disabled;
			}
			return Interactability.Available;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000756F4 File Offset: 0x000738F4
		private void Start()
		{
			if (Run.instance)
			{
				this.goldReward = (int)((float)this.goldReward * Run.instance.difficultyCoefficient);
				this.expReward = (uint)(this.expReward * Run.instance.difficultyCoefficient);
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00075740 File Offset: 0x00073940
		[Server]
		public void OnInteractionBegin(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.BarrelInteraction::OnInteractionBegin(RoR2.Interactor)' called on client");
				return;
			}
			if (!this.opened)
			{
				this.Networkopened = true;
				EntityStateMachine component = base.GetComponent<EntityStateMachine>();
				if (component)
				{
					component.SetNextState(new Opening());
				}
				CharacterBody component2 = activator.GetComponent<CharacterBody>();
				if (component2)
				{
					TeamIndex objectTeam = TeamComponent.GetObjectTeam(component2.gameObject);
					TeamManager.instance.GiveTeamMoney(objectTeam, (uint)this.goldReward);
				}
				this.CoinDrop();
				ExperienceManager.instance.AwardExperience(base.transform.position, activator.GetComponent<CharacterBody>(), (ulong)this.expReward);
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000757E0 File Offset: 0x000739E0
		[Server]
		private void CoinDrop()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.BarrelInteraction::CoinDrop()' called on client");
				return;
			}
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/CoinEmitter"), new EffectData
			{
				origin = base.transform.position,
				genericFloat = (float)this.goldReward
			}, true);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00075835 File Offset: 0x00073A35
		public string GetDisplayName()
		{
			return Language.GetString(this.displayNameToken);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00075842 File Offset: 0x00073A42
		public void OnEnable()
		{
			InstanceTracker.Add<BarrelInteraction>(this);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0007584A File Offset: 0x00073A4A
		public void OnDisable()
		{
			InstanceTracker.Remove<BarrelInteraction>(this);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00075852 File Offset: 0x00073A52
		public bool ShouldShowOnScanner()
		{
			return !this.opened;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00075870 File Offset: 0x00073A70
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x00075883 File Offset: 0x00073A83
		public bool Networkopened
		{
			get
			{
				return this.opened;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.opened, 1U);
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00075898 File Offset: 0x00073A98
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.opened);
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
				writer.Write(this.opened);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00075904 File Offset: 0x00073B04
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.opened = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.opened = reader.ReadBoolean();
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002170 RID: 8560
		public int goldReward;

		// Token: 0x04002171 RID: 8561
		public uint expReward;

		// Token: 0x04002172 RID: 8562
		public string displayNameToken = "BARREL1_NAME";

		// Token: 0x04002173 RID: 8563
		public string contextToken;

		// Token: 0x04002174 RID: 8564
		[SyncVar]
		private bool opened;
	}
}
