using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000E2 RID: 226
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamUGCDetails_t
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x00021493 File Offset: 0x0001F693
		internal static SteamUGCDetails_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamUGCDetails_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamUGCDetails_t.PackSmall));
			}
			return (SteamUGCDetails_t)Marshal.PtrToStructure(p, typeof(SteamUGCDetails_t));
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000214CC File Offset: 0x0001F6CC
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamUGCDetails_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamUGCDetails_t));
		}

		// Token: 0x0400065E RID: 1630
		internal ulong PublishedFileId;

		// Token: 0x0400065F RID: 1631
		internal Result Result;

		// Token: 0x04000660 RID: 1632
		internal WorkshopFileType FileType;

		// Token: 0x04000661 RID: 1633
		internal uint CreatorAppID;

		// Token: 0x04000662 RID: 1634
		internal uint ConsumerAppID;

		// Token: 0x04000663 RID: 1635
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string Title;

		// Token: 0x04000664 RID: 1636
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		internal string Description;

		// Token: 0x04000665 RID: 1637
		internal ulong SteamIDOwner;

		// Token: 0x04000666 RID: 1638
		internal uint TimeCreated;

		// Token: 0x04000667 RID: 1639
		internal uint TimeUpdated;

		// Token: 0x04000668 RID: 1640
		internal uint TimeAddedToUserList;

		// Token: 0x04000669 RID: 1641
		internal RemoteStoragePublishedFileVisibility Visibility;

		// Token: 0x0400066A RID: 1642
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned;

		// Token: 0x0400066B RID: 1643
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse;

		// Token: 0x0400066C RID: 1644
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated;

		// Token: 0x0400066D RID: 1645
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		internal string Tags;

		// Token: 0x0400066E RID: 1646
		internal ulong File;

		// Token: 0x0400066F RID: 1647
		internal ulong PreviewFile;

		// Token: 0x04000670 RID: 1648
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName;

		// Token: 0x04000671 RID: 1649
		internal int FileSize;

		// Token: 0x04000672 RID: 1650
		internal int PreviewFileSize;

		// Token: 0x04000673 RID: 1651
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL;

		// Token: 0x04000674 RID: 1652
		internal uint VotesUp;

		// Token: 0x04000675 RID: 1653
		internal uint VotesDown;

		// Token: 0x04000676 RID: 1654
		internal float Score;

		// Token: 0x04000677 RID: 1655
		internal uint NumChildren;

		// Token: 0x02000206 RID: 518
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D42 RID: 7490 RVA: 0x00062C9C File Offset: 0x00060E9C
			public static implicit operator SteamUGCDetails_t(SteamUGCDetails_t.PackSmall d)
			{
				return new SteamUGCDetails_t
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					FileType = d.FileType,
					CreatorAppID = d.CreatorAppID,
					ConsumerAppID = d.ConsumerAppID,
					Title = d.Title,
					Description = d.Description,
					SteamIDOwner = d.SteamIDOwner,
					TimeCreated = d.TimeCreated,
					TimeUpdated = d.TimeUpdated,
					TimeAddedToUserList = d.TimeAddedToUserList,
					Visibility = d.Visibility,
					Banned = d.Banned,
					AcceptedForUse = d.AcceptedForUse,
					TagsTruncated = d.TagsTruncated,
					Tags = d.Tags,
					File = d.File,
					PreviewFile = d.PreviewFile,
					PchFileName = d.PchFileName,
					FileSize = d.FileSize,
					PreviewFileSize = d.PreviewFileSize,
					URL = d.URL,
					VotesUp = d.VotesUp,
					VotesDown = d.VotesDown,
					Score = d.Score,
					NumChildren = d.NumChildren
				};
			}

			// Token: 0x04000AA6 RID: 2726
			internal ulong PublishedFileId;

			// Token: 0x04000AA7 RID: 2727
			internal Result Result;

			// Token: 0x04000AA8 RID: 2728
			internal WorkshopFileType FileType;

			// Token: 0x04000AA9 RID: 2729
			internal uint CreatorAppID;

			// Token: 0x04000AAA RID: 2730
			internal uint ConsumerAppID;

			// Token: 0x04000AAB RID: 2731
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			internal string Title;

			// Token: 0x04000AAC RID: 2732
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			internal string Description;

			// Token: 0x04000AAD RID: 2733
			internal ulong SteamIDOwner;

			// Token: 0x04000AAE RID: 2734
			internal uint TimeCreated;

			// Token: 0x04000AAF RID: 2735
			internal uint TimeUpdated;

			// Token: 0x04000AB0 RID: 2736
			internal uint TimeAddedToUserList;

			// Token: 0x04000AB1 RID: 2737
			internal RemoteStoragePublishedFileVisibility Visibility;

			// Token: 0x04000AB2 RID: 2738
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned;

			// Token: 0x04000AB3 RID: 2739
			[MarshalAs(UnmanagedType.I1)]
			internal bool AcceptedForUse;

			// Token: 0x04000AB4 RID: 2740
			[MarshalAs(UnmanagedType.I1)]
			internal bool TagsTruncated;

			// Token: 0x04000AB5 RID: 2741
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			internal string Tags;

			// Token: 0x04000AB6 RID: 2742
			internal ulong File;

			// Token: 0x04000AB7 RID: 2743
			internal ulong PreviewFile;

			// Token: 0x04000AB8 RID: 2744
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName;

			// Token: 0x04000AB9 RID: 2745
			internal int FileSize;

			// Token: 0x04000ABA RID: 2746
			internal int PreviewFileSize;

			// Token: 0x04000ABB RID: 2747
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL;

			// Token: 0x04000ABC RID: 2748
			internal uint VotesUp;

			// Token: 0x04000ABD RID: 2749
			internal uint VotesDown;

			// Token: 0x04000ABE RID: 2750
			internal float Score;

			// Token: 0x04000ABF RID: 2751
			internal uint NumChildren;
		}
	}
}
