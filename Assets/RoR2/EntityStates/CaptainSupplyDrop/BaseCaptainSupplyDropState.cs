using System;
using RoR2;
using UnityEngine;
using UnityEngine.UI;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x0200040E RID: 1038
	public class BaseCaptainSupplyDropState : BaseState
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool shouldShowModel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldShowEnergy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual string GetContextString(Interactor activator)
		{
			return null;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual Interactability GetInteractability(Interactor activator)
		{
			return Interactability.Disabled;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnInteractionBegin(Interactor activator)
		{
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool ShouldShowOnScanner()
		{
			return false;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool ShouldIgnoreSpherecastForInteractability(Interactor activator)
		{
			return false;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00053B1D File Offset: 0x00051D1D
		private string GetContextStringInternal(ProxyInteraction proxyInteraction, Interactor activator)
		{
			return this.GetContextString(activator);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00053B26 File Offset: 0x00051D26
		private Interactability GetInteractabilityInternal(ProxyInteraction proxyInteraction, Interactor activator)
		{
			return this.GetInteractability(activator);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00053B2F File Offset: 0x00051D2F
		private void OnInteractionBeginInternal(ProxyInteraction proxyInteraction, Interactor activator)
		{
			this.OnInteractionBegin(activator);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00053B38 File Offset: 0x00051D38
		private bool ShouldIgnoreSpherecastForInteractabilityInternal(ProxyInteraction proxyInteraction, Interactor activator)
		{
			return this.ShouldIgnoreSpherecastForInteractability(activator);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00053B41 File Offset: 0x00051D41
		private bool ShouldShowOnScannerInternal(ProxyInteraction proxyInteraction)
		{
			return this.ShouldShowOnScanner();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00053B4C File Offset: 0x00051D4C
		public override void OnEnter()
		{
			base.OnEnter();
			this.energyComponent = base.GetComponent<GenericEnergyComponent>();
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.interactionComponent = base.GetComponent<ProxyInteraction>();
			this.interactionComponent.getContextString = new Func<ProxyInteraction, Interactor, string>(this.GetContextStringInternal);
			this.interactionComponent.getInteractability = new Func<ProxyInteraction, Interactor, Interactability>(this.GetInteractabilityInternal);
			this.interactionComponent.onInteractionBegin = new Action<ProxyInteraction, Interactor>(this.OnInteractionBeginInternal);
			this.interactionComponent.shouldShowOnScanner = new Func<ProxyInteraction, bool>(this.ShouldShowOnScannerInternal);
			this.interactionComponent.shouldIgnoreSpherecastForInteractability = new Func<ProxyInteraction, Interactor, bool>(this.ShouldIgnoreSpherecastForInteractabilityInternal);
			base.GetModelTransform().gameObject.SetActive(this.shouldShowModel);
			this.energyIndicatorContainer = base.FindModelChild("EnergyIndicatorContainer").gameObject;
			this.energyIndicator = base.FindModelChild("EnergyIndicator").GetComponent<Image>();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00053C38 File Offset: 0x00051E38
		public override void OnExit()
		{
			this.interactionComponent.getContextString = null;
			this.interactionComponent.getInteractability = null;
			this.interactionComponent.onInteractionBegin = null;
			this.interactionComponent.shouldShowOnScanner = null;
			this.interactionComponent.shouldIgnoreSpherecastForInteractability = null;
			base.OnExit();
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00053C87 File Offset: 0x00051E87
		public override void Update()
		{
			base.Update();
			this.UpdateEnergyIndicator();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00053C95 File Offset: 0x00051E95
		private void UpdateEnergyIndicator()
		{
			this.energyIndicatorContainer.SetActive(this.shouldShowEnergy);
			this.energyIndicator.fillAmount = this.energyComponent.normalizedEnergy;
		}

		// Token: 0x04001827 RID: 6183
		private ProxyInteraction interactionComponent;

		// Token: 0x04001828 RID: 6184
		protected GenericEnergyComponent energyComponent;

		// Token: 0x04001829 RID: 6185
		protected TeamFilter teamFilter;

		// Token: 0x0400182A RID: 6186
		private Image energyIndicator;

		// Token: 0x0400182B RID: 6187
		private GameObject energyIndicatorContainer;
	}
}
