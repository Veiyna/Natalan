using WorldServer.Game.Entity;

namespace WorldServer.Game.AI.Fsm;

public abstract class Condition
{
    public abstract bool IsConditionMet(BNpc src);

    public virtual bool Update(BNpc src, float time)
    {
        return IsConditionMet(src);
    }
}