using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011A RID: 282
	public sealed class SessionModification : Handle
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x000036D3 File Offset: 0x000018D3
		public SessionModification()
		{
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000036DB File Offset: 0x000018DB
		public SessionModification(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00008E20 File Offset: 0x00007020
		public Result AddAttribute(SessionModificationAddAttributeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationAddAttributeOptionsInternal, SessionModificationAddAttributeOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_AddAttribute(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00008E50 File Offset: 0x00007050
		public void Release()
		{
			Bindings.EOS_SessionModification_Release(base.InnerHandle);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00008E60 File Offset: 0x00007060
		public Result RemoveAttribute(SessionModificationRemoveAttributeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationRemoveAttributeOptionsInternal, SessionModificationRemoveAttributeOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_RemoveAttribute(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00008E90 File Offset: 0x00007090
		public Result SetBucketId(SessionModificationSetBucketIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationSetBucketIdOptionsInternal, SessionModificationSetBucketIdOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_SetBucketId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00008EC0 File Offset: 0x000070C0
		public Result SetHostAddress(SessionModificationSetHostAddressOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationSetHostAddressOptionsInternal, SessionModificationSetHostAddressOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_SetHostAddress(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00008EF0 File Offset: 0x000070F0
		public Result SetInvitesAllowed(SessionModificationSetInvitesAllowedOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationSetInvitesAllowedOptionsInternal, SessionModificationSetInvitesAllowedOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_SetInvitesAllowed(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00008F20 File Offset: 0x00007120
		public Result SetJoinInProgressAllowed(SessionModificationSetJoinInProgressAllowedOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationSetJoinInProgressAllowedOptionsInternal, SessionModificationSetJoinInProgressAllowedOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_SetJoinInProgressAllowed(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00008F50 File Offset: 0x00007150
		public Result SetMaxPlayers(SessionModificationSetMaxPlayersOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationSetMaxPlayersOptionsInternal, SessionModificationSetMaxPlayersOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_SetMaxPlayers(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00008F80 File Offset: 0x00007180
		public Result SetPermissionLevel(SessionModificationSetPermissionLevelOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionModificationSetPermissionLevelOptionsInternal, SessionModificationSetPermissionLevelOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionModification_SetPermissionLevel(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x040003E0 RID: 992
		public const int SessionmodificationAddattributeApiLatest = 1;

		// Token: 0x040003E1 RID: 993
		public const int SessionmodificationMaxSessionAttributeLength = 64;

		// Token: 0x040003E2 RID: 994
		public const int SessionmodificationMaxSessionAttributes = 64;

		// Token: 0x040003E3 RID: 995
		public const int SessionmodificationMaxSessionidoverrideLength = 64;

		// Token: 0x040003E4 RID: 996
		public const int SessionmodificationMinSessionidoverrideLength = 16;

		// Token: 0x040003E5 RID: 997
		public const int SessionmodificationRemoveattributeApiLatest = 1;

		// Token: 0x040003E6 RID: 998
		public const int SessionmodificationSetbucketidApiLatest = 1;

		// Token: 0x040003E7 RID: 999
		public const int SessionmodificationSethostaddressApiLatest = 1;

		// Token: 0x040003E8 RID: 1000
		public const int SessionmodificationSetinvitesallowedApiLatest = 1;

		// Token: 0x040003E9 RID: 1001
		public const int SessionmodificationSetjoininprogressallowedApiLatest = 1;

		// Token: 0x040003EA RID: 1002
		public const int SessionmodificationSetmaxplayersApiLatest = 1;

		// Token: 0x040003EB RID: 1003
		public const int SessionmodificationSetpermissionlevelApiLatest = 1;
	}
}
