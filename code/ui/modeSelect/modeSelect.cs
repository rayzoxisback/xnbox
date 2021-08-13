

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
  private static Button close;
  readonly Panel Inner;

  public modeSelect()
  {
    StyleSheet.Load("code/ui/modeSelect/modeSelect.scss");

    AddClass( "menuopen" );
    Add.Label("Choose Mode");
    Inner = Add.Panel( "inner" );

    var pvp = Inner.Add.Button("⚔️", () => PVP()).Add.Label("PVP MODE");

    var build = Inner.Add.Button("🏗️", () => BUILD()).Add.Label("BUILD MODE");

    close = Inner.Add.Button("❌", () => CLOSE());
    close.Add.Label("FERMER");
    close.SetClass("firstTime", true);

  }

  public async void CLOSE() {
    AddClass("menuclose");
    RemoveClass("menuopen");
    await Task.Delay(1000);
    close.RemoveClass("firstTime");
  }

  public void BUILD() {
      SandboxPlayer.SetBUILDMode();
      CLOSE();
  }

  public void PVP(){
    SandboxPlayer.SetPVPMode();
    CLOSE();
  }

  public override void Tick()
	{
		base.Tick();
    
    if(Input.Down(InputButton.Slot0)){
      Log.Info("click");
      SetClass("menuopen", true);
    }
	}
}







// using Sandbox;
// using Sandbox.UI;
// using Sandbox.UI.Construct;

// namespace Sandbox.UI
// {
//   public partial class modeSelect<T> : Panel where T : modeSelectEntry, new()
//   {

//     public Panel Inner { get; protected set;}

//     public modeSelect()
//     {
//       StyleSheet.Load("code/ui/modeSelect/modeSelect.scss");

//       AddClass( "menu" );
//       Inner = Add.Panel( "inner" );
//       Inner.Add.Button("<text class=\"icon\">⚔️</text><text class=\"desc\">PVP MODE</text>");
//       Inner.Add.Button("<text class=\"icon\">🏗️</text><text class=\"desc\">BUILD MODE</text>");

//     }


//     public override void Tick()
// 		{
//       Log.Info("ici");
// 			base.Tick();

// 			SetClass( "open", Input.Down( InputButton.Score ) );
// 		}
//   }

  
// }