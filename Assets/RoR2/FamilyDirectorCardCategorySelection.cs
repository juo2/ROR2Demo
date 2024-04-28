using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000535 RID: 1333
	[CreateAssetMenu(menuName = "RoR2/DCCS/FamilyDirectorCardCategorySelection")]
	public class FamilyDirectorCardCategorySelection : DirectorCardCategorySelection
	{
		// Token: 0x06001845 RID: 6213 RVA: 0x0006A648 File Offset: 0x00068848
		public override bool IsAvailable()
		{
			return Run.instance && Run.instance.canFamilyEventTrigger && this.minimumStageCompletion <= Run.instance.stageClearCount && this.maximumStageCompletion > Run.instance.stageClearCount;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0006A688 File Offset: 0x00068888
		public override void OnSelected(ClassicStageInfo stageInfo)
		{
			if (!string.IsNullOrEmpty(this.selectionChatString))
			{
				stageInfo.StartCoroutine(stageInfo.BroadcastFamilySelection(this.selectionChatString));
			}
		}

		// Token: 0x04001DE7 RID: 7655
		private const float chatMessageDelaySeconds = 1f;

		// Token: 0x04001DE8 RID: 7656
		[SerializeField]
		private string selectionChatString;

		// Token: 0x04001DE9 RID: 7657
		[SerializeField]
		[Tooltip("The minimum (inclusive) number of stages COMPLETED (not reached) before this family event becomes available.")]
		private int minimumStageCompletion = 1;

		// Token: 0x04001DEA RID: 7658
		[SerializeField]
		[Tooltip("The maximum (exclusive) number of stages COMPLETED (not reached) before this family event becomes unavailable.")]
		private int maximumStageCompletion = int.MaxValue;
	}
}
