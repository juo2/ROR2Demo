using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace EntityStates.GameOver
{
	// Token: 0x0200037F RID: 895
	public class VoidEndingFadeToBlack : BaseGameOverControllerState
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x00046E94 File Offset: 0x00045094
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.startTime = Run.TimeStamp.now + this.delay;
			}
			this.screenInstance = UnityEngine.Object.Instantiate<GameObject>(this.screenPrefab, RoR2Application.instance.mainCanvas.transform);
			this.image = this.screenInstance.GetComponentInChildren<Image>();
			this.image.raycastTarget = false;
			this.UpdateImageAlpha(0f);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00046F0C File Offset: 0x0004510C
		public override void OnExit()
		{
			FadeToBlackManager.ForceFullBlack();
			EntityState.Destroy(this.screenInstance);
			this.screenInstance = null;
			this.image = null;
			base.OnExit();
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00046F32 File Offset: 0x00045132
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.startTime);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00046F47 File Offset: 0x00045147
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.startTime = reader.ReadTimeStamp();
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00046F5C File Offset: 0x0004515C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.UpdateImageAlpha(Mathf.Max(0f, Mathf.Min(1f, this.startTime.timeSince / this.duration)));
			if (NetworkServer.active && (this.startTime + this.duration).hasPassed)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00046FC8 File Offset: 0x000451C8
		private void UpdateImageAlpha(float finalAlpha)
		{
			Color color = this.image.color;
			color.a = finalAlpha;
			this.image.color = color;
		}

		// Token: 0x0400148A RID: 5258
		[SerializeField]
		public float delay;

		// Token: 0x0400148B RID: 5259
		[SerializeField]
		public float duration;

		// Token: 0x0400148C RID: 5260
		[SerializeField]
		public GameObject screenPrefab;

		// Token: 0x0400148D RID: 5261
		private Run.TimeStamp startTime;

		// Token: 0x0400148E RID: 5262
		private Image image;

		// Token: 0x0400148F RID: 5263
		private GameObject screenInstance;
	}
}
