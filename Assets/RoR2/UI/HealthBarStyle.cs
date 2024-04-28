using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CA1 RID: 3233
	[CreateAssetMenu(menuName = "RoR2/UI/HealthBarStyle")]
	public class HealthBarStyle : ScriptableObject
	{
		// Token: 0x04004673 RID: 18035
		public GameObject barPrefab;

		// Token: 0x04004674 RID: 18036
		public bool flashOnHealthCritical;

		// Token: 0x04004675 RID: 18037
		[FormerlySerializedAs("trailingBarStyle")]
		public HealthBarStyle.BarStyle trailingUnderHealthBarStyle;

		// Token: 0x04004676 RID: 18038
		[FormerlySerializedAs("healthBarStyle")]
		public HealthBarStyle.BarStyle instantHealthBarStyle;

		// Token: 0x04004677 RID: 18039
		public HealthBarStyle.BarStyle trailingOverHealthBarStyle;

		// Token: 0x04004678 RID: 18040
		public HealthBarStyle.BarStyle shieldBarStyle;

		// Token: 0x04004679 RID: 18041
		public HealthBarStyle.BarStyle curseBarStyle;

		// Token: 0x0400467A RID: 18042
		public HealthBarStyle.BarStyle barrierBarStyle;

		// Token: 0x0400467B RID: 18043
		public HealthBarStyle.BarStyle flashBarStyle;

		// Token: 0x0400467C RID: 18044
		public HealthBarStyle.BarStyle cullBarStyle;

		// Token: 0x0400467D RID: 18045
		public HealthBarStyle.BarStyle magneticStyle;

		// Token: 0x0400467E RID: 18046
		public HealthBarStyle.BarStyle ospStyle;

		// Token: 0x0400467F RID: 18047
		public HealthBarStyle.BarStyle lowHealthOverStyle;

		// Token: 0x04004680 RID: 18048
		public HealthBarStyle.BarStyle lowHealthUnderStyle;

		// Token: 0x02000CA2 RID: 3234
		[Serializable]
		public struct BarStyle
		{
			// Token: 0x04004681 RID: 18049
			public bool enabled;

			// Token: 0x04004682 RID: 18050
			public Color baseColor;

			// Token: 0x04004683 RID: 18051
			public Sprite sprite;

			// Token: 0x04004684 RID: 18052
			public Image.Type imageType;

			// Token: 0x04004685 RID: 18053
			public float sizeDelta;
		}
	}
}
