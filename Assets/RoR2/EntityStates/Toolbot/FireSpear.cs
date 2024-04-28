using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x0200019B RID: 411
	public class FireSpear : GenericBulletBaseState, IToolbotPrimarySkillState, ISkillState
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001F550 File Offset: 0x0001D750
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001F558 File Offset: 0x0001D758
		Transform IToolbotPrimarySkillState.muzzleTransform { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001F561 File Offset: 0x0001D761
		string IToolbotPrimarySkillState.baseMuzzleName
		{
			get
			{
				return this.muzzleName;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001F569 File Offset: 0x0001D769
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001F571 File Offset: 0x0001D771
		bool IToolbotPrimarySkillState.isInDualWield { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001F57A File Offset: 0x0001D77A
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001F582 File Offset: 0x0001D782
		int IToolbotPrimarySkillState.currentHand { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001F58B File Offset: 0x0001D78B
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0001F593 File Offset: 0x0001D793
		string IToolbotPrimarySkillState.muzzleName { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001F59C File Offset: 0x0001D79C
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001F5A4 File Offset: 0x0001D7A4
		public SkillDef skillDef { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001F5AD File Offset: 0x0001D7AD
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001F5B5 File Offset: 0x0001D7B5
		public GenericSkill activatorSkillSlot { get; set; }

		// Token: 0x0600075C RID: 1884 RVA: 0x0001F5BE File Offset: 0x0001D7BE
		public override void OnEnter()
		{
			BaseToolbotPrimarySkillStateMethods.OnEnter<FireSpear>(this, base.gameObject, base.skillLocator, base.GetModelChildLocator());
			base.OnEnter();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.stopperMask = LayerIndex.world.mask;
			bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
			bulletAttack.muzzleName = ((IToolbotPrimarySkillState)this).muzzleName;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001F61C File Offset: 0x0001D81C
		protected override void FireBullet(Ray aimRay)
		{
			base.FireBullet(aimRay);
			base.characterBody.SetSpreadBloom(1f, false);
			base.AddRecoil(-0.6f * FireSpear.recoilAmplitude, -0.8f * FireSpear.recoilAmplitude, -0.1f * FireSpear.recoilAmplitude, 0.1f * FireSpear.recoilAmplitude);
			if (!((IToolbotPrimarySkillState)this).isInDualWield)
			{
				base.PlayAnimation("Gesture, Additive", "FireSpear", "FireSpear.playbackRate", this.duration);
				return;
			}
			BaseToolbotPrimarySkillStateMethods.PlayGenericFireAnim<FireSpear>(this, base.gameObject, base.skillLocator, this.duration);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001F6AF File Offset: 0x0001D8AF
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(0.9f + base.age / this.duration, true);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001F6D6 File Offset: 0x0001D8D6
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new CooldownSpear
				{
					activatorSkillSlot = this.activatorSkillSlot
				});
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000C08B File Offset: 0x0000A28B
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			this.Serialize(base.skillLocator, writer);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.Deserialize(base.skillLocator, reader);
		}

		// Token: 0x040008E4 RID: 2276
		public float charge;

		// Token: 0x040008E5 RID: 2277
		public static float recoilAmplitude;
	}
}
