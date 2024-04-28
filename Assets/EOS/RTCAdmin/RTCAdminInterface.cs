using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C2 RID: 450
	public sealed class RTCAdminInterface : Handle
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x000036D3 File Offset: 0x000018D3
		public RTCAdminInterface()
		{
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000036DB File Offset: 0x000018DB
		public RTCAdminInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0000CE60 File Offset: 0x0000B060
		public Result CopyUserTokenByIndex(CopyUserTokenByIndexOptions options, out UserToken outUserToken)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyUserTokenByIndexOptionsInternal, CopyUserTokenByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_RTCAdmin_CopyUserTokenByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<UserTokenInternal, UserToken>(zero2, out outUserToken))
			{
				Bindings.EOS_RTCAdmin_UserToken_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public Result CopyUserTokenByUserId(CopyUserTokenByUserIdOptions options, out UserToken outUserToken)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyUserTokenByUserIdOptionsInternal, CopyUserTokenByUserIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_RTCAdmin_CopyUserTokenByUserId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<UserTokenInternal, UserToken>(zero2, out outUserToken))
			{
				Bindings.EOS_RTCAdmin_UserToken_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public void Kick(KickOptions options, object clientData, OnKickCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<KickOptionsInternal, KickOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnKickCompleteCallbackInternal onKickCompleteCallbackInternal = new OnKickCompleteCallbackInternal(RTCAdminInterface.OnKickCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onKickCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAdmin_Kick(base.InnerHandle, zero, zero2, onKickCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0000CF44 File Offset: 0x0000B144
		public void QueryJoinRoomToken(QueryJoinRoomTokenOptions options, object clientData, OnQueryJoinRoomTokenCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryJoinRoomTokenOptionsInternal, QueryJoinRoomTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryJoinRoomTokenCompleteCallbackInternal onQueryJoinRoomTokenCompleteCallbackInternal = new OnQueryJoinRoomTokenCompleteCallbackInternal(RTCAdminInterface.OnQueryJoinRoomTokenCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryJoinRoomTokenCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAdmin_QueryJoinRoomToken(base.InnerHandle, zero, zero2, onQueryJoinRoomTokenCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0000CF98 File Offset: 0x0000B198
		public void SetParticipantHardMute(SetParticipantHardMuteOptions options, object clientData, OnSetParticipantHardMuteCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetParticipantHardMuteOptionsInternal, SetParticipantHardMuteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSetParticipantHardMuteCompleteCallbackInternal onSetParticipantHardMuteCompleteCallbackInternal = new OnSetParticipantHardMuteCompleteCallbackInternal(RTCAdminInterface.OnSetParticipantHardMuteCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSetParticipantHardMuteCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAdmin_SetParticipantHardMute(base.InnerHandle, zero, zero2, onSetParticipantHardMuteCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		[MonoPInvokeCallback(typeof(OnKickCompleteCallbackInternal))]
		internal static void OnKickCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnKickCompleteCallback onKickCompleteCallback;
			KickCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnKickCompleteCallback, KickCompleteCallbackInfoInternal, KickCompleteCallbackInfo>(data, out onKickCompleteCallback, out data2))
			{
				onKickCompleteCallback(data2);
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0000D00C File Offset: 0x0000B20C
		[MonoPInvokeCallback(typeof(OnQueryJoinRoomTokenCompleteCallbackInternal))]
		internal static void OnQueryJoinRoomTokenCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryJoinRoomTokenCompleteCallback onQueryJoinRoomTokenCompleteCallback;
			QueryJoinRoomTokenCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryJoinRoomTokenCompleteCallback, QueryJoinRoomTokenCompleteCallbackInfoInternal, QueryJoinRoomTokenCompleteCallbackInfo>(data, out onQueryJoinRoomTokenCompleteCallback, out data2))
			{
				onQueryJoinRoomTokenCompleteCallback(data2);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0000D02C File Offset: 0x0000B22C
		[MonoPInvokeCallback(typeof(OnSetParticipantHardMuteCompleteCallbackInternal))]
		internal static void OnSetParticipantHardMuteCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnSetParticipantHardMuteCompleteCallback onSetParticipantHardMuteCompleteCallback;
			SetParticipantHardMuteCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSetParticipantHardMuteCompleteCallback, SetParticipantHardMuteCompleteCallbackInfoInternal, SetParticipantHardMuteCompleteCallbackInfo>(data, out onSetParticipantHardMuteCompleteCallback, out data2))
			{
				onSetParticipantHardMuteCompleteCallback(data2);
			}
		}

		// Token: 0x040005A7 RID: 1447
		public const int CopyusertokenbyindexApiLatest = 2;

		// Token: 0x040005A8 RID: 1448
		public const int CopyusertokenbyuseridApiLatest = 2;

		// Token: 0x040005A9 RID: 1449
		public const int KickApiLatest = 1;

		// Token: 0x040005AA RID: 1450
		public const int QueryjoinroomtokenApiLatest = 2;

		// Token: 0x040005AB RID: 1451
		public const int SetparticipanthardmuteApiLatest = 1;

		// Token: 0x040005AC RID: 1452
		public const int UsertokenApiLatest = 1;
	}
}
