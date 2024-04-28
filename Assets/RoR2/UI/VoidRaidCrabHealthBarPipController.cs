using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DB0 RID: 3504
	public class VoidRaidCrabHealthBarPipController : MonoBehaviour
	{
		// Token: 0x06005046 RID: 20550 RVA: 0x0014C230 File Offset: 0x0014A430
		public void InitializePips(PhasedInventorySetter phasedInventory)
		{
			int numPhases = phasedInventory.GetNumPhases();
			for (int i = 0; i < numPhases; i++)
			{
				float num = 0.01f * (float)phasedInventory.GetItemCountForPhase(i, this.minHealthPercentageDef);
				if (num > 0f)
				{
					RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.pipPrefab, base.transform).GetComponent<RectTransform>();
					component.anchorMin = new Vector2(num, component.anchorMin.y);
					component.anchorMax = new Vector2(num, component.anchorMax.y);
				}
			}
		}

		// Token: 0x04004CED RID: 19693
		[SerializeField]
		private GameObject pipPrefab;

		// Token: 0x04004CEE RID: 19694
		[SerializeField]
		private ItemDef minHealthPercentageDef;
	}
}
