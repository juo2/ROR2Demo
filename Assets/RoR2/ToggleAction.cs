using System;

// Token: 0x02000093 RID: 147
public class ToggleAction : IDisposable
{
	// Token: 0x06000299 RID: 665 RVA: 0x0000ABF3 File Offset: 0x00008DF3
	public ToggleAction(Action activationAction, Action deactivationAction)
	{
		this.active = false;
		this.activationAction = activationAction;
		this.deactivationAction = deactivationAction;
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600029A RID: 666 RVA: 0x0000AC10 File Offset: 0x00008E10
	// (set) Token: 0x0600029B RID: 667 RVA: 0x0000AC18 File Offset: 0x00008E18
	public bool active { get; private set; }

	// Token: 0x0600029C RID: 668 RVA: 0x0000AC21 File Offset: 0x00008E21
	public void SetActive(bool newActive)
	{
		if (this.active == newActive)
		{
			return;
		}
		this.active = newActive;
		if (this.active)
		{
			Action action = this.activationAction;
			if (action == null)
			{
				return;
			}
			action();
			return;
		}
		else
		{
			Action action2 = this.deactivationAction;
			if (action2 == null)
			{
				return;
			}
			action2();
			return;
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000AC5D File Offset: 0x00008E5D
	public void Dispose()
	{
		this.SetActive(false);
	}

	// Token: 0x04000243 RID: 579
	private readonly Action activationAction;

	// Token: 0x04000244 RID: 580
	private readonly Action deactivationAction;
}
