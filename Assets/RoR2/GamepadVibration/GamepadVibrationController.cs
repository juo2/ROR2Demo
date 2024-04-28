using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HG.Reflection;
using Rewired;
using UnityEngine;

namespace RoR2.GamepadVibration
{
	// Token: 0x02000B63 RID: 2915
	public abstract class GamepadVibrationController : IDisposable
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x00112738 File Offset: 0x00110938
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x00112740 File Offset: 0x00110940
		public Joystick joystick
		{
			get
			{
				return this._joystick;
			}
			private set
			{
				if (this._joystick == value)
				{
					return;
				}
				this._joystick = value;
				if (this._joystick != null)
				{
					Array.Resize<float>(ref this.motorValues, this._joystick.vibrationMotorCount);
					this.OnJoystickAssigned(this._joystick);
				}
			}
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnJoystickAssigned(Joystick joystick)
		{
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x0011277D File Offset: 0x0011097D
		public void Dispose()
		{
			this.StopVibration();
			this.DisposeInternal();
			this.joystick = null;
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void DisposeInternal()
		{
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00112794 File Offset: 0x00110994
		public void ApplyVibration(in VibrationContext vibrationContext)
		{
			Array.Clear(this.motorValues, 0, this.motorValues.Length);
			try
			{
				this.CalculateMotorValues(vibrationContext, this.motorValues);
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			float userVibrationScale = vibrationContext.userVibrationScale;
			for (int i = 0; i < this.motorValues.Length; i++)
			{
				this.joystick.SetVibration(i, this.motorValues[i] * userVibrationScale);
			}
			try
			{
				this.ApplyNonStandardVibration(vibrationContext);
			}
			catch (Exception message2)
			{
				Debug.LogError(message2);
			}
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void CalculateMotorValues(in VibrationContext vibrationContext, float[] motorValues)
		{
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void ApplyNonStandardVibration(in VibrationContext vibrationContext)
		{
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x00112828 File Offset: 0x00110A28
		public void StopVibration()
		{
			try
			{
				this.StopNonStandardVibration();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			this.joystick.StopVibration();
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void StopNonStandardVibration()
		{
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00112860 File Offset: 0x00110A60
		public static GamepadVibrationController Create(Joystick joystick)
		{
			Type item = GamepadVibrationController.defaultVibrationControllerType;
			for (int i = 0; i < GamepadVibrationController.vibrationControllerTypeResolver.Count; i++)
			{
				try
				{
					ValueTuple<Func<Joystick, bool>, Type> valueTuple = GamepadVibrationController.vibrationControllerTypeResolver[i];
					if (valueTuple.Item1(joystick))
					{
						item = valueTuple.Item2;
						break;
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
			GamepadVibrationController gamepadVibrationController = (GamepadVibrationController)Activator.CreateInstance(item);
			gamepadVibrationController.joystick = joystick;
			return gamepadVibrationController;
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x001128D8 File Offset: 0x00110AD8
		public static void RegisterResolver(Func<Joystick, bool> predicate, Type vibrationControllerType)
		{
			Func<Joystick, bool> func = predicate;
			if (func == null)
			{
				throw new ArgumentNullException("predicate");
			}
			predicate = func;
			Type type = vibrationControllerType;
			if (type == null)
			{
				throw new ArgumentNullException("vibrationControllerType");
			}
			vibrationControllerType = type;
			if (!vibrationControllerType.IsSubclassOf(typeof(GamepadVibrationController)))
			{
				throw new ArgumentException("vibrationControllerType must inherit from GamepadVibrationController");
			}
			GamepadVibrationController.vibrationControllerTypeResolver.Add(new ValueTuple<Func<Joystick, bool>, Type>(predicate, vibrationControllerType));
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00112938 File Offset: 0x00110B38
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			List<GamepadVibrationControllerResolverAttribute> list = new List<GamepadVibrationControllerResolverAttribute>();
			SearchableAttribute.GetInstances<GamepadVibrationControllerResolverAttribute>(list);
			foreach (GamepadVibrationControllerResolverAttribute gamepadVibrationControllerResolverAttribute in list)
			{
				try
				{
					MethodInfo methodInfo = (MethodInfo)gamepadVibrationControllerResolverAttribute.target;
					if (!methodInfo.IsStatic)
					{
						throw new Exception("GamepadVibrationControllerResolverAttribute target must be a static method. target=" + methodInfo.DeclaringType.FullName + "." + methodInfo.Name);
					}
					GamepadVibrationController.RegisterResolver((Func<Joystick, bool>)methodInfo.CreateDelegate(typeof(Func<Joystick, bool>)), gamepadVibrationControllerResolverAttribute.vibrationControllerType);
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x04004053 RID: 16467
		private Joystick _joystick;

		// Token: 0x04004054 RID: 16468
		private float[] motorValues = Array.Empty<float>();

		// Token: 0x04004055 RID: 16469
		[TupleElementNames(new string[]
		{
			"predicate",
			"vibrationControllerType"
		})]
		private static List<ValueTuple<Func<Joystick, bool>, Type>> vibrationControllerTypeResolver = new List<ValueTuple<Func<Joystick, bool>, Type>>();

		// Token: 0x04004056 RID: 16470
		private static Type defaultVibrationControllerType = typeof(DefaultGamepadVibrationController);
	}
}
