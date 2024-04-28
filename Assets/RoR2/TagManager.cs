using System;

namespace RoR2
{
	// Token: 0x020009C6 RID: 2502
	internal class TagManager
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000EF558 File Offset: 0x000ED758
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000EF560 File Offset: 0x000ED760
		public string tagsString { get; protected set; } = string.Empty;

		// Token: 0x040038E3 RID: 14563
		public Action<string> onTagsStringUpdated;
	}
}
