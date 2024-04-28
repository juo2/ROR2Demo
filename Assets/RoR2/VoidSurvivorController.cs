using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HG;
using RoR2.HudOverlay;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008F9 RID: 2297
	[RequireComponent(typeof(CharacterBody))]
	[RequireComponent(typeof(InputBankTest))]
	[RequireComponent(typeof(TeamComponent))]
	public class VoidSurvivorController : NetworkBehaviour, IOnTakeDamageServerReceiver, IOnDamageDealtServerReceiver
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000DA42B File Offset: 0x000D862B
		public float corruption
		{
			get
			{
				return this._corruption;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000DA433 File Offset: 0x000D8633
		public float corruptionFraction
		{
			get
			{
				return this.corruption / this.maxCorruption;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000DA442 File Offset: 0x000D8642
		public float corruptionPercentage
		{
			get
			{
				return this.corruptionFraction * 100f;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000DA450 File Offset: 0x000D8650
		public float minimumCorruption
		{
			get
			{
				return this.minimumCorruptionPerVoidItem * (float)this.voidItemCount;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000DA460 File Offset: 0x000D8660
		public bool isFullCorruption
		{
			get
			{
				return this.corruption >= this.maxCorruption;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000DA473 File Offset: 0x000D8673
		public bool isCorrupted
		{
			get
			{
				return this.characterBody && this.characterBody.HasBuff(this.corruptedBuffDef);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000DA495 File Offset: 0x000D8695
		public bool isPermanentlyCorrupted
		{
			get
			{
				return this.minimumCorruption >= this.maxCorruption;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000DA4A8 File Offset: 0x000D86A8
		private HealthComponent bodyHealthComponent
		{
			get
			{
				return this.characterBody.healthComponent;
			}
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000DA4B8 File Offset: 0x000D86B8
		private void OnEnable()
		{
			OverlayCreationParams overlayCreationParams = new OverlayCreationParams
			{
				prefab = this.overlayPrefab,
				childLocatorEntry = this.overlayChildLocatorEntry
			};
			this.overlayController = HudOverlayManager.AddOverlay(base.gameObject, overlayCreationParams);
			this.overlayController.onInstanceAdded += this.OnOverlayInstanceAdded;
			this.overlayController.onInstanceRemove += this.OnOverlayInstanceRemoved;
			if (this.characterBody)
			{
				this.characterBody.onInventoryChanged += this.OnInventoryChanged;
				if (NetworkServer.active)
				{
					HealthComponent.onCharacterHealServer += this.OnCharacterHealServer;
				}
			}
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000DA568 File Offset: 0x000D8768
		private void OnDisable()
		{
			if (this.overlayController != null)
			{
				this.overlayController.onInstanceAdded -= this.OnOverlayInstanceAdded;
				this.overlayController.onInstanceRemove -= this.OnOverlayInstanceRemoved;
				this.fillUiList.Clear();
				HudOverlayManager.RemoveOverlay(this.overlayController);
			}
			if (this.characterBody)
			{
				this.characterBody.onInventoryChanged -= this.OnInventoryChanged;
				if (NetworkServer.active)
				{
					HealthComponent.onCharacterHealServer -= this.OnCharacterHealServer;
				}
			}
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000DA600 File Offset: 0x000D8800
		private void FixedUpdate()
		{
			float num = 0f;
			if (this.characterBody.HasBuff(this.corruptedBuffDef))
			{
				num += this.corruptionFractionPerSecondWhileCorrupted * (this.maxCorruption - this.minimumCorruption);
			}
			else
			{
				num = (this.characterBody.outOfCombat ? this.corruptionPerSecondOutOfCombat : this.corruptionPerSecondInCombat);
			}
			if (NetworkServer.active && !this.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility))
			{
				this.AddCorruption(num * Time.fixedDeltaTime);
			}
			this.UpdateUI();
			if (this.characterAnimator)
			{
				this.characterAnimator.SetFloat("corruptionFraction", this.corruptionFraction);
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000DA6AC File Offset: 0x000D88AC
		private void UpdateUI()
		{
			foreach (ImageFillController imageFillController in this.fillUiList)
			{
				imageFillController.SetTValue(this.corruption / this.maxCorruption);
			}
			if (this.overlayInstanceChildLocator)
			{
				this.overlayInstanceChildLocator.FindChild("CorruptionThreshold").rotation = Quaternion.Euler(0f, 0f, Mathf.InverseLerp(0f, this.maxCorruption, this.corruption) * -360f);
				this.overlayInstanceChildLocator.FindChild("MinCorruptionThreshold").rotation = Quaternion.Euler(0f, 0f, Mathf.InverseLerp(0f, this.maxCorruption, this.minimumCorruption) * -360f);
			}
			if (this.overlayInstanceAnimator)
			{
				this.overlayInstanceAnimator.SetFloat("corruption", this.corruption);
				this.overlayInstanceAnimator.SetBool("isCorrupted", this.isCorrupted);
			}
			if (this.uiCorruptionText)
			{
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				stringBuilder.AppendInt(Mathf.FloorToInt(this.corruption), 1U, 3U).Append("%");
				this.uiCorruptionText.SetText(stringBuilder);
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			}
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000DA818 File Offset: 0x000D8A18
		private void OnOverlayInstanceAdded(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Add(instance.GetComponent<ImageFillController>());
			this.uiCorruptionText = instance.GetComponentInChildren<TextMeshProUGUI>();
			this.overlayInstanceChildLocator = instance.GetComponent<ChildLocator>();
			this.overlayInstanceAnimator = instance.GetComponent<Animator>();
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000DA84F File Offset: 0x000D8A4F
		private void OnOverlayInstanceRemoved(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Remove(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000DA864 File Offset: 0x000D8A64
		private void OnCharacterHealServer(HealthComponent healthComponent, float amount, ProcChainMask procChainMask)
		{
			if (healthComponent == this.bodyHealthComponent && !procChainMask.HasProc(ProcType.VoidSurvivorCrush))
			{
				float num = amount / this.bodyHealthComponent.fullCombinedHealth;
				this.AddCorruption(num * this.corruptionForFullHeal);
			}
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000DA8A1 File Offset: 0x000D8AA1
		public void OnDamageDealtServer(DamageReport damageReport)
		{
			if (damageReport.damageInfo.crit)
			{
				this.AddCorruption(damageReport.damageInfo.procCoefficient * this.corruptionPerCrit);
			}
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000DA8C8 File Offset: 0x000D8AC8
		public void OnTakeDamageServer(DamageReport damageReport)
		{
			float num = damageReport.damageDealt / this.bodyHealthComponent.fullCombinedHealth;
			if (!damageReport.damageInfo.procChainMask.HasProc(ProcType.VoidSurvivorCrush))
			{
				this.AddCorruption(num * this.corruptionForFullDamage);
			}
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000DA90C File Offset: 0x000D8B0C
		private void OnInventoryChanged()
		{
			this.voidItemCount = 0;
			Inventory inventory = this.characterBody.inventory;
			if (inventory)
			{
				this.voidItemCount = inventory.GetTotalItemCountOfTier(ItemTier.VoidTier1) + inventory.GetTotalItemCountOfTier(ItemTier.VoidTier2) + inventory.GetTotalItemCountOfTier(ItemTier.VoidTier3) + inventory.GetTotalItemCountOfTier(ItemTier.VoidBoss);
			}
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000DA95A File Offset: 0x000D8B5A
		[Server]
		public void AddCorruption(float amount)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoidSurvivorController::AddCorruption(System.Single)' called on client");
				return;
			}
			this.Network_corruption = Mathf.Clamp(this.corruption + amount, this.minimumCorruption, this.maxCorruption);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000DA990 File Offset: 0x000D8B90
		private void OnCorruptionModified(float newCorruption)
		{
			if (this.overlayInstanceAnimator && Mathf.Abs(newCorruption - this.corruption) > this.corruptionDeltaThresholdToAnimate)
			{
				this.overlayInstanceAnimator.SetTrigger("corruptionIncreased");
			}
			this.Network_corruption = newCorruption;
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000DA9E0 File Offset: 0x000D8BE0
		// (set) Token: 0x060033E1 RID: 13281 RVA: 0x000DA9F3 File Offset: 0x000D8BF3
		public float Network_corruption
		{
			get
			{
				return this._corruption;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnCorruptionModified(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<float>(value, ref this._corruption, 1U);
			}
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000DAA34 File Offset: 0x000D8C34
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._corruption);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._corruption);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000DAAA0 File Offset: 0x000D8CA0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._corruption = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnCorruptionModified(reader.ReadSingle());
			}
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040034C1 RID: 13505
		[Header("Cached Components")]
		public CharacterBody characterBody;

		// Token: 0x040034C2 RID: 13506
		public Animator characterAnimator;

		// Token: 0x040034C3 RID: 13507
		public EntityStateMachine corruptionModeStateMachine;

		// Token: 0x040034C4 RID: 13508
		public EntityStateMachine bodyStateMachine;

		// Token: 0x040034C5 RID: 13509
		public EntityStateMachine weaponStateMachine;

		// Token: 0x040034C6 RID: 13510
		[Header("Corruption Values")]
		public float maxCorruption;

		// Token: 0x040034C7 RID: 13511
		public float minimumCorruptionPerVoidItem;

		// Token: 0x040034C8 RID: 13512
		public float corruptionPerSecondInCombat;

		// Token: 0x040034C9 RID: 13513
		public float corruptionPerSecondOutOfCombat;

		// Token: 0x040034CA RID: 13514
		public float corruptionForFullDamage;

		// Token: 0x040034CB RID: 13515
		public float corruptionForFullHeal;

		// Token: 0x040034CC RID: 13516
		public float corruptionPerCrit;

		// Token: 0x040034CD RID: 13517
		public float corruptionDeltaThresholdToAnimate;

		// Token: 0x040034CE RID: 13518
		[Header("Corruption Mode")]
		public BuffDef corruptedBuffDef;

		// Token: 0x040034CF RID: 13519
		public float corruptionFractionPerSecondWhileCorrupted;

		// Token: 0x040034D0 RID: 13520
		[Header("UI")]
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x040034D1 RID: 13521
		[SerializeField]
		public string overlayChildLocatorEntry;

		// Token: 0x040034D2 RID: 13522
		private ChildLocator overlayInstanceChildLocator;

		// Token: 0x040034D3 RID: 13523
		private Animator overlayInstanceAnimator;

		// Token: 0x040034D4 RID: 13524
		private OverlayController overlayController;

		// Token: 0x040034D5 RID: 13525
		private List<ImageFillController> fillUiList = new List<ImageFillController>();

		// Token: 0x040034D6 RID: 13526
		private TextMeshProUGUI uiCorruptionText;

		// Token: 0x040034D7 RID: 13527
		private int voidItemCount;

		// Token: 0x040034D8 RID: 13528
		[SyncVar(hook = "OnCorruptionModified")]
		private float _corruption;
	}
}
