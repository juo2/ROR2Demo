using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007CB RID: 1995
	public class MusicTrackOverride : MonoBehaviour
	{
		// Token: 0x06002A4D RID: 10829 RVA: 0x000B6AF8 File Offset: 0x000B4CF8
		private void OnEnable()
		{
			InstanceTracker.Add<MusicTrackOverride>(this);
			if (InstanceTracker.GetInstancesList<MusicTrackOverride>().Count == 1)
			{
				MusicController.pickTrackHook += MusicTrackOverride.PickMusicTrack;
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000B6B1E File Offset: 0x000B4D1E
		private void OnDisable()
		{
			if (InstanceTracker.GetInstancesList<MusicTrackOverride>().Count == 1)
			{
				MusicController.pickTrackHook -= MusicTrackOverride.PickMusicTrack;
			}
			InstanceTracker.Remove<MusicTrackOverride>(this);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000B6B44 File Offset: 0x000B4D44
		private static void PickMusicTrack(MusicController musicController, ref MusicTrackDef newTrack)
		{
			List<MusicTrackOverride> instancesList = InstanceTracker.GetInstancesList<MusicTrackOverride>();
			int num = int.MinValue;
			int i = 0;
			int count = instancesList.Count;
			while (i < count)
			{
				MusicTrackOverride musicTrackOverride = instancesList[i];
				int num2 = musicTrackOverride.priority;
				if (num < num2)
				{
					num = num2;
					newTrack = musicTrackOverride.track;
				}
				i++;
			}
		}

		// Token: 0x04002D8F RID: 11663
		public MusicTrackDef track;

		// Token: 0x04002D90 RID: 11664
		public int priority;
	}
}
