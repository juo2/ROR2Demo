using System;
using Epic.OnlineServices;

namespace RoR2
{
	// Token: 0x020009E4 RID: 2532
	public struct UserID
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000F2A41 File Offset: 0x000F0C41
		public ulong ID
		{
			get
			{
				return this.CID.steamValue;
			}
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000F2A4E File Offset: 0x000F0C4E
		public UserID(ulong id)
		{
			this.CID = new CSteamID(id);
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000F2A5C File Offset: 0x000F0C5C
		public UserID(CSteamID cid)
		{
			this.CID = cid;
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000F2A65 File Offset: 0x000F0C65
		public static explicit operator UserID(CSteamID cid)
		{
			return new UserID(cid);
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000F2A6D File Offset: 0x000F0C6D
		public bool isValid
		{
			get
			{
				return this.CID.isValid;
			}
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000F2A7C File Offset: 0x000F0C7C
		public static bool TryParse(string str, out UserID result)
		{
			CSteamID cid;
			if (CSteamID.TryParse(str, out cid))
			{
				result = new UserID(cid);
				return true;
			}
			result.CID = CSteamID.nil;
			return false;
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000F2AAD File Offset: 0x000F0CAD
		public override string ToString()
		{
			return this.CID.ToString();
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000F2AC0 File Offset: 0x000F0CC0
		public UserID(ProductUserId id)
		{
			this.CID = new CSteamID(id);
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000F2ACE File Offset: 0x000F0CCE
		public static explicit operator UserID(ulong id)
		{
			return new UserID(id);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000F2AD8 File Offset: 0x000F0CD8
		public override bool Equals(object obj)
		{
			if (obj is UserID)
			{
				UserID first = (UserID)obj;
				return first == this;
			}
			return false;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000F2B04 File Offset: 0x000F0D04
		public override int GetHashCode()
		{
			return this.ID.GetHashCode();
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000F2B1F File Offset: 0x000F0D1F
		public static bool operator ==(UserID first, UserID second)
		{
			return first.CID == second.CID;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000F2B32 File Offset: 0x000F0D32
		public static bool operator !=(UserID first, UserID second)
		{
			return first.CID != second.CID;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000F2B45 File Offset: 0x000F0D45
		public static bool operator ==(UserID first, ulong second)
		{
			return first.ID == second;
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000F2B51 File Offset: 0x000F0D51
		public static bool operator !=(UserID first, ulong second)
		{
			return first.ID != second;
		}

		// Token: 0x04003947 RID: 14663
		private const bool useCIDforComparison = true;

		// Token: 0x04003948 RID: 14664
		public CSteamID CID;
	}
}
