using System;
using HG;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A8A RID: 2698
	public class TimerQueue
	{
		// Token: 0x06003DE7 RID: 15847 RVA: 0x000FF9AC File Offset: 0x000FDBAC
		public TimerQueue.TimerHandle CreateTimer(float time, Action action)
		{
			time += this.internalTime;
			int position = this.count;
			for (int i = 0; i < this.count; i++)
			{
				if (time < this.timers[i].time)
				{
					position = i;
					break;
				}
			}
			TimerQueue.TimerHandle timerHandle = new TimerQueue.TimerHandle(this.indexAllocator.RequestIndex());
			TimerQueue.Timer timer = new TimerQueue.Timer
			{
				time = time,
				action = action,
				handle = timerHandle
			};
			ArrayUtils.ArrayInsert<TimerQueue.Timer>(ref this.timers, ref this.count, position, timer);
			return timerHandle;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x000FFA40 File Offset: 0x000FDC40
		public void RemoveTimer(TimerQueue.TimerHandle timerHandle)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.timers[i].handle.Equals(timerHandle))
				{
					this.RemoveTimerAt(i);
					return;
				}
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000FFA7F File Offset: 0x000FDC7F
		private void RemoveTimerAt(int i)
		{
			this.indexAllocator.FreeIndex(this.timers[i].handle.uid);
			ArrayUtils.ArrayRemoveAt<TimerQueue.Timer>(this.timers, ref this.count, i, 1);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000FFAB8 File Offset: 0x000FDCB8
		public void Update(float deltaTime)
		{
			this.internalTime += deltaTime;
			int num = 0;
			while (num < this.count && this.timers[num].time <= this.internalTime)
			{
				ArrayUtils.ArrayInsert<Action>(ref this.actionsToCall, ref this.actionsToCallCount, this.actionsToCallCount, this.timers[num].action);
				num++;
			}
			for (int i = this.actionsToCallCount - 1; i >= 0; i--)
			{
				this.RemoveTimerAt(i);
			}
			for (int j = 0; j < this.actionsToCallCount; j++)
			{
				ref Action ptr = ref this.actionsToCall[j];
				try
				{
					ptr();
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
				ptr = null;
			}
			this.actionsToCallCount = 0;
		}

		// Token: 0x04003CB1 RID: 15537
		private float internalTime;

		// Token: 0x04003CB2 RID: 15538
		private int count;

		// Token: 0x04003CB3 RID: 15539
		private TimerQueue.Timer[] timers = Array.Empty<TimerQueue.Timer>();

		// Token: 0x04003CB4 RID: 15540
		private readonly IndexAllocator indexAllocator = new IndexAllocator();

		// Token: 0x04003CB5 RID: 15541
		private Action[] actionsToCall = Array.Empty<Action>();

		// Token: 0x04003CB6 RID: 15542
		private int actionsToCallCount;

		// Token: 0x02000A8B RID: 2699
		public struct TimerHandle : IEquatable<TimerQueue.TimerHandle>
		{
			// Token: 0x06003DEC RID: 15852 RVA: 0x000FFBB1 File Offset: 0x000FDDB1
			public TimerHandle(int uid)
			{
				this.uid = uid;
			}

			// Token: 0x06003DED RID: 15853 RVA: 0x000FFBBA File Offset: 0x000FDDBA
			public bool Equals(TimerQueue.TimerHandle other)
			{
				return this.uid == other.uid;
			}

			// Token: 0x06003DEE RID: 15854 RVA: 0x000FFBCA File Offset: 0x000FDDCA
			public override bool Equals(object obj)
			{
				return obj != null && obj is TimerQueue.TimerHandle && this.Equals((TimerQueue.TimerHandle)obj);
			}

			// Token: 0x06003DEF RID: 15855 RVA: 0x000FFBE7 File Offset: 0x000FDDE7
			public override int GetHashCode()
			{
				return this.uid;
			}

			// Token: 0x04003CB7 RID: 15543
			public static readonly TimerQueue.TimerHandle invalid = new TimerQueue.TimerHandle(-1);

			// Token: 0x04003CB8 RID: 15544
			public readonly int uid;
		}

		// Token: 0x02000A8C RID: 2700
		private struct Timer
		{
			// Token: 0x04003CB9 RID: 15545
			public float time;

			// Token: 0x04003CBA RID: 15546
			public Action action;

			// Token: 0x04003CBB RID: 15547
			public TimerQueue.TimerHandle handle;
		}
	}
}
