using System;
using System.Collections.Generic;
using System.Net;
using Facepunch.Steamworks;
using HGsignup.Service;
using TMPro;
using UnityEngine;

namespace RoR2.UI.MainMenu
{
	// Token: 0x02000DCC RID: 3532
	public class SignupMainMenuScreen : BaseMainMenuScreen
	{
		// Token: 0x060050E1 RID: 20705 RVA: 0x0014E47E File Offset: 0x0014C67E
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ViewablesCatalog.AddNodeToRoot(new ViewablesCatalog.Node(SignupMainMenuScreen.viewableName, false, null)
			{
				shouldShowUnviewed = new Func<UserProfile, bool>(SignupMainMenuScreen.CheckViewable)
			});
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0014E4A3 File Offset: 0x0014C6A3
		private static bool CheckViewable(UserProfile userProfile)
		{
			return !userProfile.HasViewedViewable("/" + SignupMainMenuScreen.viewableName);
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x0014E4BD File Offset: 0x0014C6BD
		private new void Awake()
		{
			this.signupClient = new SignupClient("prod");
			this.successfulEmailSubmissions = new HashSet<string>();
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0014E4DC File Offset: 0x0014C6DC
		private void OnEnable()
		{
			HGTextMeshProUGUI hgtextMeshProUGUI = this.waitingText;
			if (hgtextMeshProUGUI != null)
			{
				GameObject gameObject = hgtextMeshProUGUI.gameObject;
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			HGTextMeshProUGUI hgtextMeshProUGUI2 = this.successText;
			if (hgtextMeshProUGUI2 != null)
			{
				GameObject gameObject2 = hgtextMeshProUGUI2.gameObject;
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			HGTextMeshProUGUI hgtextMeshProUGUI3 = this.failureText;
			if (hgtextMeshProUGUI3 != null)
			{
				GameObject gameObject3 = hgtextMeshProUGUI3.gameObject;
				if (gameObject3 != null)
				{
					gameObject3.SetActive(false);
				}
			}
			if (this.backGamepadInputEvent)
			{
				this.backGamepadInputEvent.enabled = true;
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x0014E55C File Offset: 0x0014C75C
		private new void Update()
		{
			base.Update();
			if (this.submitButton)
			{
				if (this.emailInput && SignupClient.IsEmailValid(this.emailInput.text) && !this.awaitingResponse && !this.successfulEmailSubmissions.Contains(this.emailInput.text))
				{
					this.submitButton.interactable = true;
				}
				else
				{
					this.submitButton.interactable = false;
				}
			}
			if (this.backButton)
			{
				this.backButton.interactable = !this.awaitingResponse;
			}
			if (this.backGamepadInputEvent)
			{
				this.backGamepadInputEvent.enabled = !this.awaitingResponse;
			}
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x0014E618 File Offset: 0x0014C818
		public void TrySubmit()
		{
			if (!this.awaitingResponse && this.emailInput && !this.successfulEmailSubmissions.Contains(this.emailInput.text) && SignupClient.IsEmailValid(this.emailInput.text))
			{
				this.email = this.emailInput.text;
				User user = Client.Instance.User;
				user.OnEncryptedAppTicketRequestComplete = (Action<bool, byte[]>)Delegate.Combine(user.OnEncryptedAppTicketRequestComplete, new Action<bool, byte[]>(this.ProcessAppTicketRefresh));
				Client.Instance.User.RequestEncryptedAppTicketAsync(new byte[0]);
				this.awaitingResponse = true;
				HGTextMeshProUGUI hgtextMeshProUGUI = this.waitingText;
				if (hgtextMeshProUGUI != null)
				{
					GameObject gameObject = hgtextMeshProUGUI.gameObject;
					if (gameObject != null)
					{
						gameObject.SetActive(true);
					}
				}
				HGTextMeshProUGUI hgtextMeshProUGUI2 = this.successText;
				if (hgtextMeshProUGUI2 != null)
				{
					GameObject gameObject2 = hgtextMeshProUGUI2.gameObject;
					if (gameObject2 != null)
					{
						gameObject2.SetActive(false);
					}
				}
				HGTextMeshProUGUI hgtextMeshProUGUI3 = this.failureText;
				if (hgtextMeshProUGUI3 == null)
				{
					return;
				}
				GameObject gameObject3 = hgtextMeshProUGUI3.gameObject;
				if (gameObject3 == null)
				{
					return;
				}
				gameObject3.SetActive(false);
			}
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x0014E720 File Offset: 0x0014C920
		private void ProcessAppTicketRefresh(bool success, byte[] ticket)
		{
			bool flag = success;
			User user = Client.Instance.User;
			user.OnEncryptedAppTicketRequestComplete = (Action<bool, byte[]>)Delegate.Remove(user.OnEncryptedAppTicketRequestComplete, new Action<bool, byte[]>(this.ProcessAppTicketRefresh));
			if (success)
			{
				Debug.Log("Successfully got Encrypted App Ticket from Steam for mailing list!");
				if (SignupClient.IsTicketValid(ticket))
				{
					if (this.signupClient != null)
					{
						this.signupClient.SignupCompleted += this.SignupCompleted;
						this.signupClient.SignupSteamAsync(this.email, "Canary", ticket);
					}
				}
				else
				{
					Debug.LogError("Can't signup with an invalid ticket!");
					flag = false;
				}
			}
			else
			{
				Debug.LogError("Failure refreshing Encrypted App Ticket from Steam for mailing list!");
				flag = false;
			}
			if (!flag)
			{
				this.awaitingResponse = false;
				HGTextMeshProUGUI hgtextMeshProUGUI = this.waitingText;
				if (hgtextMeshProUGUI != null)
				{
					GameObject gameObject = hgtextMeshProUGUI.gameObject;
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
				HGTextMeshProUGUI hgtextMeshProUGUI2 = this.successText;
				if (hgtextMeshProUGUI2 != null)
				{
					GameObject gameObject2 = hgtextMeshProUGUI2.gameObject;
					if (gameObject2 != null)
					{
						gameObject2.SetActive(false);
					}
				}
				HGTextMeshProUGUI hgtextMeshProUGUI3 = this.failureText;
				if (hgtextMeshProUGUI3 == null)
				{
					return;
				}
				GameObject gameObject3 = hgtextMeshProUGUI3.gameObject;
				if (gameObject3 == null)
				{
					return;
				}
				gameObject3.SetActive(true);
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0014E820 File Offset: 0x0014CA20
		private void SignupCompleted(string email, HttpStatusCode statusCode)
		{
			this.awaitingResponse = false;
			HGTextMeshProUGUI hgtextMeshProUGUI = this.waitingText;
			if (hgtextMeshProUGUI != null)
			{
				GameObject gameObject = hgtextMeshProUGUI.gameObject;
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			if (statusCode == HttpStatusCode.OK)
			{
				this.successfulEmailSubmissions.Add(email);
				HGTextMeshProUGUI hgtextMeshProUGUI2 = this.successText;
				if (hgtextMeshProUGUI2 != null)
				{
					GameObject gameObject2 = hgtextMeshProUGUI2.gameObject;
					if (gameObject2 != null)
					{
						gameObject2.SetActive(true);
					}
				}
				HGTextMeshProUGUI hgtextMeshProUGUI3 = this.failureText;
				if (hgtextMeshProUGUI3 == null)
				{
					return;
				}
				GameObject gameObject3 = hgtextMeshProUGUI3.gameObject;
				if (gameObject3 == null)
				{
					return;
				}
				gameObject3.SetActive(false);
				return;
			}
			else
			{
				HGTextMeshProUGUI hgtextMeshProUGUI4 = this.successText;
				if (hgtextMeshProUGUI4 != null)
				{
					GameObject gameObject4 = hgtextMeshProUGUI4.gameObject;
					if (gameObject4 != null)
					{
						gameObject4.SetActive(false);
					}
				}
				HGTextMeshProUGUI hgtextMeshProUGUI5 = this.failureText;
				if (hgtextMeshProUGUI5 == null)
				{
					return;
				}
				GameObject gameObject5 = hgtextMeshProUGUI5.gameObject;
				if (gameObject5 == null)
				{
					return;
				}
				gameObject5.SetActive(true);
				return;
			}
		}

		// Token: 0x04004D73 RID: 19827
		private const string environment = "prod";

		// Token: 0x04004D74 RID: 19828
		private const string title = "Canary";

		// Token: 0x04004D75 RID: 19829
		private static string viewableName = "Signup";

		// Token: 0x04004D76 RID: 19830
		[SerializeField]
		private TMP_InputField emailInput;

		// Token: 0x04004D77 RID: 19831
		[SerializeField]
		private HGButton submitButton;

		// Token: 0x04004D78 RID: 19832
		[SerializeField]
		private HGButton backButton;

		// Token: 0x04004D79 RID: 19833
		[SerializeField]
		private HGTextMeshProUGUI successText;

		// Token: 0x04004D7A RID: 19834
		[SerializeField]
		private HGTextMeshProUGUI failureText;

		// Token: 0x04004D7B RID: 19835
		[SerializeField]
		private HGTextMeshProUGUI waitingText;

		// Token: 0x04004D7C RID: 19836
		[SerializeField]
		private HGGamepadInputEvent backGamepadInputEvent;

		// Token: 0x04004D7D RID: 19837
		private SignupClient signupClient;

		// Token: 0x04004D7E RID: 19838
		private string email;

		// Token: 0x04004D7F RID: 19839
		private bool awaitingResponse;

		// Token: 0x04004D80 RID: 19840
		private HashSet<string> successfulEmailSubmissions;
	}
}
