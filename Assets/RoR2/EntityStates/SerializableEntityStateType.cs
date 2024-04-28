using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	public struct SerializableEntityStateType
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		public SerializableEntityStateType(string typeName)
		{
			this._typeName = "";
			this.typeName = typeName;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public SerializableEntityStateType(Type stateType)
		{
			this._typeName = "";
			this.stateType = stateType;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		public string typeName
		{
			get
			{
				return this._typeName;
			}
			private set
			{
				this.stateType = Type.GetType(value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000D521 File Offset: 0x0000B721
		public Type stateType
		{
			get
			{
				if (this._typeName == null)
				{
					return null;
				}
				Type type = Type.GetType(this._typeName);
				if (!(type != null) || !type.IsSubclassOf(typeof(EntityState)))
				{
					return null;
				}
				return type;
			}
			set
			{
				this._typeName = ((value != null && value.IsSubclassOf(typeof(EntityState))) ? value.AssemblyQualifiedName : "");
			}
		}

		// Token: 0x04000359 RID: 857
		[SerializeField]
		private string _typeName;
	}
}
