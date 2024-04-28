using System;
using System.Collections.Generic;
using System.Globalization;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Networking;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D1A RID: 3354
	[RequireComponent(typeof(RectTransform))]
	public class HostGamePanelController : MonoBehaviour
	{
		// Token: 0x06004C67 RID: 19559 RVA: 0x0013B99C File Offset: 0x00139B9C
		private void Awake()
		{
			this.BuildGameModeChoices();
			this.SetDefaultHostNameIfEmpty();
			this.BuildMaxPlayerChoices();
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0013B9B0 File Offset: 0x00139BB0
		private void OnEnable()
		{
			this.PlatformOnEnable();
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0013B9B8 File Offset: 0x00139BB8
		private void OnDisable()
		{
			this.PlatformOnDisable();
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0013B9C0 File Offset: 0x00139BC0
		private void Update()
		{
			this.capacityWarningLabel.SetActive(NetworkManagerSystem.SvMaxPlayersConVar.instance.intValue > RoR2Application.maxPlayers);
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0013B9E0 File Offset: 0x00139BE0
		private void BuildGameModeChoices()
		{
			List<CarouselController.Choice> list = new List<CarouselController.Choice>(GameModeCatalog.gameModeCount);
			for (GameModeIndex gameModeIndex = (GameModeIndex)0; gameModeIndex < (GameModeIndex)GameModeCatalog.gameModeCount; gameModeIndex++)
			{
				Run gameModePrefabComponent = GameModeCatalog.GetGameModePrefabComponent(gameModeIndex);
				ExpansionRequirementComponent component = gameModePrefabComponent.GetComponent<ExpansionRequirementComponent>();
				if (gameModePrefabComponent.userPickable && (!component || !component.requiredExpansion || EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(component.requiredExpansion.requiredEntitlement)))
				{
					list.Add(new CarouselController.Choice
					{
						suboptionDisplayToken = gameModePrefabComponent.nameToken,
						convarValue = gameModePrefabComponent.name
					});
				}
			}
			this.gameModePicker.choices = list.ToArray();
			this.gameModePicker.gameObject.SetActive(list.Count > 1);
			string @string = Console.instance.FindConVar("gamemode").GetString();
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].convarValue == @string)
				{
					flag = true;
					break;
				}
			}
			if (list.Count == 1 || !flag)
			{
				Debug.LogFormat("Invalid gamemode {0} detected. Reverting to ClassicRun.", new object[]
				{
					@string
				});
				this.gameModePicker.SubmitSetting(list[0].convarValue);
			}
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0013BB28 File Offset: 0x00139D28
		private void BuildMaxPlayerChoices()
		{
			int num = 2;
			int num2 = RoR2Application.hardMaxPlayers;
			int? num3 = this.PlatformGetCurrentLobbyCapacity();
			if (num3 != null)
			{
				num = Math.Max(2, num3.Value);
				num2 = num;
			}
			List<CarouselController.Choice> list = new List<CarouselController.Choice>(num2 - num + 1);
			for (int i = num; i <= num2; i++)
			{
				string convarValue = TextSerialization.ToStringInvariant(i);
				list.Add(new CarouselController.Choice
				{
					suboptionDisplayToken = i.ToString(CultureInfo.CurrentCulture),
					convarValue = convarValue
				});
			}
			this.maxPlayersPicker.choices = list.ToArray();
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0013BBC0 File Offset: 0x00139DC0
		private void SetDefaultHostNameIfEmpty()
		{
			NetworkManagerSystem.SvHostNameConVar instance = NetworkManagerSystem.SvHostNameConVar.instance;
			if (string.IsNullOrEmpty(instance.GetString()))
			{
				instance.SetString(Language.GetStringFormatted("HOSTGAMEPANEL_DEFAULT_SERVER_NAME_FORMAT", new object[]
				{
					RoR2Application.GetBestUserName()
				}));
			}
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0013BC00 File Offset: 0x00139E00
		private void PlatformOnEnable()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyChanged = (Action)Delegate.Combine(lobbyManager.onLobbyChanged, new Action(this.OnLobbyChanged));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyDataUpdated = (Action)Delegate.Combine(lobbyManager2.onLobbyDataUpdated, new Action(this.OnLobbyDataUpdated));
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0013BC5C File Offset: 0x00139E5C
		private void PlatformOnDisable()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyDataUpdated = (Action)Delegate.Remove(lobbyManager.onLobbyDataUpdated, new Action(this.OnLobbyDataUpdated));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyChanged = (Action)Delegate.Remove(lobbyManager2.onLobbyChanged, new Action(this.OnLobbyChanged));
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0013BCB5 File Offset: 0x00139EB5
		private void OnLobbyDataUpdated()
		{
			this.BuildMaxPlayerChoices();
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0013BCB5 File Offset: 0x00139EB5
		private void OnLobbyChanged()
		{
			this.BuildMaxPlayerChoices();
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0013BCC0 File Offset: 0x00139EC0
		private int? PlatformGetCurrentLobbyCapacity()
		{
			LobbyManager.LobbyData newestLobbyData = PlatformSystems.lobbyManager.newestLobbyData;
			if (newestLobbyData == null)
			{
				return null;
			}
			return new int?(newestLobbyData.totalMaxPlayers);
		}

		// Token: 0x0400497B RID: 18811
		public CarouselController gameModePicker;

		// Token: 0x0400497C RID: 18812
		public CarouselController maxPlayersPicker;

		// Token: 0x0400497D RID: 18813
		public GameObject capacityWarningLabel;
	}
}
