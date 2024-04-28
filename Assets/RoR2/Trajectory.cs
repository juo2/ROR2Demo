using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A8D RID: 2701
	public struct Trajectory
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000FFBFC File Offset: 0x000FDDFC
		private static float defaultGravity
		{
			get
			{
				return Physics.gravity.y;
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x000FFC08 File Offset: 0x000FDE08
		public static float CalculateApex(float initialSpeed)
		{
			return Trajectory.CalculateApex(initialSpeed, Trajectory.defaultGravity);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x000FFC15 File Offset: 0x000FDE15
		public static float CalculateApex(float initialSpeed, float gravity)
		{
			return initialSpeed * initialSpeed / (2f * -gravity);
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x000FFC23 File Offset: 0x000FDE23
		public static float CalculateGroundSpeed(float time, float distance)
		{
			return distance / time;
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x000FFC23 File Offset: 0x000FDE23
		public static float CalculateGroundTravelTime(float hSpeed, float hDistance)
		{
			return hDistance / hSpeed;
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000FFC28 File Offset: 0x000FDE28
		public static float CalculateInitialYSpeed(float timeToTarget, float destinationYOffset)
		{
			return Trajectory.CalculateInitialYSpeed(timeToTarget, destinationYOffset, Trajectory.defaultGravity);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000FFC36 File Offset: 0x000FDE36
		public static float CalculateInitialYSpeed(float timeToTarget, float destinationYOffset, float gravity)
		{
			return (destinationYOffset + 0.5f * -gravity * timeToTarget * timeToTarget) / timeToTarget;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x000FFC48 File Offset: 0x000FDE48
		public static float CalculateInitialYSpeedForHeight(float height)
		{
			return Trajectory.CalculateInitialYSpeedForHeight(height, Trajectory.defaultGravity);
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x000FFC55 File Offset: 0x000FDE55
		public static float CalculateInitialYSpeedForHeight(float height, float gravity)
		{
			return Mathf.Sqrt(height * (2f * -gravity));
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000FFC66 File Offset: 0x000FDE66
		public static Vector3 CalculatePositionAtTime(Vector3 origin, Vector3 initialVelocity, float t)
		{
			return Trajectory.CalculatePositionAtTime(origin, initialVelocity, t, Trajectory.defaultGravity);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000FFC78 File Offset: 0x000FDE78
		public static Vector3 CalculatePositionAtTime(Vector3 origin, Vector3 initialVelocity, float t, float gravity)
		{
			Vector3 result = origin + initialVelocity * t;
			result.y += 0.5f * gravity * t * t;
			return result;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000FFCAA File Offset: 0x000FDEAA
		public static float CalculatePositionYAtTime(float originY, float initialVelocityY, float t)
		{
			return Trajectory.CalculatePositionYAtTime(originY, initialVelocityY, t, Trajectory.defaultGravity);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000FFCB9 File Offset: 0x000FDEB9
		public static float CalculatePositionYAtTime(float originY, float initialVelocityY, float t, float gravity)
		{
			return originY + initialVelocityY * t + 0.5f * gravity * t * t;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000FFCCC File Offset: 0x000FDECC
		public static float CalculateInitialYSpeedForFlightDuration(float duration)
		{
			return Trajectory.CalculateInitialYSpeedForFlightDuration(duration, Trajectory.defaultGravity);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000FFCD9 File Offset: 0x000FDED9
		public static float CalculateInitialYSpeedForFlightDuration(float duration, float gravity)
		{
			return duration * gravity * -0.5f;
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000FFCE4 File Offset: 0x000FDEE4
		public static float CalculateFlightDuration(float vSpeed)
		{
			return Trajectory.CalculateFlightDuration(vSpeed, Trajectory.defaultGravity);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000FFCF1 File Offset: 0x000FDEF1
		public static float CalculateFlightDuration(float vSpeed, float gravity)
		{
			return 2f * vSpeed / -gravity;
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000FFCFD File Offset: 0x000FDEFD
		public static float CalculateFlightDuration(float originY, float endY, float vSpeed)
		{
			return Trajectory.CalculateFlightDuration(originY, endY, vSpeed, Trajectory.defaultGravity);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000FFD0C File Offset: 0x000FDF0C
		public static float CalculateFlightDuration(float originY, float endY, float vSpeed, float gravity)
		{
			float num = endY - originY;
			float num2 = Mathf.Sqrt(vSpeed * vSpeed - 4f * (0.5f * gravity) * -num);
			return (-vSpeed - num2) / gravity;
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x000FFD3D File Offset: 0x000FDF3D
		public static float CalculateGroundSpeedToClearDistance(float vSpeed, float distance)
		{
			return Trajectory.CalculateGroundSpeedToClearDistance(vSpeed, distance, Trajectory.defaultGravity);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000FFD4B File Offset: 0x000FDF4B
		public static float CalculateGroundSpeedToClearDistance(float vSpeed, float distance, float gravity)
		{
			return distance / Trajectory.CalculateFlightDuration(vSpeed, gravity);
		}
	}
}
