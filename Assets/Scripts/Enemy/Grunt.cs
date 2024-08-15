using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemyController
{
    [Header("Skill")]
    public float kickForce = 10f;


    //public void KickOff()
    //{
    //    if (attackTarget != null)
    //    { 
    //        transform.LookAt(attackTarget.transform);

    //        Vector3 direction = attackTarget.transform.position - transform.position;
    //        direction.Normalize();

    //        attackTarget.GetComponent<CharacterController>().Move(direction * kickForce);
    //    }
    //}

}
