using System;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F5 RID: 1013
	public class ReloadPistols : GenericReload
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x0005129C File Offset: 0x0004F49C
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Gesture, Override", "ReloadPistols", "ReloadPistols.playbackRate", this.duration);
			base.PlayAnimation("Gesture, Additive", "ReloadPistols", "ReloadPistols.playbackRate", this.duration);
			Transform transform = base.FindModelChild("GunMeshL");
			if (transform != null)
			{
				transform.gameObject.SetActive(false);
			}
			Transform transform2 = base.FindModelChild("GunMeshR");
			if (transform2 == null)
			{
				return;
			}
			transform2.gameObject.SetActive(false);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0005131C File Offset: 0x0004F51C
		public override void OnExit()
		{
			Transform transform = base.FindModelChild("ReloadFXL");
			if (transform != null)
			{
				transform.gameObject.SetActive(false);
			}
			Transform transform2 = base.FindModelChild("ReloadFXR");
			if (transform2 != null)
			{
				transform2.gameObject.SetActive(false);
			}
			Transform transform3 = base.FindModelChild("GunMeshL");
			if (transform3 != null)
			{
				transform3.gameObject.SetActive(true);
			}
			Transform transform4 = base.FindModelChild("GunMeshR");
			if (transform4 != null)
			{
				transform4.gameObject.SetActive(true);
			}
			this.PlayAnimation("Gesture, Override", "ReloadPistolsExit");
			this.PlayAnimation("Gesture, Additive", "ReloadPistolsExit");
			base.OnExit();
		}
	}
}
