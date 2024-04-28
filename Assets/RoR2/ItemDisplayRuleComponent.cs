using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000787 RID: 1927
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[SelectionBase]
	public class ItemDisplayRuleComponent : MonoBehaviour
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000AF3C5 File Offset: 0x000AD5C5
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x000AF3CD File Offset: 0x000AD5CD
		public ItemDisplayRuleType ruleType
		{
			get
			{
				return this._ruleType;
			}
			set
			{
				this._ruleType = value;
				if (this._ruleType != ItemDisplayRuleType.ParentedPrefab)
				{
					this.prefab = null;
				}
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000AF3E5 File Offset: 0x000AD5E5
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000AF3ED File Offset: 0x000AD5ED
		public GameObject prefab
		{
			get
			{
				return this._prefab;
			}
			set
			{
				if (!this.prefabInstance || this._prefab != value)
				{
					this._prefab = value;
					this.BuildPreview();
				}
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000AF417 File Offset: 0x000AD617
		private void Start()
		{
			this.BuildPreview();
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000AF420 File Offset: 0x000AD620
		public bool SetItemDisplayRule(ItemDisplayRule newItemDisplayRule, ChildLocator childLocator, UnityEngine.Object newKeyAsset)
		{
			this.itemDisplayRule = newItemDisplayRule;
			string childName = this.itemDisplayRule.childName;
			Transform transform = childLocator.FindChild(childName);
			if (!transform)
			{
				Debug.LogWarningFormat(childLocator.gameObject, "Could not fully load item display rules for {0} because child {1} could not be found in the child locator.", new object[]
				{
					childLocator.gameObject.name,
					childName
				});
				return false;
			}
			base.transform.parent = transform;
			this.keyAsset = newKeyAsset;
			this.nameInLocator = this.itemDisplayRule.childName;
			this.ruleType = this.itemDisplayRule.ruleType;
			ItemDisplayRuleType ruleType = this.ruleType;
			if (ruleType != ItemDisplayRuleType.ParentedPrefab)
			{
				if (ruleType == ItemDisplayRuleType.LimbMask)
				{
					this.prefab = null;
					this.limbMask = this.itemDisplayRule.limbMask;
				}
			}
			else
			{
				base.transform.localPosition = this.itemDisplayRule.localPos;
				base.transform.localEulerAngles = this.itemDisplayRule.localAngles;
				base.transform.localScale = this.itemDisplayRule.localScale;
				this.prefab = this.itemDisplayRule.followerPrefab;
				this.limbMask = LimbFlags.None;
			}
			return true;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000AF534 File Offset: 0x000AD734
		private void DestroyPreview()
		{
			if (this.prefabInstance)
			{
				UnityEngine.Object.DestroyImmediate(this.prefabInstance);
			}
			this.prefabInstance = null;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000AF558 File Offset: 0x000AD758
		private void BuildPreview()
		{
			this.DestroyPreview();
			if (this.prefab)
			{
				this.prefabInstance = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
				this.prefabInstance.name = "Preview";
				this.prefabInstance.transform.parent = base.transform;
				this.prefabInstance.transform.localPosition = Vector3.zero;
				this.prefabInstance.transform.localRotation = Quaternion.identity;
				this.prefabInstance.transform.localScale = Vector3.one;
				ItemDisplayRuleComponent.SetPreviewFlags(this.prefabInstance.transform);
			}
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000AF604 File Offset: 0x000AD804
		private static void SetPreviewFlags(Transform transform)
		{
			transform.gameObject.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			foreach (object obj in transform)
			{
				ItemDisplayRuleComponent.SetPreviewFlags((Transform)obj);
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000AF664 File Offset: 0x000AD864
		private void OnDestroy()
		{
			this.DestroyPreview();
		}

		// Token: 0x04002C07 RID: 11271
		public UnityEngine.Object keyAsset;

		// Token: 0x04002C08 RID: 11272
		public LimbFlags limbMask;

		// Token: 0x04002C09 RID: 11273
		[SerializeField]
		[HideInInspector]
		private ItemDisplayRuleType _ruleType;

		// Token: 0x04002C0A RID: 11274
		public string nameInLocator;

		// Token: 0x04002C0B RID: 11275
		[HideInInspector]
		[SerializeField]
		private GameObject _prefab;

		// Token: 0x04002C0C RID: 11276
		private GameObject prefabInstance;

		// Token: 0x04002C0D RID: 11277
		private ItemDisplayRule itemDisplayRule;
	}
}
