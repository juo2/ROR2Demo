using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005F6 RID: 1526
	public class BlueprintTerminal : NetworkBehaviour
	{
		// Token: 0x06001BE2 RID: 7138 RVA: 0x00076AFE File Offset: 0x00074CFE
		private void SetHasBeenPurchased(bool newHasBeenPurchased)
		{
			if (this.hasBeenPurchased != newHasBeenPurchased)
			{
				this.NetworkhasBeenPurchased = newHasBeenPurchased;
				this.Rebuild();
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00076B16 File Offset: 0x00074D16
		public void Start()
		{
			if (NetworkServer.active)
			{
				this.RollChoice();
			}
			if (NetworkClient.active)
			{
				this.Rebuild();
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00076B34 File Offset: 0x00074D34
		private void RollChoice()
		{
			WeightedSelection<int> weightedSelection = new WeightedSelection<int>(8);
			for (int i = 0; i < this.unlockableOptions.Length; i++)
			{
				weightedSelection.AddChoice(i, this.unlockableOptions[i].weight);
			}
			this.unlockableChoice = weightedSelection.Evaluate(UnityEngine.Random.value);
			this.Rebuild();
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00076B8C File Offset: 0x00074D8C
		private void Rebuild()
		{
			BlueprintTerminal.UnlockableOption unlockableOption = this.unlockableOptions[this.unlockableChoice];
			if (this.displayInstance)
			{
				UnityEngine.Object.Destroy(this.displayInstance);
			}
			this.displayBaseTransform.gameObject.SetActive(!this.hasBeenPurchased);
			if (!this.hasBeenPurchased && this.displayBaseTransform)
			{
				Debug.Log("Found base");
				UnlockableDef resolvedUnlockableDef = unlockableOption.GetResolvedUnlockableDef();
				if (resolvedUnlockableDef)
				{
					Debug.Log("Found unlockable");
					GameObject displayModelPrefab = resolvedUnlockableDef.displayModelPrefab;
					if (displayModelPrefab)
					{
						Debug.Log("Found prefab");
						this.displayInstance = UnityEngine.Object.Instantiate<GameObject>(displayModelPrefab, this.displayBaseTransform.position, this.displayBaseTransform.transform.rotation, this.displayBaseTransform);
						Renderer componentInChildren = this.displayInstance.GetComponentInChildren<Renderer>();
						float num = 1f;
						if (componentInChildren)
						{
							this.displayInstance.transform.rotation = Quaternion.identity;
							Vector3 size = componentInChildren.bounds.size;
							float f = size.x * size.y * size.z;
							num *= Mathf.Pow(this.idealDisplayVolume, 0.33333334f) / Mathf.Pow(f, 0.33333334f);
						}
						this.displayInstance.transform.localScale = new Vector3(num, num, num);
					}
				}
			}
			PurchaseInteraction component = base.GetComponent<PurchaseInteraction>();
			if (component)
			{
				component.Networkcost = unlockableOption.cost;
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00076D1C File Offset: 0x00074F1C
		[Server]
		public void GrantUnlock(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.BlueprintTerminal::GrantUnlock(RoR2.Interactor)' called on client");
				return;
			}
			this.SetHasBeenPurchased(true);
			BlueprintTerminal.UnlockableOption unlockableOption = this.unlockableOptions[this.unlockableChoice];
			UnlockableDef resolvedUnlockableDef = unlockableOption.GetResolvedUnlockableDef();
			EffectManager.SpawnEffect(this.unlockEffect, new EffectData
			{
				origin = base.transform.position
			}, true);
			if (Run.instance)
			{
				Util.PlaySound(this.unlockSoundString, interactor.gameObject);
				CharacterBody component = interactor.GetComponent<CharacterBody>();
				Run.instance.GrantUnlockToSinglePlayer(resolvedUnlockableDef, component);
				string pickupToken = "???";
				if (resolvedUnlockableDef != null)
				{
					pickupToken = resolvedUnlockableDef.nameToken;
				}
				Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
				{
					subjectAsCharacterBody = component,
					baseToken = "PLAYER_PICKUP",
					pickupToken = pickupToken,
					pickupColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unlockable),
					pickupQuantity = 1U
				});
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00076E14 File Offset: 0x00075014
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x00076E27 File Offset: 0x00075027
		public bool NetworkhasBeenPurchased
		{
			get
			{
				return this.hasBeenPurchased;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetHasBeenPurchased(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this.hasBeenPurchased, 1U);
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00076E68 File Offset: 0x00075068
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.hasBeenPurchased);
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
				writer.Write(this.hasBeenPurchased);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00076ED4 File Offset: 0x000750D4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.hasBeenPurchased = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetHasBeenPurchased(reader.ReadBoolean());
			}
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040021AD RID: 8621
		[SyncVar(hook = "SetHasBeenPurchased")]
		private bool hasBeenPurchased;

		// Token: 0x040021AE RID: 8622
		public Transform displayBaseTransform;

		// Token: 0x040021AF RID: 8623
		[Tooltip("The unlockables to grant")]
		public BlueprintTerminal.UnlockableOption[] unlockableOptions;

		// Token: 0x040021B0 RID: 8624
		private int unlockableChoice;

		// Token: 0x040021B1 RID: 8625
		public string unlockSoundString;

		// Token: 0x040021B2 RID: 8626
		public float idealDisplayVolume = 1.5f;

		// Token: 0x040021B3 RID: 8627
		public GameObject unlockEffect;

		// Token: 0x040021B4 RID: 8628
		private GameObject displayInstance;

		// Token: 0x020005F7 RID: 1527
		[Serializable]
		public struct UnlockableOption
		{
			// Token: 0x06001BEE RID: 7150 RVA: 0x00076F18 File Offset: 0x00075118
			public UnlockableDef GetResolvedUnlockableDef()
			{
				string text = this.unlockableName;
				if (!this.unlockableDef && !string.IsNullOrEmpty(text))
				{
					this.unlockableDef = UnlockableCatalog.GetUnlockableDef(text);
				}
				return this.unlockableDef;
			}

			// Token: 0x040021B5 RID: 8629
			[Obsolete("'unlockableName' will be discontinued. Use 'unlockableDef' instead.", false)]
			[Tooltip("'unlockableName' will be discontinued. Use 'unlockableDef' instead.")]
			public string unlockableName;

			// Token: 0x040021B6 RID: 8630
			public UnlockableDef unlockableDef;

			// Token: 0x040021B7 RID: 8631
			public int cost;

			// Token: 0x040021B8 RID: 8632
			public float weight;
		}
	}
}
