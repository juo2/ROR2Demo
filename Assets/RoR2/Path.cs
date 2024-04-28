using System;
using System.Collections.Generic;
using RoR2.Navigation;

namespace RoR2
{
	// Token: 0x0200097F RID: 2431
	public class Path : IDisposable
	{
		// Token: 0x06003720 RID: 14112 RVA: 0x000E8BF3 File Offset: 0x000E6DF3
		private static Path.Waypoint[] GetWaypointsBuffer()
		{
			if (Path.waypointsBufferPool.Count == 0)
			{
				return new Path.Waypoint[64];
			}
			return Path.waypointsBufferPool.Pop();
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000E8C13 File Offset: 0x000E6E13
		private static void FreeWaypointsBuffer(Path.Waypoint[] buffer)
		{
			if (buffer.Length != 64)
			{
				return;
			}
			Path.waypointsBufferPool.Push(buffer);
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000E8C28 File Offset: 0x000E6E28
		public Path(NodeGraph nodeGraph)
		{
			this.nodeGraph = nodeGraph;
			this.waypointsBuffer = Path.GetWaypointsBuffer();
			this.firstWaypointIndex = this.waypointsBuffer.Length;
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000E8C50 File Offset: 0x000E6E50
		public void Dispose()
		{
			Path.FreeWaypointsBuffer(this.waypointsBuffer);
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000E8C5D File Offset: 0x000E6E5D
		// (set) Token: 0x06003725 RID: 14117 RVA: 0x000E8C65 File Offset: 0x000E6E65
		public NodeGraph nodeGraph { get; private set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000E8C6E File Offset: 0x000E6E6E
		// (set) Token: 0x06003727 RID: 14119 RVA: 0x000E8C76 File Offset: 0x000E6E76
		public PathStatus status { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000E8C7F File Offset: 0x000E6E7F
		// (set) Token: 0x06003729 RID: 14121 RVA: 0x000E8C87 File Offset: 0x000E6E87
		public int waypointsCount { get; private set; }

		// Token: 0x17000524 RID: 1316
		public Path.Waypoint this[int i]
		{
			get
			{
				return this.waypointsBuffer[this.firstWaypointIndex + i];
			}
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000E8CA8 File Offset: 0x000E6EA8
		public void PushWaypointToFront(NodeGraph.NodeIndex nodeIndex, float minJumpHeight)
		{
			if (this.waypointsCount + 1 >= this.waypointsBuffer.Length)
			{
				Path.Waypoint[] array = this.waypointsBuffer;
				this.waypointsBuffer = new Path.Waypoint[this.waypointsCount + 32];
				Array.Copy(array, 0, this.waypointsBuffer, this.waypointsBuffer.Length - array.Length, array.Length);
				Path.FreeWaypointsBuffer(array);
			}
			int num = this.waypointsBuffer.Length;
			int num2 = this.waypointsCount + 1;
			this.waypointsCount = num2;
			this.firstWaypointIndex = num - num2;
			this.waypointsBuffer[this.firstWaypointIndex] = new Path.Waypoint
			{
				nodeIndex = nodeIndex,
				minJumpHeight = minJumpHeight
			};
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000E8D50 File Offset: 0x000E6F50
		public void WriteWaypointsToList(List<Path.Waypoint> waypointsList)
		{
			if (waypointsList.Capacity < waypointsList.Count + this.waypointsCount)
			{
				waypointsList.Capacity = waypointsList.Count + this.waypointsCount;
			}
			for (int i = this.firstWaypointIndex; i < this.waypointsBuffer.Length; i++)
			{
				waypointsList.Add(this.waypointsBuffer[i]);
			}
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000E8DAF File Offset: 0x000E6FAF
		public void Clear()
		{
			this.status = PathStatus.Invalid;
			this.waypointsCount = 0;
			this.firstWaypointIndex = this.waypointsBuffer.Length;
		}

		// Token: 0x0400378E RID: 14222
		private static readonly Stack<Path.Waypoint[]> waypointsBufferPool = new Stack<Path.Waypoint[]>();

		// Token: 0x0400378F RID: 14223
		private const int pooledBufferSize = 64;

		// Token: 0x04003792 RID: 14226
		private Path.Waypoint[] waypointsBuffer;

		// Token: 0x04003794 RID: 14228
		private int firstWaypointIndex;

		// Token: 0x02000980 RID: 2432
		public struct Waypoint : IEquatable<Path.Waypoint>
		{
			// Token: 0x0600372F RID: 14127 RVA: 0x000E8DD9 File Offset: 0x000E6FD9
			public bool Equals(Path.Waypoint other)
			{
				return this.nodeIndex.Equals(other.nodeIndex) && this.minJumpHeight.Equals(other.minJumpHeight);
			}

			// Token: 0x06003730 RID: 14128 RVA: 0x000E8E04 File Offset: 0x000E7004
			public override bool Equals(object other)
			{
				if (other is Path.Waypoint)
				{
					Path.Waypoint other2 = (Path.Waypoint)other;
					return this.Equals(other2);
				}
				return false;
			}

			// Token: 0x06003731 RID: 14129 RVA: 0x000E8E2B File Offset: 0x000E702B
			public override int GetHashCode()
			{
				return this.nodeIndex.GetHashCode() * 397 ^ this.minJumpHeight.GetHashCode();
			}

			// Token: 0x04003795 RID: 14229
			public NodeGraph.NodeIndex nodeIndex;

			// Token: 0x04003796 RID: 14230
			public float minJumpHeight;
		}
	}
}
