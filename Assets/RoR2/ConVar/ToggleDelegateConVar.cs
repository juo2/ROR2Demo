using System;

namespace RoR2.ConVar
{
	// Token: 0x02000E43 RID: 3651
	public sealed class ToggleDelegateConVar : ToggleVirtualConVar
	{
		// Token: 0x060053CD RID: 21453 RVA: 0x00159C1C File Offset: 0x00157E1C
		public ToggleDelegateConVar(string name, ConVarFlags flags, string defaultValue, string helpText, Action onEnable, Action onDisable) : base(name, flags, defaultValue, helpText)
		{
			this.onEnable = onEnable;
			this.onDisable = onDisable;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00159C39 File Offset: 0x00157E39
		protected override void OnEnable()
		{
			Action action = this.onEnable;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00159C4B File Offset: 0x00157E4B
		protected override void OnDisable()
		{
			Action action = this.onDisable;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x04004FAE RID: 20398
		private readonly Action onEnable;

		// Token: 0x04004FAF RID: 20399
		private readonly Action onDisable;
	}
}
