using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200005C RID: 92
	internal class SteamController : IDisposable
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003080 File Offset: 0x00001280
		internal SteamController(BaseSteamworks steamworks, IntPtr pointer)
		{
			this.steamworks = steamworks;
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64(pointer);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32(pointer);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32(pointer);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64(pointer);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac(pointer);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000030FD File Offset: 0x000012FD
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003114 File Offset: 0x00001314
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003130 File Offset: 0x00001330
		public void ActivateActionSet(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle)
		{
			this.platform.ISteamController_ActivateActionSet(controllerHandle.Value, actionSetHandle.Value);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003149 File Offset: 0x00001349
		public void ActivateActionSetLayer(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle)
		{
			this.platform.ISteamController_ActivateActionSetLayer(controllerHandle.Value, actionSetLayerHandle.Value);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003162 File Offset: 0x00001362
		public void DeactivateActionSetLayer(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle)
		{
			this.platform.ISteamController_DeactivateActionSetLayer(controllerHandle.Value, actionSetLayerHandle.Value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000317B File Offset: 0x0000137B
		public void DeactivateAllActionSetLayers(ControllerHandle_t controllerHandle)
		{
			this.platform.ISteamController_DeactivateAllActionSetLayers(controllerHandle.Value);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000318E File Offset: 0x0000138E
		public ControllerActionSetHandle_t GetActionSetHandle(string pszActionSetName)
		{
			return this.platform.ISteamController_GetActionSetHandle(pszActionSetName);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000319C File Offset: 0x0000139C
		public int GetActiveActionSetLayers(ControllerHandle_t controllerHandle, IntPtr handlesOut)
		{
			return this.platform.ISteamController_GetActiveActionSetLayers(controllerHandle.Value, handlesOut);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031B0 File Offset: 0x000013B0
		public ControllerAnalogActionData_t GetAnalogActionData(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle)
		{
			return this.platform.ISteamController_GetAnalogActionData(controllerHandle.Value, analogActionHandle.Value);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031C9 File Offset: 0x000013C9
		public ControllerAnalogActionHandle_t GetAnalogActionHandle(string pszActionName)
		{
			return this.platform.ISteamController_GetAnalogActionHandle(pszActionName);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031D7 File Offset: 0x000013D7
		public int GetAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, out ControllerActionOrigin originsOut)
		{
			return this.platform.ISteamController_GetAnalogActionOrigins(controllerHandle.Value, actionSetHandle.Value, analogActionHandle.Value, out originsOut);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000031F8 File Offset: 0x000013F8
		public int GetConnectedControllers(IntPtr handlesOut)
		{
			return this.platform.ISteamController_GetConnectedControllers(handlesOut);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003206 File Offset: 0x00001406
		public ControllerHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			return this.platform.ISteamController_GetControllerForGamepadIndex(nIndex);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003214 File Offset: 0x00001414
		public ControllerActionSetHandle_t GetCurrentActionSet(ControllerHandle_t controllerHandle)
		{
			return this.platform.ISteamController_GetCurrentActionSet(controllerHandle.Value);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003227 File Offset: 0x00001427
		public ControllerDigitalActionData_t GetDigitalActionData(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle)
		{
			return this.platform.ISteamController_GetDigitalActionData(controllerHandle.Value, digitalActionHandle.Value);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003240 File Offset: 0x00001440
		public ControllerDigitalActionHandle_t GetDigitalActionHandle(string pszActionName)
		{
			return this.platform.ISteamController_GetDigitalActionHandle(pszActionName);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000324E File Offset: 0x0000144E
		public int GetDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, out ControllerActionOrigin originsOut)
		{
			return this.platform.ISteamController_GetDigitalActionOrigins(controllerHandle.Value, actionSetHandle.Value, digitalActionHandle.Value, out originsOut);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000326F File Offset: 0x0000146F
		public int GetGamepadIndexForController(ControllerHandle_t ulControllerHandle)
		{
			return this.platform.ISteamController_GetGamepadIndexForController(ulControllerHandle.Value);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003282 File Offset: 0x00001482
		public string GetGlyphForActionOrigin(ControllerActionOrigin eOrigin)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamController_GetGlyphForActionOrigin(eOrigin));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003295 File Offset: 0x00001495
		public SteamInputType GetInputTypeForHandle(ControllerHandle_t controllerHandle)
		{
			return this.platform.ISteamController_GetInputTypeForHandle(controllerHandle.Value);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000032A8 File Offset: 0x000014A8
		public ControllerMotionData_t GetMotionData(ControllerHandle_t controllerHandle)
		{
			return this.platform.ISteamController_GetMotionData(controllerHandle.Value);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032BB File Offset: 0x000014BB
		public string GetStringForActionOrigin(ControllerActionOrigin eOrigin)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamController_GetStringForActionOrigin(eOrigin));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032CE File Offset: 0x000014CE
		public bool Init()
		{
			return this.platform.ISteamController_Init();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032DB File Offset: 0x000014DB
		public void RunFrame()
		{
			this.platform.ISteamController_RunFrame();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032E8 File Offset: 0x000014E8
		public void SetLEDColor(ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			this.platform.ISteamController_SetLEDColor(controllerHandle.Value, nColorR, nColorG, nColorB, nFlags);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003301 File Offset: 0x00001501
		public bool ShowAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle, float flScale, float flXPosition, float flYPosition)
		{
			return this.platform.ISteamController_ShowAnalogActionOrigins(controllerHandle.Value, analogActionHandle.Value, flScale, flXPosition, flYPosition);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000331F File Offset: 0x0000151F
		public bool ShowBindingPanel(ControllerHandle_t controllerHandle)
		{
			return this.platform.ISteamController_ShowBindingPanel(controllerHandle.Value);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003332 File Offset: 0x00001532
		public bool ShowDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle, float flScale, float flXPosition, float flYPosition)
		{
			return this.platform.ISteamController_ShowDigitalActionOrigins(controllerHandle.Value, digitalActionHandle.Value, flScale, flXPosition, flYPosition);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003350 File Offset: 0x00001550
		public bool Shutdown()
		{
			return this.platform.ISteamController_Shutdown();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000335D File Offset: 0x0000155D
		public void StopAnalogActionMomentum(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction)
		{
			this.platform.ISteamController_StopAnalogActionMomentum(controllerHandle.Value, eAction.Value);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003376 File Offset: 0x00001576
		public void TriggerHapticPulse(ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			this.platform.ISteamController_TriggerHapticPulse(controllerHandle.Value, eTargetPad, usDurationMicroSec);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000338B File Offset: 0x0000158B
		public void TriggerRepeatedHapticPulse(ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			this.platform.ISteamController_TriggerRepeatedHapticPulse(controllerHandle.Value, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000033A6 File Offset: 0x000015A6
		public void TriggerVibration(ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			this.platform.ISteamController_TriggerVibration(controllerHandle.Value, usLeftSpeed, usRightSpeed);
		}

		// Token: 0x04000468 RID: 1128
		internal Platform.Interface platform;

		// Token: 0x04000469 RID: 1129
		internal BaseSteamworks steamworks;
	}
}
