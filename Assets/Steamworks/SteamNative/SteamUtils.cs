using System;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200006E RID: 110
	internal class SteamUtils : IDisposable
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00007F84 File Offset: 0x00006184
		internal SteamUtils(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00008001 File Offset: 0x00006201
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00008018 File Offset: 0x00006218
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00008034 File Offset: 0x00006234
		public bool BOverlayNeedsPresent()
		{
			return this.platform.ISteamUtils_BOverlayNeedsPresent();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00008044 File Offset: 0x00006244
		public CallbackHandle CheckFileSignature(string szFileName, Action<CheckFileSignature_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUtils_CheckFileSignature(szFileName);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return CheckFileSignature_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00008082 File Offset: 0x00006282
		public SteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t hSteamAPICall)
		{
			return this.platform.ISteamUtils_GetAPICallFailureReason(hSteamAPICall.Value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008095 File Offset: 0x00006295
		public bool GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, ref bool pbFailed)
		{
			return this.platform.ISteamUtils_GetAPICallResult(hSteamAPICall.Value, pCallback, cubCallback, iCallbackExpected, ref pbFailed);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000080AE File Offset: 0x000062AE
		public uint GetAppID()
		{
			return this.platform.ISteamUtils_GetAppID();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000080BB File Offset: 0x000062BB
		public Universe GetConnectedUniverse()
		{
			return this.platform.ISteamUtils_GetConnectedUniverse();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000080C8 File Offset: 0x000062C8
		public bool GetCSERIPPort(out uint unIP, out ushort usPort)
		{
			return this.platform.ISteamUtils_GetCSERIPPort(out unIP, out usPort);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000080D7 File Offset: 0x000062D7
		public byte GetCurrentBatteryPower()
		{
			return this.platform.ISteamUtils_GetCurrentBatteryPower();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000080E4 File Offset: 0x000062E4
		public string GetEnteredGamepadTextInput()
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchText = 4096U;
			if (!this.platform.ISteamUtils_GetEnteredGamepadTextInput(stringBuilder, cchText))
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008114 File Offset: 0x00006314
		public uint GetEnteredGamepadTextLength()
		{
			return this.platform.ISteamUtils_GetEnteredGamepadTextLength();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008121 File Offset: 0x00006321
		public bool GetImageRGBA(int iImage, IntPtr pubDest, int nDestBufferSize)
		{
			return this.platform.ISteamUtils_GetImageRGBA(iImage, pubDest, nDestBufferSize);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008131 File Offset: 0x00006331
		public bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
		{
			return this.platform.ISteamUtils_GetImageSize(iImage, out pnWidth, out pnHeight);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008141 File Offset: 0x00006341
		public uint GetIPCCallCount()
		{
			return this.platform.ISteamUtils_GetIPCCallCount();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000814E File Offset: 0x0000634E
		public string GetIPCountry()
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamUtils_GetIPCountry());
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008160 File Offset: 0x00006360
		public uint GetSecondsSinceAppActive()
		{
			return this.platform.ISteamUtils_GetSecondsSinceAppActive();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000816D File Offset: 0x0000636D
		public uint GetSecondsSinceComputerActive()
		{
			return this.platform.ISteamUtils_GetSecondsSinceComputerActive();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000817A File Offset: 0x0000637A
		public uint GetServerRealTime()
		{
			return this.platform.ISteamUtils_GetServerRealTime();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008187 File Offset: 0x00006387
		public string GetSteamUILanguage()
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamUtils_GetSteamUILanguage());
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00008199 File Offset: 0x00006399
		public bool IsAPICallCompleted(SteamAPICall_t hSteamAPICall, ref bool pbFailed)
		{
			return this.platform.ISteamUtils_IsAPICallCompleted(hSteamAPICall.Value, ref pbFailed);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000081AD File Offset: 0x000063AD
		public bool IsOverlayEnabled()
		{
			return this.platform.ISteamUtils_IsOverlayEnabled();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000081BA File Offset: 0x000063BA
		public bool IsSteamInBigPictureMode()
		{
			return this.platform.ISteamUtils_IsSteamInBigPictureMode();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000081C7 File Offset: 0x000063C7
		public bool IsSteamRunningInVR()
		{
			return this.platform.ISteamUtils_IsSteamRunningInVR();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000081D4 File Offset: 0x000063D4
		public bool IsVRHeadsetStreamingEnabled()
		{
			return this.platform.ISteamUtils_IsVRHeadsetStreamingEnabled();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000081E1 File Offset: 0x000063E1
		public void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			this.platform.ISteamUtils_SetOverlayNotificationInset(nHorizontalInset, nVerticalInset);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000081F0 File Offset: 0x000063F0
		public void SetOverlayNotificationPosition(NotificationPosition eNotificationPosition)
		{
			this.platform.ISteamUtils_SetOverlayNotificationPosition(eNotificationPosition);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000081FE File Offset: 0x000063FE
		public void SetVRHeadsetStreamingEnabled(bool bEnabled)
		{
			this.platform.ISteamUtils_SetVRHeadsetStreamingEnabled(bEnabled);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000820C File Offset: 0x0000640C
		public void SetWarningMessageHook(IntPtr pFunction)
		{
			this.platform.ISteamUtils_SetWarningMessageHook(pFunction);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000821A File Offset: 0x0000641A
		public bool ShowGamepadTextInput(GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText)
		{
			return this.platform.ISteamUtils_ShowGamepadTextInput(eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000822E File Offset: 0x0000642E
		public void StartVRDashboard()
		{
			this.platform.ISteamUtils_StartVRDashboard();
		}

		// Token: 0x0400048C RID: 1164
		internal Platform.Interface platform;

		// Token: 0x0400048D RID: 1165
		internal BaseSteamworks steamworks;
	}
}
