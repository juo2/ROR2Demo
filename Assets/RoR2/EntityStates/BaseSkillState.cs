using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000B1 RID: 177
	public class BaseSkillState : BaseState, ISkillState
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000C07A File Offset: 0x0000A27A
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000C082 File Offset: 0x0000A282
		public GenericSkill activatorSkillSlot { get; set; }

		// Token: 0x060002DF RID: 735 RVA: 0x0000C08B File Offset: 0x0000A28B
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			this.Serialize(base.skillLocator, writer);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.Deserialize(base.skillLocator, reader);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C0B7 File Offset: 0x0000A2B7
		public bool IsKeyDownAuthority()
		{
			return this.IsKeyDownAuthority(base.skillLocator, base.inputBank);
		}
	}
}
