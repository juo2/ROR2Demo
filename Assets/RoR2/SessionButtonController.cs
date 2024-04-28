using System;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000064 RID: 100
public class SessionButtonController : MonoBehaviour
{
	// Token: 0x0600019E RID: 414 RVA: 0x00008683 File Offset: 0x00006883
	public void AddListener(UnityAction call)
	{
		this.Button.onClick.AddListener(call);
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00008696 File Offset: 0x00006896
	public void SetText(int currentParticipationNumber, int maxParticipationNumber, string hostName)
	{
		this.Text.text = string.Concat(new object[]
		{
			currentParticipationNumber,
			"/",
			maxParticipationNumber,
			" ",
			hostName
		});
	}

	// Token: 0x040001B8 RID: 440
	public HGButton Button;

	// Token: 0x040001B9 RID: 441
	public HGTextMeshProUGUI Text;
}
