using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Rewired;
using Rewired.Integration.UnityUI;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008D6 RID: 2262
	public class MPEventSystemManager : MonoBehaviour
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000D5C97 File Offset: 0x000D3E97
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000D5C9E File Offset: 0x000D3E9E
		public static MPEventSystem combinedEventSystem { get; private set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000D5CA6 File Offset: 0x000D3EA6
		// (set) Token: 0x060032A9 RID: 12969 RVA: 0x000D5CAD File Offset: 0x000D3EAD
		public static MPEventSystem kbmEventSystem { get; private set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000D5CB5 File Offset: 0x000D3EB5
		// (set) Token: 0x060032AB RID: 12971 RVA: 0x000D5CBC File Offset: 0x000D3EBC
		public static MPEventSystem primaryEventSystem { get; private set; }

		// Token: 0x060032AC RID: 12972 RVA: 0x000D5CC4 File Offset: 0x000D3EC4
		public static MPEventSystem FindEventSystem(Player inputPlayer)
		{
			MPEventSystem result;
			MPEventSystemManager.eventSystems.TryGetValue(inputPlayer.id, out result);
			return result;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000D5CE8 File Offset: 0x000D3EE8
		private static void Initialize()
		{
			GameObject original = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/MPEventSystem");
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, RoR2Application.instance.transform);
				gameObject.name = string.Format(CultureInfo.InvariantCulture, "MPEventSystem Player{0}", i);
				MPEventSystem component = gameObject.GetComponent<MPEventSystem>();
				RewiredStandaloneInputModule component2 = gameObject.GetComponent<RewiredStandaloneInputModule>();
				Player player = players[i];
				component2.RewiredPlayerIds = new int[]
				{
					player.id
				};
				gameObject.GetComponent<MPInput>().player = player;
				if (i == 1)
				{
					MPEventSystemManager.kbmEventSystem = component;
					component.allowCursorPush = false;
				}
				component.player = players[i];
				MPEventSystemManager.eventSystems[players[i].id] = component;
			}
			MPEventSystemManager.combinedEventSystem = MPEventSystemManager.eventSystems[0];
			MPEventSystemManager.combinedEventSystem.isCombinedEventSystem = true;
			MPEventSystemManager.RefreshEventSystems();
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000D5DE0 File Offset: 0x000D3FE0
		private static void RefreshEventSystems()
		{
			int count = LocalUserManager.readOnlyLocalUsersList.Count;
			ReadOnlyCollection<MPEventSystem> readOnlyInstancesList = MPEventSystem.readOnlyInstancesList;
			readOnlyInstancesList[0].enabled = (count <= 1);
			for (int i = 1; i < readOnlyInstancesList.Count; i++)
			{
				readOnlyInstancesList[i].enabled = (readOnlyInstancesList[i].localUser != null);
			}
			int num = 0;
			for (int j = 0; j < readOnlyInstancesList.Count; j++)
			{
				MPEventSystem mpeventSystem = readOnlyInstancesList[j];
				int playerSlot;
				if (!readOnlyInstancesList[j].enabled)
				{
					playerSlot = -1;
				}
				else
				{
					num = (playerSlot = num) + 1;
				}
				mpeventSystem.playerSlot = playerSlot;
			}
			MPEventSystemManager.primaryEventSystem = ((count > 0) ? LocalUserManager.readOnlyLocalUsersList[0].eventSystem : MPEventSystemManager.combinedEventSystem);
			MPEventSystemManager.availability.MakeAvailable();
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000D5EA2 File Offset: 0x000D40A2
		private void Awake()
		{
			MPEventSystemManager.Initialize();
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000D5EA9 File Offset: 0x000D40A9
		private void Update()
		{
			if (!Application.isBatchMode)
			{
				Cursor.lockState = ((MPEventSystemManager.kbmEventSystem.isCursorVisible || MPEventSystemManager.combinedEventSystem.isCursorVisible) ? CursorLockMode.Confined : CursorLockMode.Locked);
				Cursor.visible = false;
			}
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000D5ED9 File Offset: 0x000D40D9
		static MPEventSystemManager()
		{
			LocalUserManager.onLocalUsersUpdated += MPEventSystemManager.RefreshEventSystems;
		}

		// Token: 0x040033D7 RID: 13271
		private static readonly Dictionary<int, MPEventSystem> eventSystems = new Dictionary<int, MPEventSystem>();

		// Token: 0x040033DB RID: 13275
		public static ResourceAvailability availability;
	}
}
