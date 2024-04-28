using System;

namespace Facepunch.Steamworks.Callbacks
{
	// Token: 0x02000185 RID: 389
	public enum Result
	{
		// Token: 0x040008B8 RID: 2232
		OK = 1,
		// Token: 0x040008B9 RID: 2233
		Fail,
		// Token: 0x040008BA RID: 2234
		NoConnection,
		// Token: 0x040008BB RID: 2235
		InvalidPassword = 5,
		// Token: 0x040008BC RID: 2236
		LoggedInElsewhere,
		// Token: 0x040008BD RID: 2237
		InvalidProtocolVer,
		// Token: 0x040008BE RID: 2238
		InvalidParam,
		// Token: 0x040008BF RID: 2239
		FileNotFound,
		// Token: 0x040008C0 RID: 2240
		Busy,
		// Token: 0x040008C1 RID: 2241
		InvalidState,
		// Token: 0x040008C2 RID: 2242
		InvalidName,
		// Token: 0x040008C3 RID: 2243
		InvalidEmail,
		// Token: 0x040008C4 RID: 2244
		DuplicateName,
		// Token: 0x040008C5 RID: 2245
		AccessDenied,
		// Token: 0x040008C6 RID: 2246
		Timeout,
		// Token: 0x040008C7 RID: 2247
		Banned,
		// Token: 0x040008C8 RID: 2248
		AccountNotFound,
		// Token: 0x040008C9 RID: 2249
		InvalidSteamID,
		// Token: 0x040008CA RID: 2250
		ServiceUnavailable,
		// Token: 0x040008CB RID: 2251
		NotLoggedOn,
		// Token: 0x040008CC RID: 2252
		Pending,
		// Token: 0x040008CD RID: 2253
		EncryptionFailure,
		// Token: 0x040008CE RID: 2254
		InsufficientPrivilege,
		// Token: 0x040008CF RID: 2255
		LimitExceeded,
		// Token: 0x040008D0 RID: 2256
		Revoked,
		// Token: 0x040008D1 RID: 2257
		Expired,
		// Token: 0x040008D2 RID: 2258
		AlreadyRedeemed,
		// Token: 0x040008D3 RID: 2259
		DuplicateRequest,
		// Token: 0x040008D4 RID: 2260
		AlreadyOwned,
		// Token: 0x040008D5 RID: 2261
		IPNotFound,
		// Token: 0x040008D6 RID: 2262
		PersistFailed,
		// Token: 0x040008D7 RID: 2263
		LockingFailed,
		// Token: 0x040008D8 RID: 2264
		LogonSessionReplaced,
		// Token: 0x040008D9 RID: 2265
		ConnectFailed,
		// Token: 0x040008DA RID: 2266
		HandshakeFailed,
		// Token: 0x040008DB RID: 2267
		IOFailure,
		// Token: 0x040008DC RID: 2268
		RemoteDisconnect,
		// Token: 0x040008DD RID: 2269
		ShoppingCartNotFound,
		// Token: 0x040008DE RID: 2270
		Blocked,
		// Token: 0x040008DF RID: 2271
		Ignored,
		// Token: 0x040008E0 RID: 2272
		NoMatch,
		// Token: 0x040008E1 RID: 2273
		AccountDisabled,
		// Token: 0x040008E2 RID: 2274
		ServiceReadOnly,
		// Token: 0x040008E3 RID: 2275
		AccountNotFeatured,
		// Token: 0x040008E4 RID: 2276
		AdministratorOK,
		// Token: 0x040008E5 RID: 2277
		ContentVersion,
		// Token: 0x040008E6 RID: 2278
		TryAnotherCM,
		// Token: 0x040008E7 RID: 2279
		PasswordRequiredToKickSession,
		// Token: 0x040008E8 RID: 2280
		AlreadyLoggedInElsewhere,
		// Token: 0x040008E9 RID: 2281
		Suspended,
		// Token: 0x040008EA RID: 2282
		Cancelled,
		// Token: 0x040008EB RID: 2283
		DataCorruption,
		// Token: 0x040008EC RID: 2284
		DiskFull,
		// Token: 0x040008ED RID: 2285
		RemoteCallFailed,
		// Token: 0x040008EE RID: 2286
		PasswordUnset,
		// Token: 0x040008EF RID: 2287
		ExternalAccountUnlinked,
		// Token: 0x040008F0 RID: 2288
		PSNTicketInvalid,
		// Token: 0x040008F1 RID: 2289
		ExternalAccountAlreadyLinked,
		// Token: 0x040008F2 RID: 2290
		RemoteFileConflict,
		// Token: 0x040008F3 RID: 2291
		IllegalPassword,
		// Token: 0x040008F4 RID: 2292
		SameAsPreviousValue,
		// Token: 0x040008F5 RID: 2293
		AccountLogonDenied,
		// Token: 0x040008F6 RID: 2294
		CannotUseOldPassword,
		// Token: 0x040008F7 RID: 2295
		InvalidLoginAuthCode,
		// Token: 0x040008F8 RID: 2296
		AccountLogonDeniedNoMail,
		// Token: 0x040008F9 RID: 2297
		HardwareNotCapableOfIPT,
		// Token: 0x040008FA RID: 2298
		IPTInitError,
		// Token: 0x040008FB RID: 2299
		ParentalControlRestricted,
		// Token: 0x040008FC RID: 2300
		FacebookQueryError,
		// Token: 0x040008FD RID: 2301
		ExpiredLoginAuthCode,
		// Token: 0x040008FE RID: 2302
		IPLoginRestrictionFailed,
		// Token: 0x040008FF RID: 2303
		AccountLockedDown,
		// Token: 0x04000900 RID: 2304
		AccountLogonDeniedVerifiedEmailRequired,
		// Token: 0x04000901 RID: 2305
		NoMatchingURL,
		// Token: 0x04000902 RID: 2306
		BadResponse,
		// Token: 0x04000903 RID: 2307
		RequirePasswordReEntry,
		// Token: 0x04000904 RID: 2308
		ValueOutOfRange,
		// Token: 0x04000905 RID: 2309
		UnexpectedError,
		// Token: 0x04000906 RID: 2310
		Disabled,
		// Token: 0x04000907 RID: 2311
		InvalidCEGSubmission,
		// Token: 0x04000908 RID: 2312
		RestrictedDevice,
		// Token: 0x04000909 RID: 2313
		RegionLocked,
		// Token: 0x0400090A RID: 2314
		RateLimitExceeded,
		// Token: 0x0400090B RID: 2315
		AccountLoginDeniedNeedTwoFactor,
		// Token: 0x0400090C RID: 2316
		ItemDeleted,
		// Token: 0x0400090D RID: 2317
		AccountLoginDeniedThrottle,
		// Token: 0x0400090E RID: 2318
		TwoFactorCodeMismatch,
		// Token: 0x0400090F RID: 2319
		TwoFactorActivationCodeMismatch,
		// Token: 0x04000910 RID: 2320
		AccountAssociatedToMultiplePartners,
		// Token: 0x04000911 RID: 2321
		NotModified,
		// Token: 0x04000912 RID: 2322
		NoMobileDevice,
		// Token: 0x04000913 RID: 2323
		TimeNotSynced,
		// Token: 0x04000914 RID: 2324
		SmsCodeFailed,
		// Token: 0x04000915 RID: 2325
		AccountLimitExceeded,
		// Token: 0x04000916 RID: 2326
		AccountActivityLimitExceeded,
		// Token: 0x04000917 RID: 2327
		PhoneActivityLimitExceeded,
		// Token: 0x04000918 RID: 2328
		RefundToWallet,
		// Token: 0x04000919 RID: 2329
		EmailSendFailure,
		// Token: 0x0400091A RID: 2330
		NotSettled,
		// Token: 0x0400091B RID: 2331
		NeedCaptcha,
		// Token: 0x0400091C RID: 2332
		GSLTDenied,
		// Token: 0x0400091D RID: 2333
		GSOwnerDenied,
		// Token: 0x0400091E RID: 2334
		InvalidItemType,
		// Token: 0x0400091F RID: 2335
		IPBanned
	}
}
