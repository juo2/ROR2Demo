using System;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x02000759 RID: 1881
	public interface IInteractable
	{
		// Token: 0x060026EF RID: 9967
		[CanBeNull]
		string GetContextString([NotNull] Interactor activator);

		// Token: 0x060026F0 RID: 9968
		Interactability GetInteractability([NotNull] Interactor activator);

		// Token: 0x060026F1 RID: 9969
		void OnInteractionBegin([NotNull] Interactor activator);

		// Token: 0x060026F2 RID: 9970
		bool ShouldIgnoreSpherecastForInteractibility([NotNull] Interactor activator);

		// Token: 0x060026F3 RID: 9971
		bool ShouldShowOnScanner();
	}
}
