using System;
using System.Linq;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000EC2 RID: 3778
	[RegisterAchievement("StayAlive1", "Items.ExtraLife", null, null)]
	public class StayAlive1Achievement : BaseAchievement
	{
		// Token: 0x06005610 RID: 22032 RVA: 0x0015F346 File Offset: 0x0015D546
		public override void OnInstall()
		{
			base.OnInstall();
			RoR2Application.onUpdate += this.Check;
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x0015F35F File Offset: 0x0015D55F
		public override void OnUninstall()
		{
			RoR2Application.onUpdate -= this.Check;
			base.OnUninstall();
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0015F378 File Offset: 0x0015D578
		private void Check()
		{
			NetworkUser networkUser = NetworkUser.readOnlyLocalPlayersList.FirstOrDefault((NetworkUser v) => v.localUser == base.localUser);
			if (networkUser)
			{
				GameObject masterObject = networkUser.masterObject;
				if (masterObject)
				{
					CharacterMaster component = masterObject.GetComponent<CharacterMaster>();
					if (component && component.currentLifeStopwatch >= 1800f)
					{
						base.Grant();
					}
				}
			}
		}

		// Token: 0x04005077 RID: 20599
		private const float requirement = 1800f;
	}
}
