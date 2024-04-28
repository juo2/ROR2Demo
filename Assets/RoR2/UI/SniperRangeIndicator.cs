using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D8B RID: 3467
	[RequireComponent(typeof(HudElement))]
	public class SniperRangeIndicator : MonoBehaviour
	{
		// Token: 0x06004F6A RID: 20330 RVA: 0x00148993 File Offset: 0x00146B93
		private void Awake()
		{
			this.hudElement = base.GetComponent<HudElement>();
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x001489A4 File Offset: 0x00146BA4
		private void FixedUpdate()
		{
			float num = float.PositiveInfinity;
			if (this.hudElement.targetCharacterBody)
			{
				InputBankTest component = this.hudElement.targetCharacterBody.GetComponent<InputBankTest>();
				if (component)
				{
					Ray ray = new Ray(component.aimOrigin, component.aimDirection);
					RaycastHit raycastHit = default(RaycastHit);
					if (Util.CharacterRaycast(this.hudElement.targetCharacterBody.gameObject, ray, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
					{
						num = raycastHit.distance;
					}
				}
			}
			this.label.text = "Dis: " + ((num > 999f) ? "999m" : string.Format("{0:D3}m", Mathf.FloorToInt(num)));
		}

		// Token: 0x04004C1B RID: 19483
		public TextMeshProUGUI label;

		// Token: 0x04004C1C RID: 19484
		private HudElement hudElement;
	}
}
