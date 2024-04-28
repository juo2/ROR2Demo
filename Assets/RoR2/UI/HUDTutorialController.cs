using System;
using EntityStates;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D11 RID: 3345
	[RequireComponent(typeof(HUD))]
	public class HUDTutorialController : MonoBehaviour
	{
		// Token: 0x06004C40 RID: 19520 RVA: 0x0013A416 File Offset: 0x00138616
		private void Awake()
		{
			this.hud = base.GetComponent<HUD>();
			if (this.equipmentTutorialObject)
			{
				this.equipmentTutorialObject.SetActive(false);
			}
			if (this.sprintTutorialObject)
			{
				this.sprintTutorialObject.SetActive(false);
			}
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0013A456 File Offset: 0x00138656
		private UserProfile GetUserProfile()
		{
			if (this.hud && this.hud.localUserViewer != null)
			{
				return this.hud.localUserViewer.userProfile;
			}
			return null;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0013A484 File Offset: 0x00138684
		private void HandleTutorial(GameObject tutorialPopup, ref UserProfile.TutorialProgression tutorialProgression, bool dismiss = false, bool progress = true)
		{
			if (tutorialPopup && !dismiss)
			{
				tutorialPopup.SetActive(true);
			}
			tutorialProgression.shouldShow = false;
			if (progress)
			{
				tutorialProgression.showCount += 1U;
			}
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0013A4B0 File Offset: 0x001386B0
		private void Update()
		{
			if (this.hud && this.hud.localUserViewer != null)
			{
				UserProfile userProfile = this.GetUserProfile();
				CharacterBody cachedBody = this.hud.localUserViewer.cachedBody;
				if (userProfile != null && cachedBody)
				{
					if (userProfile.tutorialEquipment.shouldShow && this.equipmentIcon.hasEquipment)
					{
						this.HandleTutorial(this.equipmentTutorialObject, ref userProfile.tutorialEquipment, false, true);
					}
					if (userProfile.tutorialSprint.shouldShow)
					{
						if (cachedBody.isSprinting)
						{
							this.HandleTutorial(null, ref userProfile.tutorialSprint, true, true);
							return;
						}
						EntityStateMachine component = cachedBody.GetComponent<EntityStateMachine>();
						if (((component != null) ? component.state : null) is GenericCharacterMain)
						{
							this.sprintTutorialStopwatch += Time.deltaTime;
						}
						if (this.sprintTutorialStopwatch >= this.sprintTutorialTriggerTime)
						{
							this.HandleTutorial(this.sprintTutorialObject, ref userProfile.tutorialSprint, false, false);
							return;
						}
					}
					else if (this.sprintTutorialObject && this.sprintTutorialObject.activeInHierarchy && cachedBody.isSprinting)
					{
						UnityEngine.Object.Destroy(this.sprintTutorialObject);
						this.sprintTutorialObject = null;
						UserProfile userProfile2 = userProfile;
						userProfile2.tutorialSprint.showCount = userProfile2.tutorialSprint.showCount + 1U;
					}
				}
			}
		}

		// Token: 0x04004923 RID: 18723
		private HUD hud;

		// Token: 0x04004924 RID: 18724
		[Header("Equipment Tutorial")]
		[Tooltip("The tutorial popup object.")]
		public GameObject equipmentTutorialObject;

		// Token: 0x04004925 RID: 18725
		[Tooltip("The equipment icon to monitor for a change to trigger the tutorial popup.")]
		public EquipmentIcon equipmentIcon;

		// Token: 0x04004926 RID: 18726
		[Header("Sprint Tutorial")]
		[Tooltip("The tutorial popup object.")]
		public GameObject sprintTutorialObject;

		// Token: 0x04004927 RID: 18727
		[Tooltip("How long to wait for the player to sprint before showing the tutorial popup.")]
		public float sprintTutorialTriggerTime = 30f;

		// Token: 0x04004928 RID: 18728
		private float sprintTutorialStopwatch;
	}
}
