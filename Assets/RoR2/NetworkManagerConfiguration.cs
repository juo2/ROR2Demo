using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000081 RID: 129
public class NetworkManagerConfiguration : MonoBehaviour
{
	// Token: 0x0400020B RID: 523
	public new bool DontDestroyOnLoad = true;

	// Token: 0x0400020C RID: 524
	public bool RunInBackground = true;

	// Token: 0x0400020D RID: 525
	public LogFilter.FilterLevel LogLevel = LogFilter.FilterLevel.Info;

	// Token: 0x0400020E RID: 526
	public string OfflineScene;

	// Token: 0x0400020F RID: 527
	public string OnlineScene;

	// Token: 0x04000210 RID: 528
	public GameObject PlayerPrefab;

	// Token: 0x04000211 RID: 529
	public bool AutoCreatePlayer = true;

	// Token: 0x04000212 RID: 530
	public PlayerSpawnMethod PlayerSpawnMethod;

	// Token: 0x04000213 RID: 531
	public List<GameObject> SpawnPrefabs = new List<GameObject>();

	// Token: 0x04000214 RID: 532
	public bool CustomConfig;

	// Token: 0x04000215 RID: 533
	public int MaxConnections = 4;

	// Token: 0x04000216 RID: 534
	public List<QosType> QosChannels = new List<QosType>();
}
