using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.AntiCheatClient;
using Epic.OnlineServices.AntiCheatServer;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.CustomInvites;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.KWS;
using Epic.OnlineServices.Leaderboards;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.Logging;
using Epic.OnlineServices.Mods;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.PlayerDataStorage;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.ProgressionSnapshot;
using Epic.OnlineServices.Reports;
using Epic.OnlineServices.RTC;
using Epic.OnlineServices.RTCAdmin;
using Epic.OnlineServices.RTCAudio;
using Epic.OnlineServices.Sanctions;
using Epic.OnlineServices.Sessions;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.TitleStorage;
using Epic.OnlineServices.UI;
using Epic.OnlineServices.UserInfo;

namespace Epic.OnlineServices
{
	// Token: 0x02000011 RID: 17
	public static class Bindings
	{
		// Token: 0x06000079 RID: 121
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Achievements_AddNotifyAchievementsUnlocked(IntPtr handle, IntPtr options, IntPtr clientData, OnAchievementsUnlockedCallbackInternal notificationFn);

		// Token: 0x0600007A RID: 122
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Achievements_AddNotifyAchievementsUnlockedV2(IntPtr handle, IntPtr options, IntPtr clientData, OnAchievementsUnlockedCallbackV2Internal notificationFn);

		// Token: 0x0600007B RID: 123
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionByAchievementId(IntPtr handle, IntPtr options, ref IntPtr outDefinition);

		// Token: 0x0600007C RID: 124
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionByIndex(IntPtr handle, IntPtr options, ref IntPtr outDefinition);

		// Token: 0x0600007D RID: 125
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(IntPtr handle, IntPtr options, ref IntPtr outDefinition);

		// Token: 0x0600007E RID: 126
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByIndex(IntPtr handle, IntPtr options, ref IntPtr outDefinition);

		// Token: 0x0600007F RID: 127
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyPlayerAchievementByAchievementId(IntPtr handle, IntPtr options, ref IntPtr outAchievement);

		// Token: 0x06000080 RID: 128
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyPlayerAchievementByIndex(IntPtr handle, IntPtr options, ref IntPtr outAchievement);

		// Token: 0x06000081 RID: 129
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyUnlockedAchievementByAchievementId(IntPtr handle, IntPtr options, ref IntPtr outAchievement);

		// Token: 0x06000082 RID: 130
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Achievements_CopyUnlockedAchievementByIndex(IntPtr handle, IntPtr options, ref IntPtr outAchievement);

		// Token: 0x06000083 RID: 131
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_DefinitionV2_Release(IntPtr achievementDefinition);

		// Token: 0x06000084 RID: 132
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_Definition_Release(IntPtr achievementDefinition);

		// Token: 0x06000085 RID: 133
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Achievements_GetAchievementDefinitionCount(IntPtr handle, IntPtr options);

		// Token: 0x06000086 RID: 134
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Achievements_GetPlayerAchievementCount(IntPtr handle, IntPtr options);

		// Token: 0x06000087 RID: 135
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Achievements_GetUnlockedAchievementCount(IntPtr handle, IntPtr options);

		// Token: 0x06000088 RID: 136
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_PlayerAchievement_Release(IntPtr achievement);

		// Token: 0x06000089 RID: 137
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_QueryDefinitions(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryDefinitionsCompleteCallbackInternal completionDelegate);

		// Token: 0x0600008A RID: 138
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_QueryPlayerAchievements(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryPlayerAchievementsCompleteCallbackInternal completionDelegate);

		// Token: 0x0600008B RID: 139
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_RemoveNotifyAchievementsUnlocked(IntPtr handle, ulong inId);

		// Token: 0x0600008C RID: 140
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_UnlockAchievements(IntPtr handle, IntPtr options, IntPtr clientData, OnUnlockAchievementsCompleteCallbackInternal completionDelegate);

		// Token: 0x0600008D RID: 141
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Achievements_UnlockedAchievement_Release(IntPtr achievement);

		// Token: 0x0600008E RID: 142
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ActiveSession_CopyInfo(IntPtr handle, IntPtr options, ref IntPtr outActiveSessionInfo);

		// Token: 0x0600008F RID: 143
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_ActiveSession_GetRegisteredPlayerByIndex(IntPtr handle, IntPtr options);

		// Token: 0x06000090 RID: 144
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_ActiveSession_GetRegisteredPlayerCount(IntPtr handle, IntPtr options);

		// Token: 0x06000091 RID: 145
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_ActiveSession_Info_Release(IntPtr activeSessionInfo);

		// Token: 0x06000092 RID: 146
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_ActiveSession_Release(IntPtr activeSessionHandle);

		// Token: 0x06000093 RID: 147
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_AddExternalIntegrityCatalog(IntPtr handle, IntPtr options);

		// Token: 0x06000094 RID: 148
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatClient_AddNotifyMessageToPeer(IntPtr handle, IntPtr options, IntPtr clientData, OnMessageToPeerCallbackInternal notificationFn);

		// Token: 0x06000095 RID: 149
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatClient_AddNotifyMessageToServer(IntPtr handle, IntPtr options, IntPtr clientData, OnMessageToServerCallbackInternal notificationFn);

		// Token: 0x06000096 RID: 150
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatClient_AddNotifyPeerActionRequired(IntPtr handle, IntPtr options, IntPtr clientData, OnPeerActionRequiredCallbackInternal notificationFn);

		// Token: 0x06000097 RID: 151
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatClient_AddNotifyPeerAuthStatusChanged(IntPtr handle, IntPtr options, IntPtr clientData, OnPeerAuthStatusChangedCallbackInternal notificationFn);

		// Token: 0x06000098 RID: 152
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_BeginSession(IntPtr handle, IntPtr options);

		// Token: 0x06000099 RID: 153
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_EndSession(IntPtr handle, IntPtr options);

		// Token: 0x0600009A RID: 154
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_GetProtectMessageOutputLength(IntPtr handle, IntPtr options, ref uint outBufferSizeBytes);

		// Token: 0x0600009B RID: 155
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_PollStatus(IntPtr handle, IntPtr options, ref AntiCheatClientViolationType outViolationType, IntPtr outMessage);

		// Token: 0x0600009C RID: 156
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_ProtectMessage(IntPtr handle, IntPtr options, IntPtr outBuffer, ref uint outBytesWritten);

		// Token: 0x0600009D RID: 157
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_ReceiveMessageFromPeer(IntPtr handle, IntPtr options);

		// Token: 0x0600009E RID: 158
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_ReceiveMessageFromServer(IntPtr handle, IntPtr options);

		// Token: 0x0600009F RID: 159
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_RegisterPeer(IntPtr handle, IntPtr options);

		// Token: 0x060000A0 RID: 160
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatClient_RemoveNotifyMessageToPeer(IntPtr handle, ulong notificationId);

		// Token: 0x060000A1 RID: 161
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatClient_RemoveNotifyMessageToServer(IntPtr handle, ulong notificationId);

		// Token: 0x060000A2 RID: 162
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatClient_RemoveNotifyPeerActionRequired(IntPtr handle, ulong notificationId);

		// Token: 0x060000A3 RID: 163
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatClient_RemoveNotifyPeerAuthStatusChanged(IntPtr handle, ulong notificationId);

		// Token: 0x060000A4 RID: 164
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_UnprotectMessage(IntPtr handle, IntPtr options, IntPtr outBuffer, ref uint outBytesWritten);

		// Token: 0x060000A5 RID: 165
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatClient_UnregisterPeer(IntPtr handle, IntPtr options);

		// Token: 0x060000A6 RID: 166
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatServer_AddNotifyClientActionRequired(IntPtr handle, IntPtr options, IntPtr clientData, OnClientActionRequiredCallbackInternal notificationFn);

		// Token: 0x060000A7 RID: 167
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatServer_AddNotifyClientAuthStatusChanged(IntPtr handle, IntPtr options, IntPtr clientData, OnClientAuthStatusChangedCallbackInternal notificationFn);

		// Token: 0x060000A8 RID: 168
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_AntiCheatServer_AddNotifyMessageToClient(IntPtr handle, IntPtr options, IntPtr clientData, OnMessageToClientCallbackInternal notificationFn);

		// Token: 0x060000A9 RID: 169
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_BeginSession(IntPtr handle, IntPtr options);

		// Token: 0x060000AA RID: 170
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_EndSession(IntPtr handle, IntPtr options);

		// Token: 0x060000AB RID: 171
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_GetProtectMessageOutputLength(IntPtr handle, IntPtr options, ref uint outBufferSizeBytes);

		// Token: 0x060000AC RID: 172
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogEvent(IntPtr handle, IntPtr options);

		// Token: 0x060000AD RID: 173
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogGameRoundEnd(IntPtr handle, IntPtr options);

		// Token: 0x060000AE RID: 174
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogGameRoundStart(IntPtr handle, IntPtr options);

		// Token: 0x060000AF RID: 175
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerDespawn(IntPtr handle, IntPtr options);

		// Token: 0x060000B0 RID: 176
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerRevive(IntPtr handle, IntPtr options);

		// Token: 0x060000B1 RID: 177
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerSpawn(IntPtr handle, IntPtr options);

		// Token: 0x060000B2 RID: 178
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerTakeDamage(IntPtr handle, IntPtr options);

		// Token: 0x060000B3 RID: 179
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerTick(IntPtr handle, IntPtr options);

		// Token: 0x060000B4 RID: 180
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerUseAbility(IntPtr handle, IntPtr options);

		// Token: 0x060000B5 RID: 181
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_LogPlayerUseWeapon(IntPtr handle, IntPtr options);

		// Token: 0x060000B6 RID: 182
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_ProtectMessage(IntPtr handle, IntPtr options, IntPtr outBuffer, ref uint outBytesWritten);

		// Token: 0x060000B7 RID: 183
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_ReceiveMessageFromClient(IntPtr handle, IntPtr options);

		// Token: 0x060000B8 RID: 184
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_RegisterClient(IntPtr handle, IntPtr options);

		// Token: 0x060000B9 RID: 185
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_RegisterEvent(IntPtr handle, IntPtr options);

		// Token: 0x060000BA RID: 186
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatServer_RemoveNotifyClientActionRequired(IntPtr handle, ulong notificationId);

		// Token: 0x060000BB RID: 187
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatServer_RemoveNotifyClientAuthStatusChanged(IntPtr handle, ulong notificationId);

		// Token: 0x060000BC RID: 188
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_AntiCheatServer_RemoveNotifyMessageToClient(IntPtr handle, ulong notificationId);

		// Token: 0x060000BD RID: 189
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_SetClientDetails(IntPtr handle, IntPtr options);

		// Token: 0x060000BE RID: 190
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_SetClientNetworkState(IntPtr handle, IntPtr options);

		// Token: 0x060000BF RID: 191
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_SetGameSessionId(IntPtr handle, IntPtr options);

		// Token: 0x060000C0 RID: 192
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_UnprotectMessage(IntPtr handle, IntPtr options, IntPtr outBuffer, ref uint outBytesWritten);

		// Token: 0x060000C1 RID: 193
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_AntiCheatServer_UnregisterClient(IntPtr handle, IntPtr options);

		// Token: 0x060000C2 RID: 194
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Auth_AddNotifyLoginStatusChanged(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Auth.OnLoginStatusChangedCallbackInternal notification);

		// Token: 0x060000C3 RID: 195
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Auth_CopyIdToken(IntPtr handle, IntPtr options, ref IntPtr outIdToken);

		// Token: 0x060000C4 RID: 196
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Auth_CopyUserAuthToken(IntPtr handle, IntPtr options, IntPtr localUserId, ref IntPtr outUserAuthToken);

		// Token: 0x060000C5 RID: 197
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_DeletePersistentAuth(IntPtr handle, IntPtr options, IntPtr clientData, OnDeletePersistentAuthCallbackInternal completionDelegate);

		// Token: 0x060000C6 RID: 198
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Auth_GetLoggedInAccountByIndex(IntPtr handle, int index);

		// Token: 0x060000C7 RID: 199
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_Auth_GetLoggedInAccountsCount(IntPtr handle);

		// Token: 0x060000C8 RID: 200
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern LoginStatus EOS_Auth_GetLoginStatus(IntPtr handle, IntPtr localUserId);

		// Token: 0x060000C9 RID: 201
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Auth_GetMergedAccountByIndex(IntPtr handle, IntPtr localUserId, uint index);

		// Token: 0x060000CA RID: 202
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Auth_GetMergedAccountsCount(IntPtr handle, IntPtr localUserId);

		// Token: 0x060000CB RID: 203
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Auth_GetSelectedAccountId(IntPtr handle, IntPtr localUserId, ref IntPtr outSelectedAccountId);

		// Token: 0x060000CC RID: 204
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_IdToken_Release(IntPtr idToken);

		// Token: 0x060000CD RID: 205
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_LinkAccount(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Auth.OnLinkAccountCallbackInternal completionDelegate);

		// Token: 0x060000CE RID: 206
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_Login(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Auth.OnLoginCallbackInternal completionDelegate);

		// Token: 0x060000CF RID: 207
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_Logout(IntPtr handle, IntPtr options, IntPtr clientData, OnLogoutCallbackInternal completionDelegate);

		// Token: 0x060000D0 RID: 208
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_QueryIdToken(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryIdTokenCallbackInternal completionDelegate);

		// Token: 0x060000D1 RID: 209
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_RemoveNotifyLoginStatusChanged(IntPtr handle, ulong inId);

		// Token: 0x060000D2 RID: 210
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_Token_Release(IntPtr authToken);

		// Token: 0x060000D3 RID: 211
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_VerifyIdToken(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Auth.OnVerifyIdTokenCallbackInternal completionDelegate);

		// Token: 0x060000D4 RID: 212
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Auth_VerifyUserAuth(IntPtr handle, IntPtr options, IntPtr clientData, OnVerifyUserAuthCallbackInternal completionDelegate);

		// Token: 0x060000D5 RID: 213
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ByteArray_ToString(IntPtr byteArray, uint length, IntPtr outBuffer, ref uint inOutBufferLength);

		// Token: 0x060000D6 RID: 214
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Connect_AddNotifyAuthExpiration(IntPtr handle, IntPtr options, IntPtr clientData, OnAuthExpirationCallbackInternal notification);

		// Token: 0x060000D7 RID: 215
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Connect_AddNotifyLoginStatusChanged(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Connect.OnLoginStatusChangedCallbackInternal notification);

		// Token: 0x060000D8 RID: 216
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Connect_CopyIdToken(IntPtr handle, IntPtr options, ref IntPtr outIdToken);

		// Token: 0x060000D9 RID: 217
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Connect_CopyProductUserExternalAccountByAccountId(IntPtr handle, IntPtr options, ref IntPtr outExternalAccountInfo);

		// Token: 0x060000DA RID: 218
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Connect_CopyProductUserExternalAccountByAccountType(IntPtr handle, IntPtr options, ref IntPtr outExternalAccountInfo);

		// Token: 0x060000DB RID: 219
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Connect_CopyProductUserExternalAccountByIndex(IntPtr handle, IntPtr options, ref IntPtr outExternalAccountInfo);

		// Token: 0x060000DC RID: 220
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Connect_CopyProductUserInfo(IntPtr handle, IntPtr options, ref IntPtr outExternalAccountInfo);

		// Token: 0x060000DD RID: 221
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_CreateDeviceId(IntPtr handle, IntPtr options, IntPtr clientData, OnCreateDeviceIdCallbackInternal completionDelegate);

		// Token: 0x060000DE RID: 222
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_CreateUser(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Connect.OnCreateUserCallbackInternal completionDelegate);

		// Token: 0x060000DF RID: 223
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_DeleteDeviceId(IntPtr handle, IntPtr options, IntPtr clientData, OnDeleteDeviceIdCallbackInternal completionDelegate);

		// Token: 0x060000E0 RID: 224
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_ExternalAccountInfo_Release(IntPtr externalAccountInfo);

		// Token: 0x060000E1 RID: 225
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Connect_GetExternalAccountMapping(IntPtr handle, IntPtr options);

		// Token: 0x060000E2 RID: 226
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Connect_GetLoggedInUserByIndex(IntPtr handle, int index);

		// Token: 0x060000E3 RID: 227
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_Connect_GetLoggedInUsersCount(IntPtr handle);

		// Token: 0x060000E4 RID: 228
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern LoginStatus EOS_Connect_GetLoginStatus(IntPtr handle, IntPtr localUserId);

		// Token: 0x060000E5 RID: 229
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Connect_GetProductUserExternalAccountCount(IntPtr handle, IntPtr options);

		// Token: 0x060000E6 RID: 230
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Connect_GetProductUserIdMapping(IntPtr handle, IntPtr options, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060000E7 RID: 231
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_IdToken_Release(IntPtr idToken);

		// Token: 0x060000E8 RID: 232
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_LinkAccount(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Connect.OnLinkAccountCallbackInternal completionDelegate);

		// Token: 0x060000E9 RID: 233
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_Login(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Connect.OnLoginCallbackInternal completionDelegate);

		// Token: 0x060000EA RID: 234
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_QueryExternalAccountMappings(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryExternalAccountMappingsCallbackInternal completionDelegate);

		// Token: 0x060000EB RID: 235
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_QueryProductUserIdMappings(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryProductUserIdMappingsCallbackInternal completionDelegate);

		// Token: 0x060000EC RID: 236
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_RemoveNotifyAuthExpiration(IntPtr handle, ulong inId);

		// Token: 0x060000ED RID: 237
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_RemoveNotifyLoginStatusChanged(IntPtr handle, ulong inId);

		// Token: 0x060000EE RID: 238
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_TransferDeviceIdAccount(IntPtr handle, IntPtr options, IntPtr clientData, OnTransferDeviceIdAccountCallbackInternal completionDelegate);

		// Token: 0x060000EF RID: 239
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_UnlinkAccount(IntPtr handle, IntPtr options, IntPtr clientData, OnUnlinkAccountCallbackInternal completionDelegate);

		// Token: 0x060000F0 RID: 240
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Connect_VerifyIdToken(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Connect.OnVerifyIdTokenCallbackInternal completionDelegate);

		// Token: 0x060000F1 RID: 241
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ContinuanceToken_ToString(IntPtr continuanceToken, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060000F2 RID: 242
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_CustomInvites_AddNotifyCustomInviteAccepted(IntPtr handle, IntPtr options, IntPtr clientData, OnCustomInviteAcceptedCallbackInternal notificationFn);

		// Token: 0x060000F3 RID: 243
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_CustomInvites_AddNotifyCustomInviteReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnCustomInviteReceivedCallbackInternal notificationFn);

		// Token: 0x060000F4 RID: 244
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_CustomInvites_FinalizeInvite(IntPtr handle, IntPtr options);

		// Token: 0x060000F5 RID: 245
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_CustomInvites_RemoveNotifyCustomInviteAccepted(IntPtr handle, ulong inId);

		// Token: 0x060000F6 RID: 246
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_CustomInvites_RemoveNotifyCustomInviteReceived(IntPtr handle, ulong inId);

		// Token: 0x060000F7 RID: 247
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_CustomInvites_SendCustomInvite(IntPtr handle, IntPtr options, IntPtr clientData, OnSendCustomInviteCallbackInternal completionDelegate);

		// Token: 0x060000F8 RID: 248
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_CustomInvites_SetCustomInvite(IntPtr handle, IntPtr options);

		// Token: 0x060000F9 RID: 249
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_EResult_IsOperationComplete(Result result);

		// Token: 0x060000FA RID: 250
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_EResult_ToString(Result result);

		// Token: 0x060000FB RID: 251
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_CatalogItem_Release(IntPtr catalogItem);

		// Token: 0x060000FC RID: 252
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_CatalogOffer_Release(IntPtr catalogOffer);

		// Token: 0x060000FD RID: 253
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_CatalogRelease_Release(IntPtr catalogRelease);

		// Token: 0x060000FE RID: 254
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_Checkout(IntPtr handle, IntPtr options, IntPtr clientData, OnCheckoutCallbackInternal completionDelegate);

		// Token: 0x060000FF RID: 255
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyEntitlementById(IntPtr handle, IntPtr options, ref IntPtr outEntitlement);

		// Token: 0x06000100 RID: 256
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyEntitlementByIndex(IntPtr handle, IntPtr options, ref IntPtr outEntitlement);

		// Token: 0x06000101 RID: 257
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyEntitlementByNameAndIndex(IntPtr handle, IntPtr options, ref IntPtr outEntitlement);

		// Token: 0x06000102 RID: 258
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyItemById(IntPtr handle, IntPtr options, ref IntPtr outItem);

		// Token: 0x06000103 RID: 259
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyItemImageInfoByIndex(IntPtr handle, IntPtr options, ref IntPtr outImageInfo);

		// Token: 0x06000104 RID: 260
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyItemReleaseByIndex(IntPtr handle, IntPtr options, ref IntPtr outRelease);

		// Token: 0x06000105 RID: 261
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyOfferById(IntPtr handle, IntPtr options, ref IntPtr outOffer);

		// Token: 0x06000106 RID: 262
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyOfferByIndex(IntPtr handle, IntPtr options, ref IntPtr outOffer);

		// Token: 0x06000107 RID: 263
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyOfferImageInfoByIndex(IntPtr handle, IntPtr options, ref IntPtr outImageInfo);

		// Token: 0x06000108 RID: 264
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyOfferItemByIndex(IntPtr handle, IntPtr options, ref IntPtr outItem);

		// Token: 0x06000109 RID: 265
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyTransactionById(IntPtr handle, IntPtr options, ref IntPtr outTransaction);

		// Token: 0x0600010A RID: 266
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_CopyTransactionByIndex(IntPtr handle, IntPtr options, ref IntPtr outTransaction);

		// Token: 0x0600010B RID: 267
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_Entitlement_Release(IntPtr entitlement);

		// Token: 0x0600010C RID: 268
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetEntitlementsByNameCount(IntPtr handle, IntPtr options);

		// Token: 0x0600010D RID: 269
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetEntitlementsCount(IntPtr handle, IntPtr options);

		// Token: 0x0600010E RID: 270
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetItemImageInfoCount(IntPtr handle, IntPtr options);

		// Token: 0x0600010F RID: 271
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetItemReleaseCount(IntPtr handle, IntPtr options);

		// Token: 0x06000110 RID: 272
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetOfferCount(IntPtr handle, IntPtr options);

		// Token: 0x06000111 RID: 273
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetOfferImageInfoCount(IntPtr handle, IntPtr options);

		// Token: 0x06000112 RID: 274
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetOfferItemCount(IntPtr handle, IntPtr options);

		// Token: 0x06000113 RID: 275
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_GetTransactionCount(IntPtr handle, IntPtr options);

		// Token: 0x06000114 RID: 276
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_KeyImageInfo_Release(IntPtr keyImageInfo);

		// Token: 0x06000115 RID: 277
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_QueryEntitlements(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryEntitlementsCallbackInternal completionDelegate);

		// Token: 0x06000116 RID: 278
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_QueryOffers(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryOffersCallbackInternal completionDelegate);

		// Token: 0x06000117 RID: 279
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_QueryOwnership(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryOwnershipCallbackInternal completionDelegate);

		// Token: 0x06000118 RID: 280
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_QueryOwnershipToken(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryOwnershipTokenCallbackInternal completionDelegate);

		// Token: 0x06000119 RID: 281
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_RedeemEntitlements(IntPtr handle, IntPtr options, IntPtr clientData, OnRedeemEntitlementsCallbackInternal completionDelegate);

		// Token: 0x0600011A RID: 282
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_Transaction_CopyEntitlementByIndex(IntPtr handle, IntPtr options, ref IntPtr outEntitlement);

		// Token: 0x0600011B RID: 283
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Ecom_Transaction_GetEntitlementsCount(IntPtr handle, IntPtr options);

		// Token: 0x0600011C RID: 284
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Ecom_Transaction_GetTransactionId(IntPtr handle, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x0600011D RID: 285
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Ecom_Transaction_Release(IntPtr transaction);

		// Token: 0x0600011E RID: 286
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_EpicAccountId_FromString(IntPtr accountIdString);

		// Token: 0x0600011F RID: 287
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_EpicAccountId_IsValid(IntPtr accountId);

		// Token: 0x06000120 RID: 288
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_EpicAccountId_ToString(IntPtr accountId, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x06000121 RID: 289
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Friends_AcceptInvite(IntPtr handle, IntPtr options, IntPtr clientData, OnAcceptInviteCallbackInternal completionDelegate);

		// Token: 0x06000122 RID: 290
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Friends_AddNotifyFriendsUpdate(IntPtr handle, IntPtr options, IntPtr clientData, OnFriendsUpdateCallbackInternal friendsUpdateHandler);

		// Token: 0x06000123 RID: 291
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Friends_GetFriendAtIndex(IntPtr handle, IntPtr options);

		// Token: 0x06000124 RID: 292
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_Friends_GetFriendsCount(IntPtr handle, IntPtr options);

		// Token: 0x06000125 RID: 293
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern FriendsStatus EOS_Friends_GetStatus(IntPtr handle, IntPtr options);

		// Token: 0x06000126 RID: 294
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Friends_QueryFriends(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryFriendsCallbackInternal completionDelegate);

		// Token: 0x06000127 RID: 295
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Friends_RejectInvite(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Friends.OnRejectInviteCallbackInternal completionDelegate);

		// Token: 0x06000128 RID: 296
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Friends_RemoveNotifyFriendsUpdate(IntPtr handle, ulong notificationId);

		// Token: 0x06000129 RID: 297
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Friends_SendInvite(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Friends.OnSendInviteCallbackInternal completionDelegate);

		// Token: 0x0600012A RID: 298
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Initialize(IntPtr options);

		// Token: 0x0600012B RID: 299
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_KWS_AddNotifyPermissionsUpdateReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnPermissionsUpdateReceivedCallbackInternal notificationFn);

		// Token: 0x0600012C RID: 300
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_KWS_CopyPermissionByIndex(IntPtr handle, IntPtr options, ref IntPtr outPermission);

		// Token: 0x0600012D RID: 301
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_CreateUser(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.KWS.OnCreateUserCallbackInternal completionDelegate);

		// Token: 0x0600012E RID: 302
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_KWS_GetPermissionByKey(IntPtr handle, IntPtr options, ref KWSPermissionStatus outPermission);

		// Token: 0x0600012F RID: 303
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_KWS_GetPermissionsCount(IntPtr handle, IntPtr options);

		// Token: 0x06000130 RID: 304
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_PermissionStatus_Release(IntPtr permissionStatus);

		// Token: 0x06000131 RID: 305
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_QueryAgeGate(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryAgeGateCallbackInternal completionDelegate);

		// Token: 0x06000132 RID: 306
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_QueryPermissions(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryPermissionsCallbackInternal completionDelegate);

		// Token: 0x06000133 RID: 307
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_RemoveNotifyPermissionsUpdateReceived(IntPtr handle, ulong inId);

		// Token: 0x06000134 RID: 308
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_RequestPermissions(IntPtr handle, IntPtr options, IntPtr clientData, OnRequestPermissionsCallbackInternal completionDelegate);

		// Token: 0x06000135 RID: 309
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_KWS_UpdateParentEmail(IntPtr handle, IntPtr options, IntPtr clientData, OnUpdateParentEmailCallbackInternal completionDelegate);

		// Token: 0x06000136 RID: 310
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Leaderboards_CopyLeaderboardDefinitionByIndex(IntPtr handle, IntPtr options, ref IntPtr outLeaderboardDefinition);

		// Token: 0x06000137 RID: 311
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId(IntPtr handle, IntPtr options, ref IntPtr outLeaderboardDefinition);

		// Token: 0x06000138 RID: 312
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Leaderboards_CopyLeaderboardRecordByIndex(IntPtr handle, IntPtr options, ref IntPtr outLeaderboardRecord);

		// Token: 0x06000139 RID: 313
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Leaderboards_CopyLeaderboardRecordByUserId(IntPtr handle, IntPtr options, ref IntPtr outLeaderboardRecord);

		// Token: 0x0600013A RID: 314
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Leaderboards_CopyLeaderboardUserScoreByIndex(IntPtr handle, IntPtr options, ref IntPtr outLeaderboardUserScore);

		// Token: 0x0600013B RID: 315
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Leaderboards_CopyLeaderboardUserScoreByUserId(IntPtr handle, IntPtr options, ref IntPtr outLeaderboardUserScore);

		// Token: 0x0600013C RID: 316
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_Definition_Release(IntPtr leaderboardDefinition);

		// Token: 0x0600013D RID: 317
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Leaderboards_GetLeaderboardDefinitionCount(IntPtr handle, IntPtr options);

		// Token: 0x0600013E RID: 318
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Leaderboards_GetLeaderboardRecordCount(IntPtr handle, IntPtr options);

		// Token: 0x0600013F RID: 319
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Leaderboards_GetLeaderboardUserScoreCount(IntPtr handle, IntPtr options);

		// Token: 0x06000140 RID: 320
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_LeaderboardDefinition_Release(IntPtr leaderboardDefinition);

		// Token: 0x06000141 RID: 321
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_LeaderboardRecord_Release(IntPtr leaderboardRecord);

		// Token: 0x06000142 RID: 322
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_LeaderboardUserScore_Release(IntPtr leaderboardUserScore);

		// Token: 0x06000143 RID: 323
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_QueryLeaderboardDefinitions(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryLeaderboardDefinitionsCompleteCallbackInternal completionDelegate);

		// Token: 0x06000144 RID: 324
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_QueryLeaderboardRanks(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryLeaderboardRanksCompleteCallbackInternal completionDelegate);

		// Token: 0x06000145 RID: 325
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Leaderboards_QueryLeaderboardUserScores(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryLeaderboardUserScoresCompleteCallbackInternal completionDelegate);

		// Token: 0x06000146 RID: 326
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyDetails_CopyAttributeByIndex(IntPtr handle, IntPtr options, ref IntPtr outAttribute);

		// Token: 0x06000147 RID: 327
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyDetails_CopyAttributeByKey(IntPtr handle, IntPtr options, ref IntPtr outAttribute);

		// Token: 0x06000148 RID: 328
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyDetails_CopyInfo(IntPtr handle, IntPtr options, ref IntPtr outLobbyDetailsInfo);

		// Token: 0x06000149 RID: 329
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyDetails_CopyMemberAttributeByIndex(IntPtr handle, IntPtr options, ref IntPtr outAttribute);

		// Token: 0x0600014A RID: 330
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyDetails_CopyMemberAttributeByKey(IntPtr handle, IntPtr options, ref IntPtr outAttribute);

		// Token: 0x0600014B RID: 331
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_LobbyDetails_GetAttributeCount(IntPtr handle, IntPtr options);

		// Token: 0x0600014C RID: 332
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_LobbyDetails_GetLobbyOwner(IntPtr handle, IntPtr options);

		// Token: 0x0600014D RID: 333
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_LobbyDetails_GetMemberAttributeCount(IntPtr handle, IntPtr options);

		// Token: 0x0600014E RID: 334
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_LobbyDetails_GetMemberByIndex(IntPtr handle, IntPtr options);

		// Token: 0x0600014F RID: 335
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_LobbyDetails_GetMemberCount(IntPtr handle, IntPtr options);

		// Token: 0x06000150 RID: 336
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_LobbyDetails_Info_Release(IntPtr lobbyDetailsInfo);

		// Token: 0x06000151 RID: 337
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_LobbyDetails_Release(IntPtr lobbyHandle);

		// Token: 0x06000152 RID: 338
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_AddAttribute(IntPtr handle, IntPtr options);

		// Token: 0x06000153 RID: 339
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_AddMemberAttribute(IntPtr handle, IntPtr options);

		// Token: 0x06000154 RID: 340
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_LobbyModification_Release(IntPtr lobbyModificationHandle);

		// Token: 0x06000155 RID: 341
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_RemoveAttribute(IntPtr handle, IntPtr options);

		// Token: 0x06000156 RID: 342
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_RemoveMemberAttribute(IntPtr handle, IntPtr options);

		// Token: 0x06000157 RID: 343
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_SetBucketId(IntPtr handle, IntPtr options);

		// Token: 0x06000158 RID: 344
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_SetInvitesAllowed(IntPtr handle, IntPtr options);

		// Token: 0x06000159 RID: 345
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_SetMaxMembers(IntPtr handle, IntPtr options);

		// Token: 0x0600015A RID: 346
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbyModification_SetPermissionLevel(IntPtr handle, IntPtr options);

		// Token: 0x0600015B RID: 347
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbySearch_CopySearchResultByIndex(IntPtr handle, IntPtr options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x0600015C RID: 348
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_LobbySearch_Find(IntPtr handle, IntPtr options, IntPtr clientData, LobbySearchOnFindCallbackInternal completionDelegate);

		// Token: 0x0600015D RID: 349
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_LobbySearch_GetSearchResultCount(IntPtr handle, IntPtr options);

		// Token: 0x0600015E RID: 350
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_LobbySearch_Release(IntPtr lobbySearchHandle);

		// Token: 0x0600015F RID: 351
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbySearch_RemoveParameter(IntPtr handle, IntPtr options);

		// Token: 0x06000160 RID: 352
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbySearch_SetLobbyId(IntPtr handle, IntPtr options);

		// Token: 0x06000161 RID: 353
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbySearch_SetMaxResults(IntPtr handle, IntPtr options);

		// Token: 0x06000162 RID: 354
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbySearch_SetParameter(IntPtr handle, IntPtr options);

		// Token: 0x06000163 RID: 355
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_LobbySearch_SetTargetUserId(IntPtr handle, IntPtr options);

		// Token: 0x06000164 RID: 356
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyJoinLobbyAccepted(IntPtr handle, IntPtr options, IntPtr clientData, OnJoinLobbyAcceptedCallbackInternal notificationFn);

		// Token: 0x06000165 RID: 357
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyLobbyInviteAccepted(IntPtr handle, IntPtr options, IntPtr clientData, OnLobbyInviteAcceptedCallbackInternal notificationFn);

		// Token: 0x06000166 RID: 358
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyLobbyInviteReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnLobbyInviteReceivedCallbackInternal notificationFn);

		// Token: 0x06000167 RID: 359
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyLobbyMemberStatusReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnLobbyMemberStatusReceivedCallbackInternal notificationFn);

		// Token: 0x06000168 RID: 360
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyLobbyMemberUpdateReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnLobbyMemberUpdateReceivedCallbackInternal notificationFn);

		// Token: 0x06000169 RID: 361
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyLobbyUpdateReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnLobbyUpdateReceivedCallbackInternal notificationFn);

		// Token: 0x0600016A RID: 362
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Lobby_AddNotifyRTCRoomConnectionChanged(IntPtr handle, IntPtr options, IntPtr clientData, OnRTCRoomConnectionChangedCallbackInternal notificationFn);

		// Token: 0x0600016B RID: 363
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_Attribute_Release(IntPtr lobbyAttribute);

		// Token: 0x0600016C RID: 364
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_CopyLobbyDetailsHandle(IntPtr handle, IntPtr options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x0600016D RID: 365
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_CopyLobbyDetailsHandleByInviteId(IntPtr handle, IntPtr options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x0600016E RID: 366
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_CopyLobbyDetailsHandleByUiEventId(IntPtr handle, IntPtr options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x0600016F RID: 367
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_CreateLobby(IntPtr handle, IntPtr options, IntPtr clientData, OnCreateLobbyCallbackInternal completionDelegate);

		// Token: 0x06000170 RID: 368
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_CreateLobbySearch(IntPtr handle, IntPtr options, ref IntPtr outLobbySearchHandle);

		// Token: 0x06000171 RID: 369
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_DestroyLobby(IntPtr handle, IntPtr options, IntPtr clientData, OnDestroyLobbyCallbackInternal completionDelegate);

		// Token: 0x06000172 RID: 370
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Lobby_GetInviteCount(IntPtr handle, IntPtr options);

		// Token: 0x06000173 RID: 371
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_GetInviteIdByIndex(IntPtr handle, IntPtr options, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x06000174 RID: 372
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_GetRTCRoomName(IntPtr handle, IntPtr options, IntPtr outBuffer, ref uint inOutBufferLength);

		// Token: 0x06000175 RID: 373
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_IsRTCRoomConnected(IntPtr handle, IntPtr options, ref int bOutIsConnected);

		// Token: 0x06000176 RID: 374
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_JoinLobby(IntPtr handle, IntPtr options, IntPtr clientData, OnJoinLobbyCallbackInternal completionDelegate);

		// Token: 0x06000177 RID: 375
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_KickMember(IntPtr handle, IntPtr options, IntPtr clientData, OnKickMemberCallbackInternal completionDelegate);

		// Token: 0x06000178 RID: 376
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_LeaveLobby(IntPtr handle, IntPtr options, IntPtr clientData, OnLeaveLobbyCallbackInternal completionDelegate);

		// Token: 0x06000179 RID: 377
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_PromoteMember(IntPtr handle, IntPtr options, IntPtr clientData, OnPromoteMemberCallbackInternal completionDelegate);

		// Token: 0x0600017A RID: 378
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_QueryInvites(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Lobby.OnQueryInvitesCallbackInternal completionDelegate);

		// Token: 0x0600017B RID: 379
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RejectInvite(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Lobby.OnRejectInviteCallbackInternal completionDelegate);

		// Token: 0x0600017C RID: 380
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyJoinLobbyAccepted(IntPtr handle, ulong inId);

		// Token: 0x0600017D RID: 381
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyLobbyInviteAccepted(IntPtr handle, ulong inId);

		// Token: 0x0600017E RID: 382
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyLobbyInviteReceived(IntPtr handle, ulong inId);

		// Token: 0x0600017F RID: 383
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived(IntPtr handle, ulong inId);

		// Token: 0x06000180 RID: 384
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived(IntPtr handle, ulong inId);

		// Token: 0x06000181 RID: 385
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyLobbyUpdateReceived(IntPtr handle, ulong inId);

		// Token: 0x06000182 RID: 386
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_RemoveNotifyRTCRoomConnectionChanged(IntPtr handle, ulong inId);

		// Token: 0x06000183 RID: 387
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_SendInvite(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Lobby.OnSendInviteCallbackInternal completionDelegate);

		// Token: 0x06000184 RID: 388
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Lobby_UpdateLobby(IntPtr handle, IntPtr options, IntPtr clientData, OnUpdateLobbyCallbackInternal completionDelegate);

		// Token: 0x06000185 RID: 389
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Lobby_UpdateLobbyModification(IntPtr handle, IntPtr options, ref IntPtr outLobbyModificationHandle);

		// Token: 0x06000186 RID: 390
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Logging_SetCallback(LogMessageFuncInternal callback);

		// Token: 0x06000187 RID: 391
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Logging_SetLogLevel(LogCategory logCategory, LogLevel logLevel);

		// Token: 0x06000188 RID: 392
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Metrics_BeginPlayerSession(IntPtr handle, IntPtr options);

		// Token: 0x06000189 RID: 393
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Metrics_EndPlayerSession(IntPtr handle, IntPtr options);

		// Token: 0x0600018A RID: 394
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Mods_CopyModInfo(IntPtr handle, IntPtr options, ref IntPtr outEnumeratedMods);

		// Token: 0x0600018B RID: 395
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Mods_EnumerateMods(IntPtr handle, IntPtr options, IntPtr clientData, OnEnumerateModsCallbackInternal completionDelegate);

		// Token: 0x0600018C RID: 396
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Mods_InstallMod(IntPtr handle, IntPtr options, IntPtr clientData, OnInstallModCallbackInternal completionDelegate);

		// Token: 0x0600018D RID: 397
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Mods_ModInfo_Release(IntPtr modInfo);

		// Token: 0x0600018E RID: 398
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Mods_UninstallMod(IntPtr handle, IntPtr options, IntPtr clientData, OnUninstallModCallbackInternal completionDelegate);

		// Token: 0x0600018F RID: 399
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Mods_UpdateMod(IntPtr handle, IntPtr options, IntPtr clientData, OnUpdateModCallbackInternal completionDelegate);

		// Token: 0x06000190 RID: 400
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_AcceptConnection(IntPtr handle, IntPtr options);

		// Token: 0x06000191 RID: 401
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_P2P_AddNotifyIncomingPacketQueueFull(IntPtr handle, IntPtr options, IntPtr clientData, OnIncomingPacketQueueFullCallbackInternal incomingPacketQueueFullHandler);

		// Token: 0x06000192 RID: 402
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_P2P_AddNotifyPeerConnectionClosed(IntPtr handle, IntPtr options, IntPtr clientData, OnRemoteConnectionClosedCallbackInternal connectionClosedHandler);

		// Token: 0x06000193 RID: 403
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_P2P_AddNotifyPeerConnectionEstablished(IntPtr handle, IntPtr options, IntPtr clientData, OnPeerConnectionEstablishedCallbackInternal connectionEstablishedHandler);

		// Token: 0x06000194 RID: 404
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_P2P_AddNotifyPeerConnectionRequest(IntPtr handle, IntPtr options, IntPtr clientData, OnIncomingConnectionRequestCallbackInternal connectionRequestHandler);

		// Token: 0x06000195 RID: 405
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_ClearPacketQueue(IntPtr handle, IntPtr options);

		// Token: 0x06000196 RID: 406
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_CloseConnection(IntPtr handle, IntPtr options);

		// Token: 0x06000197 RID: 407
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_CloseConnections(IntPtr handle, IntPtr options);

		// Token: 0x06000198 RID: 408
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_GetNATType(IntPtr handle, IntPtr options, ref NATType outNATType);

		// Token: 0x06000199 RID: 409
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_GetNextReceivedPacketSize(IntPtr handle, IntPtr options, ref uint outPacketSizeBytes);

		// Token: 0x0600019A RID: 410
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_GetPacketQueueInfo(IntPtr handle, IntPtr options, ref PacketQueueInfoInternal outPacketQueueInfo);

		// Token: 0x0600019B RID: 411
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_GetPortRange(IntPtr handle, IntPtr options, ref ushort outPort, ref ushort outNumAdditionalPortsToTry);

		// Token: 0x0600019C RID: 412
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_GetRelayControl(IntPtr handle, IntPtr options, ref RelayControl outRelayControl);

		// Token: 0x0600019D RID: 413
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_P2P_QueryNATType(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryNATTypeCompleteCallbackInternal completionDelegate);

		// Token: 0x0600019E RID: 414
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_ReceivePacket(IntPtr handle, IntPtr options, ref IntPtr outPeerId, ref SocketIdInternal outSocketId, ref byte outChannel, IntPtr outData, ref uint outBytesWritten);

		// Token: 0x0600019F RID: 415
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_P2P_RemoveNotifyIncomingPacketQueueFull(IntPtr handle, ulong notificationId);

		// Token: 0x060001A0 RID: 416
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_P2P_RemoveNotifyPeerConnectionClosed(IntPtr handle, ulong notificationId);

		// Token: 0x060001A1 RID: 417
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_P2P_RemoveNotifyPeerConnectionEstablished(IntPtr handle, ulong notificationId);

		// Token: 0x060001A2 RID: 418
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_P2P_RemoveNotifyPeerConnectionRequest(IntPtr handle, ulong notificationId);

		// Token: 0x060001A3 RID: 419
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_SendPacket(IntPtr handle, IntPtr options);

		// Token: 0x060001A4 RID: 420
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_SetPacketQueueSize(IntPtr handle, IntPtr options);

		// Token: 0x060001A5 RID: 421
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_SetPortRange(IntPtr handle, IntPtr options);

		// Token: 0x060001A6 RID: 422
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_P2P_SetRelayControl(IntPtr handle, IntPtr options);

		// Token: 0x060001A7 RID: 423
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_CheckForLauncherAndRestart(IntPtr handle);

		// Token: 0x060001A8 RID: 424
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_Create(IntPtr options);

		// Token: 0x060001A9 RID: 425
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetAchievementsInterface(IntPtr handle);

		// Token: 0x060001AA RID: 426
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_GetActiveCountryCode(IntPtr handle, IntPtr localUserId, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060001AB RID: 427
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_GetActiveLocaleCode(IntPtr handle, IntPtr localUserId, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060001AC RID: 428
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetAntiCheatClientInterface(IntPtr handle);

		// Token: 0x060001AD RID: 429
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetAntiCheatServerInterface(IntPtr handle);

		// Token: 0x060001AE RID: 430
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetAuthInterface(IntPtr handle);

		// Token: 0x060001AF RID: 431
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetConnectInterface(IntPtr handle);

		// Token: 0x060001B0 RID: 432
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetCustomInvitesInterface(IntPtr handle);

		// Token: 0x060001B1 RID: 433
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetEcomInterface(IntPtr handle);

		// Token: 0x060001B2 RID: 434
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetFriendsInterface(IntPtr handle);

		// Token: 0x060001B3 RID: 435
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetKWSInterface(IntPtr handle);

		// Token: 0x060001B4 RID: 436
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetLeaderboardsInterface(IntPtr handle);

		// Token: 0x060001B5 RID: 437
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetLobbyInterface(IntPtr handle);

		// Token: 0x060001B6 RID: 438
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetMetricsInterface(IntPtr handle);

		// Token: 0x060001B7 RID: 439
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetModsInterface(IntPtr handle);

		// Token: 0x060001B8 RID: 440
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_GetOverrideCountryCode(IntPtr handle, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060001B9 RID: 441
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_GetOverrideLocaleCode(IntPtr handle, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060001BA RID: 442
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetP2PInterface(IntPtr handle);

		// Token: 0x060001BB RID: 443
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetPlayerDataStorageInterface(IntPtr handle);

		// Token: 0x060001BC RID: 444
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetPresenceInterface(IntPtr handle);

		// Token: 0x060001BD RID: 445
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetProgressionSnapshotInterface(IntPtr handle);

		// Token: 0x060001BE RID: 446
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetRTCAdminInterface(IntPtr handle);

		// Token: 0x060001BF RID: 447
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetRTCInterface(IntPtr handle);

		// Token: 0x060001C0 RID: 448
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetReportsInterface(IntPtr handle);

		// Token: 0x060001C1 RID: 449
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetSanctionsInterface(IntPtr handle);

		// Token: 0x060001C2 RID: 450
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetSessionsInterface(IntPtr handle);

		// Token: 0x060001C3 RID: 451
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetStatsInterface(IntPtr handle);

		// Token: 0x060001C4 RID: 452
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetTitleStorageInterface(IntPtr handle);

		// Token: 0x060001C5 RID: 453
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetUIInterface(IntPtr handle);

		// Token: 0x060001C6 RID: 454
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_Platform_GetUserInfoInterface(IntPtr handle);

		// Token: 0x060001C7 RID: 455
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Platform_Release(IntPtr handle);

		// Token: 0x060001C8 RID: 456
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_SetOverrideCountryCode(IntPtr handle, IntPtr newCountryCode);

		// Token: 0x060001C9 RID: 457
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Platform_SetOverrideLocaleCode(IntPtr handle, IntPtr newLocaleCode);

		// Token: 0x060001CA RID: 458
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Platform_Tick(IntPtr handle);

		// Token: 0x060001CB RID: 459
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorageFileTransferRequest_CancelRequest(IntPtr handle);

		// Token: 0x060001CC RID: 460
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorageFileTransferRequest_GetFileRequestState(IntPtr handle);

		// Token: 0x060001CD RID: 461
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorageFileTransferRequest_GetFilename(IntPtr handle, uint filenameStringBufferSizeBytes, IntPtr outStringBuffer, ref int outStringLength);

		// Token: 0x060001CE RID: 462
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PlayerDataStorageFileTransferRequest_Release(IntPtr playerDataStorageFileTransferHandle);

		// Token: 0x060001CF RID: 463
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorage_CopyFileMetadataAtIndex(IntPtr handle, IntPtr copyFileMetadataOptions, ref IntPtr outMetadata);

		// Token: 0x060001D0 RID: 464
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorage_CopyFileMetadataByFilename(IntPtr handle, IntPtr copyFileMetadataOptions, ref IntPtr outMetadata);

		// Token: 0x060001D1 RID: 465
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorage_DeleteCache(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnDeleteCacheCompleteCallbackInternal completionCallback);

		// Token: 0x060001D2 RID: 466
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PlayerDataStorage_DeleteFile(IntPtr handle, IntPtr deleteOptions, IntPtr clientData, OnDeleteFileCompleteCallbackInternal completionCallback);

		// Token: 0x060001D3 RID: 467
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PlayerDataStorage_DuplicateFile(IntPtr handle, IntPtr duplicateOptions, IntPtr clientData, OnDuplicateFileCompleteCallbackInternal completionCallback);

		// Token: 0x060001D4 RID: 468
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PlayerDataStorage_FileMetadata_Release(IntPtr fileMetadata);

		// Token: 0x060001D5 RID: 469
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PlayerDataStorage_GetFileMetadataCount(IntPtr handle, IntPtr getFileMetadataCountOptions, ref int outFileMetadataCount);

		// Token: 0x060001D6 RID: 470
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PlayerDataStorage_QueryFile(IntPtr handle, IntPtr queryFileOptions, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnQueryFileCompleteCallbackInternal completionCallback);

		// Token: 0x060001D7 RID: 471
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PlayerDataStorage_QueryFileList(IntPtr handle, IntPtr queryFileListOptions, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnQueryFileListCompleteCallbackInternal completionCallback);

		// Token: 0x060001D8 RID: 472
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_PlayerDataStorage_ReadFile(IntPtr handle, IntPtr readOptions, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnReadFileCompleteCallbackInternal completionCallback);

		// Token: 0x060001D9 RID: 473
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_PlayerDataStorage_WriteFile(IntPtr handle, IntPtr writeOptions, IntPtr clientData, OnWriteFileCompleteCallbackInternal completionCallback);

		// Token: 0x060001DA RID: 474
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PresenceModification_DeleteData(IntPtr handle, IntPtr options);

		// Token: 0x060001DB RID: 475
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_PresenceModification_Release(IntPtr presenceModificationHandle);

		// Token: 0x060001DC RID: 476
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PresenceModification_SetData(IntPtr handle, IntPtr options);

		// Token: 0x060001DD RID: 477
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PresenceModification_SetJoinInfo(IntPtr handle, IntPtr options);

		// Token: 0x060001DE RID: 478
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PresenceModification_SetRawRichText(IntPtr handle, IntPtr options);

		// Token: 0x060001DF RID: 479
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_PresenceModification_SetStatus(IntPtr handle, IntPtr options);

		// Token: 0x060001E0 RID: 480
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Presence_AddNotifyJoinGameAccepted(IntPtr handle, IntPtr options, IntPtr clientData, OnJoinGameAcceptedCallbackInternal notificationFn);

		// Token: 0x060001E1 RID: 481
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Presence_AddNotifyOnPresenceChanged(IntPtr handle, IntPtr options, IntPtr clientData, OnPresenceChangedCallbackInternal notificationHandler);

		// Token: 0x060001E2 RID: 482
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Presence_CopyPresence(IntPtr handle, IntPtr options, ref IntPtr outPresence);

		// Token: 0x060001E3 RID: 483
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Presence_CreatePresenceModification(IntPtr handle, IntPtr options, ref IntPtr outPresenceModificationHandle);

		// Token: 0x060001E4 RID: 484
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Presence_GetJoinInfo(IntPtr handle, IntPtr options, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060001E5 RID: 485
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_Presence_HasPresence(IntPtr handle, IntPtr options);

		// Token: 0x060001E6 RID: 486
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Presence_Info_Release(IntPtr presenceInfo);

		// Token: 0x060001E7 RID: 487
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Presence_QueryPresence(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryPresenceCompleteCallbackInternal completionDelegate);

		// Token: 0x060001E8 RID: 488
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Presence_RemoveNotifyJoinGameAccepted(IntPtr handle, ulong inId);

		// Token: 0x060001E9 RID: 489
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Presence_RemoveNotifyOnPresenceChanged(IntPtr handle, ulong notificationId);

		// Token: 0x060001EA RID: 490
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Presence_SetPresence(IntPtr handle, IntPtr options, IntPtr clientData, SetPresenceCompleteCallbackInternal completionDelegate);

		// Token: 0x060001EB RID: 491
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_ProductUserId_FromString(IntPtr productUserIdString);

		// Token: 0x060001EC RID: 492
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_ProductUserId_IsValid(IntPtr accountId);

		// Token: 0x060001ED RID: 493
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ProductUserId_ToString(IntPtr accountId, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x060001EE RID: 494
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ProgressionSnapshot_AddProgression(IntPtr handle, IntPtr options);

		// Token: 0x060001EF RID: 495
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ProgressionSnapshot_BeginSnapshot(IntPtr handle, IntPtr options, ref uint outSnapshotId);

		// Token: 0x060001F0 RID: 496
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_ProgressionSnapshot_DeleteSnapshot(IntPtr handle, IntPtr options, IntPtr clientData, OnDeleteSnapshotCallbackInternal completionDelegate);

		// Token: 0x060001F1 RID: 497
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_ProgressionSnapshot_EndSnapshot(IntPtr handle, IntPtr options);

		// Token: 0x060001F2 RID: 498
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_ProgressionSnapshot_SubmitSnapshot(IntPtr handle, IntPtr options, IntPtr clientData, OnSubmitSnapshotCallbackInternal completionDelegate);

		// Token: 0x060001F3 RID: 499
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAdmin_CopyUserTokenByIndex(IntPtr handle, IntPtr options, ref IntPtr outUserToken);

		// Token: 0x060001F4 RID: 500
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAdmin_CopyUserTokenByUserId(IntPtr handle, IntPtr options, ref IntPtr outUserToken);

		// Token: 0x060001F5 RID: 501
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAdmin_Kick(IntPtr handle, IntPtr options, IntPtr clientData, OnKickCompleteCallbackInternal completionDelegate);

		// Token: 0x060001F6 RID: 502
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAdmin_QueryJoinRoomToken(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryJoinRoomTokenCompleteCallbackInternal completionDelegate);

		// Token: 0x060001F7 RID: 503
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAdmin_SetParticipantHardMute(IntPtr handle, IntPtr options, IntPtr clientData, OnSetParticipantHardMuteCompleteCallbackInternal completionDelegate);

		// Token: 0x060001F8 RID: 504
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAdmin_UserToken_Release(IntPtr userToken);

		// Token: 0x060001F9 RID: 505
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTCAudio_AddNotifyAudioBeforeRender(IntPtr handle, IntPtr options, IntPtr clientData, OnAudioBeforeRenderCallbackInternal completionDelegate);

		// Token: 0x060001FA RID: 506
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTCAudio_AddNotifyAudioBeforeSend(IntPtr handle, IntPtr options, IntPtr clientData, OnAudioBeforeSendCallbackInternal completionDelegate);

		// Token: 0x060001FB RID: 507
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTCAudio_AddNotifyAudioDevicesChanged(IntPtr handle, IntPtr options, IntPtr clientData, OnAudioDevicesChangedCallbackInternal completionDelegate);

		// Token: 0x060001FC RID: 508
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTCAudio_AddNotifyAudioInputState(IntPtr handle, IntPtr options, IntPtr clientData, OnAudioInputStateCallbackInternal completionDelegate);

		// Token: 0x060001FD RID: 509
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTCAudio_AddNotifyAudioOutputState(IntPtr handle, IntPtr options, IntPtr clientData, OnAudioOutputStateCallbackInternal completionDelegate);

		// Token: 0x060001FE RID: 510
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTCAudio_AddNotifyParticipantUpdated(IntPtr handle, IntPtr options, IntPtr clientData, OnParticipantUpdatedCallbackInternal completionDelegate);

		// Token: 0x060001FF RID: 511
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_RTCAudio_GetAudioInputDeviceByIndex(IntPtr handle, IntPtr options);

		// Token: 0x06000200 RID: 512
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_RTCAudio_GetAudioInputDevicesCount(IntPtr handle, IntPtr options);

		// Token: 0x06000201 RID: 513
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_RTCAudio_GetAudioOutputDeviceByIndex(IntPtr handle, IntPtr options);

		// Token: 0x06000202 RID: 514
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_RTCAudio_GetAudioOutputDevicesCount(IntPtr handle, IntPtr options);

		// Token: 0x06000203 RID: 515
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAudio_RegisterPlatformAudioUser(IntPtr handle, IntPtr options);

		// Token: 0x06000204 RID: 516
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_RemoveNotifyAudioBeforeRender(IntPtr handle, ulong notificationId);

		// Token: 0x06000205 RID: 517
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_RemoveNotifyAudioBeforeSend(IntPtr handle, ulong notificationId);

		// Token: 0x06000206 RID: 518
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_RemoveNotifyAudioDevicesChanged(IntPtr handle, ulong notificationId);

		// Token: 0x06000207 RID: 519
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_RemoveNotifyAudioInputState(IntPtr handle, ulong notificationId);

		// Token: 0x06000208 RID: 520
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_RemoveNotifyAudioOutputState(IntPtr handle, ulong notificationId);

		// Token: 0x06000209 RID: 521
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_RemoveNotifyParticipantUpdated(IntPtr handle, ulong notificationId);

		// Token: 0x0600020A RID: 522
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAudio_SendAudio(IntPtr handle, IntPtr options);

		// Token: 0x0600020B RID: 523
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAudio_SetAudioInputSettings(IntPtr handle, IntPtr options);

		// Token: 0x0600020C RID: 524
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAudio_SetAudioOutputSettings(IntPtr handle, IntPtr options);

		// Token: 0x0600020D RID: 525
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTCAudio_UnregisterPlatformAudioUser(IntPtr handle, IntPtr options);

		// Token: 0x0600020E RID: 526
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_UpdateReceiving(IntPtr handle, IntPtr options, IntPtr clientData, OnUpdateReceivingCallbackInternal completionDelegate);

		// Token: 0x0600020F RID: 527
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTCAudio_UpdateSending(IntPtr handle, IntPtr options, IntPtr clientData, OnUpdateSendingCallbackInternal completionDelegate);

		// Token: 0x06000210 RID: 528
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTC_AddNotifyDisconnected(IntPtr handle, IntPtr options, IntPtr clientData, OnDisconnectedCallbackInternal completionDelegate);

		// Token: 0x06000211 RID: 529
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_RTC_AddNotifyParticipantStatusChanged(IntPtr handle, IntPtr options, IntPtr clientData, OnParticipantStatusChangedCallbackInternal completionDelegate);

		// Token: 0x06000212 RID: 530
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTC_BlockParticipant(IntPtr handle, IntPtr options, IntPtr clientData, OnBlockParticipantCallbackInternal completionDelegate);

		// Token: 0x06000213 RID: 531
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_RTC_GetAudioInterface(IntPtr handle);

		// Token: 0x06000214 RID: 532
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTC_JoinRoom(IntPtr handle, IntPtr options, IntPtr clientData, OnJoinRoomCallbackInternal completionDelegate);

		// Token: 0x06000215 RID: 533
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTC_LeaveRoom(IntPtr handle, IntPtr options, IntPtr clientData, OnLeaveRoomCallbackInternal completionDelegate);

		// Token: 0x06000216 RID: 534
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTC_RemoveNotifyDisconnected(IntPtr handle, ulong notificationId);

		// Token: 0x06000217 RID: 535
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_RTC_RemoveNotifyParticipantStatusChanged(IntPtr handle, ulong notificationId);

		// Token: 0x06000218 RID: 536
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTC_SetRoomSetting(IntPtr handle, IntPtr options);

		// Token: 0x06000219 RID: 537
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_RTC_SetSetting(IntPtr handle, IntPtr options);

		// Token: 0x0600021A RID: 538
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Reports_SendPlayerBehaviorReport(IntPtr handle, IntPtr options, IntPtr clientData, OnSendPlayerBehaviorReportCompleteCallbackInternal completionDelegate);

		// Token: 0x0600021B RID: 539
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sanctions_CopyPlayerSanctionByIndex(IntPtr handle, IntPtr options, ref IntPtr outSanction);

		// Token: 0x0600021C RID: 540
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Sanctions_GetPlayerSanctionCount(IntPtr handle, IntPtr options);

		// Token: 0x0600021D RID: 541
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sanctions_PlayerSanction_Release(IntPtr sanction);

		// Token: 0x0600021E RID: 542
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sanctions_QueryActivePlayerSanctions(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryActivePlayerSanctionsCallbackInternal completionDelegate);

		// Token: 0x0600021F RID: 543
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_SessionDetails_Attribute_Release(IntPtr sessionAttribute);

		// Token: 0x06000220 RID: 544
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionDetails_CopyInfo(IntPtr handle, IntPtr options, ref IntPtr outSessionInfo);

		// Token: 0x06000221 RID: 545
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionDetails_CopySessionAttributeByIndex(IntPtr handle, IntPtr options, ref IntPtr outSessionAttribute);

		// Token: 0x06000222 RID: 546
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionDetails_CopySessionAttributeByKey(IntPtr handle, IntPtr options, ref IntPtr outSessionAttribute);

		// Token: 0x06000223 RID: 547
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_SessionDetails_GetSessionAttributeCount(IntPtr handle, IntPtr options);

		// Token: 0x06000224 RID: 548
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_SessionDetails_Info_Release(IntPtr sessionInfo);

		// Token: 0x06000225 RID: 549
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_SessionDetails_Release(IntPtr sessionHandle);

		// Token: 0x06000226 RID: 550
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_AddAttribute(IntPtr handle, IntPtr options);

		// Token: 0x06000227 RID: 551
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_SessionModification_Release(IntPtr sessionModificationHandle);

		// Token: 0x06000228 RID: 552
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_RemoveAttribute(IntPtr handle, IntPtr options);

		// Token: 0x06000229 RID: 553
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_SetBucketId(IntPtr handle, IntPtr options);

		// Token: 0x0600022A RID: 554
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_SetHostAddress(IntPtr handle, IntPtr options);

		// Token: 0x0600022B RID: 555
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_SetInvitesAllowed(IntPtr handle, IntPtr options);

		// Token: 0x0600022C RID: 556
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_SetJoinInProgressAllowed(IntPtr handle, IntPtr options);

		// Token: 0x0600022D RID: 557
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_SetMaxPlayers(IntPtr handle, IntPtr options);

		// Token: 0x0600022E RID: 558
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionModification_SetPermissionLevel(IntPtr handle, IntPtr options);

		// Token: 0x0600022F RID: 559
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionSearch_CopySearchResultByIndex(IntPtr handle, IntPtr options, ref IntPtr outSessionHandle);

		// Token: 0x06000230 RID: 560
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_SessionSearch_Find(IntPtr handle, IntPtr options, IntPtr clientData, SessionSearchOnFindCallbackInternal completionDelegate);

		// Token: 0x06000231 RID: 561
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_SessionSearch_GetSearchResultCount(IntPtr handle, IntPtr options);

		// Token: 0x06000232 RID: 562
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_SessionSearch_Release(IntPtr sessionSearchHandle);

		// Token: 0x06000233 RID: 563
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionSearch_RemoveParameter(IntPtr handle, IntPtr options);

		// Token: 0x06000234 RID: 564
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionSearch_SetMaxResults(IntPtr handle, IntPtr options);

		// Token: 0x06000235 RID: 565
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionSearch_SetParameter(IntPtr handle, IntPtr options);

		// Token: 0x06000236 RID: 566
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionSearch_SetSessionId(IntPtr handle, IntPtr options);

		// Token: 0x06000237 RID: 567
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_SessionSearch_SetTargetUserId(IntPtr handle, IntPtr options);

		// Token: 0x06000238 RID: 568
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Sessions_AddNotifyJoinSessionAccepted(IntPtr handle, IntPtr options, IntPtr clientData, OnJoinSessionAcceptedCallbackInternal notificationFn);

		// Token: 0x06000239 RID: 569
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Sessions_AddNotifySessionInviteAccepted(IntPtr handle, IntPtr options, IntPtr clientData, OnSessionInviteAcceptedCallbackInternal notificationFn);

		// Token: 0x0600023A RID: 570
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_Sessions_AddNotifySessionInviteReceived(IntPtr handle, IntPtr options, IntPtr clientData, OnSessionInviteReceivedCallbackInternal notificationFn);

		// Token: 0x0600023B RID: 571
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_CopyActiveSessionHandle(IntPtr handle, IntPtr options, ref IntPtr outSessionHandle);

		// Token: 0x0600023C RID: 572
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_CopySessionHandleByInviteId(IntPtr handle, IntPtr options, ref IntPtr outSessionHandle);

		// Token: 0x0600023D RID: 573
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_CopySessionHandleByUiEventId(IntPtr handle, IntPtr options, ref IntPtr outSessionHandle);

		// Token: 0x0600023E RID: 574
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_CopySessionHandleForPresence(IntPtr handle, IntPtr options, ref IntPtr outSessionHandle);

		// Token: 0x0600023F RID: 575
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_CreateSessionModification(IntPtr handle, IntPtr options, ref IntPtr outSessionModificationHandle);

		// Token: 0x06000240 RID: 576
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_CreateSessionSearch(IntPtr handle, IntPtr options, ref IntPtr outSessionSearchHandle);

		// Token: 0x06000241 RID: 577
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_DestroySession(IntPtr handle, IntPtr options, IntPtr clientData, OnDestroySessionCallbackInternal completionDelegate);

		// Token: 0x06000242 RID: 578
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_DumpSessionState(IntPtr handle, IntPtr options);

		// Token: 0x06000243 RID: 579
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_EndSession(IntPtr handle, IntPtr options, IntPtr clientData, OnEndSessionCallbackInternal completionDelegate);

		// Token: 0x06000244 RID: 580
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Sessions_GetInviteCount(IntPtr handle, IntPtr options);

		// Token: 0x06000245 RID: 581
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_GetInviteIdByIndex(IntPtr handle, IntPtr options, IntPtr outBuffer, ref int inOutBufferLength);

		// Token: 0x06000246 RID: 582
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_IsUserInSession(IntPtr handle, IntPtr options);

		// Token: 0x06000247 RID: 583
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_JoinSession(IntPtr handle, IntPtr options, IntPtr clientData, OnJoinSessionCallbackInternal completionDelegate);

		// Token: 0x06000248 RID: 584
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_QueryInvites(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Sessions.OnQueryInvitesCallbackInternal completionDelegate);

		// Token: 0x06000249 RID: 585
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_RegisterPlayers(IntPtr handle, IntPtr options, IntPtr clientData, OnRegisterPlayersCallbackInternal completionDelegate);

		// Token: 0x0600024A RID: 586
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_RejectInvite(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Sessions.OnRejectInviteCallbackInternal completionDelegate);

		// Token: 0x0600024B RID: 587
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_RemoveNotifyJoinSessionAccepted(IntPtr handle, ulong inId);

		// Token: 0x0600024C RID: 588
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_RemoveNotifySessionInviteAccepted(IntPtr handle, ulong inId);

		// Token: 0x0600024D RID: 589
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_RemoveNotifySessionInviteReceived(IntPtr handle, ulong inId);

		// Token: 0x0600024E RID: 590
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_SendInvite(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.Sessions.OnSendInviteCallbackInternal completionDelegate);

		// Token: 0x0600024F RID: 591
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_StartSession(IntPtr handle, IntPtr options, IntPtr clientData, OnStartSessionCallbackInternal completionDelegate);

		// Token: 0x06000250 RID: 592
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_UnregisterPlayers(IntPtr handle, IntPtr options, IntPtr clientData, OnUnregisterPlayersCallbackInternal completionDelegate);

		// Token: 0x06000251 RID: 593
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Sessions_UpdateSession(IntPtr handle, IntPtr options, IntPtr clientData, OnUpdateSessionCallbackInternal completionDelegate);

		// Token: 0x06000252 RID: 594
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Sessions_UpdateSessionModification(IntPtr handle, IntPtr options, ref IntPtr outSessionModificationHandle);

		// Token: 0x06000253 RID: 595
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Shutdown();

		// Token: 0x06000254 RID: 596
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Stats_CopyStatByIndex(IntPtr handle, IntPtr options, ref IntPtr outStat);

		// Token: 0x06000255 RID: 597
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_Stats_CopyStatByName(IntPtr handle, IntPtr options, ref IntPtr outStat);

		// Token: 0x06000256 RID: 598
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_Stats_GetStatsCount(IntPtr handle, IntPtr options);

		// Token: 0x06000257 RID: 599
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Stats_IngestStat(IntPtr handle, IntPtr options, IntPtr clientData, OnIngestStatCompleteCallbackInternal completionDelegate);

		// Token: 0x06000258 RID: 600
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Stats_QueryStats(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryStatsCompleteCallbackInternal completionDelegate);

		// Token: 0x06000259 RID: 601
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_Stats_Stat_Release(IntPtr stat);

		// Token: 0x0600025A RID: 602
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_TitleStorageFileTransferRequest_CancelRequest(IntPtr handle);

		// Token: 0x0600025B RID: 603
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_TitleStorageFileTransferRequest_GetFileRequestState(IntPtr handle);

		// Token: 0x0600025C RID: 604
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_TitleStorageFileTransferRequest_GetFilename(IntPtr handle, uint filenameStringBufferSizeBytes, IntPtr outStringBuffer, ref int outStringLength);

		// Token: 0x0600025D RID: 605
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_TitleStorageFileTransferRequest_Release(IntPtr titleStorageFileTransferHandle);

		// Token: 0x0600025E RID: 606
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_TitleStorage_CopyFileMetadataAtIndex(IntPtr handle, IntPtr options, ref IntPtr outMetadata);

		// Token: 0x0600025F RID: 607
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_TitleStorage_CopyFileMetadataByFilename(IntPtr handle, IntPtr options, ref IntPtr outMetadata);

		// Token: 0x06000260 RID: 608
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_TitleStorage_DeleteCache(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnDeleteCacheCompleteCallbackInternal completionCallback);

		// Token: 0x06000261 RID: 609
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_TitleStorage_FileMetadata_Release(IntPtr fileMetadata);

		// Token: 0x06000262 RID: 610
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_TitleStorage_GetFileMetadataCount(IntPtr handle, IntPtr options);

		// Token: 0x06000263 RID: 611
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_TitleStorage_QueryFile(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnQueryFileCompleteCallbackInternal completionCallback);

		// Token: 0x06000264 RID: 612
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_TitleStorage_QueryFileList(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnQueryFileListCompleteCallbackInternal completionCallback);

		// Token: 0x06000265 RID: 613
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern IntPtr EOS_TitleStorage_ReadFile(IntPtr handle, IntPtr options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnReadFileCompleteCallbackInternal completionCallback);

		// Token: 0x06000266 RID: 614
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UI_AcknowledgeEventId(IntPtr handle, IntPtr options);

		// Token: 0x06000267 RID: 615
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern ulong EOS_UI_AddNotifyDisplaySettingsUpdated(IntPtr handle, IntPtr options, IntPtr clientData, OnDisplaySettingsUpdatedCallbackInternal notificationFn);

		// Token: 0x06000268 RID: 616
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_UI_GetFriendsVisible(IntPtr handle, IntPtr options);

		// Token: 0x06000269 RID: 617
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern NotificationLocation EOS_UI_GetNotificationLocationPreference(IntPtr handle);

		// Token: 0x0600026A RID: 618
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern KeyCombination EOS_UI_GetToggleFriendsKey(IntPtr handle, IntPtr options);

		// Token: 0x0600026B RID: 619
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UI_HideFriends(IntPtr handle, IntPtr options, IntPtr clientData, OnHideFriendsCallbackInternal completionDelegate);

		// Token: 0x0600026C RID: 620
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern int EOS_UI_IsValidKeyCombination(IntPtr handle, KeyCombination keyCombination);

		// Token: 0x0600026D RID: 621
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UI_RemoveNotifyDisplaySettingsUpdated(IntPtr handle, ulong id);

		// Token: 0x0600026E RID: 622
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UI_SetDisplayPreference(IntPtr handle, IntPtr options);

		// Token: 0x0600026F RID: 623
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UI_SetToggleFriendsKey(IntPtr handle, IntPtr options);

		// Token: 0x06000270 RID: 624
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UI_ShowFriends(IntPtr handle, IntPtr options, IntPtr clientData, OnShowFriendsCallbackInternal completionDelegate);

		// Token: 0x06000271 RID: 625
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UserInfo_CopyExternalUserInfoByAccountId(IntPtr handle, IntPtr options, ref IntPtr outExternalUserInfo);

		// Token: 0x06000272 RID: 626
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UserInfo_CopyExternalUserInfoByAccountType(IntPtr handle, IntPtr options, ref IntPtr outExternalUserInfo);

		// Token: 0x06000273 RID: 627
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UserInfo_CopyExternalUserInfoByIndex(IntPtr handle, IntPtr options, ref IntPtr outExternalUserInfo);

		// Token: 0x06000274 RID: 628
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern Result EOS_UserInfo_CopyUserInfo(IntPtr handle, IntPtr options, ref IntPtr outUserInfo);

		// Token: 0x06000275 RID: 629
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UserInfo_ExternalUserInfo_Release(IntPtr externalUserInfo);

		// Token: 0x06000276 RID: 630
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern uint EOS_UserInfo_GetExternalUserInfoCount(IntPtr handle, IntPtr options);

		// Token: 0x06000277 RID: 631
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UserInfo_QueryUserInfo(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryUserInfoCallbackInternal completionDelegate);

		// Token: 0x06000278 RID: 632
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UserInfo_QueryUserInfoByDisplayName(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryUserInfoByDisplayNameCallbackInternal completionDelegate);

		// Token: 0x06000279 RID: 633
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UserInfo_QueryUserInfoByExternalAccount(IntPtr handle, IntPtr options, IntPtr clientData, OnQueryUserInfoByExternalAccountCallbackInternal completionDelegate);

		// Token: 0x0600027A RID: 634
		[DllImport("EOSSDK-Win64-Shipping")]
		internal static extern void EOS_UserInfo_Release(IntPtr userInfo);
	}
}
