using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200061A RID: 1562
	[RequireComponent(typeof(CharacterBody))]
	public class CaptainSupplyDropController : NetworkBehaviour
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0007A5AE File Offset: 0x000787AE
		private bool canUseOrbitalSkills
		{
			get
			{
				return SceneCatalog.mostRecentSceneDef.sceneType == SceneType.Stage;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0007A5BD File Offset: 0x000787BD
		private bool hasEffectiveAuthority
		{
			get
			{
				return this.characterBody.hasEffectiveAuthority;
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0007A5CA File Offset: 0x000787CA
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
			this.hasDoneInitialReset = false;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0007A5E0 File Offset: 0x000787E0
		private void FixedUpdate()
		{
			this.UpdateSkillOverrides();
			if (this.hasEffectiveAuthority && !this.hasDoneInitialReset)
			{
				this.hasDoneInitialReset = true;
				if (this.supplyDrop1Skill)
				{
					this.supplyDrop1Skill.Reset();
				}
				if (this.supplyDrop2Skill)
				{
					this.supplyDrop2Skill.Reset();
				}
			}
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0007A63A File Offset: 0x0007883A
		private void OnDisable()
		{
			this.SetSkillOverride(ref this.currentSupplyDrop1SkillDef, null, this.supplyDrop1Skill);
			this.SetSkillOverride(ref this.currentSupplyDrop2SkillDef, null, this.supplyDrop2Skill);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0007A662 File Offset: 0x00078862
		private void SetSkillOverride([CanBeNull] ref SkillDef currentSkillDef, [CanBeNull] SkillDef newSkillDef, [NotNull] GenericSkill component)
		{
			if (currentSkillDef == newSkillDef)
			{
				return;
			}
			if (currentSkillDef != null)
			{
				component.UnsetSkillOverride(this, currentSkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			currentSkillDef = newSkillDef;
			if (currentSkillDef != null)
			{
				component.SetSkillOverride(this, currentSkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0007A68C File Offset: 0x0007888C
		private void UpdateSkillOverrides()
		{
			if (!base.enabled)
			{
				return;
			}
			byte b2;
			if (this.hasEffectiveAuthority)
			{
				byte b = 0;
				if (this.supplyDrop1Skill.stock > 0 || !this.supplyDrop1Skill.skillDef)
				{
					b |= 1;
				}
				if (this.supplyDrop2Skill.stock > 0 || !this.supplyDrop2Skill.skillDef)
				{
					b |= 2;
				}
				if (b != this.authorityEnabledSkillsMask)
				{
					this.authorityEnabledSkillsMask = b;
					if (NetworkServer.active)
					{
						this.NetworknetEnabledSkillsMask = this.authorityEnabledSkillsMask;
					}
					else
					{
						this.CallCmdSetSkillMask(this.authorityEnabledSkillsMask);
					}
				}
				b2 = this.authorityEnabledSkillsMask;
			}
			else
			{
				b2 = this.netEnabledSkillsMask;
			}
			bool flag = (b2 & 1) > 0;
			bool flag2 = (b2 & 2) > 0;
			this.SetSkillOverride(ref this.currentSupplyDrop1SkillDef, flag ? null : this.usedUpSkillDef, this.supplyDrop1Skill);
			this.SetSkillOverride(ref this.currentSupplyDrop2SkillDef, flag2 ? null : this.usedUpSkillDef, this.supplyDrop2Skill);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0007A786 File Offset: 0x00078986
		[Command]
		private void CmdSetSkillMask(byte newMask)
		{
			this.NetworknetEnabledSkillsMask = newMask;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0007A790 File Offset: 0x00078990
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0007A7A3 File Offset: 0x000789A3
		public byte NetworknetEnabledSkillsMask
		{
			get
			{
				return this.netEnabledSkillsMask;
			}
			[param: In]
			set
			{
				base.SetSyncVar<byte>(value, ref this.netEnabledSkillsMask, 1U);
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0007A7B7 File Offset: 0x000789B7
		protected static void InvokeCmdCmdSetSkillMask(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSetSkillMask called on client.");
				return;
			}
			((CaptainSupplyDropController)obj).CmdSetSkillMask((byte)reader.ReadPackedUInt32());
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0007A7E0 File Offset: 0x000789E0
		public void CallCmdSetSkillMask(byte newMask)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSetSkillMask called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSetSkillMask(newMask);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CaptainSupplyDropController.kCmdCmdSetSkillMask);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32((uint)newMask);
			base.SendCommandInternal(networkWriter, 0, "CmdSetSkillMask");
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0007A86A File Offset: 0x00078A6A
		static CaptainSupplyDropController()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(CaptainSupplyDropController), CaptainSupplyDropController.kCmdCmdSetSkillMask, new NetworkBehaviour.CmdDelegate(CaptainSupplyDropController.InvokeCmdCmdSetSkillMask));
			NetworkCRC.RegisterBehaviour("CaptainSupplyDropController", 0);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0007A8A8 File Offset: 0x00078AA8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.netEnabledSkillsMask);
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
				writer.WritePackedUInt32((uint)this.netEnabledSkillsMask);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0007A914 File Offset: 0x00078B14
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.netEnabledSkillsMask = (byte)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.netEnabledSkillsMask = (byte)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002296 RID: 8854
		[Header("Referenced Components")]
		public GenericSkill orbitalStrikeSkill;

		// Token: 0x04002297 RID: 8855
		public GenericSkill prepSupplyDropSkill;

		// Token: 0x04002298 RID: 8856
		public GenericSkill supplyDrop1Skill;

		// Token: 0x04002299 RID: 8857
		public GenericSkill supplyDrop2Skill;

		// Token: 0x0400229A RID: 8858
		[Header("Skill Defs")]
		public SkillDef usedUpSkillDef;

		// Token: 0x0400229B RID: 8859
		public SkillDef lostConnectionSkillDef;

		// Token: 0x0400229C RID: 8860
		[SyncVar]
		private byte netEnabledSkillsMask;

		// Token: 0x0400229D RID: 8861
		private byte authorityEnabledSkillsMask;

		// Token: 0x0400229E RID: 8862
		private bool hasDoneInitialReset;

		// Token: 0x0400229F RID: 8863
		private CharacterBody characterBody;

		// Token: 0x040022A0 RID: 8864
		[CanBeNull]
		private SkillDef currentSupplyDrop1SkillDef;

		// Token: 0x040022A1 RID: 8865
		[CanBeNull]
		private SkillDef currentSupplyDrop2SkillDef;

		// Token: 0x040022A2 RID: 8866
		[CanBeNull]
		private SkillDef currentPrepSupplyDropSkillDef;

		// Token: 0x040022A3 RID: 8867
		[CanBeNull]
		private SkillDef currentOrbitalStrikeSkillDef;

		// Token: 0x040022A4 RID: 8868
		private static int kCmdCmdSetSkillMask = 176967897;
	}
}
