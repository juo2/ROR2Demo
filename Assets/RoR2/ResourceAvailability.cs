using System;
using JetBrains.Annotations;

// Token: 0x02000088 RID: 136
public struct ResourceAvailability
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A3AB File Offset: 0x000085AB
	// (set) Token: 0x0600023D RID: 573 RVA: 0x0000A3B3 File Offset: 0x000085B3
	public bool available { get; private set; }

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x0600023E RID: 574 RVA: 0x0000A3BC File Offset: 0x000085BC
	// (remove) Token: 0x0600023F RID: 575 RVA: 0x0000A3F4 File Offset: 0x000085F4
	private event Action onAvailable;

	// Token: 0x06000240 RID: 576 RVA: 0x0000A429 File Offset: 0x00008629
	public void MakeAvailable()
	{
		if (this.available)
		{
			return;
		}
		this.available = true;
		Action action = this.onAvailable;
		if (action != null)
		{
			action();
		}
		this.onAvailable = null;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000A453 File Offset: 0x00008653
	public void CallWhenAvailable([NotNull] Action callback)
	{
		if (this.available)
		{
			callback();
			return;
		}
		this.onAvailable += callback;
	}
}
