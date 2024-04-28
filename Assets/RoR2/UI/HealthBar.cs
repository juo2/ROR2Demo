using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D12 RID: 3346
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(RectTransform))]
	public class HealthBar : MonoBehaviour
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x0013A601 File Offset: 0x00138801
		// (set) Token: 0x06004C46 RID: 19526 RVA: 0x0013A60C File Offset: 0x0013880C
		public HealthComponent source
		{
			get
			{
				return this._source;
			}
			set
			{
				if (this._source != value)
				{
					this.RemoveInventoryChangedHandler();
					this._source = value;
					this.healthFractionVelocity = 0f;
					this.cachedFractionalValue = (this._source ? (this._source.health / this._source.fullCombinedHealth) : 0f);
					this.AddInventoryChangedHandler();
					this.isInventoryCheckDirty = true;
				}
			}
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0013A67D File Offset: 0x0013887D
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.barAllocator = new UIElementAllocator<Image>(this.barContainer, this.style.barPrefab, true, false);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0013A6AE File Offset: 0x001388AE
		private void Start()
		{
			this.UpdateHealthbar(0f);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0013A6BB File Offset: 0x001388BB
		public void Update()
		{
			this.UpdateHealthbar(Time.deltaTime);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0013A6C8 File Offset: 0x001388C8
		private void OnEnable()
		{
			this.AddInventoryChangedHandler();
			this.isInventoryCheckDirty = true;
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0013A6D7 File Offset: 0x001388D7
		private void OnDisable()
		{
			this.RemoveInventoryChangedHandler();
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0013A6E0 File Offset: 0x001388E0
		private void ApplyBars()
		{
			HealthBar.<>c__DisplayClass35_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.i = 0;
			this.barAllocator.AllocateElements(this.barInfoCollection.GetActiveCount());
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.lowHealthUnderBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.trailingUnderHealthbarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.instantHealthbarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.trailingOverHealthbarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.shieldBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.curseBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.barrierBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.flashBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.cullBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.magneticBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.ospBarInfo, ref CS$<>8__locals1);
			this.<ApplyBars>g__HandleBar|35_1(ref this.barInfoCollection.lowHealthOverBarInfo, ref CS$<>8__locals1);
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0013A7F8 File Offset: 0x001389F8
		private void UpdateBarInfos()
		{
			if (!this.source)
			{
				return;
			}
			if (this.isInventoryCheckDirty)
			{
				this.CheckInventory();
			}
			HealthComponent.HealthBarValues healthBarValues = this.source.GetHealthBarValues();
			HealthBar.<>c__DisplayClass38_0 CS$<>8__locals1;
			CS$<>8__locals1.currentBarEnd = 0f;
			float fullCombinedHealth = this.source.fullCombinedHealth;
			this.barInfoCollection.lowHealthUnderBarInfo.enabled = (this.hasLowHealthItem && this.source.isHealthLow);
			this.barInfoCollection.lowHealthUnderBarInfo.normalizedXMin = 0f;
			this.barInfoCollection.lowHealthUnderBarInfo.normalizedXMax = HealthComponent.lowHealthFraction * (1f - healthBarValues.curseFraction);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.lowHealthUnderBarInfo, ref this.style.lowHealthUnderStyle);
			this.cachedFractionalValue = Mathf.SmoothDamp(this.cachedFractionalValue, healthBarValues.healthFraction, ref this.healthFractionVelocity, 0.2f, float.PositiveInfinity, Time.deltaTime);
			ref HealthBar.BarInfo ptr = ref this.barInfoCollection.trailingUnderHealthbarInfo;
			ptr.normalizedXMin = 0f;
			ptr.normalizedXMax = Mathf.Max(this.cachedFractionalValue, healthBarValues.healthFraction);
			ptr.enabled = !ptr.normalizedXMax.Equals(ptr.normalizedXMin);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref ptr, ref this.style.trailingUnderHealthBarStyle);
			this.barInfoCollection.instantHealthbarInfo.enabled = (healthBarValues.healthFraction > 0f);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.instantHealthbarInfo, ref this.style.instantHealthBarStyle);
			HealthBar.<UpdateBarInfos>g__AddBar|38_0(ref this.barInfoCollection.instantHealthbarInfo, healthBarValues.healthFraction, ref CS$<>8__locals1);
			ref HealthBar.BarInfo ptr2 = ref this.barInfoCollection.trailingOverHealthbarInfo;
			ptr2.normalizedXMin = 0f;
			ptr2.normalizedXMax = Mathf.Min(this.cachedFractionalValue + 0.01f, healthBarValues.healthFraction);
			ptr2.enabled = !ptr2.normalizedXMax.Equals(ptr2.normalizedXMin);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref ptr2, ref this.style.trailingOverHealthBarStyle);
			if (healthBarValues.isVoid)
			{
				ptr2.color = HealthBar.voidPanelColor;
			}
			if (healthBarValues.isBoss || healthBarValues.hasInfusion)
			{
				ptr2.color = HealthBar.infusionPanelColor;
			}
			if (this.healthCritical && this.style.flashOnHealthCritical)
			{
				ptr2.color = HealthBar.GetCriticallyHurtColor();
			}
			ref HealthBar.BarInfo ptr3 = ref this.barInfoCollection.shieldBarInfo;
			ptr3.enabled = (healthBarValues.shieldFraction > 0f);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref ptr3, ref this.style.shieldBarStyle);
			if (healthBarValues.hasVoidShields)
			{
				ptr3.color = HealthBar.voidShieldsColor;
			}
			HealthBar.<UpdateBarInfos>g__AddBar|38_0(ref ptr3, healthBarValues.shieldFraction, ref CS$<>8__locals1);
			this.barInfoCollection.curseBarInfo.enabled = (healthBarValues.curseFraction > 0f);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.curseBarInfo, ref this.style.curseBarStyle);
			this.barInfoCollection.curseBarInfo.normalizedXMin = 1f - healthBarValues.curseFraction;
			this.barInfoCollection.curseBarInfo.normalizedXMax = 1f;
			this.barInfoCollection.barrierBarInfo.enabled = (this.source.barrier > 0f);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.barrierBarInfo, ref this.style.barrierBarStyle);
			this.barInfoCollection.barrierBarInfo.normalizedXMin = 0f;
			this.barInfoCollection.barrierBarInfo.normalizedXMax = healthBarValues.barrierFraction;
			this.barInfoCollection.magneticBarInfo.enabled = (this.source.magnetiCharge > 0f);
			this.barInfoCollection.magneticBarInfo.normalizedXMin = 0f;
			this.barInfoCollection.magneticBarInfo.normalizedXMax = healthBarValues.magneticFraction;
			this.barInfoCollection.magneticBarInfo.color = new Color(75f, 0f, 130f);
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.magneticBarInfo, ref this.style.magneticStyle);
			float num = healthBarValues.cullFraction;
			if (healthBarValues.isElite && this.viewerBody)
			{
				num = Mathf.Max(num, this.viewerBody.executeEliteHealthFraction);
			}
			this.barInfoCollection.cullBarInfo.enabled = (num > 0f);
			this.barInfoCollection.cullBarInfo.normalizedXMin = 0f;
			this.barInfoCollection.cullBarInfo.normalizedXMax = num;
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.cullBarInfo, ref this.style.cullBarStyle);
			float ospFraction = healthBarValues.ospFraction;
			this.barInfoCollection.ospBarInfo.enabled = (ospFraction > 0f);
			this.barInfoCollection.ospBarInfo.normalizedXMin = 0f;
			this.barInfoCollection.ospBarInfo.normalizedXMax = ospFraction;
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.ospBarInfo, ref this.style.ospStyle);
			this.barInfoCollection.lowHealthOverBarInfo.enabled = (this.hasLowHealthItem && !this.source.isHealthLow);
			this.barInfoCollection.lowHealthOverBarInfo.normalizedXMin = HealthComponent.lowHealthFraction * (1f - healthBarValues.curseFraction);
			this.barInfoCollection.lowHealthOverBarInfo.normalizedXMax = HealthComponent.lowHealthFraction * (1f - healthBarValues.curseFraction) + 0.005f;
			HealthBar.<UpdateBarInfos>g__ApplyStyle|38_1(ref this.barInfoCollection.lowHealthOverBarInfo, ref this.style.lowHealthOverStyle);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0013AC80 File Offset: 0x00138E80
		private void UpdateHealthbar(float deltaTime)
		{
			float num = 1f;
			if (this.source)
			{
				CharacterBody body = this.source.body;
				bool isElite = body.isElite;
				float fullHealth = this.source.fullHealth;
				if (this.eliteBackdropRectTransform)
				{
					if (isElite)
					{
						num += 1f;
					}
					this.eliteBackdropRectTransform.gameObject.SetActive(isElite);
				}
				if (this.scaleHealthbarWidth && body)
				{
					float x = Util.Remap(Mathf.Clamp((body.baseMaxHealth + body.baseMaxShield) * num, 0f, this.maxHealthbarHealth), this.minHealthbarHealth, this.maxHealthbarHealth, this.minHealthbarWidth, this.maxHealthbarWidth);
					this.rectTransform.sizeDelta = new Vector2(x, this.rectTransform.sizeDelta.y);
				}
				if (this.currentHealthText)
				{
					float num2 = Mathf.Ceil(this.source.combinedHealth);
					if (num2 != this.displayStringCurrentHealth)
					{
						this.displayStringCurrentHealth = num2;
						this.currentHealthText.text = num2.ToString();
					}
				}
				if (this.fullHealthText)
				{
					float num3 = Mathf.Ceil(fullHealth);
					if (num3 != this.displayStringFullHealth)
					{
						this.displayStringFullHealth = num3;
						this.fullHealthText.text = num3.ToString();
					}
				}
				this.healthCritical = (this.source.isHealthLow && this.source.alive);
				if (this.criticallyHurtImage)
				{
					if (this.healthCritical)
					{
						this.criticallyHurtImage.enabled = true;
						this.criticallyHurtImage.color = HealthBar.GetCriticallyHurtColor();
					}
					else
					{
						this.criticallyHurtImage.enabled = false;
					}
				}
				if (this.deadImage)
				{
					this.deadImage.enabled = !this.source.alive;
				}
			}
			this.UpdateBarInfos();
			this.ApplyBars();
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0013AE6C File Offset: 0x0013906C
		private void AddInventoryChangedHandler()
		{
			HealthComponent source = this.source;
			UnityEngine.Object exists;
			if (source == null)
			{
				exists = null;
			}
			else
			{
				CharacterBody body = source.body;
				exists = ((body != null) ? body.inventory : null);
			}
			if (exists)
			{
				this.source.body.inventory.onInventoryChanged += this.OnInventoryChanged;
			}
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0013AEC0 File Offset: 0x001390C0
		private void RemoveInventoryChangedHandler()
		{
			HealthComponent source = this.source;
			UnityEngine.Object exists;
			if (source == null)
			{
				exists = null;
			}
			else
			{
				CharacterBody body = source.body;
				exists = ((body != null) ? body.inventory : null);
			}
			if (exists)
			{
				this.source.body.inventory.onInventoryChanged -= this.OnInventoryChanged;
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0013AF13 File Offset: 0x00139113
		private void OnInventoryChanged()
		{
			this.isInventoryCheckDirty = true;
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0013AF1C File Offset: 0x0013911C
		private void CheckInventory()
		{
			this.isInventoryCheckDirty = false;
			HealthComponent source = this.source;
			Inventory inventory;
			if (source == null)
			{
				inventory = null;
			}
			else
			{
				CharacterBody body = source.body;
				inventory = ((body != null) ? body.inventory : null);
			}
			Inventory inventory2 = inventory;
			if (inventory2)
			{
				this.hasLowHealthItem = false;
				foreach (ItemIndex itemIndex in ItemCatalog.GetItemsWithTag(ItemTag.LowHealth))
				{
					if (inventory2.GetItemCount(itemIndex) > 0)
					{
						this.hasLowHealthItem = true;
						break;
					}
				}
			}
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0013AFB4 File Offset: 0x001391B4
		public static Color GetCriticallyHurtColor()
		{
			if (Mathf.FloorToInt(Time.fixedTime * 10f) % 2 != 0)
			{
				return ColorCatalog.GetColor(ColorCatalog.ColorIndex.Teleporter);
			}
			return Color.white;
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0013B06C File Offset: 0x0013926C
		[CompilerGenerated]
		internal static void <ApplyBars>g__SetRectPosition|35_0(RectTransform rectTransform, float xMin, float xMax, float sizeDelta)
		{
			rectTransform.anchorMin = new Vector2(xMin, 0f);
			rectTransform.anchorMax = new Vector2(xMax, 1f);
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.sizeDelta = new Vector2(sizeDelta * 0.5f + 1f, sizeDelta + 1f);
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0013B0C8 File Offset: 0x001392C8
		[CompilerGenerated]
		private void <ApplyBars>g__HandleBar|35_1(ref HealthBar.BarInfo barInfo, ref HealthBar.<>c__DisplayClass35_0 A_2)
		{
			if (!barInfo.enabled)
			{
				return;
			}
			Image image = this.barAllocator.elements[A_2.i];
			image.type = barInfo.imageType;
			image.sprite = barInfo.sprite;
			image.color = barInfo.color;
			HealthBar.<ApplyBars>g__SetRectPosition|35_0((RectTransform)image.transform, barInfo.normalizedXMin, barInfo.normalizedXMax, barInfo.sizeDelta);
			int i = A_2.i + 1;
			A_2.i = i;
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0013B14C File Offset: 0x0013934C
		[CompilerGenerated]
		internal static void <UpdateBarInfos>g__AddBar|38_0(ref HealthBar.BarInfo barInfo, float fraction, ref HealthBar.<>c__DisplayClass38_0 A_2)
		{
			if (barInfo.enabled)
			{
				barInfo.normalizedXMin = A_2.currentBarEnd;
				A_2.currentBarEnd = (barInfo.normalizedXMax = barInfo.normalizedXMin + fraction);
			}
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0013B184 File Offset: 0x00139384
		[CompilerGenerated]
		internal static void <UpdateBarInfos>g__ApplyStyle|38_1(ref HealthBar.BarInfo barInfo, ref HealthBarStyle.BarStyle barStyle)
		{
			barInfo.enabled &= barStyle.enabled;
			barInfo.color = barStyle.baseColor;
			barInfo.sprite = barStyle.sprite;
			barInfo.imageType = barStyle.imageType;
			barInfo.sizeDelta = barStyle.sizeDelta;
		}

		// Token: 0x04004929 RID: 18729
		private HealthComponent _source;

		// Token: 0x0400492A RID: 18730
		public HealthBarStyle style;

		// Token: 0x0400492B RID: 18731
		[Tooltip("The container rect for the actual bars.")]
		public RectTransform barContainer;

		// Token: 0x0400492C RID: 18732
		public RectTransform eliteBackdropRectTransform;

		// Token: 0x0400492D RID: 18733
		public Image criticallyHurtImage;

		// Token: 0x0400492E RID: 18734
		public Image deadImage;

		// Token: 0x0400492F RID: 18735
		public float maxLastHitTimer = 1f;

		// Token: 0x04004930 RID: 18736
		public bool scaleHealthbarWidth;

		// Token: 0x04004931 RID: 18737
		public float minHealthbarWidth;

		// Token: 0x04004932 RID: 18738
		public float maxHealthbarWidth;

		// Token: 0x04004933 RID: 18739
		public float minHealthbarHealth;

		// Token: 0x04004934 RID: 18740
		public float maxHealthbarHealth;

		// Token: 0x04004935 RID: 18741
		private float displayStringCurrentHealth;

		// Token: 0x04004936 RID: 18742
		private float displayStringFullHealth;

		// Token: 0x04004937 RID: 18743
		private RectTransform rectTransform;

		// Token: 0x04004938 RID: 18744
		private float cachedFractionalValue = 1f;

		// Token: 0x04004939 RID: 18745
		private float healthFractionVelocity;

		// Token: 0x0400493A RID: 18746
		private bool healthCritical;

		// Token: 0x0400493B RID: 18747
		private bool isInventoryCheckDirty = true;

		// Token: 0x0400493C RID: 18748
		private bool hasLowHealthItem;

		// Token: 0x0400493D RID: 18749
		[NonSerialized]
		public CharacterBody viewerBody;

		// Token: 0x0400493E RID: 18750
		private static readonly Color infusionPanelColor = new Color32(231, 84, 58, byte.MaxValue);

		// Token: 0x0400493F RID: 18751
		private static readonly Color voidPanelColor = new Color32(217, 123, byte.MaxValue, byte.MaxValue);

		// Token: 0x04004940 RID: 18752
		private static readonly Color voidShieldsColor = new Color32(byte.MaxValue, 57, 199, byte.MaxValue);

		// Token: 0x04004941 RID: 18753
		private float theta;

		// Token: 0x04004942 RID: 18754
		private UIElementAllocator<Image> barAllocator;

		// Token: 0x04004943 RID: 18755
		private HealthBar.BarInfoCollection barInfoCollection;

		// Token: 0x04004944 RID: 18756
		public TextMeshProUGUI currentHealthText;

		// Token: 0x04004945 RID: 18757
		public TextMeshProUGUI fullHealthText;

		// Token: 0x02000D13 RID: 3347
		private struct BarInfo
		{
			// Token: 0x04004946 RID: 18758
			public bool enabled;

			// Token: 0x04004947 RID: 18759
			public Color color;

			// Token: 0x04004948 RID: 18760
			public Sprite sprite;

			// Token: 0x04004949 RID: 18761
			public Image.Type imageType;

			// Token: 0x0400494A RID: 18762
			public float normalizedXMin;

			// Token: 0x0400494B RID: 18763
			public float normalizedXMax;

			// Token: 0x0400494C RID: 18764
			public float sizeDelta;
		}

		// Token: 0x02000D14 RID: 3348
		private struct BarInfoCollection
		{
			// Token: 0x06004C5A RID: 19546 RVA: 0x0013B1D4 File Offset: 0x001393D4
			public int GetActiveCount()
			{
				HealthBar.BarInfoCollection.<>c__DisplayClass12_0 CS$<>8__locals1;
				CS$<>8__locals1.count = 0;
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.lowHealthUnderBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.trailingUnderHealthbarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.instantHealthbarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.trailingOverHealthbarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.shieldBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.curseBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.barrierBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.flashBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.cullBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.magneticBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.ospBarInfo, ref CS$<>8__locals1);
				HealthBar.BarInfoCollection.<GetActiveCount>g__Check|12_0(ref this.lowHealthOverBarInfo, ref CS$<>8__locals1);
				return CS$<>8__locals1.count;
			}

			// Token: 0x06004C5B RID: 19547 RVA: 0x0013B28B File Offset: 0x0013948B
			[CompilerGenerated]
			internal static void <GetActiveCount>g__Check|12_0(ref HealthBar.BarInfo field, ref HealthBar.BarInfoCollection.<>c__DisplayClass12_0 A_1)
			{
				A_1.count += (field.enabled ? 1 : 0);
			}

			// Token: 0x0400494D RID: 18765
			public HealthBar.BarInfo trailingUnderHealthbarInfo;

			// Token: 0x0400494E RID: 18766
			public HealthBar.BarInfo instantHealthbarInfo;

			// Token: 0x0400494F RID: 18767
			public HealthBar.BarInfo trailingOverHealthbarInfo;

			// Token: 0x04004950 RID: 18768
			public HealthBar.BarInfo shieldBarInfo;

			// Token: 0x04004951 RID: 18769
			public HealthBar.BarInfo curseBarInfo;

			// Token: 0x04004952 RID: 18770
			public HealthBar.BarInfo barrierBarInfo;

			// Token: 0x04004953 RID: 18771
			public HealthBar.BarInfo cullBarInfo;

			// Token: 0x04004954 RID: 18772
			public HealthBar.BarInfo flashBarInfo;

			// Token: 0x04004955 RID: 18773
			public HealthBar.BarInfo magneticBarInfo;

			// Token: 0x04004956 RID: 18774
			public HealthBar.BarInfo ospBarInfo;

			// Token: 0x04004957 RID: 18775
			public HealthBar.BarInfo lowHealthOverBarInfo;

			// Token: 0x04004958 RID: 18776
			public HealthBar.BarInfo lowHealthUnderBarInfo;
		}
	}
}
