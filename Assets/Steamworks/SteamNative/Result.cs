using System;

namespace SteamNative
{
	// Token: 0x0200000A RID: 10
	internal enum Result
	{
		// Token: 0x04000063 RID: 99
		OK = 1,
		// Token: 0x04000064 RID: 100
		Fail,
		// Token: 0x04000065 RID: 101
		NoConnection,
		// Token: 0x04000066 RID: 102
		InvalidPassword = 5,
		// Token: 0x04000067 RID: 103
		LoggedInElsewhere,
		// Token: 0x04000068 RID: 104
		InvalidProtocolVer,
		// Token: 0x04000069 RID: 105
		InvalidParam,
		// Token: 0x0400006A RID: 106
		FileNotFound,
		// Token: 0x0400006B RID: 107
		Busy,
		// Token: 0x0400006C RID: 108
		InvalidState,
		// Token: 0x0400006D RID: 109
		InvalidName,
		// Token: 0x0400006E RID: 110
		InvalidEmail,
		// Token: 0x0400006F RID: 111
		DuplicateName,
		// Token: 0x04000070 RID: 112
		AccessDenied,
		// Token: 0x04000071 RID: 113
		Timeout,
		// Token: 0x04000072 RID: 114
		Banned,
		// Token: 0x04000073 RID: 115
		AccountNotFound,
		// Token: 0x04000074 RID: 116
		InvalidSteamID,
		// Token: 0x04000075 RID: 117
		ServiceUnavailable,
		// Token: 0x04000076 RID: 118
		NotLoggedOn,
		// Token: 0x04000077 RID: 119
		Pending,
		// Token: 0x04000078 RID: 120
		EncryptionFailure,
		// Token: 0x04000079 RID: 121
		InsufficientPrivilege,
		// Token: 0x0400007A RID: 122
		LimitExceeded,
		// Token: 0x0400007B RID: 123
		Revoked,
		// Token: 0x0400007C RID: 124
		Expired,
		// Token: 0x0400007D RID: 125
		AlreadyRedeemed,
		// Token: 0x0400007E RID: 126
		DuplicateRequest,
		// Token: 0x0400007F RID: 127
		AlreadyOwned,
		// Token: 0x04000080 RID: 128
		IPNotFound,
		// Token: 0x04000081 RID: 129
		PersistFailed,
		// Token: 0x04000082 RID: 130
		LockingFailed,
		// Token: 0x04000083 RID: 131
		LogonSessionReplaced,
		// Token: 0x04000084 RID: 132
		ConnectFailed,
		// Token: 0x04000085 RID: 133
		HandshakeFailed,
		// Token: 0x04000086 RID: 134
		IOFailure,
		// Token: 0x04000087 RID: 135
		RemoteDisconnect,
		// Token: 0x04000088 RID: 136
		ShoppingCartNotFound,
		// Token: 0x04000089 RID: 137
		Blocked,
		// Token: 0x0400008A RID: 138
		Ignored,
		// Token: 0x0400008B RID: 139
		NoMatch,
		// Token: 0x0400008C RID: 140
		AccountDisabled,
		// Token: 0x0400008D RID: 141
		ServiceReadOnly,
		// Token: 0x0400008E RID: 142
		AccountNotFeatured,
		// Token: 0x0400008F RID: 143
		AdministratorOK,
		// Token: 0x04000090 RID: 144
		ContentVersion,
		// Token: 0x04000091 RID: 145
		TryAnotherCM,
		// Token: 0x04000092 RID: 146
		PasswordRequiredToKickSession,
		// Token: 0x04000093 RID: 147
		AlreadyLoggedInElsewhere,
		// Token: 0x04000094 RID: 148
		Suspended,
		// Token: 0x04000095 RID: 149
		Cancelled,
		// Token: 0x04000096 RID: 150
		DataCorruption,
		// Token: 0x04000097 RID: 151
		DiskFull,
		// Token: 0x04000098 RID: 152
		RemoteCallFailed,
		// Token: 0x04000099 RID: 153
		PasswordUnset,
		// Token: 0x0400009A RID: 154
		ExternalAccountUnlinked,
		// Token: 0x0400009B RID: 155
		PSNTicketInvalid,
		// Token: 0x0400009C RID: 156
		ExternalAccountAlreadyLinked,
		// Token: 0x0400009D RID: 157
		RemoteFileConflict,
		// Token: 0x0400009E RID: 158
		IllegalPassword,
		// Token: 0x0400009F RID: 159
		SameAsPreviousValue,
		// Token: 0x040000A0 RID: 160
		AccountLogonDenied,
		// Token: 0x040000A1 RID: 161
		CannotUseOldPassword,
		// Token: 0x040000A2 RID: 162
		InvalidLoginAuthCode,
		// Token: 0x040000A3 RID: 163
		AccountLogonDeniedNoMail,
		// Token: 0x040000A4 RID: 164
		HardwareNotCapableOfIPT,
		// Token: 0x040000A5 RID: 165
		IPTInitError,
		// Token: 0x040000A6 RID: 166
		ParentalControlRestricted,
		// Token: 0x040000A7 RID: 167
		FacebookQueryError,
		// Token: 0x040000A8 RID: 168
		ExpiredLoginAuthCode,
		// Token: 0x040000A9 RID: 169
		IPLoginRestrictionFailed,
		// Token: 0x040000AA RID: 170
		AccountLockedDown,
		// Token: 0x040000AB RID: 171
		AccountLogonDeniedVerifiedEmailRequired,
		// Token: 0x040000AC RID: 172
		NoMatchingURL,
		// Token: 0x040000AD RID: 173
		BadResponse,
		// Token: 0x040000AE RID: 174
		RequirePasswordReEntry,
		// Token: 0x040000AF RID: 175
		ValueOutOfRange,
		// Token: 0x040000B0 RID: 176
		UnexpectedError,
		// Token: 0x040000B1 RID: 177
		Disabled,
		// Token: 0x040000B2 RID: 178
		InvalidCEGSubmission,
		// Token: 0x040000B3 RID: 179
		RestrictedDevice,
		// Token: 0x040000B4 RID: 180
		RegionLocked,
		// Token: 0x040000B5 RID: 181
		RateLimitExceeded,
		// Token: 0x040000B6 RID: 182
		AccountLoginDeniedNeedTwoFactor,
		// Token: 0x040000B7 RID: 183
		ItemDeleted,
		// Token: 0x040000B8 RID: 184
		AccountLoginDeniedThrottle,
		// Token: 0x040000B9 RID: 185
		TwoFactorCodeMismatch,
		// Token: 0x040000BA RID: 186
		TwoFactorActivationCodeMismatch,
		// Token: 0x040000BB RID: 187
		AccountAssociatedToMultiplePartners,
		// Token: 0x040000BC RID: 188
		NotModified,
		// Token: 0x040000BD RID: 189
		NoMobileDevice,
		// Token: 0x040000BE RID: 190
		TimeNotSynced,
		// Token: 0x040000BF RID: 191
		SmsCodeFailed,
		// Token: 0x040000C0 RID: 192
		AccountLimitExceeded,
		// Token: 0x040000C1 RID: 193
		AccountActivityLimitExceeded,
		// Token: 0x040000C2 RID: 194
		PhoneActivityLimitExceeded,
		// Token: 0x040000C3 RID: 195
		RefundToWallet,
		// Token: 0x040000C4 RID: 196
		EmailSendFailure,
		// Token: 0x040000C5 RID: 197
		NotSettled,
		// Token: 0x040000C6 RID: 198
		NeedCaptcha,
		// Token: 0x040000C7 RID: 199
		GSLTDenied,
		// Token: 0x040000C8 RID: 200
		GSOwnerDenied,
		// Token: 0x040000C9 RID: 201
		InvalidItemType,
		// Token: 0x040000CA RID: 202
		IPBanned,
		// Token: 0x040000CB RID: 203
		GSLTExpired,
		// Token: 0x040000CC RID: 204
		InsufficientFunds,
		// Token: 0x040000CD RID: 205
		TooManyPending,
		// Token: 0x040000CE RID: 206
		NoSiteLicensesFound,
		// Token: 0x040000CF RID: 207
		WGNetworkSendExceeded,
		// Token: 0x040000D0 RID: 208
		AccountNotFriends,
		// Token: 0x040000D1 RID: 209
		LimitedUserAccount
	}
}
