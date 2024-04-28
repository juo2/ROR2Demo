using System;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D3B RID: 3387
	public class LocalUserSignInCardController : MonoBehaviour
	{
		// Token: 0x06004D3B RID: 19771 RVA: 0x0013F00C File Offset: 0x0013D20C
		private void Update()
		{
			if (this.requestedUserProfile != null != this.userProfileSelectionList)
			{
				if (!this.userProfileSelectionList)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.userProfileSelectionListPrefab, base.transform);
					this.userProfileSelectionList = gameObject.GetComponent<UserProfileListController>();
					this.userProfileSelectionList.GetComponent<MPEventSystemProvider>().eventSystem = MPEventSystemManager.FindEventSystem(this.rewiredPlayer);
					this.userProfileSelectionList.onProfileSelected += this.OnUserSelectedUserProfile;
				}
				else
				{
					UnityEngine.Object.Destroy(this.userProfileSelectionList.gameObject);
					this.userProfileSelectionList = null;
				}
			}
			if (this.rewiredPlayer == null)
			{
				this.nameLabel.gameObject.SetActive(false);
				this.promptLabel.text = "Press 'Start'";
				this.cardImage.color = this.unselectedColor;
				this.cardImage.sprite = this.playerCardNone;
				return;
			}
			this.cardImage.color = this.selectedColor;
			this.nameLabel.gameObject.SetActive(true);
			if (this.requestedUserProfile == null)
			{
				this.cardImage.sprite = this.playerCardNone;
				this.nameLabel.text = "";
				this.promptLabel.text = "...";
				return;
			}
			this.cardImage.sprite = this.playerCardKBM;
			this.nameLabel.text = this.requestedUserProfile.name;
			this.promptLabel.text = "";
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x0013F187 File Offset: 0x0013D387
		// (set) Token: 0x06004D3D RID: 19773 RVA: 0x0013F18F File Offset: 0x0013D38F
		public Player rewiredPlayer
		{
			get
			{
				return this._rewiredPlayer;
			}
			set
			{
				if (this._rewiredPlayer == value)
				{
					return;
				}
				this._rewiredPlayer = value;
				if (this._rewiredPlayer == null)
				{
					this.requestedUserProfile = null;
				}
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x0013F1B1 File Offset: 0x0013D3B1
		// (set) Token: 0x06004D3F RID: 19775 RVA: 0x0013F1B9 File Offset: 0x0013D3B9
		public UserProfile requestedUserProfile
		{
			get
			{
				return this._requestedUserProfile;
			}
			private set
			{
				if (this._requestedUserProfile == value)
				{
					return;
				}
				if (this._requestedUserProfile != null)
				{
					this._requestedUserProfile.isClaimed = false;
				}
				this._requestedUserProfile = value;
				if (this._requestedUserProfile != null)
				{
					this._requestedUserProfile.isClaimed = true;
				}
			}
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x0013F1F4 File Offset: 0x0013D3F4
		private void OnUserSelectedUserProfile(UserProfile userProfile)
		{
			this.requestedUserProfile = userProfile;
		}

		// Token: 0x04004A3C RID: 19004
		public TextMeshProUGUI nameLabel;

		// Token: 0x04004A3D RID: 19005
		public TextMeshProUGUI promptLabel;

		// Token: 0x04004A3E RID: 19006
		public Image cardImage;

		// Token: 0x04004A3F RID: 19007
		public Sprite playerCardNone;

		// Token: 0x04004A40 RID: 19008
		public Sprite playerCardKBM;

		// Token: 0x04004A41 RID: 19009
		public Sprite playerCardController;

		// Token: 0x04004A42 RID: 19010
		public Color unselectedColor;

		// Token: 0x04004A43 RID: 19011
		public Color selectedColor;

		// Token: 0x04004A44 RID: 19012
		private UserProfileListController userProfileSelectionList;

		// Token: 0x04004A45 RID: 19013
		public GameObject userProfileSelectionListPrefab;

		// Token: 0x04004A46 RID: 19014
		private Player _rewiredPlayer;

		// Token: 0x04004A47 RID: 19015
		private UserProfile _requestedUserProfile;
	}
}
