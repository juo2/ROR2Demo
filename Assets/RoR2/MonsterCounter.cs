using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007BB RID: 1979
	public class MonsterCounter : MonoBehaviour
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x000B4CEB File Offset: 0x000B2EEB
		private int CountEnemies()
		{
			return TeamComponent.GetTeamMembers(TeamIndex.Monster).Count;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000B4CF8 File Offset: 0x000B2EF8
		private void Update()
		{
			this.enemyList = this.CountEnemies();
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000B4D06 File Offset: 0x000B2F06
		private void OnGUI()
		{
			GUI.Label(new Rect(12f, 160f, 200f, 25f), "Living Monsters: " + this.enemyList);
		}

		// Token: 0x04002D31 RID: 11569
		private int enemyList;
	}
}
