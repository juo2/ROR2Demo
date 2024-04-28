using System;
using System.Runtime.InteropServices;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000818 RID: 2072
	public class PortalSpawner : NetworkBehaviour
	{
		// Token: 0x06002CF1 RID: 11505 RVA: 0x000BFAB0 File Offset: 0x000BDCB0
		private void Start()
		{
			if (this.modelChildLocator)
			{
				Transform transform = this.modelChildLocator.FindChild(this.previewChildName);
				if (transform)
				{
					this.previewChild = transform.gameObject;
				}
			}
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
				bool flag = !this.requiredExpansion || Run.instance.IsExpansionEnabled(this.requiredExpansion);
				bool flag2 = Run.instance.stageClearCount >= this.minStagesCleared;
				if ((string.IsNullOrEmpty(this.bannedEventFlag) || !Run.instance.GetEventFlag(this.bannedEventFlag)) && this.rng.nextNormalizedFloat <= this.spawnChance && flag && flag2)
				{
					this.NetworkwillSpawn = true;
					if (!string.IsNullOrEmpty(this.spawnPreviewMessageToken))
					{
						Chat.SendBroadcastChat(new Chat.SimpleChatMessage
						{
							baseToken = this.spawnPreviewMessageToken
						});
					}
					if (this.previewChild)
					{
						this.previewChild.SetActive(true);
					}
				}
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000BFBD4 File Offset: 0x000BDDD4
		[Server]
		public bool AttemptSpawnPortalServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.PortalSpawner::AttemptSpawnPortalServer()' called on client");
				return false;
			}
			if (this.willSpawn)
			{
				if (this.previewChild)
				{
					this.previewChild.SetActive(false);
				}
				this.NetworkwillSpawn = false;
				DirectorPlacementRule.PlacementMode placementMode = DirectorPlacementRule.PlacementMode.Approximate;
				if (this.maxSpawnDistance <= 0f)
				{
					placementMode = DirectorPlacementRule.PlacementMode.Direct;
				}
				Transform transform = this.spawnReferenceLocation;
				if (!transform)
				{
					transform = base.transform;
				}
				GameObject exists = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.portalSpawnCard, new DirectorPlacementRule
				{
					minDistance = this.minSpawnDistance,
					maxDistance = this.maxSpawnDistance,
					placementMode = placementMode,
					position = base.transform.position,
					spawnOnTarget = transform
				}, this.rng));
				if (exists && !string.IsNullOrEmpty(this.spawnMessageToken))
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = this.spawnMessageToken
					});
				}
				return exists;
			}
			return false;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000BFCD2 File Offset: 0x000BDED2
		private void OnWillSpawnUpdated(bool newValue)
		{
			this.NetworkwillSpawn = newValue;
			if (this.previewChild)
			{
				this.previewChild.SetActive(newValue);
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000BFCF4 File Offset: 0x000BDEF4
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x000BFD07 File Offset: 0x000BDF07
		public bool NetworkwillSpawn
		{
			get
			{
				return this.willSpawn;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnWillSpawnUpdated(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this.willSpawn, 1U);
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000BFD48 File Offset: 0x000BDF48
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.willSpawn);
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
				writer.Write(this.willSpawn);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000BFDB4 File Offset: 0x000BDFB4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.willSpawn = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnWillSpawnUpdated(reader.ReadBoolean());
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002F12 RID: 12050
		[SerializeField]
		private InteractableSpawnCard portalSpawnCard;

		// Token: 0x04002F13 RID: 12051
		[SerializeField]
		[Range(0f, 1f)]
		private float spawnChance;

		// Token: 0x04002F14 RID: 12052
		[Tooltip("The portal is spawned relative to this transform.  If null, it uses this object's transform for reference.")]
		[SerializeField]
		private Transform spawnReferenceLocation;

		// Token: 0x04002F15 RID: 12053
		[SerializeField]
		private float minSpawnDistance;

		// Token: 0x04002F16 RID: 12054
		[Tooltip("The maximum spawn distance for the portal relative to the spawnReferenceLocation.  If 0, it will be spawned at exactly the referenced location.")]
		[SerializeField]
		private float maxSpawnDistance;

		// Token: 0x04002F17 RID: 12055
		[SerializeField]
		private string spawnPreviewMessageToken;

		// Token: 0x04002F18 RID: 12056
		[SerializeField]
		private string spawnMessageToken;

		// Token: 0x04002F19 RID: 12057
		[SerializeField]
		private ChildLocator modelChildLocator;

		// Token: 0x04002F1A RID: 12058
		[SerializeField]
		private string previewChildName;

		// Token: 0x04002F1B RID: 12059
		[SerializeField]
		private ExpansionDef requiredExpansion;

		// Token: 0x04002F1C RID: 12060
		[SerializeField]
		private int minStagesCleared;

		// Token: 0x04002F1D RID: 12061
		[SerializeField]
		private string bannedEventFlag;

		// Token: 0x04002F1E RID: 12062
		private Xoroshiro128Plus rng;

		// Token: 0x04002F1F RID: 12063
		private GameObject previewChild;

		// Token: 0x04002F20 RID: 12064
		[SyncVar(hook = "OnWillSpawnUpdated")]
		private bool willSpawn;
	}
}
