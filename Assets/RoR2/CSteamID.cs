using System;
using System.Globalization;
using Epic.OnlineServices;
using RoR2;

// Token: 0x02000016 RID: 22
public struct CSteamID : IEquatable<CSteamID>, IEquatable<UserID>
{
	// Token: 0x06000057 RID: 87 RVA: 0x00003461 File Offset: 0x00001661
	public CSteamID(ProductUserId egsValue)
	{
		this.stringValue = egsValue.ToString();
		this.steamValue = 0UL;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003477 File Offset: 0x00001677
	public CSteamID(string egsValue)
	{
		this.stringValue = egsValue;
		this.steamValue = 0UL;
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000059 RID: 89 RVA: 0x00003488 File Offset: 0x00001688
	public ProductUserId egsValue
	{
		get
		{
			return ProductUserId.FromString(this.stringValue);
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003495 File Offset: 0x00001695
	public CSteamID(ulong value)
	{
		this.steamValue = value;
		this.stringValue = null;
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600005B RID: 91 RVA: 0x000034A5 File Offset: 0x000016A5
	public bool isValid
	{
		get
		{
			return this.isSteam || this.isEGS;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600005C RID: 92 RVA: 0x000034B7 File Offset: 0x000016B7
	public bool isSteam
	{
		get
		{
			return this.steamValue > 0UL;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600005D RID: 93 RVA: 0x000034C3 File Offset: 0x000016C3
	public bool isEGS
	{
		get
		{
			return this.stringValue != null;
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000034CE File Offset: 0x000016CE
	public override string ToString()
	{
		if (this.value != null)
		{
			return this.value.ToString();
		}
		return string.Empty;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000034EC File Offset: 0x000016EC
	private static bool ParseFromString(string str, out CSteamID result)
	{
		if (str[0] <= '9')
		{
			ulong num;
			bool flag = TextSerialization.TryParseInvariant(str, out num);
			if (flag)
			{
				result = new CSteamID(flag ? num : 0UL);
				return flag;
			}
		}
		result = CSteamID.nil;
		if (PlatformSystems.EgsToggleConVar.value == 1 && str.Length <= 32)
		{
			bool flag;
			ProductUserId productUserId = ProductUserId.FromString(str, out flag);
			if (flag && productUserId != null)
			{
				result = new CSteamID(productUserId);
				return true;
			}
		}
		return false;
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000060 RID: 96 RVA: 0x0000356B File Offset: 0x0000176B
	public object value
	{
		get
		{
			if (this.steamValue != 0UL)
			{
				return this.steamValue;
			}
			if (this.stringValue != null)
			{
				return this.egsValue;
			}
			return null;
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003591 File Offset: 0x00001791
	public static bool operator ==(CSteamID a, CSteamID b)
	{
		return a.Equals(b);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000359B File Offset: 0x0000179B
	public static bool operator !=(CSteamID a, CSteamID b)
	{
		return !a.Equals(b);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000035A8 File Offset: 0x000017A8
	public static bool operator ==(CSteamID a, UserID b)
	{
		return a.Equals(b);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000035B2 File Offset: 0x000017B2
	public static bool operator !=(CSteamID a, UserID b)
	{
		return !a.Equals(b);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000035BF File Offset: 0x000017BF
	public override bool Equals(object obj)
	{
		return (obj is CSteamID && this.Equals((CSteamID)obj)) || (obj is UserID && this.Equals((UserID)obj));
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000035EF File Offset: 0x000017EF
	public bool Equals(CSteamID other)
	{
		return this.steamValue == other.steamValue && this.stringValue == other.stringValue;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003612 File Offset: 0x00001812
	public bool Equals(UserID other)
	{
		return this.isSteam && this.steamValue == other.ID;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x0000362D File Offset: 0x0000182D
	public override int GetHashCode()
	{
		if (this.value != null)
		{
			return this.value.GetHashCode();
		}
		return 0;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003644 File Offset: 0x00001844
	public static bool TryParse(string str, out CSteamID result)
	{
		if (!string.IsNullOrEmpty(str))
		{
			return CSteamID.ParseFromString(str, out result);
		}
		result = CSteamID.nil;
		return false;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003664 File Offset: 0x00001864
	private uint GetBitField(int bitOffset, int bitCount)
	{
		uint num = uint.MaxValue >> 32 - bitCount;
		return (uint)(this.steamValue >> bitOffset) & num;
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600006B RID: 107 RVA: 0x00003689 File Offset: 0x00001889
	public uint accountId
	{
		get
		{
			return this.GetBitField(0, 32);
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600006C RID: 108 RVA: 0x00003694 File Offset: 0x00001894
	public uint accountInstance
	{
		get
		{
			return this.GetBitField(32, 20);
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600006D RID: 109 RVA: 0x000036A0 File Offset: 0x000018A0
	public CSteamID.EAccountType accountType
	{
		get
		{
			return (CSteamID.EAccountType)this.GetBitField(52, 4);
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600006E RID: 110 RVA: 0x000036AB File Offset: 0x000018AB
	public CSteamID.EUniverse universe
	{
		get
		{
			return (CSteamID.EUniverse)this.GetBitField(56, 8);
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600006F RID: 111 RVA: 0x000036B6 File Offset: 0x000018B6
	public bool isLobby
	{
		get
		{
			return this.accountType == CSteamID.EAccountType.k_EAccountTypeChat;
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000036C4 File Offset: 0x000018C4
	public string ToSteamID()
	{
		uint accountId = this.accountId;
		return string.Format(CultureInfo.InvariantCulture, "STEAM_{0}:{1}:{2}", (uint)this.universe, accountId & 1U, accountId >> 1);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003704 File Offset: 0x00001904
	public string ToCommunityID()
	{
		char c = 'I';
		switch (this.accountType)
		{
		case CSteamID.EAccountType.k_EAccountTypeInvalid:
			c = 'I';
			break;
		case CSteamID.EAccountType.k_EAccountTypeIndividual:
			c = 'U';
			break;
		case CSteamID.EAccountType.k_EAccountTypeMultiseat:
			c = 'M';
			break;
		case CSteamID.EAccountType.k_EAccountTypeGameServer:
			c = 'G';
			break;
		case CSteamID.EAccountType.k_EAccountTypeAnonGameServer:
			c = 'A';
			break;
		case CSteamID.EAccountType.k_EAccountTypePending:
			c = 'P';
			break;
		case CSteamID.EAccountType.k_EAccountTypeContentServer:
			c = 'C';
			break;
		case CSteamID.EAccountType.k_EAccountTypeClan:
			c = 'g';
			break;
		case CSteamID.EAccountType.k_EAccountTypeChat:
			c = 'T';
			break;
		case CSteamID.EAccountType.k_EAccountTypeConsoleUser:
			c = 'I';
			break;
		case CSteamID.EAccountType.k_EAccountTypeAnonUser:
			c = 'a';
			break;
		case CSteamID.EAccountType.k_EAccountTypeMax:
			c = 'I';
			break;
		}
		return string.Format(CultureInfo.InvariantCulture, "[{0}:{1}:{2}]", c, 1, this.accountId);
	}

	// Token: 0x04000067 RID: 103
	public readonly string stringValue;

	// Token: 0x04000068 RID: 104
	public readonly ulong steamValue;

	// Token: 0x04000069 RID: 105
	public static readonly CSteamID nil;

	// Token: 0x02000017 RID: 23
	public enum EAccountType
	{
		// Token: 0x0400006B RID: 107
		k_EAccountTypeInvalid,
		// Token: 0x0400006C RID: 108
		k_EAccountTypeIndividual,
		// Token: 0x0400006D RID: 109
		k_EAccountTypeMultiseat,
		// Token: 0x0400006E RID: 110
		k_EAccountTypeGameServer,
		// Token: 0x0400006F RID: 111
		k_EAccountTypeAnonGameServer,
		// Token: 0x04000070 RID: 112
		k_EAccountTypePending,
		// Token: 0x04000071 RID: 113
		k_EAccountTypeContentServer,
		// Token: 0x04000072 RID: 114
		k_EAccountTypeClan,
		// Token: 0x04000073 RID: 115
		k_EAccountTypeChat,
		// Token: 0x04000074 RID: 116
		k_EAccountTypeConsoleUser,
		// Token: 0x04000075 RID: 117
		k_EAccountTypeAnonUser,
		// Token: 0x04000076 RID: 118
		k_EAccountTypeMax
	}

	// Token: 0x02000018 RID: 24
	public enum EUniverse
	{
		// Token: 0x04000078 RID: 120
		k_EUniverseInvalid,
		// Token: 0x04000079 RID: 121
		k_EUniversePublic,
		// Token: 0x0400007A RID: 122
		k_EUniverseBeta,
		// Token: 0x0400007B RID: 123
		k_EUniverseInternal,
		// Token: 0x0400007C RID: 124
		k_EUniverseDev,
		// Token: 0x0400007D RID: 125
		k_EUniverseMax
	}
}
