using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MegaConstruct
{
	// Token: 0x02000286 RID: 646
	public class RaiseShield : FlyState
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x0002FB10 File Offset: 0x0002DD10
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active && this.attachmentPrefab)
			{
				this.attachment = UnityEngine.Object.Instantiate<GameObject>(this.attachmentPrefab).GetComponent<NetworkedBodyAttachment>();
				this.attachment.AttachToGameObjectAndSpawn(base.characterBody.gameObject, null);
			}
			MasterSpawnSlotController component = base.GetComponent<MasterSpawnSlotController>();
			if (NetworkServer.active && component)
			{
				component.SpawnAllOpen(base.gameObject, Run.instance.stageRng, null);
			}
			this.PlayAnimation(this.animationLayerName, this.animationEnterStateName);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool CanExecuteSkill(GenericSkill skillSlot)
		{
			return false;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002FBA3 File Offset: 0x0002DDA3
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new ExitShield());
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002FBC9 File Offset: 0x0002DDC9
		public override void OnExit()
		{
			if (NetworkServer.active && this.attachment)
			{
				EntityState.Destroy(this.attachment.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x04000D64 RID: 3428
		[SerializeField]
		public GameObject attachmentPrefab;

		// Token: 0x04000D65 RID: 3429
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000D66 RID: 3430
		[SerializeField]
		public string animationEnterStateName;

		// Token: 0x04000D67 RID: 3431
		[SerializeField]
		public float duration;

		// Token: 0x04000D68 RID: 3432
		private NetworkedBodyAttachment attachment;
	}
}
