using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using HG.Collections.Generic;
using Rewired;
using RoR2.UI;
using UnityEngine;

namespace RoR2.GamepadVibration
{
	// Token: 0x02000B67 RID: 2919
	public static class GamepadVibrationManager
	{
		// Token: 0x06004253 RID: 16979 RVA: 0x00112AD8 File Offset: 0x00110CD8
		[SystemInitializer(new Type[]
		{
			typeof(GamepadVibrationController)
		})]
		private static void Init()
		{
			RoR2Application.onUpdate += GamepadVibrationManager.Update;
			ReInput.ControllerConnectedEvent += GamepadVibrationManager.OnControllerConnected;
			ReInput.ControllerPreDisconnectEvent += GamepadVibrationManager.OnControllerPreDisconnect;
			RoR2Application.onShutDown = (Action)Delegate.Combine(RoR2Application.onShutDown, new Action(GamepadVibrationManager.OnApplicationShutDown));
			foreach (Controller controller in ReInput.controllers.Controllers)
			{
				GamepadVibrationManager.OnControllerConnected(new ControllerStatusChangedEventArgs(controller.name, controller.id, controller.type));
			}
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x00112B94 File Offset: 0x00110D94
		private static void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			Joystick joystick = args.controller as Joystick;
			if (joystick != null)
			{
				GamepadVibrationManager.joystickToVibrationController.Add(joystick, GamepadVibrationController.Create(joystick));
			}
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x00112BC4 File Offset: 0x00110DC4
		private static void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			Joystick joystick = args.controller as Joystick;
			if (joystick != null)
			{
				int num = GamepadVibrationManager.joystickToVibrationController.FindKeyIndex(joystick);
				if (num >= 0)
				{
					GamepadVibrationManager.joystickToVibrationController[num].Value.Dispose();
					GamepadVibrationManager.joystickToVibrationController.RemoveAt(num);
				}
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x00112C14 File Offset: 0x00110E14
		private static void OnApplicationShutDown()
		{
			for (int i = GamepadVibrationManager.joystickToVibrationController.Count - 1; i >= 0; i--)
			{
				GamepadVibrationManager.joystickToVibrationController[i].Value.Dispose();
				GamepadVibrationManager.joystickToVibrationController.RemoveAt(i);
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x00112C5C File Offset: 0x00110E5C
		private static void Update()
		{
			if (Time.deltaTime <= 0f)
			{
				foreach (KeyValuePair<Joystick, GamepadVibrationController> keyValuePair in GamepadVibrationManager.joystickToVibrationController)
				{
					keyValuePair.Value.StopVibration();
				}
				return;
			}
			ReadOnlyCollection<LocalUser> readOnlyLocalUsersList = LocalUserManager.readOnlyLocalUsersList;
			ArrayUtils.EnsureCapacity<int>(ref GamepadVibrationManager.joystickIndexToLocalUserIndex, GamepadVibrationManager.joystickToVibrationController.Count);
			int[] array = GamepadVibrationManager.joystickIndexToLocalUserIndex;
			int num = -1;
			ArrayUtils.SetRange<int>(array, num, 0, GamepadVibrationManager.joystickToVibrationController.Count);
			for (int i = 0; i < readOnlyLocalUsersList.Count; i++)
			{
				LocalUser localUser = readOnlyLocalUsersList[i];
				if (localUser.inputPlayer != null && localUser.eventSystem && localUser.eventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad)
				{
					Player inputPlayer = localUser.inputPlayer;
					Joystick joystick = ((inputPlayer != null) ? inputPlayer.controllers.GetLastActiveController<Joystick>() : null) as Joystick;
					if (joystick != null)
					{
						int num2 = GamepadVibrationManager.joystickToVibrationController.FindKeyIndex(joystick);
						if (num2 >= 0)
						{
							GamepadVibrationManager.joystickIndexToLocalUserIndex[num2] = i;
						}
					}
				}
			}
			for (int j = 0; j < GamepadVibrationManager.joystickToVibrationController.Count; j++)
			{
				GamepadVibrationController value = GamepadVibrationManager.joystickToVibrationController[j].Value;
				int num3 = GamepadVibrationManager.joystickIndexToLocalUserIndex[j];
				LocalUser localUser2 = (num3 >= 0) ? readOnlyLocalUsersList[num3] : null;
				if (localUser2 != null && localUser2.userProfile != null)
				{
					VibrationContext vibrationContext = new VibrationContext
					{
						localUser = localUser2,
						cameraRigController = localUser2.cameraRigController,
						userVibrationScale = localUser2.userProfile.gamepadVibrationScale
					};
					value.ApplyVibration(vibrationContext);
				}
				else
				{
					value.StopVibration();
				}
			}
		}

		// Token: 0x0400405C RID: 16476
		private static readonly AssociationList<Joystick, GamepadVibrationController> joystickToVibrationController = new AssociationList<Joystick, GamepadVibrationController>(4, null, false);

		// Token: 0x0400405D RID: 16477
		private static int[] joystickIndexToLocalUserIndex = new int[4];
	}
}
