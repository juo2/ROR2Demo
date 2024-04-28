using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200021E RID: 542
	public sealed class PresenceModification : Handle
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x000036D3 File Offset: 0x000018D3
		public PresenceModification()
		{
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000036DB File Offset: 0x000018DB
		public PresenceModification(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		public Result DeleteData(PresenceModificationDeleteDataOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PresenceModificationDeleteDataOptionsInternal, PresenceModificationDeleteDataOptions>(ref zero, options);
			Result result = Bindings.EOS_PresenceModification_DeleteData(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		public void Release()
		{
			Bindings.EOS_PresenceModification_Release(base.InnerHandle);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0000F608 File Offset: 0x0000D808
		public Result SetData(PresenceModificationSetDataOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PresenceModificationSetDataOptionsInternal, PresenceModificationSetDataOptions>(ref zero, options);
			Result result = Bindings.EOS_PresenceModification_SetData(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0000F638 File Offset: 0x0000D838
		public Result SetJoinInfo(PresenceModificationSetJoinInfoOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PresenceModificationSetJoinInfoOptionsInternal, PresenceModificationSetJoinInfoOptions>(ref zero, options);
			Result result = Bindings.EOS_PresenceModification_SetJoinInfo(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0000F668 File Offset: 0x0000D868
		public Result SetRawRichText(PresenceModificationSetRawRichTextOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PresenceModificationSetRawRichTextOptionsInternal, PresenceModificationSetRawRichTextOptions>(ref zero, options);
			Result result = Bindings.EOS_PresenceModification_SetRawRichText(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0000F698 File Offset: 0x0000D898
		public Result SetStatus(PresenceModificationSetStatusOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PresenceModificationSetStatusOptionsInternal, PresenceModificationSetStatusOptions>(ref zero, options);
			Result result = Bindings.EOS_PresenceModification_SetStatus(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x040006AC RID: 1708
		public const int PresencemodificationDatarecordidApiLatest = 1;

		// Token: 0x040006AD RID: 1709
		public const int PresencemodificationDeletedataApiLatest = 1;

		// Token: 0x040006AE RID: 1710
		public const int PresencemodificationJoininfoMaxLength = 255;

		// Token: 0x040006AF RID: 1711
		public const int PresencemodificationSetdataApiLatest = 1;

		// Token: 0x040006B0 RID: 1712
		public const int PresencemodificationSetjoininfoApiLatest = 1;

		// Token: 0x040006B1 RID: 1713
		public const int PresencemodificationSetrawrichtextApiLatest = 1;

		// Token: 0x040006B2 RID: 1714
		public const int PresencemodificationSetstatusApiLatest = 1;
	}
}
