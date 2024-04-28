using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000547 RID: 1351
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000228ED File Offset: 0x00020AED
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x000228F8 File Offset: 0x00020AF8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00022914 File Offset: 0x00020B14
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0002291C File Offset: 0x00020B1C
		public string ApplicationId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ApplicationId, out result);
				return result;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x00022938 File Offset: 0x00020B38
		public string ClientId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ClientId, out result);
				return result;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00022954 File Offset: 0x00020B54
		public string ProductId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ProductId, out result);
				return result;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00022970 File Offset: 0x00020B70
		public string SandboxId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SandboxId, out result);
				return result;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0002298C File Offset: 0x00020B8C
		public string DeploymentId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DeploymentId, out result);
				return result;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000229A8 File Offset: 0x00020BA8
		public string DisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DisplayName, out result);
				return result;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000229C4 File Offset: 0x00020BC4
		public bool IsExternalAccountInfoPresent
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsExternalAccountInfoPresent, out result);
				return result;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000229E0 File Offset: 0x00020BE0
		public ExternalAccountType ExternalAccountIdType
		{
			get
			{
				return this.m_ExternalAccountIdType;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000229E8 File Offset: 0x00020BE8
		public string ExternalAccountId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ExternalAccountId, out result);
				return result;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00022A04 File Offset: 0x00020C04
		public string ExternalAccountDisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ExternalAccountDisplayName, out result);
				return result;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00022A20 File Offset: 0x00020C20
		public string Platform
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Platform, out result);
				return result;
			}
		}

		// Token: 0x04000F1C RID: 3868
		private Result m_ResultCode;

		// Token: 0x04000F1D RID: 3869
		private IntPtr m_ClientData;

		// Token: 0x04000F1E RID: 3870
		private IntPtr m_ApplicationId;

		// Token: 0x04000F1F RID: 3871
		private IntPtr m_ClientId;

		// Token: 0x04000F20 RID: 3872
		private IntPtr m_ProductId;

		// Token: 0x04000F21 RID: 3873
		private IntPtr m_SandboxId;

		// Token: 0x04000F22 RID: 3874
		private IntPtr m_DeploymentId;

		// Token: 0x04000F23 RID: 3875
		private IntPtr m_DisplayName;

		// Token: 0x04000F24 RID: 3876
		private int m_IsExternalAccountInfoPresent;

		// Token: 0x04000F25 RID: 3877
		private ExternalAccountType m_ExternalAccountIdType;

		// Token: 0x04000F26 RID: 3878
		private IntPtr m_ExternalAccountId;

		// Token: 0x04000F27 RID: 3879
		private IntPtr m_ExternalAccountDisplayName;

		// Token: 0x04000F28 RID: 3880
		private IntPtr m_Platform;
	}
}
