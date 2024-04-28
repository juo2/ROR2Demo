using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000977 RID: 2423
	[Serializable]
	public struct NetworkUserId : IEquatable<NetworkUserId>
	{
		// Token: 0x06003700 RID: 14080 RVA: 0x000E7CCE File Offset: 0x000E5ECE
		private NetworkUserId(ulong value, byte subId)
		{
			this.value = value;
			this.subId = subId;
			this.strValue = null;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000E7CE5 File Offset: 0x000E5EE5
		public NetworkUserId(string strValue, byte subId)
		{
			this.value = 0UL;
			this.subId = subId;
			this.strValue = strValue;
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000E7D00 File Offset: 0x000E5F00
		public NetworkUserId(UserID userId, byte subId)
		{
			if (userId.CID.isSteam)
			{
				this.value = userId.CID.steamValue;
				this.strValue = null;
			}
			else
			{
				this.strValue = userId.CID.stringValue;
				this.value = 0UL;
			}
			this.subId = subId;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000E7D55 File Offset: 0x000E5F55
		public static NetworkUserId FromIp(string ip, byte subId)
		{
			return new NetworkUserId((ulong)((long)ip.GetHashCode()), subId);
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000E7D64 File Offset: 0x000E5F64
		public static NetworkUserId FromId(ulong id, byte subId)
		{
			return new NetworkUserId(id, subId);
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000E7D6D File Offset: 0x000E5F6D
		public bool HasValidValue()
		{
			return this.value != 0UL || this.strValue != null;
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000E7D82 File Offset: 0x000E5F82
		public bool Equals(NetworkUserId other)
		{
			return this.value == other.value && this.strValue == other.strValue && this.subId == other.subId;
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000E7DB5 File Offset: 0x000E5FB5
		public override bool Equals(object obj)
		{
			return obj != null && obj is NetworkUserId && this.Equals((NetworkUserId)obj);
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000E7DD4 File Offset: 0x000E5FD4
		public override int GetHashCode()
		{
			return this.value.GetHashCode() * 397 ^ this.subId.GetHashCode();
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000E7E01 File Offset: 0x000E6001
		public CSteamID steamId
		{
			get
			{
				return new CSteamID(this.value);
			}
		}

		// Token: 0x04003750 RID: 14160
		[SerializeField]
		public ulong value;

		// Token: 0x04003751 RID: 14161
		[SerializeField]
		public string strValue;

		// Token: 0x04003752 RID: 14162
		[SerializeField]
		public readonly byte subId;
	}
}
