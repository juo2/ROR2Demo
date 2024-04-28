using System;
using HG;
using UnityEngine;

namespace RoR2.Audio
{
	// Token: 0x02000E57 RID: 3671
	public static class LoopSoundManager
	{
		// Token: 0x06005415 RID: 21525 RVA: 0x0015B270 File Offset: 0x00159470
		public static LoopSoundManager.SoundLoopPtr PlaySoundLoopLocal(GameObject gameObject, LoopSoundDef loopSoundDef)
		{
			gameObject.GetComponent<AkGameObj>();
			LoopSoundManager.SoundLoopHelper soundLoopHelper = gameObject.GetComponent<LoopSoundManager.SoundLoopHelper>();
			if (!soundLoopHelper)
			{
				soundLoopHelper = gameObject.AddComponent<LoopSoundManager.SoundLoopHelper>();
				soundLoopHelper.cachedGameObject = soundLoopHelper.gameObject;
			}
			LoopSoundManager.SoundLoopPtr soundLoopPtr = new LoopSoundManager.SoundLoopPtr(LoopSoundManager.soundLoopHeap.Alloc());
			LoopSoundManager.SoundLoopPtr last = soundLoopHelper.last;
			ref LoopSoundManager.SoundLoopNode @ref = ref soundLoopPtr.GetRef();
			@ref.owner = soundLoopHelper;
			@ref.loopSoundDef = loopSoundDef;
			if (soundLoopHelper.last.isValid)
			{
				@ref.previous = last;
				last.GetRef().next = soundLoopPtr;
			}
			else
			{
				soundLoopHelper.first = soundLoopPtr;
			}
			soundLoopHelper.last = soundLoopPtr;
			@ref.akId = AkSoundEngine.PostEvent(loopSoundDef.startSoundName, gameObject);
			return soundLoopPtr;
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0015B31C File Offset: 0x0015951C
		public static LoopSoundManager.SoundLoopPtr PlaySoundLoopLocalRtpc(GameObject gameObject, LoopSoundDef loopSoundDef, string rtpcName, float rtpcValue)
		{
			LoopSoundManager.SoundLoopPtr result = LoopSoundManager.PlaySoundLoopLocal(gameObject, loopSoundDef);
			ref LoopSoundManager.SoundLoopNode @ref = ref result.GetRef();
			if (@ref.akId != 0U)
			{
				AkSoundEngine.SetRTPCValueByPlayingID(rtpcName, rtpcValue, @ref.akId);
			}
			return result;
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0015B350 File Offset: 0x00159550
		public static void StopSoundLoopLocal(LoopSoundManager.SoundLoopPtr ptr)
		{
			if (!ptr.isValid)
			{
				return;
			}
			ref LoopSoundManager.SoundLoopNode @ref = ref ptr.GetRef();
			AkSoundEngine.PostEvent(@ref.loopSoundDef.stopSoundName, @ref.owner.cachedGameObject);
			if (@ref.previous.isValid)
			{
				@ref.previous.GetRef().next = @ref.next;
			}
			else
			{
				@ref.owner.first = @ref.next;
			}
			if (@ref.next.isValid)
			{
				@ref.next.GetRef().previous = @ref.previous;
			}
			else
			{
				@ref.owner.last = @ref.previous;
			}
			LoopSoundManager.soundLoopHeap.Free(ptr.ptr);
		}

		// Token: 0x04004FED RID: 20461
		private static readonly ValueHeap<LoopSoundManager.SoundLoopNode> soundLoopHeap = new ValueHeap<LoopSoundManager.SoundLoopNode>(128U);

		// Token: 0x02000E58 RID: 3672
		public struct SoundLoopPtr
		{
			// Token: 0x06005419 RID: 21529 RVA: 0x0015B419 File Offset: 0x00159619
			public SoundLoopPtr(ValueHeap<LoopSoundManager.SoundLoopNode>.Ptr ptr)
			{
				this.ptr = ptr;
			}

			// Token: 0x170007CE RID: 1998
			// (get) Token: 0x0600541A RID: 21530 RVA: 0x0015B422 File Offset: 0x00159622
			public bool isValid
			{
				get
				{
					return LoopSoundManager.soundLoopHeap.PtrIsValid(this.ptr);
				}
			}

			// Token: 0x0600541B RID: 21531 RVA: 0x0015B434 File Offset: 0x00159634
			public LoopSoundManager.SoundLoopNode GetValue()
			{
				return LoopSoundManager.soundLoopHeap.GetValue(this.ptr);
			}

			// Token: 0x0600541C RID: 21532 RVA: 0x0015B446 File Offset: 0x00159646
			public void SetValue(in LoopSoundManager.SoundLoopNode value)
			{
				LoopSoundManager.soundLoopHeap.SetValue(this.ptr, value);
			}

			// Token: 0x0600541D RID: 21533 RVA: 0x0015B459 File Offset: 0x00159659
			public ref LoopSoundManager.SoundLoopNode GetRef()
			{
				return LoopSoundManager.soundLoopHeap.GetRef(this.ptr);
			}

			// Token: 0x0600541E RID: 21534 RVA: 0x0015B46B File Offset: 0x0015966B
			public void SetRtpc(string rtpcName, float value)
			{
				AkSoundEngine.SetRTPCValueByPlayingID(rtpcName, value, this.GetRef().akId);
			}

			// Token: 0x04004FEE RID: 20462
			public readonly ValueHeap<LoopSoundManager.SoundLoopNode>.Ptr ptr;
		}

		// Token: 0x02000E59 RID: 3673
		public class SoundLoopHelper : MonoBehaviour
		{
			// Token: 0x170007CF RID: 1999
			// (get) Token: 0x0600541F RID: 21535 RVA: 0x0015B480 File Offset: 0x00159680
			// (set) Token: 0x06005420 RID: 21536 RVA: 0x0015B488 File Offset: 0x00159688
			public LoopSoundManager.SoundLoopPtr first { get; set; }

			// Token: 0x170007D0 RID: 2000
			// (get) Token: 0x06005421 RID: 21537 RVA: 0x0015B491 File Offset: 0x00159691
			// (set) Token: 0x06005422 RID: 21538 RVA: 0x0015B499 File Offset: 0x00159699
			public LoopSoundManager.SoundLoopPtr last { get; set; }

			// Token: 0x170007D1 RID: 2001
			// (get) Token: 0x06005423 RID: 21539 RVA: 0x0015B4A2 File Offset: 0x001596A2
			// (set) Token: 0x06005424 RID: 21540 RVA: 0x0015B4AA File Offset: 0x001596AA
			public GameObject cachedGameObject { get; set; }

			// Token: 0x06005425 RID: 21541 RVA: 0x0015B4B4 File Offset: 0x001596B4
			private void OnDestroy()
			{
				while (this.first.isValid)
				{
					LoopSoundManager.StopSoundLoopLocal(this.first);
				}
			}
		}

		// Token: 0x02000E5A RID: 3674
		public struct SoundLoopNode
		{
			// Token: 0x04004FF2 RID: 20466
			public LoopSoundManager.SoundLoopHelper owner;

			// Token: 0x04004FF3 RID: 20467
			public LoopSoundDef loopSoundDef;

			// Token: 0x04004FF4 RID: 20468
			public uint akId;

			// Token: 0x04004FF5 RID: 20469
			public LoopSoundManager.SoundLoopPtr next;

			// Token: 0x04004FF6 RID: 20470
			public LoopSoundManager.SoundLoopPtr previous;
		}
	}
}
