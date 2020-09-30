public interface IAttackable
{
    AttackBehaviour CurrentAttackBehaviour { get; set; }

    void OnExecuteAttack(int animationIndex);
}
