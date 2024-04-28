using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public static class SingletonHelper
{
	// Token: 0x0600024E RID: 590 RVA: 0x0000A5B8 File Offset: 0x000087B8
	public static void Assign<T>(ref T field, T instance) where T : UnityEngine.Object
	{
		if (!field)
		{
			field = instance;
			return;
		}
		Debug.LogErrorFormat(instance, "Duplicate instance of singleton class {0}. Only one should exist at a time.", new object[]
		{
			typeof(T).Name
		});
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000A607 File Offset: 0x00008807
	public static void Unassign<T>(ref T field, T instance) where T : UnityEngine.Object
	{
		if (field == instance)
		{
			field = default(T);
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000A628 File Offset: 0x00008828
	public static T Assign<T>(T existingInstance, T instance) where T : UnityEngine.Object
	{
		if (!existingInstance)
		{
			return instance;
		}
		Debug.LogErrorFormat(instance, "Duplicate instance of singleton class {0}. Only one should exist at a time.", new object[]
		{
			typeof(T).Name
		});
		return existingInstance;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000A664 File Offset: 0x00008864
	public static T Unassign<T>(T existingInstance, T instance) where T : UnityEngine.Object
	{
		if (instance == existingInstance)
		{
			return default(T);
		}
		return existingInstance;
	}
}
