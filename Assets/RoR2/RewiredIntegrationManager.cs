using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A04 RID: 2564
	public static class RewiredIntegrationManager
	{
		// Token: 0x06003B39 RID: 15161 RVA: 0x000F573C File Offset: 0x000F393C
		public static void Init()
		{
			UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/Rewired Input Manager"));
			ReInput.ControllerConnectedEvent += RewiredIntegrationManager.AssignNewController;
			foreach (ControllerType controllerType in new ControllerType[]
			{
				ControllerType.Keyboard,
				ControllerType.Mouse,
				ControllerType.Joystick
			})
			{
				Controller[] controllers = ReInput.controllers.GetControllers(controllerType);
				if (controllers != null)
				{
					for (int j = 0; j < controllers.Length; j++)
					{
						RewiredIntegrationManager.AssignNewController(controllers[j]);
					}
				}
			}
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000F57B8 File Offset: 0x000F39B8
		private static void AssignJoystickToAvailablePlayer(Controller controller)
		{
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				Player player = players[i];
				if (player.name != "PlayerMain" && player.controllers.joystickCount == 0 && !player.controllers.hasKeyboard && !player.controllers.hasMouse)
				{
					player.controllers.AddController(controller, false);
					return;
				}
			}
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000F5830 File Offset: 0x000F3A30
		private static void AssignNewController(ControllerStatusChangedEventArgs args)
		{
			RewiredIntegrationManager.AssignNewController(ReInput.controllers.GetController(args.controllerType, args.controllerId));
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000F584D File Offset: 0x000F3A4D
		private static void AssignNewController(Controller controller)
		{
			ReInput.players.GetPlayer("PlayerMain").controllers.AddController(controller, false);
			if (controller.type == ControllerType.Joystick)
			{
				RewiredIntegrationManager.AssignJoystickToAvailablePlayer(controller);
			}
		}
	}
}
