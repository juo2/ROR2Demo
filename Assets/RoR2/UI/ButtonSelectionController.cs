using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CC3 RID: 3267
	public class ButtonSelectionController : MonoBehaviour
	{
		// Token: 0x06004A7C RID: 19068 RVA: 0x00131A98 File Offset: 0x0012FC98
		public void SelectThisButton(MPButton selectedButton)
		{
			for (int i = 0; i < this.buttons.Length; i++)
			{
				this.buttons[i] == selectedButton;
			}
		}

		// Token: 0x0400472E RID: 18222
		public MPButton[] buttons;
	}
}
