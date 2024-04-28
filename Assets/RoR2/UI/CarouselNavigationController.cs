using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CC6 RID: 3270
	public class CarouselNavigationController : MonoBehaviour
	{
		// Token: 0x06004A87 RID: 19079 RVA: 0x00131CF2 File Offset: 0x0012FEF2
		private void Awake()
		{
			this.buttonAllocator = new UIElementAllocator<MPButton>(this.container, this.buttonPrefab, true, false);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00131D10 File Offset: 0x0012FF10
		private void Start()
		{
			if (this.leftButton)
			{
				this.leftButton.onClick.AddListener(new UnityAction(this.NavigatePreviousPage));
			}
			if (this.rightButton)
			{
				this.rightButton.onClick.AddListener(new UnityAction(this.NavigateNextPage));
			}
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x00131D6F File Offset: 0x0012FF6F
		private void OnEnable()
		{
			this.Rebuild();
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x00131D77 File Offset: 0x0012FF77
		public void SetDisplayData(CarouselNavigationController.DisplayData newDisplayData)
		{
			if (newDisplayData.Equals(this.displayData))
			{
				return;
			}
			this.displayData = newDisplayData;
			this.Rebuild();
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x00131D98 File Offset: 0x0012FF98
		private void Rebuild()
		{
			this.buttonAllocator.AllocateElements(this.displayData.pageCount);
			int i = 0;
			int count = this.buttonAllocator.elements.Count;
			while (i < count)
			{
				MPButton mpbutton = this.buttonAllocator.elements[i];
				ColorBlock colors = mpbutton.colors;
				colors.colorMultiplier = 1f;
				mpbutton.colors = colors;
				mpbutton.onClick.RemoveAllListeners();
				CarouselNavigationController.DisplayData buttonDisplayData = new CarouselNavigationController.DisplayData(this.displayData.pageCount, i);
				mpbutton.onClick.AddListener(delegate()
				{
					this.SetDisplayData(buttonDisplayData);
					Action<int> action = this.onPageChangeSubmitted;
					if (action == null)
					{
						return;
					}
					action(this.displayData.pageIndex);
				});
				i++;
			}
			if (this.displayData.pageIndex >= 0 && this.displayData.pageIndex < this.displayData.pageCount)
			{
				MPButton mpbutton2 = this.buttonAllocator.elements[this.displayData.pageIndex];
				ColorBlock colors2 = mpbutton2.colors;
				colors2.colorMultiplier = 2f;
				mpbutton2.colors = colors2;
			}
			bool flag = this.displayData.pageCount > 1;
			bool interactable = flag && (this.allowLooping || this.displayData.pageIndex > 0);
			bool interactable2 = flag && (this.allowLooping || this.displayData.pageIndex < this.displayData.pageCount - 1);
			if (this.leftButton)
			{
				this.leftButton.gameObject.SetActive(flag);
				this.leftButton.interactable = interactable;
			}
			if (this.rightButton)
			{
				this.rightButton.gameObject.SetActive(flag);
				this.rightButton.interactable = interactable2;
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00131F5C File Offset: 0x0013015C
		public void NavigateNextPage()
		{
			if (this.displayData.pageCount <= 0)
			{
				return;
			}
			int num = this.displayData.pageIndex + 1;
			bool flag = num >= this.displayData.pageCount;
			if (flag)
			{
				if (!this.allowLooping)
				{
					return;
				}
				num = 0;
			}
			Debug.LogFormat("NavigateNextPage() desiredPageIndex={0} overflow={1}", new object[]
			{
				num,
				flag
			});
			this.SetDisplayData(new CarouselNavigationController.DisplayData(this.displayData.pageCount, num));
			Action<int> action = this.onPageChangeSubmitted;
			if (action != null)
			{
				action(num);
			}
			if (flag)
			{
				Action action2 = this.onOverflow;
				if (action2 == null)
				{
					return;
				}
				action2();
			}
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x00132008 File Offset: 0x00130208
		public void NavigatePreviousPage()
		{
			if (this.displayData.pageCount <= 0)
			{
				return;
			}
			int num = this.displayData.pageIndex - 1;
			bool flag = num < 0;
			if (flag)
			{
				if (!this.allowLooping)
				{
					return;
				}
				num = this.displayData.pageCount - 1;
			}
			Debug.LogFormat("NavigatePreviousPage() desiredPageIndex={0} underflow={1}", new object[]
			{
				num,
				flag
			});
			this.SetDisplayData(new CarouselNavigationController.DisplayData(this.displayData.pageCount, num));
			Action<int> action = this.onPageChangeSubmitted;
			if (action != null)
			{
				action(num);
			}
			if (flag)
			{
				Action action2 = this.onUnderflow;
				if (action2 == null)
				{
					return;
				}
				action2();
			}
		}

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x06004A8E RID: 19086 RVA: 0x001320B4 File Offset: 0x001302B4
		// (remove) Token: 0x06004A8F RID: 19087 RVA: 0x001320EC File Offset: 0x001302EC
		public event Action<int> onPageChangeSubmitted;

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06004A90 RID: 19088 RVA: 0x00132124 File Offset: 0x00130324
		// (remove) Token: 0x06004A91 RID: 19089 RVA: 0x0013215C File Offset: 0x0013035C
		public event Action onUnderflow;

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x06004A92 RID: 19090 RVA: 0x00132194 File Offset: 0x00130394
		// (remove) Token: 0x06004A93 RID: 19091 RVA: 0x001321CC File Offset: 0x001303CC
		public event Action onOverflow;

		// Token: 0x04004739 RID: 18233
		public GameObject buttonPrefab;

		// Token: 0x0400473A RID: 18234
		public RectTransform container;

		// Token: 0x0400473B RID: 18235
		public MPButton leftButton;

		// Token: 0x0400473C RID: 18236
		public MPButton rightButton;

		// Token: 0x0400473D RID: 18237
		public bool allowLooping;

		// Token: 0x0400473E RID: 18238
		public UIElementAllocator<MPButton> buttonAllocator;

		// Token: 0x0400473F RID: 18239
		private int currentPageIndex;

		// Token: 0x04004740 RID: 18240
		private CarouselNavigationController.DisplayData displayData = new CarouselNavigationController.DisplayData(0, 0);

		// Token: 0x02000CC7 RID: 3271
		public struct DisplayData : IEquatable<CarouselNavigationController.DisplayData>
		{
			// Token: 0x06004A95 RID: 19093 RVA: 0x00132216 File Offset: 0x00130416
			public DisplayData(int pageCount, int pageIndex)
			{
				this.pageCount = pageCount;
				this.pageIndex = pageIndex;
			}

			// Token: 0x06004A96 RID: 19094 RVA: 0x00132226 File Offset: 0x00130426
			public bool Equals(CarouselNavigationController.DisplayData other)
			{
				return this.pageCount == other.pageCount && this.pageIndex == other.pageIndex;
			}

			// Token: 0x06004A97 RID: 19095 RVA: 0x00132248 File Offset: 0x00130448
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is CarouselNavigationController.DisplayData)
				{
					CarouselNavigationController.DisplayData other = (CarouselNavigationController.DisplayData)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06004A98 RID: 19096 RVA: 0x00132274 File Offset: 0x00130474
			public override int GetHashCode()
			{
				return this.pageCount * 397 ^ this.pageIndex;
			}

			// Token: 0x04004744 RID: 18244
			public readonly int pageCount;

			// Token: 0x04004745 RID: 18245
			public readonly int pageIndex;
		}
	}
}
