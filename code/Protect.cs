using Sandbox.UI;
namespace Sandbox
{
  public partial class Protect
  {
    public bool SameOwner( Player owner, Entity entity, bool IgnoreWorld = false)
    {
      var eowner = entity.Owner;
      if(entity is WorldEntity && IgnoreWorld == true) return true;
      return (entity.Owner == owner);
    }
    public bool InVehicle( Player player )
    {
      return player.Tags.Has("driving");
    }

    public bool NeedRestrictPVPWeapon(Player player, Weapon weapon )
    {
      var sbp = player as SandboxPlayer;
      if(sbp == null) return false;
      if(sbp.GameMode == (int) SandboxPlayer.GM.BUILD){
        if(weapon != null) {
           weapon.Remove();
           ChatBox.AddChatEntry( To.Single( sbp ), "Warning :", "You can't spawn (" + weapon.ClassInfo.Name + ") in this mode, switch to PVP :)" );
           sbp.Inventory.SetActiveSlot(0,false);
          return true;
        }
      }
      return false;
    }


  }
}