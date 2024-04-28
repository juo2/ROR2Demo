using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D75 RID: 3445
	public class RuleBookViewerStrip : MonoBehaviour
	{
		// Token: 0x06004EF0 RID: 20208 RVA: 0x0014641C File Offset: 0x0014461C
		private RuleChoiceController CreateChoice()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.choicePrefab, this.choiceContainer);
			gameObject.SetActive(true);
			RuleChoiceController component = gameObject.GetComponent<RuleChoiceController>();
			component.strip = this;
			return component;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00146442 File Offset: 0x00144642
		private void DestroyChoice(RuleChoiceController choiceController)
		{
			UnityEngine.Object.Destroy(choiceController.gameObject);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x00146450 File Offset: 0x00144650
		public void SetData(List<RuleChoiceDef> newChoices, int choiceIndex)
		{
			this.AllocateChoices(newChoices.Count);
			int num = this.currentDisplayChoiceIndex;
			int count = newChoices.Count;
			bool canVote = count > 1;
			for (int i = 0; i < count; i++)
			{
				this.choiceControllers[i].canVote = canVote;
				this.choiceControllers[i].SetChoice(newChoices[i]);
				if (newChoices[i].localIndex == choiceIndex)
				{
					num = i;
				}
			}
			this.currentDisplayChoiceIndex = num;
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x001464CC File Offset: 0x001446CC
		private void AllocateChoices(int desiredCount)
		{
			while (this.choiceControllers.Count > desiredCount)
			{
				int index = this.choiceControllers.Count - 1;
				this.DestroyChoice(this.choiceControllers[index]);
				this.choiceControllers.RemoveAt(index);
			}
			while (this.choiceControllers.Count < desiredCount)
			{
				this.choiceControllers.Add(this.CreateChoice());
			}
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x00146538 File Offset: 0x00144738
		public void Update()
		{
			if (this.choiceControllers.Count == 0)
			{
				return;
			}
			if (this.currentDisplayChoiceIndex >= this.choiceControllers.Count)
			{
				this.currentDisplayChoiceIndex = this.choiceControllers.Count - 1;
			}
			Vector3 localPosition = this.choiceControllers[this.currentDisplayChoiceIndex].transform.localPosition;
			float target = 0f;
			RectTransform.Axis axis = this.movementAxis;
			if (axis != RectTransform.Axis.Horizontal)
			{
				if (axis == RectTransform.Axis.Vertical)
				{
					target = -localPosition.y;
				}
			}
			else
			{
				target = -localPosition.x;
			}
			this.currentPosition = Mathf.SmoothDamp(this.currentPosition, target, ref this.velocity, this.movementDuration);
			this.UpdatePosition();
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x001465E1 File Offset: 0x001447E1
		private void OnEnable()
		{
			this.UpdatePosition();
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x001465EC File Offset: 0x001447EC
		private void UpdatePosition()
		{
			Vector3 localPosition = this.choiceContainer.localPosition;
			RectTransform.Axis axis = this.movementAxis;
			if (axis != RectTransform.Axis.Horizontal)
			{
				if (axis == RectTransform.Axis.Vertical)
				{
					localPosition.y = this.currentPosition;
				}
			}
			else
			{
				localPosition.x = this.currentPosition;
			}
			this.choiceContainer.localPosition = localPosition;
		}

		// Token: 0x04004B99 RID: 19353
		public GameObject choicePrefab;

		// Token: 0x04004B9A RID: 19354
		public RectTransform choiceContainer;

		// Token: 0x04004B9B RID: 19355
		public RectTransform.Axis movementAxis = RectTransform.Axis.Vertical;

		// Token: 0x04004B9C RID: 19356
		public float movementDuration = 0.1f;

		// Token: 0x04004B9D RID: 19357
		public readonly List<RuleChoiceController> choiceControllers = new List<RuleChoiceController>();

		// Token: 0x04004B9E RID: 19358
		public int currentDisplayChoiceIndex;

		// Token: 0x04004B9F RID: 19359
		private float velocity;

		// Token: 0x04004BA0 RID: 19360
		private float currentPosition;
	}
}
