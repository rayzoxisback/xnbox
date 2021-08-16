using Sandbox;
using System;
partial class SandboxPlayer
{
	[Net] public bool ModeLock { get; set; }
	[Net] public int ModeLockWait { get; set; }
	[Net] public int GameMode { get; set; } // 1 == Build - 2 == PVP

	public void GameModeInit()
	{
		GameMode = 1;
		base.Spawn();
	}

	public void RespawnRules()
	{
		Inventory.DeleteContents();
		Inventory.Add( new Flashlight() );
		if ( GameMode == 1 )
		{
			AntiDamage = true;
			Inventory.Add( new GravGun() );
			Inventory.Add( new Tool() );
			Inventory.Add( new PhysGun(), true );
		}
		else if ( GameMode == 2 )
		{
			AntiDamage = false;
			Inventory.Add( new SMG() );
			Inventory.Add( new Shotgun() );
			Inventory.Add( new Pistol(), true );
		}
	}

	public async void TempLockModeInc( int i )
	{
		if ( i == 0 ) { ModeLockWait = 0; return; }
		await Task.Delay( 1000 );
		i--;
		ModeLockWait = i;
		TempLockModeInc( i );
		return;
	}
	public async void TempLockMode()
	{
		var waitS = 20;
		ModeLockWait = waitS;
		TempLockModeInc( waitS );
		ModeLock = true;
		await Task.Delay( waitS * 1000 );
		ModeLock = false;
	}


	[ServerCmd( "pvp_mode" )]

	public static void SetPVPMode()
	{
		var target = ConsoleSystem.Caller.Pawn as SandboxPlayer;
		if ( target == null || (target.GameMode == 2) ) return;
		if ( target.ModeLock ) return;
		target.GameMode = 2;
		target.TempLockMode();
		target.Respawn();
		return;
	}

	[ServerCmd( "build_mode" )]

	public static void SetBUILDMode()
	{
		var target = ConsoleSystem.Caller.Pawn as SandboxPlayer;
		if ( target == null || (target.GameMode == 1) ) return;
		if ( target.ModeLock ) return;
		target.GameMode = 1;
		target.TempLockMode();
		target.Respawn();
		return;
	}

};