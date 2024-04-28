using System;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009E5 RID: 2533
	public abstract class UserManager
	{
		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06003A73 RID: 14963 RVA: 0x000F2B60 File Offset: 0x000F0D60
		// (remove) Token: 0x06003A74 RID: 14964 RVA: 0x000F2B94 File Offset: 0x000F0D94
		public static event Action OnDisplayNameMappingComplete;

		// Token: 0x06003A75 RID: 14965 RVA: 0x000F2BC7 File Offset: 0x000F0DC7
		internal void InvokeDisplayMappingCompleteAction()
		{
			Action onDisplayNameMappingComplete = UserManager.OnDisplayNameMappingComplete;
			if (onDisplayNameMappingComplete == null)
			{
				return;
			}
			onDisplayNameMappingComplete();
		}

		// Token: 0x06003A76 RID: 14966
		public abstract void GetAvatar(UserID userID, GameObject requestSender, Texture2D tex, UserManager.AvatarSize size, Action<Texture2D> onRecieved);

		// Token: 0x06003A77 RID: 14967 RVA: 0x000F2BD8 File Offset: 0x000F0DD8
		protected static Texture2D BuildTexture(Texture2D generatedTexture, int width, int height)
		{
			if (generatedTexture && (generatedTexture.width != width || generatedTexture.height != height))
			{
				generatedTexture.Resize(width, height);
			}
			if (generatedTexture == null)
			{
				generatedTexture = new Texture2D(width, height);
			}
			return generatedTexture;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000F2C10 File Offset: 0x000F0E10
		public virtual string GetUserDisplayName(UserID other)
		{
			return string.Empty;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public virtual int GetUserID()
		{
			return 0;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000F2C17 File Offset: 0x000F0E17
		public virtual string GetUserName()
		{
			return "";
		}

		// Token: 0x0400394A RID: 14666
		public static BoolConVar P_UseSocialIcon = new BoolConVar("UseSocialIconFlag", ConVarFlags.Archive, "1", "A per-platform flag that indicates whether we display user icons or not.");

		// Token: 0x020009E6 RID: 2534
		public enum AvatarSize
		{
			// Token: 0x0400394C RID: 14668
			Small,
			// Token: 0x0400394D RID: 14669
			Medium,
			// Token: 0x0400394E RID: 14670
			Large
		}
	}
}
