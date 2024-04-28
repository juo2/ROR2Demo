using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000508 RID: 1288
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00020B94 File Offset: 0x0001ED94
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00020B9C File Offset: 0x0001ED9C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00020BDC File Offset: 0x0001EDDC
		public bool IsAccountInfoPresent
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsAccountInfoPresent, out result);
				return result;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public ExternalAccountType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00020C00 File Offset: 0x0001EE00
		public string AccountId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_AccountId, out result);
				return result;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00020C1C File Offset: 0x0001EE1C
		public string Platform
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Platform, out result);
				return result;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00020C38 File Offset: 0x0001EE38
		public string DeviceType
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DeviceType, out result);
				return result;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00020C54 File Offset: 0x0001EE54
		public string ClientId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ClientId, out result);
				return result;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00020C70 File Offset: 0x0001EE70
		public string ProductId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ProductId, out result);
				return result;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00020C8C File Offset: 0x0001EE8C
		public string SandboxId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SandboxId, out result);
				return result;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		public string DeploymentId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DeploymentId, out result);
				return result;
			}
		}

		// Token: 0x04000E5B RID: 3675
		private Result m_ResultCode;

		// Token: 0x04000E5C RID: 3676
		private IntPtr m_ClientData;

		// Token: 0x04000E5D RID: 3677
		private IntPtr m_ProductUserId;

		// Token: 0x04000E5E RID: 3678
		private int m_IsAccountInfoPresent;

		// Token: 0x04000E5F RID: 3679
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000E60 RID: 3680
		private IntPtr m_AccountId;

		// Token: 0x04000E61 RID: 3681
		private IntPtr m_Platform;

		// Token: 0x04000E62 RID: 3682
		private IntPtr m_DeviceType;

		// Token: 0x04000E63 RID: 3683
		private IntPtr m_ClientId;

		// Token: 0x04000E64 RID: 3684
		private IntPtr m_ProductId;

		// Token: 0x04000E65 RID: 3685
		private IntPtr m_SandboxId;

		// Token: 0x04000E66 RID: 3686
		private IntPtr m_DeploymentId;
	}
}
