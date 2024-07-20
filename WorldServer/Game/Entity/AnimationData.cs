using WorldServer.Game.Entity.Enums;

namespace WorldServer.Game.Entity;

public struct AnimationData
{
    public MoveType AnimationType;
    public MoveSpeed AnimationSpeed;
    public MoveState AnimationState;
    public byte HeadPosition;
    public byte UnknownRotation;
}