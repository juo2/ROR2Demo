using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000904 RID: 2308
	public class WispAI : MonoBehaviour
	{
		// Token: 0x06003424 RID: 13348 RVA: 0x000DBAD5 File Offset: 0x000D9CD5
		private void Awake()
		{
			this.bodyDirectionComponent = this.body.GetComponent<CharacterDirection>();
			this.bodyMotorComponent = this.body.GetComponent<CharacterMotor>();
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000DBAFC File Offset: 0x000D9CFC
		private void FixedUpdate()
		{
			if (!this.body)
			{
				return;
			}
			if (!this.targetTransform)
			{
				this.targetTransform = this.SearchForTarget();
			}
			if (this.targetTransform)
			{
				Vector3 vector = this.targetTransform.position - this.body.transform.position;
				this.bodyMotorComponent.moveDirection = vector;
				this.bodyDirectionComponent.moveVector = Vector3.Lerp(this.bodyDirectionComponent.moveVector, vector, Time.deltaTime);
				if (this.fireSkill && vector.sqrMagnitude < this.fireRange * this.fireRange)
				{
					this.fireSkill.ExecuteIfReady();
				}
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000DBBBC File Offset: 0x000D9DBC
		private Transform SearchForTarget()
		{
			Vector3 position = this.body.transform.position;
			Vector3 forward = this.bodyDirectionComponent.forward;
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Player);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				Transform transform = teamMembers[i].transform;
				Vector3 vector = transform.position - position;
				if (Vector3.Dot(forward, vector) > 0f)
				{
					WispAI.candidateList.Add(new WispAI.TargetSearchCandidate
					{
						transform = transform,
						positionDiff = vector,
						sqrDistance = vector.sqrMagnitude
					});
				}
			}
			WispAI.candidateList.Sort(delegate(WispAI.TargetSearchCandidate a, WispAI.TargetSearchCandidate b)
			{
				if (a.sqrDistance < b.sqrDistance)
				{
					return -1;
				}
				if (a.sqrDistance != b.sqrDistance)
				{
					return 1;
				}
				return 0;
			});
			Transform result = null;
			for (int j = 0; j < WispAI.candidateList.Count; j++)
			{
				if (!Physics.Raycast(position, WispAI.candidateList[j].positionDiff, Mathf.Sqrt(WispAI.candidateList[j].sqrDistance), LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					result = WispAI.candidateList[j].transform;
					break;
				}
			}
			WispAI.candidateList.Clear();
			return result;
		}

		// Token: 0x0400351B RID: 13595
		[Tooltip("The character to control.")]
		public GameObject body;

		// Token: 0x0400351C RID: 13596
		[Tooltip("The enemy to target.")]
		public Transform targetTransform;

		// Token: 0x0400351D RID: 13597
		[Tooltip("The skill to activate for a ranged attack.")]
		public GenericSkill fireSkill;

		// Token: 0x0400351E RID: 13598
		[Tooltip("How close the character must be to the enemy to use a ranged attack.")]
		public float fireRange;

		// Token: 0x0400351F RID: 13599
		private CharacterDirection bodyDirectionComponent;

		// Token: 0x04003520 RID: 13600
		private CharacterMotor bodyMotorComponent;

		// Token: 0x04003521 RID: 13601
		private static List<WispAI.TargetSearchCandidate> candidateList = new List<WispAI.TargetSearchCandidate>();

		// Token: 0x02000905 RID: 2309
		private struct TargetSearchCandidate
		{
			// Token: 0x04003522 RID: 13602
			public Transform transform;

			// Token: 0x04003523 RID: 13603
			public Vector3 positionDiff;

			// Token: 0x04003524 RID: 13604
			public float sqrDistance;
		}
	}
}
