using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CAD RID: 3245
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class AchievementListPanelController : MonoBehaviour
	{
		// Token: 0x06004A01 RID: 18945 RVA: 0x001300A0 File Offset: 0x0012E2A0
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x001300AE File Offset: 0x0012E2AE
		private void OnEnable()
		{
			this.Rebuild();
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x001300B8 File Offset: 0x0012E2B8
		private UserProfile GetUserProfile()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			if (eventSystem)
			{
				LocalUser localUser = LocalUserManager.FindLocalUser(eventSystem.player);
				if (localUser != null)
				{
					return localUser.userProfile;
				}
			}
			return null;
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x001300F0 File Offset: 0x0012E2F0
		static AchievementListPanelController()
		{
			AchievementListPanelController.BuildAchievementListOrder();
			AchievementManager.onAchievementsRegistered += AchievementListPanelController.BuildAchievementListOrder;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x00130114 File Offset: 0x0012E314
		private static void BuildAchievementListOrder()
		{
			AchievementListPanelController.sortedAchievementIdentifiers.Clear();
			HashSet<string> encounteredIdentifiers = new HashSet<string>();
			ReadOnlyCollection<string> readOnlyAchievementIdentifiers = AchievementManager.readOnlyAchievementIdentifiers;
			for (int i = 0; i < readOnlyAchievementIdentifiers.Count; i++)
			{
				string achievementIdentifier = readOnlyAchievementIdentifiers[i];
				if (string.IsNullOrEmpty(AchievementManager.GetAchievementDef(achievementIdentifier).prerequisiteAchievementIdentifier))
				{
					AchievementListPanelController.AddAchievementToOrderedList(achievementIdentifier, encounteredIdentifiers);
				}
			}
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x0013016C File Offset: 0x0012E36C
		private static void AddAchievementToOrderedList(string achievementIdentifier, HashSet<string> encounteredIdentifiers)
		{
			if (encounteredIdentifiers.Contains(achievementIdentifier))
			{
				return;
			}
			encounteredIdentifiers.Add(achievementIdentifier);
			AchievementListPanelController.sortedAchievementIdentifiers.Add(achievementIdentifier);
			string[] childAchievementIdentifiers = AchievementManager.GetAchievementDef(achievementIdentifier).childAchievementIdentifiers;
			for (int i = 0; i < childAchievementIdentifiers.Length; i++)
			{
				AchievementListPanelController.AddAchievementToOrderedList(childAchievementIdentifiers[i], encounteredIdentifiers);
			}
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x001301BC File Offset: 0x0012E3BC
		private void SetCardCount(int desiredCardCount)
		{
			while (this.cardsList.Count < desiredCardCount)
			{
				AchievementCardController component = UnityEngine.Object.Instantiate<GameObject>(this.achievementCardPrefab, this.container).GetComponent<AchievementCardController>();
				this.cardsList.Add(component);
			}
			while (this.cardsList.Count > desiredCardCount)
			{
				UnityEngine.Object.Destroy(this.cardsList[this.cardsList.Count - 1].gameObject);
				this.cardsList.RemoveAt(this.cardsList.Count - 1);
			}
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x00130248 File Offset: 0x0012E448
		private void Rebuild()
		{
			UserProfile userProfile = this.GetUserProfile();
			this.SetCardCount(AchievementListPanelController.sortedAchievementIdentifiers.Count);
			for (int i = 0; i < AchievementListPanelController.sortedAchievementIdentifiers.Count; i++)
			{
				this.cardsList[i].SetAchievement(AchievementListPanelController.sortedAchievementIdentifiers[i], userProfile);
			}
		}

		// Token: 0x040046CA RID: 18122
		public GameObject achievementCardPrefab;

		// Token: 0x040046CB RID: 18123
		public RectTransform container;

		// Token: 0x040046CC RID: 18124
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040046CD RID: 18125
		private readonly List<AchievementCardController> cardsList = new List<AchievementCardController>();

		// Token: 0x040046CE RID: 18126
		private static readonly List<string> sortedAchievementIdentifiers = new List<string>();
	}
}
