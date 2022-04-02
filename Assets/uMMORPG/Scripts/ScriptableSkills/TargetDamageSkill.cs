using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName="uMMORPG Skill/Target Damage", order=999)]
public class TargetDamageSkill : DamageSkill
{

    public Vector3 lastpos = new Vector3(0, 0, 0);
    public Vector3 lastposmonst = new Vector3(0, 0, 0);

    //Have animation still happen even if no target, just for funsies

    void Update() { 
        
    }

    public override bool CheckTarget(Entity caster)
    {
        // Later check if there's a monster attacking you or within your cast range
        return caster.target != null && caster.CanAttack(caster.target);
    }

    public override bool CheckDistance(Entity caster, int skillLevel, out Vector3 destination)
    {
        // target still around?
        if (caster.target != null)
        {
            // Techincally works, just make it prettier/feel better to play with, maybe get different animations too
            Vector3 vel = (caster.transform.position - lastpos) / Time.deltaTime;
            lastpos = caster.transform.position;

            Vector3 velMost = (caster.target.transform.position - lastposmonst) / Time.deltaTime;
            lastposmonst = caster.target.transform.position;

            if((vel - velMost).magnitude * castTime.baseValue * 2 + (caster.transform.position - caster.target.transform.position).magnitude <= castRange.baseValue)
            {
                destination = Utils.ClosestPoint(caster.target, caster.transform.position);
                return Utils.ClosestDistance(caster, caster.target) <= castRange.Get(skillLevel);
            }
           
        }
        destination = caster.transform.position;
        return false;
    }

    public override void Apply(Entity caster, int skillLevel)
    {
        // deal damage directly with base damage + skill damage
        caster.combat.DealDamageAt(caster.target,
                                   caster.combat.damage + damage.Get(skillLevel),
                                   stunChance.Get(skillLevel),
                                   stunTime.Get(skillLevel));
    }
}
