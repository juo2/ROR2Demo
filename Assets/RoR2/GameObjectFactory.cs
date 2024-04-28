using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200053A RID: 1338
	[CreateAssetMenu(menuName = "RoR2/GameObjectFactory")]
	public class GameObjectFactory : ScriptableObject
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0006A8BA File Offset: 0x00068ABA
		public void Reset()
		{
			this.Clear();
			this.clearOnPerformInstantiate = true;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0006A8C9 File Offset: 0x00068AC9
		public void Clear()
		{
			this.prefab = null;
			this.spawnPosition = Vector3.zero;
			this.spawnRotation = Quaternion.identity;
			this.isNetworkPrefab = false;
			this.spawnModificationsClip = null;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0006A8F6 File Offset: 0x00068AF6
		public void SetPrefab(GameObject newPrefab)
		{
			this.prefab = newPrefab;
			GameObject gameObject = this.prefab;
			this.isNetworkPrefab = ((gameObject != null) ? gameObject.GetComponent<NetworkIdentity>() : null);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0006A91C File Offset: 0x00068B1C
		public void SetSpawnLocation(Transform newSpawnLocation)
		{
			this.spawnPosition = newSpawnLocation.position;
			this.spawnRotation = newSpawnLocation.rotation;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0006A936 File Offset: 0x00068B36
		public void SetSpawnModificationsClip(AnimationClip newSpawnModificationsClip)
		{
			this.spawnModificationsClip = newSpawnModificationsClip;
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0006A940 File Offset: 0x00068B40
		public void PerformInstantiate()
		{
			if (!this.prefab)
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, this.spawnPosition, this.spawnRotation);
			if (this.spawnModificationsClip)
			{
				UnityEngine.Object obj = gameObject.AddComponent<Animator>();
				this.spawnModificationsClip.SampleAnimation(gameObject, 0f);
				UnityEngine.Object.Destroy(obj);
			}
			gameObject.SetActive(true);
			if (this.isNetworkPrefab && NetworkServer.active)
			{
				NetworkServer.Spawn(gameObject);
			}
			if (this.clearOnPerformInstantiate)
			{
				this.Clear();
			}
		}

		// Token: 0x04001DFE RID: 7678
		public bool clearOnPerformInstantiate = true;

		// Token: 0x04001DFF RID: 7679
		private GameObject prefab;

		// Token: 0x04001E00 RID: 7680
		private Vector3 spawnPosition;

		// Token: 0x04001E01 RID: 7681
		private Quaternion spawnRotation;

		// Token: 0x04001E02 RID: 7682
		private bool isNetworkPrefab;

		// Token: 0x04001E03 RID: 7683
		private AnimationClip spawnModificationsClip;
	}
}
