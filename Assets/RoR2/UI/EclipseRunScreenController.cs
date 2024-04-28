using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CFA RID: 3322
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class EclipseRunScreenController : MonoBehaviour
	{
		// Token: 0x06004BA0 RID: 19360 RVA: 0x00136CA0 File Offset: 0x00134EA0
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00136CB0 File Offset: 0x00134EB0
		private void OnEnable()
		{
			UserProfile.onSurvivorPreferenceChangedGlobal += this.OnSurvivorPreferenceChangedGlobal;
			this.eventSystemLocator.onEventSystemDiscovered += this.OnEventSystemDiscovered;
			if (this.eventSystemLocator.eventSystem)
			{
				this.OnEventSystemDiscovered(this.eventSystemLocator.eventSystem);
			}
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00136D08 File Offset: 0x00134F08
		private void OnDisable()
		{
			this.eventSystemLocator.onEventSystemDiscovered -= this.OnEventSystemDiscovered;
			UserProfile.onSurvivorPreferenceChangedGlobal -= this.OnSurvivorPreferenceChangedGlobal;
			this.localUser = null;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00136D39 File Offset: 0x00134F39
		private void OnSurvivorPreferenceChangedGlobal(UserProfile userProfile)
		{
			this.UpdateDisplayedSurvivor();
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00136D41 File Offset: 0x00134F41
		public void BeginGamemode()
		{
			Console.instance.SubmitCmd(null, "transition_command \"gamemode EclipseRun; host 0;\"", false);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x00136D54 File Offset: 0x00134F54
		public void SetDisplayedSurvivor(SurvivorDef newSurvivorDef)
		{
			newSurvivorDef;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00136D5D File Offset: 0x00134F5D
		private void OnEventSystemDiscovered(MPEventSystem eventSystem)
		{
			this.localUser = eventSystem.localUser;
			this.UpdateDisplayedSurvivor();
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x00136D74 File Offset: 0x00134F74
		private void UpdateDisplayedSurvivor()
		{
			string empty = string.Empty;
			string token = string.Empty;
			string token2 = string.Empty;
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			SurvivorDef survivorDef;
			if (eventSystem == null)
			{
				survivorDef = null;
			}
			else
			{
				LocalUser localUser = eventSystem.localUser;
				survivorDef = ((localUser != null) ? localUser.userProfile.GetSurvivorPreference() : null);
			}
			SurvivorDef survivorDef2 = survivorDef;
			if (survivorDef2)
			{
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(EclipseRun.GetEclipseDifficultyIndex(Mathf.Clamp(EclipseRun.GetLocalUserSurvivorCompletedEclipseLevel(this.localUser, survivorDef2) + 1, EclipseRun.minEclipseLevel, EclipseRun.maxEclipseLevel)));
				token = difficultyDef.nameToken;
				token2 = difficultyDef.descriptionToken;
			}
			if (this.survivorName)
			{
				this.survivorName.token = survivorDef2.displayNameToken;
			}
			if (this.eclipseDifficultyName)
			{
				this.eclipseDifficultyName.token = token;
			}
			if (this.eclipseDifficultyDescription)
			{
				this.eclipseDifficultyDescription.token = token2;
			}
		}

		// Token: 0x0400485B RID: 18523
		[Header("Required references")]
		public LanguageTextMeshController survivorName;

		// Token: 0x0400485C RID: 18524
		public LanguageTextMeshController eclipseDifficultyName;

		// Token: 0x0400485D RID: 18525
		public LanguageTextMeshController eclipseDifficultyDescription;

		// Token: 0x0400485E RID: 18526
		private LocalUser localUser;

		// Token: 0x0400485F RID: 18527
		private MPEventSystemLocator eventSystemLocator;
	}
}
