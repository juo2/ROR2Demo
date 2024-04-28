using System;
using System.Security.Cryptography;
using System.Text;
using EntityStates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200080F RID: 2063
	public class PortalDialerController : NetworkBehaviour
	{
		// Token: 0x06002CCA RID: 11466 RVA: 0x000BF34D File Offset: 0x000BD54D
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.hasher = SHA256.Create();
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000BF361 File Offset: 0x000BD561
		private void OnDestroy()
		{
			if (this.hasher != null)
			{
				this.hasher.Dispose();
				this.hasher = null;
			}
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000BF380 File Offset: 0x000BD580
		private byte[] GetSequence()
		{
			byte[] array = new byte[this.buttons.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)this.buttons[i].currentDigitDef.value;
			}
			return array;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000BF3C0 File Offset: 0x000BD5C0
		private Sha256Hash GetResult(byte[] sequence)
		{
			this.hasher.Initialize();
			return Sha256Hash.FromBytes(this.hasher.ComputeHash(sequence), 0);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000BF3E0 File Offset: 0x000BD5E0
		[Server]
		public bool PerformActionServer(byte[] sequence)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.PortalDialerController::PerformActionServer(System.Byte[])' called on client");
				return false;
			}
			Sha256Hash result = this.GetResult(sequence);
			Debug.LogFormat("Performing action for sequence={0} hash={1}", new object[]
			{
				sequence,
				result.ToString()
			});
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (result.Equals(this.actions[i].hashAsset.value))
				{
					UnityEvent action = this.actions[i].action;
					if (action != null)
					{
						action.Invoke();
					}
					return true;
				}
			}
			Debug.LogFormat("Could not find action to perform for {0}", new object[]
			{
				result.ToString()
			});
			return false;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000BF4A0 File Offset: 0x000BD6A0
		private static string SequenceToString(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(bytes[0]);
			for (int i = 1; i < bytes.Length; i++)
			{
				stringBuilder.Append(":");
				stringBuilder.Append(bytes[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000BF4E8 File Offset: 0x000BD6E8
		public void PrintResult()
		{
			Debug.Log(this.GetResult(this.GetSequence()).ToString());
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000BF514 File Offset: 0x000BD714
		[Server]
		public void OpenArtifactPortalServer(ArtifactDef artifactDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PortalDialerController::OpenArtifactPortalServer(RoR2.ArtifactDef)' called on client");
				return;
			}
			ArtifactTrialMissionController.trialArtifact = artifactDef;
			this.SpawnPortalServer(SceneCatalog.GetSceneDefFromSceneName("artifactworld"));
			base.GetComponent<PurchaseInteraction>().SetAvailable(false);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000BF550 File Offset: 0x000BD750
		[Server]
		public void SpawnPortalServer(SceneDef destination)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PortalDialerController::SpawnPortalServer(RoR2.SceneDef)' called on client");
				return;
			}
			GameObject original = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/PortalArtifactworld");
			if (destination.preferredPortalPrefab)
			{
				original = destination.preferredPortalPrefab;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, this.portalSpawnLocation.position, this.portalSpawnLocation.rotation);
			SceneExitController component = gameObject.GetComponent<SceneExitController>();
			component.useRunNextStageScene = false;
			component.destinationScene = destination;
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000BF5C8 File Offset: 0x000BD7C8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002EF8 RID: 12024
		public PortalDialerButtonController[] buttons;

		// Token: 0x04002EF9 RID: 12025
		public PortalDialerButtonController[] dialingOrder;

		// Token: 0x04002EFA RID: 12026
		public PortalDialerController.DialedAction[] actions;

		// Token: 0x04002EFB RID: 12027
		public Transform portalSpawnLocation;

		// Token: 0x04002EFC RID: 12028
		private SHA256 hasher;

		// Token: 0x04002EFD RID: 12029
		private byte[] dialedNumber;

		// Token: 0x02000810 RID: 2064
		[Serializable]
		public struct DialedAction
		{
			// Token: 0x04002EFE RID: 12030
			public Sha256HashAsset hashAsset;

			// Token: 0x04002EFF RID: 12031
			public UnityEvent action;
		}

		// Token: 0x02000811 RID: 2065
		private class PortalDialerBaseState : BaseState
		{
			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000BF5D6 File Offset: 0x000BD7D6
			// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x000BF5DE File Offset: 0x000BD7DE
			private protected PortalDialerController portalDialer { protected get; private set; }

			// Token: 0x06002CDA RID: 11482 RVA: 0x000BF5E7 File Offset: 0x000BD7E7
			public override void OnEnter()
			{
				base.OnEnter();
				this.portalDialer = base.GetComponent<PortalDialerController>();
			}
		}

		// Token: 0x02000812 RID: 2066
		private class PortalDialerIdleState : PortalDialerController.PortalDialerBaseState
		{
			// Token: 0x06002CDC RID: 11484 RVA: 0x000BF5FC File Offset: 0x000BD7FC
			public override void OnEnter()
			{
				base.OnEnter();
				PortalDialerButtonController[] buttons = base.portalDialer.buttons;
				for (int i = 0; i < buttons.Length; i++)
				{
					buttons[i].GetComponent<GenericInteraction>().Networkinteractability = Interactability.Available;
				}
				this.dialerInteraction = base.portalDialer.GetComponent<PurchaseInteraction>();
				if (NetworkServer.active)
				{
					this.dialerInteraction.SetAvailable(true);
					this.dialerInteraction.onPurchase.AddListener(new UnityAction<Interactor>(this.OnActivationServer));
					if (Run.instance.GetEventFlag("NoArtifactWorld"))
					{
						this.outer.SetNextState(new PortalDialerController.PortalDialerDisabledState());
					}
				}
			}

			// Token: 0x06002CDD RID: 11485 RVA: 0x000BF698 File Offset: 0x000BD898
			private void OnActivationServer(Interactor interactor)
			{
				this.outer.SetNextState(new PortalDialerController.PortalDialerPreDialState());
			}

			// Token: 0x06002CDE RID: 11486 RVA: 0x000BF6AC File Offset: 0x000BD8AC
			public override void OnExit()
			{
				if (NetworkServer.active)
				{
					this.dialerInteraction.SetAvailable(false);
					this.dialerInteraction.onPurchase.RemoveListener(new UnityAction<Interactor>(this.OnActivationServer));
				}
				foreach (PortalDialerButtonController portalDialerButtonController in base.portalDialer.buttons)
				{
					if (portalDialerButtonController)
					{
						portalDialerButtonController.GetComponent<GenericInteraction>().Networkinteractability = Interactability.Disabled;
					}
				}
				base.OnExit();
			}

			// Token: 0x04002F01 RID: 12033
			private PurchaseInteraction dialerInteraction;
		}

		// Token: 0x02000813 RID: 2067
		private class PortalDialerDisabledState : PortalDialerController.PortalDialerBaseState
		{
			// Token: 0x06002CE0 RID: 11488 RVA: 0x000BF728 File Offset: 0x000BD928
			public override void OnEnter()
			{
				base.OnEnter();
				PortalDialerButtonController[] buttons = base.portalDialer.buttons;
				for (int i = 0; i < buttons.Length; i++)
				{
					buttons[i].GetComponent<GenericInteraction>().Networkinteractability = Interactability.Disabled;
				}
				this.dialerInteraction = base.portalDialer.GetComponent<PurchaseInteraction>();
				if (NetworkServer.active)
				{
					this.dialerInteraction.SetAvailable(false);
				}
			}

			// Token: 0x04002F02 RID: 12034
			private PurchaseInteraction dialerInteraction;
		}

		// Token: 0x02000814 RID: 2068
		private class PortalDialerPreDialState : PortalDialerController.PortalDialerBaseState
		{
			// Token: 0x06002CE2 RID: 11490 RVA: 0x000BF788 File Offset: 0x000BD988
			public override void OnEnter()
			{
				base.OnEnter();
				this.duration = PortalDialerController.PortalDialerPreDialState.baseDuration;
				if (NetworkServer.active)
				{
					this.sequenceServer = new byte[base.portalDialer.buttons.Length];
				}
				if (PortalDialerController.PortalDialerPreDialState.portalPrespawnEffect)
				{
					EffectManager.SimpleEffect(PortalDialerController.PortalDialerPreDialState.portalPrespawnEffect, base.portalDialer.portalSpawnLocation.position, base.portalDialer.portalSpawnLocation.rotation, false);
				}
				Util.PlaySound("Play_env_hiddenLab_laptop_activate", base.gameObject);
			}

			// Token: 0x06002CE3 RID: 11491 RVA: 0x000BF80D File Offset: 0x000BDA0D
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= this.duration)
				{
					this.outer.SetNextState(new PortalDialerController.PortalDialerDialDigitState
					{
						currentDigit = 0,
						sequenceServer = this.sequenceServer
					});
				}
			}

			// Token: 0x04002F03 RID: 12035
			public static float baseDuration;

			// Token: 0x04002F04 RID: 12036
			public static GameObject portalPrespawnEffect;

			// Token: 0x04002F05 RID: 12037
			private float duration;

			// Token: 0x04002F06 RID: 12038
			private byte[] sequenceServer;
		}

		// Token: 0x02000815 RID: 2069
		private class PortalDialerDialDigitState : PortalDialerController.PortalDialerBaseState
		{
			// Token: 0x06002CE5 RID: 11493 RVA: 0x000BF850 File Offset: 0x000BDA50
			public override void OnEnter()
			{
				base.OnEnter();
				this.duration = PortalDialerController.PortalDialerDialDigitState.baseDuration;
				PortalDialerButtonController portalDialerButtonController = base.portalDialer.dialingOrder[this.currentDigit];
				if (NetworkServer.active)
				{
					this.sequenceServer[Array.IndexOf<PortalDialerButtonController>(base.portalDialer.buttons, portalDialerButtonController)] = (byte)portalDialerButtonController.currentDigitDef.value;
					if (portalDialerButtonController)
					{
						portalDialerButtonController.NetworkcurrentDigitIndex = 0;
					}
				}
				Util.PlaySound("Play_env_hiddenLab_laptop_sequence_lock", base.gameObject);
			}

			// Token: 0x06002CE6 RID: 11494 RVA: 0x0000EC55 File Offset: 0x0000CE55
			public override void OnExit()
			{
				base.OnExit();
			}

			// Token: 0x06002CE7 RID: 11495 RVA: 0x000BF8CC File Offset: 0x000BDACC
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= this.duration)
				{
					if (this.currentDigit >= base.portalDialer.buttons.Length - 1)
					{
						Debug.LogFormat("Submitting sequence {0}", new object[]
						{
							PortalDialerController.SequenceToString(this.sequenceServer)
						});
						this.outer.SetNextState(new PortalDialerController.PortalDialerPostDialState
						{
							sequenceServer = this.sequenceServer
						});
						return;
					}
					this.outer.SetNextState(new PortalDialerController.PortalDialerDialDigitState
					{
						currentDigit = this.currentDigit + 1,
						sequenceServer = this.sequenceServer
					});
				}
			}

			// Token: 0x06002CE8 RID: 11496 RVA: 0x000BF973 File Offset: 0x000BDB73
			public override void OnSerialize(NetworkWriter writer)
			{
				base.OnSerialize(writer);
				writer.Write((byte)this.currentDigit);
			}

			// Token: 0x06002CE9 RID: 11497 RVA: 0x000BF989 File Offset: 0x000BDB89
			public override void OnDeserialize(NetworkReader reader)
			{
				base.OnDeserialize(reader);
				this.currentDigit = (int)reader.ReadByte();
			}

			// Token: 0x04002F07 RID: 12039
			public static float baseDuration;

			// Token: 0x04002F08 RID: 12040
			public int currentDigit;

			// Token: 0x04002F09 RID: 12041
			private float duration;

			// Token: 0x04002F0A RID: 12042
			public byte[] sequenceServer;
		}

		// Token: 0x02000816 RID: 2070
		private class PortalDialerPostDialState : PortalDialerController.PortalDialerBaseState
		{
			// Token: 0x06002CEB RID: 11499 RVA: 0x000BF9A0 File Offset: 0x000BDBA0
			public override void OnEnter()
			{
				base.OnEnter();
				this.duration = PortalDialerController.PortalDialerPostDialState.baseDuration;
				if (PortalDialerController.PortalDialerPostDialState.portalSpawnEffect)
				{
					EffectManager.SimpleEffect(PortalDialerController.PortalDialerPostDialState.portalSpawnEffect, base.portalDialer.portalSpawnLocation.position, base.portalDialer.portalSpawnLocation.rotation, false);
				}
			}

			// Token: 0x06002CEC RID: 11500 RVA: 0x0000EC55 File Offset: 0x0000CE55
			public override void OnExit()
			{
				base.OnExit();
			}

			// Token: 0x06002CED RID: 11501 RVA: 0x000BF9F5 File Offset: 0x000BDBF5
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= this.duration)
				{
					this.outer.SetNextState(new PortalDialerController.PortalDialerPerformActionState
					{
						sequenceServer = this.sequenceServer
					});
				}
			}

			// Token: 0x04002F0B RID: 12043
			public static float baseDuration;

			// Token: 0x04002F0C RID: 12044
			public static GameObject portalSpawnEffect;

			// Token: 0x04002F0D RID: 12045
			private float duration;

			// Token: 0x04002F0E RID: 12046
			public byte[] sequenceServer;
		}

		// Token: 0x02000817 RID: 2071
		private class PortalDialerPerformActionState : PortalDialerController.PortalDialerBaseState
		{
			// Token: 0x06002CEF RID: 11503 RVA: 0x000BFA30 File Offset: 0x000BDC30
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active)
				{
					if (base.portalDialer.PerformActionServer(this.sequenceServer))
					{
						EffectManager.SimpleSoundEffect(PortalDialerController.PortalDialerPerformActionState.nseSuccess.index, base.portalDialer.portalSpawnLocation.position, true);
						return;
					}
					EffectManager.SimpleSoundEffect(PortalDialerController.PortalDialerPerformActionState.nseFail.index, base.portalDialer.portalSpawnLocation.position, true);
					this.outer.SetNextState(new PortalDialerController.PortalDialerIdleState());
				}
			}

			// Token: 0x04002F0F RID: 12047
			public byte[] sequenceServer;

			// Token: 0x04002F10 RID: 12048
			public static NetworkSoundEventDef nseSuccess;

			// Token: 0x04002F11 RID: 12049
			public static NetworkSoundEventDef nseFail;
		}
	}
}
