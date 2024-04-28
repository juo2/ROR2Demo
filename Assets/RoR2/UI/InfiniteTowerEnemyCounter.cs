using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D1E RID: 3358
	public class InfiniteTowerEnemyCounter : MonoBehaviour
	{
		// Token: 0x06004C81 RID: 19585 RVA: 0x0013BE14 File Offset: 0x0013A014
		private void OnEnable()
		{
			InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
			if (infiniteTowerRun)
			{
				this.waveController = infiniteTowerRun.waveController;
				if (this.waveController)
				{
					this.combatSquad = this.waveController.GetComponent<CombatSquad>();
					if (this.combatSquad)
					{
						this.rootObject.SetActive(this.waveController.HasFullProgress() && this.combatSquad.memberCount > 0);
						return;
					}
					this.rootObject.SetActive(false);
					return;
				}
				else
				{
					this.rootObject.SetActive(false);
				}
			}
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0013BEB0 File Offset: 0x0013A0B0
		private void Update()
		{
			if (this.combatSquad)
			{
				this.rootObject.SetActive(this.waveController.HasFullProgress() && this.combatSquad.memberCount > 0);
				if (this.counterText)
				{
					string text = this.combatSquad.memberCount.ToString();
					this.counterText.text = Language.GetStringFormatted(this.token, new object[]
					{
						text
					});
				}
			}
		}

		// Token: 0x04004985 RID: 18821
		[Tooltip("The root we're toggling")]
		[SerializeField]
		private GameObject rootObject;

		// Token: 0x04004986 RID: 18822
		[Tooltip("The text we're setting")]
		[SerializeField]
		private TextMeshProUGUI counterText;

		// Token: 0x04004987 RID: 18823
		[SerializeField]
		[Tooltip("The language token for the text field")]
		private string token;

		// Token: 0x04004988 RID: 18824
		private InfiniteTowerWaveController waveController;

		// Token: 0x04004989 RID: 18825
		private CombatSquad combatSquad;
	}
}
