using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200082F RID: 2095
	public class ProxyInteraction : MonoBehaviour, IInteractable
	{
		// Token: 0x06002D95 RID: 11669 RVA: 0x000C24F8 File Offset: 0x000C06F8
		public string GetContextString(Interactor activator)
		{
			Func<ProxyInteraction, Interactor, string> func = this.getContextString;
			if (func == null)
			{
				return null;
			}
			return func(this, activator);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000C250D File Offset: 0x000C070D
		public Interactability GetInteractability(Interactor activator)
		{
			Func<ProxyInteraction, Interactor, Interactability> func = this.getInteractability;
			if (func == null)
			{
				return Interactability.Disabled;
			}
			return func(this, activator);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000C2522 File Offset: 0x000C0722
		public void OnInteractionBegin(Interactor activator)
		{
			Action<ProxyInteraction, Interactor> action = this.onInteractionBegin;
			if (action == null)
			{
				return;
			}
			action(this, activator);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000C2536 File Offset: 0x000C0736
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			Func<ProxyInteraction, Interactor, bool> func = this.shouldIgnoreSpherecastForInteractability;
			return func == null || func(this, activator);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000C254B File Offset: 0x000C074B
		public bool ShouldShowOnScanner()
		{
			Func<ProxyInteraction, bool> func = this.shouldShowOnScanner;
			return func != null && func(this);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000C255F File Offset: 0x000C075F
		private void OnEnable()
		{
			InstanceTracker.Add<ProxyInteraction>(this);
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000C2567 File Offset: 0x000C0767
		private void OnDisable()
		{
			InstanceTracker.Remove<ProxyInteraction>(this);
		}

		// Token: 0x04002F9B RID: 12187
		public Func<ProxyInteraction, Interactor, string> getContextString;

		// Token: 0x04002F9C RID: 12188
		public Func<ProxyInteraction, Interactor, Interactability> getInteractability;

		// Token: 0x04002F9D RID: 12189
		public Action<ProxyInteraction, Interactor> onInteractionBegin;

		// Token: 0x04002F9E RID: 12190
		public Func<ProxyInteraction, Interactor, bool> shouldIgnoreSpherecastForInteractability;

		// Token: 0x04002F9F RID: 12191
		public Func<ProxyInteraction, bool> shouldShowOnScanner;
	}
}
