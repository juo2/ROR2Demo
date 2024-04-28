using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200001E RID: 30
	public enum Result
	{
		// Token: 0x04000054 RID: 84
		Success,
		// Token: 0x04000055 RID: 85
		NoConnection,
		// Token: 0x04000056 RID: 86
		InvalidCredentials,
		// Token: 0x04000057 RID: 87
		InvalidUser,
		// Token: 0x04000058 RID: 88
		InvalidAuth,
		// Token: 0x04000059 RID: 89
		AccessDenied,
		// Token: 0x0400005A RID: 90
		MissingPermissions,
		// Token: 0x0400005B RID: 91
		TokenNotAccount,
		// Token: 0x0400005C RID: 92
		TooManyRequests,
		// Token: 0x0400005D RID: 93
		AlreadyPending,
		// Token: 0x0400005E RID: 94
		InvalidParameters,
		// Token: 0x0400005F RID: 95
		InvalidRequest,
		// Token: 0x04000060 RID: 96
		UnrecognizedResponse,
		// Token: 0x04000061 RID: 97
		IncompatibleVersion,
		// Token: 0x04000062 RID: 98
		NotConfigured,
		// Token: 0x04000063 RID: 99
		AlreadyConfigured,
		// Token: 0x04000064 RID: 100
		NotImplemented,
		// Token: 0x04000065 RID: 101
		Canceled,
		// Token: 0x04000066 RID: 102
		NotFound,
		// Token: 0x04000067 RID: 103
		OperationWillRetry,
		// Token: 0x04000068 RID: 104
		NoChange,
		// Token: 0x04000069 RID: 105
		VersionMismatch,
		// Token: 0x0400006A RID: 106
		LimitExceeded,
		// Token: 0x0400006B RID: 107
		Disabled,
		// Token: 0x0400006C RID: 108
		DuplicateNotAllowed,
		// Token: 0x0400006D RID: 109
		MissingParametersDEPRECATED,
		// Token: 0x0400006E RID: 110
		InvalidSandboxId,
		// Token: 0x0400006F RID: 111
		TimedOut,
		// Token: 0x04000070 RID: 112
		PartialResult,
		// Token: 0x04000071 RID: 113
		MissingRole,
		// Token: 0x04000072 RID: 114
		MissingFeature,
		// Token: 0x04000073 RID: 115
		InvalidSandbox,
		// Token: 0x04000074 RID: 116
		InvalidDeployment,
		// Token: 0x04000075 RID: 117
		InvalidProduct,
		// Token: 0x04000076 RID: 118
		InvalidProductUserID,
		// Token: 0x04000077 RID: 119
		ServiceFailure,
		// Token: 0x04000078 RID: 120
		CacheDirectoryMissing,
		// Token: 0x04000079 RID: 121
		CacheDirectoryInvalid,
		// Token: 0x0400007A RID: 122
		InvalidState,
		// Token: 0x0400007B RID: 123
		RequestInProgress,
		// Token: 0x0400007C RID: 124
		AuthAccountLocked = 1001,
		// Token: 0x0400007D RID: 125
		AuthAccountLockedForUpdate,
		// Token: 0x0400007E RID: 126
		AuthInvalidRefreshToken,
		// Token: 0x0400007F RID: 127
		AuthInvalidToken,
		// Token: 0x04000080 RID: 128
		AuthAuthenticationFailure,
		// Token: 0x04000081 RID: 129
		AuthInvalidPlatformToken,
		// Token: 0x04000082 RID: 130
		AuthWrongAccount,
		// Token: 0x04000083 RID: 131
		AuthWrongClient,
		// Token: 0x04000084 RID: 132
		AuthFullAccountRequired,
		// Token: 0x04000085 RID: 133
		AuthHeadlessAccountRequired,
		// Token: 0x04000086 RID: 134
		AuthPasswordResetRequired,
		// Token: 0x04000087 RID: 135
		AuthPasswordCannotBeReused,
		// Token: 0x04000088 RID: 136
		AuthExpired,
		// Token: 0x04000089 RID: 137
		AuthScopeConsentRequired,
		// Token: 0x0400008A RID: 138
		AuthApplicationNotFound,
		// Token: 0x0400008B RID: 139
		AuthScopeNotFound,
		// Token: 0x0400008C RID: 140
		AuthAccountFeatureRestricted,
		// Token: 0x0400008D RID: 141
		AuthPinGrantCode = 1020,
		// Token: 0x0400008E RID: 142
		AuthPinGrantExpired,
		// Token: 0x0400008F RID: 143
		AuthPinGrantPending,
		// Token: 0x04000090 RID: 144
		AuthExternalAuthNotLinked = 1030,
		// Token: 0x04000091 RID: 145
		AuthExternalAuthRevoked = 1032,
		// Token: 0x04000092 RID: 146
		AuthExternalAuthInvalid,
		// Token: 0x04000093 RID: 147
		AuthExternalAuthRestricted,
		// Token: 0x04000094 RID: 148
		AuthExternalAuthCannotLogin,
		// Token: 0x04000095 RID: 149
		AuthExternalAuthExpired,
		// Token: 0x04000096 RID: 150
		AuthExternalAuthIsLastLoginType,
		// Token: 0x04000097 RID: 151
		AuthExchangeCodeNotFound = 1040,
		// Token: 0x04000098 RID: 152
		AuthOriginatingExchangeCodeSessionExpired,
		// Token: 0x04000099 RID: 153
		AuthPersistentAuthAccountNotActive = 1050,
		// Token: 0x0400009A RID: 154
		AuthMFARequired = 1060,
		// Token: 0x0400009B RID: 155
		AuthParentalControls = 1070,
		// Token: 0x0400009C RID: 156
		AuthNoRealId = 1080,
		// Token: 0x0400009D RID: 157
		FriendsInviteAwaitingAcceptance = 2000,
		// Token: 0x0400009E RID: 158
		FriendsNoInvitation,
		// Token: 0x0400009F RID: 159
		FriendsAlreadyFriends = 2003,
		// Token: 0x040000A0 RID: 160
		FriendsNotFriends,
		// Token: 0x040000A1 RID: 161
		FriendsTargetUserTooManyInvites,
		// Token: 0x040000A2 RID: 162
		FriendsLocalUserTooManyInvites,
		// Token: 0x040000A3 RID: 163
		FriendsTargetUserFriendLimitExceeded,
		// Token: 0x040000A4 RID: 164
		FriendsLocalUserFriendLimitExceeded,
		// Token: 0x040000A5 RID: 165
		PresenceDataInvalid = 3000,
		// Token: 0x040000A6 RID: 166
		PresenceDataLengthInvalid,
		// Token: 0x040000A7 RID: 167
		PresenceDataKeyInvalid,
		// Token: 0x040000A8 RID: 168
		PresenceDataKeyLengthInvalid,
		// Token: 0x040000A9 RID: 169
		PresenceDataValueInvalid,
		// Token: 0x040000AA RID: 170
		PresenceDataValueLengthInvalid,
		// Token: 0x040000AB RID: 171
		PresenceRichTextInvalid,
		// Token: 0x040000AC RID: 172
		PresenceRichTextLengthInvalid,
		// Token: 0x040000AD RID: 173
		PresenceStatusInvalid,
		// Token: 0x040000AE RID: 174
		EcomEntitlementStale = 4000,
		// Token: 0x040000AF RID: 175
		EcomCatalogOfferStale,
		// Token: 0x040000B0 RID: 176
		EcomCatalogItemStale,
		// Token: 0x040000B1 RID: 177
		EcomCatalogOfferPriceInvalid,
		// Token: 0x040000B2 RID: 178
		EcomCheckoutLoadError,
		// Token: 0x040000B3 RID: 179
		SessionsSessionInProgress = 5000,
		// Token: 0x040000B4 RID: 180
		SessionsTooManyPlayers,
		// Token: 0x040000B5 RID: 181
		SessionsNoPermission,
		// Token: 0x040000B6 RID: 182
		SessionsSessionAlreadyExists,
		// Token: 0x040000B7 RID: 183
		SessionsInvalidLock,
		// Token: 0x040000B8 RID: 184
		SessionsInvalidSession,
		// Token: 0x040000B9 RID: 185
		SessionsSandboxNotAllowed,
		// Token: 0x040000BA RID: 186
		SessionsInviteFailed,
		// Token: 0x040000BB RID: 187
		SessionsInviteNotFound,
		// Token: 0x040000BC RID: 188
		SessionsUpsertNotAllowed,
		// Token: 0x040000BD RID: 189
		SessionsAggregationFailed,
		// Token: 0x040000BE RID: 190
		SessionsHostAtCapacity,
		// Token: 0x040000BF RID: 191
		SessionsSandboxAtCapacity,
		// Token: 0x040000C0 RID: 192
		SessionsSessionNotAnonymous,
		// Token: 0x040000C1 RID: 193
		SessionsOutOfSync,
		// Token: 0x040000C2 RID: 194
		SessionsTooManyInvites,
		// Token: 0x040000C3 RID: 195
		SessionsPresenceSessionExists,
		// Token: 0x040000C4 RID: 196
		SessionsDeploymentAtCapacity,
		// Token: 0x040000C5 RID: 197
		SessionsNotAllowed,
		// Token: 0x040000C6 RID: 198
		SessionsPlayerSanctioned,
		// Token: 0x040000C7 RID: 199
		PlayerDataStorageFilenameInvalid = 6000,
		// Token: 0x040000C8 RID: 200
		PlayerDataStorageFilenameLengthInvalid,
		// Token: 0x040000C9 RID: 201
		PlayerDataStorageFilenameInvalidChars,
		// Token: 0x040000CA RID: 202
		PlayerDataStorageFileSizeTooLarge,
		// Token: 0x040000CB RID: 203
		PlayerDataStorageFileSizeInvalid,
		// Token: 0x040000CC RID: 204
		PlayerDataStorageFileHandleInvalid,
		// Token: 0x040000CD RID: 205
		PlayerDataStorageDataInvalid,
		// Token: 0x040000CE RID: 206
		PlayerDataStorageDataLengthInvalid,
		// Token: 0x040000CF RID: 207
		PlayerDataStorageStartIndexInvalid,
		// Token: 0x040000D0 RID: 208
		PlayerDataStorageRequestInProgress,
		// Token: 0x040000D1 RID: 209
		PlayerDataStorageUserThrottled,
		// Token: 0x040000D2 RID: 210
		PlayerDataStorageEncryptionKeyNotSet,
		// Token: 0x040000D3 RID: 211
		PlayerDataStorageUserErrorFromDataCallback,
		// Token: 0x040000D4 RID: 212
		PlayerDataStorageFileHeaderHasNewerVersion,
		// Token: 0x040000D5 RID: 213
		PlayerDataStorageFileCorrupted,
		// Token: 0x040000D6 RID: 214
		ConnectExternalTokenValidationFailed = 7000,
		// Token: 0x040000D7 RID: 215
		ConnectUserAlreadyExists,
		// Token: 0x040000D8 RID: 216
		ConnectAuthExpired,
		// Token: 0x040000D9 RID: 217
		ConnectInvalidToken,
		// Token: 0x040000DA RID: 218
		ConnectUnsupportedTokenType,
		// Token: 0x040000DB RID: 219
		ConnectLinkAccountFailed,
		// Token: 0x040000DC RID: 220
		ConnectExternalServiceUnavailable,
		// Token: 0x040000DD RID: 221
		ConnectExternalServiceConfigurationFailure,
		// Token: 0x040000DE RID: 222
		ConnectLinkAccountFailedMissingNintendoIdAccountDEPRECATED,
		// Token: 0x040000DF RID: 223
		SocialOverlayLoadError = 8000,
		// Token: 0x040000E0 RID: 224
		LobbyNotOwner = 9000,
		// Token: 0x040000E1 RID: 225
		LobbyInvalidLock,
		// Token: 0x040000E2 RID: 226
		LobbyLobbyAlreadyExists,
		// Token: 0x040000E3 RID: 227
		LobbySessionInProgress,
		// Token: 0x040000E4 RID: 228
		LobbyTooManyPlayers,
		// Token: 0x040000E5 RID: 229
		LobbyNoPermission,
		// Token: 0x040000E6 RID: 230
		LobbyInvalidSession,
		// Token: 0x040000E7 RID: 231
		LobbySandboxNotAllowed,
		// Token: 0x040000E8 RID: 232
		LobbyInviteFailed,
		// Token: 0x040000E9 RID: 233
		LobbyInviteNotFound,
		// Token: 0x040000EA RID: 234
		LobbyUpsertNotAllowed,
		// Token: 0x040000EB RID: 235
		LobbyAggregationFailed,
		// Token: 0x040000EC RID: 236
		LobbyHostAtCapacity,
		// Token: 0x040000ED RID: 237
		LobbySandboxAtCapacity,
		// Token: 0x040000EE RID: 238
		LobbyTooManyInvites,
		// Token: 0x040000EF RID: 239
		LobbyDeploymentAtCapacity,
		// Token: 0x040000F0 RID: 240
		LobbyNotAllowed,
		// Token: 0x040000F1 RID: 241
		LobbyMemberUpdateOnly,
		// Token: 0x040000F2 RID: 242
		LobbyPresenceLobbyExists,
		// Token: 0x040000F3 RID: 243
		TitleStorageUserErrorFromDataCallback = 10000,
		// Token: 0x040000F4 RID: 244
		TitleStorageEncryptionKeyNotSet,
		// Token: 0x040000F5 RID: 245
		TitleStorageFileCorrupted,
		// Token: 0x040000F6 RID: 246
		TitleStorageFileHeaderHasNewerVersion,
		// Token: 0x040000F7 RID: 247
		ModsModSdkProcessIsAlreadyRunning = 11000,
		// Token: 0x040000F8 RID: 248
		ModsModSdkCommandIsEmpty,
		// Token: 0x040000F9 RID: 249
		ModsModSdkProcessCreationFailed,
		// Token: 0x040000FA RID: 250
		ModsCriticalError,
		// Token: 0x040000FB RID: 251
		ModsToolInternalError,
		// Token: 0x040000FC RID: 252
		ModsIPCFailure,
		// Token: 0x040000FD RID: 253
		ModsInvalidIPCResponse,
		// Token: 0x040000FE RID: 254
		ModsURILaunchFailure,
		// Token: 0x040000FF RID: 255
		ModsModIsNotInstalled,
		// Token: 0x04000100 RID: 256
		ModsUserDoesNotOwnTheGame,
		// Token: 0x04000101 RID: 257
		ModsOfferRequestByIdInvalidResult,
		// Token: 0x04000102 RID: 258
		ModsCouldNotFindOffer,
		// Token: 0x04000103 RID: 259
		ModsOfferRequestByIdFailure,
		// Token: 0x04000104 RID: 260
		ModsPurchaseFailure,
		// Token: 0x04000105 RID: 261
		ModsInvalidGameInstallInfo,
		// Token: 0x04000106 RID: 262
		ModsCannotGetManifestLocation,
		// Token: 0x04000107 RID: 263
		ModsUnsupportedOS,
		// Token: 0x04000108 RID: 264
		AntiCheatClientProtectionNotAvailable = 12000,
		// Token: 0x04000109 RID: 265
		AntiCheatInvalidMode,
		// Token: 0x0400010A RID: 266
		AntiCheatClientProductIdMismatch,
		// Token: 0x0400010B RID: 267
		AntiCheatClientSandboxIdMismatch,
		// Token: 0x0400010C RID: 268
		AntiCheatProtectMessageSessionKeyRequired,
		// Token: 0x0400010D RID: 269
		AntiCheatProtectMessageValidationFailed,
		// Token: 0x0400010E RID: 270
		AntiCheatProtectMessageInitializationFailed,
		// Token: 0x0400010F RID: 271
		AntiCheatPeerAlreadyRegistered,
		// Token: 0x04000110 RID: 272
		AntiCheatPeerNotFound,
		// Token: 0x04000111 RID: 273
		AntiCheatPeerNotProtected,
		// Token: 0x04000112 RID: 274
		AntiCheatClientDeploymentIdMismatch,
		// Token: 0x04000113 RID: 275
		AntiCheatDeviceIdAuthIsNotSupported,
		// Token: 0x04000114 RID: 276
		TooManyParticipants = 13000,
		// Token: 0x04000115 RID: 277
		RoomAlreadyExists,
		// Token: 0x04000116 RID: 278
		UserKicked,
		// Token: 0x04000117 RID: 279
		UserBanned,
		// Token: 0x04000118 RID: 280
		RoomWasLeft,
		// Token: 0x04000119 RID: 281
		ReconnectionTimegateExpired,
		// Token: 0x0400011A RID: 282
		ProgressionSnapshotSnapshotIdUnavailable = 14000,
		// Token: 0x0400011B RID: 283
		ParentEmailMissing = 15000,
		// Token: 0x0400011C RID: 284
		UserGraduated,
		// Token: 0x0400011D RID: 285
		AndroidJavaVMNotStored = 17000,
		// Token: 0x0400011E RID: 286
		UnexpectedError = 2147483647
	}
}
