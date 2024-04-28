using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000211 RID: 529
	public class Info : ISettable
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0000EC6B File Offset: 0x0000CE6B
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0000EC73 File Offset: 0x0000CE73
		public Status Status { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public EpicAccountId UserId { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0000EC95 File Offset: 0x0000CE95
		public string ProductId { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0000EC9E File Offset: 0x0000CE9E
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0000ECA6 File Offset: 0x0000CEA6
		public string ProductVersion { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0000ECAF File Offset: 0x0000CEAF
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		public string Platform { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public string RichText { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0000ECD1 File Offset: 0x0000CED1
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0000ECD9 File Offset: 0x0000CED9
		public DataRecord[] Records { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0000ECE2 File Offset: 0x0000CEE2
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0000ECEA File Offset: 0x0000CEEA
		public string ProductName { get; set; }

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		internal void Set(InfoInternal? other)
		{
			if (other != null)
			{
				this.Status = other.Value.Status;
				this.UserId = other.Value.UserId;
				this.ProductId = other.Value.ProductId;
				this.ProductVersion = other.Value.ProductVersion;
				this.Platform = other.Value.Platform;
				this.RichText = other.Value.RichText;
				this.Records = other.Value.Records;
				this.ProductName = other.Value.ProductName;
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0000EDB5 File Offset: 0x0000CFB5
		public void Set(object other)
		{
			this.Set(other as InfoInternal?);
		}
	}
}
