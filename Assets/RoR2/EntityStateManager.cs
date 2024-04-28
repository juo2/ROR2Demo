using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EntityStates;
using HG;
using HG.GeneralSerializer;
using RoR2;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x0200001A RID: 26
public class EntityStateManager : ScriptableObject, ISerializationCallbackReceiver
{
	// Token: 0x06000076 RID: 118 RVA: 0x00003804 File Offset: 0x00001A04
	private EntityStateManager.StateInfo GetStateInfo(Type stateType)
	{
		if (stateType == null || !stateType.IsSubclassOf(typeof(EntityState)))
		{
			return null;
		}
		EntityStateManager.StateInfo stateInfo = this.stateInfoList.Find((EntityStateManager.StateInfo currentItem) => currentItem.serializedType.stateType == stateType);
		if (stateInfo == null)
		{
			stateInfo = new EntityStateManager.StateInfo();
			stateInfo.SetStateType(stateType);
			this.stateInfoList.Add(stateInfo);
		}
		return stateInfo;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003880 File Offset: 0x00001A80
	private void ApplyStatic()
	{
		foreach (EntityStateManager.StateInfo stateInfo in this.stateInfoList)
		{
			stateInfo.ApplyStatic();
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000026ED File Offset: 0x000008ED
	public void Initialize()
	{
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000038D0 File Offset: 0x00001AD0
	private static void SetEntityStateConfigurations(EntityStateConfiguration[] newEntityStateConfigurations)
	{
		EntityStateConfiguration[] array = ArrayUtils.Clone<EntityStateConfiguration>(newEntityStateConfigurations);
		Array.Sort<EntityStateConfiguration>(array, (EntityStateConfiguration a, EntityStateConfiguration b) => a.name.CompareTo(b.name));
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ApplyStatic();
		}
		EntityStateManager.instanceFieldInitializers.Clear();
		foreach (EntityStateConfiguration entityStateConfiguration in array)
		{
			Action<object> action = entityStateConfiguration.BuildInstanceInitializer();
			if (action != null)
			{
				EntityStateManager.instanceFieldInitializers[(Type)entityStateConfiguration.targetType] = action;
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003960 File Offset: 0x00001B60
	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{
		foreach (EntityStateManager.StateInfo stateInfo2 in this.stateInfoList)
		{
			stateInfo2.RefreshStateType();
		}
		this.stateInfoList.RemoveAll((EntityStateManager.StateInfo stateInfo) => !stateInfo.IsValid());
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000039DC File Offset: 0x00001BDC
	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		foreach (EntityStateManager.StateInfo stateInfo in this.stateInfoList)
		{
			stateInfo.RefreshStateType();
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003A2C File Offset: 0x00001C2C
	private void GenerateInstanceFieldInitializers()
	{
		EntityStateManager.instanceFieldInitializers.Clear();
		foreach (EntityStateManager.StateInfo stateInfo in this.stateInfoList)
		{
			Type stateType = stateInfo.serializedType.stateType;
			if (!(stateType == null))
			{
				Action<EntityState> action = stateInfo.GenerateInstanceFieldInitializerDelegate();
				if (action != null)
				{
					EntityStateManager.instanceFieldInitializers.Add(stateType, action);
				}
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003AB0 File Offset: 0x00001CB0
	public static void InitializeStateFields(EntityState entityState)
	{
		Action<EntityState> action;
		EntityStateManager.instanceFieldInitializers.TryGetValue(entityState.GetType(), out action);
		if (action != null)
		{
			action(entityState);
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003ADC File Offset: 0x00001CDC
	[ContextMenu("Migrate to individual assets")]
	public void MigrateToEntityStateTypes()
	{
		List<EntityStateManager.StateInfo> list = new List<EntityStateManager.StateInfo>();
		foreach (EntityStateManager.StateInfo stateInfo in this.stateInfoList)
		{
			if (this.MigrateToEntityStateType(stateInfo))
			{
				list.Add(stateInfo);
			}
		}
		foreach (EntityStateManager.StateInfo stateInfo2 in list)
		{
		}
		foreach (Type type in from t in typeof(EntityState).Assembly.GetTypes()
		where typeof(EntityState).IsAssignableFrom(t)
		select t)
		{
			this.GetOrCreateEntityStateSerializer(type);
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private EntityStateConfiguration GetOrCreateEntityStateSerializer(Type type)
	{
		return null;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003BEC File Offset: 0x00001DEC
	private bool MigrateToEntityStateType(EntityStateManager.StateInfo stateInfo)
	{
		Type stateType = stateInfo.serializedType.stateType;
		if (stateType == null)
		{
			Debug.LogWarningFormat("Could not migrate type named \"{0}\": Type could not be found.", new object[]
			{
				typeof(SerializableEntityStateType).GetField("_typeName", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stateInfo.serializedType)
			});
			return false;
		}
		EntityStateConfiguration orCreateEntityStateSerializer = this.GetOrCreateEntityStateSerializer(stateType);
		foreach (EntityStateManager.StateInfo.Field field in stateInfo.GetFields())
		{
			string fieldName = field.GetFieldName();
			FieldInfo field2 = stateType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			ref SerializedField orCreateField = ref orCreateEntityStateSerializer.serializedFieldsCollection.GetOrCreateField(fieldName);
			orCreateField.fieldName = fieldName;
			try
			{
				orCreateField.fieldValue.SetValue(field2, field.valueAsSystemObject);
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}
		EditorUtil.SetDirty(orCreateEntityStateSerializer);
		return true;
	}

	// Token: 0x04000085 RID: 133
	[SerializeField]
	private List<EntityStateManager.StateInfo> stateInfoList = new List<EntityStateManager.StateInfo>();

	// Token: 0x04000086 RID: 134
	[SerializeField]
	[HideInInspector]
	private string endMarker = "GIT_END";

	// Token: 0x04000087 RID: 135
	private static readonly Dictionary<Type, Action<EntityState>> instanceFieldInitializers = new Dictionary<Type, Action<EntityState>>();

	// Token: 0x0200001B RID: 27
	[Serializable]
	private class StateInfo
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003D12 File Offset: 0x00001F12
		private static bool FieldHasSerializeAttribute(FieldInfo fieldInfo)
		{
			return fieldInfo.GetCustomAttributes(true).Any<SerializeField>();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D20 File Offset: 0x00001F20
		private static bool FieldLacksNonSerializedAttribute(FieldInfo fieldInfo)
		{
			return !fieldInfo.GetCustomAttributes(true).Any<NonSerializedAttribute>();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D34 File Offset: 0x00001F34
		public void SetStateType(Type stateType)
		{
			this.serializedType.stateType = stateType;
			stateType = this.serializedType.stateType;
			if (stateType == null)
			{
				return;
			}
			IEnumerable<FieldInfo> first = stateType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).Where(new Func<FieldInfo, bool>(EntityStateManager.StateInfo.FieldHasSerializeAttribute));
			IEnumerable<FieldInfo> second = (from fieldInfo in stateType.GetFields(BindingFlags.Static | BindingFlags.Public)
			where fieldInfo.DeclaringType == stateType
			select fieldInfo).Where(new Func<FieldInfo, bool>(EntityStateManager.StateInfo.FieldLacksNonSerializedAttribute));
			List<FieldInfo> list = first.Concat(second).ToList<FieldInfo>();
			Dictionary<FieldInfo, EntityStateManager.StateInfo.Field> dictionary = new Dictionary<FieldInfo, EntityStateManager.StateInfo.Field>();
			using (List<FieldInfo>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FieldInfo fieldInfo = enumerator.Current;
					EntityStateManager.StateInfo.Field field = this.stateFieldList.Find((EntityStateManager.StateInfo.Field item) => item.GetFieldName() == fieldInfo.Name);
					if (field == null)
					{
						Debug.LogFormat("Could not find field {0}.{1}. Initializing new field.", new object[]
						{
							stateType.Name,
							fieldInfo.Name
						});
						field = new EntityStateManager.StateInfo.Field(fieldInfo.Name);
					}
					dictionary[fieldInfo] = field;
				}
			}
			this.stateFieldList.Clear();
			foreach (FieldInfo fieldInfo2 in list)
			{
				EntityStateManager.StateInfo.Field field2 = dictionary[fieldInfo2];
				field2.owner = this;
				field2.SetFieldInfo(fieldInfo2);
				this.stateFieldList.Add(field2);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003F08 File Offset: 0x00002108
		public void RefreshStateType()
		{
			this.SetStateType(this.serializedType.stateType);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003F1C File Offset: 0x0000211C
		public void ApplyStatic()
		{
			Type stateType = this.serializedType.stateType;
			if (stateType != null)
			{
				foreach (EntityStateManager.StateInfo.Field field in this.stateFieldList)
				{
					FieldInfo field2 = stateType.GetField(field.GetFieldName(), BindingFlags.Static | BindingFlags.Public);
					if (field.MatchesFieldInfo(field2) && field2.IsStatic)
					{
						field.Apply(field2, null);
					}
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003FA8 File Offset: 0x000021A8
		public Action<EntityState> GenerateInstanceFieldInitializerDelegate()
		{
			Type stateType = this.serializedType.stateType;
			if (stateType == null)
			{
				return null;
			}
			List<EntityStateManager.StateInfo.FieldValuePair> list = new List<EntityStateManager.StateInfo.FieldValuePair>();
			for (int i = 0; i < this.stateFieldList.Count; i++)
			{
				EntityStateManager.StateInfo.Field field = this.stateFieldList[i];
				EntityStateManager.StateInfo.FieldValuePair fieldValuePair = new EntityStateManager.StateInfo.FieldValuePair
				{
					fieldInfo = stateType.GetField(field.GetFieldName(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy),
					value = field.valueAsSystemObject
				};
				if (!(fieldValuePair.fieldInfo == null))
				{
					list.Add(fieldValuePair);
				}
			}
			EntityStateManager.StateInfo.FieldValuePair[] fieldValuePairs = list.ToArray();
			if (fieldValuePairs.Length == 0)
			{
				return null;
			}
			return delegate(EntityState entityState)
			{
				for (int j = 0; j < fieldValuePairs.Length; j++)
				{
					EntityStateManager.StateInfo.FieldValuePair fieldValuePair2 = fieldValuePairs[j];
					fieldValuePair2.fieldInfo.SetValue(entityState, fieldValuePair2.value);
				}
			};
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004068 File Offset: 0x00002268
		public EntityStateManager.StateInfo.Field FindField(string fieldName)
		{
			return this.stateFieldList.Find((EntityStateManager.StateInfo.Field value) => value.GetFieldName() == fieldName);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004099 File Offset: 0x00002299
		public bool IsValid()
		{
			return this.serializedType.stateType != null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000040AC File Offset: 0x000022AC
		public IList<EntityStateManager.StateInfo.Field> GetFields()
		{
			return this.stateFieldList.AsReadOnly();
		}

		// Token: 0x04000088 RID: 136
		public SerializableEntityStateType serializedType;

		// Token: 0x04000089 RID: 137
		[FormerlySerializedAs("stateStaticFieldList")]
		[SerializeField]
		private List<EntityStateManager.StateInfo.Field> stateFieldList = new List<EntityStateManager.StateInfo.Field>();

		// Token: 0x0400008A RID: 138
		private const BindingFlags defaultInstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x0400008B RID: 139
		private const BindingFlags defaultStaticBindingFlags = BindingFlags.Static | BindingFlags.Public;

		// Token: 0x0200001C RID: 28
		[Serializable]
		public class Field
		{
			// Token: 0x0600008D RID: 141 RVA: 0x000040CC File Offset: 0x000022CC
			public Field(string fieldName)
			{
				this._fieldName = fieldName;
			}

			// Token: 0x0600008E RID: 142 RVA: 0x000040DC File Offset: 0x000022DC
			public void SetFieldInfo(FieldInfo fieldInfo)
			{
				this._fieldName = fieldInfo.Name;
				EntityStateManager.StateInfo.Field.ValueType valueType = EntityStateManager.StateInfo.Field.ValueType.Invalid;
				Type fieldType = fieldInfo.FieldType;
				if (fieldType == typeof(int))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.Int;
				}
				else if (fieldType == typeof(float))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.Float;
				}
				else if (fieldType == typeof(string))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.String;
				}
				else if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.Object;
				}
				else if (fieldType == typeof(bool))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.Bool;
				}
				else if (fieldType == typeof(AnimationCurve))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.AnimationCurve;
				}
				else if (fieldType == typeof(Vector3))
				{
					valueType = EntityStateManager.StateInfo.Field.ValueType.Vector3;
				}
				if (this._valueType != valueType)
				{
					this.ResetValues();
					this._valueType = valueType;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600008F RID: 143 RVA: 0x000041AF File Offset: 0x000023AF
			public EntityStateManager.StateInfo.Field.ValueType valueType
			{
				get
				{
					return this._valueType;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000090 RID: 144 RVA: 0x000041B7 File Offset: 0x000023B7
			public int intValue
			{
				get
				{
					return this._intValue;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000091 RID: 145 RVA: 0x000041BF File Offset: 0x000023BF
			public bool boolValue
			{
				get
				{
					return this._intValue != 0;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000092 RID: 146 RVA: 0x000041CA File Offset: 0x000023CA
			public float floatValue
			{
				get
				{
					return this._floatValue;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000093 RID: 147 RVA: 0x000041D2 File Offset: 0x000023D2
			public string stringValue
			{
				get
				{
					return this._stringValue;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000094 RID: 148 RVA: 0x000041DA File Offset: 0x000023DA
			public UnityEngine.Object objectValue
			{
				get
				{
					return this._objectValue;
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000095 RID: 149 RVA: 0x000041E2 File Offset: 0x000023E2
			public AnimationCurve animationCurveValue
			{
				get
				{
					return this._animationCurveValue;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000096 RID: 150 RVA: 0x000041EA File Offset: 0x000023EA
			public Vector3 vector3Value
			{
				get
				{
					return this._vector3Value;
				}
			}

			// Token: 0x06000097 RID: 151 RVA: 0x000041F4 File Offset: 0x000023F4
			public bool MatchesFieldInfo(FieldInfo fieldInfo)
			{
				if (fieldInfo == null)
				{
					return false;
				}
				Type fieldType = fieldInfo.FieldType;
				switch (this._valueType)
				{
				case EntityStateManager.StateInfo.Field.ValueType.Invalid:
					return false;
				case EntityStateManager.StateInfo.Field.ValueType.Int:
					return fieldType.IsAssignableFrom(typeof(int));
				case EntityStateManager.StateInfo.Field.ValueType.Float:
					return fieldType.IsAssignableFrom(typeof(float));
				case EntityStateManager.StateInfo.Field.ValueType.String:
					return fieldType.IsAssignableFrom(typeof(string));
				case EntityStateManager.StateInfo.Field.ValueType.Object:
					return this._objectValue == null || fieldType.IsAssignableFrom(this._objectValue.GetType());
				case EntityStateManager.StateInfo.Field.ValueType.Bool:
					return fieldType.IsAssignableFrom(typeof(bool));
				case EntityStateManager.StateInfo.Field.ValueType.AnimationCurve:
					return fieldType.IsAssignableFrom(typeof(AnimationCurve));
				case EntityStateManager.StateInfo.Field.ValueType.Vector3:
					return fieldType.IsAssignableFrom(typeof(Vector3));
				default:
					return false;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000098 RID: 152 RVA: 0x000042D0 File Offset: 0x000024D0
			public object valueAsSystemObject
			{
				get
				{
					switch (this._valueType)
					{
					case EntityStateManager.StateInfo.Field.ValueType.Invalid:
						return null;
					case EntityStateManager.StateInfo.Field.ValueType.Int:
						return this.intValue;
					case EntityStateManager.StateInfo.Field.ValueType.Float:
						return this.floatValue;
					case EntityStateManager.StateInfo.Field.ValueType.String:
						return this.stringValue;
					case EntityStateManager.StateInfo.Field.ValueType.Object:
						if (!this.objectValue)
						{
							return null;
						}
						return this.objectValue;
					case EntityStateManager.StateInfo.Field.ValueType.Bool:
						return this.boolValue;
					case EntityStateManager.StateInfo.Field.ValueType.AnimationCurve:
						return this.animationCurveValue;
					case EntityStateManager.StateInfo.Field.ValueType.Vector3:
						return this.vector3Value;
					default:
						return null;
					}
				}
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00004363 File Offset: 0x00002563
			public void Apply(FieldInfo fieldInfo, object instance)
			{
				fieldInfo.SetValue(instance, this.valueAsSystemObject);
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00004372 File Offset: 0x00002572
			public void ResetValues()
			{
				this._intValue = 0;
				this._floatValue = 0f;
				this._stringValue = null;
				this._objectValue = null;
				this._animationCurveValue = null;
				this._vector3Value = Vector3.zero;
			}

			// Token: 0x0600009B RID: 155 RVA: 0x000043A6 File Offset: 0x000025A6
			public void SetValue(int value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.Int;
				this._intValue = value;
			}

			// Token: 0x0600009C RID: 156 RVA: 0x000043BC File Offset: 0x000025BC
			public void SetValue(float value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.Float;
				this._floatValue = value;
			}

			// Token: 0x0600009D RID: 157 RVA: 0x000043D2 File Offset: 0x000025D2
			public void SetValue(string value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.String;
				this._stringValue = value;
			}

			// Token: 0x0600009E RID: 158 RVA: 0x000043E8 File Offset: 0x000025E8
			public void SetValue(UnityEngine.Object value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.Object;
				this._objectValue = value;
			}

			// Token: 0x0600009F RID: 159 RVA: 0x000043FE File Offset: 0x000025FE
			public void SetValue(bool value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.Bool;
				this._intValue = (value ? 1 : 0);
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x0000441A File Offset: 0x0000261A
			public void SetValue(AnimationCurve value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.AnimationCurve;
				this._animationCurveValue = value;
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x00004430 File Offset: 0x00002630
			public void SetValue(Vector3 value)
			{
				this.ResetValues();
				this._valueType = EntityStateManager.StateInfo.Field.ValueType.Vector3;
				this._vector3Value = value;
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00004446 File Offset: 0x00002646
			public string GetFieldName()
			{
				return this._fieldName;
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x0000444E File Offset: 0x0000264E
			public FieldInfo GetFieldInfo()
			{
				EntityStateManager.StateInfo stateInfo = this.owner;
				if (stateInfo == null)
				{
					return null;
				}
				Type stateType = stateInfo.serializedType.stateType;
				if (stateType == null)
				{
					return null;
				}
				return stateType.GetField(this._fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			}

			// Token: 0x0400008C RID: 140
			[NonSerialized]
			public EntityStateManager.StateInfo owner;

			// Token: 0x0400008D RID: 141
			[SerializeField]
			private string _fieldName;

			// Token: 0x0400008E RID: 142
			[SerializeField]
			private EntityStateManager.StateInfo.Field.ValueType _valueType;

			// Token: 0x0400008F RID: 143
			[SerializeField]
			private int _intValue;

			// Token: 0x04000090 RID: 144
			[SerializeField]
			private float _floatValue;

			// Token: 0x04000091 RID: 145
			[SerializeField]
			private string _stringValue;

			// Token: 0x04000092 RID: 146
			[SerializeField]
			private UnityEngine.Object _objectValue;

			// Token: 0x04000093 RID: 147
			[SerializeField]
			private AnimationCurve _animationCurveValue;

			// Token: 0x04000094 RID: 148
			[SerializeField]
			private Vector3 _vector3Value;

			// Token: 0x0200001D RID: 29
			public enum ValueType
			{
				// Token: 0x04000096 RID: 150
				Invalid,
				// Token: 0x04000097 RID: 151
				Int,
				// Token: 0x04000098 RID: 152
				Float,
				// Token: 0x04000099 RID: 153
				String,
				// Token: 0x0400009A RID: 154
				Object,
				// Token: 0x0400009B RID: 155
				Bool,
				// Token: 0x0400009C RID: 156
				AnimationCurve,
				// Token: 0x0400009D RID: 157
				Vector3
			}
		}

		// Token: 0x0200001E RID: 30
		private struct FieldValuePair
		{
			// Token: 0x0400009E RID: 158
			public FieldInfo fieldInfo;

			// Token: 0x0400009F RID: 159
			public object value;
		}
	}
}
