using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D74 RID: 3444
	public class RuleBookViewer : MonoBehaviour
	{
		// Token: 0x06004EEA RID: 20202 RVA: 0x00146315 File Offset: 0x00144515
		private void Awake()
		{
			this.cachedRuleBook = new RuleBook();
			this.cachedRuleChoiceMask = new RuleChoiceMask();
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0014632D File Offset: 0x0014452D
		private void Start()
		{
			this.categoryElementAllocator = new UIElementAllocator<RuleCategoryController>(this.categoryContainer, this.categoryPrefab, true, false);
			this.AllocateCategories(RuleCatalog.categoryCount);
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x00146353 File Offset: 0x00144553
		private void Update()
		{
			if (PreGameController.instance)
			{
				this.SetData(PreGameController.instance.resolvedRuleChoiceMask, PreGameController.instance.readOnlyRuleBook);
			}
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0014637B File Offset: 0x0014457B
		private void AllocateCategories(int desiredCount)
		{
			this.categoryElementAllocator.AllocateElements(desiredCount);
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0014638C File Offset: 0x0014458C
		private void SetData(RuleChoiceMask choiceAvailability, RuleBook ruleBook)
		{
			if (choiceAvailability.Equals(this.cachedRuleChoiceMask) && ruleBook.Equals(this.cachedRuleBook))
			{
				return;
			}
			this.cachedRuleChoiceMask.Copy(choiceAvailability);
			this.cachedRuleBook.Copy(ruleBook);
			for (int i = 0; i < RuleCatalog.categoryCount; i++)
			{
				RuleCategoryController ruleCategoryController = this.categoryElementAllocator.elements[i];
				ruleCategoryController.SetData(RuleCatalog.GetCategoryDef(i), this.cachedRuleChoiceMask, this.cachedRuleBook);
				ruleCategoryController.gameObject.SetActive(!ruleCategoryController.shouldHide);
			}
		}

		// Token: 0x04004B94 RID: 19348
		[Tooltip("The prefab to use for categories.")]
		public GameObject categoryPrefab;

		// Token: 0x04004B95 RID: 19349
		public RectTransform categoryContainer;

		// Token: 0x04004B96 RID: 19350
		private UIElementAllocator<RuleCategoryController> categoryElementAllocator;

		// Token: 0x04004B97 RID: 19351
		private RuleChoiceMask cachedRuleChoiceMask;

		// Token: 0x04004B98 RID: 19352
		private RuleBook cachedRuleBook;
	}
}
