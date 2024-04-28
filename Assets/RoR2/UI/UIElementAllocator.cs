using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DBD RID: 3517
	public class UIElementAllocator<T> where T : Component
	{
		// Token: 0x06005083 RID: 20611 RVA: 0x0014CFF0 File Offset: 0x0014B1F0
		public UIElementAllocator([NotNull] RectTransform containerTransform, [NotNull] GameObject elementPrefab, bool markElementsUnsavable = true, bool acquireExistingChildren = false)
		{
			this.containerTransform = containerTransform;
			this.elementPrefab = elementPrefab;
			this.markElementsUnsavable = markElementsUnsavable;
			this.elementControllerComponentsList = new List<T>();
			this.elements = new ReadOnlyCollection<T>(this.elementControllerComponentsList);
			if (acquireExistingChildren)
			{
				for (int i = 0; i < containerTransform.childCount; i++)
				{
					T component = containerTransform.GetChild(i).GetComponent<T>();
					if (component && component.gameObject.activeInHierarchy)
					{
						this.elementControllerComponentsList.Add(component);
					}
				}
			}
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0014D084 File Offset: 0x0014B284
		private void DestroyElementAt(int i)
		{
			T t = this.elementControllerComponentsList[i];
			UIElementAllocator<T>.ElementOperationDelegate elementOperationDelegate = this.onDestroyElement;
			if (elementOperationDelegate != null)
			{
				elementOperationDelegate(i, t);
			}
			GameObject gameObject = t.gameObject;
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
			this.elementControllerComponentsList.RemoveAt(i);
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0014D0E0 File Offset: 0x0014B2E0
		public void AllocateElements(int desiredCount)
		{
			if (desiredCount < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.containerTransform.gameObject.scene.IsValid())
			{
				return;
			}
			for (int i = this.elementControllerComponentsList.Count - 1; i >= desiredCount; i--)
			{
				this.DestroyElementAt(i);
			}
			for (int j = this.elementControllerComponentsList.Count; j < desiredCount; j++)
			{
				T component = UnityEngine.Object.Instantiate<GameObject>(this.elementPrefab, this.containerTransform).GetComponent<T>();
				this.elementControllerComponentsList.Add(component);
				GameObject gameObject = component.gameObject;
				if (this.markElementsUnsavable)
				{
					gameObject.hideFlags |= (HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild);
				}
				gameObject.SetActive(true);
				UIElementAllocator<T>.ElementOperationDelegate elementOperationDelegate = this.onCreateElement;
				if (elementOperationDelegate != null)
				{
					elementOperationDelegate(j, component);
				}
			}
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0014D1AC File Offset: 0x0014B3AC
		public void MoveElementsToContainerEnd()
		{
			int num = this.containerTransform.childCount - this.elementControllerComponentsList.Count;
			for (int i = this.elementControllerComponentsList.Count - 1; i >= 0; i--)
			{
				this.elementControllerComponentsList[i].transform.SetSiblingIndex(num + i);
			}
		}

		// Token: 0x04004D19 RID: 19737
		public readonly RectTransform containerTransform;

		// Token: 0x04004D1A RID: 19738
		public readonly GameObject elementPrefab;

		// Token: 0x04004D1B RID: 19739
		public readonly bool markElementsUnsavable;

		// Token: 0x04004D1C RID: 19740
		[NotNull]
		private List<T> elementControllerComponentsList;

		// Token: 0x04004D1D RID: 19741
		[NotNull]
		public readonly ReadOnlyCollection<T> elements;

		// Token: 0x04004D1E RID: 19742
		[CanBeNull]
		public UIElementAllocator<T>.ElementOperationDelegate onCreateElement;

		// Token: 0x04004D1F RID: 19743
		[CanBeNull]
		public UIElementAllocator<T>.ElementOperationDelegate onDestroyElement;

		// Token: 0x02000DBE RID: 3518
		// (Invoke) Token: 0x06005088 RID: 20616
		public delegate void ElementOperationDelegate(int index, T element);
	}
}
