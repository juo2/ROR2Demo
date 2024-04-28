using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D0E RID: 3342
	public class HUDScaleController : MonoBehaviour
	{
		// Token: 0x06004C30 RID: 19504 RVA: 0x0013A1A6 File Offset: 0x001383A6
		public void OnEnable()
		{
			HUDScaleController.instancesList.Add(this);
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0013A1B3 File Offset: 0x001383B3
		public void OnDisable()
		{
			HUDScaleController.instancesList.Remove(this);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0013A1C1 File Offset: 0x001383C1
		private void Start()
		{
			this.SetScale();
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0013A1CC File Offset: 0x001383CC
		private void SetScale()
		{
			BaseConVar baseConVar = Console.instance.FindConVar("hud_scale");
			float num;
			if (baseConVar != null && TextSerialization.TryParseInvariant(baseConVar.GetString(), out num))
			{
				Vector3 localScale = new Vector3(num / 100f, num / 100f, num / 100f);
				RectTransform[] array = this.rectTransforms;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].localScale = localScale;
				}
			}
		}

		// Token: 0x0400491A RID: 18714
		public RectTransform[] rectTransforms;

		// Token: 0x0400491B RID: 18715
		private static List<HUDScaleController> instancesList = new List<HUDScaleController>();

		// Token: 0x02000D0F RID: 3343
		private class HUDScaleConVar : BaseConVar
		{
			// Token: 0x06004C36 RID: 19510 RVA: 0x00009F73 File Offset: 0x00008173
			private HUDScaleConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06004C37 RID: 19511 RVA: 0x0013A248 File Offset: 0x00138448
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num) && num != 0)
				{
					this.intValue = num;
					foreach (HUDScaleController hudscaleController in HUDScaleController.instancesList)
					{
						hudscaleController.SetScale();
					}
				}
			}

			// Token: 0x06004C38 RID: 19512 RVA: 0x0013A2AC File Offset: 0x001384AC
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(this.intValue);
			}

			// Token: 0x0400491C RID: 18716
			public static HUDScaleController.HUDScaleConVar instance = new HUDScaleController.HUDScaleConVar("hud_scale", ConVarFlags.Archive, "100", "Scales the size of HUD elements in-game. Defaults to 100.");

			// Token: 0x0400491D RID: 18717
			private int intValue;
		}
	}
}
