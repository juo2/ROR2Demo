using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000061 RID: 97
	internal class SteamHTTP : IDisposable
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000044A0 File Offset: 0x000026A0
		internal SteamHTTP(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000451D File Offset: 0x0000271D
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004534 File Offset: 0x00002734
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004550 File Offset: 0x00002750
		public HTTPCookieContainerHandle CreateCookieContainer(bool bAllowResponsesToModify)
		{
			return this.platform.ISteamHTTP_CreateCookieContainer(bAllowResponsesToModify);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000455E File Offset: 0x0000275E
		public HTTPRequestHandle CreateHTTPRequest(HTTPMethod eHTTPRequestMethod, string pchAbsoluteURL)
		{
			return this.platform.ISteamHTTP_CreateHTTPRequest(eHTTPRequestMethod, pchAbsoluteURL);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000456D File Offset: 0x0000276D
		public bool DeferHTTPRequest(HTTPRequestHandle hRequest)
		{
			return this.platform.ISteamHTTP_DeferHTTPRequest(hRequest.Value);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004580 File Offset: 0x00002780
		public bool GetHTTPDownloadProgressPct(HTTPRequestHandle hRequest, out float pflPercentOut)
		{
			return this.platform.ISteamHTTP_GetHTTPDownloadProgressPct(hRequest.Value, out pflPercentOut);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004594 File Offset: 0x00002794
		public bool GetHTTPRequestWasTimedOut(HTTPRequestHandle hRequest, ref bool pbWasTimedOut)
		{
			return this.platform.ISteamHTTP_GetHTTPRequestWasTimedOut(hRequest.Value, ref pbWasTimedOut);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000045A8 File Offset: 0x000027A8
		public bool GetHTTPResponseBodyData(HTTPRequestHandle hRequest, out byte pBodyDataBuffer, uint unBufferSize)
		{
			return this.platform.ISteamHTTP_GetHTTPResponseBodyData(hRequest.Value, out pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000045BD File Offset: 0x000027BD
		public bool GetHTTPResponseBodySize(HTTPRequestHandle hRequest, out uint unBodySize)
		{
			return this.platform.ISteamHTTP_GetHTTPResponseBodySize(hRequest.Value, out unBodySize);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000045D1 File Offset: 0x000027D1
		public bool GetHTTPResponseHeaderSize(HTTPRequestHandle hRequest, string pchHeaderName, out uint unResponseHeaderSize)
		{
			return this.platform.ISteamHTTP_GetHTTPResponseHeaderSize(hRequest.Value, pchHeaderName, out unResponseHeaderSize);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000045E6 File Offset: 0x000027E6
		public bool GetHTTPResponseHeaderValue(HTTPRequestHandle hRequest, string pchHeaderName, out byte pHeaderValueBuffer, uint unBufferSize)
		{
			return this.platform.ISteamHTTP_GetHTTPResponseHeaderValue(hRequest.Value, pchHeaderName, out pHeaderValueBuffer, unBufferSize);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000045FD File Offset: 0x000027FD
		public bool GetHTTPStreamingResponseBodyData(HTTPRequestHandle hRequest, uint cOffset, out byte pBodyDataBuffer, uint unBufferSize)
		{
			return this.platform.ISteamHTTP_GetHTTPStreamingResponseBodyData(hRequest.Value, cOffset, out pBodyDataBuffer, unBufferSize);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004614 File Offset: 0x00002814
		public bool PrioritizeHTTPRequest(HTTPRequestHandle hRequest)
		{
			return this.platform.ISteamHTTP_PrioritizeHTTPRequest(hRequest.Value);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004627 File Offset: 0x00002827
		public bool ReleaseCookieContainer(HTTPCookieContainerHandle hCookieContainer)
		{
			return this.platform.ISteamHTTP_ReleaseCookieContainer(hCookieContainer.Value);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000463A File Offset: 0x0000283A
		public bool ReleaseHTTPRequest(HTTPRequestHandle hRequest)
		{
			return this.platform.ISteamHTTP_ReleaseHTTPRequest(hRequest.Value);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000464D File Offset: 0x0000284D
		public bool SendHTTPRequest(HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle)
		{
			return this.platform.ISteamHTTP_SendHTTPRequest(hRequest.Value, ref pCallHandle.Value);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004666 File Offset: 0x00002866
		public bool SendHTTPRequestAndStreamResponse(HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle)
		{
			return this.platform.ISteamHTTP_SendHTTPRequestAndStreamResponse(hRequest.Value, ref pCallHandle.Value);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000467F File Offset: 0x0000287F
		public bool SetCookie(HTTPCookieContainerHandle hCookieContainer, string pchHost, string pchUrl, string pchCookie)
		{
			return this.platform.ISteamHTTP_SetCookie(hCookieContainer.Value, pchHost, pchUrl, pchCookie);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004696 File Offset: 0x00002896
		public bool SetHTTPRequestAbsoluteTimeoutMS(HTTPRequestHandle hRequest, uint unMilliseconds)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(hRequest.Value, unMilliseconds);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000046AA File Offset: 0x000028AA
		public bool SetHTTPRequestContextValue(HTTPRequestHandle hRequest, ulong ulContextValue)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestContextValue(hRequest.Value, ulContextValue);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000046BE File Offset: 0x000028BE
		public bool SetHTTPRequestCookieContainer(HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestCookieContainer(hRequest.Value, hCookieContainer.Value);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000046D7 File Offset: 0x000028D7
		public bool SetHTTPRequestGetOrPostParameter(HTTPRequestHandle hRequest, string pchParamName, string pchParamValue)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestGetOrPostParameter(hRequest.Value, pchParamName, pchParamValue);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000046EC File Offset: 0x000028EC
		public bool SetHTTPRequestHeaderValue(HTTPRequestHandle hRequest, string pchHeaderName, string pchHeaderValue)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestHeaderValue(hRequest.Value, pchHeaderName, pchHeaderValue);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004701 File Offset: 0x00002901
		public bool SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle hRequest, uint unTimeoutSeconds)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(hRequest.Value, unTimeoutSeconds);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004715 File Offset: 0x00002915
		public bool SetHTTPRequestRawPostBody(HTTPRequestHandle hRequest, string pchContentType, out byte pubBody, uint unBodyLen)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestRawPostBody(hRequest.Value, pchContentType, out pubBody, unBodyLen);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000472C File Offset: 0x0000292C
		public bool SetHTTPRequestRequiresVerifiedCertificate(HTTPRequestHandle hRequest, bool bRequireVerifiedCertificate)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(hRequest.Value, bRequireVerifiedCertificate);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004740 File Offset: 0x00002940
		public bool SetHTTPRequestUserAgentInfo(HTTPRequestHandle hRequest, string pchUserAgentInfo)
		{
			return this.platform.ISteamHTTP_SetHTTPRequestUserAgentInfo(hRequest.Value, pchUserAgentInfo);
		}

		// Token: 0x04000472 RID: 1138
		internal Platform.Interface platform;

		// Token: 0x04000473 RID: 1139
		internal BaseSteamworks steamworks;
	}
}
