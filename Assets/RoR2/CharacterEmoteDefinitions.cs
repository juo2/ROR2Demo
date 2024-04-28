using System;
using EntityStates;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000630 RID: 1584
	public class CharacterEmoteDefinitions : MonoBehaviour
	{
		// Token: 0x06001DFD RID: 7677 RVA: 0x00080BD4 File Offset: 0x0007EDD4
		public int FindEmoteIndex(string name)
		{
			for (int i = 0; i < this.emoteDefinitions.Length; i++)
			{
				if (this.emoteDefinitions[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040023DC RID: 9180
		public CharacterEmoteDefinitions.EmoteDef[] emoteDefinitions;

		// Token: 0x02000631 RID: 1585
		[Serializable]
		public struct EmoteDef
		{
			// Token: 0x040023DD RID: 9181
			public string name;

			// Token: 0x040023DE RID: 9182
			public string displayName;

			// Token: 0x040023DF RID: 9183
			public EntityStateMachine targetStateMachine;

			// Token: 0x040023E0 RID: 9184
			public SerializableEntityStateType state;
		}
	}
}
