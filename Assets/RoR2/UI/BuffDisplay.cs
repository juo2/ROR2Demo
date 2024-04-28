using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CC1 RID: 3265
	[RequireComponent(typeof(RectTransform))]
	public class BuffDisplay : MonoBehaviour
	{
		// Token: 0x06004A6F RID: 19055 RVA: 0x00131795 File Offset: 0x0012F995
		private void Awake()
		{
			this.rectTranform = base.GetComponent<RectTransform>();
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x001317A4 File Offset: 0x0012F9A4
		private void AllocateIcons()
		{
			int num = 0;
			if (this.source)
			{
				foreach (BuffIndex buffType in BuffCatalog.nonHiddenBuffIndices)
				{
					if (this.source.HasBuff(buffType))
					{
						num++;
					}
				}
			}
			if (num != this.buffIcons.Count)
			{
				while (this.buffIcons.Count > num)
				{
					UnityEngine.Object.Destroy(this.buffIcons[this.buffIcons.Count - 1].gameObject);
					this.buffIcons.RemoveAt(this.buffIcons.Count - 1);
				}
				while (this.buffIcons.Count < num)
				{
					BuffIcon component = UnityEngine.Object.Instantiate<GameObject>(this.buffIconPrefab, this.rectTranform).GetComponent<BuffIcon>();
					this.buffIcons.Add(component);
				}
				this.UpdateLayout();
			}
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00131880 File Offset: 0x0012FA80
		private void UpdateLayout()
		{
			this.AllocateIcons();
			float width = this.rectTranform.rect.width;
			if (this.source)
			{
				Vector2 zero = Vector2.zero;
				int num = 0;
				foreach (BuffIndex buffIndex in BuffCatalog.nonHiddenBuffIndices)
				{
					if (this.source.HasBuff(buffIndex))
					{
						BuffIcon buffIcon = this.buffIcons[num];
						buffIcon.buffDef = BuffCatalog.GetBuffDef(buffIndex);
						buffIcon.rectTransform.anchoredPosition = zero;
						buffIcon.buffCount = this.source.GetBuffCount(buffIndex);
						zero.x += this.iconWidth;
						buffIcon.UpdateIcon();
						num++;
					}
				}
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x0013193E File Offset: 0x0012FB3E
		private void Update()
		{
			this.UpdateLayout();
		}

		// Token: 0x04004720 RID: 18208
		private RectTransform rectTranform;

		// Token: 0x04004721 RID: 18209
		public CharacterBody source;

		// Token: 0x04004722 RID: 18210
		public GameObject buffIconPrefab;

		// Token: 0x04004723 RID: 18211
		public float iconWidth = 24f;

		// Token: 0x04004724 RID: 18212
		[SerializeField]
		[HideInInspector]
		private List<BuffIcon> buffIcons;
	}
}
