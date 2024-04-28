using System;
using RoR2;
using UnityEngine;

namespace EntityStates.QuestVolatileBattery
{
	// Token: 0x0200021E RID: 542
	public class QuestVolatileBatteryBaseState : BaseState
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0002728E File Offset: 0x0002548E
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00027296 File Offset: 0x00025496
		private protected NetworkedBodyAttachment networkedBodyAttachment { protected get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0002729F File Offset: 0x0002549F
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x000272A7 File Offset: 0x000254A7
		private protected HealthComponent attachedHealthComponent { protected get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x000272B0 File Offset: 0x000254B0
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x000272B8 File Offset: 0x000254B8
		private protected CharacterModel attachedCharacterModel { protected get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x000272C1 File Offset: 0x000254C1
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x000272C9 File Offset: 0x000254C9
		private protected Transform[] displays { protected get; private set; } = Array.Empty<Transform>();

		// Token: 0x0600098A RID: 2442 RVA: 0x000272D4 File Offset: 0x000254D4
		public override void OnEnter()
		{
			base.OnEnter();
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			if (this.networkedBodyAttachment && this.networkedBodyAttachment.attachedBody)
			{
				this.attachedHealthComponent = this.networkedBodyAttachment.attachedBody.healthComponent;
				ModelLocator modelLocator = this.networkedBodyAttachment.attachedBody.modelLocator;
				if (modelLocator)
				{
					Transform modelTransform = modelLocator.modelTransform;
					if (modelTransform)
					{
						this.attachedCharacterModel = modelTransform.GetComponent<CharacterModel>();
					}
				}
			}
		}
	}
}
