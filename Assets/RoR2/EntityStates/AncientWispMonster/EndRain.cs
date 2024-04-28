using System;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x0200049A RID: 1178
	public class EndRain : BaseState
	{
		// Token: 0x06001521 RID: 5409 RVA: 0x0005DC30 File Offset: 0x0005BE30
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = EndRain.baseDuration / this.attackSpeedStat;
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = Vector3.zero;
			}
			base.PlayAnimation("Body", "EndRain", "EndRain.playbackRate", this.duration);
			NodeGraph airNodes = SceneInfo.instance.airNodes;
			NodeGraph.NodeIndex nodeIndex = airNodes.FindClosestNode(base.transform.position, base.characterBody.hullClassification, float.PositiveInfinity);
			Vector3 position;
			airNodes.GetNodePosition(nodeIndex, out position);
			base.transform.position = position;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0005DCCE File Offset: 0x0005BECE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001AF3 RID: 6899
		public static float baseDuration = 3f;

		// Token: 0x04001AF4 RID: 6900
		public static GameObject effectPrefab;

		// Token: 0x04001AF5 RID: 6901
		public static GameObject delayPrefab;

		// Token: 0x04001AF6 RID: 6902
		private float duration;
	}
}
