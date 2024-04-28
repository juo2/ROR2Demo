using System;
using EntityStates;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200062D RID: 1581
	public class CharacterDeathBehavior : MonoBehaviour
	{
		// Token: 0x06001DE4 RID: 7652 RVA: 0x000803F4 File Offset: 0x0007E5F4
		public void OnDeath()
		{
			if (Util.HasEffectiveAuthority(base.gameObject))
			{
				if (this.deathStateMachine)
				{
					this.deathStateMachine.SetNextState(EntityStateCatalog.InstantiateState(this.deathState));
				}
				EntityStateMachine[] array = this.idleStateMachine;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetNextState(new Idle());
				}
			}
			base.gameObject.layer = LayerIndex.debris.intVal;
			CharacterMotor component = base.GetComponent<CharacterMotor>();
			if (component)
			{
				component.Motor.RebuildCollidableLayers();
			}
			ILifeBehavior[] components = base.GetComponents<ILifeBehavior>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].OnDeathStart();
			}
			ModelLocator component2 = base.GetComponent<ModelLocator>();
			if (component2)
			{
				Transform modelTransform = component2.modelTransform;
				if (modelTransform)
				{
					components = modelTransform.GetComponents<ILifeBehavior>();
					for (int i = 0; i < components.Length; i++)
					{
						components[i].OnDeathStart();
					}
				}
			}
		}

		// Token: 0x040023BB RID: 9147
		[Tooltip("The state machine to set the state of when this character is killed.")]
		public EntityStateMachine deathStateMachine;

		// Token: 0x040023BC RID: 9148
		[Tooltip("The state to enter when this character is killed.")]
		public SerializableEntityStateType deathState;

		// Token: 0x040023BD RID: 9149
		[Tooltip("The state machine(s) to set to idle when this character is killed.")]
		public EntityStateMachine[] idleStateMachine;
	}
}
