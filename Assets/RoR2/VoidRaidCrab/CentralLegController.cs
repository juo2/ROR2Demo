using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EntityStates;
using EntityStates.VoidRaidCrab;
using UnityEngine;

namespace RoR2.VoidRaidCrab
{
	// Token: 0x02000B68 RID: 2920
	[RequireComponent(typeof(CharacterBody))]
	public class CentralLegController : MonoBehaviour
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x00112E3A File Offset: 0x0011103A
		private bool hasEffectiveAuthority
		{
			get
			{
				return this.body && this.body.hasEffectiveAuthority;
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x00112E58 File Offset: 0x00111058
		private void Awake()
		{
			this.body = base.GetComponent<CharacterBody>();
			this.bodyStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Body");
			this.legSupportTracker = new CentralLegController.LegSupportTracker(this.legControllers);
			this.bodyStateMachine.nextStateModifier = new EntityStateMachine.ModifyNextStateDelegate(this.ModifyBodyNextState);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x00112EAF File Offset: 0x001110AF
		private void FixedUpdate()
		{
			if (this.hasEffectiveAuthority)
			{
				this.UpdateLegsAuthority();
			}
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x00112EBF File Offset: 0x001110BF
		private void ModifyBodyNextState(EntityStateMachine entityStateMachine, ref EntityState newNextState)
		{
			if (!this.hasEffectiveAuthority)
			{
				return;
			}
			if (this.AreLegsBlockingBodyAnimation())
			{
				newNextState = new WaitForLegsAvailable
				{
					nextState = newNextState
				};
			}
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x00112EE4 File Offset: 0x001110E4
		private void UpdateLegsAuthority()
		{
			bool allLegsNeededForAnimation = !this.bodyStateMachine.IsInMainState();
			this.legSupportTracker.allLegsNeededForAnimation = allLegsNeededForAnimation;
			this.legSupportTracker.Refresh();
			if (this.bodyStateMachine.CanInterruptState(InterruptPriority.Pain))
			{
				bool flag = false;
				for (int i = 0; i < this.legControllers.Length; i++)
				{
					if (this.legControllers[i].IsBreakPending())
					{
						flag = true;
						this.legControllers[i].CompleteBreakAuthority();
					}
				}
				if (flag)
				{
					EntityState nextState;
					if (!this.legSupportTracker.IsLegStateStable())
					{
						nextState = new Collapse();
					}
					else
					{
						nextState = new LegBreakStunState();
					}
					this.bodyStateMachine.SetNextState(nextState);
				}
			}
			this.TryNextStompAuthority();
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x00112F88 File Offset: 0x00111188
		public bool AreLegsBlockingBodyAnimation()
		{
			for (int i = 0; i < this.legControllers.Length; i++)
			{
				if (this.legControllers[i].IsStomping())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x00112FBC File Offset: 0x001111BC
		private void TryNextStompAuthority()
		{
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x00112FCC File Offset: 0x001111CC
		private int WrapToRange(int value, int minInclusive, int maxExclusive)
		{
			if (maxExclusive <= minInclusive)
			{
				throw new ArgumentException("'max' must be greater than 'min'");
			}
			value -= minInclusive;
			int num = maxExclusive - minInclusive;
			int num2 = value % num;
			return minInclusive + ((num2 < 0) ? (num2 + num) : num2);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x00113001 File Offset: 0x00111201
		private LegController GetLegRingBuffered(int i)
		{
			return this.legControllers[this.WrapToRange(i, 0, this.legControllers.Length)];
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x0011301C File Offset: 0x0011121C
		private bool CheckLegsShouldCollapse()
		{
			int num = 0;
			int num2 = this.legControllers.Length / 2;
			for (int i = 0; i < this.legControllers.Length; i++)
			{
				if (this.legControllers[i].IsBroken())
				{
					num++;
					if (num >= num2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x00113064 File Offset: 0x00111264
		public bool AreAnyBreaksPending()
		{
			LegController[] array = this.legControllers;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsBreakPending())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x00113094 File Offset: 0x00111294
		public CentralLegController.SuppressBreaksRequest SuppressBreaks()
		{
			if (this.suppressBreaksRequests.Count == 0)
			{
				LegController[] array = this.legControllers;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].isBreakSuppressed = true;
				}
			}
			CentralLegController.SuppressBreaksRequest suppressBreaksRequest = new CentralLegController.SuppressBreaksRequest(new Action<CentralLegController.SuppressBreaksRequest>(this.RemoveSuppressBreaksRequest));
			this.suppressBreaksRequests.Add(suppressBreaksRequest);
			return suppressBreaksRequest;
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x001130EC File Offset: 0x001112EC
		public void RegenerateAllBrokenServer()
		{
			foreach (LegController legController in this.legControllers)
			{
				if (legController.IsBroken())
				{
					legController.RegenerateServer();
				}
			}
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x00113120 File Offset: 0x00111320
		private void RemoveSuppressBreaksRequest(CentralLegController.SuppressBreaksRequest request)
		{
			this.suppressBreaksRequests.Remove(request);
			if (this.suppressBreaksRequests.Count == 0)
			{
				LegController[] array = this.legControllers;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].isBreakSuppressed = false;
				}
			}
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x00113168 File Offset: 0x00111368
		public bool IsBodyRelated(CharacterBody bodyToCheck)
		{
			if (this.body == bodyToCheck)
			{
				return true;
			}
			foreach (LegController legController in this.legControllers)
			{
				if (bodyToCheck == legController.jointBody)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x001131C0 File Offset: 0x001113C0
		[CompilerGenerated]
		internal static void <CheckLegsShouldCollapse>g__PushSupportToStreak|18_0(bool isLegSupportingWeight, ref CentralLegController.<>c__DisplayClass18_0 A_1)
		{
			if (isLegSupportingWeight)
			{
				A_1.nonSupportingLegStreak = 0;
				return;
			}
			int nonSupportingLegStreak = A_1.nonSupportingLegStreak + 1;
			A_1.nonSupportingLegStreak = nonSupportingLegStreak;
			A_1.nonSupportingLegHighestStreak = Math.Max(A_1.nonSupportingLegHighestStreak, A_1.nonSupportingLegStreak);
		}

		// Token: 0x0400405E RID: 16478
		[SerializeField]
		private LegController[] legControllers;

		// Token: 0x0400405F RID: 16479
		private CharacterBody body;

		// Token: 0x04004060 RID: 16480
		private EntityStateMachine bodyStateMachine;

		// Token: 0x04004061 RID: 16481
		private List<CentralLegController.SuppressBreaksRequest> suppressBreaksRequests = new List<CentralLegController.SuppressBreaksRequest>();

		// Token: 0x04004062 RID: 16482
		private CentralLegController.LegSupportTracker legSupportTracker;

		// Token: 0x04004063 RID: 16483
		private int stompRequesterLegIndex = -1;

		// Token: 0x04004064 RID: 16484
		private const bool useComplexCollapseCheck = false;

		// Token: 0x02000B69 RID: 2921
		public class SuppressBreaksRequest : IDisposable
		{
			// Token: 0x0600426A RID: 17002 RVA: 0x001131FF File Offset: 0x001113FF
			public SuppressBreaksRequest(Action<CentralLegController.SuppressBreaksRequest> onDispose)
			{
				this.onDispose = onDispose;
			}

			// Token: 0x0600426B RID: 17003 RVA: 0x0011320E File Offset: 0x0011140E
			public void Dispose()
			{
				Action<CentralLegController.SuppressBreaksRequest> action = this.onDispose;
				if (action == null)
				{
					return;
				}
				action(this);
			}

			// Token: 0x04004065 RID: 16485
			private Action<CentralLegController.SuppressBreaksRequest> onDispose;
		}

		// Token: 0x02000B6A RID: 2922
		private class LegSupportTracker
		{
			// Token: 0x0600426C RID: 17004 RVA: 0x00113221 File Offset: 0x00111421
			public LegSupportTracker(LegController[] legControllers)
			{
				this.legControllers = legControllers;
				this.legBoolsPool = new FixedSizeArrayPool<bool>(legControllers.Length);
				this.currentLegSupportStates = new bool[legControllers.Length];
				this.halfLegsCount = legControllers.Length / 2;
			}

			// Token: 0x0600426D RID: 17005 RVA: 0x00113258 File Offset: 0x00111458
			public void Refresh()
			{
				for (int i = 0; i < this.legControllers.Length; i++)
				{
					this.currentLegSupportStates[i] = this.legControllers[i].IsSupportingWeight();
				}
			}

			// Token: 0x0600426E RID: 17006 RVA: 0x0011328D File Offset: 0x0011148D
			public bool IsLegStateStable()
			{
				return this.IsLegStateStable(this.currentLegSupportStates);
			}

			// Token: 0x0600426F RID: 17007 RVA: 0x0011329C File Offset: 0x0011149C
			public bool IsLegStateStable(bool[] legSupportStates)
			{
				int num = 0;
				for (int i = 0; i < this.legControllers.Length; i++)
				{
					if (legSupportStates[i])
					{
						num++;
					}
				}
				return num > this.halfLegsCount;
			}

			// Token: 0x06004270 RID: 17008 RVA: 0x001132D0 File Offset: 0x001114D0
			public bool IsLegNeededForSupport(int legIndex)
			{
				if (this.allLegsNeededForAnimation)
				{
					return true;
				}
				bool[] array = this.legBoolsPool.Request();
				Array.Copy(this.currentLegSupportStates, array, this.currentLegSupportStates.Length);
				array[legIndex] = false;
				bool result = !this.IsLegStateStable(array);
				this.legBoolsPool.Return(array);
				return result;
			}

			// Token: 0x04004066 RID: 16486
			public bool allLegsNeededForAnimation;

			// Token: 0x04004067 RID: 16487
			private LegController[] legControllers;

			// Token: 0x04004068 RID: 16488
			private bool[] currentLegSupportStates;

			// Token: 0x04004069 RID: 16489
			private int halfLegsCount;

			// Token: 0x0400406A RID: 16490
			public readonly FixedSizeArrayPool<bool> legBoolsPool;
		}
	}
}
