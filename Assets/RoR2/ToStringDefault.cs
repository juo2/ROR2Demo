using System;

// Token: 0x02000091 RID: 145
public struct ToStringDefault<T> : IToStringImplementation<T>
{
	// Token: 0x06000286 RID: 646 RVA: 0x0000AB2C File Offset: 0x00008D2C
	public string DoToString(in T input)
	{
		T t = input;
		return t.ToString();
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000AB4D File Offset: 0x00008D4D
	string IToStringImplementation<!0>.DoToString(in T input)
	{
		return this.DoToString(input);
	}
}
