using UnityEngine;

public interface IHurt
{
    // bool Hurt(Skill_HitData hitData, ISkillOwner hurtSource);
    void Hurt(int damage, IStateMachineOwner attacker);
}
