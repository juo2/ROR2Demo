using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
[DisallowMultipleComponent]
public class ChildLocator : MonoBehaviour
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x000055FC File Offset: 0x000037FC
	public int Count
	{
		get
		{
			return this.transformPairs.Length;
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00005608 File Offset: 0x00003808
	public int FindChildIndex(string childName)
	{
		for (int i = 0; i < this.transformPairs.Length; i++)
		{
			if (childName == this.transformPairs[i].name)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00005644 File Offset: 0x00003844
	public int FindChildIndex(Transform childTransform)
	{
		for (int i = 0; i < this.transformPairs.Length; i++)
		{
			if (childTransform == this.transformPairs[i].transform)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000567B File Offset: 0x0000387B
	public string FindChildName(int childIndex)
	{
		if ((ulong)childIndex < (ulong)((long)this.transformPairs.Length))
		{
			return this.transformPairs[childIndex].name;
		}
		return null;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000569D File Offset: 0x0000389D
	public Transform FindChild(string childName)
	{
		return this.FindChild(this.FindChildIndex(childName));
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000056AC File Offset: 0x000038AC
	public GameObject FindChildGameObject(int childIndex)
	{
		Transform transform = this.FindChild(childIndex);
		if (!transform)
		{
			return null;
		}
		return transform.gameObject;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000056D1 File Offset: 0x000038D1
	public GameObject FindChildGameObject(string childName)
	{
		return this.FindChildGameObject(this.FindChildIndex(childName));
	}

	// Token: 0x060000FB RID: 251 RVA: 0x000056E0 File Offset: 0x000038E0
	public Transform FindChild(int childIndex)
	{
		if ((ulong)childIndex < (ulong)((long)this.transformPairs.Length))
		{
			return this.transformPairs[childIndex].transform;
		}
		return null;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00005702 File Offset: 0x00003902
	public T FindChildComponent<T>(string childName)
	{
		return this.FindChildComponent<T>(this.FindChildIndex(childName));
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00005714 File Offset: 0x00003914
	public T FindChildComponent<T>(int childIndex)
	{
		Transform transform = this.FindChild(childIndex);
		if (!transform)
		{
			return default(T);
		}
		return transform.GetComponent<T>();
	}

	// Token: 0x040000ED RID: 237
	[SerializeField]
	private ChildLocator.NameTransformPair[] transformPairs = Array.Empty<ChildLocator.NameTransformPair>();

	// Token: 0x02000036 RID: 54
	[Serializable]
	private struct NameTransformPair
	{
		// Token: 0x040000EE RID: 238
		public string name;

		// Token: 0x040000EF RID: 239
		public Transform transform;
	}
}
