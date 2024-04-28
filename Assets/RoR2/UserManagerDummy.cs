using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009E7 RID: 2535
	public class UserManagerDummy : UserManager
	{
		// Token: 0x06003A7D RID: 14973 RVA: 0x000F2C3A File Offset: 0x000F0E3A
		public override void GetAvatar(UserID userID, GameObject requestSender, Texture2D tex, UserManager.AvatarSize size, Action<Texture2D> onRecieved)
		{
			if (onRecieved != null)
			{
				onRecieved(null);
			}
		}
	}
}
