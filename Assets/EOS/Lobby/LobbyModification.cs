using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034A RID: 842
	public sealed class LobbyModification : Handle
	{
		// Token: 0x06001514 RID: 5396 RVA: 0x000036D3 File Offset: 0x000018D3
		public LobbyModification()
		{
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x000036DB File Offset: 0x000018DB
		public LobbyModification(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00016F20 File Offset: 0x00015120
		public Result AddAttribute(LobbyModificationAddAttributeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationAddAttributeOptionsInternal, LobbyModificationAddAttributeOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_AddAttribute(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00016F50 File Offset: 0x00015150
		public Result AddMemberAttribute(LobbyModificationAddMemberAttributeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationAddMemberAttributeOptionsInternal, LobbyModificationAddMemberAttributeOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_AddMemberAttribute(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00016F80 File Offset: 0x00015180
		public void Release()
		{
			Bindings.EOS_LobbyModification_Release(base.InnerHandle);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00016F90 File Offset: 0x00015190
		public Result RemoveAttribute(LobbyModificationRemoveAttributeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationRemoveAttributeOptionsInternal, LobbyModificationRemoveAttributeOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_RemoveAttribute(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00016FC0 File Offset: 0x000151C0
		public Result RemoveMemberAttribute(LobbyModificationRemoveMemberAttributeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationRemoveMemberAttributeOptionsInternal, LobbyModificationRemoveMemberAttributeOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_RemoveMemberAttribute(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00016FF0 File Offset: 0x000151F0
		public Result SetBucketId(LobbyModificationSetBucketIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationSetBucketIdOptionsInternal, LobbyModificationSetBucketIdOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_SetBucketId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00017020 File Offset: 0x00015220
		public Result SetInvitesAllowed(LobbyModificationSetInvitesAllowedOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationSetInvitesAllowedOptionsInternal, LobbyModificationSetInvitesAllowedOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_SetInvitesAllowed(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00017050 File Offset: 0x00015250
		public Result SetMaxMembers(LobbyModificationSetMaxMembersOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationSetMaxMembersOptionsInternal, LobbyModificationSetMaxMembersOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_SetMaxMembers(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00017080 File Offset: 0x00015280
		public Result SetPermissionLevel(LobbyModificationSetPermissionLevelOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyModificationSetPermissionLevelOptionsInternal, LobbyModificationSetPermissionLevelOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbyModification_SetPermissionLevel(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x04000A21 RID: 2593
		public const int LobbymodificationAddattributeApiLatest = 1;

		// Token: 0x04000A22 RID: 2594
		public const int LobbymodificationAddmemberattributeApiLatest = 1;

		// Token: 0x04000A23 RID: 2595
		public const int LobbymodificationMaxAttributeLength = 64;

		// Token: 0x04000A24 RID: 2596
		public const int LobbymodificationMaxAttributes = 64;

		// Token: 0x04000A25 RID: 2597
		public const int LobbymodificationRemoveattributeApiLatest = 1;

		// Token: 0x04000A26 RID: 2598
		public const int LobbymodificationRemovememberattributeApiLatest = 1;

		// Token: 0x04000A27 RID: 2599
		public const int LobbymodificationSetbucketidApiLatest = 1;

		// Token: 0x04000A28 RID: 2600
		public const int LobbymodificationSetinvitesallowedApiLatest = 1;

		// Token: 0x04000A29 RID: 2601
		public const int LobbymodificationSetmaxmembersApiLatest = 1;

		// Token: 0x04000A2A RID: 2602
		public const int LobbymodificationSetpermissionlevelApiLatest = 1;
	}
}
