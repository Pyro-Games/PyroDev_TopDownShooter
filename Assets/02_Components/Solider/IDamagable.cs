
public struct AttackDefiniton
{
    public IAttackable attacker;
    // public IDamagable target;
    public float damagePoint;
}

public interface IDamagable
{
    void GetDamage(AttackDefiniton attackDefiniton);

}
