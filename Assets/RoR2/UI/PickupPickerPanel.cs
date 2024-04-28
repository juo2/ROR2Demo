using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D61 RID: 3425
	[RequireComponent(typeof(RectTransform))]
	public class PickupPickerPanel : MonoBehaviour
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x00143F33 File Offset: 0x00142133
		// (set) Token: 0x06004E7C RID: 20092 RVA: 0x00143F3B File Offset: 0x0014213B
		public PickupPickerController pickerController { get; set; }

		// Token: 0x06004E7D RID: 20093 RVA: 0x00143F44 File Offset: 0x00142144
		private void Awake()
		{
			this.buttonAllocator = new UIElementAllocator<MPButton>(this.buttonContainer, this.buttonPrefab, true, false);
			this.buttonAllocator.onCreateElement = new UIElementAllocator<MPButton>.ElementOperationDelegate(this.OnCreateButton);
			this.gridlayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			this.gridlayoutGroup.constraintCount = this.maxColumnCount;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00143FA0 File Offset: 0x001421A0
		private void OnCreateButton(int index, MPButton button)
		{
			button.onClick.AddListener(delegate()
			{
				this.pickerController.SubmitChoice(index);
			});
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00143FD8 File Offset: 0x001421D8
		public void SetPickupOptions(PickupPickerController.Option[] options)
		{
			this.buttonAllocator.AllocateElements(options.Length);
			ReadOnlyCollection<MPButton> elements = this.buttonAllocator.elements;
			Sprite sprite = LegacyResourcesAPI.Load<Sprite>("Textures/MiscIcons/texUnlockIcon");
			if (options.Length != 0)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(options[0].pickupIndex);
				Color baseColor = pickupDef.baseColor;
				Color darkColor = pickupDef.darkColor;
				Image[] array = this.coloredImages;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].color *= baseColor;
				}
				array = this.darkColoredImages;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].color *= darkColor;
				}
			}
			for (int j = 0; j < options.Length; j++)
			{
				MPButton mpbutton = elements[j];
				int num = j - j % this.maxColumnCount;
				int num2 = j % this.maxColumnCount;
				int num3 = num2 - this.maxColumnCount;
				int num4 = num2 - 1;
				int num5 = num2 + 1;
				int num6 = num2 + this.maxColumnCount;
				Navigation navigation = mpbutton.navigation;
				navigation.mode = Navigation.Mode.Explicit;
				if (num4 >= 0)
				{
					MPButton selectOnLeft = elements[num + num4];
					navigation.selectOnLeft = selectOnLeft;
				}
				if (num5 < this.maxColumnCount && num + num5 < options.Length)
				{
					MPButton selectOnRight = elements[num + num5];
					navigation.selectOnRight = selectOnRight;
				}
				if (num + num3 >= 0)
				{
					MPButton selectOnUp = elements[num + num3];
					navigation.selectOnUp = selectOnUp;
				}
				if (num + num6 < options.Length)
				{
					MPButton selectOnDown = elements[num + num6];
					navigation.selectOnDown = selectOnDown;
				}
				mpbutton.navigation = navigation;
				int num7 = j;
				PickupDef pickupDef2 = PickupCatalog.GetPickupDef(options[num7].pickupIndex);
				Image component = mpbutton.GetComponent<ChildLocator>().FindChild("Icon").GetComponent<Image>();
				if (options[num7].available)
				{
					component.color = Color.white;
					component.sprite = ((pickupDef2 != null) ? pickupDef2.iconSprite : null);
					mpbutton.interactable = true;
				}
				else
				{
					component.color = Color.gray;
					component.sprite = sprite;
					mpbutton.interactable = false;
				}
			}
		}

		// Token: 0x04004B22 RID: 19234
		public GridLayoutGroup gridlayoutGroup;

		// Token: 0x04004B23 RID: 19235
		public RectTransform buttonContainer;

		// Token: 0x04004B24 RID: 19236
		public GameObject buttonPrefab;

		// Token: 0x04004B25 RID: 19237
		public Image[] coloredImages;

		// Token: 0x04004B26 RID: 19238
		public Image[] darkColoredImages;

		// Token: 0x04004B27 RID: 19239
		public int maxColumnCount;

		// Token: 0x04004B28 RID: 19240
		private UIElementAllocator<MPButton> buttonAllocator;
	}
}
