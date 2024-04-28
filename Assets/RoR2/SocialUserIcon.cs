using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020008DA RID: 2266
	[RequireComponent(typeof(RawImage))]
	public class SocialUserIcon : UIBehaviour
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000D6674 File Offset: 0x000D4874
		private Texture defaultTexture
		{
			get
			{
				return LegacyResourcesAPI.Load<Texture>("Textures/UI/texDefaultSocialUserIcon");
			}
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000D6680 File Offset: 0x000D4880
		protected override void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.generatedTexture);
			this.generatedTexture = null;
			base.OnDestroy();
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000D669A File Offset: 0x000D489A
		protected override void Awake()
		{
			base.Awake();
			this.rawImageComponent = base.GetComponent<RawImage>();
			this.rawImageComponent.texture = this.defaultTexture;
			if (!UserManager.P_UseSocialIcon.value)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000D66D7 File Offset: 0x000D48D7
		private void HandleNewTexture(Texture2D tex)
		{
			this.generatedTexture = tex;
			this.rawImageComponent.texture = tex;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000D66EC File Offset: 0x000D48EC
		public virtual void Refresh()
		{
			if (!PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.UserIcon))
			{
				return;
			}
			PlatformSystems.userManager.GetAvatar(this.userID, base.gameObject, this.generatedTexture, this.avatarSize, new Action<Texture2D>(this.HandleNewTexture));
			if (!this.generatedTexture)
			{
				this.rawImageComponent.texture = this.defaultTexture;
			}
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000D6754 File Offset: 0x000D4954
		public virtual void SetFromMaster(CharacterMaster master)
		{
			if (!PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.UserIcon))
			{
				return;
			}
			if (master)
			{
				PlayerCharacterMasterController component = master.GetComponent<PlayerCharacterMasterController>();
				if (component)
				{
					NetworkUser networkUser = component.networkUser;
					this.RefreshWithUser(new UserID(new CSteamID(networkUser.id.value)));
					return;
				}
			}
			this.userID = default(UserID);
			this.sourceType = SocialUserIcon.SourceType.Local;
			this.Refresh();
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000D67C4 File Offset: 0x000D49C4
		public void RefreshWithUser(UserID newUserID)
		{
			if (!PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.UserIcon))
			{
				return;
			}
			if (this.sourceType == SocialUserIcon.SourceType.Network && newUserID.Equals(this.userID))
			{
				return;
			}
			this.sourceType = SocialUserIcon.SourceType.Network;
			this.userID = newUserID;
			this.Refresh();
		}

		// Token: 0x040033F3 RID: 13299
		private RawImage rawImageComponent;

		// Token: 0x040033F4 RID: 13300
		protected Texture2D generatedTexture;

		// Token: 0x040033F5 RID: 13301
		private UserID userID;

		// Token: 0x040033F6 RID: 13302
		[SerializeField]
		private UserManager.AvatarSize avatarSize;

		// Token: 0x040033F7 RID: 13303
		private SocialUserIcon.SourceType sourceType;

		// Token: 0x020008DB RID: 2267
		private enum SourceType
		{
			// Token: 0x040033F9 RID: 13305
			Local,
			// Token: 0x040033FA RID: 13306
			Network
		}
	}
}
