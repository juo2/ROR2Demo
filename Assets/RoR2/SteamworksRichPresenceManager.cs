using System;
using System.Collections.Generic;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RoR2
{
	// Token: 0x020009D5 RID: 2517
	internal static class SteamworksRichPresenceManager
	{
		// Token: 0x06003A05 RID: 14853 RVA: 0x000F1E38 File Offset: 0x000F0038
		private static void SetKeyValue([NotNull] string key, [CanBeNull] string value)
		{
			if (Client.Instance != null && Client.Instance.User != null)
			{
				Client.Instance.User.SetRichPresence(key, value);
			}
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000F1E60 File Offset: 0x000F0060
		public static void Init()
		{
			new SteamworksRichPresenceManager.DifficultyField().Install();
			new SteamworksRichPresenceManager.GameModeField().Install();
			new SteamworksRichPresenceManager.ParticipationField().Install();
			new SteamworksRichPresenceManager.MinutesField().Install();
			new SteamworksRichPresenceManager.SteamPlayerGroupField().Install();
			new SteamworksRichPresenceManager.SteamDisplayField().Install();
			RoR2Application.onUpdate += SteamworksRichPresenceManager.BaseRichPresenceField.ProcessDirtyFields;
		}

		// Token: 0x0400392B RID: 14635
		private const string rpDifficulty = "difficulty";

		// Token: 0x0400392C RID: 14636
		private const string rpGameMode = "gamemode";

		// Token: 0x0400392D RID: 14637
		private const string rpParticipationType = "participation_type";

		// Token: 0x0400392E RID: 14638
		private const string rpMinutes = "minutes";

		// Token: 0x0400392F RID: 14639
		private const string rpSteamPlayerGroupSize = "steam_player_group_size";

		// Token: 0x04003930 RID: 14640
		private const string rpSteamPlayerGroup = "steam_player_group";

		// Token: 0x04003931 RID: 14641
		private const string rpSteamDisplay = "steam_display";

		// Token: 0x020009D6 RID: 2518
		private abstract class BaseRichPresenceField
		{
			// Token: 0x06003A07 RID: 14855 RVA: 0x000F1EBA File Offset: 0x000F00BA
			public static void ProcessDirtyFields()
			{
				while (SteamworksRichPresenceManager.BaseRichPresenceField.dirtyFields.Count > 0)
				{
					SteamworksRichPresenceManager.BaseRichPresenceField.dirtyFields.Dequeue().UpdateIfNecessary();
				}
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x06003A08 RID: 14856
			protected abstract string key { get; }

			// Token: 0x06003A09 RID: 14857
			[CanBeNull]
			protected abstract string RebuildValue();

			// Token: 0x06003A0A RID: 14858 RVA: 0x000026ED File Offset: 0x000008ED
			protected virtual void OnChanged()
			{
			}

			// Token: 0x06003A0B RID: 14859 RVA: 0x000F1EDA File Offset: 0x000F00DA
			public void SetDirty()
			{
				if (!this.isDirty)
				{
					this.isDirty = true;
					SteamworksRichPresenceManager.BaseRichPresenceField.dirtyFields.Enqueue(this);
				}
			}

			// Token: 0x06003A0C RID: 14860 RVA: 0x000F1EF8 File Offset: 0x000F00F8
			private void UpdateIfNecessary()
			{
				if (!this.installed)
				{
					return;
				}
				this.isDirty = false;
				string a = this.RebuildValue();
				if (a != this.currentValue)
				{
					this.currentValue = a;
					SteamworksRichPresenceManager.SetKeyValue(this.key, this.currentValue);
					this.OnChanged();
				}
			}

			// Token: 0x06003A0D RID: 14861 RVA: 0x000026ED File Offset: 0x000008ED
			protected virtual void OnInstall()
			{
			}

			// Token: 0x06003A0E RID: 14862 RVA: 0x000026ED File Offset: 0x000008ED
			protected virtual void OnUninstall()
			{
			}

			// Token: 0x06003A0F RID: 14863 RVA: 0x000F1F48 File Offset: 0x000F0148
			public void Install()
			{
				if (!this.installed)
				{
					this.OnInstall();
					this.SetDirty();
					this.installed = true;
				}
			}

			// Token: 0x06003A10 RID: 14864 RVA: 0x000F1F65 File Offset: 0x000F0165
			public void Uninstall()
			{
				if (this.installed)
				{
					this.OnUninstall();
					this.installed = false;
					SteamworksRichPresenceManager.SetKeyValue(this.key, null);
				}
			}

			// Token: 0x06003A11 RID: 14865 RVA: 0x000F1F88 File Offset: 0x000F0188
			protected void SetDirtyableValue<T>(ref T field, T value) where T : struct, IEquatable<T>
			{
				if (!field.Equals(value))
				{
					field = value;
					this.SetDirty();
				}
			}

			// Token: 0x06003A12 RID: 14866 RVA: 0x000F1FA6 File Offset: 0x000F01A6
			protected void SetDirtyableReference<T>(ref T field, T value) where T : class
			{
				if (field != value)
				{
					field = value;
					this.SetDirty();
				}
			}

			// Token: 0x04003932 RID: 14642
			private static readonly Queue<SteamworksRichPresenceManager.BaseRichPresenceField> dirtyFields = new Queue<SteamworksRichPresenceManager.BaseRichPresenceField>();

			// Token: 0x04003933 RID: 14643
			private bool isDirty;

			// Token: 0x04003934 RID: 14644
			[CanBeNull]
			private string currentValue;

			// Token: 0x04003935 RID: 14645
			private bool installed;
		}

		// Token: 0x020009D7 RID: 2519
		private sealed class DifficultyField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000F1FD4 File Offset: 0x000F01D4
			protected override string key
			{
				get
				{
					return "difficulty";
				}
			}

			// Token: 0x06003A16 RID: 14870 RVA: 0x000F1FDC File Offset: 0x000F01DC
			protected override string RebuildValue()
			{
				if (!Run.instance)
				{
					return null;
				}
				if (DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).countsAsHardMode)
				{
					return "Hard";
				}
				switch (Run.instance.selectedDifficulty)
				{
				case DifficultyIndex.Easy:
					return "Easy";
				case DifficultyIndex.Normal:
					return "Normal";
				case DifficultyIndex.Hard:
					return "Hard";
				default:
					return null;
				}
			}

			// Token: 0x06003A17 RID: 14871 RVA: 0x000F2045 File Offset: 0x000F0245
			private void SetDirty(Run run)
			{
				base.SetDirty();
			}

			// Token: 0x06003A18 RID: 14872 RVA: 0x000F204D File Offset: 0x000F024D
			protected override void OnInstall()
			{
				base.OnInstall();
				Run.onRunStartGlobal += this.SetDirty;
				Run.onRunDestroyGlobal += this.SetDirty;
			}

			// Token: 0x06003A19 RID: 14873 RVA: 0x000F2077 File Offset: 0x000F0277
			protected override void OnUninstall()
			{
				Run.onRunStartGlobal -= this.SetDirty;
				Run.onRunDestroyGlobal -= this.SetDirty;
				base.OnUninstall();
			}
		}

		// Token: 0x020009D8 RID: 2520
		private sealed class GameModeField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x06003A1B RID: 14875 RVA: 0x000F20A9 File Offset: 0x000F02A9
			protected override string key
			{
				get
				{
					return "gamemode";
				}
			}

			// Token: 0x06003A1C RID: 14876 RVA: 0x000F20B0 File Offset: 0x000F02B0
			protected override string RebuildValue()
			{
				if (!Run.instance)
				{
					return null;
				}
				Run run = GameModeCatalog.FindGameModePrefabComponent(Run.instance.name);
				if (run == null)
				{
					return null;
				}
				return run.name;
			}

			// Token: 0x06003A1D RID: 14877 RVA: 0x000F2045 File Offset: 0x000F0245
			private void SetDirty(Run run)
			{
				base.SetDirty();
			}

			// Token: 0x06003A1E RID: 14878 RVA: 0x000F20DA File Offset: 0x000F02DA
			protected override void OnInstall()
			{
				base.OnInstall();
				Run.onRunStartGlobal += this.SetDirty;
				Run.onRunDestroyGlobal += this.SetDirty;
			}

			// Token: 0x06003A1F RID: 14879 RVA: 0x000F2104 File Offset: 0x000F0304
			protected override void OnUninstall()
			{
				Run.onRunStartGlobal -= this.SetDirty;
				Run.onRunDestroyGlobal -= this.SetDirty;
				base.OnUninstall();
			}
		}

		// Token: 0x020009D9 RID: 2521
		private sealed class ParticipationField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000F212E File Offset: 0x000F032E
			protected override string key
			{
				get
				{
					return "participation_type";
				}
			}

			// Token: 0x06003A22 RID: 14882 RVA: 0x000F2135 File Offset: 0x000F0335
			private void SetParticipationType(SteamworksRichPresenceManager.ParticipationField.ParticipationType newParticipationType)
			{
				if (this.participationType != newParticipationType)
				{
					this.participationType = newParticipationType;
					base.SetDirty();
				}
			}

			// Token: 0x06003A23 RID: 14883 RVA: 0x000F2150 File Offset: 0x000F0350
			protected override string RebuildValue()
			{
				switch (this.participationType)
				{
				case SteamworksRichPresenceManager.ParticipationField.ParticipationType.Alive:
					return "Alive";
				case SteamworksRichPresenceManager.ParticipationField.ParticipationType.Dead:
					return "Dead";
				case SteamworksRichPresenceManager.ParticipationField.ParticipationType.Spectator:
					return "Spectator";
				default:
					return null;
				}
			}

			// Token: 0x06003A24 RID: 14884 RVA: 0x000F2190 File Offset: 0x000F0390
			protected override void OnInstall()
			{
				base.OnInstall();
				LocalUserManager.onUserSignIn += this.OnLocalUserDiscovered;
				LocalUserManager.onUserSignOut += this.OnLocalUserLost;
				Run.onRunStartGlobal += this.OnRunStart;
				Run.onRunDestroyGlobal += this.OnRunDestroy;
			}

			// Token: 0x06003A25 RID: 14885 RVA: 0x000F21E8 File Offset: 0x000F03E8
			protected override void OnUninstall()
			{
				LocalUserManager.onUserSignIn -= this.OnLocalUserDiscovered;
				LocalUserManager.onUserSignOut -= this.OnLocalUserLost;
				Run.onRunStartGlobal -= this.OnRunStart;
				Run.onRunDestroyGlobal -= this.OnRunDestroy;
				this.SetCurrentMaster(null);
			}

			// Token: 0x06003A26 RID: 14886 RVA: 0x000F2240 File Offset: 0x000F0440
			private void SetTrackedUser(LocalUser newTrackedUser)
			{
				if (this.trackedUser != null)
				{
					this.trackedUser.onMasterChanged -= this.OnMasterChanged;
				}
				this.trackedUser = newTrackedUser;
				if (this.trackedUser != null)
				{
					this.trackedUser.onMasterChanged += this.OnMasterChanged;
				}
			}

			// Token: 0x06003A27 RID: 14887 RVA: 0x000F2292 File Offset: 0x000F0492
			private void OnLocalUserDiscovered(LocalUser localUser)
			{
				if (this.trackedUser == null)
				{
					this.SetTrackedUser(localUser);
				}
			}

			// Token: 0x06003A28 RID: 14888 RVA: 0x000F22A3 File Offset: 0x000F04A3
			private void OnLocalUserLost(LocalUser localUser)
			{
				if (this.trackedUser == localUser)
				{
					this.SetTrackedUser(null);
				}
			}

			// Token: 0x06003A29 RID: 14889 RVA: 0x000F22B5 File Offset: 0x000F04B5
			private void OnRunStart(Run run)
			{
				if (this.trackedUser != null && !this.trackedUser.cachedMasterObject)
				{
					this.SetParticipationType(SteamworksRichPresenceManager.ParticipationField.ParticipationType.Spectator);
				}
			}

			// Token: 0x06003A2A RID: 14890 RVA: 0x000F22D8 File Offset: 0x000F04D8
			private void OnRunDestroy(Run run)
			{
				if (this.trackedUser != null)
				{
					this.SetParticipationType(SteamworksRichPresenceManager.ParticipationField.ParticipationType.None);
				}
			}

			// Token: 0x06003A2B RID: 14891 RVA: 0x000F22EC File Offset: 0x000F04EC
			private void OnMasterChanged()
			{
				PlayerCharacterMasterController cachedMasterController = this.trackedUser.cachedMasterController;
				this.SetCurrentMaster(cachedMasterController ? cachedMasterController.master : null);
			}

			// Token: 0x06003A2C RID: 14892 RVA: 0x000F231C File Offset: 0x000F051C
			private void SetCurrentMaster(CharacterMaster newMaster)
			{
				if (this.currentMaster != null)
				{
					this.currentMaster.onBodyDeath.RemoveListener(new UnityAction(this.OnBodyDeath));
					this.currentMaster.onBodyStart -= this.OnBodyStart;
				}
				this.currentMaster = newMaster;
				if (this.currentMaster != null)
				{
					this.currentMaster.onBodyDeath.AddListener(new UnityAction(this.OnBodyDeath));
					this.currentMaster.onBodyStart += this.OnBodyStart;
				}
			}

			// Token: 0x06003A2D RID: 14893 RVA: 0x000F23A6 File Offset: 0x000F05A6
			private void OnBodyDeath()
			{
				this.SetParticipationType(SteamworksRichPresenceManager.ParticipationField.ParticipationType.Dead);
			}

			// Token: 0x06003A2E RID: 14894 RVA: 0x000F23AF File Offset: 0x000F05AF
			private void OnBodyStart(CharacterBody body)
			{
				this.SetParticipationType(SteamworksRichPresenceManager.ParticipationField.ParticipationType.Alive);
			}

			// Token: 0x04003936 RID: 14646
			private SteamworksRichPresenceManager.ParticipationField.ParticipationType participationType;

			// Token: 0x04003937 RID: 14647
			private LocalUser trackedUser;

			// Token: 0x04003938 RID: 14648
			private CharacterMaster currentMaster;

			// Token: 0x020009DA RID: 2522
			private enum ParticipationType
			{
				// Token: 0x0400393A RID: 14650
				None,
				// Token: 0x0400393B RID: 14651
				Alive,
				// Token: 0x0400393C RID: 14652
				Dead,
				// Token: 0x0400393D RID: 14653
				Spectator
			}
		}

		// Token: 0x020009DB RID: 2523
		private sealed class MinutesField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000F23B8 File Offset: 0x000F05B8
			protected override string key
			{
				get
				{
					return "minutes";
				}
			}

			// Token: 0x06003A31 RID: 14897 RVA: 0x000F23BF File Offset: 0x000F05BF
			protected override string RebuildValue()
			{
				return TextSerialization.ToStringInvariant(this.minutes);
			}

			// Token: 0x06003A32 RID: 14898 RVA: 0x000F23CC File Offset: 0x000F05CC
			private void FixedUpdate()
			{
				uint value = 0U;
				if (Run.instance)
				{
					value = (uint)Mathf.FloorToInt(Run.instance.GetRunStopwatch() / 60f);
				}
				base.SetDirtyableValue<uint>(ref this.minutes, value);
			}

			// Token: 0x06003A33 RID: 14899 RVA: 0x000F240A File Offset: 0x000F060A
			protected override void OnInstall()
			{
				base.OnInstall();
				RoR2Application.onFixedUpdate += this.FixedUpdate;
			}

			// Token: 0x06003A34 RID: 14900 RVA: 0x000F2423 File Offset: 0x000F0623
			protected override void OnUninstall()
			{
				RoR2Application.onFixedUpdate -= this.FixedUpdate;
				base.OnUninstall();
			}

			// Token: 0x0400393E RID: 14654
			private uint minutes;
		}

		// Token: 0x020009DC RID: 2524
		private sealed class SteamPlayerGroupField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000F243C File Offset: 0x000F063C
			protected override string key
			{
				get
				{
					return "steam_player_group";
				}
			}

			// Token: 0x06003A37 RID: 14903 RVA: 0x000F2443 File Offset: 0x000F0643
			private void SetLobbyId(CSteamID newLobbyId)
			{
				if (this.lobbyId != newLobbyId)
				{
					this.lobbyId = newLobbyId;
					this.UpdateGroupID();
				}
			}

			// Token: 0x06003A38 RID: 14904 RVA: 0x000F2460 File Offset: 0x000F0660
			private void SetHostId(CSteamID newHostId)
			{
				if (this.hostId != newHostId)
				{
					this.hostId = newHostId;
					this.UpdateGroupID();
				}
			}

			// Token: 0x06003A39 RID: 14905 RVA: 0x000F247D File Offset: 0x000F067D
			private void SetGroupId(CSteamID newGroupId)
			{
				if (this.groupId != newGroupId)
				{
					this.groupId = newGroupId;
					base.SetDirty();
				}
			}

			// Token: 0x06003A3A RID: 14906 RVA: 0x000F249C File Offset: 0x000F069C
			private void UpdateGroupID()
			{
				if (this.hostId != CSteamID.nil)
				{
					this.SetGroupId(this.hostId);
					if (!(this.groupSizeField is SteamworksRichPresenceManager.SteamPlayerGroupSizeFieldGame))
					{
						SteamworksRichPresenceManager.SteamPlayerGroupSizeField steamPlayerGroupSizeField = this.groupSizeField;
						if (steamPlayerGroupSizeField != null)
						{
							steamPlayerGroupSizeField.Uninstall();
						}
						this.groupSizeField = new SteamworksRichPresenceManager.SteamPlayerGroupSizeFieldGame();
						this.groupSizeField.Install();
						return;
					}
				}
				else
				{
					this.SetGroupId(this.lobbyId);
					if (!(this.groupSizeField is SteamworksRichPresenceManager.SteamPlayerGroupSizeFieldLobby))
					{
						SteamworksRichPresenceManager.SteamPlayerGroupSizeField steamPlayerGroupSizeField2 = this.groupSizeField;
						if (steamPlayerGroupSizeField2 != null)
						{
							steamPlayerGroupSizeField2.Uninstall();
						}
						this.groupSizeField = new SteamworksRichPresenceManager.SteamPlayerGroupSizeFieldLobby();
						this.groupSizeField.Install();
					}
				}
			}

			// Token: 0x06003A3B RID: 14907 RVA: 0x000F253C File Offset: 0x000F073C
			protected override void OnInstall()
			{
				base.OnInstall();
				NetworkManagerSystem.onClientConnectGlobal += this.OnClientConnectGlobal;
				NetworkManagerSystem.onClientDisconnectGlobal += this.OnClientDisconnectGlobal;
				NetworkManagerSystem.onStartServerGlobal += this.OnStartServerGlobal;
				NetworkManagerSystem.onStopServerGlobal += this.OnStopServerGlobal;
				LobbyManager lobbyManager = PlatformSystems.lobbyManager;
				lobbyManager.onLobbyChanged = (Action)Delegate.Combine(lobbyManager.onLobbyChanged, new Action(this.OnLobbyChanged));
			}

			// Token: 0x06003A3C RID: 14908 RVA: 0x000F25BC File Offset: 0x000F07BC
			protected override void OnUninstall()
			{
				NetworkManagerSystem.onClientConnectGlobal -= this.OnClientConnectGlobal;
				NetworkManagerSystem.onClientDisconnectGlobal -= this.OnClientDisconnectGlobal;
				NetworkManagerSystem.onStartServerGlobal -= this.OnStartServerGlobal;
				NetworkManagerSystem.onStopServerGlobal -= this.OnStopServerGlobal;
				LobbyManager lobbyManager = PlatformSystems.lobbyManager;
				lobbyManager.onLobbyChanged = (Action)Delegate.Remove(lobbyManager.onLobbyChanged, new Action(this.OnLobbyChanged));
				SteamworksRichPresenceManager.SteamPlayerGroupSizeField steamPlayerGroupSizeField = this.groupSizeField;
				if (steamPlayerGroupSizeField != null)
				{
					steamPlayerGroupSizeField.Uninstall();
				}
				this.groupSizeField = null;
				base.OnUninstall();
			}

			// Token: 0x06003A3D RID: 14909 RVA: 0x000F2651 File Offset: 0x000F0851
			protected override string RebuildValue()
			{
				if (this.groupId == CSteamID.nil)
				{
					return null;
				}
				return TextSerialization.ToStringInvariant(this.groupId.steamValue);
			}

			// Token: 0x06003A3E RID: 14910 RVA: 0x000F2678 File Offset: 0x000F0878
			private void OnClientConnectGlobal(NetworkConnection conn)
			{
				SteamNetworkConnection steamNetworkConnection;
				if ((steamNetworkConnection = (conn as SteamNetworkConnection)) != null)
				{
					this.hostId = steamNetworkConnection.steamId;
				}
			}

			// Token: 0x06003A3F RID: 14911 RVA: 0x000F269B File Offset: 0x000F089B
			private void OnClientDisconnectGlobal(NetworkConnection conn)
			{
				this.hostId = CSteamID.nil;
			}

			// Token: 0x06003A40 RID: 14912 RVA: 0x000F26A8 File Offset: 0x000F08A8
			private void OnStartServerGlobal()
			{
				this.hostId = NetworkManagerSystem.singleton.serverP2PId;
			}

			// Token: 0x06003A41 RID: 14913 RVA: 0x000F269B File Offset: 0x000F089B
			private void OnStopServerGlobal()
			{
				this.hostId = CSteamID.nil;
			}

			// Token: 0x06003A42 RID: 14914 RVA: 0x000F26BA File Offset: 0x000F08BA
			private void OnLobbyChanged()
			{
				this.SetLobbyId(new CSteamID(Client.Instance.Lobby.CurrentLobby));
			}

			// Token: 0x0400393F RID: 14655
			private CSteamID lobbyId = CSteamID.nil;

			// Token: 0x04003940 RID: 14656
			private CSteamID hostId = CSteamID.nil;

			// Token: 0x04003941 RID: 14657
			private CSteamID groupId = CSteamID.nil;

			// Token: 0x04003942 RID: 14658
			private SteamworksRichPresenceManager.SteamPlayerGroupSizeField groupSizeField;
		}

		// Token: 0x020009DD RID: 2525
		private abstract class SteamPlayerGroupSizeField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000F26FF File Offset: 0x000F08FF
			protected override string key
			{
				get
				{
					return "steam_player_group_size";
				}
			}

			// Token: 0x06003A45 RID: 14917 RVA: 0x000F2706 File Offset: 0x000F0906
			protected override string RebuildValue()
			{
				return TextSerialization.ToStringInvariant(this.groupSize);
			}

			// Token: 0x04003943 RID: 14659
			protected int groupSize;
		}

		// Token: 0x020009DE RID: 2526
		private sealed class SteamPlayerGroupSizeFieldLobby : SteamworksRichPresenceManager.SteamPlayerGroupSizeField
		{
			// Token: 0x06003A47 RID: 14919 RVA: 0x000F2713 File Offset: 0x000F0913
			protected override void OnInstall()
			{
				base.OnInstall();
				LobbyManager lobbyManager = PlatformSystems.lobbyManager;
				lobbyManager.onPlayerCountUpdated = (Action)Delegate.Combine(lobbyManager.onPlayerCountUpdated, new Action(this.UpdateGroupSize));
				this.UpdateGroupSize();
			}

			// Token: 0x06003A48 RID: 14920 RVA: 0x000F2747 File Offset: 0x000F0947
			protected override void OnUninstall()
			{
				LobbyManager lobbyManager = PlatformSystems.lobbyManager;
				lobbyManager.onPlayerCountUpdated = (Action)Delegate.Remove(lobbyManager.onPlayerCountUpdated, new Action(this.UpdateGroupSize));
				base.OnUninstall();
			}

			// Token: 0x06003A49 RID: 14921 RVA: 0x000F2775 File Offset: 0x000F0975
			private void UpdateGroupSize()
			{
				base.SetDirtyableValue<int>(ref this.groupSize, PlatformSystems.lobbyManager.calculatedTotalPlayerCount);
			}
		}

		// Token: 0x020009DF RID: 2527
		private sealed class SteamPlayerGroupSizeFieldGame : SteamworksRichPresenceManager.SteamPlayerGroupSizeField
		{
			// Token: 0x06003A4B RID: 14923 RVA: 0x000F2795 File Offset: 0x000F0995
			protected override void OnInstall()
			{
				base.OnInstall();
				NetworkUser.onNetworkUserDiscovered += this.OnNetworkUserDiscovered;
				NetworkUser.onNetworkUserLost += this.OnNetworkUserLost;
				this.UpdateGroupSize();
			}

			// Token: 0x06003A4C RID: 14924 RVA: 0x000F27C5 File Offset: 0x000F09C5
			protected override void OnUninstall()
			{
				NetworkUser.onNetworkUserDiscovered -= this.OnNetworkUserDiscovered;
				NetworkUser.onNetworkUserLost -= this.OnNetworkUserLost;
				base.OnUninstall();
			}

			// Token: 0x06003A4D RID: 14925 RVA: 0x000F27EF File Offset: 0x000F09EF
			private void UpdateGroupSize()
			{
				base.SetDirtyableValue<int>(ref this.groupSize, NetworkUser.readOnlyInstancesList.Count);
			}

			// Token: 0x06003A4E RID: 14926 RVA: 0x000F2807 File Offset: 0x000F0A07
			private void OnNetworkUserLost(NetworkUser networkuser)
			{
				this.UpdateGroupSize();
			}

			// Token: 0x06003A4F RID: 14927 RVA: 0x000F2807 File Offset: 0x000F0A07
			private void OnNetworkUserDiscovered(NetworkUser networkUser)
			{
				this.UpdateGroupSize();
			}
		}

		// Token: 0x020009E0 RID: 2528
		private sealed class SteamDisplayField : SteamworksRichPresenceManager.BaseRichPresenceField
		{
			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000F280F File Offset: 0x000F0A0F
			protected override string key
			{
				get
				{
					return "steam_display";
				}
			}

			// Token: 0x06003A52 RID: 14930 RVA: 0x000F2818 File Offset: 0x000F0A18
			protected override string RebuildValue()
			{
				Scene activeScene = SceneManager.GetActiveScene();
				if (Run.instance)
				{
					if (GameOverController.instance)
					{
						return "#Display_GameOver";
					}
					return "#Display_InGame";
				}
				else
				{
					if (NetworkSession.instance)
					{
						return "#Display_PreGame";
					}
					if (SteamLobbyFinder.running)
					{
						return "#Display_Quickplay";
					}
					if (PlatformSystems.lobbyManager.isInLobby)
					{
						return "#Display_InLobby";
					}
					if (activeScene.name == "logbook")
					{
						return "#Display_Logbook";
					}
					return "#Display_MainMenu";
				}
			}

			// Token: 0x06003A53 RID: 14931 RVA: 0x000F289E File Offset: 0x000F0A9E
			protected override void OnInstall()
			{
				base.OnInstall();
				RoR2Application.onUpdate += base.SetDirty;
			}

			// Token: 0x06003A54 RID: 14932 RVA: 0x000F28B7 File Offset: 0x000F0AB7
			protected override void OnUninstall()
			{
				RoR2Application.onUpdate -= base.SetDirty;
				base.OnUninstall();
			}
		}
	}
}
