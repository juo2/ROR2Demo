using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using EntityStates;
using HG;
using HG.GeneralSerializer;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000532 RID: 1330
	[CreateAssetMenu(menuName = "RoR2/EntityStateConfiguration")]
	public class EntityStateConfiguration : ScriptableObject
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x0006A068 File Offset: 0x00068268
		[ContextMenu("Set Name from Target Type")]
		public void SetNameFromTargetType()
		{
			Type type = (Type)this.targetType;
			if (type == null)
			{
				return;
			}
			base.name = type.FullName;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0006A098 File Offset: 0x00068298
		public void ApplyStatic()
		{
			Type type = (Type)this.targetType;
			if (type == null)
			{
				return;
			}
			foreach (SerializedField serializedField in this.serializedFieldsCollection.serializedFields)
			{
				try
				{
					FieldInfo field = type.GetField(serializedField.fieldName, BindingFlags.Static | BindingFlags.Public);
					if (field != null)
					{
						FieldInfo fieldInfo = field;
						object obj = null;
						SerializedValue fieldValue = serializedField.fieldValue;
						fieldInfo.SetValue(obj, fieldValue.GetValue(field));
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0006A124 File Offset: 0x00068324
		[CanBeNull]
		public Action<object> BuildInstanceInitializer()
		{
			EntityStateConfiguration.<>c__DisplayClass4_0 CS$<>8__locals1 = new EntityStateConfiguration.<>c__DisplayClass4_0();
			Type type = (Type)this.targetType;
			if (type == null)
			{
				return null;
			}
			SerializedField[] serializedFields = this.serializedFieldsCollection.serializedFields;
			List<ValueTuple<FieldInfo, object>> list = CollectionPool<ValueTuple<FieldInfo, object>, List<ValueTuple<FieldInfo, object>>>.RentCollection();
			for (int i = 0; i < serializedFields.Length; i++)
			{
				ref SerializedField ptr = ref serializedFields[i];
				try
				{
					FieldInfo field = type.GetField(ptr.fieldName);
					if (!(field == null))
					{
						if (EntityStateConfiguration.<BuildInstanceInitializer>g__FieldPassesFilter|4_0(field))
						{
							list.Add(new ValueTuple<FieldInfo, object>(field, ptr.fieldValue.GetValue(field)));
						}
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
			if (list.Count == 0)
			{
				list = CollectionPool<ValueTuple<FieldInfo, object>, List<ValueTuple<FieldInfo, object>>>.ReturnCollection(list);
				return null;
			}
			CS$<>8__locals1.fieldValuesArray = list.ToArray();
			list = CollectionPool<ValueTuple<FieldInfo, object>, List<ValueTuple<FieldInfo, object>>>.ReturnCollection(list);
			return new Action<object>(CS$<>8__locals1.<BuildInstanceInitializer>g__InitializeObject|1);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0006A204 File Offset: 0x00068404
		private void Awake()
		{
			this.serializedFieldsCollection.PurgeUnityPsuedoNullFields();
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0006A211 File Offset: 0x00068411
		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				RoR2Application.onNextUpdate += delegate()
				{
					Action<EntityStateConfiguration> action = EntityStateConfiguration.onEditorUpdatedConfigurationGlobal;
					if (action == null)
					{
						return;
					}
					action(this);
				};
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001835 RID: 6197 RVA: 0x0006A22C File Offset: 0x0006842C
		// (remove) Token: 0x06001836 RID: 6198 RVA: 0x0006A260 File Offset: 0x00068460
		public static event Action<EntityStateConfiguration> onEditorUpdatedConfigurationGlobal;

		// Token: 0x06001838 RID: 6200 RVA: 0x0006A293 File Offset: 0x00068493
		[CompilerGenerated]
		internal static bool <BuildInstanceInitializer>g__FieldPassesFilter|4_0(FieldInfo fieldInfo)
		{
			return fieldInfo.GetCustomAttribute<SerializeField>() != null;
		}

		// Token: 0x04001DCE RID: 7630
		[SerializableSystemType.RequiredBaseTypeAttribute(typeof(EntityState))]
		public SerializableSystemType targetType;

		// Token: 0x04001DCF RID: 7631
		public SerializedFieldCollection serializedFieldsCollection;
	}
}
