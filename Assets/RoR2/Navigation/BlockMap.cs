using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using Unity.Collections;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B2A RID: 2858
	public class BlockMap<TItem, TItemPositionGetter> where TItemPositionGetter : IPosition3Getter<TItem>
	{
		// Token: 0x06004114 RID: 16660 RVA: 0x0010D6D9 File Offset: 0x0010B8D9
		public BlockMap() : this(new Vector3(15f, 30f, 15f))
		{
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0010D6F8 File Offset: 0x0010B8F8
		public BlockMap(Vector3 cellSize)
		{
			this.cellSize = cellSize;
			this.invCellSize = new Vector3(1f / cellSize.x, 1f / cellSize.y, 1f / cellSize.z);
			this.Reset();
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x0010D75D File Offset: 0x0010B95D
		public void Reset()
		{
			this.worldBoundingBox = default(Bounds);
			ArrayUtils.Clear<TItem>(this.itemsPackedByCell, ref this.itemCount);
			this.cellCounts = Vector3Int.zero;
			this.cellCount1D = 0;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x0010D790 File Offset: 0x0010B990
		public void Set<T>(T newItems, int newItemsLength, TItemPositionGetter newItemPositionGetter) where T : IList<TItem>
		{
			this.Reset();
			using (NativeArray<BlockMapCellIndex> nativeArray = new NativeArray<BlockMapCellIndex>(newItemsLength, Allocator.Temp, NativeArrayOptions.UninitializedMemory))
			{
				try
				{
					this.itemPositionGetter = newItemPositionGetter;
					if (newItems.Count > 0)
					{
						this.worldBoundingBox = new Bounds(this.itemPositionGetter.GetPosition3(newItems[0]), Vector3.zero);
						for (int i = 1; i < newItems.Count; i++)
						{
							this.worldBoundingBox.Encapsulate(this.itemPositionGetter.GetPosition3(newItems[i]));
						}
					}
					this.worldBoundingBox.min = this.worldBoundingBox.min - Vector3.one;
					this.worldBoundingBox.max = this.worldBoundingBox.max + Vector3.one;
					Vector3 size = this.worldBoundingBox.size;
					this.cellCounts = Vector3Int.Max(Vector3Int.CeilToInt(Vector3.Scale(size, this.invCellSize)), Vector3Int.one);
					this.cellCount1D = this.cellCounts.x * this.cellCounts.y * this.cellCounts.z;
					Array.Resize<BlockMapCell>(ref this.cells, this.cellCount1D);
					Array.Clear(this.cells, 0, this.cells.Length);
					Vector3 min = this.worldBoundingBox.min;
					for (int j = 0; j < newItems.Count; j++)
					{
						Vector3 position = this.itemPositionGetter.GetPosition3(newItems[j]);
						Vector3Int vector3Int = this.WorldPositionToGridPosFloor(position);
						BlockMapCellIndex blockMapCellIndex = this.GridPosToCellIndex(vector3Int);
						nativeArray[j] = blockMapCellIndex;
						BlockMapCell[] array = this.cells;
						BlockMapCellIndex blockMapCellIndex2 = blockMapCellIndex;
						array[(int)blockMapCellIndex2].itemCount = array[(int)blockMapCellIndex2].itemCount + 1;
					}
					int num = 0;
					for (int k = 0; k < this.cells.Length; k++)
					{
						ref BlockMapCell ptr = ref this.cells[k];
						ptr.itemStartIndex = num;
						num += ptr.itemCount;
					}
					this.itemCount = newItems.Count;
					ArrayUtils.EnsureCapacity<TItem>(ref this.itemsPackedByCell, this.itemCount);
					NativeArray<int> nativeArray2 = new NativeArray<int>(this.cells.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
					for (int l = 0; l < this.itemCount; l++)
					{
						BlockMapCellIndex blockMapCellIndex3 = nativeArray[l];
						ref BlockMapCell ptr2 = ref this.cells[(int)blockMapCellIndex3];
						TItem titem = newItems[l];
						int index = (int)blockMapCellIndex3;
						int num2 = nativeArray2[index];
						nativeArray2[index] = num2 + 1;
						int num3 = num2;
						this.itemsPackedByCell[ptr2.itemStartIndex + num3] = titem;
					}
					nativeArray2.Dispose();
				}
				catch
				{
					this.Reset();
					throw;
				}
			}
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x0010DA98 File Offset: 0x0010BC98
		private Vector3Int WorldPositionToGridPosFloor(in Vector3 worldPosition)
		{
			Vector3 vector = worldPosition - this.worldBoundingBox.min;
			return this.LocalPositionToGridPosFloor(vector);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x0010DAC4 File Offset: 0x0010BCC4
		private Vector3Int WorldPositionToGridPosCeil(in Vector3 worldPosition)
		{
			Vector3 vector = worldPosition - this.worldBoundingBox.min;
			return this.LocalPositionToGridPosCeil(vector);
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x0010DAF0 File Offset: 0x0010BCF0
		private Vector3Int LocalPositionToGridPosFloor(in Vector3 localPosition)
		{
			return Vector3Int.FloorToInt(Vector3.Scale(localPosition, this.invCellSize));
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0010DB08 File Offset: 0x0010BD08
		private Vector3Int LocalPositionToGridPosCeil(in Vector3 localPosition)
		{
			return Vector3Int.CeilToInt(Vector3.Scale(localPosition, this.invCellSize));
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0010DB20 File Offset: 0x0010BD20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private BlockMapCellIndex GridPosToCellIndex(in Vector3Int gridPos)
		{
			Vector3Int vector3Int = gridPos;
			BlockMapCellIndex blockMapCellIndex = (BlockMapCellIndex)(vector3Int.y * this.cellCounts.z);
			vector3Int = gridPos;
			BlockMapCellIndex blockMapCellIndex2 = (blockMapCellIndex + vector3Int.z) * (BlockMapCellIndex)this.cellCounts.x;
			vector3Int = gridPos;
			return blockMapCellIndex2 + vector3Int.x;
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0010DB74 File Offset: 0x0010BD74
		private Vector3Int CellIndexToGridPos(BlockMapCellIndex cellIndex)
		{
			Vector3Int zero = Vector3Int.zero;
			int num = this.cellCounts.x * this.cellCounts.z;
			zero.y = (int)(cellIndex / (BlockMapCellIndex)num);
			int num2 = cellIndex - (BlockMapCellIndex)(zero.y * num);
			zero.z = num2 / this.cellCounts.x;
			num2 -= zero.z * this.cellCounts.x;
			zero.x = num2;
			return zero;
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x0010DBEC File Offset: 0x0010BDEC
		private Bounds GridPosToBounds(in Vector3Int gridPos)
		{
			Vector3 vector = gridPos;
			vector.Scale(this.cellSize);
			vector += this.worldBoundingBox.min;
			Bounds result = default(Bounds);
			result.SetMinMax(vector, vector + this.cellSize);
			return result;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x0010DC44 File Offset: 0x0010BE44
		private bool GridPosIsValid(Vector3Int gridPos)
		{
			return gridPos.x >= 0 && gridPos.y >= 0 && gridPos.z >= 0 && gridPos.x < this.cellCounts.x && gridPos.y < this.cellCounts.y && gridPos.z < this.cellCounts.z;
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0010DCB0 File Offset: 0x0010BEB0
		public void GetItemsInSphere(Vector3 point, float radius, List<TItem> dest)
		{
			Bounds sphereBounds = BlockMap<TItem, TItemPositionGetter>.GetSphereBounds(point, radius);
			int count = dest.Count;
			this.GetBoundOverlappingCellItems(sphereBounds, dest);
			float num = radius * radius;
			for (int i = dest.Count - 1; i >= count; i--)
			{
				TItem item = dest[i];
				if ((this.itemPositionGetter.GetPosition3(item) - point).sqrMagnitude > num)
				{
					dest.RemoveAt(i);
				}
			}
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x0010DD24 File Offset: 0x0010BF24
		public void GetItemsInBounds(in Bounds bounds, List<TItem> dest)
		{
			int count = dest.Count;
			this.GetBoundOverlappingCellItems(bounds, dest);
			for (int i = dest.Count - 1; i >= count; i--)
			{
				TItem item = dest[i];
				Vector3 position = this.itemPositionGetter.GetPosition3(item);
				if (!this.worldBoundingBox.Contains(position))
				{
					dest.RemoveAt(i);
				}
			}
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0010DD84 File Offset: 0x0010BF84
		private void GetBoundOverlappingCellItems(in Bounds bounds, List<TItem> dest)
		{
			List<BlockMapCellIndex> list = CollectionPool<BlockMapCellIndex, List<BlockMapCellIndex>>.RentCollection();
			this.GetBoundOverlappingCells(bounds, list);
			foreach (BlockMapCellIndex blockMapCellIndex in list)
			{
				ref BlockMapCell ptr = ref this.cells[(int)blockMapCellIndex];
				int i = ptr.itemStartIndex;
				int num = ptr.itemStartIndex + ptr.itemCount;
				while (i < num)
				{
					dest.Add(this.itemsPackedByCell[i]);
					i++;
				}
			}
			list = CollectionPool<BlockMapCellIndex, List<BlockMapCellIndex>>.ReturnCollection(list);
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0010DE2C File Offset: 0x0010C02C
		private void GetBoundOverlappingCells(Bounds bounds, List<BlockMapCellIndex> dest)
		{
			Vector3 vector = bounds.min;
			Vector3Int vector3Int = this.WorldPositionToGridPosFloor(vector);
			vector = bounds.max;
			Vector3Int vector3Int2 = this.WorldPositionToGridPosCeil(vector);
			vector3Int.Clamp(Vector3Int.zero, this.cellCounts);
			vector3Int2.Clamp(Vector3Int.zero, this.cellCounts);
			Vector3Int vector3Int3 = vector3Int;
			vector3Int3.y = vector3Int.y;
			while (vector3Int3.y < vector3Int2.y)
			{
				vector3Int3.z = vector3Int.z;
				int num;
				while (vector3Int3.z < vector3Int2.z)
				{
					vector3Int3.x = vector3Int.x;
					while (vector3Int3.x < vector3Int2.x)
					{
						dest.Add(this.GridPosToCellIndex(vector3Int3));
						num = vector3Int3.x + 1;
						vector3Int3.x = num;
					}
					num = vector3Int3.z + 1;
					vector3Int3.z = num;
				}
				num = vector3Int3.y + 1;
				vector3Int3.y = num;
			}
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x0010DF30 File Offset: 0x0010C130
		public bool GetNearestItemWhichPassesFilter<TItemFilter>(Vector3 position, float maxDistance, ref TItemFilter filter, out TItem dest) where TItemFilter : IBlockMapSearchFilter<TItem>
		{
			BlockMap<TItem, TItemPositionGetter>.SingleSearchResultHandler singleSearchResultHandler = default(BlockMap<TItem, TItemPositionGetter>.SingleSearchResultHandler);
			this.GetNearestItemsWhichPassFilter<TItemFilter, BlockMap<TItem, TItemPositionGetter>.SingleSearchResultHandler>(position, maxDistance, ref filter, ref singleSearchResultHandler);
			dest = (singleSearchResultHandler.foundResult ? singleSearchResultHandler.result : default(TItem));
			return singleSearchResultHandler.foundResult;
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x0010DF7C File Offset: 0x0010C17C
		public void GetNearestItemsWhichPassFilter<TItemFilter>(Vector3 position, float maxDistance, ref TItemFilter filter, List<TItem> dest) where TItemFilter : IBlockMapSearchFilter<TItem>
		{
			BlockMap<TItem, TItemPositionGetter>.ListWriteSearchResultHandler listWriteSearchResultHandler = new BlockMap<TItem, TItemPositionGetter>.ListWriteSearchResultHandler(dest);
			this.GetNearestItemsWhichPassFilter<TItemFilter, BlockMap<TItem, TItemPositionGetter>.ListWriteSearchResultHandler>(position, maxDistance, ref filter, ref listWriteSearchResultHandler);
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x0010DFA0 File Offset: 0x0010C1A0
		private void GetNearestItemsWhichPassFilter<TItemFilter, TSearchResultHandler>(Vector3 position, float maxDistance, ref TItemFilter filter, ref TSearchResultHandler searchResultHandler) where TItemFilter : IBlockMapSearchFilter<TItem> where TSearchResultHandler : BlockMap<TItem, TItemPositionGetter>.ISearchResultHandler
		{
			BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_0<TItemFilter, TSearchResultHandler> CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.position = position;
			CS$<>8__locals1.maxDistance = maxDistance;
			CS$<>8__locals1.maxDistance = Mathf.Max(CS$<>8__locals1.maxDistance, 0f);
			CS$<>8__locals1.candidatesInsideRadius = CollectionPool<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair, List<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair>>.RentCollection();
			CS$<>8__locals1.candidatesOutsideRadius = CollectionPool<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair, List<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair>>.RentCollection();
			try
			{
				Vector3Int vector3Int = this.WorldPositionToGridPosFloor(CS$<>8__locals1.position);
				Bounds currentBounds = this.GridPosToBounds(vector3Int);
				BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_1<TItemFilter, TSearchResultHandler> CS$<>8__locals2;
				CS$<>8__locals2.currentBounds = currentBounds;
				float additionalRadius = this.cellSize.ComponentMin();
				CS$<>8__locals2.radius = this.DistanceToNearestWall(CS$<>8__locals1.position, CS$<>8__locals2.currentBounds);
				CS$<>8__locals2.radiusSqr = CS$<>8__locals2.radius * CS$<>8__locals2.radius;
				BlockMapCellIndex cellIndex = this.GridPosToCellIndex(vector3Int);
				CS$<>8__locals2.visitedCells = new BoundsInt(vector3Int, Vector3Int.one);
				CS$<>8__locals2.visitedCellCount = 0;
				bool flag = this.GridPosIsValid(vector3Int);
				if (flag)
				{
					this.<GetNearestItemsWhichPassFilter>g__VisitCell|33_1<TItemFilter, TSearchResultHandler>(cellIndex, ref CS$<>8__locals1, ref CS$<>8__locals2);
				}
				if (!flag)
				{
					this.<GetNearestItemsWhichPassFilter>g__AddRadius|33_2<TItemFilter, TSearchResultHandler>(Mathf.Sqrt(this.worldBoundingBox.SqrDistance(CS$<>8__locals1.position)), ref CS$<>8__locals1, ref CS$<>8__locals2);
				}
				bool flag2 = true;
				while (flag2)
				{
					while (CS$<>8__locals1.candidatesInsideRadius.Count > 0)
					{
						int num = -1;
						float num2 = float.PositiveInfinity;
						for (int i = 0; i < CS$<>8__locals1.candidatesInsideRadius.Count; i++)
						{
							BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair itemDistanceSqrPair = CS$<>8__locals1.candidatesInsideRadius[i];
							if (itemDistanceSqrPair.distanceSqr < num2)
							{
								num2 = itemDistanceSqrPair.distanceSqr;
								num = i;
							}
						}
						if (num != -1)
						{
							BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair itemDistanceSqrPair2 = CS$<>8__locals1.candidatesInsideRadius[num];
							CS$<>8__locals1.candidatesInsideRadius.RemoveAt(num);
							bool flag3 = false;
							if (filter.CheckItem(this.itemsPackedByCell[itemDistanceSqrPair2.itemIndex], ref flag3) && !searchResultHandler.OnEncounterResult(this.itemsPackedByCell[itemDistanceSqrPair2.itemIndex]))
							{
								return;
							}
							if (flag3)
							{
								return;
							}
						}
					}
					flag2 = this.<GetNearestItemsWhichPassFilter>g__AddRadius|33_2<TItemFilter, TSearchResultHandler>(additionalRadius, ref CS$<>8__locals1, ref CS$<>8__locals2);
				}
			}
			finally
			{
				CS$<>8__locals1.candidatesOutsideRadius = CollectionPool<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair, List<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair>>.ReturnCollection(CS$<>8__locals1.candidatesOutsideRadius);
				CS$<>8__locals1.candidatesInsideRadius = CollectionPool<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair, List<BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair>>.ReturnCollection(CS$<>8__locals1.candidatesInsideRadius);
			}
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x0010E1D4 File Offset: 0x0010C3D4
		private float DistanceToNearestWall(in Vector3 position, in Bounds bounds)
		{
			Vector3 a = position;
			Bounds bounds2 = bounds;
			Vector3 vector = a - bounds2.min;
			bounds2 = bounds;
			Vector3 vector2 = bounds2.max - position;
			float num = vector.x;
			num = ((num < vector.y) ? num : vector.y);
			num = ((num < vector.z) ? num : vector.z);
			num = ((num < vector2.x) ? num : vector2.x);
			num = ((num < vector2.y) ? num : vector2.y);
			return (num < vector2.z) ? num : vector2.z;
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x0010E27C File Offset: 0x0010C47C
		private float DistanceToNearestWall(in Bounds innerBounds, in Bounds outerBounds)
		{
			Bounds bounds = innerBounds;
			Vector3 min = bounds.min;
			bounds = outerBounds;
			Vector3 vector = min - bounds.min;
			bounds = outerBounds;
			Vector3 max = bounds.max;
			bounds = innerBounds;
			Vector3 vector2 = max - bounds.max;
			float num = vector.x;
			num = ((num < vector.y) ? num : vector.y);
			num = ((num < vector.z) ? num : vector.z);
			num = ((num < vector2.x) ? num : vector2.x);
			num = ((num < vector2.y) ? num : vector2.y);
			return (num < vector2.z) ? num : vector2.z;
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x0010E334 File Offset: 0x0010C534
		private static Bounds GetSphereBounds(Vector3 origin, float radius)
		{
			float num = radius * 2f;
			return new Bounds(origin, new Vector3(num, num, num));
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x0010E358 File Offset: 0x0010C558
		private BoundsInt WorldBoundsOverlappingToGridBounds(Bounds worldBounds)
		{
			Vector3 vector = worldBounds.min;
			Vector3Int minPosition = this.WorldPositionToGridPosFloor(vector);
			vector = worldBounds.max;
			Vector3Int maxPosition = this.WorldPositionToGridPosCeil(vector);
			BoundsInt result = default(BoundsInt);
			result.SetMinMax(minPosition, maxPosition);
			return result;
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x0010E39C File Offset: 0x0010C59C
		private BoundsInt WorldBoundsToOverlappingGridBoundsClamped(Bounds worldBounds)
		{
			Vector3 vector = worldBounds.min;
			Vector3Int minPosition = this.WorldPositionToGridPosFloor(vector);
			vector = worldBounds.max;
			Vector3Int maxPosition = this.WorldPositionToGridPosCeil(vector);
			minPosition.Clamp(Vector3Int.zero, this.cellCounts);
			maxPosition.Clamp(Vector3Int.zero, this.cellCounts);
			BoundsInt result = default(BoundsInt);
			result.SetMinMax(minPosition, maxPosition);
			return result;
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x0010E404 File Offset: 0x0010C604
		private BlockMap<TItem, TItemPositionGetter>.GridEnumerable ValidGridPositionsInBounds(Bounds bounds)
		{
			Vector3 vector = bounds.min;
			Vector3Int startPos = this.WorldPositionToGridPosFloor(vector);
			vector = bounds.max;
			Vector3Int endPos = this.WorldPositionToGridPosCeil(vector);
			startPos.Clamp(Vector3Int.zero, this.cellCounts);
			endPos.Clamp(Vector3Int.zero, this.cellCounts);
			return new BlockMap<TItem, TItemPositionGetter>.GridEnumerable(startPos, endPos);
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x0010E460 File Offset: 0x0010C660
		private BlockMap<TItem, TItemPositionGetter>.GridEnumerable ValidGridPositionsInBoundsWithExclusion(Bounds bounds, BoundsInt excludedCells)
		{
			Vector3 vector = bounds.min;
			Vector3Int startPos = this.WorldPositionToGridPosFloor(vector);
			vector = bounds.max;
			Vector3Int endPos = this.WorldPositionToGridPosCeil(vector);
			startPos.Clamp(Vector3Int.zero, this.cellCounts - Vector3Int.one);
			endPos.Clamp(Vector3Int.zero, this.cellCounts - Vector3Int.one);
			return new BlockMap<TItem, TItemPositionGetter>.GridEnumerable(startPos, endPos);
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x0010E4D0 File Offset: 0x0010C6D0
		[CompilerGenerated]
		private void <GetNearestItemsWhichPassFilter>g__VisitItem|33_0<TItemFilter, TSearchResultHandler>(int itemIndex, ref BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_0<TItemFilter, TSearchResultHandler> A_2, ref BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_1<TItemFilter, TSearchResultHandler> A_3) where TItemFilter : IBlockMapSearchFilter<TItem> where TSearchResultHandler : BlockMap<TItem, TItemPositionGetter>.ISearchResultHandler
		{
			Vector3 position = this.itemPositionGetter.GetPosition3(this.itemsPackedByCell[itemIndex]);
			BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair itemDistanceSqrPair = new BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair
			{
				itemIndex = itemIndex,
				distanceSqr = (position - A_2.position).sqrMagnitude
			};
			((itemDistanceSqrPair.distanceSqr < A_3.radiusSqr) ? A_2.candidatesInsideRadius : A_2.candidatesOutsideRadius).Add(itemDistanceSqrPair);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x0010E54C File Offset: 0x0010C74C
		[CompilerGenerated]
		private void <GetNearestItemsWhichPassFilter>g__VisitCell|33_1<TItemFilter, TSearchResultHandler>(BlockMapCellIndex cellIndex, ref BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_0<TItemFilter, TSearchResultHandler> A_2, ref BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_1<TItemFilter, TSearchResultHandler> A_3) where TItemFilter : IBlockMapSearchFilter<TItem> where TSearchResultHandler : BlockMap<TItem, TItemPositionGetter>.ISearchResultHandler
		{
			int visitedCellCount = A_3.visitedCellCount + 1;
			A_3.visitedCellCount = visitedCellCount;
			ref BlockMapCell ptr = ref this.cells[(int)cellIndex];
			int i = ptr.itemStartIndex;
			int num = ptr.itemStartIndex + ptr.itemCount;
			while (i < num)
			{
				this.<GetNearestItemsWhichPassFilter>g__VisitItem|33_0<TItemFilter, TSearchResultHandler>(i, ref A_2, ref A_3);
				i++;
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x0010E5A0 File Offset: 0x0010C7A0
		[CompilerGenerated]
		private bool <GetNearestItemsWhichPassFilter>g__AddRadius|33_2<TItemFilter, TSearchResultHandler>(float additionalRadius, ref BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_0<TItemFilter, TSearchResultHandler> A_2, ref BlockMap<TItem, TItemPositionGetter>.<>c__DisplayClass33_1<TItemFilter, TSearchResultHandler> A_3) where TItemFilter : IBlockMapSearchFilter<TItem> where TSearchResultHandler : BlockMap<TItem, TItemPositionGetter>.ISearchResultHandler
		{
			A_3.radius += additionalRadius;
			bool flag = A_3.radius >= A_2.maxDistance;
			if (flag)
			{
				A_3.radius = A_2.maxDistance;
			}
			A_3.radiusSqr = A_3.radius * A_3.radius;
			A_3.currentBounds = BlockMap<TItem, TItemPositionGetter>.GetSphereBounds(A_2.position, A_3.radius);
			for (int i = A_2.candidatesOutsideRadius.Count - 1; i >= 0; i--)
			{
				BlockMap<TItem, TItemPositionGetter>.ItemDistanceSqrPair itemDistanceSqrPair = A_2.candidatesOutsideRadius[i];
				if (itemDistanceSqrPair.distanceSqr < A_3.radiusSqr)
				{
					A_2.candidatesOutsideRadius.RemoveAt(i);
					A_2.candidatesInsideRadius.Add(itemDistanceSqrPair);
				}
			}
			bool flag2 = A_3.visitedCellCount >= this.cellCount1D;
			if (!flag2)
			{
				BoundsInt visitedCells = this.WorldBoundsToOverlappingGridBoundsClamped(A_3.currentBounds);
				foreach (Vector3Int vector3Int in new BoundsIntDifferenceEnumerable(ref visitedCells, ref A_3.visitedCells))
				{
					this.<GetNearestItemsWhichPassFilter>g__VisitCell|33_1<TItemFilter, TSearchResultHandler>(this.GridPosToCellIndex(vector3Int), ref A_2, ref A_3);
				}
				A_3.visitedCells = visitedCells;
			}
			if (A_2.candidatesInsideRadius.Count == 0)
			{
				if (flag)
				{
					return false;
				}
				if (flag2 && A_2.candidatesOutsideRadius.Count == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003F8D RID: 16269
		private const bool debugDraw = false;

		// Token: 0x04003F8E RID: 16270
		private const bool aggressiveDebug = false;

		// Token: 0x04003F8F RID: 16271
		private Vector3 cellSize;

		// Token: 0x04003F90 RID: 16272
		private Vector3 invCellSize;

		// Token: 0x04003F91 RID: 16273
		private Bounds worldBoundingBox;

		// Token: 0x04003F92 RID: 16274
		private BlockMapCell[] cells = Array.Empty<BlockMapCell>();

		// Token: 0x04003F93 RID: 16275
		private int cellCount1D;

		// Token: 0x04003F94 RID: 16276
		private Vector3Int cellCounts;

		// Token: 0x04003F95 RID: 16277
		private TItem[] itemsPackedByCell = Array.Empty<TItem>();

		// Token: 0x04003F96 RID: 16278
		private int itemCount;

		// Token: 0x04003F97 RID: 16279
		private TItemPositionGetter itemPositionGetter;

		// Token: 0x02000B2B RID: 2859
		private struct ItemDistanceSqrPair
		{
			// Token: 0x04003F98 RID: 16280
			public int itemIndex;

			// Token: 0x04003F99 RID: 16281
			public float distanceSqr;
		}

		// Token: 0x02000B2C RID: 2860
		private interface ISearchResultHandler
		{
			// Token: 0x06004131 RID: 16689
			bool OnEncounterResult(TItem result);
		}

		// Token: 0x02000B2D RID: 2861
		private struct SingleSearchResultHandler : BlockMap<TItem, TItemPositionGetter>.ISearchResultHandler
		{
			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06004132 RID: 16690 RVA: 0x0010E6DD File Offset: 0x0010C8DD
			// (set) Token: 0x06004133 RID: 16691 RVA: 0x0010E6E5 File Offset: 0x0010C8E5
			public bool foundResult { get; private set; }

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06004134 RID: 16692 RVA: 0x0010E6EE File Offset: 0x0010C8EE
			// (set) Token: 0x06004135 RID: 16693 RVA: 0x0010E6F6 File Offset: 0x0010C8F6
			public TItem result { get; private set; }

			// Token: 0x06004136 RID: 16694 RVA: 0x0010E6FF File Offset: 0x0010C8FF
			public bool OnEncounterResult(TItem result)
			{
				this.foundResult = true;
				this.result = result;
				return false;
			}
		}

		// Token: 0x02000B2E RID: 2862
		private struct ListWriteSearchResultHandler : BlockMap<TItem, TItemPositionGetter>.ISearchResultHandler
		{
			// Token: 0x06004137 RID: 16695 RVA: 0x0010E710 File Offset: 0x0010C910
			public ListWriteSearchResultHandler(List<TItem> dest)
			{
				this.dest = dest;
			}

			// Token: 0x06004138 RID: 16696 RVA: 0x0010E719 File Offset: 0x0010C919
			public bool OnEncounterResult(TItem result)
			{
				this.dest.Add(result);
				return true;
			}

			// Token: 0x04003F9C RID: 16284
			private readonly List<TItem> dest;
		}

		// Token: 0x02000B2F RID: 2863
		private struct GridEnumerator
		{
			// Token: 0x06004139 RID: 16697 RVA: 0x0010E728 File Offset: 0x0010C928
			public GridEnumerator(in Vector3Int startCellIndex, in Vector3Int endCellIndex)
			{
				this.startPos = startCellIndex;
				this.endPos = endCellIndex;
				this._current = startCellIndex;
				int x = this._current.x - 1;
				this._current.x = x;
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x0600413A RID: 16698 RVA: 0x0010E76E File Offset: 0x0010C96E
			public Vector3Int Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x0600413B RID: 16699 RVA: 0x0010E778 File Offset: 0x0010C978
			public bool MoveNext()
			{
				int num = this._current.x + 1;
				this._current.x = num;
				if (num >= this.endPos.x)
				{
					this._current.x = this.startPos.x;
					num = this._current.z + 1;
					this._current.z = num;
					if (num >= this.endPos.z)
					{
						this._current.z = this.startPos.z;
						num = this._current.y + 1;
						this._current.y = num;
						if (num >= this.endPos.y)
						{
							this._current.y = this.startPos.y;
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0600413C RID: 16700 RVA: 0x0010E848 File Offset: 0x0010CA48
			public void Reset()
			{
				this._current = this.startPos;
			}

			// Token: 0x04003F9D RID: 16285
			private readonly Vector3Int startPos;

			// Token: 0x04003F9E RID: 16286
			private readonly Vector3Int endPos;

			// Token: 0x04003F9F RID: 16287
			private Vector3Int _current;
		}

		// Token: 0x02000B30 RID: 2864
		private struct GridEnumerable
		{
			// Token: 0x0600413D RID: 16701 RVA: 0x0010E856 File Offset: 0x0010CA56
			public GridEnumerable(Vector3Int startPos, Vector3Int endPos)
			{
				this.startPos = startPos;
				this.endPos = endPos;
			}

			// Token: 0x0600413E RID: 16702 RVA: 0x0010E866 File Offset: 0x0010CA66
			public BlockMap<TItem, TItemPositionGetter>.GridEnumerator GetEnumerator()
			{
				return new BlockMap<TItem, TItemPositionGetter>.GridEnumerator(ref this.startPos, ref this.endPos);
			}

			// Token: 0x04003FA0 RID: 16288
			private readonly Vector3Int startPos;

			// Token: 0x04003FA1 RID: 16289
			private readonly Vector3Int endPos;
		}
	}
}
