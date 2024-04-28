using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x0200000A RID: 10
	public static class Helper
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021A2 File Offset: 0x000003A2
		public static int GetAllocationCount()
		{
			return Helper.s_Allocations.Count;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021AE File Offset: 0x000003AE
		internal static bool TryMarshalGet<T>(T[] source, out uint target)
		{
			return Helper.TryConvert<T>(source, out target);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021B7 File Offset: 0x000003B7
		internal static bool TryMarshalGet<T>(IntPtr source, out T target) where T : Handle, new()
		{
			return Helper.TryConvert<T>(source, out target);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021C0 File Offset: 0x000003C0
		internal static bool TryMarshalGet<TSource, TTarget>(TSource source, out TTarget target) where TTarget : ISettable, new()
		{
			return Helper.TryConvert<TSource, TTarget>(source, out target);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021C9 File Offset: 0x000003C9
		internal static bool TryMarshalGet(int source, out bool target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021D2 File Offset: 0x000003D2
		internal static bool TryMarshalGet(bool source, out int target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021DB File Offset: 0x000003DB
		internal static bool TryMarshalGet(long source, out DateTimeOffset? target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021E4 File Offset: 0x000003E4
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, int arrayLength, bool isElementAllocated)
		{
			return Helper.TryFetch<T>(source, out target, arrayLength, isElementAllocated);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021EF File Offset: 0x000003EF
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, uint arrayLength, bool isElementAllocated)
		{
			return Helper.TryFetch<T>(source, out target, (int)arrayLength, isElementAllocated);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021FA File Offset: 0x000003FA
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, int arrayLength)
		{
			return Helper.TryMarshalGet<T>(source, out target, arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002216 File Offset: 0x00000416
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, uint arrayLength)
		{
			return Helper.TryMarshalGet<T>(source, out target, arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002232 File Offset: 0x00000432
		internal static bool TryMarshalGetHandle<THandle>(IntPtr source, out THandle[] target, uint arrayLength) where THandle : Handle, new()
		{
			return Helper.TryFetchHandle<THandle>(source, out target, (int)arrayLength);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000223C File Offset: 0x0000043C
		internal static bool TryMarshalGet<TSource, TTarget>(TSource[] source, out TTarget[] target) where TSource : struct where TTarget : class, ISettable, new()
		{
			return Helper.TryConvert<TSource, TTarget>(source, out target);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002248 File Offset: 0x00000448
		internal static bool TryMarshalGet<TSource, TTarget>(IntPtr source, out TTarget[] target, int arrayLength) where TSource : struct where TTarget : class, ISettable, new()
		{
			target = Helper.GetDefault<TTarget[]>();
			TSource[] source2;
			return Helper.TryMarshalGet<TSource>(source, out source2, arrayLength) && Helper.TryMarshalGet<TSource, TTarget>(source2, out target);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002270 File Offset: 0x00000470
		internal static bool TryMarshalGet<TSource, TTarget>(IntPtr source, out TTarget[] target, uint arrayLength) where TSource : struct where TTarget : class, ISettable, new()
		{
			return Helper.TryMarshalGet<TSource, TTarget>(source, out target, (int)arrayLength);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002287 File Offset: 0x00000487
		internal static bool TryMarshalGet<T>(IntPtr source, out T? target) where T : struct
		{
			return Helper.TryFetch<T>(source, out target);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002290 File Offset: 0x00000490
		internal static bool TryMarshalGet(byte[] source, out string target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000229C File Offset: 0x0000049C
		internal static bool TryMarshalGet(IntPtr source, out object target)
		{
			target = null;
			BoxedData boxedData;
			if (Helper.TryFetch<BoxedData>(source, out boxedData))
			{
				target = boxedData.Data;
				return true;
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022C1 File Offset: 0x000004C1
		internal static bool TryMarshalGet(IntPtr source, out string target)
		{
			return Helper.TryFetch(source, out target);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022CA File Offset: 0x000004CA
		internal static bool TryMarshalGet<T, TEnum>(T source, out T target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<T>();
			if ((int)((object)currentEnum) == (int)((object)comparisonEnum))
			{
				target = source;
				return true;
			}
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022F9 File Offset: 0x000004F9
		internal static bool TryMarshalGet<TTarget, TEnum>(ISettable source, out TTarget target, TEnum currentEnum, TEnum comparisonEnum) where TTarget : ISettable, new()
		{
			target = Helper.GetDefault<TTarget>();
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryConvert<ISettable, TTarget>(source, out target);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002328 File Offset: 0x00000528
		internal static bool TryMarshalGet<TEnum>(int source, out bool? target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<bool?>();
			bool value;
			if ((int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryConvert(source, out value))
			{
				target = new bool?(value);
				return true;
			}
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002371 File Offset: 0x00000571
		internal static bool TryMarshalGet<T, TEnum>(T source, out T? target, TEnum currentEnum, TEnum comparisonEnum) where T : struct
		{
			target = Helper.GetDefault<T?>();
			if ((int)((object)currentEnum) == (int)((object)comparisonEnum))
			{
				target = new T?(source);
				return true;
			}
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023A5 File Offset: 0x000005A5
		internal static bool TryMarshalGet<T, TEnum>(IntPtr source, out T target, TEnum currentEnum, TEnum comparisonEnum) where T : Handle, new()
		{
			target = Helper.GetDefault<T>();
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryMarshalGet<T>(source, out target);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023D3 File Offset: 0x000005D3
		internal static bool TryMarshalGet<TEnum>(IntPtr source, out IntPtr? target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<IntPtr?>();
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryMarshalGet<IntPtr>(source, out target);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002401 File Offset: 0x00000601
		internal static bool TryMarshalGet<TEnum>(IntPtr source, out string target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<string>();
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryMarshalGet(source, out target);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000242C File Offset: 0x0000062C
		internal static bool TryMarshalGet<TInternal, TPublic>(IntPtr source, out TPublic target) where TInternal : struct where TPublic : class, ISettable, new()
		{
			target = Helper.GetDefault<TPublic>();
			TInternal? tinternal;
			if (Helper.TryMarshalGet<TInternal>(source, out tinternal) && tinternal != null)
			{
				target = Activator.CreateInstance<TPublic>();
				target.Set(tinternal);
				return true;
			}
			return false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002478 File Offset: 0x00000678
		internal static bool TryMarshalGet<TCallbackInfoInternal, TCallbackInfo>(IntPtr callbackInfoAddress, out TCallbackInfo callbackInfo, out IntPtr clientDataAddress) where TCallbackInfoInternal : struct, ICallbackInfoInternal where TCallbackInfo : class, ISettable, new()
		{
			callbackInfo = default(TCallbackInfo);
			clientDataAddress = IntPtr.Zero;
			TCallbackInfoInternal tcallbackInfoInternal;
			if (Helper.TryFetch<TCallbackInfoInternal>(callbackInfoAddress, out tcallbackInfoInternal))
			{
				callbackInfo = Activator.CreateInstance<TCallbackInfo>();
				callbackInfo.Set(tcallbackInfoInternal);
				clientDataAddress = tcallbackInfoInternal.ClientDataAddress;
				return true;
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024CC File Offset: 0x000006CC
		internal static bool TryMarshalSet<T>(ref T target, T source)
		{
			target = source;
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024D6 File Offset: 0x000006D6
		internal static bool TryMarshalSet<TTarget>(ref TTarget target, object source) where TTarget : ISettable, new()
		{
			return Helper.TryConvert<object, TTarget>(source, out target);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024DF File Offset: 0x000006DF
		internal static bool TryMarshalSet(ref IntPtr target, Handle source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024E8 File Offset: 0x000006E8
		internal static bool TryMarshalSet<T>(ref IntPtr target, T? source) where T : struct
		{
			return Helper.TryAllocate<T>(ref target, source);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024F1 File Offset: 0x000006F1
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, bool isElementAllocated)
		{
			return Helper.TryAllocate<T>(ref target, source, isElementAllocated);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024FB File Offset: 0x000006FB
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source)
		{
			return Helper.TryMarshalSet<T>(ref target, source, !typeof(T).IsValueType);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002516 File Offset: 0x00000716
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out int arrayLength, bool isElementAllocated)
		{
			arrayLength = 0;
			if (Helper.TryMarshalSet<T>(ref target, source, isElementAllocated))
			{
				arrayLength = source.Length;
				return true;
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002530 File Offset: 0x00000730
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out uint arrayLength, bool isElementAllocated)
		{
			arrayLength = 0U;
			int num = 0;
			if (Helper.TryMarshalSet<T>(ref target, source, out num, isElementAllocated))
			{
				arrayLength = (uint)num;
				return true;
			}
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002554 File Offset: 0x00000754
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out uint arrayLength)
		{
			return Helper.TryMarshalSet<T>(ref target, source, out arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002570 File Offset: 0x00000770
		internal static bool TryMarshalSet(ref long target, DateTimeOffset? source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002579 File Offset: 0x00000779
		internal static bool TryMarshalSet(ref int target, bool source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002582 File Offset: 0x00000782
		internal static bool TryMarshalSet(ref byte[] target, string source, int length)
		{
			return Helper.TryConvert(source, out target, length);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000258C File Offset: 0x0000078C
		internal static bool TryMarshalSet(ref IntPtr target, string source)
		{
			return Helper.TryAllocate(ref target, source);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002595 File Offset: 0x00000795
		internal static bool TryMarshalSet<T, TEnum>(ref T target, T source, ref TEnum currentEnum, TEnum comparisonEnum, IDisposable disposable = null)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose<IDisposable>(ref disposable);
				if (Helper.TryMarshalSet<T>(ref target, source))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025BA File Offset: 0x000007BA
		internal static bool TryMarshalSet<TTarget, TEnum>(ref TTarget target, ISettable source, ref TEnum currentEnum, TEnum comparisonEnum, IDisposable disposable = null) where TTarget : ISettable, new()
		{
			if (source != null)
			{
				Helper.TryMarshalDispose<IDisposable>(ref disposable);
				if (Helper.TryConvert<ISettable, TTarget>(source, out target))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025DA File Offset: 0x000007DA
		internal static bool TryMarshalSet<T, TEnum>(ref T target, T? source, ref TEnum currentEnum, TEnum comparisonEnum, IDisposable disposable = null) where T : struct
		{
			if (source != null)
			{
				Helper.TryMarshalDispose<IDisposable>(ref disposable);
				if (Helper.TryMarshalSet<T>(ref target, source.Value))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002606 File Offset: 0x00000806
		internal static bool TryMarshalSet<TEnum>(ref IntPtr target, Handle source, ref TEnum currentEnum, TEnum comparisonEnum, IDisposable disposable = null)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose<IDisposable>(ref disposable);
				if (Helper.TryMarshalSet(ref target, source))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000262C File Offset: 0x0000082C
		internal static bool TryMarshalSet<TEnum>(ref IntPtr target, string source, ref TEnum currentEnum, TEnum comparisonEnum, IDisposable disposable = null)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose(ref target);
				target = IntPtr.Zero;
				Helper.TryMarshalDispose<IDisposable>(ref disposable);
				if (Helper.TryMarshalSet(ref target, source))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000265A File Offset: 0x0000085A
		internal static bool TryMarshalSet<TEnum>(ref int target, bool? source, ref TEnum currentEnum, TEnum comparisonEnum, IDisposable disposable = null)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose<IDisposable>(ref disposable);
				if (Helper.TryMarshalSet(ref target, source.Value))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002688 File Offset: 0x00000888
		internal static bool TryMarshalSet<TInternal, TPublic>(ref IntPtr target, TPublic source) where TInternal : struct, ISettable where TPublic : class
		{
			if (source != null)
			{
				TInternal source2 = Activator.CreateInstance<TInternal>();
				source2.Set(source);
				if (Helper.TryAllocate<TInternal>(ref target, source2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026C4 File Offset: 0x000008C4
		internal static bool TryMarshalSet<TInternal, TPublic>(ref IntPtr target, TPublic[] source, out int arrayLength) where TInternal : struct, ISettable where TPublic : class
		{
			arrayLength = 0;
			if (source != null)
			{
				TInternal[] array = new TInternal[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i].Set(source[i]);
				}
				if (Helper.TryMarshalSet<TInternal>(ref target, array))
				{
					arrayLength = source.Length;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002720 File Offset: 0x00000920
		internal static bool TryMarshalSet<TInternal, TPublic>(ref IntPtr target, TPublic[] source, out uint arrayLength) where TInternal : struct, ISettable where TPublic : class
		{
			arrayLength = 0U;
			int num;
			if (Helper.TryMarshalSet<TInternal, TPublic>(ref target, source, out num))
			{
				arrayLength = (uint)num;
				return true;
			}
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002744 File Offset: 0x00000944
		internal static bool TryMarshalSet<TInternal, TPublic>(ref IntPtr target, TPublic[] source, out int arrayLength, bool isElementAllocated) where TInternal : struct, ISettable where TPublic : class
		{
			arrayLength = 0;
			if (source != null)
			{
				TInternal[] array = new TInternal[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i].Set(source[i]);
				}
				if (Helper.TryMarshalSet<TInternal>(ref target, array, isElementAllocated))
				{
					arrayLength = source.Length;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027A0 File Offset: 0x000009A0
		internal static bool TryMarshalSet<TInternal, TPublic>(ref IntPtr target, TPublic[] source, out uint arrayLength, bool isElementAllocated) where TInternal : struct, ISettable where TPublic : class
		{
			arrayLength = 0U;
			int num;
			if (Helper.TryMarshalSet<TInternal, TPublic>(ref target, source, out num, isElementAllocated))
			{
				arrayLength = (uint)num;
				return true;
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027C2 File Offset: 0x000009C2
		internal static bool TryMarshalCopy(IntPtr target, byte[] source)
		{
			if (target != IntPtr.Zero && source != null)
			{
				Marshal.Copy(source, 0, target, source.Length);
				return true;
			}
			return false;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027E2 File Offset: 0x000009E2
		internal static bool TryMarshalAllocate(ref IntPtr target, int size, out Helper.Allocation allocation)
		{
			Helper.TryMarshalDispose(ref target);
			target = Marshal.AllocHGlobal(size);
			Marshal.WriteByte(target, 0, 0);
			allocation = new Helper.Allocation(size);
			Helper.s_Allocations.Add(target, allocation);
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002813 File Offset: 0x00000A13
		internal static bool TryMarshalAllocate(ref IntPtr target, uint size, out Helper.Allocation allocation)
		{
			return Helper.TryMarshalAllocate(ref target, (int)size, out allocation);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002820 File Offset: 0x00000A20
		internal static bool TryMarshalAllocate(ref IntPtr target, int size)
		{
			Helper.Allocation allocation;
			return Helper.TryMarshalAllocate(ref target, size, out allocation);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002838 File Offset: 0x00000A38
		internal static bool TryMarshalAllocate(ref IntPtr target, uint size)
		{
			Helper.Allocation allocation;
			return Helper.TryMarshalAllocate(ref target, size, out allocation);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000284E File Offset: 0x00000A4E
		internal static bool TryMarshalDispose<TDisposable>(ref TDisposable disposable) where TDisposable : IDisposable
		{
			if (disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000286C File Offset: 0x00000A6C
		internal static bool TryMarshalDispose(ref IntPtr value)
		{
			return Helper.TryRelease(ref value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002874 File Offset: 0x00000A74
		internal static bool TryMarshalDispose<TEnum>(ref IntPtr member, TEnum currentEnum, TEnum comparisonEnum)
		{
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryRelease(ref member);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002898 File Offset: 0x00000A98
		internal static T GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028AE File Offset: 0x00000AAE
		internal static void AddCallback(ref IntPtr clientDataAddress, object clientData, Delegate publicDelegate, Delegate privateDelegate, params Delegate[] structDelegates)
		{
			Helper.TryAllocateCacheOnly<BoxedData>(ref clientDataAddress, new BoxedData(clientData));
			Helper.s_Callbacks.Add(clientDataAddress, new Helper.DelegateHolder(publicDelegate, privateDelegate, structDelegates));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028D2 File Offset: 0x00000AD2
		internal static void AddStaticCallback(string key, Delegate publicDelegate, Delegate privateDelegate)
		{
			Helper.s_StaticCallbacks[key] = new Helper.DelegateHolder(publicDelegate, privateDelegate, Array.Empty<Delegate>());
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000028EC File Offset: 0x00000AEC
		internal static bool TryAssignNotificationIdToCallback(IntPtr clientDataAddress, ulong notificationId)
		{
			if (notificationId != 0UL)
			{
				Helper.DelegateHolder delegateHolder = null;
				if (Helper.s_Callbacks.TryGetValue(clientDataAddress, out delegateHolder))
				{
					delegateHolder.NotificationId = new ulong?(notificationId);
					return true;
				}
			}
			else
			{
				Helper.s_Callbacks.Remove(clientDataAddress);
				Helper.TryRelease(ref clientDataAddress);
			}
			return false;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002930 File Offset: 0x00000B30
		internal static bool TryRemoveCallbackByNotificationId(ulong notificationId)
		{
			IEnumerable<KeyValuePair<IntPtr, Helper.DelegateHolder>> source = Helper.s_Callbacks.Where(delegate(KeyValuePair<IntPtr, Helper.DelegateHolder> pair)
			{
				if (pair.Value.NotificationId != null)
				{
					ulong? notificationId2 = pair.Value.NotificationId;
					ulong notificationId3 = notificationId;
					return notificationId2.GetValueOrDefault() == notificationId3 & notificationId2 != null;
				}
				return false;
			});
			if (source.Any<KeyValuePair<IntPtr, Helper.DelegateHolder>>())
			{
				IntPtr key = source.First<KeyValuePair<IntPtr, Helper.DelegateHolder>>().Key;
				Helper.s_Callbacks.Remove(key);
				Helper.TryRelease(ref key);
				return true;
			}
			return false;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002990 File Offset: 0x00000B90
		internal static bool TryGetAndRemoveCallback<TCallback, TCallbackInfoInternal, TCallbackInfo>(IntPtr callbackInfoAddress, out TCallback callback, out TCallbackInfo callbackInfo) where TCallback : class where TCallbackInfoInternal : struct, ICallbackInfoInternal where TCallbackInfo : class, ICallbackInfo, ISettable, new()
		{
			callback = default(TCallback);
			callbackInfo = default(TCallbackInfo);
			IntPtr zero = IntPtr.Zero;
			return Helper.TryMarshalGet<TCallbackInfoInternal, TCallbackInfo>(callbackInfoAddress, out callbackInfo, out zero) && Helper.TryGetAndRemoveCallback<TCallback, TCallbackInfo>(zero, callbackInfo, out callback);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029D0 File Offset: 0x00000BD0
		internal static bool TryGetStructCallback<TDelegate, TCallbackInfoInternal, TCallbackInfo>(IntPtr callbackInfoAddress, out TDelegate callback, out TCallbackInfo callbackInfo) where TDelegate : class where TCallbackInfoInternal : struct, ICallbackInfoInternal where TCallbackInfo : class, ISettable, new()
		{
			callback = default(TDelegate);
			callbackInfo = default(TCallbackInfo);
			IntPtr zero = IntPtr.Zero;
			return Helper.TryMarshalGet<TCallbackInfoInternal, TCallbackInfo>(callbackInfoAddress, out callbackInfo, out zero) && Helper.TryGetStructCallback<TDelegate>(zero, out callback);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A08 File Offset: 0x00000C08
		private static bool TryAllocate<T>(ref IntPtr target, T source)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			if (source == null)
			{
				return false;
			}
			Helper.Allocation allocation;
			if (!Helper.TryMarshalAllocate(ref target, Marshal.SizeOf(typeof(T)), out allocation))
			{
				return false;
			}
			allocation.SetCachedData(source, null);
			Marshal.StructureToPtr<T>(source, target, false);
			return true;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A84 File Offset: 0x00000C84
		private static bool TryAllocate<T>(ref IntPtr target, T? source) where T : struct
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			return source != null && Helper.TryAllocate<T>(ref target, source.Value);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private static bool TryAllocate(ref IntPtr target, string source)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			byte[] source2;
			return source != null && Helper.TryConvert(source, out source2) && Helper.TryAllocate<byte>(ref target, source2, false);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B20 File Offset: 0x00000D20
		private static bool TryAllocate<T>(ref IntPtr target, T[] source, bool isElementAllocated)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			if (source == null)
			{
				return false;
			}
			int num;
			if (isElementAllocated)
			{
				num = Marshal.SizeOf(typeof(IntPtr));
			}
			else
			{
				num = Marshal.SizeOf(typeof(T));
			}
			Helper.Allocation allocation;
			if (!Helper.TryMarshalAllocate(ref target, source.Length * num, out allocation))
			{
				return false;
			}
			allocation.SetCachedData(source, new bool?(isElementAllocated));
			for (int i = 0; i < source.Length; i++)
			{
				T t = (T)((object)source.GetValue(i));
				if (isElementAllocated)
				{
					IntPtr zero = IntPtr.Zero;
					if (typeof(T) == typeof(string))
					{
						Helper.TryAllocate(ref zero, (string)((object)t));
					}
					else if (typeof(T).BaseType == typeof(Handle))
					{
						Helper.TryConvert((Handle)((object)t), out zero);
					}
					else
					{
						Helper.TryAllocate<T>(ref zero, t);
					}
					IntPtr ptr = new IntPtr(target.ToInt64() + (long)(i * num));
					Marshal.StructureToPtr<IntPtr>(zero, ptr, false);
				}
				else
				{
					IntPtr ptr2 = new IntPtr(target.ToInt64() + (long)(i * num));
					Marshal.StructureToPtr<T>(t, ptr2, false);
				}
			}
			return true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C70 File Offset: 0x00000E70
		private static bool TryAllocateCacheOnly<T>(ref IntPtr target, T source)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			if (source == null)
			{
				return false;
			}
			Helper.Allocation allocation;
			if (!Helper.TryMarshalAllocate(ref target, 1, out allocation))
			{
				return false;
			}
			allocation.SetCachedData(source, null);
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CD4 File Offset: 0x00000ED4
		private static bool TryRelease(ref IntPtr target)
		{
			if (target == IntPtr.Zero)
			{
				return false;
			}
			Helper.Allocation allocation = null;
			if (!Helper.s_Allocations.TryGetValue(target, out allocation))
			{
				return false;
			}
			if (allocation.IsCachedArrayElementAllocated != null)
			{
				int num;
				if (allocation.IsCachedArrayElementAllocated.Value)
				{
					num = Marshal.SizeOf(typeof(IntPtr));
				}
				else
				{
					num = Marshal.SizeOf(allocation.CachedData.GetType().GetElementType());
				}
				Array array = allocation.CachedData as Array;
				for (int i = 0; i < array.Length; i++)
				{
					if (allocation.IsCachedArrayElementAllocated.Value)
					{
						IntPtr ptr = new IntPtr(target.ToInt64() + (long)(i * num));
						ptr = Marshal.ReadIntPtr(ptr);
						Helper.TryRelease(ref ptr);
					}
					else
					{
						object value = array.GetValue(i);
						if (value is IDisposable)
						{
							IDisposable disposable = value as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
			}
			if (allocation.CachedData is IDisposable)
			{
				IDisposable disposable2 = allocation.CachedData as IDisposable;
				if (disposable2 != null)
				{
					disposable2.Dispose();
				}
			}
			Marshal.FreeHGlobal(target);
			Helper.s_Allocations.Remove(target);
			target = IntPtr.Zero;
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E14 File Offset: 0x00001014
		private static bool TryFetch<T>(IntPtr source, out T target)
		{
			target = Helper.GetDefault<T>();
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (Helper.s_Allocations.ContainsKey(source))
			{
				Helper.Allocation allocation = Helper.s_Allocations[source];
				if (allocation.CachedData != null)
				{
					if (allocation.CachedData.GetType() == typeof(T))
					{
						target = (T)((object)allocation.CachedData);
						return true;
					}
					throw new CachedTypeAllocationException(source, allocation.CachedData.GetType(), typeof(T));
				}
			}
			target = (T)((object)Marshal.PtrToStructure(source, typeof(T)));
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EC4 File Offset: 0x000010C4
		private static bool TryFetch<T>(IntPtr source, out T? target) where T : struct
		{
			target = Helper.GetDefault<T?>();
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (Helper.s_Allocations.ContainsKey(source))
			{
				Helper.Allocation allocation = Helper.s_Allocations[source];
				if (allocation.CachedData != null)
				{
					if (allocation.CachedData.GetType() == typeof(T))
					{
						target = (T?)allocation.CachedData;
						return true;
					}
					throw new CachedTypeAllocationException(source, allocation.CachedData.GetType(), typeof(T));
				}
			}
			target = (T?)Marshal.PtrToStructure(source, typeof(T));
			return true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F74 File Offset: 0x00001174
		private static bool TryFetchHandle<THandle>(IntPtr source, out THandle[] target, int arrayLength) where THandle : Handle, new()
		{
			target = null;
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (Helper.s_Allocations.ContainsKey(source))
			{
				Helper.Allocation allocation = Helper.s_Allocations[source];
				if (allocation.CachedData != null)
				{
					if (!(allocation.CachedData.GetType() == typeof(THandle[])))
					{
						throw new CachedTypeAllocationException(source, allocation.CachedData.GetType(), typeof(THandle[]));
					}
					Array array = (Array)allocation.CachedData;
					if (array.Length == arrayLength)
					{
						target = (array as THandle[]);
						return true;
					}
					throw new CachedArrayAllocationException(source, array.Length, arrayLength);
				}
			}
			int num = Marshal.SizeOf(typeof(IntPtr));
			List<THandle> list = new List<THandle>();
			for (int i = 0; i < arrayLength; i++)
			{
				THandle item;
				Helper.TryConvert<THandle>(Marshal.ReadIntPtr(new IntPtr(source.ToInt64() + (long)(i * num))), out item);
				list.Add(item);
			}
			target = list.ToArray();
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003074 File Offset: 0x00001274
		private static bool TryFetch<T>(IntPtr source, out T[] target, int arrayLength, bool isElementAllocated)
		{
			target = null;
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (Helper.s_Allocations.ContainsKey(source))
			{
				Helper.Allocation allocation = Helper.s_Allocations[source];
				if (allocation.CachedData != null)
				{
					if (!(allocation.CachedData.GetType() == typeof(T[])))
					{
						throw new CachedTypeAllocationException(source, allocation.CachedData.GetType(), typeof(T[]));
					}
					Array array = (Array)allocation.CachedData;
					if (array.Length == arrayLength)
					{
						target = (array as T[]);
						return true;
					}
					throw new CachedArrayAllocationException(source, array.Length, arrayLength);
				}
			}
			int num;
			if (isElementAllocated)
			{
				num = Marshal.SizeOf(typeof(IntPtr));
			}
			else
			{
				num = Marshal.SizeOf(typeof(T));
			}
			List<T> list = new List<T>();
			for (int i = 0; i < arrayLength; i++)
			{
				IntPtr intPtr = new IntPtr(source.ToInt64() + (long)(i * num));
				if (isElementAllocated)
				{
					intPtr = Marshal.ReadIntPtr(intPtr);
				}
				T item;
				Helper.TryFetch<T>(intPtr, out item);
				list.Add(item);
			}
			target = list.ToArray();
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003194 File Offset: 0x00001394
		private static bool TryFetch(IntPtr source, out string target)
		{
			target = null;
			if (source == IntPtr.Zero)
			{
				return false;
			}
			int num = 0;
			while (Marshal.ReadByte(source, num) != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, num);
			target = Encoding.UTF8.GetString(array);
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031E2 File Offset: 0x000013E2
		private static bool TryConvert<THandle>(IntPtr source, out THandle target) where THandle : Handle, new()
		{
			target = default(THandle);
			if (source != IntPtr.Zero)
			{
				target = Activator.CreateInstance<THandle>();
				target.InnerHandle = source;
			}
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003211 File Offset: 0x00001411
		internal static bool TryConvert<TSource, TTarget>(TSource source, out TTarget target) where TTarget : ISettable, new()
		{
			target = Helper.GetDefault<TTarget>();
			if (source != null)
			{
				target = Activator.CreateInstance<TTarget>();
				target.Set(source);
			}
			return true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003244 File Offset: 0x00001444
		private static bool TryConvert(Handle source, out IntPtr target)
		{
			target = IntPtr.Zero;
			if (source != null)
			{
				target = source.InnerHandle;
			}
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003260 File Offset: 0x00001460
		private static bool TryConvert(byte[] source, out string target)
		{
			target = null;
			if (source == null)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			while (num2 < source.Length && source[num2] != 0)
			{
				num++;
				num2++;
			}
			target = Encoding.UTF8.GetString(source.Take(num).ToArray<byte>());
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032A9 File Offset: 0x000014A9
		private static bool TryConvert(string source, out byte[] target, int length)
		{
			if (source == null)
			{
				source = "";
			}
			target = Encoding.UTF8.GetBytes(new string(source.Take(length).ToArray<char>()).PadRight(length, '\0'));
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032DA File Offset: 0x000014DA
		private static bool TryConvert(string source, out byte[] target)
		{
			return Helper.TryConvert(source, out target, source.Length + 1);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032EB File Offset: 0x000014EB
		private static bool TryConvert<T>(T[] source, out int target)
		{
			target = 0;
			if (source != null)
			{
				target = source.Length;
			}
			return true;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032FC File Offset: 0x000014FC
		private static bool TryConvert<T>(T[] source, out uint target)
		{
			target = 0U;
			int num;
			if (Helper.TryConvert<T>(source, out num))
			{
				target = (uint)num;
				return true;
			}
			return false;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000331C File Offset: 0x0000151C
		internal static bool TryConvert<TSource, TTarget>(TSource[] source, out TTarget[] target) where TTarget : ISettable, new()
		{
			target = Helper.GetDefault<TTarget[]>();
			if (source != null)
			{
				target = new TTarget[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					target[i] = Activator.CreateInstance<TTarget>();
					target[i].Set(source[i]);
				}
			}
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000337A File Offset: 0x0000157A
		private static bool TryConvert(int source, out bool target)
		{
			target = (source != 0);
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003383 File Offset: 0x00001583
		private static bool TryConvert(bool source, out int target)
		{
			target = (source ? 1 : 0);
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003390 File Offset: 0x00001590
		private static bool TryConvert(DateTimeOffset? source, out long target)
		{
			target = -1L;
			if (source != null)
			{
				DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long num = (source.Value.UtcDateTime - d).Ticks / 10000000L;
				target = num;
			}
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000033E8 File Offset: 0x000015E8
		private static bool TryConvert(long source, out DateTimeOffset? target)
		{
			target = null;
			if (source >= 0L)
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				long num = source * 10000000L;
				target = new DateTimeOffset?(new DateTimeOffset(dateTime.Ticks + num, TimeSpan.Zero));
			}
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000343C File Offset: 0x0000163C
		private static bool CanRemoveCallback<TCallbackInfo>(IntPtr clientDataAddress, TCallbackInfo callbackInfo) where TCallbackInfo : ICallbackInfo
		{
			Helper.DelegateHolder delegateHolder = null;
			return (!Helper.s_Callbacks.TryGetValue(clientDataAddress, out delegateHolder) || delegateHolder.NotificationId == null) && (callbackInfo.GetResultCode() == null || Common.IsOperationComplete(callbackInfo.GetResultCode().Value));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034A0 File Offset: 0x000016A0
		private static bool TryGetAndRemoveCallback<TCallback, TCallbackInfo>(IntPtr clientDataAddress, TCallbackInfo callbackInfo, out TCallback callback) where TCallback : class where TCallbackInfo : ICallbackInfo
		{
			callback = default(TCallback);
			if (clientDataAddress != IntPtr.Zero && Helper.s_Callbacks.ContainsKey(clientDataAddress))
			{
				callback = (Helper.s_Callbacks[clientDataAddress].Public as TCallback);
				if (callback != null)
				{
					if (Helper.CanRemoveCallback<TCallbackInfo>(clientDataAddress, callbackInfo))
					{
						Helper.s_Callbacks.Remove(clientDataAddress);
						Helper.TryRelease(ref clientDataAddress);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000351C File Offset: 0x0000171C
		internal static bool TryGetStaticCallback<TCallback>(string key, out TCallback callback) where TCallback : class
		{
			callback = default(TCallback);
			if (Helper.s_StaticCallbacks.ContainsKey(key))
			{
				callback = (Helper.s_StaticCallbacks[key].Public as TCallback);
				if (callback != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003570 File Offset: 0x00001770
		private static bool TryGetStructCallback<TCallback>(IntPtr clientDataAddress, out TCallback structCallback) where TCallback : class
		{
			structCallback = default(TCallback);
			if (clientDataAddress != IntPtr.Zero && Helper.s_Callbacks.ContainsKey(clientDataAddress))
			{
				structCallback = (Helper.s_Callbacks[clientDataAddress].StructDelegates.FirstOrDefault((Delegate delegat) => delegat.GetType() == typeof(TCallback)) as TCallback);
				if (structCallback != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000005 RID: 5
		private static Dictionary<IntPtr, Helper.Allocation> s_Allocations = new Dictionary<IntPtr, Helper.Allocation>();

		// Token: 0x04000006 RID: 6
		private static Dictionary<IntPtr, Helper.DelegateHolder> s_Callbacks = new Dictionary<IntPtr, Helper.DelegateHolder>();

		// Token: 0x04000007 RID: 7
		private static Dictionary<string, Helper.DelegateHolder> s_StaticCallbacks = new Dictionary<string, Helper.DelegateHolder>();

		// Token: 0x02000632 RID: 1586
		internal class Allocation
		{
			// Token: 0x17000C43 RID: 3139
			// (get) Token: 0x060026E8 RID: 9960 RVA: 0x00029703 File Offset: 0x00027903
			// (set) Token: 0x060026E9 RID: 9961 RVA: 0x0002970B File Offset: 0x0002790B
			public int Size { get; private set; }

			// Token: 0x17000C44 RID: 3140
			// (get) Token: 0x060026EA RID: 9962 RVA: 0x00029714 File Offset: 0x00027914
			// (set) Token: 0x060026EB RID: 9963 RVA: 0x0002971C File Offset: 0x0002791C
			public object CachedData { get; private set; }

			// Token: 0x17000C45 RID: 3141
			// (get) Token: 0x060026EC RID: 9964 RVA: 0x00029725 File Offset: 0x00027925
			// (set) Token: 0x060026ED RID: 9965 RVA: 0x0002972D File Offset: 0x0002792D
			public bool? IsCachedArrayElementAllocated { get; private set; }

			// Token: 0x060026EE RID: 9966 RVA: 0x00029736 File Offset: 0x00027936
			public Allocation(int size)
			{
				this.Size = size;
			}

			// Token: 0x060026EF RID: 9967 RVA: 0x00029745 File Offset: 0x00027945
			public void SetCachedData(object data, bool? isCachedArrayElementAllocated = null)
			{
				this.CachedData = data;
				this.IsCachedArrayElementAllocated = isCachedArrayElementAllocated;
			}
		}

		// Token: 0x02000633 RID: 1587
		private class DelegateHolder
		{
			// Token: 0x17000C46 RID: 3142
			// (get) Token: 0x060026F0 RID: 9968 RVA: 0x00029755 File Offset: 0x00027955
			// (set) Token: 0x060026F1 RID: 9969 RVA: 0x0002975D File Offset: 0x0002795D
			public Delegate Public { get; private set; }

			// Token: 0x17000C47 RID: 3143
			// (get) Token: 0x060026F2 RID: 9970 RVA: 0x00029766 File Offset: 0x00027966
			// (set) Token: 0x060026F3 RID: 9971 RVA: 0x0002976E File Offset: 0x0002796E
			public Delegate Private { get; private set; }

			// Token: 0x17000C48 RID: 3144
			// (get) Token: 0x060026F4 RID: 9972 RVA: 0x00029777 File Offset: 0x00027977
			// (set) Token: 0x060026F5 RID: 9973 RVA: 0x0002977F File Offset: 0x0002797F
			public Delegate[] StructDelegates { get; private set; }

			// Token: 0x17000C49 RID: 3145
			// (get) Token: 0x060026F6 RID: 9974 RVA: 0x00029788 File Offset: 0x00027988
			// (set) Token: 0x060026F7 RID: 9975 RVA: 0x00029790 File Offset: 0x00027990
			public ulong? NotificationId { get; set; }

			// Token: 0x060026F8 RID: 9976 RVA: 0x00029799 File Offset: 0x00027999
			public DelegateHolder(Delegate publicDelegate, Delegate privateDelegate, params Delegate[] structDelegates)
			{
				this.Public = publicDelegate;
				this.Private = privateDelegate;
				this.StructDelegates = structDelegates;
			}
		}
	}
}
