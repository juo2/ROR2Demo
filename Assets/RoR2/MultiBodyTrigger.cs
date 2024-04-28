using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020007C1 RID: 1985
	public class MultiBodyTrigger : MonoBehaviour
	{
		// Token: 0x06002A14 RID: 10772 RVA: 0x000B5709 File Offset: 0x000B3909
		private void OnEnable()
		{
			this.cachedEnabled = true;
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000B5714 File Offset: 0x000B3914
		private void OnDisable()
		{
			this.cachedEnabled = false;
			this.collisionsQueue.Clear();
			List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			this.SetEncounteredBodies(list, 0);
			list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000B5748 File Offset: 0x000B3948
		private void OnTriggerStay(Collider collider)
		{
			if (this.cachedEnabled)
			{
				this.collisionsQueue.Enqueue(collider);
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000B5760 File Offset: 0x000B3960
		private void FixedUpdate()
		{
			if (this.collisionsQueue.Count == 0 && this.encounteredBodies.Count == 0)
			{
				return;
			}
			List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			while (this.collisionsQueue.Count > 0)
			{
				Collider collider = this.collisionsQueue.Dequeue();
				if (collider)
				{
					CharacterBody component = collider.GetComponent<CharacterBody>();
					if (component && (!this.playerControlledOnly || component.isPlayerControlled) && ListUtils.FirstOccurrenceByReference<List<CharacterBody>, CharacterBody>(list, component) == -1)
					{
						list.Add(component);
					}
				}
			}
			List<CharacterBody> newEncounteredBodies = list;
			int candidateCount;
			if (!this.playerControlledOnly)
			{
				candidateCount = 0;
			}
			else
			{
				Run instance = Run.instance;
				candidateCount = ((instance != null) ? instance.livingPlayerCount : 0);
			}
			this.SetEncounteredBodies(newEncounteredBodies, candidateCount);
			list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000B5810 File Offset: 0x000B3A10
		private void SetEncounteredBodies(List<CharacterBody> newEncounteredBodies, int candidateCount)
		{
			List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			List<CharacterBody> list2 = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			ListUtils.FindExclusiveEntriesByReference<CharacterBody>(this.encounteredBodies, newEncounteredBodies, list2, list);
			bool flag = this.encounteredBodies.Count == 0;
			bool flag2 = newEncounteredBodies.Count == 0;
			ListUtils.CloneTo<CharacterBody>(newEncounteredBodies, this.encounteredBodies);
			foreach (CharacterBody arg in list2)
			{
				try
				{
					CharacterBody.CharacterBodyUnityEvent characterBodyUnityEvent = this.onAnyQualifyingBodyExit;
					if (characterBodyUnityEvent != null)
					{
						characterBodyUnityEvent.Invoke(arg);
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
			if (flag != flag2)
			{
				if (flag2)
				{
					try
					{
						CharacterBody.CharacterBodyUnityEvent characterBodyUnityEvent2 = this.onLastQualifyingBodyExit;
						if (characterBodyUnityEvent2 != null)
						{
							characterBodyUnityEvent2.Invoke(list2[list2.Count - 1]);
						}
						goto IL_D8;
					}
					catch (Exception message2)
					{
						Debug.LogError(message2);
						goto IL_D8;
					}
				}
				try
				{
					CharacterBody.CharacterBodyUnityEvent characterBodyUnityEvent3 = this.onFirstQualifyingBodyEnter;
					if (characterBodyUnityEvent3 != null)
					{
						characterBodyUnityEvent3.Invoke(list[0]);
					}
				}
				catch (Exception message3)
				{
					Debug.LogError(message3);
				}
			}
			IL_D8:
			foreach (CharacterBody arg2 in list)
			{
				try
				{
					CharacterBody.CharacterBodyUnityEvent characterBodyUnityEvent4 = this.onAnyQualifyingBodyEnter;
					if (characterBodyUnityEvent4 != null)
					{
						characterBodyUnityEvent4.Invoke(arg2);
					}
				}
				catch (Exception message4)
				{
					Debug.LogError(message4);
				}
			}
			list2 = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list2);
			list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
			bool flag3 = this.encounteredBodies.Count >= candidateCount && candidateCount > 0;
			if (this.allCandidatesPreviouslyTriggering != flag3)
			{
				try
				{
					if (flag3)
					{
						UnityEvent unityEvent = this.onAllQualifyingBodiesEnter;
						if (unityEvent != null)
						{
							unityEvent.Invoke();
						}
					}
					else
					{
						UnityEvent unityEvent2 = this.onAllQualifyingBodiesExit;
						if (unityEvent2 != null)
						{
							unityEvent2.Invoke();
						}
					}
				}
				catch (Exception message5)
				{
					Debug.LogError(message5);
				}
				this.allCandidatesPreviouslyTriggering = flag3;
			}
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000B5A00 File Offset: 0x000B3C00
		public void KillAllOutsideWithVoidDeath()
		{
			List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
			ListUtils.AddRange<CharacterBody, ReadOnlyCollection<CharacterBody>>(list, CharacterBody.readOnlyInstancesList);
			foreach (CharacterBody characterBody in list)
			{
				if (!this.encounteredBodies.Contains(characterBody))
				{
					CharacterMaster master = characterBody.master;
					if (master)
					{
						try
						{
							master.TrueKill(BodyCatalog.FindBodyPrefab("BrotherBody"), null, DamageType.VoidDeath);
						}
						catch (Exception message)
						{
							Debug.LogError(message);
						}
					}
				}
			}
			list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
		}

		// Token: 0x04002D53 RID: 11603
		[Header("Parameters")]
		public bool playerControlledOnly = true;

		// Token: 0x04002D54 RID: 11604
		[Header("Events")]
		public CharacterBody.CharacterBodyUnityEvent onFirstQualifyingBodyEnter;

		// Token: 0x04002D55 RID: 11605
		public CharacterBody.CharacterBodyUnityEvent onLastQualifyingBodyExit;

		// Token: 0x04002D56 RID: 11606
		public CharacterBody.CharacterBodyUnityEvent onAnyQualifyingBodyEnter;

		// Token: 0x04002D57 RID: 11607
		public CharacterBody.CharacterBodyUnityEvent onAnyQualifyingBodyExit;

		// Token: 0x04002D58 RID: 11608
		public UnityEvent onAllQualifyingBodiesEnter;

		// Token: 0x04002D59 RID: 11609
		public UnityEvent onAllQualifyingBodiesExit;

		// Token: 0x04002D5A RID: 11610
		private readonly Queue<Collider> collisionsQueue = new Queue<Collider>();

		// Token: 0x04002D5B RID: 11611
		private readonly List<CharacterBody> encounteredBodies = new List<CharacterBody>();

		// Token: 0x04002D5C RID: 11612
		private bool allCandidatesPreviouslyTriggering;

		// Token: 0x04002D5D RID: 11613
		private bool cachedEnabled;
	}
}
