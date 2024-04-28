using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CFE RID: 3326
	[RequireComponent(typeof(RectTransform))]
	public class ExpBar : MonoBehaviour
	{
		// Token: 0x06004BC6 RID: 19398 RVA: 0x001379F5 File Offset: 0x00135BF5
		private void Awake()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x00137A04 File Offset: 0x00135C04
		public void Update()
		{
			TeamIndex teamIndex = this.source ? this.source.teamIndex : TeamIndex.Neutral;
			float x = 0f;
			if (this.source && TeamManager.instance)
			{
				x = Mathf.InverseLerp(TeamManager.instance.GetTeamCurrentLevelExperience(teamIndex), TeamManager.instance.GetTeamNextLevelExperience(teamIndex), TeamManager.instance.GetTeamExperience(teamIndex));
			}
			if (this.fillRectTransform)
			{
				Rect rect = this.rectTransform.rect;
				Rect rect2 = this.fillRectTransform.rect;
				this.fillRectTransform.anchorMin = new Vector2(0f, 0f);
				this.fillRectTransform.anchorMax = new Vector2(x, 1f);
				this.fillRectTransform.sizeDelta = new Vector2(1f, 1f);
			}
		}

		// Token: 0x04004884 RID: 18564
		public CharacterMaster source;

		// Token: 0x04004885 RID: 18565
		public RectTransform fillRectTransform;

		// Token: 0x04004886 RID: 18566
		private RectTransform rectTransform;
	}
}
