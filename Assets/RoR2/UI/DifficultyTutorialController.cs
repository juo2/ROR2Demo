using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CF4 RID: 3316
	public class DifficultyTutorialController : MonoBehaviour
	{
		// Token: 0x06004B89 RID: 19337 RVA: 0x001367B7 File Offset: 0x001349B7
		private void Awake()
		{
			this.hud = base.GetComponentInParent<HUD>();
			if (this.difficultyTutorialObject)
			{
				this.difficultyTutorialObject.SetActive(false);
			}
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x001367E0 File Offset: 0x001349E0
		private void Update()
		{
			if (this.hud)
			{
				UserProfile userProfile = this.hud.localUserViewer.userProfile;
				CharacterBody cachedBody = this.hud.localUserViewer.cachedBody;
				if (userProfile != null && cachedBody && this.difficultyTutorialObject && userProfile.tutorialDifficulty.shouldShow && Run.instance && Run.instance.fixedTime >= this.difficultyTutorialTriggerTime)
				{
					this.difficultyTutorialObject.SetActive(true);
					userProfile.tutorialDifficulty.shouldShow = false;
					UserProfile userProfile2 = userProfile;
					userProfile2.tutorialDifficulty.showCount = userProfile2.tutorialDifficulty.showCount + 1U;
				}
			}
		}

		// Token: 0x04004843 RID: 18499
		private HUD hud;

		// Token: 0x04004844 RID: 18500
		[Tooltip("The tutorial popup object.")]
		public GameObject difficultyTutorialObject;

		// Token: 0x04004845 RID: 18501
		[Tooltip("The time at which to trigger the tutorial popup.")]
		public float difficultyTutorialTriggerTime = 60f;
	}
}
