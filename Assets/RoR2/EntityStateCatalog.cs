using System;
using System.Collections.Generic;
using System.Linq;
using EntityStates;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005A6 RID: 1446
	public static class EntityStateCatalog
	{
		// Token: 0x06001A0F RID: 6671 RVA: 0x000708C4 File Offset: 0x0006EAC4
		private static void SetStateInstanceInitializer([NotNull] Type stateType, [NotNull] Action<object> initializer)
		{
			if (!typeof(EntityState).IsAssignableFrom(stateType))
			{
				return;
			}
			EntityStateCatalog.instanceFieldInitializers[stateType] = initializer;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000708E8 File Offset: 0x0006EAE8
		public static void InitializeStateFields([NotNull] EntityState entityState)
		{
			Action<object> action;
			if (EntityStateCatalog.instanceFieldInitializers.TryGetValue(entityState.GetType(), out action))
			{
				action(entityState);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00070910 File Offset: 0x0006EB10
		private static void SetElements(Type[] newEntityStateTypes, EntityStateConfiguration[] newEntityStateConfigurations)
		{
			ArrayUtils.CloneTo<Type>(newEntityStateTypes, ref EntityStateCatalog.stateIndexToType);
			string[] array = new string[EntityStateCatalog.stateIndexToType.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = EntityStateCatalog.stateIndexToType[i].AssemblyQualifiedName;
			}
			Array.Sort<string, Type>(array, EntityStateCatalog.stateIndexToType, StringComparer.Ordinal);
			EntityStateCatalog.stateTypeToIndex.Clear();
			for (int j = 0; j < EntityStateCatalog.stateIndexToType.Length; j++)
			{
				Type key = EntityStateCatalog.stateIndexToType[j];
				EntityStateCatalog.stateTypeToIndex[key] = (EntityStateIndex)j;
			}
			Array.Resize<string>(ref EntityStateCatalog.stateIndexToTypeName, EntityStateCatalog.stateIndexToType.Length);
			for (int k = 0; k < EntityStateCatalog.stateIndexToType.Length; k++)
			{
				EntityStateCatalog.stateIndexToTypeName[k] = EntityStateCatalog.stateIndexToType[k].Name;
			}
			EntityStateCatalog.instanceFieldInitializers.Clear();
			for (int l = 0; l < newEntityStateConfigurations.Length; l++)
			{
				EntityStateCatalog.ApplyEntityStateConfiguration(newEntityStateConfigurations[l]);
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000709F8 File Offset: 0x0006EBF8
		private static void ApplyEntityStateConfiguration(EntityStateConfiguration entityStateConfiguration)
		{
			Type type = (Type)entityStateConfiguration.targetType;
			if (type == null)
			{
				Debug.LogFormat("ApplyEntityStateConfiguration({0}) failed: state type is null.", new object[]
				{
					entityStateConfiguration
				});
				return;
			}
			if (!EntityStateCatalog.stateTypeToIndex.ContainsKey(type))
			{
				Debug.LogFormat("ApplyEntityStateConfiguration({0}) failed: state type {1} is not registered.", new object[]
				{
					entityStateConfiguration,
					type.FullName
				});
				return;
			}
			entityStateConfiguration.ApplyStatic();
			Action<object> action = entityStateConfiguration.BuildInstanceInitializer();
			if (action == null)
			{
				EntityStateCatalog.instanceFieldInitializers.Remove(type);
				return;
			}
			EntityStateCatalog.instanceFieldInitializers[type] = action;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00070A83 File Offset: 0x0006EC83
		private static void OnStateConfigurationUpdatedByEditor(EntityStateConfiguration entityStateConfiguration)
		{
			EntityStateCatalog.ApplyEntityStateConfiguration(entityStateConfiguration);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00070A8B File Offset: 0x0006EC8B
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			EntityStateCatalog.SetElements(ContentManager.entityStateTypes, ContentManager.entityStateConfigurations);
			EntityStateConfiguration.onEditorUpdatedConfigurationGlobal += EntityStateCatalog.OnStateConfigurationUpdatedByEditor;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00070AB0 File Offset: 0x0006ECB0
		public static EntityStateIndex GetStateIndex(Type entityStateType)
		{
			EntityStateIndex result;
			if (EntityStateCatalog.stateTypeToIndex.TryGetValue(entityStateType, out result))
			{
				return result;
			}
			return EntityStateIndex.Invalid;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00070AD0 File Offset: 0x0006ECD0
		public static Type GetStateType(EntityStateIndex entityStateIndex)
		{
			Type[] array = EntityStateCatalog.stateIndexToType;
			Type type = null;
			return ArrayUtils.GetSafe<Type>(array, (int)entityStateIndex, type);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00070AEC File Offset: 0x0006ECEC
		[CanBeNull]
		public static string GetStateTypeName(Type entityStateType)
		{
			return EntityStateCatalog.GetStateTypeName(EntityStateCatalog.GetStateIndex(entityStateType));
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00070AF9 File Offset: 0x0006ECF9
		[CanBeNull]
		public static string GetStateTypeName(EntityStateIndex entityStateIndex)
		{
			return ArrayUtils.GetSafe<string>(EntityStateCatalog.stateIndexToTypeName, (int)entityStateIndex);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00070B08 File Offset: 0x0006ED08
		public static EntityState InstantiateState(EntityStateIndex entityStateIndex)
		{
			Type stateType = EntityStateCatalog.GetStateType(entityStateIndex);
			if (stateType != null)
			{
				return Activator.CreateInstance(stateType) as EntityState;
			}
			Debug.LogFormat("Bad stateTypeIndex {0}", new object[]
			{
				entityStateIndex
			});
			return null;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00070B4C File Offset: 0x0006ED4C
		public static EntityState InstantiateState(Type stateType)
		{
			if (stateType != null && stateType.IsSubclassOf(typeof(EntityState)))
			{
				return Activator.CreateInstance(stateType) as EntityState;
			}
			Debug.LogFormat("Bad stateType {0}", new object[]
			{
				(stateType == null) ? "null" : stateType.FullName
			});
			return null;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00070BAA File Offset: 0x0006EDAA
		public static EntityState InstantiateState(SerializableEntityStateType serializableStateType)
		{
			return EntityStateCatalog.InstantiateState(serializableStateType.stateType);
		}

		// Token: 0x04002052 RID: 8274
		private static readonly Dictionary<Type, EntityStateIndex> stateTypeToIndex = new Dictionary<Type, EntityStateIndex>();

		// Token: 0x04002053 RID: 8275
		private static Type[] stateIndexToType = Array.Empty<Type>();

		// Token: 0x04002054 RID: 8276
		private static readonly Dictionary<Type, Action<object>> instanceFieldInitializers = new Dictionary<Type, Action<object>>();

		// Token: 0x04002055 RID: 8277
		private static string[] stateIndexToTypeName = Array.Empty<string>();

		// Token: 0x04002056 RID: 8278
		[Obsolete("This is only for use in legacy editors only until they're fully phased out.")]
		public static string[] baseGameStateTypeNames = (from type in typeof(EntityState).Assembly.GetTypes()
		where typeof(EntityState).IsAssignableFrom(type)
		select type.FullName into typeName
		orderby typeName
		select typeName).ToArray<string>();
	}
}
