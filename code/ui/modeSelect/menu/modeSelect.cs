

using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

[Library]
public partial class modeSelect : Panel
{
	public static modeSelect Instance;

	private static Button pvp;
	private static Label pvpLock;
	private static Button build;
	private static Label buildLock;
	private static Button propkill;
	private static Label propkillLabel;
	private static Label propkillLock;
	private static Button close;



	readonly Panel Inner;

	public modeSelect()
	{
		StyleSheet.Load( "code/ui/modeSelect/menu/modeSelect.scss" );

		AddClass( "menuopen" );
		Add.Label( "Choose Mode" );
		Inner = Add.Panel( "inner" );

		pvp = Inner.Add.Button( "⚔️", () => PVP() );
		pvp.Add.Label( "PVP MODE" );
		pvpLock = pvp.Add.Label( "" );
		build = Inner.Add.Button( "🏗️", () => BUILD() );
		build.Add.Label( "BUILD MODE" );
		buildLock = build.Add.Label( "" );

		propkill = Inner.Add.Button( "📦", () => PKSWITCH() );
		propkillLabel = propkill.Add.Label();
		propkillLock = propkill.Add.Label();

		close = Inner.Add.Button( "❌", () => CLOSE() );
		close.Add.Label( "FERMER" );
		close.SetClass( "firstTime", true );

	}

	public async void CLOSE()
	{
		AddClass( "menuclose" );
		RemoveClass( "menuopen" );
		await Task.Delay( 1000 );
		close.RemoveClass( "firstTime" );
	}

	public void BUILD()
	{
		SandboxPlayer.SetBUILDMode();
		CLOSE();
	}

	public void PVP()
	{
		SandboxPlayer.SetPVPMode();
		CLOSE();
	}

	public void PKSWITCH()
	{
		SandboxPlayer.SetPropKillModeSwitch();
	}

	public string getTimeOut(int wait, bool islock)
	{
		var zero = (wait >= 10 ? "" : "0");
		var rwait = zero + wait ;
		return islock ? "SlowMode, Wait " + rwait + " Seconds" : "";
	}

	public override void Tick()
	{
		base.Tick();
		var p = (Local.Pawn as SandboxPlayer);
		if ( p == null ) return;

		if ( pvp != null && build != null )
		{
			var islock = p.ModeLock == true;
			var l = islock ? "lock" : "unlock";
			var u = !islock ? "lock" : "unlock";
			var txt = getTimeOut(p.ModeLockWait, islock);
			pvpLock.Text = txt;
			buildLock.Text = txt;
			pvp.AddClass( l );
			pvp.RemoveClass( u );
			build.AddClass( l );
			build.RemoveClass( u );
		}

		if ( propkill != null && propkillLabel != null )
		{
			var islock = p.PKLockSwitch == true;
			var txt = getTimeOut(p.PKLockWait, islock);
			propkillLock.Text = txt;
			propkill.AddClass( islock ? "lock" : "unlock" );
			propkill.RemoveClass( !islock ? "lock" : "unlock" );
			propkillLabel.Text = "PROPKILL MODE " + (islock ? "+" : "-");
		}

		if ( Input.Down( InputButton.Slot0 ) )
		{
			SetClass( "menuopen", true );
		}
	}
}