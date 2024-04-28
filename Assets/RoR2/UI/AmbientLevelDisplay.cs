using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CB9 RID: 3257
	public class AmbientLevelDisplay : MonoBehaviour
	{
		// Token: 0x06004A4A RID: 19018 RVA: 0x00130FF0 File Offset: 0x0012F1F0
		private void Update()
		{
			string text = "-";
			if (Run.instance)
			{
				text = Run.instance.ambientLevelFloor.ToString();
			}
			this.text.text = Language.GetStringFormatted("AMBIENT_LEVEL_DISPLAY_FORMAT", new object[]
			{
				text
			});
		}

		// Token: 0x040046FA RID: 18170
		public TextMeshProUGUI text;
	}
}
