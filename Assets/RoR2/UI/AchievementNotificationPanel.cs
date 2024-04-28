using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CAE RID: 3246
	public class AchievementNotificationPanel : MonoBehaviour
	{
		// Token: 0x06004A0A RID: 18954 RVA: 0x001302B1 File Offset: 0x0012E4B1
		private void Awake()
		{
			AchievementNotificationPanel.instancesList.Add(this);
			this.onStart.Invoke();
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x001302C9 File Offset: 0x0012E4C9
		private void OnDestroy()
		{
			AchievementNotificationPanel.instancesList.Remove(this);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x001302D7 File Offset: 0x0012E4D7
		public void SetAchievementDef(AchievementDef achievementDef)
		{
			this.achievementIconImage.sprite = achievementDef.GetAchievedIcon();
			this.achievementName.text = Language.GetString(achievementDef.nameToken);
			this.achievementDescription.text = Language.GetString(achievementDef.descriptionToken);
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x00130318 File Offset: 0x0012E518
		[CanBeNull]
		private static Canvas GetUserCanvas([NotNull] LocalUser localUser)
		{
			if (!Run.instance)
			{
				return RoR2Application.instance.mainCanvas;
			}
			CameraRigController cameraRigController = localUser.cameraRigController;
			if (!cameraRigController)
			{
				return null;
			}
			HUD hud = cameraRigController.hud;
			if (!hud)
			{
				return null;
			}
			return hud.mainContainer.GetComponent<Canvas>();
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x00130369 File Offset: 0x0012E569
		private static bool IsAppropriateTimeToDisplayUserAchievementNotification(LocalUser localUser)
		{
			return !GameOverController.instance;
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x00130378 File Offset: 0x0012E578
		private static void DispatchAchievementNotification(Canvas canvas, AchievementDef achievementDef)
		{
			UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/AchievementNotificationPanel"), canvas.transform).GetComponent<AchievementNotificationPanel>().SetAchievementDef(achievementDef);
			Util.PlaySound(achievementDef.GetAchievementSoundString(), RoR2Application.instance.gameObject);
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x001303B0 File Offset: 0x0012E5B0
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			RoR2Application.onFixedUpdate += AchievementNotificationPanel.StaticFixedUpdate;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x001303C4 File Offset: 0x0012E5C4
		private static void StaticFixedUpdate()
		{
			foreach (LocalUser localUser in LocalUserManager.readOnlyLocalUsersList)
			{
				if (localUser.userProfile.hasUnviewedAchievement)
				{
					Canvas canvas = AchievementNotificationPanel.GetUserCanvas(localUser);
					if (!(canvas == null) && !AchievementNotificationPanel.instancesList.Any((AchievementNotificationPanel instance) => instance.transform.parent == canvas.transform) && AchievementNotificationPanel.IsAppropriateTimeToDisplayUserAchievementNotification(localUser))
					{
						string text = localUser.userProfile.PopNextUnviewedAchievementName();
						if (text != null)
						{
							AchievementDef achievementDef = AchievementManager.GetAchievementDef(text);
							if (achievementDef != null)
							{
								AchievementNotificationPanel.DispatchAchievementNotification(canvas, achievementDef);
							}
						}
					}
				}
			}
		}

		// Token: 0x040046CF RID: 18127
		private static readonly List<AchievementNotificationPanel> instancesList = new List<AchievementNotificationPanel>();

		// Token: 0x040046D0 RID: 18128
		public Image achievementIconImage;

		// Token: 0x040046D1 RID: 18129
		public TextMeshProUGUI achievementName;

		// Token: 0x040046D2 RID: 18130
		public TextMeshProUGUI achievementDescription;

		// Token: 0x040046D3 RID: 18131
		public UnityEvent onStart;
	}
}
