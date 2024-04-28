using System;

namespace RoR2.DirectionalSearch
{
	// Token: 0x02000CA0 RID: 3232
	public class PickupSearch : BaseDirectionalSearch<GenericPickupController, PickupSearchSelector, PickupSearchFilter>
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x0012F3C1 File Offset: 0x0012D5C1
		// (set) Token: 0x060049D7 RID: 18903 RVA: 0x0012F3CE File Offset: 0x0012D5CE
		public bool requireTransmutable
		{
			get
			{
				return this.candidateFilter.requireTransmutable;
			}
			set
			{
				this.candidateFilter.requireTransmutable = value;
			}
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x0012F3DC File Offset: 0x0012D5DC
		public PickupSearch() : base(default(PickupSearchSelector), default(PickupSearchFilter))
		{
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0012F401 File Offset: 0x0012D601
		public PickupSearch(PickupSearchSelector selector, PickupSearchFilter candidateFilter) : base(selector, candidateFilter)
		{
		}
	}
}
