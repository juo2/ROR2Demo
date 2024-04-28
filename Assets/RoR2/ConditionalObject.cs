using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[DefaultExecutionOrder(-5)]
public class ConditionalObject : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x000037B3 File Offset: 0x000019B3
	private void CheckConditions()
	{
		base.gameObject.SetActive(this.enabledOnSteam);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000037C6 File Offset: 0x000019C6
	private void Awake()
	{
		if (!this.disableInProduction)
		{
			this.CheckConditions();
		}
	}

	// Token: 0x0400007E RID: 126
	public bool enabledOnSwitch = true;

	// Token: 0x0400007F RID: 127
	public bool enabledOnXbox = true;

	// Token: 0x04000080 RID: 128
	public bool enabledOnPS4 = true;

	// Token: 0x04000081 RID: 129
	public bool enabledOnSteam = true;

	// Token: 0x04000082 RID: 130
	public bool enabledOnEGS = true;

	// Token: 0x04000083 RID: 131
	public bool disableInProduction;

	// Token: 0x04000084 RID: 132
	public bool disableIfNoActiveRun;
}
