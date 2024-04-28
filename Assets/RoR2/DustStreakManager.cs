using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004DF RID: 1247
	public class DustStreakManager : MonoBehaviour
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x00063EEB File Offset: 0x000620EB
		private void Start()
		{
			this.streakTimer = UnityEngine.Random.Range(this.timeBetweenStreaksMin, this.timeBetweenStreaksMax);
			this.startDustStreaks = true;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00063F0C File Offset: 0x0006210C
		private void FixedUpdate()
		{
			if (this.startDustStreaks)
			{
				this.streakTimer -= Time.deltaTime;
				if (this.streakTimer <= 0f)
				{
					this.streakTimer = UnityEngine.Random.Range(this.timeBetweenStreaksMin, this.timeBetweenStreaksMax);
					this.streakNum = UnityEngine.Random.Range(0, this.dustStreakLocations.Count);
					EffectManager.SimpleEffect(this.dustStreakPrefab, this.dustStreakLocations[this.streakNum].position, this.dustStreakPrefab.transform.rotation, true);
				}
			}
		}

		// Token: 0x04001C6F RID: 7279
		public GameObject dustStreakPrefab;

		// Token: 0x04001C70 RID: 7280
		public float timeBetweenStreaksMin;

		// Token: 0x04001C71 RID: 7281
		public float timeBetweenStreaksMax;

		// Token: 0x04001C72 RID: 7282
		private float streakTimer;

		// Token: 0x04001C73 RID: 7283
		public List<Transform> dustStreakLocations = new List<Transform>();

		// Token: 0x04001C74 RID: 7284
		private int streakNum;

		// Token: 0x04001C75 RID: 7285
		private bool startDustStreaks;
	}
}
