using System;
using RoR2.Stats;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D22 RID: 3362
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class InfiniteTowerMenuController : MonoBehaviour
	{
		// Token: 0x06004C8E RID: 19598 RVA: 0x0013C2D5 File Offset: 0x0013A4D5
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x0013C2E4 File Offset: 0x0013A4E4
		private void OnEnable()
		{
			UserProfile.onSurvivorPreferenceChangedGlobal += this.OnSurvivorPreferenceChangedGlobal;
			this.eventSystemLocator.onEventSystemDiscovered += this.OnEventSystemDiscovered;
			if (this.eventSystemLocator.eventSystem)
			{
				this.OnEventSystemDiscovered(this.eventSystemLocator.eventSystem);
			}
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0013C33C File Offset: 0x0013A53C
		private void OnDisable()
		{
			this.eventSystemLocator.onEventSystemDiscovered -= this.OnEventSystemDiscovered;
			UserProfile.onSurvivorPreferenceChangedGlobal -= this.OnSurvivorPreferenceChangedGlobal;
			this.localUser = null;
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0013C36D File Offset: 0x0013A56D
		private void OnSurvivorPreferenceChangedGlobal(UserProfile userProfile)
		{
			this.UpdateDisplayedSurvivor();
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0013C375 File Offset: 0x0013A575
		public void BeginGamemode()
		{
			Console.instance.SubmitCmd(null, "transition_command \"gamemode InfiniteTowerRun; host 0;\"", false);
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x00136D54 File Offset: 0x00134F54
		public void SetDisplayedSurvivor(SurvivorDef newSurvivorDef)
		{
			newSurvivorDef;
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0013C388 File Offset: 0x0013A588
		private void OnEventSystemDiscovered(MPEventSystem eventSystem)
		{
			this.localUser = eventSystem.localUser;
			this.UpdateDisplayedSurvivor();
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0013C39C File Offset: 0x0013A59C
		private void UpdateDisplayedSurvivor()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			UserProfile userProfile;
			if (eventSystem == null)
			{
				userProfile = null;
			}
			else
			{
				LocalUser localUser = eventSystem.localUser;
				userProfile = ((localUser != null) ? localUser.userProfile : null);
			}
			UserProfile userProfile2 = userProfile;
			if (userProfile2 != null)
			{
				StatSheet statSheet = userProfile2.statSheet;
				SurvivorDef survivorPreference = userProfile2.GetSurvivorPreference();
				if (survivorPreference)
				{
					string bodyName = BodyCatalog.GetBodyName(SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorPreference.survivorIndex));
					this.SetHighestWaveDisplay(this.easyHighestWaveText, PerBodyStatDef.highestInfiniteTowerWaveReachedEasy, statSheet, bodyName);
					this.SetHighestWaveDisplay(this.normalHighestWaveText, PerBodyStatDef.highestInfiniteTowerWaveReachedNormal, statSheet, bodyName);
					this.SetHighestWaveDisplay(this.hardHighestWaveText, PerBodyStatDef.highestInfiniteTowerWaveReachedHard, statSheet, bodyName);
				}
				if (this.survivorNameText)
				{
					this.survivorNameText.token = survivorPreference.displayNameToken;
				}
			}
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0013C450 File Offset: 0x0013A650
		private void SetHighestWaveDisplay(LanguageTextMeshController text, PerBodyStatDef statDef, StatSheet statSheet, string bodyName)
		{
			ulong statValueULong = statSheet.GetStatValueULong(statDef, bodyName);
			text.formatArgs = new object[]
			{
				statValueULong
			};
		}

		// Token: 0x0400499C RID: 18844
		[SerializeField]
		private LanguageTextMeshController survivorNameText;

		// Token: 0x0400499D RID: 18845
		[SerializeField]
		private LanguageTextMeshController easyHighestWaveText;

		// Token: 0x0400499E RID: 18846
		[SerializeField]
		private LanguageTextMeshController normalHighestWaveText;

		// Token: 0x0400499F RID: 18847
		[SerializeField]
		private LanguageTextMeshController hardHighestWaveText;

		// Token: 0x040049A0 RID: 18848
		private LocalUser localUser;

		// Token: 0x040049A1 RID: 18849
		private MPEventSystemLocator eventSystemLocator;
	}
}
