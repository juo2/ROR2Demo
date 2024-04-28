using System;
using HG;
using RoR2.Networking;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x0200089B RID: 2203
	[RequireComponent(typeof(NetworkIdentity))]
	public class SkillLocator : NetworkBehaviour
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000CECF8 File Offset: 0x000CCEF8
		public GenericSkill primaryBonusStockSkill
		{
			get
			{
				if (!this.primaryBonusStockOverrideSkill)
				{
					return this.primary;
				}
				return this.primaryBonusStockOverrideSkill;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000CED14 File Offset: 0x000CCF14
		public GenericSkill secondaryBonusStockSkill
		{
			get
			{
				if (!this.secondaryBonusStockOverrideSkill)
				{
					return this.secondary;
				}
				return this.secondaryBonusStockOverrideSkill;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000CED30 File Offset: 0x000CCF30
		public GenericSkill utilityBonusStockSkill
		{
			get
			{
				if (!this.utilityBonusStockOverrideSkill)
				{
					return this.utility;
				}
				return this.utilityBonusStockOverrideSkill;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000CED4C File Offset: 0x000CCF4C
		public GenericSkill specialBonusStockSkill
		{
			get
			{
				if (!this.specialBonusStockOverrideSkill)
				{
					return this.special;
				}
				return this.specialBonusStockOverrideSkill;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000CED68 File Offset: 0x000CCF68
		public int skillSlotCount
		{
			get
			{
				return this.allSkills.Length;
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000CED72 File Offset: 0x000CCF72
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.allSkills = base.GetComponents<GenericSkill>();
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000CED8C File Offset: 0x000CCF8C
		private void Start()
		{
			this.UpdateAuthority();
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000CED94 File Offset: 0x000CCF94
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000CEDA2 File Offset: 0x000CCFA2
		public override void OnStopAuthority()
		{
			base.OnStopAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000CEDB0 File Offset: 0x000CCFB0
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.networkIdentity);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000CEDC4 File Offset: 0x000CCFC4
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				for (int i = 0; i < this.allSkills.Length; i++)
				{
					GenericSkill genericSkill = this.allSkills[i];
					if (genericSkill.baseSkill != genericSkill.defaultSkillDef)
					{
						num |= 1U << i;
					}
				}
			}
			writer.WritePackedUInt32(num);
			for (int j = 0; j < this.allSkills.Length; j++)
			{
				if ((num & 1U << j) != 0U)
				{
					GenericSkill genericSkill2 = this.allSkills[j];
					SkillDef baseSkill = genericSkill2.baseSkill;
					writer.WritePackedUInt32((uint)(((baseSkill != null) ? baseSkill.skillIndex : -1) + 1));
				}
			}
			return num > 0U;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000CEE60 File Offset: 0x000CD060
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			this.inDeserialize = true;
			uint num = reader.ReadPackedUInt32();
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				if ((num & 1U << i) != 0U)
				{
					GenericSkill genericSkill = this.allSkills[i];
					SkillDef skillDef = SkillCatalog.GetSkillDef((int)(reader.ReadPackedUInt32() - 1U));
					if (initialState || !this.hasEffectiveAuthority)
					{
						genericSkill.SetBaseSkill(skillDef);
					}
				}
			}
			this.inDeserialize = false;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000CEEC8 File Offset: 0x000CD0C8
		public GenericSkill FindSkill(string skillName)
		{
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				if (this.allSkills[i].skillName == skillName)
				{
					return this.allSkills[i];
				}
			}
			return null;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000CEF08 File Offset: 0x000CD108
		public GenericSkill FindSkillByFamilyName(string skillFamilyName)
		{
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				if (SkillCatalog.GetSkillFamilyName(this.allSkills[i].skillFamily.catalogIndex) == skillFamilyName)
				{
					return this.allSkills[i];
				}
			}
			return null;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000CEF54 File Offset: 0x000CD154
		public GenericSkill FindSkillByDef(SkillDef skillDef)
		{
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				if (this.allSkills[i].skillDef == skillDef)
				{
					return this.allSkills[i];
				}
			}
			return null;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000CEF8E File Offset: 0x000CD18E
		public GenericSkill GetSkill(SkillSlot skillSlot)
		{
			switch (skillSlot)
			{
			case SkillSlot.Primary:
				return this.primary;
			case SkillSlot.Secondary:
				return this.secondary;
			case SkillSlot.Utility:
				return this.utility;
			case SkillSlot.Special:
				return this.special;
			default:
				return null;
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000CEFC5 File Offset: 0x000CD1C5
		public GenericSkill GetSkillAtIndex(int index)
		{
			return ArrayUtils.GetSafe<GenericSkill>(this.allSkills, index);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000CEFD3 File Offset: 0x000CD1D3
		public int GetSkillSlotIndex(GenericSkill skillSlot)
		{
			return Array.IndexOf<GenericSkill>(this.allSkills, skillSlot);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000CEFE4 File Offset: 0x000CD1E4
		public SkillSlot FindSkillSlot(GenericSkill skillComponent)
		{
			if (!skillComponent)
			{
				return SkillSlot.None;
			}
			if (skillComponent == this.primary)
			{
				return SkillSlot.Primary;
			}
			if (skillComponent == this.secondary)
			{
				return SkillSlot.Secondary;
			}
			if (skillComponent == this.utility)
			{
				return SkillSlot.Utility;
			}
			if (skillComponent == this.special)
			{
				return SkillSlot.Special;
			}
			return SkillSlot.None;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000CF03C File Offset: 0x000CD23C
		[Server]
		public void ApplyLoadoutServer(Loadout loadout, BodyIndex bodyIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SkillLocator::ApplyLoadoutServer(RoR2.Loadout,RoR2.BodyIndex)' called on client");
				return;
			}
			if (bodyIndex == BodyIndex.None)
			{
				return;
			}
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				uint num = loadout.bodyLoadoutManager.GetSkillVariant(bodyIndex, i);
				GenericSkill genericSkill = this.allSkills[i];
				SkillFamily.Variant[] variants = genericSkill.skillFamily.variants;
				if (!ArrayUtils.IsInBounds<SkillFamily.Variant>(variants, num))
				{
					num = 0U;
				}
				genericSkill.SetBaseSkill(variants[(int)num].skillDef);
			}
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000CF0B4 File Offset: 0x000CD2B4
		public void ResetSkills()
		{
			if (NetworkServer.active && this.networkIdentity.clientAuthorityOwner != null)
			{
				NetworkWriter networkWriter = new NetworkWriter();
				networkWriter.StartMessage(56);
				networkWriter.Write(base.gameObject);
				networkWriter.FinishMessage();
				this.networkIdentity.clientAuthorityOwner.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
			}
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				this.allSkills[i].Reset();
			}
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000CF134 File Offset: 0x000CD334
		public void ApplyAmmoPack()
		{
			if (NetworkServer.active && !this.networkIdentity.hasAuthority)
			{
				NetworkWriter networkWriter = new NetworkWriter();
				networkWriter.StartMessage(63);
				networkWriter.Write(base.gameObject);
				networkWriter.FinishMessage();
				NetworkConnection clientAuthorityOwner = this.networkIdentity.clientAuthorityOwner;
				if (clientAuthorityOwner != null)
				{
					clientAuthorityOwner.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
					return;
				}
			}
			else
			{
				foreach (GenericSkill genericSkill in this.allSkills)
				{
					if (genericSkill.CanApplyAmmoPack())
					{
						genericSkill.ApplyAmmoPack();
					}
				}
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000CF1C4 File Offset: 0x000CD3C4
		[NetworkMessageHandler(msgType = 56, client = true)]
		private static void HandleResetSkills(NetworkMessage netMsg)
		{
			GameObject gameObject = netMsg.reader.ReadGameObject();
			if (!NetworkServer.active && gameObject)
			{
				SkillLocator component = gameObject.GetComponent<SkillLocator>();
				if (component)
				{
					component.ResetSkills();
				}
			}
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000CF204 File Offset: 0x000CD404
		[NetworkMessageHandler(msgType = 63, client = true)]
		private static void HandleAmmoPackPickup(NetworkMessage netMsg)
		{
			GameObject gameObject = netMsg.reader.ReadGameObject();
			if (!NetworkServer.active && gameObject)
			{
				SkillLocator component = gameObject.GetComponent<SkillLocator>();
				if (component)
				{
					component.ApplyAmmoPack();
				}
			}
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000CF241 File Offset: 0x000CD441
		public void DeductCooldownFromAllSkillsServer(float deduction)
		{
			if (this.hasEffectiveAuthority)
			{
				this.DeductCooldownFromAllSkillsAuthority(deduction);
				return;
			}
			this.CallRpcDeductCooldownFromAllSkillsServer(deduction);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000CF25A File Offset: 0x000CD45A
		[ClientRpc]
		private void RpcDeductCooldownFromAllSkillsServer(float deduction)
		{
			if (this.hasEffectiveAuthority)
			{
				this.DeductCooldownFromAllSkillsAuthority(deduction);
			}
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000CF26C File Offset: 0x000CD46C
		private void DeductCooldownFromAllSkillsAuthority(float deduction)
		{
			for (int i = 0; i < this.allSkills.Length; i++)
			{
				GenericSkill genericSkill = this.allSkills[i];
				if (genericSkill.stock < genericSkill.maxStock)
				{
					genericSkill.rechargeStopwatch += deduction;
				}
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000CF2B1 File Offset: 0x000CD4B1
		protected static void InvokeRpcRpcDeductCooldownFromAllSkillsServer(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcDeductCooldownFromAllSkillsServer called on server.");
				return;
			}
			((SkillLocator)obj).RpcDeductCooldownFromAllSkillsServer(reader.ReadSingle());
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000CF2DC File Offset: 0x000CD4DC
		public void CallRpcDeductCooldownFromAllSkillsServer(float deduction)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcDeductCooldownFromAllSkillsServer called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)SkillLocator.kRpcRpcDeductCooldownFromAllSkillsServer);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(deduction);
			this.SendRPCInternal(networkWriter, 0, "RpcDeductCooldownFromAllSkillsServer");
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000CF34F File Offset: 0x000CD54F
		static SkillLocator()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(SkillLocator), SkillLocator.kRpcRpcDeductCooldownFromAllSkillsServer, new NetworkBehaviour.CmdDelegate(SkillLocator.InvokeRpcRpcDeductCooldownFromAllSkillsServer));
			NetworkCRC.RegisterBehaviour("SkillLocator", 0);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400325A RID: 12890
		[FormerlySerializedAs("skill1")]
		public GenericSkill primary;

		// Token: 0x0400325B RID: 12891
		[FormerlySerializedAs("skill2")]
		public GenericSkill secondary;

		// Token: 0x0400325C RID: 12892
		[FormerlySerializedAs("skill3")]
		public GenericSkill utility;

		// Token: 0x0400325D RID: 12893
		[FormerlySerializedAs("skill4")]
		public GenericSkill special;

		// Token: 0x0400325E RID: 12894
		public SkillLocator.PassiveSkill passiveSkill;

		// Token: 0x0400325F RID: 12895
		[SerializeField]
		private GenericSkill primaryBonusStockOverrideSkill;

		// Token: 0x04003260 RID: 12896
		[SerializeField]
		private GenericSkill secondaryBonusStockOverrideSkill;

		// Token: 0x04003261 RID: 12897
		[SerializeField]
		private GenericSkill utilityBonusStockOverrideSkill;

		// Token: 0x04003262 RID: 12898
		[SerializeField]
		private GenericSkill specialBonusStockOverrideSkill;

		// Token: 0x04003263 RID: 12899
		private NetworkIdentity networkIdentity;

		// Token: 0x04003264 RID: 12900
		private GenericSkill[] allSkills;

		// Token: 0x04003265 RID: 12901
		private bool hasEffectiveAuthority;

		// Token: 0x04003266 RID: 12902
		private uint skillDefDirtyFlags;

		// Token: 0x04003267 RID: 12903
		private bool inDeserialize;

		// Token: 0x04003268 RID: 12904
		private static int kRpcRpcDeductCooldownFromAllSkillsServer = -2090076365;

		// Token: 0x0200089C RID: 2204
		[Serializable]
		public struct PassiveSkill
		{
			// Token: 0x04003269 RID: 12905
			public bool enabled;

			// Token: 0x0400326A RID: 12906
			public string skillNameToken;

			// Token: 0x0400326B RID: 12907
			public string skillDescriptionToken;

			// Token: 0x0400326C RID: 12908
			public string keywordToken;

			// Token: 0x0400326D RID: 12909
			public Sprite icon;
		}
	}
}
