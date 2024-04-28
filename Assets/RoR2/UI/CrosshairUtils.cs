using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CE6 RID: 3302
	public static class CrosshairUtils
	{
		// Token: 0x06004B48 RID: 19272 RVA: 0x00135534 File Offset: 0x00133734
		public static CrosshairUtils.OverrideRequest RequestOverrideForBody(CharacterBody body, GameObject crosshairPrefab, CrosshairUtils.OverridePriority overridePriority)
		{
			CrosshairUtils.CrosshairOverrideBehavior crosshairOverrideBehavior = body.GetComponent<CrosshairUtils.CrosshairOverrideBehavior>();
			if (!crosshairOverrideBehavior)
			{
				crosshairOverrideBehavior = body.gameObject.AddComponent<CrosshairUtils.CrosshairOverrideBehavior>();
			}
			return crosshairOverrideBehavior.AddRequest(crosshairPrefab, overridePriority);
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x00135564 File Offset: 0x00133764
		public static GameObject GetCrosshairPrefabForBody(CharacterBody body)
		{
			CrosshairUtils.CrosshairOverrideBehavior component = body.GetComponent<CrosshairUtils.CrosshairOverrideBehavior>();
			if (component)
			{
				GameObject overridePrefab = component.GetOverridePrefab();
				if (overridePrefab)
				{
					return overridePrefab;
				}
			}
			return body.defaultCrosshairPrefab;
		}

		// Token: 0x02000CE7 RID: 3303
		public enum OverridePriority
		{
			// Token: 0x040047F9 RID: 18425
			Sprint,
			// Token: 0x040047FA RID: 18426
			Skill,
			// Token: 0x040047FB RID: 18427
			PrioritySkill
		}

		// Token: 0x02000CE8 RID: 3304
		public class OverrideRequest : IDisposable, IComparable<CrosshairUtils.OverrideRequest>
		{
			// Token: 0x06004B4A RID: 19274 RVA: 0x00135597 File Offset: 0x00133797
			public OverrideRequest(GameObject crosshairPrefab, CrosshairUtils.OverridePriority overridePriority, Action<CrosshairUtils.OverrideRequest> onDispose)
			{
				this.disposeCallback = onDispose;
				this.prefab = crosshairPrefab;
				this.priority = overridePriority;
			}

			// Token: 0x06004B4B RID: 19275 RVA: 0x001355B4 File Offset: 0x001337B4
			public int CompareTo(CrosshairUtils.OverrideRequest other)
			{
				return this.priority.CompareTo(other.priority);
			}

			// Token: 0x06004B4C RID: 19276 RVA: 0x001355D2 File Offset: 0x001337D2
			public void Dispose()
			{
				Action<CrosshairUtils.OverrideRequest> action = this.disposeCallback;
				if (action != null)
				{
					action(this);
				}
				this.disposeCallback = null;
			}

			// Token: 0x040047FC RID: 18428
			public readonly GameObject prefab;

			// Token: 0x040047FD RID: 18429
			public readonly CrosshairUtils.OverridePriority priority;

			// Token: 0x040047FE RID: 18430
			private Action<CrosshairUtils.OverrideRequest> disposeCallback;
		}

		// Token: 0x02000CE9 RID: 3305
		private class CrosshairOverrideBehavior : MonoBehaviour
		{
			// Token: 0x06004B4D RID: 19277 RVA: 0x001355ED File Offset: 0x001337ED
			public GameObject GetOverridePrefab()
			{
				if (this.requestList.Count > 0)
				{
					return this.requestList[this.requestList.Count - 1].prefab;
				}
				return null;
			}

			// Token: 0x06004B4E RID: 19278 RVA: 0x0013561C File Offset: 0x0013381C
			public CrosshairUtils.OverrideRequest AddRequest(GameObject crosshairPrefab, CrosshairUtils.OverridePriority overridePriority)
			{
				CrosshairUtils.OverrideRequest overrideRequest = new CrosshairUtils.OverrideRequest(crosshairPrefab, overridePriority, new Action<CrosshairUtils.OverrideRequest>(this.RemoveRequest));
				this.requestList.Add(overrideRequest);
				if (this.requestList.Count > 1)
				{
					this.requestList.Sort();
				}
				return overrideRequest;
			}

			// Token: 0x06004B4F RID: 19279 RVA: 0x00135663 File Offset: 0x00133863
			private void RemoveRequest(CrosshairUtils.OverrideRequest request)
			{
				this.requestList.Remove(request);
			}

			// Token: 0x06004B50 RID: 19280 RVA: 0x00135674 File Offset: 0x00133874
			public void OnDestroy()
			{
				foreach (CrosshairUtils.OverrideRequest overrideRequest in this.requestList)
				{
					overrideRequest.Dispose();
				}
				this.requestList.Clear();
			}

			// Token: 0x040047FF RID: 18431
			private List<CrosshairUtils.OverrideRequest> requestList = new List<CrosshairUtils.OverrideRequest>();
		}
	}
}
