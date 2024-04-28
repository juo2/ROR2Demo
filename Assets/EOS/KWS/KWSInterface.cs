using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E9 RID: 1001
	public sealed class KWSInterface : Handle
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x000036D3 File Offset: 0x000018D3
		public KWSInterface()
		{
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000036DB File Offset: 0x000018DB
		public KWSInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000198A4 File Offset: 0x00017AA4
		public ulong AddNotifyPermissionsUpdateReceived(AddNotifyPermissionsUpdateReceivedOptions options, object clientData, OnPermissionsUpdateReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyPermissionsUpdateReceivedOptionsInternal, AddNotifyPermissionsUpdateReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnPermissionsUpdateReceivedCallbackInternal onPermissionsUpdateReceivedCallbackInternal = new OnPermissionsUpdateReceivedCallbackInternal(KWSInterface.OnPermissionsUpdateReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onPermissionsUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_KWS_AddNotifyPermissionsUpdateReceived(base.InnerHandle, zero, zero2, onPermissionsUpdateReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00019904 File Offset: 0x00017B04
		public Result CopyPermissionByIndex(CopyPermissionByIndexOptions options, out PermissionStatus outPermission)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyPermissionByIndexOptionsInternal, CopyPermissionByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_KWS_CopyPermissionByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<PermissionStatusInternal, PermissionStatus>(zero2, out outPermission))
			{
				Bindings.EOS_KWS_PermissionStatus_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0001994C File Offset: 0x00017B4C
		public void CreateUser(CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateUserOptionsInternal, CreateUserOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCreateUserCallbackInternal onCreateUserCallbackInternal = new OnCreateUserCallbackInternal(KWSInterface.OnCreateUserCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onCreateUserCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_CreateUser(base.InnerHandle, zero, zero2, onCreateUserCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000199A0 File Offset: 0x00017BA0
		public Result GetPermissionByKey(GetPermissionByKeyOptions options, out KWSPermissionStatus outPermission)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetPermissionByKeyOptionsInternal, GetPermissionByKeyOptions>(ref zero, options);
			outPermission = Helper.GetDefault<KWSPermissionStatus>();
			Result result = Bindings.EOS_KWS_GetPermissionByKey(base.InnerHandle, zero, ref outPermission);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000199D8 File Offset: 0x00017BD8
		public int GetPermissionsCount(GetPermissionsCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetPermissionsCountOptionsInternal, GetPermissionsCountOptions>(ref zero, options);
			int result = Bindings.EOS_KWS_GetPermissionsCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00019A08 File Offset: 0x00017C08
		public void QueryAgeGate(QueryAgeGateOptions options, object clientData, OnQueryAgeGateCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryAgeGateOptionsInternal, QueryAgeGateOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryAgeGateCallbackInternal onQueryAgeGateCallbackInternal = new OnQueryAgeGateCallbackInternal(KWSInterface.OnQueryAgeGateCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryAgeGateCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_QueryAgeGate(base.InnerHandle, zero, zero2, onQueryAgeGateCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00019A5C File Offset: 0x00017C5C
		public void QueryPermissions(QueryPermissionsOptions options, object clientData, OnQueryPermissionsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryPermissionsOptionsInternal, QueryPermissionsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryPermissionsCallbackInternal onQueryPermissionsCallbackInternal = new OnQueryPermissionsCallbackInternal(KWSInterface.OnQueryPermissionsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryPermissionsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_QueryPermissions(base.InnerHandle, zero, zero2, onQueryPermissionsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00019AB0 File Offset: 0x00017CB0
		public void RemoveNotifyPermissionsUpdateReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_KWS_RemoveNotifyPermissionsUpdateReceived(base.InnerHandle, inId);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public void RequestPermissions(RequestPermissionsOptions options, object clientData, OnRequestPermissionsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RequestPermissionsOptionsInternal, RequestPermissionsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRequestPermissionsCallbackInternal onRequestPermissionsCallbackInternal = new OnRequestPermissionsCallbackInternal(KWSInterface.OnRequestPermissionsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onRequestPermissionsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_RequestPermissions(base.InnerHandle, zero, zero2, onRequestPermissionsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00019B1C File Offset: 0x00017D1C
		public void UpdateParentEmail(UpdateParentEmailOptions options, object clientData, OnUpdateParentEmailCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateParentEmailOptionsInternal, UpdateParentEmailOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUpdateParentEmailCallbackInternal onUpdateParentEmailCallbackInternal = new OnUpdateParentEmailCallbackInternal(KWSInterface.OnUpdateParentEmailCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUpdateParentEmailCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_KWS_UpdateParentEmail(base.InnerHandle, zero, zero2, onUpdateParentEmailCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00019B70 File Offset: 0x00017D70
		[MonoPInvokeCallback(typeof(OnCreateUserCallbackInternal))]
		internal static void OnCreateUserCallbackInternalImplementation(IntPtr data)
		{
			OnCreateUserCallback onCreateUserCallback;
			CreateUserCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCreateUserCallback, CreateUserCallbackInfoInternal, CreateUserCallbackInfo>(data, out onCreateUserCallback, out data2))
			{
				onCreateUserCallback(data2);
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00019B90 File Offset: 0x00017D90
		[MonoPInvokeCallback(typeof(OnPermissionsUpdateReceivedCallbackInternal))]
		internal static void OnPermissionsUpdateReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnPermissionsUpdateReceivedCallback onPermissionsUpdateReceivedCallback;
			PermissionsUpdateReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnPermissionsUpdateReceivedCallback, PermissionsUpdateReceivedCallbackInfoInternal, PermissionsUpdateReceivedCallbackInfo>(data, out onPermissionsUpdateReceivedCallback, out data2))
			{
				onPermissionsUpdateReceivedCallback(data2);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00019BB0 File Offset: 0x00017DB0
		[MonoPInvokeCallback(typeof(OnQueryAgeGateCallbackInternal))]
		internal static void OnQueryAgeGateCallbackInternalImplementation(IntPtr data)
		{
			OnQueryAgeGateCallback onQueryAgeGateCallback;
			QueryAgeGateCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryAgeGateCallback, QueryAgeGateCallbackInfoInternal, QueryAgeGateCallbackInfo>(data, out onQueryAgeGateCallback, out data2))
			{
				onQueryAgeGateCallback(data2);
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00019BD0 File Offset: 0x00017DD0
		[MonoPInvokeCallback(typeof(OnQueryPermissionsCallbackInternal))]
		internal static void OnQueryPermissionsCallbackInternalImplementation(IntPtr data)
		{
			OnQueryPermissionsCallback onQueryPermissionsCallback;
			QueryPermissionsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryPermissionsCallback, QueryPermissionsCallbackInfoInternal, QueryPermissionsCallbackInfo>(data, out onQueryPermissionsCallback, out data2))
			{
				onQueryPermissionsCallback(data2);
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00019BF0 File Offset: 0x00017DF0
		[MonoPInvokeCallback(typeof(OnRequestPermissionsCallbackInternal))]
		internal static void OnRequestPermissionsCallbackInternalImplementation(IntPtr data)
		{
			OnRequestPermissionsCallback onRequestPermissionsCallback;
			RequestPermissionsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnRequestPermissionsCallback, RequestPermissionsCallbackInfoInternal, RequestPermissionsCallbackInfo>(data, out onRequestPermissionsCallback, out data2))
			{
				onRequestPermissionsCallback(data2);
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00019C10 File Offset: 0x00017E10
		[MonoPInvokeCallback(typeof(OnUpdateParentEmailCallbackInternal))]
		internal static void OnUpdateParentEmailCallbackInternalImplementation(IntPtr data)
		{
			OnUpdateParentEmailCallback onUpdateParentEmailCallback;
			UpdateParentEmailCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUpdateParentEmailCallback, UpdateParentEmailCallbackInfoInternal, UpdateParentEmailCallbackInfo>(data, out onUpdateParentEmailCallback, out data2))
			{
				onUpdateParentEmailCallback(data2);
			}
		}

		// Token: 0x04000B57 RID: 2903
		public const int AddnotifypermissionsupdatereceivedApiLatest = 1;

		// Token: 0x04000B58 RID: 2904
		public const int CopypermissionbyindexApiLatest = 1;

		// Token: 0x04000B59 RID: 2905
		public const int CreateuserApiLatest = 1;

		// Token: 0x04000B5A RID: 2906
		public const int GetpermissionbykeyApiLatest = 1;

		// Token: 0x04000B5B RID: 2907
		public const int GetpermissionscountApiLatest = 1;

		// Token: 0x04000B5C RID: 2908
		public const int MaxPermissionLength = 32;

		// Token: 0x04000B5D RID: 2909
		public const int MaxPermissions = 16;

		// Token: 0x04000B5E RID: 2910
		public const int PermissionstatusApiLatest = 1;

		// Token: 0x04000B5F RID: 2911
		public const int QueryagegateApiLatest = 1;

		// Token: 0x04000B60 RID: 2912
		public const int QuerypermissionsApiLatest = 1;

		// Token: 0x04000B61 RID: 2913
		public const int RequestpermissionsApiLatest = 1;

		// Token: 0x04000B62 RID: 2914
		public const int UpdateparentemailApiLatest = 1;
	}
}
