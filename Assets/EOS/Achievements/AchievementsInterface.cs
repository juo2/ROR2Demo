using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005F1 RID: 1521
	public sealed class AchievementsInterface : Handle
	{
		// Token: 0x06002526 RID: 9510 RVA: 0x000036D3 File Offset: 0x000018D3
		public AchievementsInterface()
		{
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000036DB File Offset: 0x000018DB
		public AchievementsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000275D0 File Offset: 0x000257D0
		public ulong AddNotifyAchievementsUnlocked(AddNotifyAchievementsUnlockedOptions options, object clientData, OnAchievementsUnlockedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAchievementsUnlockedOptionsInternal, AddNotifyAchievementsUnlockedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAchievementsUnlockedCallbackInternal onAchievementsUnlockedCallbackInternal = new OnAchievementsUnlockedCallbackInternal(AchievementsInterface.OnAchievementsUnlockedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onAchievementsUnlockedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Achievements_AddNotifyAchievementsUnlocked(base.InnerHandle, zero, zero2, onAchievementsUnlockedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00027630 File Offset: 0x00025830
		public ulong AddNotifyAchievementsUnlockedV2(AddNotifyAchievementsUnlockedV2Options options, object clientData, OnAchievementsUnlockedCallbackV2 notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAchievementsUnlockedV2OptionsInternal, AddNotifyAchievementsUnlockedV2Options>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAchievementsUnlockedCallbackV2Internal onAchievementsUnlockedCallbackV2Internal = new OnAchievementsUnlockedCallbackV2Internal(AchievementsInterface.OnAchievementsUnlockedCallbackV2InternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onAchievementsUnlockedCallbackV2Internal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Achievements_AddNotifyAchievementsUnlockedV2(base.InnerHandle, zero, zero2, onAchievementsUnlockedCallbackV2Internal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00027690 File Offset: 0x00025890
		public Result CopyAchievementDefinitionByAchievementId(CopyAchievementDefinitionByAchievementIdOptions options, out Definition outDefinition)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyAchievementDefinitionByAchievementIdOptionsInternal, CopyAchievementDefinitionByAchievementIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionByAchievementId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero2, out outDefinition))
			{
				Bindings.EOS_Achievements_Definition_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000276D8 File Offset: 0x000258D8
		public Result CopyAchievementDefinitionByIndex(CopyAchievementDefinitionByIndexOptions options, out Definition outDefinition)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyAchievementDefinitionByIndexOptionsInternal, CopyAchievementDefinitionByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero2, out outDefinition))
			{
				Bindings.EOS_Achievements_Definition_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00027720 File Offset: 0x00025920
		public Result CopyAchievementDefinitionV2ByAchievementId(CopyAchievementDefinitionV2ByAchievementIdOptions options, out DefinitionV2 outDefinition)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyAchievementDefinitionV2ByAchievementIdOptionsInternal, CopyAchievementDefinitionV2ByAchievementIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<DefinitionV2Internal, DefinitionV2>(zero2, out outDefinition))
			{
				Bindings.EOS_Achievements_DefinitionV2_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00027768 File Offset: 0x00025968
		public Result CopyAchievementDefinitionV2ByIndex(CopyAchievementDefinitionV2ByIndexOptions options, out DefinitionV2 outDefinition)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyAchievementDefinitionV2ByIndexOptionsInternal, CopyAchievementDefinitionV2ByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionV2ByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<DefinitionV2Internal, DefinitionV2>(zero2, out outDefinition))
			{
				Bindings.EOS_Achievements_DefinitionV2_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000277B0 File Offset: 0x000259B0
		public Result CopyPlayerAchievementByAchievementId(CopyPlayerAchievementByAchievementIdOptions options, out PlayerAchievement outAchievement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyPlayerAchievementByAchievementIdOptionsInternal, CopyPlayerAchievementByAchievementIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyPlayerAchievementByAchievementId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<PlayerAchievementInternal, PlayerAchievement>(zero2, out outAchievement))
			{
				Bindings.EOS_Achievements_PlayerAchievement_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000277F8 File Offset: 0x000259F8
		public Result CopyPlayerAchievementByIndex(CopyPlayerAchievementByIndexOptions options, out PlayerAchievement outAchievement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyPlayerAchievementByIndexOptionsInternal, CopyPlayerAchievementByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyPlayerAchievementByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<PlayerAchievementInternal, PlayerAchievement>(zero2, out outAchievement))
			{
				Bindings.EOS_Achievements_PlayerAchievement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00027840 File Offset: 0x00025A40
		public Result CopyUnlockedAchievementByAchievementId(CopyUnlockedAchievementByAchievementIdOptions options, out UnlockedAchievement outAchievement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyUnlockedAchievementByAchievementIdOptionsInternal, CopyUnlockedAchievementByAchievementIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyUnlockedAchievementByAchievementId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<UnlockedAchievementInternal, UnlockedAchievement>(zero2, out outAchievement))
			{
				Bindings.EOS_Achievements_UnlockedAchievement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00027888 File Offset: 0x00025A88
		public Result CopyUnlockedAchievementByIndex(CopyUnlockedAchievementByIndexOptions options, out UnlockedAchievement outAchievement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyUnlockedAchievementByIndexOptionsInternal, CopyUnlockedAchievementByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Achievements_CopyUnlockedAchievementByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<UnlockedAchievementInternal, UnlockedAchievement>(zero2, out outAchievement))
			{
				Bindings.EOS_Achievements_UnlockedAchievement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000278D0 File Offset: 0x00025AD0
		public uint GetAchievementDefinitionCount(GetAchievementDefinitionCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetAchievementDefinitionCountOptionsInternal, GetAchievementDefinitionCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Achievements_GetAchievementDefinitionCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00027900 File Offset: 0x00025B00
		public uint GetPlayerAchievementCount(GetPlayerAchievementCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetPlayerAchievementCountOptionsInternal, GetPlayerAchievementCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Achievements_GetPlayerAchievementCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00027930 File Offset: 0x00025B30
		public uint GetUnlockedAchievementCount(GetUnlockedAchievementCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetUnlockedAchievementCountOptionsInternal, GetUnlockedAchievementCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Achievements_GetUnlockedAchievementCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00027960 File Offset: 0x00025B60
		public void QueryDefinitions(QueryDefinitionsOptions options, object clientData, OnQueryDefinitionsCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryDefinitionsOptionsInternal, QueryDefinitionsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryDefinitionsCompleteCallbackInternal onQueryDefinitionsCompleteCallbackInternal = new OnQueryDefinitionsCompleteCallbackInternal(AchievementsInterface.OnQueryDefinitionsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryDefinitionsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Achievements_QueryDefinitions(base.InnerHandle, zero, zero2, onQueryDefinitionsCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000279B4 File Offset: 0x00025BB4
		public void QueryPlayerAchievements(QueryPlayerAchievementsOptions options, object clientData, OnQueryPlayerAchievementsCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryPlayerAchievementsOptionsInternal, QueryPlayerAchievementsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryPlayerAchievementsCompleteCallbackInternal onQueryPlayerAchievementsCompleteCallbackInternal = new OnQueryPlayerAchievementsCompleteCallbackInternal(AchievementsInterface.OnQueryPlayerAchievementsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryPlayerAchievementsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Achievements_QueryPlayerAchievements(base.InnerHandle, zero, zero2, onQueryPlayerAchievementsCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00027A08 File Offset: 0x00025C08
		public void RemoveNotifyAchievementsUnlocked(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Achievements_RemoveNotifyAchievementsUnlocked(base.InnerHandle, inId);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00027A20 File Offset: 0x00025C20
		public void UnlockAchievements(UnlockAchievementsOptions options, object clientData, OnUnlockAchievementsCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnlockAchievementsOptionsInternal, UnlockAchievementsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUnlockAchievementsCompleteCallbackInternal onUnlockAchievementsCompleteCallbackInternal = new OnUnlockAchievementsCompleteCallbackInternal(AchievementsInterface.OnUnlockAchievementsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUnlockAchievementsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Achievements_UnlockAchievements(base.InnerHandle, zero, zero2, onUnlockAchievementsCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00027A74 File Offset: 0x00025C74
		[MonoPInvokeCallback(typeof(OnAchievementsUnlockedCallbackInternal))]
		internal static void OnAchievementsUnlockedCallbackInternalImplementation(IntPtr data)
		{
			OnAchievementsUnlockedCallback onAchievementsUnlockedCallback;
			OnAchievementsUnlockedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallback, OnAchievementsUnlockedCallbackInfoInternal, OnAchievementsUnlockedCallbackInfo>(data, out onAchievementsUnlockedCallback, out data2))
			{
				onAchievementsUnlockedCallback(data2);
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00027A94 File Offset: 0x00025C94
		[MonoPInvokeCallback(typeof(OnAchievementsUnlockedCallbackV2Internal))]
		internal static void OnAchievementsUnlockedCallbackV2InternalImplementation(IntPtr data)
		{
			OnAchievementsUnlockedCallbackV2 onAchievementsUnlockedCallbackV;
			OnAchievementsUnlockedCallbackV2Info data2;
			if (Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallbackV2, OnAchievementsUnlockedCallbackV2InfoInternal, OnAchievementsUnlockedCallbackV2Info>(data, out onAchievementsUnlockedCallbackV, out data2))
			{
				onAchievementsUnlockedCallbackV(data2);
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00027AB4 File Offset: 0x00025CB4
		[MonoPInvokeCallback(typeof(OnQueryDefinitionsCompleteCallbackInternal))]
		internal static void OnQueryDefinitionsCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryDefinitionsCompleteCallback onQueryDefinitionsCompleteCallback;
			OnQueryDefinitionsCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryDefinitionsCompleteCallback, OnQueryDefinitionsCompleteCallbackInfoInternal, OnQueryDefinitionsCompleteCallbackInfo>(data, out onQueryDefinitionsCompleteCallback, out data2))
			{
				onQueryDefinitionsCompleteCallback(data2);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00027AD4 File Offset: 0x00025CD4
		[MonoPInvokeCallback(typeof(OnQueryPlayerAchievementsCompleteCallbackInternal))]
		internal static void OnQueryPlayerAchievementsCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryPlayerAchievementsCompleteCallback onQueryPlayerAchievementsCompleteCallback;
			OnQueryPlayerAchievementsCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryPlayerAchievementsCompleteCallback, OnQueryPlayerAchievementsCompleteCallbackInfoInternal, OnQueryPlayerAchievementsCompleteCallbackInfo>(data, out onQueryPlayerAchievementsCompleteCallback, out data2))
			{
				onQueryPlayerAchievementsCompleteCallback(data2);
			}
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00027AF4 File Offset: 0x00025CF4
		[MonoPInvokeCallback(typeof(OnUnlockAchievementsCompleteCallbackInternal))]
		internal static void OnUnlockAchievementsCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnUnlockAchievementsCompleteCallback onUnlockAchievementsCompleteCallback;
			OnUnlockAchievementsCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUnlockAchievementsCompleteCallback, OnUnlockAchievementsCompleteCallbackInfoInternal, OnUnlockAchievementsCompleteCallbackInfo>(data, out onUnlockAchievementsCompleteCallback, out data2))
			{
				onUnlockAchievementsCompleteCallback(data2);
			}
		}

		// Token: 0x04001194 RID: 4500
		public const int AchievementUnlocktimeUndefined = -1;

		// Token: 0x04001195 RID: 4501
		public const int AddnotifyachievementsunlockedApiLatest = 1;

		// Token: 0x04001196 RID: 4502
		public const int Addnotifyachievementsunlockedv2ApiLatest = 2;

		// Token: 0x04001197 RID: 4503
		public const int Copyachievementdefinitionv2ByachievementidApiLatest = 2;

		// Token: 0x04001198 RID: 4504
		public const int Copyachievementdefinitionv2ByindexApiLatest = 2;

		// Token: 0x04001199 RID: 4505
		public const int CopydefinitionbyachievementidApiLatest = 1;

		// Token: 0x0400119A RID: 4506
		public const int CopydefinitionbyindexApiLatest = 1;

		// Token: 0x0400119B RID: 4507
		public const int Copydefinitionv2ByachievementidApiLatest = 2;

		// Token: 0x0400119C RID: 4508
		public const int Copydefinitionv2ByindexApiLatest = 2;

		// Token: 0x0400119D RID: 4509
		public const int CopyplayerachievementbyachievementidApiLatest = 2;

		// Token: 0x0400119E RID: 4510
		public const int CopyplayerachievementbyindexApiLatest = 2;

		// Token: 0x0400119F RID: 4511
		public const int CopyunlockedachievementbyachievementidApiLatest = 1;

		// Token: 0x040011A0 RID: 4512
		public const int CopyunlockedachievementbyindexApiLatest = 1;

		// Token: 0x040011A1 RID: 4513
		public const int DefinitionApiLatest = 1;

		// Token: 0x040011A2 RID: 4514
		public const int Definitionv2ApiLatest = 2;

		// Token: 0x040011A3 RID: 4515
		public const int GetachievementdefinitioncountApiLatest = 1;

		// Token: 0x040011A4 RID: 4516
		public const int GetplayerachievementcountApiLatest = 1;

		// Token: 0x040011A5 RID: 4517
		public const int GetunlockedachievementcountApiLatest = 1;

		// Token: 0x040011A6 RID: 4518
		public const int PlayerachievementApiLatest = 2;

		// Token: 0x040011A7 RID: 4519
		public const int PlayerstatinfoApiLatest = 1;

		// Token: 0x040011A8 RID: 4520
		public const int QuerydefinitionsApiLatest = 3;

		// Token: 0x040011A9 RID: 4521
		public const int QueryplayerachievementsApiLatest = 2;

		// Token: 0x040011AA RID: 4522
		public const int StatthresholdApiLatest = 1;

		// Token: 0x040011AB RID: 4523
		public const int StatthresholdsApiLatest = 1;

		// Token: 0x040011AC RID: 4524
		public const int UnlockachievementsApiLatest = 1;

		// Token: 0x040011AD RID: 4525
		public const int UnlockedachievementApiLatest = 1;
	}
}
