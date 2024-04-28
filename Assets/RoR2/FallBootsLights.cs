using System;
using EntityStates.Headstompers;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006D7 RID: 1751
	public class FallBootsLights : MonoBehaviour
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x00094DB2 File Offset: 0x00092FB2
		private void Start()
		{
			this.characterModel = base.GetComponentInParent<CharacterModel>();
			this.FindSourceStateMachine();
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00094DC8 File Offset: 0x00092FC8
		private void FindSourceStateMachine()
		{
			if (!this.characterModel || !this.characterModel.body)
			{
				return;
			}
			BaseHeadstompersState baseHeadstompersState = BaseHeadstompersState.FindForBody(this.characterModel.body);
			this.sourceStateMachine = ((baseHeadstompersState != null) ? baseHeadstompersState.outer : null);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00094E18 File Offset: 0x00093018
		private void Update()
		{
			if (!this.sourceStateMachine)
			{
				this.FindSourceStateMachine();
			}
			bool flag = this.sourceStateMachine && !(this.sourceStateMachine.state is HeadstompersCooldown);
			if (flag != this.isReady)
			{
				if (flag)
				{
					this.readyEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.readyEffect, base.transform.position, base.transform.rotation, base.transform);
					Util.PlaySound("Play_item_proc_fallboots_activate", base.gameObject);
				}
				else if (this.readyEffectInstance)
				{
					UnityEngine.Object.Destroy(this.readyEffectInstance);
				}
				this.isReady = flag;
			}
			bool flag2 = this.sourceStateMachine && this.sourceStateMachine.state is HeadstompersFall;
			if (flag2 != this.isTriggered)
			{
				if (flag2)
				{
					this.triggerEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.triggerEffect, base.transform.position, base.transform.rotation, base.transform);
					Util.PlaySound("Play_item_proc_fallboots_activate", base.gameObject);
				}
				else if (this.triggerEffectInstance)
				{
					UnityEngine.Object.Destroy(this.triggerEffectInstance);
				}
				this.isTriggered = flag2;
			}
			bool flag3 = this.sourceStateMachine && this.sourceStateMachine.state is HeadstompersCharge;
			if (flag3 != this.isCharging)
			{
				if (flag3)
				{
					this.chargingEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargingEffect, base.transform.position, base.transform.rotation, base.transform);
				}
				else if (this.chargingEffectInstance)
				{
					UnityEngine.Object.Destroy(this.chargingEffectInstance);
				}
				this.isCharging = flag3;
			}
		}

		// Token: 0x04002782 RID: 10114
		public GameObject readyEffect;

		// Token: 0x04002783 RID: 10115
		public GameObject triggerEffect;

		// Token: 0x04002784 RID: 10116
		public GameObject chargingEffect;

		// Token: 0x04002785 RID: 10117
		private GameObject readyEffectInstance;

		// Token: 0x04002786 RID: 10118
		private GameObject triggerEffectInstance;

		// Token: 0x04002787 RID: 10119
		private GameObject chargingEffectInstance;

		// Token: 0x04002788 RID: 10120
		private bool isReady;

		// Token: 0x04002789 RID: 10121
		private bool isTriggered;

		// Token: 0x0400278A RID: 10122
		private bool isCharging;

		// Token: 0x0400278B RID: 10123
		private CharacterModel characterModel;

		// Token: 0x0400278C RID: 10124
		private EntityStateMachine sourceStateMachine;
	}
}
