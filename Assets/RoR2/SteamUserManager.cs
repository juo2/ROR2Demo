using System;
using Facepunch.Steamworks;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009D0 RID: 2512
	public class SteamUserManager : UserManager
	{
		// Token: 0x06003993 RID: 14739 RVA: 0x000F0331 File Offset: 0x000EE531
		public override void GetAvatar(UserID id, GameObject sender, Texture2D cachedTexture, UserManager.AvatarSize size, Action<Texture2D> onRecieved)
		{
			SteamUserManager.GetSteamAvatar(id, sender, cachedTexture, size, onRecieved);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000F0340 File Offset: 0x000EE540
		public static void GetSteamAvatar(UserID id, GameObject sender, Texture2D cachedTexture, UserManager.AvatarSize size, Action<Texture2D> onRecieved)
		{
			ulong id2 = id.ID;
			Client instance = Client.Instance;
			Friends.AvatarSize size2;
			switch (size)
			{
			case UserManager.AvatarSize.Small:
				size2 = Friends.AvatarSize.Small;
				goto IL_48;
			case UserManager.AvatarSize.Medium:
				size2 = Friends.AvatarSize.Medium;
				goto IL_48;
			}
			size2 = Friends.AvatarSize.Large;
			IL_48:
			if (instance != null)
			{
				Image cachedAvatar = instance.Friends.GetCachedAvatar(size2, id2);
				if (cachedAvatar != null)
				{
					SteamUserManager.OnSteamAvatarReceived(cachedAvatar, sender, cachedTexture, onRecieved);
					return;
				}
				Action<Image> callback = delegate(Image x)
				{
					SteamUserManager.OnSteamAvatarReceived(x, sender, cachedTexture, onRecieved);
				};
				instance.Friends.GetAvatar(size2, id2, callback);
			}
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000F03E4 File Offset: 0x000EE5E4
		private static void OnSteamAvatarReceived(Image image, GameObject sender, Texture2D tex, Action<Texture2D> onRecieved)
		{
			if (image == null)
			{
				return;
			}
			if (sender == null)
			{
				return;
			}
			int width = image.Width;
			int height = image.Height;
			tex = UserManager.BuildTexture(tex, width, height);
			byte[] data = image.Data;
			Color32[] array = new Color32[data.Length / 4];
			for (int i = 0; i < height; i++)
			{
				int num = height - 1 - i;
				for (int j = 0; j < width; j++)
				{
					int num2 = (i * width + j) * 4;
					array[num * width + j] = new Color32(data[num2], data[num2 + 1], data[num2 + 2], data[num2 + 3]);
				}
			}
			if (tex)
			{
				tex.SetPixels32(array);
				tex.Apply();
			}
			onRecieved(tex);
		}
	}
}
