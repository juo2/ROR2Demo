using System;
using System.Collections.Generic;

namespace RoR2.ConVar
{
	// Token: 0x02000E42 RID: 3650
	public abstract class ToggleVirtualConVar : BaseConVar
	{
		// Token: 0x060053C3 RID: 21443 RVA: 0x00159B51 File Offset: 0x00157D51
		static ToggleVirtualConVar()
		{
			RoR2Application.onShutDown = (Action)Delegate.Combine(RoR2Application.onShutDown, new Action(ToggleVirtualConVar.OnApplicationShutDown));
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00009F73 File Offset: 0x00008173
		public ToggleVirtualConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060053C5 RID: 21445 RVA: 0x00159B7D File Offset: 0x00157D7D
		// (set) Token: 0x060053C6 RID: 21446 RVA: 0x00159B85 File Offset: 0x00157D85
		public bool enabled
		{
			get
			{
				return this._enabled;
			}
			protected set
			{
				if (this._enabled == value)
				{
					return;
				}
				this._enabled = value;
				if (this._enabled)
				{
					ToggleVirtualConVar.enabledInstances.Add(this);
					this.OnEnable();
					return;
				}
				ToggleVirtualConVar.enabledInstances.Remove(this);
				this.OnDisable();
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x00159BC4 File Offset: 0x00157DC4
		public bool value
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x060053C8 RID: 21448
		protected abstract void OnEnable();

		// Token: 0x060053C9 RID: 21449
		protected abstract void OnDisable();

		// Token: 0x060053CA RID: 21450 RVA: 0x00159BCC File Offset: 0x00157DCC
		public override void SetString(string newValue)
		{
			this.enabled = BaseConVar.ParseBoolInvariant(newValue);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x00159BDA File Offset: 0x00157DDA
		public override string GetString()
		{
			if (!this.enabled)
			{
				return "0";
			}
			return "1";
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00159BEF File Offset: 0x00157DEF
		private static void OnApplicationShutDown()
		{
			while (ToggleVirtualConVar.enabledInstances.Count > 0)
			{
				ToggleVirtualConVar.enabledInstances[ToggleVirtualConVar.enabledInstances.Count - 1].enabled = false;
			}
		}

		// Token: 0x04004FAC RID: 20396
		private bool _enabled;

		// Token: 0x04004FAD RID: 20397
		private static readonly List<ToggleVirtualConVar> enabledInstances = new List<ToggleVirtualConVar>();
	}
}
