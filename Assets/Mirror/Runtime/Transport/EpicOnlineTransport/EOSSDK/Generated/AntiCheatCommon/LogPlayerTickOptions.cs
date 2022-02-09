// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.AntiCheatCommon
{
	public class LogPlayerTickOptions
	{
		/// <summary>
		/// Locally unique value used in RegisterClient/RegisterPeer
		/// </summary>
		public System.IntPtr PlayerHandle { get; set; }

		/// <summary>
		/// Player's current world position as a 3D vector
		/// </summary>
		public Vec3f PlayerPosition { get; set; }

		/// <summary>
		/// Player's view rotation as a quaternion
		/// </summary>
		public Quat PlayerViewRotation { get; set; }

		/// <summary>
		/// True if the player's view is zoomed (e.g. using a sniper rifle), otherwise false
		/// </summary>
		public bool IsPlayerViewZoomed { get; set; }

		/// <summary>
		/// Player's current health value
		/// </summary>
		public float PlayerHealth { get; set; }

		/// <summary>
		/// Any movement state applicable
		/// </summary>
		public AntiCheatCommonPlayerMovementState PlayerMovementState { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerTickOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_PlayerHandle;
		private System.IntPtr m_PlayerPosition;
		private System.IntPtr m_PlayerViewRotation;
		private int m_IsPlayerViewZoomed;
		private float m_PlayerHealth;
		private AntiCheatCommonPlayerMovementState m_PlayerMovementState;

		public System.IntPtr PlayerHandle
		{
			set
			{
				m_PlayerHandle = value;
			}
		}

		public Vec3f PlayerPosition
		{
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, Vec3f>(ref m_PlayerPosition, value);
			}
		}

		public Quat PlayerViewRotation
		{
			set
			{
				Helper.TryMarshalSet<QuatInternal, Quat>(ref m_PlayerViewRotation, value);
			}
		}

		public bool IsPlayerViewZoomed
		{
			set
			{
				Helper.TryMarshalSet(ref m_IsPlayerViewZoomed, value);
			}
		}

		public float PlayerHealth
		{
			set
			{
				m_PlayerHealth = value;
			}
		}

		public AntiCheatCommonPlayerMovementState PlayerMovementState
		{
			set
			{
				m_PlayerMovementState = value;
			}
		}

		public void Set(LogPlayerTickOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = AntiCheatCommonInterface.LogplayertickApiLatest;
				PlayerHandle = other.PlayerHandle;
				PlayerPosition = other.PlayerPosition;
				PlayerViewRotation = other.PlayerViewRotation;
				IsPlayerViewZoomed = other.IsPlayerViewZoomed;
				PlayerHealth = other.PlayerHealth;
				PlayerMovementState = other.PlayerMovementState;
			}
		}

		public void Set(object other)
		{
			Set(other as LogPlayerTickOptions);
		}

		public void Dispose()
		{
			Helper.TryMarshalDispose(ref m_PlayerHandle);
			Helper.TryMarshalDispose(ref m_PlayerPosition);
			Helper.TryMarshalDispose(ref m_PlayerViewRotation);
		}
	}
}