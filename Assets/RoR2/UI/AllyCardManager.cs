using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using HG;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CB2 RID: 3250
	[RequireComponent(typeof(RectTransform))]
	public class AllyCardManager : MonoBehaviour, ILayoutGroup, ILayoutController
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x001306DA File Offset: 0x0012E8DA
		// (set) Token: 0x06004A2B RID: 18987 RVA: 0x001306E7 File Offset: 0x0012E8E7
		public GameObject sourceGameObject
		{
			get
			{
				return this.currentSource.gameObject;
			}
			set
			{
				if (this.currentSource.gameObject == value)
				{
					return;
				}
				this.currentSource = new AllyCardManager.SourceInfo(value);
				this.OnSourceChanged();
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0013070C File Offset: 0x0012E90C
		private void OnCardCreated(int index, AllyCardController element)
		{
			Vector2 vector = element.rectTransform.anchorMin;
			vector.x = 0f;
			vector.y = 1f;
			element.rectTransform.anchorMin = vector;
			vector = element.rectTransform.anchorMax;
			vector.x = 1f;
			vector.y = 1f;
			element.rectTransform.anchorMax = vector;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x00130779 File Offset: 0x0012E979
		private void OnSourceChanged()
		{
			this.needsRefresh = true;
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00130784 File Offset: 0x0012E984
		private TeamIndex FindTargetTeam()
		{
			TeamIndex result = TeamIndex.None;
			TeamComponent teamComponent = this.currentSource.teamComponent;
			if (teamComponent)
			{
				result = teamComponent.teamIndex;
			}
			return result;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x001307AF File Offset: 0x0012E9AF
		private void SetCharacterData(AllyCardManager.CharacterDataSet newCharacterData)
		{
			if (newCharacterData.Equals(this.currentCharacterData))
			{
				return;
			}
			this.currentCharacterData.CopyFrom(newCharacterData);
			this.BuildFromCharacterData(this.currentCharacterData);
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x001307D8 File Offset: 0x0012E9D8
		private void PopulateCharacterDataSet(AllyCardManager.CharacterDataSet characterDataSet)
		{
			TeamIndex teamIndex = this.FindTargetTeam();
			ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CharacterMaster characterMaster = readOnlyInstancesList[i];
				if (characterMaster.teamIndex == teamIndex)
				{
					CharacterBody body = characterMaster.GetBody();
					if ((!body || !body.teamComponent || !body.teamComponent.hideAllyCardDisplay) && (!characterMaster.playerCharacterMasterController || characterMaster.playerCharacterMasterController.networkUser) && this.currentSource.master != characterMaster)
					{
						AllyCardManager.CharacterData characterData = new AllyCardManager.CharacterData(characterMaster);
						characterDataSet.Add(ref characterData);
					}
				}
			}
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x00130884 File Offset: 0x0012EA84
		private void BuildFromCharacterData(AllyCardManager.CharacterDataSet characterDataSet)
		{
			if (characterDataSet.count < this.displayElementCount)
			{
				Array.Clear(this.displayElements, characterDataSet.count, this.displayElementCount - characterDataSet.count);
			}
			this.displayElementCount = characterDataSet.count;
			ArrayUtils.EnsureCapacity<AllyCardManager.DisplayElement>(ref this.displayElements, this.displayElementCount);
			int i = 0;
			int count = characterDataSet.count;
			while (i < count)
			{
				ref AllyCardManager.CharacterData ptr = ref characterDataSet[i];
				this.displayElements[i] = new AllyCardManager.DisplayElement
				{
					master = ptr.master,
					priority = -1
				};
				i++;
			}
			int num = 0;
			int j = 0;
			int count2 = characterDataSet.count;
			while (j < count2)
			{
				if (characterDataSet[j].isPlayer)
				{
					this.displayElements[j].priority = num;
					num += 2;
				}
				j++;
			}
			int k = 0;
			int count3 = characterDataSet.count;
			while (k < count3)
			{
				if (!characterDataSet[k].isMinion)
				{
					ref AllyCardManager.DisplayElement ptr2 = ref this.displayElements[k];
					if (ptr2.priority == -1)
					{
						ptr2.priority = num;
						num += 2;
					}
				}
				k++;
			}
			int l = 0;
			int count4 = characterDataSet.count;
			while (l < count4)
			{
				ref AllyCardManager.CharacterData ptr3 = ref characterDataSet[l];
				if (ptr3.isMinion)
				{
					ref AllyCardManager.DisplayElement ptr4 = ref this.displayElements[l];
					if (ptr4.priority == -1)
					{
						int num2 = this.<BuildFromCharacterData>g__FindIndexForMaster|22_0(ptr3.leaderMaster);
						if (num2 != -1)
						{
							ptr4.priority = this.displayElements[num2].priority + 1;
							ptr4.shouldIndent = true;
						}
					}
				}
				l++;
			}
			int m = 0;
			int count5 = characterDataSet.count;
			while (m < count5)
			{
				ref AllyCardManager.DisplayElement ptr5 = ref this.displayElements[m];
				if (ptr5.priority == -1)
				{
					ptr5.priority = num;
					num += 2;
				}
				m++;
			}
			AllyCardManager.DisplayElement[] array = this.displayElements;
			int index = 0;
			IComparer<AllyCardManager.DisplayElement> instance = AllyCardManager.DisplayElementComparer.instance;
			Array.Sort<AllyCardManager.DisplayElement>(array, index, this.displayElementCount, instance);
			this.cardAllocator.AllocateElements(this.displayElementCount);
			for (int n = 0; n < this.displayElementCount; n++)
			{
				ref AllyCardManager.DisplayElement ptr6 = ref this.displayElements[n];
				AllyCardController allyCardController = this.cardAllocator.elements[n];
				allyCardController.sourceMaster = ptr6.master;
				allyCardController.shouldIndent = ptr6.shouldIndent;
			}
			ArrayUtils.Clear<AllyCardManager.DisplayElement>(this.displayElements, ref this.displayElementCount);
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00130AF4 File Offset: 0x0012ECF4
		private void Awake()
		{
			this.cardAllocator = new UIElementAllocator<AllyCardController>((RectTransform)base.transform, LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/AllyCard"), true, false);
			this.rectTransform = (RectTransform)base.transform;
			UIElementAllocator<AllyCardController> uielementAllocator = this.cardAllocator;
			uielementAllocator.onCreateElement = (UIElementAllocator<AllyCardController>.ElementOperationDelegate)Delegate.Combine(uielementAllocator.onCreateElement, new UIElementAllocator<AllyCardController>.ElementOperationDelegate(this.OnCardCreated));
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x00130B5C File Offset: 0x0012ED5C
		private void FixedUpdate()
		{
			AllyCardManager.CharacterDataSet characterDataSet = AllyCardManager.CharacterDataSetPool.Request();
			this.PopulateCharacterDataSet(characterDataSet);
			this.SetCharacterData(characterDataSet);
			AllyCardManager.CharacterDataSetPool.Return(ref characterDataSet);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x00130B84 File Offset: 0x0012ED84
		private void OnEnable()
		{
			this.needsRefresh = true;
			this.currentCharacterData = AllyCardManager.CharacterDataSetPool.Request();
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x00130B98 File Offset: 0x0012ED98
		private void OnDisable()
		{
			AllyCardManager.CharacterDataSetPool.Return(ref this.currentCharacterData);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00130BA8 File Offset: 0x0012EDA8
		public void SetLayoutHorizontal()
		{
			if (this.cardAllocator == null)
			{
				return;
			}
			ReadOnlyCollection<AllyCardController> elements = this.cardAllocator.elements;
			int i = 0;
			int count = elements.Count;
			while (i < count)
			{
				AllyCardController allyCardController = elements[i];
				RectTransform rectTransform = allyCardController.rectTransform;
				Vector2 vector = rectTransform.offsetMin;
				vector.x = (allyCardController.shouldIndent ? this.indentWidth : 0f);
				rectTransform.offsetMin = vector;
				vector = rectTransform.offsetMax;
				vector.x = 0f;
				rectTransform.offsetMax = vector;
				i++;
			}
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x00130C34 File Offset: 0x0012EE34
		public void SetLayoutVertical()
		{
			if (this.cardAllocator == null)
			{
				return;
			}
			ReadOnlyCollection<AllyCardController> elements = this.cardAllocator.elements;
			float b = this.rectTransform.rect.height / (float)elements.Count;
			float num = 0f;
			int i = 0;
			int count = elements.Count;
			while (i < count)
			{
				AllyCardController allyCardController = elements[i];
				RectTransform rectTransform = allyCardController.rectTransform;
				float preferredHeight = allyCardController.layoutElement.preferredHeight;
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
				Vector2 anchoredPosition = rectTransform.anchoredPosition;
				anchoredPosition.y = num;
				rectTransform.anchoredPosition = anchoredPosition;
				num -= Mathf.Min(preferredHeight, b);
				i++;
			}
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x00130CFC File Offset: 0x0012EEFC
		[CompilerGenerated]
		private int <BuildFromCharacterData>g__FindIndexForMaster|22_0(CharacterMaster master)
		{
			for (int i = 0; i < this.displayElementCount; i++)
			{
				if (master == this.displayElements[i].master)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040046E2 RID: 18146
		public float indentWidth = 16f;

		// Token: 0x040046E3 RID: 18147
		private AllyCardManager.SourceInfo currentSource;

		// Token: 0x040046E4 RID: 18148
		private UIElementAllocator<AllyCardController> cardAllocator;

		// Token: 0x040046E5 RID: 18149
		private RectTransform rectTransform;

		// Token: 0x040046E6 RID: 18150
		private bool needsRefresh;

		// Token: 0x040046E7 RID: 18151
		private AllyCardManager.DisplayElement[] displayElements = Array.Empty<AllyCardManager.DisplayElement>();

		// Token: 0x040046E8 RID: 18152
		private int displayElementCount;

		// Token: 0x040046E9 RID: 18153
		private AllyCardManager.CharacterDataSet currentCharacterData;

		// Token: 0x02000CB3 RID: 3251
		private struct SourceInfo
		{
			// Token: 0x06004A3A RID: 19002 RVA: 0x00130D34 File Offset: 0x0012EF34
			public SourceInfo(GameObject gameObject)
			{
				bool flag = gameObject;
				this.gameObject = gameObject;
				this.teamComponent = (flag ? gameObject.GetComponent<TeamComponent>() : null);
				CharacterBody characterBody = flag ? gameObject.GetComponent<CharacterBody>() : null;
				this.master = (characterBody ? characterBody.master : null);
			}

			// Token: 0x040046EA RID: 18154
			public readonly GameObject gameObject;

			// Token: 0x040046EB RID: 18155
			public readonly TeamComponent teamComponent;

			// Token: 0x040046EC RID: 18156
			public readonly CharacterMaster master;
		}

		// Token: 0x02000CB4 RID: 3252
		private struct DisplayElement
		{
			// Token: 0x040046ED RID: 18157
			public CharacterMaster master;

			// Token: 0x040046EE RID: 18158
			public bool shouldIndent;

			// Token: 0x040046EF RID: 18159
			public int priority;
		}

		// Token: 0x02000CB5 RID: 3253
		private class DisplayElementComparer : IComparer<AllyCardManager.DisplayElement>
		{
			// Token: 0x06004A3B RID: 19003 RVA: 0x00130D88 File Offset: 0x0012EF88
			public int Compare(AllyCardManager.DisplayElement a, AllyCardManager.DisplayElement b)
			{
				int num = a.priority - b.priority;
				if (num == 0)
				{
					num = (int)(a.master.netId.Value - b.master.netId.Value);
				}
				return num;
			}

			// Token: 0x06004A3C RID: 19004 RVA: 0x00004479 File Offset: 0x00002679
			private DisplayElementComparer()
			{
			}

			// Token: 0x040046F0 RID: 18160
			public static AllyCardManager.DisplayElementComparer instance = new AllyCardManager.DisplayElementComparer();
		}

		// Token: 0x02000CB6 RID: 3254
		private struct CharacterData
		{
			// Token: 0x06004A3E RID: 19006 RVA: 0x00130DDC File Offset: 0x0012EFDC
			public CharacterData(CharacterMaster master)
			{
				this.master = master;
				this.leaderMaster = master.minionOwnership.ownerMaster;
				this.isMinion = this.leaderMaster;
				this.isPlayer = master.playerCharacterMasterController;
				this.masterInstanceId = master.gameObject.GetInstanceID();
				this.leaderMasterInstanceId = (this.isMinion ? this.leaderMaster.gameObject.GetInstanceID() : 0);
			}

			// Token: 0x06004A3F RID: 19007 RVA: 0x00130E55 File Offset: 0x0012F055
			public bool Equals(in AllyCardManager.CharacterData other)
			{
				return this.masterInstanceId == other.masterInstanceId && this.leaderMasterInstanceId == other.leaderMasterInstanceId && this.isMinion == other.isMinion && this.isPlayer == other.isPlayer;
			}

			// Token: 0x040046F1 RID: 18161
			public readonly CharacterMaster master;

			// Token: 0x040046F2 RID: 18162
			public readonly CharacterMaster leaderMaster;

			// Token: 0x040046F3 RID: 18163
			private readonly int masterInstanceId;

			// Token: 0x040046F4 RID: 18164
			private readonly int leaderMasterInstanceId;

			// Token: 0x040046F5 RID: 18165
			public readonly bool isMinion;

			// Token: 0x040046F6 RID: 18166
			public readonly bool isPlayer;
		}

		// Token: 0x02000CB7 RID: 3255
		private class CharacterDataSet
		{
			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06004A40 RID: 19008 RVA: 0x00130E91 File Offset: 0x0012F091
			public int count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x06004A41 RID: 19009 RVA: 0x00130E9C File Offset: 0x0012F09C
			public bool Equals(AllyCardManager.CharacterDataSet other)
			{
				if (other == null)
				{
					return false;
				}
				if (this._count != other._count)
				{
					return false;
				}
				for (int i = 0; i < this._count; i++)
				{
					if (!this.buffer[i].Equals(other.buffer[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06004A42 RID: 19010 RVA: 0x00130EF1 File Offset: 0x0012F0F1
			public void Clear()
			{
				Array.Clear(this.buffer, 0, this._count);
				this._count = 0;
			}

			// Token: 0x06004A43 RID: 19011 RVA: 0x00130F0C File Offset: 0x0012F10C
			public void Add(ref AllyCardManager.CharacterData element)
			{
				ArrayUtils.ArrayAppend<AllyCardManager.CharacterData>(ref this.buffer, ref this._count, element);
			}

			// Token: 0x170006C5 RID: 1733
			public AllyCardManager.CharacterData this[int i]
			{
				get
				{
					return ref this.buffer[i];
				}
			}

			// Token: 0x06004A45 RID: 19013 RVA: 0x00130F30 File Offset: 0x0012F130
			public void CopyFrom(AllyCardManager.CharacterDataSet src)
			{
				int num = this._count - src._count;
				if (num > 0)
				{
					Array.Clear(this.buffer, src._count, num);
				}
				ArrayUtils.EnsureCapacity<AllyCardManager.CharacterData>(ref this.buffer, src.buffer.Length);
				this._count = src.count;
				Array.Copy(src.buffer, this.buffer, this._count);
			}

			// Token: 0x040046F7 RID: 18167
			private AllyCardManager.CharacterData[] buffer = new AllyCardManager.CharacterData[128];

			// Token: 0x040046F8 RID: 18168
			private int _count;
		}

		// Token: 0x02000CB8 RID: 3256
		private static class CharacterDataSetPool
		{
			// Token: 0x06004A47 RID: 19015 RVA: 0x00130FAF File Offset: 0x0012F1AF
			public static AllyCardManager.CharacterDataSet Request()
			{
				if (AllyCardManager.CharacterDataSetPool.pool.Count == 0)
				{
					return new AllyCardManager.CharacterDataSet();
				}
				return AllyCardManager.CharacterDataSetPool.pool.Pop();
			}

			// Token: 0x06004A48 RID: 19016 RVA: 0x00130FCD File Offset: 0x0012F1CD
			public static void Return(ref AllyCardManager.CharacterDataSet characterDataSet)
			{
				characterDataSet.Clear();
				AllyCardManager.CharacterDataSetPool.pool.Push(characterDataSet);
			}

			// Token: 0x040046F9 RID: 18169
			private static readonly Stack<AllyCardManager.CharacterDataSet> pool = new Stack<AllyCardManager.CharacterDataSet>();
		}
	}
}
