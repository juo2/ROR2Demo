using System;
using RoR2;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000061 RID: 97
public class EclipseDifficultyMedalDisplay : MonoBehaviour
{
	// Token: 0x06000197 RID: 407 RVA: 0x00008557 File Offset: 0x00006757
	private void OnEnable()
	{
		UserProfile.onSurvivorPreferenceChangedGlobal += this.OnSurvivorPreferenceChangedGlobal;
		this.Refresh();
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00008570 File Offset: 0x00006770
	private void OnDisable()
	{
		UserProfile.onSurvivorPreferenceChangedGlobal -= this.OnSurvivorPreferenceChangedGlobal;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00008583 File Offset: 0x00006783
	private void OnSurvivorPreferenceChangedGlobal(UserProfile userProfile)
	{
		this.Refresh();
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000858C File Offset: 0x0000678C
	private void Refresh()
	{
		LocalUser firstLocalUser = LocalUserManager.GetFirstLocalUser();
		SurvivorDef survivorDef = (firstLocalUser != null) ? firstLocalUser.userProfile.GetSurvivorPreference() : null;
		if (survivorDef)
		{
			int localUserSurvivorCompletedEclipseLevel = EclipseRun.GetLocalUserSurvivorCompletedEclipseLevel(firstLocalUser, survivorDef);
			if (this.eclipseLevel <= localUserSurvivorCompletedEclipseLevel)
			{
				bool flag = true;
				foreach (SurvivorDef survivorDef2 in SurvivorCatalog.orderedSurvivorDefs)
				{
					if (this.ShouldDisplaySurvivor(survivorDef2, firstLocalUser))
					{
						localUserSurvivorCompletedEclipseLevel = EclipseRun.GetLocalUserSurvivorCompletedEclipseLevel(firstLocalUser, survivorDef2);
						if (localUserSurvivorCompletedEclipseLevel < this.eclipseLevel)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					this.iconImage.sprite = this.completeSprite;
					return;
				}
				this.iconImage.sprite = this.incompleteSprite;
				return;
			}
			else
			{
				this.iconImage.sprite = this.unearnedSprite;
			}
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00008668 File Offset: 0x00006868
	private bool ShouldDisplaySurvivor(SurvivorDef survivorDef, LocalUser localUser)
	{
		return survivorDef && !survivorDef.hidden && survivorDef.CheckUserHasRequiredEntitlement(localUser);
	}

	// Token: 0x040001AE RID: 430
	[SerializeField]
	private int eclipseLevel;

	// Token: 0x040001AF RID: 431
	[SerializeField]
	private Image iconImage;

	// Token: 0x040001B0 RID: 432
	[SerializeField]
	private Sprite unearnedSprite;

	// Token: 0x040001B1 RID: 433
	[SerializeField]
	private Sprite incompleteSprite;

	// Token: 0x040001B2 RID: 434
	[SerializeField]
	private Sprite completeSprite;
}
