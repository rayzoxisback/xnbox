

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
  }
}