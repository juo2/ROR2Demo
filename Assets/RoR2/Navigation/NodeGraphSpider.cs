using System;
using System.Collections;
using System.Collections.Generic;
using HG;
using JetBrains.Annotations;

namespace RoR2.Navigation
{
	// Token: 0x02000B58 RID: 2904
	public class NodeGraphSpider
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x00111C36 File Offset: 0x0010FE36
		// (set) Token: 0x060041FB RID: 16891 RVA: 0x00111C3E File Offset: 0x0010FE3E
		[NotNull]
		public List<NodeGraphSpider.StepInfo> collectedSteps { get; private set; }

		// Token: 0x060041FC RID: 16892 RVA: 0x00111C48 File Offset: 0x0010FE48
		public NodeGraphSpider([NotNull] NodeGraph nodeGraph, HullMask hullMask)
		{
			if (nodeGraph == null)
			{
				throw new ArgumentNullException("nodeGraph", "'nodeGraph' must be valid.");
			}
			this.nodeGraph = nodeGraph;
			this.hullMask = hullMask;
			this.collectedSteps = new List<NodeGraphSpider.StepInfo>();
			this.uncheckedSteps = new List<NodeGraphSpider.StepInfo>();
			this.visitedNodes = new BitArray(nodeGraph.GetNodeCount());
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x00111CA4 File Offset: 0x0010FEA4
		public bool PerformStep()
		{
			List<NodeGraphSpider.StepInfo> list = this.uncheckedSteps;
			this.uncheckedSteps = new List<NodeGraphSpider.StepInfo>();
			List<NodeGraph.LinkIndex> list2 = CollectionPool<NodeGraph.LinkIndex, List<NodeGraph.LinkIndex>>.RentCollection();
			for (int i = 0; i < list.Count; i++)
			{
				NodeGraphSpider.StepInfo stepInfo = list[i];
				list2.Clear();
				this.nodeGraph.GetActiveNodeLinks(stepInfo.node, list2);
				for (int j = 0; j < list2.Count; j++)
				{
					NodeGraph.LinkIndex linkIndex = list2[j];
					if (this.nodeGraph.IsLinkSuitableForHull(linkIndex, this.hullMask))
					{
						NodeGraph.NodeIndex linkEndNode = this.nodeGraph.GetLinkEndNode(linkIndex);
						if (!this.visitedNodes[linkEndNode.nodeIndex])
						{
							this.uncheckedSteps.Add(new NodeGraphSpider.StepInfo
							{
								node = linkEndNode,
								previousStep = stepInfo
							});
							this.visitedNodes[linkEndNode.nodeIndex] = true;
						}
					}
				}
				this.collectedSteps.Add(stepInfo);
			}
			list2 = CollectionPool<NodeGraph.LinkIndex, List<NodeGraph.LinkIndex>>.ReturnCollection(list2);
			return list.Count > 0;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x00111DA8 File Offset: 0x0010FFA8
		public void AddNodeForNextStep(NodeGraph.NodeIndex nodeIndex)
		{
			if (nodeIndex == NodeGraph.NodeIndex.invalid)
			{
				return;
			}
			if (!this.visitedNodes[nodeIndex.nodeIndex])
			{
				this.uncheckedSteps.Add(new NodeGraphSpider.StepInfo
				{
					node = nodeIndex,
					previousStep = null
				});
				this.visitedNodes[nodeIndex.nodeIndex] = true;
			}
		}

		// Token: 0x0400402D RID: 16429
		[NotNull]
		private NodeGraph nodeGraph;

		// Token: 0x0400402F RID: 16431
		private List<NodeGraphSpider.StepInfo> uncheckedSteps;

		// Token: 0x04004030 RID: 16432
		private BitArray visitedNodes;

		// Token: 0x04004031 RID: 16433
		public HullMask hullMask;

		// Token: 0x02000B59 RID: 2905
		public class StepInfo
		{
			// Token: 0x04004032 RID: 16434
			public NodeGraph.NodeIndex node;

			// Token: 0x04004033 RID: 16435
			public NodeGraphSpider.StepInfo previousStep;
		}
	}
}
