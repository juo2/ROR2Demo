using System;
using System.Collections;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007F3 RID: 2035
	public class OutsideInteractableLocker : MonoBehaviour
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000BBD71 File Offset: 0x000B9F71
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x000BBD79 File Offset: 0x000B9F79
		public float radius { get; set; }

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000BBD82 File Offset: 0x000B9F82
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.lockObjectMap = new Dictionary<PurchaseInteraction, GameObject>();
				return;
			}
			base.enabled = false;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000BBD9E File Offset: 0x000B9F9E
		private void OnEnable()
		{
			if (NetworkServer.active)
			{
				this.currentCoroutine = this.ChestLockCoroutine();
				this.updateTimer = this.updateInterval;
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000BBDBF File Offset: 0x000B9FBF
		private void OnDisable()
		{
			if (NetworkServer.active)
			{
				this.currentCoroutine = null;
				this.UnlockAll();
			}
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000BBDD5 File Offset: 0x000B9FD5
		private void FixedUpdate()
		{
			this.updateTimer -= Time.fixedDeltaTime;
			if (this.updateTimer <= 0f)
			{
				this.updateTimer = this.updateInterval;
				IEnumerator enumerator = this.currentCoroutine;
				if (enumerator == null)
				{
					return;
				}
				enumerator.MoveNext();
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000BBE13 File Offset: 0x000BA013
		private int CompareCandidatesByDistance(OutsideInteractableLocker.Candidate a, OutsideInteractableLocker.Candidate b)
		{
			return a.distanceSqr.CompareTo(b.distanceSqr);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000BBE28 File Offset: 0x000BA028
		private void LockPurchasable(PurchaseInteraction purchaseInteraction)
		{
			if (purchaseInteraction.lockGameObject)
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.lockPrefab, purchaseInteraction.transform.position, Quaternion.identity);
			NetworkServer.Spawn(gameObject);
			purchaseInteraction.NetworklockGameObject = gameObject;
			this.lockObjectMap.Add(purchaseInteraction, gameObject);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000BBE7C File Offset: 0x000BA07C
		private void UnlockPurchasable(PurchaseInteraction purchaseInteraction)
		{
			GameObject gameObject;
			if (!this.lockObjectMap.TryGetValue(purchaseInteraction, out gameObject))
			{
				return;
			}
			if (gameObject != purchaseInteraction.lockGameObject)
			{
				return;
			}
			UnityEngine.Object.Destroy(gameObject);
			this.lockObjectMap.Remove(purchaseInteraction);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000BBEBC File Offset: 0x000BA0BC
		private void UnlockAll()
		{
			foreach (GameObject obj in this.lockObjectMap.Values)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.lockObjectMap.Clear();
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000BBF1C File Offset: 0x000BA11C
		private IEnumerator ChestLockCoroutine()
		{
			OutsideInteractableLocker.Candidate[] candidates = new OutsideInteractableLocker.Candidate[64];
			int candidatesCount = 0;
			for (;;)
			{
				Vector3 position = base.transform.position;
				List<PurchaseInteraction> instancesList = InstanceTracker.GetInstancesList<PurchaseInteraction>();
				int num = candidatesCount;
				candidatesCount = instancesList.Count;
				ArrayUtils.EnsureCapacity<OutsideInteractableLocker.Candidate>(ref candidates, candidatesCount);
				for (int j = num; j < candidatesCount; j++)
				{
					candidates[j] = default(OutsideInteractableLocker.Candidate);
				}
				for (int k = 0; k < candidatesCount; k++)
				{
					PurchaseInteraction purchaseInteraction = instancesList[k];
					candidates[k] = new OutsideInteractableLocker.Candidate
					{
						purchaseInteraction = purchaseInteraction,
						distanceSqr = (purchaseInteraction.transform.position - position).sqrMagnitude
					};
				}
				yield return null;
				OutsideInteractableLocker.CandidateDistanceCompararer candidateDistanceCompararer = default(OutsideInteractableLocker.CandidateDistanceCompararer);
				Array.Sort<OutsideInteractableLocker.Candidate>(candidates, 0, candidatesCount, candidateDistanceCompararer);
				yield return null;
				int num3;
				for (int i = 0; i < candidatesCount; i = num3)
				{
					PurchaseInteraction purchaseInteraction2 = candidates[i].purchaseInteraction;
					if (purchaseInteraction2)
					{
						float num2 = this.radius * this.radius;
						if (candidates[i].distanceSqr <= num2 != this.lockInside || !purchaseInteraction2.available)
						{
							this.UnlockPurchasable(purchaseInteraction2);
						}
						else
						{
							this.LockPurchasable(purchaseInteraction2);
						}
						yield return null;
					}
					num3 = i + 1;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x04002E58 RID: 11864
		[Tooltip("The networked object which will be instantiated to lock purchasables.")]
		public GameObject lockPrefab;

		// Token: 0x04002E59 RID: 11865
		[Tooltip("How long to wait between steps.")]
		public float updateInterval = 0.1f;

		// Token: 0x04002E5A RID: 11866
		[Tooltip("Whether or not to invert the requirements.")]
		public bool lockInside;

		// Token: 0x04002E5C RID: 11868
		private Dictionary<PurchaseInteraction, GameObject> lockObjectMap;

		// Token: 0x04002E5D RID: 11869
		private float updateTimer;

		// Token: 0x04002E5E RID: 11870
		private IEnumerator currentCoroutine;

		// Token: 0x020007F4 RID: 2036
		private struct Candidate
		{
			// Token: 0x04002E5F RID: 11871
			public PurchaseInteraction purchaseInteraction;

			// Token: 0x04002E60 RID: 11872
			public float distanceSqr;
		}

		// Token: 0x020007F5 RID: 2037
		private struct CandidateDistanceCompararer : IComparer<OutsideInteractableLocker.Candidate>
		{
			// Token: 0x06002BF2 RID: 11250 RVA: 0x000BBE13 File Offset: 0x000BA013
			public int Compare(OutsideInteractableLocker.Candidate a, OutsideInteractableLocker.Candidate b)
			{
				return a.distanceSqr.CompareTo(b.distanceSqr);
			}
		}
	}
}
