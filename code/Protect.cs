using Sandbox.UI;
namespace Sandbox
{
  public partial class Protect
  {

    public void SendError( Player sbp, string name, string message, bool showMessage )
    {
      if (!showMessage) return; 
      if (sbp == null) return;
      ChatBox.AddChatEntry(To.Single(sbp),name,message);
    }

    public bool SameOwner( Player owner, Entity entity, bool IgnoreWorld = false, bool showMessage = true )
    {
      var eowner = entity.Owner;
      if(entity is WorldEntity && IgnoreWorld == true) return true;
      var isOwner = (entity.Owner == owner);
      if(!isOwner) SendError(owner, "Owner Error", "This entity is not yours", showMessage);
      return isOwner;
    }
    public bool InVehicle( Player player, bool showMessage = false )
    {
      var isDriving = player.Tags.Has("driving");
      if(isDriving) SendError(player, "Vehicle", "You can't do that in vehicle", showMessage);
      return isDriving;
    }

    public bool NeedRestrictPVPWeapon(Player player, Weapon weapon, bool showMessage = false )
    {
      var sbp = player as SandboxPlayer;
      if(sbp == null) return false;
      if(sbp.GameMode == (int) SandboxPlayer.GM.BUILD){
        if(weapon != null) {
           weapon.Remove();
           SendError(sbp, "Warning :", "You can't spawn (" + weapon.ClassInfo.Name + ") in this mode, switch to PVP :)", showMessage );
           sbp.Inventory.SetActiveSlot(0,false);
          return true;
        }
      }
      return false;
    }


  }
}