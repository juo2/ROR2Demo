using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.UI.LogBook
{
	// Token: 0x02000DD6 RID: 3542
	public class Entry
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x0014FE9E File Offset: 0x0014E09E
		// (set) Token: 0x0600514B RID: 20811 RVA: 0x0014FEA6 File Offset: 0x0014E0A6
		[NotNull]
		public Entry.GetStatusDelegate getStatusImplementation { private get; set; } = new Entry.GetStatusDelegate(Entry.GetStatusDefault);

		// Token: 0x0600514C RID: 20812 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public static EntryStatus GetStatusDefault(in Entry entry, [NotNull] UserProfile viewerProfile)
		{
			return EntryStatus.Unimplemented;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0014FEAF File Offset: 0x0014E0AF
		public EntryStatus GetStatus([NotNull] UserProfile viewerProfile)
		{
			return this.getStatusImplementation(this, viewerProfile);
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600514E RID: 20814 RVA: 0x0014FEBF File Offset: 0x0014E0BF
		// (set) Token: 0x0600514F RID: 20815 RVA: 0x0014FEC7 File Offset: 0x0014E0C7
		public Entry.GetTooltipContentDelegate getTooltipContentImplementation { private get; set; } = new Entry.GetTooltipContentDelegate(Entry.GetTooltipContentDefault);

		// Token: 0x06005150 RID: 20816 RVA: 0x0014FED0 File Offset: 0x0014E0D0
		public static TooltipContent GetTooltipContentDefault(in Entry entry, [NotNull] UserProfile viewerProfile, EntryStatus entryStatus)
		{
			return new TooltipContent
			{
				overrideTitleText = entry.GetDisplayName(viewerProfile),
				overrideBodyText = entry.GetCategoryDisplayName(viewerProfile)
			};
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0014FF04 File Offset: 0x0014E104
		public TooltipContent GetTooltipContent([NotNull] UserProfile viewerProfile, EntryStatus entryStatus)
		{
			return this.getTooltipContentImplementation(this, viewerProfile, entryStatus);
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06005152 RID: 20818 RVA: 0x0014FF15 File Offset: 0x0014E115
		// (set) Token: 0x06005153 RID: 20819 RVA: 0x0014FF1D File Offset: 0x0014E11D
		[NotNull]
		public Entry.GetDisplayNameDelegate getDisplayNameImplementation { private get; set; } = new Entry.GetDisplayNameDelegate(Entry.GetDisplayNameDefault);

		// Token: 0x06005154 RID: 20820 RVA: 0x0014FF26 File Offset: 0x0014E126
		[NotNull]
		public static string GetDisplayNameDefault(in Entry entry, [NotNull] UserProfile viewerProfile)
		{
			return Language.GetString(entry.nameToken ?? string.Empty);
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0014FF3D File Offset: 0x0014E13D
		[NotNull]
		public string GetDisplayName([NotNull] UserProfile viewerProfile)
		{
			return this.getDisplayNameImplementation(this, viewerProfile);
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06005156 RID: 20822 RVA: 0x0014FF4D File Offset: 0x0014E14D
		// (set) Token: 0x06005157 RID: 20823 RVA: 0x0014FF55 File Offset: 0x0014E155
		[NotNull]
		public Entry.GetDisplayNameDelegate getCategoryDisplayNameImplementation { private get; set; } = new Entry.GetDisplayNameDelegate(Entry.GetCategoryDisplayNameDefault);

		// Token: 0x06005158 RID: 20824 RVA: 0x0014FF5E File Offset: 0x0014E15E
		[NotNull]
		public static string GetCategoryDisplayNameDefault(in Entry entry, [NotNull] UserProfile viewerProfile)
		{
			CategoryDef categoryDef = entry.category;
			return Language.GetString(((categoryDef != null) ? categoryDef.nameToken : null) ?? string.Empty);
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0014FF81 File Offset: 0x0014E181
		[NotNull]
		public string GetCategoryDisplayName([NotNull] UserProfile viewerProfile)
		{
			return this.getCategoryDisplayNameImplementation(this, viewerProfile);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x0014FF91 File Offset: 0x0014E191
		// (set) Token: 0x0600515B RID: 20827 RVA: 0x0014FF99 File Offset: 0x0014E199
		[NotNull]
		public Entry.IsWIPDelegate isWIPImplementation { private get; set; } = new Entry.IsWIPDelegate(Entry.IsWIPReturnFalse);

		// Token: 0x0600515C RID: 20828 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private static bool IsWIPReturnFalse(in Entry entry)
		{
			return false;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600515D RID: 20829 RVA: 0x0014FFA2 File Offset: 0x0014E1A2
		public bool isWip
		{
			get
			{
				return this.isWIPImplementation(this);
			}
		}

		// Token: 0x04004DBB RID: 19899
		public string nameToken;

		// Token: 0x04004DBC RID: 19900
		public CategoryDef category;

		// Token: 0x04004DBD RID: 19901
		public Texture iconTexture;

		// Token: 0x04004DBE RID: 19902
		public Texture bgTexture;

		// Token: 0x04004DBF RID: 19903
		public Color color;

		// Token: 0x04004DC0 RID: 19904
		public GameObject modelPrefab;

		// Token: 0x04004DC1 RID: 19905
		public object extraData;

		// Token: 0x04004DC2 RID: 19906
		public Action<PageBuilder> pageBuilderMethod;

		// Token: 0x04004DC3 RID: 19907
		[CanBeNull]
		public ViewablesCatalog.Node viewableNode;

		// Token: 0x02000DD7 RID: 3543
		// (Invoke) Token: 0x06005160 RID: 20832
		public delegate EntryStatus GetStatusDelegate(in Entry entry, [NotNull] UserProfile viewerProfile);

		// Token: 0x02000DD8 RID: 3544
		// (Invoke) Token: 0x06005164 RID: 20836
		public delegate TooltipContent GetTooltipContentDelegate(in Entry entry, UserProfile viewerProfile, EntryStatus entryStatus);

		// Token: 0x02000DD9 RID: 3545
		// (Invoke) Token: 0x06005168 RID: 20840
		[NotNull]
		public delegate string GetDisplayNameDelegate(in Entry entry, [NotNull] UserProfile viewerProfile);

		// Token: 0x02000DDA RID: 3546
		// (Invoke) Token: 0x0600516C RID: 20844
		public delegate bool IsWIPDelegate(in Entry entry);
	}
}
