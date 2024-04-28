using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace HG.Coroutines
{
	// Token: 0x0200001D RID: 29
	public class CoroutineRunner
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004F3C File Offset: 0x0000313C
		public CoroutineRunner(Func<IEnumerator> coroutine, CoroutineRunner.OnYieldDelegate onYield = null)
		{
			this.coroutineStack = new Stack<IEnumerator>();
			this.currentRunStopwatch = new Stopwatch();
			this.onYield = onYield;
			this.coroutineStack.Push(coroutine());
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004F94 File Offset: 0x00003194
		public bool Run()
		{
			this.elapsedStopwatch.Start();
			this.currentRunStopwatch.Restart();
			try
			{
				while (this.coroutineStack.Count > 0)
				{
					IEnumerator enumerator = this.coroutineStack.Peek();
					if (!enumerator.MoveNext())
					{
						this.coroutineStack.Pop();
					}
					else
					{
						IEnumerator item;
						if ((item = (enumerator.Current as IEnumerator)) != null)
						{
							this.coroutineStack.Push(item);
						}
						bool flag = false;
						CoroutineRunner.OnYieldDelegate onYieldDelegate = this.onYield;
						if (onYieldDelegate != null)
						{
							onYieldDelegate(enumerator.Current, ref flag);
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			catch (Exception)
			{
				this.coroutineStack.Clear();
				throw;
			}
			finally
			{
				this.currentRunStopwatch.Stop();
				this.elapsedStopwatch.Stop();
			}
			return this.coroutineStack.Count > 0;
		}

		// Token: 0x04000037 RID: 55
		public CoroutineRunner.OnYieldDelegate onYield;

		// Token: 0x04000038 RID: 56
		public readonly Stopwatch currentRunStopwatch = new Stopwatch();

		// Token: 0x04000039 RID: 57
		public readonly Stopwatch elapsedStopwatch = new Stopwatch();

		// Token: 0x0400003A RID: 58
		private Stack<IEnumerator> coroutineStack;

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x06000155 RID: 341
		public delegate void OnYieldDelegate(object result, ref bool shouldRest);
	}
}
