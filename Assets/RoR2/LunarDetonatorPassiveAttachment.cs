using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200079F RID: 1951
	[RequireComponent(typeof(NetworkedBodyAttachment))]
	public class LunarDetonatorPassiveAttachment : NetworkBehaviour, INetworkedBodyAttachmentListener
	{
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000B24E3 File Offset: 0x000B06E3
		// (set) Token: 0x0600292B RID: 10539 RVA: 0x000B24EC File Offset: 0x000B06EC
		public GenericSkill monitoredSkill
		{
			get
			{
				return this._monitoredSkill;
			}
			set
			{
				if (this._monitoredSkill == value)
				{
					return;
				}
				this._monitoredSkill = value;
				int num = -1;
				if (this._monitoredSkill)
				{
					SkillLocator component = this._monitoredSkill.GetComponent<SkillLocator>();
					if (component)
					{
						num = component.GetSkillSlotIndex(this._monitoredSkill);
					}
				}
				this.SetSkillSlotIndexPlusOne((uint)(num + 1));
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000B2543 File Offset: 0x000B0743
		private void Awake()
		{
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000B2551 File Offset: 0x000B0751
		private void FixedUpdate()
		{
			if (this.networkedBodyAttachment.hasEffectiveAuthority)
			{
				this.FixedUpdateAuthority();
			}
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000B2566 File Offset: 0x000B0766
		private void OnDestroy()
		{
			if (this.damageListener)
			{
				UnityEngine.Object.Destroy(this.damageListener);
			}
			this.damageListener = null;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000B2587 File Offset: 0x000B0787
		public override void OnStartClient()
		{
			this.SetSkillSlotIndexPlusOne(this.skillSlotIndexPlusOne);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000B2595 File Offset: 0x000B0795
		private void SetSkillSlotIndexPlusOne(uint newSkillSlotIndexPlusOne)
		{
			this.NetworkskillSlotIndexPlusOne = newSkillSlotIndexPlusOne;
			if (!NetworkServer.active)
			{
				this.ResolveMonitoredSkill();
			}
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000B25AC File Offset: 0x000B07AC
		private void ResolveMonitoredSkill()
		{
			if (this.networkedBodyAttachment.attachedBody)
			{
				SkillLocator component = this.networkedBodyAttachment.attachedBody.GetComponent<SkillLocator>();
				if (component)
				{
					this.monitoredSkill = component.GetSkillAtIndex((int)(this.skillSlotIndexPlusOne - 1U));
				}
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000B25F8 File Offset: 0x000B07F8
		private void FixedUpdateAuthority()
		{
			bool flag = false;
			if (this.monitoredSkill)
			{
				flag = (this.monitoredSkill.stock > 0);
			}
			if (this.skillAvailable != flag)
			{
				this.skillAvailable = flag;
				if (!NetworkServer.active)
				{
					this.CallCmdSetSkillAvailable(this.skillAvailable);
				}
			}
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000B2646 File Offset: 0x000B0846
		[Command]
		private void CmdSetSkillAvailable(bool newSkillAvailable)
		{
			this.skillAvailable = newSkillAvailable;
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000B264F File Offset: 0x000B084F
		public void OnAttachedBodyDiscovered(NetworkedBodyAttachment networkedBodyAttachment, CharacterBody attachedBody)
		{
			if (NetworkServer.active)
			{
				this.damageListener = attachedBody.gameObject.AddComponent<LunarDetonatorPassiveAttachment.DamageListener>();
				this.damageListener.passiveController = this;
			}
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000B2678 File Offset: 0x000B0878
		// (set) Token: 0x06002938 RID: 10552 RVA: 0x000B268B File Offset: 0x000B088B
		public uint NetworkskillSlotIndexPlusOne
		{
			get
			{
				return this.skillSlotIndexPlusOne;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetSkillSlotIndexPlusOne(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<uint>(value, ref this.skillSlotIndexPlusOne, 1U);
			}
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000B26CA File Offset: 0x000B08CA
		protected static void InvokeCmdCmdSetSkillAvailable(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSetSkillAvailable called on client.");
				return;
			}
			((LunarDetonatorPassiveAttachment)obj).CmdSetSkillAvailable(reader.ReadBoolean());
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000B26F4 File Offset: 0x000B08F4
		public void CallCmdSetSkillAvailable(bool newSkillAvailable)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSetSkillAvailable called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSetSkillAvailable(newSkillAvailable);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)LunarDetonatorPassiveAttachment.kCmdCmdSetSkillAvailable);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(newSkillAvailable);
			base.SendCommandInternal(networkWriter, 0, "CmdSetSkillAvailable");
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000B277E File Offset: 0x000B097E
		static LunarDetonatorPassiveAttachment()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(LunarDetonatorPassiveAttachment), LunarDetonatorPassiveAttachment.kCmdCmdSetSkillAvailable, new NetworkBehaviour.CmdDelegate(LunarDetonatorPassiveAttachment.InvokeCmdCmdSetSkillAvailable));
			NetworkCRC.RegisterBehaviour("LunarDetonatorPassiveAttachment", 0);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000B27BC File Offset: 0x000B09BC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32(this.skillSlotIndexPlusOne);
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
				writer.WritePackedUInt32(this.skillSlotIndexPlusOne);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000B2828 File Offset: 0x000B0A28
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.skillSlotIndexPlusOne = reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetSkillSlotIndexPlusOne(reader.ReadPackedUInt32());
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002CA3 RID: 11427
		private GenericSkill _monitoredSkill;

		// Token: 0x04002CA4 RID: 11428
		[SyncVar(hook = "SetSkillSlotIndexPlusOne")]
		private uint skillSlotIndexPlusOne;

		// Token: 0x04002CA5 RID: 11429
		private bool skillAvailable;

		// Token: 0x04002CA6 RID: 11430
		private NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x04002CA7 RID: 11431
		private LunarDetonatorPassiveAttachment.DamageListener damageListener;

		// Token: 0x04002CA8 RID: 11432
		private static int kCmdCmdSetSkillAvailable = -1453655134;

		// Token: 0x020007A0 RID: 1952
		private class DamageListener : MonoBehaviour, IOnDamageDealtServerReceiver
		{
			// Token: 0x0600293F RID: 10559 RVA: 0x000B286C File Offset: 0x000B0A6C
			public void OnDamageDealtServer(DamageReport damageReport)
			{
				if (this.passiveController.skillAvailable && damageReport.victim.alive && Util.CheckRoll(damageReport.damageInfo.procCoefficient * 100f, damageReport.attackerMaster))
				{
					damageReport.victimBody.AddTimedBuff(RoR2Content.Buffs.LunarDetonationCharge, 10f);
				}
			}

			// Token: 0x04002CA9 RID: 11433
			public LunarDetonatorPassiveAttachment passiveController;
		}
	}
}
