using System;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x02000499 RID: 1177
	public class ChargeRain : BaseState
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x0005DB20 File Offset: 0x0005BD20
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeRain.baseDuration / this.attackSpeedStat;
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = Vector3.zero;
			}
			base.PlayAnimation("Body", "ChargeRain", "ChargeRain.playbackRate", this.duration);
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, 999f, LayerIndex.world.mask))
			{
				NodeGraph groundNodes = SceneInfo.instance.groundNodes;
				NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(raycastHit.point, HullClassification.BeetleQueen, float.PositiveInfinity);
				Vector3 a;
				groundNodes.GetNodePosition(nodeIndex, out a);
				base.transform.position = a + Vector3.up * 2f;
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0005DBF4 File Offset: 0x0005BDF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new ChannelRain());
				return;
			}
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001AEF RID: 6895
		public static float baseDuration = 3f;

		// Token: 0x04001AF0 RID: 6896
		public static GameObject effectPrefab;

		// Token: 0x04001AF1 RID: 6897
		public static GameObject delayPrefab;

		// Token: 0x04001AF2 RID: 6898
		private float duration;
	}
}
