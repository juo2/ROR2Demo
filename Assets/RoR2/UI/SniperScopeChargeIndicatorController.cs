using System;
using EntityStates.Sniper.Scope;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D8C RID: 3468
	[RequireComponent(typeof(HudElement))]
	public class SniperScopeChargeIndicatorController : MonoBehaviour
	{
		// Token: 0x06004F6D RID: 20333 RVA: 0x00148A8D File Offset: 0x00146C8D
		private void Awake()
		{
			this.hudElement = base.GetComponent<HudElement>();
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00148A9C File Offset: 0x00146C9C
		private void FixedUpdate()
		{
			float fillAmount = 0f;
			if (this.hudElement.targetCharacterBody)
			{
				SkillLocator component = this.hudElement.targetCharacterBody.GetComponent<SkillLocator>();
				if (component && component.secondary)
				{
					EntityStateMachine stateMachine = component.secondary.stateMachine;
					if (stateMachine)
					{
						ScopeSniper scopeSniper = stateMachine.state as ScopeSniper;
						if (scopeSniper != null)
						{
							fillAmount = scopeSniper.charge;
						}
					}
				}
			}
			if (this.image)
			{
				this.image.fillAmount = fillAmount;
			}
		}

		// Token: 0x04004C1D RID: 19485
		private GameObject sourceGameObject;

		// Token: 0x04004C1E RID: 19486
		private HudElement hudElement;

		// Token: 0x04004C1F RID: 19487
		public Image image;
	}
}
