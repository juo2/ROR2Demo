using System;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200083D RID: 2109
	[RequireComponent(typeof(CharacterBody))]
	public class RailMotor : MonoBehaviour
	{
		// Token: 0x06002E08 RID: 11784 RVA: 0x000C3DFC File Offset: 0x000C1FFC
		private void Start()
		{
			this.characterDirection = base.GetComponent<CharacterDirection>();
			this.inputBank = base.GetComponent<InputBankTest>();
			this.characterBody = base.GetComponent<CharacterBody>();
			this.railGraph = SceneInfo.instance.railNodes;
			ModelLocator component = base.GetComponent<ModelLocator>();
			if (component)
			{
				this.modelAnimator = component.modelTransform.GetComponent<Animator>();
			}
			this.nodeA = this.railGraph.FindClosestNode(base.transform.position, this.characterBody.hullClassification, float.PositiveInfinity);
			NodeGraph.LinkIndex[] activeNodeLinks = this.railGraph.GetActiveNodeLinks(this.nodeA);
			this.currentLink = activeNodeLinks[0];
			this.UpdateNodeAndLinkInfo();
			this.useRootMotion = this.characterBody.rootMotionInMainState;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000C3EC0 File Offset: 0x000C20C0
		private void UpdateNodeAndLinkInfo()
		{
			this.nodeA = this.railGraph.GetLinkStartNode(this.currentLink);
			this.nodeB = this.railGraph.GetLinkEndNode(this.currentLink);
			this.railGraph.GetNodePosition(this.nodeA, out this.nodeAPosition);
			this.railGraph.GetNodePosition(this.nodeB, out this.nodeBPosition);
			this.linkVector = this.nodeBPosition - this.nodeAPosition;
			this.linkLength = this.linkVector.magnitude;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000C3F54 File Offset: 0x000C2154
		private void FixedUpdate()
		{
			this.UpdateNodeAndLinkInfo();
			if (this.inputBank)
			{
				bool value = false;
				if (this.inputMoveVector.sqrMagnitude > 0f)
				{
					value = true;
					this.characterDirection.moveVector = this.linkVector;
					if (this.linkLerp == 0f || this.linkLerp == 1f)
					{
						NodeGraph.NodeIndex nodeIndex;
						if (this.linkLerp == 0f)
						{
							nodeIndex = this.nodeA;
						}
						else
						{
							nodeIndex = this.nodeB;
						}
						NodeGraph.LinkIndex[] activeNodeLinks = this.railGraph.GetActiveNodeLinks(nodeIndex);
						float num = -1f;
						NodeGraph.LinkIndex lhs = this.currentLink;
						Debug.DrawRay(base.transform.position, this.inputMoveVector, Color.green);
						foreach (NodeGraph.LinkIndex linkIndex in activeNodeLinks)
						{
							NodeGraph.NodeIndex linkStartNode = this.railGraph.GetLinkStartNode(linkIndex);
							NodeGraph.NodeIndex linkEndNode = this.railGraph.GetLinkEndNode(linkIndex);
							if (!(linkStartNode != nodeIndex))
							{
								Vector3 vector;
								this.railGraph.GetNodePosition(linkStartNode, out vector);
								Vector3 a;
								this.railGraph.GetNodePosition(linkEndNode, out a);
								Vector3 vector2 = a - vector;
								Vector3 rhs = new Vector3(vector2.x, 0f, vector2.z);
								Debug.DrawRay(vector, vector2, Color.red);
								float num2 = Vector3.Dot(this.inputMoveVector, rhs);
								if (num2 > num)
								{
									num = num2;
									lhs = linkIndex;
								}
							}
						}
						if (lhs != this.currentLink)
						{
							this.currentLink = lhs;
							this.UpdateNodeAndLinkInfo();
							this.linkLerp = 0f;
						}
					}
				}
				this.modelAnimator.SetBool("isMoving", value);
				if (this.useRootMotion)
				{
					this.TravelLink();
				}
				else
				{
					this.TravelLink();
				}
			}
			base.transform.position = Vector3.Lerp(this.nodeAPosition, this.nodeBPosition, this.linkLerp);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000C413C File Offset: 0x000C233C
		private void TravelLink()
		{
			this.projectedMoveVector = Vector3.Project(this.inputMoveVector, this.linkVector);
			this.projectedMoveVector = this.projectedMoveVector.normalized;
			if (this.characterBody.rootMotionInMainState)
			{
				this.currentMoveSpeed = this.rootMotion.magnitude / Time.fixedDeltaTime;
				this.rootMotion = Vector3.zero;
			}
			else
			{
				float target;
				if (this.projectedMoveVector.sqrMagnitude > 0f)
				{
					target = this.characterBody.moveSpeed * this.inputMoveVector.magnitude;
				}
				else
				{
					target = 0f;
				}
				this.currentMoveSpeed = Mathf.MoveTowards(this.currentMoveSpeed, target, this.characterBody.acceleration * Time.fixedDeltaTime);
			}
			if (this.currentMoveSpeed > 0f)
			{
				Vector3 lhs = this.projectedMoveVector * this.currentMoveSpeed;
				float num = this.currentMoveSpeed / this.linkLength * Mathf.Sign(Vector3.Dot(lhs, this.linkVector)) * Time.fixedDeltaTime;
				this.linkLerp = Mathf.Clamp01(this.linkLerp + num);
			}
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000C4250 File Offset: 0x000C2450
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(this.nodeAPosition, 0.5f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(this.nodeBPosition, 0.5f);
			Gizmos.DrawLine(this.nodeAPosition, this.nodeBPosition);
		}

		// Token: 0x04002FE9 RID: 12265
		public Vector3 inputMoveVector;

		// Token: 0x04002FEA RID: 12266
		public Vector3 rootMotion;

		// Token: 0x04002FEB RID: 12267
		private Animator modelAnimator;

		// Token: 0x04002FEC RID: 12268
		private InputBankTest inputBank;

		// Token: 0x04002FED RID: 12269
		private NodeGraph railGraph;

		// Token: 0x04002FEE RID: 12270
		private NodeGraph.NodeIndex nodeA;

		// Token: 0x04002FEF RID: 12271
		private NodeGraph.NodeIndex nodeB;

		// Token: 0x04002FF0 RID: 12272
		private NodeGraph.LinkIndex currentLink;

		// Token: 0x04002FF1 RID: 12273
		private CharacterBody characterBody;

		// Token: 0x04002FF2 RID: 12274
		private CharacterDirection characterDirection;

		// Token: 0x04002FF3 RID: 12275
		private float linkLerp;

		// Token: 0x04002FF4 RID: 12276
		private Vector3 projectedMoveVector;

		// Token: 0x04002FF5 RID: 12277
		private Vector3 nodeAPosition;

		// Token: 0x04002FF6 RID: 12278
		private Vector3 nodeBPosition;

		// Token: 0x04002FF7 RID: 12279
		private Vector3 linkVector;

		// Token: 0x04002FF8 RID: 12280
		private float linkLength;

		// Token: 0x04002FF9 RID: 12281
		private float currentMoveSpeed;

		// Token: 0x04002FFA RID: 12282
		private bool useRootMotion;
	}
}
