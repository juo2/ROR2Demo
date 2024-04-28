using System;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000779 RID: 1913
	public struct EquipmentState : IEquatable<EquipmentState>
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x000ADC50 File Offset: 0x000ABE50
		public bool isPerfomingRecharge
		{
			get
			{
				return !this.chargeFinishTime.isPositiveInfinity;
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000ADC6E File Offset: 0x000ABE6E
		public EquipmentState(EquipmentIndex equipmentIndex, Run.FixedTimeStamp chargeFinishTime, byte charges)
		{
			this.equipmentIndex = equipmentIndex;
			this.chargeFinishTime = chargeFinishTime;
			this.charges = charges;
			this.dirty = true;
			this.equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000ADC98 File Offset: 0x000ABE98
		public bool Equals(EquipmentState other)
		{
			return this.equipmentIndex == other.equipmentIndex && this.chargeFinishTime.Equals(other.chargeFinishTime) && this.charges == other.charges;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000ADCD9 File Offset: 0x000ABED9
		public override bool Equals(object obj)
		{
			return obj != null && obj is EquipmentState && this.Equals((EquipmentState)obj);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000ADCF8 File Offset: 0x000ABEF8
		public override int GetHashCode()
		{
			return (int)(this.equipmentIndex * (EquipmentIndex)397 ^ (EquipmentIndex)this.chargeFinishTime.GetHashCode());
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000ADD28 File Offset: 0x000ABF28
		public static EquipmentState Deserialize(NetworkReader reader)
		{
			EquipmentIndex equipmentIndex = reader.ReadEquipmentIndex();
			Run.FixedTimeStamp fixedTimeStamp = reader.ReadFixedTimeStamp();
			byte b = reader.ReadByte();
			return new EquipmentState(equipmentIndex, fixedTimeStamp, b);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000ADD50 File Offset: 0x000ABF50
		public static void Serialize(NetworkWriter writer, EquipmentState equipmentState)
		{
			writer.Write(equipmentState.equipmentIndex);
			writer.Write(equipmentState.chargeFinishTime);
			writer.Write(equipmentState.charges);
		}

		// Token: 0x04002BB3 RID: 11187
		public readonly EquipmentIndex equipmentIndex;

		// Token: 0x04002BB4 RID: 11188
		public readonly Run.FixedTimeStamp chargeFinishTime;

		// Token: 0x04002BB5 RID: 11189
		public readonly byte charges;

		// Token: 0x04002BB6 RID: 11190
		public bool dirty;

		// Token: 0x04002BB7 RID: 11191
		[CanBeNull]
		public readonly EquipmentDef equipmentDef;

		// Token: 0x04002BB8 RID: 11192
		public static readonly EquipmentState empty = new EquipmentState(EquipmentIndex.None, Run.FixedTimeStamp.negativeInfinity, 0);
	}
}
