using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
[Serializable]
public class SceneField
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A46B File Offset: 0x0000866B
	public string SceneName
	{
		get
		{
			return this.sceneName;
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000A473 File Offset: 0x00008673
	public SceneField(string sceneName)
	{
		this.sceneName = sceneName;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000A46B File Offset: 0x0000866B
	public static implicit operator string(SceneField sceneField)
	{
		return sceneField.sceneName;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000A48D File Offset: 0x0000868D
	public static implicit operator bool(SceneField sceneField)
	{
		return !string.IsNullOrEmpty(sceneField.sceneName);
	}

	// Token: 0x04000239 RID: 569
	[SerializeField]
	private UnityEngine.Object sceneAsset;

	// Token: 0x0400023A RID: 570
	[SerializeField]
	private string sceneName = "";
}
