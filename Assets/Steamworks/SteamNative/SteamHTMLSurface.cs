using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000060 RID: 96
	internal class SteamHTMLSurface : IDisposable
	{
		// Token: 0x06000120 RID: 288 RVA: 0x000040FC File Offset: 0x000022FC
		internal SteamHTMLSurface(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004179 File Offset: 0x00002379
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004190 File Offset: 0x00002390
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000041AC File Offset: 0x000023AC
		public void AddHeader(HHTMLBrowser unBrowserHandle, string pchKey, string pchValue)
		{
			this.platform.ISteamHTMLSurface_AddHeader(unBrowserHandle.Value, pchKey, pchValue);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000041C1 File Offset: 0x000023C1
		public void AllowStartRequest(HHTMLBrowser unBrowserHandle, bool bAllowed)
		{
			this.platform.ISteamHTMLSurface_AllowStartRequest(unBrowserHandle.Value, bAllowed);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000041D5 File Offset: 0x000023D5
		public void CopyToClipboard(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_CopyToClipboard(unBrowserHandle.Value);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000041E8 File Offset: 0x000023E8
		public CallbackHandle CreateBrowser(string pchUserAgent, string pchUserCSS, Action<HTML_BrowserReady_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamHTMLSurface_CreateBrowser(pchUserAgent, pchUserCSS);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return HTML_BrowserReady_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004227 File Offset: 0x00002427
		public void DestructISteamHTMLSurface()
		{
			this.platform.ISteamHTMLSurface_DestructISteamHTMLSurface();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004234 File Offset: 0x00002434
		public void ExecuteJavascript(HHTMLBrowser unBrowserHandle, string pchScript)
		{
			this.platform.ISteamHTMLSurface_ExecuteJavascript(unBrowserHandle.Value, pchScript);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004248 File Offset: 0x00002448
		public void Find(HHTMLBrowser unBrowserHandle, string pchSearchStr, bool bCurrentlyInFind, bool bReverse)
		{
			this.platform.ISteamHTMLSurface_Find(unBrowserHandle.Value, pchSearchStr, bCurrentlyInFind, bReverse);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000425F File Offset: 0x0000245F
		public void GetLinkAtPosition(HHTMLBrowser unBrowserHandle, int x, int y)
		{
			this.platform.ISteamHTMLSurface_GetLinkAtPosition(unBrowserHandle.Value, x, y);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004274 File Offset: 0x00002474
		public void GoBack(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_GoBack(unBrowserHandle.Value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004287 File Offset: 0x00002487
		public void GoForward(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_GoForward(unBrowserHandle.Value);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000429A File Offset: 0x0000249A
		public bool Init()
		{
			return this.platform.ISteamHTMLSurface_Init();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000042A7 File Offset: 0x000024A7
		public void JSDialogResponse(HHTMLBrowser unBrowserHandle, bool bResult)
		{
			this.platform.ISteamHTMLSurface_JSDialogResponse(unBrowserHandle.Value, bResult);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000042BB File Offset: 0x000024BB
		public void KeyChar(HHTMLBrowser unBrowserHandle, uint cUnicodeChar, HTMLKeyModifiers eHTMLKeyModifiers)
		{
			this.platform.ISteamHTMLSurface_KeyChar(unBrowserHandle.Value, cUnicodeChar, eHTMLKeyModifiers);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000042D0 File Offset: 0x000024D0
		public void KeyDown(HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, HTMLKeyModifiers eHTMLKeyModifiers)
		{
			this.platform.ISteamHTMLSurface_KeyDown(unBrowserHandle.Value, nNativeKeyCode, eHTMLKeyModifiers);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000042E5 File Offset: 0x000024E5
		public void KeyUp(HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, HTMLKeyModifiers eHTMLKeyModifiers)
		{
			this.platform.ISteamHTMLSurface_KeyUp(unBrowserHandle.Value, nNativeKeyCode, eHTMLKeyModifiers);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000042FA File Offset: 0x000024FA
		public void LoadURL(HHTMLBrowser unBrowserHandle, string pchURL, string pchPostData)
		{
			this.platform.ISteamHTMLSurface_LoadURL(unBrowserHandle.Value, pchURL, pchPostData);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000430F File Offset: 0x0000250F
		public void MouseDoubleClick(HHTMLBrowser unBrowserHandle, HTMLMouseButton eMouseButton)
		{
			this.platform.ISteamHTMLSurface_MouseDoubleClick(unBrowserHandle.Value, eMouseButton);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004323 File Offset: 0x00002523
		public void MouseDown(HHTMLBrowser unBrowserHandle, HTMLMouseButton eMouseButton)
		{
			this.platform.ISteamHTMLSurface_MouseDown(unBrowserHandle.Value, eMouseButton);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004337 File Offset: 0x00002537
		public void MouseMove(HHTMLBrowser unBrowserHandle, int x, int y)
		{
			this.platform.ISteamHTMLSurface_MouseMove(unBrowserHandle.Value, x, y);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000434C File Offset: 0x0000254C
		public void MouseUp(HHTMLBrowser unBrowserHandle, HTMLMouseButton eMouseButton)
		{
			this.platform.ISteamHTMLSurface_MouseUp(unBrowserHandle.Value, eMouseButton);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004360 File Offset: 0x00002560
		public void MouseWheel(HHTMLBrowser unBrowserHandle, int nDelta)
		{
			this.platform.ISteamHTMLSurface_MouseWheel(unBrowserHandle.Value, nDelta);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004374 File Offset: 0x00002574
		public void PasteFromClipboard(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_PasteFromClipboard(unBrowserHandle.Value);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004387 File Offset: 0x00002587
		public void Reload(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_Reload(unBrowserHandle.Value);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000439A File Offset: 0x0000259A
		public void RemoveBrowser(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_RemoveBrowser(unBrowserHandle.Value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000043AD File Offset: 0x000025AD
		public void SetBackgroundMode(HHTMLBrowser unBrowserHandle, bool bBackgroundMode)
		{
			this.platform.ISteamHTMLSurface_SetBackgroundMode(unBrowserHandle.Value, bBackgroundMode);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000043C1 File Offset: 0x000025C1
		public void SetCookie(string pchHostname, string pchKey, string pchValue, string pchPath, RTime32 nExpires, bool bSecure, bool bHTTPOnly)
		{
			this.platform.ISteamHTMLSurface_SetCookie(pchHostname, pchKey, pchValue, pchPath, nExpires.Value, bSecure, bHTTPOnly);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000043DE File Offset: 0x000025DE
		public void SetDPIScalingFactor(HHTMLBrowser unBrowserHandle, float flDPIScaling)
		{
			this.platform.ISteamHTMLSurface_SetDPIScalingFactor(unBrowserHandle.Value, flDPIScaling);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000043F2 File Offset: 0x000025F2
		public void SetHorizontalScroll(HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll)
		{
			this.platform.ISteamHTMLSurface_SetHorizontalScroll(unBrowserHandle.Value, nAbsolutePixelScroll);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004406 File Offset: 0x00002606
		public void SetKeyFocus(HHTMLBrowser unBrowserHandle, bool bHasKeyFocus)
		{
			this.platform.ISteamHTMLSurface_SetKeyFocus(unBrowserHandle.Value, bHasKeyFocus);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000441A File Offset: 0x0000261A
		public void SetPageScaleFactor(HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY)
		{
			this.platform.ISteamHTMLSurface_SetPageScaleFactor(unBrowserHandle.Value, flZoom, nPointX, nPointY);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004431 File Offset: 0x00002631
		public void SetSize(HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight)
		{
			this.platform.ISteamHTMLSurface_SetSize(unBrowserHandle.Value, unWidth, unHeight);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004446 File Offset: 0x00002646
		public void SetVerticalScroll(HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll)
		{
			this.platform.ISteamHTMLSurface_SetVerticalScroll(unBrowserHandle.Value, nAbsolutePixelScroll);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000445A File Offset: 0x0000265A
		public bool Shutdown()
		{
			return this.platform.ISteamHTMLSurface_Shutdown();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004467 File Offset: 0x00002667
		public void StopFind(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_StopFind(unBrowserHandle.Value);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000447A File Offset: 0x0000267A
		public void StopLoad(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_StopLoad(unBrowserHandle.Value);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000448D File Offset: 0x0000268D
		public void ViewSource(HHTMLBrowser unBrowserHandle)
		{
			this.platform.ISteamHTMLSurface_ViewSource(unBrowserHandle.Value);
		}

		// Token: 0x04000470 RID: 1136
		internal Platform.Interface platform;

		// Token: 0x04000471 RID: 1137
		internal BaseSteamworks steamworks;
	}
}
