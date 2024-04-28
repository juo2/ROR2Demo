using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GolemMonster
{
	// Token: 0x0200036D RID: 877
	public class SpawnState : GenericCharacterSpawnState
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00045DC0 File Offset: 0x00043FC0
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				modelTransform.GetComponent<PrintController>().enabled = true;
			}
			Transform transform = base.FindModelChild("Eye");
			this.eyeGameObject = ((transform != null) ? transform.gameObject : null);
			if (this.eyeGameObject)
			{
				this.eyeGameObject.SetActive(false);
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00045E24 File Offset: 0x00044024
		public override void OnExit()
		{
			if (!this.outer.destroying && this.eyeGameObject)
			{
				this.eyeGameObject.SetActive(true);
			}
		}

		// Token: 0x0400142F RID: 5167
		private GameObject eyeGameObject;
	}
}
