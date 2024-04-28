using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000726 RID: 1830
	public class DamageDealtMessage : MessageBase
	{
		// Token: 0x06002608 RID: 9736 RVA: 0x000A5F94 File Offset: 0x000A4194
		public override void Serialize(NetworkWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.victim);
			writer.Write(this.damage);
			writer.Write(this.attacker);
			writer.Write(this.position);
			writer.Write(this.crit);
			writer.Write(this.damageType);
			writer.Write(this.damageColorIndex);
			writer.Write(this.hitLowHealth);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000A600C File Offset: 0x000A420C
		public override void Deserialize(NetworkReader reader)
		{
			base.Deserialize(reader);
			this.victim = reader.ReadGameObject();
			this.damage = reader.ReadSingle();
			this.attacker = reader.ReadGameObject();
			this.position = reader.ReadVector3();
			this.crit = reader.ReadBoolean();
			this.damageType = reader.ReadDamageType();
			this.damageColorIndex = reader.ReadDamageColorIndex();
			this.hitLowHealth = reader.ReadBoolean();
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000A6080 File Offset: 0x000A4280
		public bool isSilent
		{
			get
			{
				return (this.damageType & DamageType.Silent) > DamageType.Generic;
			}
		}

		// Token: 0x040029DA RID: 10714
		public GameObject victim;

		// Token: 0x040029DB RID: 10715
		public float damage;

		// Token: 0x040029DC RID: 10716
		public GameObject attacker;

		// Token: 0x040029DD RID: 10717
		public Vector3 position;

		// Token: 0x040029DE RID: 10718
		public bool crit;

		// Token: 0x040029DF RID: 10719
		public DamageType damageType;

		// Token: 0x040029E0 RID: 10720
		public DamageColorIndex damageColorIndex;

		// Token: 0x040029E1 RID: 10721
		public bool hitLowHealth;
	}
}
