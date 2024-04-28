using System;
using RoR2;
using UnityEngine;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000415 RID: 1045
	public class PlatingBuffMainState : BaseMainState
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00053EBC File Offset: 0x000520BC
		private static BuffIndex buff
		{
			get
			{
				return JunkContent.Buffs.BodyArmor.buffIndex;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldShowEnergy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00053EC8 File Offset: 0x000520C8
		protected override string GetContextString(Interactor activator)
		{
			return Language.GetString("CAPTAIN_SUPPLY_DEFENSE_RESTOCK_INTERACTION");
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00053ED4 File Offset: 0x000520D4
		protected override Interactability GetInteractability(Interactor activator)
		{
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (!component)
			{
				return Interactability.Disabled;
			}
			if (this.energyComponent.energy < this.activationCost)
			{
				return Interactability.ConditionsNotMet;
			}
			if (component.GetBuffCount(PlatingBuffMainState.buff) >= PlatingBuffMainState.maximumBuffStack)
			{
				return Interactability.ConditionsNotMet;
			}
			return Interactability.Available;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00053F1C File Offset: 0x0005211C
		protected override void OnInteractionBegin(Interactor activator)
		{
			CharacterBody component = activator.GetComponent<CharacterBody>();
			int num = 0;
			while (num < PlatingBuffMainState.buffCountToGrant && component.GetBuffCount(PlatingBuffMainState.buff) < PlatingBuffMainState.maximumBuffStack && this.energyComponent.TakeEnergy(this.activationCost))
			{
				component.AddBuff(PlatingBuffMainState.buff);
				num++;
			}
		}

		// Token: 0x04001835 RID: 6197
		public static int maximumBuffStack;

		// Token: 0x04001836 RID: 6198
		public static int buffCountToGrant;

		// Token: 0x04001837 RID: 6199
		[SerializeField]
		public float activationCost;
	}
}
