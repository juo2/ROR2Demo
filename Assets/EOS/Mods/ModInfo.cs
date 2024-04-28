using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C9 RID: 713
	public class ModInfo : ISettable
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x000134D3 File Offset: 0x000116D3
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x000134DB File Offset: 0x000116DB
		public ModIdentifier[] Mods { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x000134E4 File Offset: 0x000116E4
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x000134EC File Offset: 0x000116EC
		public ModEnumerationType Type { get; set; }

		// Token: 0x0600121C RID: 4636 RVA: 0x000134F8 File Offset: 0x000116F8
		internal void Set(ModInfoInternal? other)
		{
			if (other != null)
			{
				this.Mods = other.Value.Mods;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00013538 File Offset: 0x00011738
		public void Set(object other)
		{
			this.Set(other as ModInfoInternal?);
		}
	}
}
