using System;
using System.Collections.Generic;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007A1 RID: 1953
	[RequireComponent(typeof(CharacterBody))]
	public class MageCalibrationController : MonoBehaviour
	{
		// Token: 0x06002941 RID: 10561 RVA: 0x000B28C6 File Offset: 0x000B0AC6
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
			this.characterBody.onInventoryChanged += this.OnInventoryChanged;
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000B28FC File Offset: 0x000B0AFC
		private void Start()
		{
			this.currentElement = this.GetAwardedElementFromInventory();
			this.RefreshCalibrationElement(this.currentElement);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000B2916 File Offset: 0x000B0B16
		private void OnDestroy()
		{
			this.characterBody.onInventoryChanged -= this.OnInventoryChanged;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000B292F File Offset: 0x000B0B2F
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x000B2937 File Offset: 0x000B0B37
		private MageElement currentElement
		{
			get
			{
				return this._currentElement;
			}
			set
			{
				if (value == this._currentElement)
				{
					return;
				}
				this._currentElement = value;
				this.RefreshCalibrationElement(this._currentElement);
			}
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000B2956 File Offset: 0x000B0B56
		private void OnInventoryChanged()
		{
			base.enabled = true;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000B2960 File Offset: 0x000B0B60
		private void FixedUpdate()
		{
			base.enabled = false;
			this.currentElement = this.GetAwardedElementFromInventory();
			if (this.hasEffectiveAuthority && this.currentElement == MageElement.None)
			{
				MageElement mageElement = this.CalcElementToAward();
				if (mageElement != MageElement.None && !(this.stateMachine.state is MageCalibrate))
				{
					MageCalibrate mageCalibrate = new MageCalibrate();
					mageCalibrate.element = mageElement;
					this.stateMachine.SetInterruptState(mageCalibrate, this.calibrationStateInterruptPriority);
				}
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000B29CC File Offset: 0x000B0BCC
		private MageElement GetAwardedElementFromInventory()
		{
			Inventory inventory = this.characterBody.inventory;
			if (inventory)
			{
				MageElement mageElement = (MageElement)inventory.GetItemCount(JunkContent.Items.MageAttunement);
				if (mageElement >= MageElement.None && mageElement < MageElement.Count)
				{
					return mageElement;
				}
			}
			return MageElement.None;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000B2A08 File Offset: 0x000B0C08
		private MageElement CalcElementToAward()
		{
			for (int i = 0; i < MageCalibrationController.elementCounter.Length; i++)
			{
				MageCalibrationController.elementCounter[i] = 0;
			}
			Inventory inventory = this.characterBody.inventory;
			if (!inventory)
			{
				return MageElement.None;
			}
			List<ItemIndex> itemAcquisitionOrder = inventory.itemAcquisitionOrder;
			for (int j = 0; j < itemAcquisitionOrder.Count; j++)
			{
				ItemCatalog.GetItemDef(itemAcquisitionOrder[j]);
			}
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(inventory.currentEquipmentIndex);
			if (equipmentDef)
			{
				MageElement mageElement = equipmentDef.mageElement;
				if (mageElement != MageElement.None)
				{
					MageCalibrationController.elementCounter[(int)mageElement] += 2;
				}
			}
			MageElement result = MageElement.None;
			int num = 0;
			for (MageElement mageElement2 = MageElement.Fire; mageElement2 < MageElement.Count; mageElement2 += 1)
			{
				int num2 = MageCalibrationController.elementCounter[(int)mageElement2];
				if (num2 > num)
				{
					result = mageElement2;
					num = num2;
				}
			}
			if (num >= 5)
			{
				return result;
			}
			return MageElement.None;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000B2ADA File Offset: 0x000B0CDA
		public MageElement GetActiveCalibrationElement()
		{
			return this.currentElement;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000B2AE4 File Offset: 0x000B0CE4
		public void SetElement(MageElement newElement)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			Inventory inventory = this.characterBody.inventory;
			if (inventory)
			{
				MageElement mageElement = (MageElement)inventory.GetItemCount(JunkContent.Items.MageAttunement);
				if (mageElement != newElement)
				{
					int count = (int)(newElement - mageElement);
					inventory.GiveItem(JunkContent.Items.MageAttunement, count);
				}
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000B2B30 File Offset: 0x000B0D30
		public void RefreshCalibrationElement(MageElement targetElement)
		{
			MageCalibrationController.CalibrationInfo calibrationInfo = this.calibrationInfos[(int)targetElement];
			this.calibrationOverlayRenderer.enabled = calibrationInfo.enableCalibrationOverlay;
			this.calibrationOverlayRenderer.material = calibrationInfo.calibrationOverlayMaterial;
		}

		// Token: 0x04002CAA RID: 11434
		public MageCalibrationController.CalibrationInfo[] calibrationInfos;

		// Token: 0x04002CAB RID: 11435
		public SkinnedMeshRenderer calibrationOverlayRenderer;

		// Token: 0x04002CAC RID: 11436
		[Tooltip("The state machine upon which to perform the calibration state.")]
		public EntityStateMachine stateMachine;

		// Token: 0x04002CAD RID: 11437
		[Tooltip("The priority with which the calibration state will try to interrupt the current state.")]
		public InterruptPriority calibrationStateInterruptPriority;

		// Token: 0x04002CAE RID: 11438
		private CharacterBody characterBody;

		// Token: 0x04002CAF RID: 11439
		private bool hasEffectiveAuthority;

		// Token: 0x04002CB0 RID: 11440
		private MageElement _currentElement;

		// Token: 0x04002CB1 RID: 11441
		private static readonly int[] elementCounter = new int[4];

		// Token: 0x020007A2 RID: 1954
		[Serializable]
		public struct CalibrationInfo
		{
			// Token: 0x04002CB2 RID: 11442
			public bool enableCalibrationOverlay;

			// Token: 0x04002CB3 RID: 11443
			public Material calibrationOverlayMaterial;
		}
	}
}
