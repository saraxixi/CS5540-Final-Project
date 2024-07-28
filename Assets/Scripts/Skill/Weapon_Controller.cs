using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    [SerializeField] private new Collider collider;
    [SerializeField] private MeleeWeaponTrail weaponTrail;

    private List<string> enemyTagList;
    private List<IHurt> enemyList = new List<IHurt>();
    private Action<IHurt, Vector3> onHitAction;

    public void Init(List<string> enemyTagList, Action<IHurt, Vector3> onHitAction)
    {
        collider.enabled = false;
        this.enemyTagList = enemyTagList;
        this.onHitAction = onHitAction;
        weaponTrail.Emit = false;
    }

    public void StartSkillHit()
    {
        weaponTrail.Emit = true;
        collider.enabled = true;
    }

    public void StopSkillHit()
    {
        weaponTrail.Emit = false;
        collider.enabled = false;
        enemyList.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        // Check hit target
        if (enemyTagList.Contains(other.tag))
        {
            IHurt enemy = other.GetComponentInParent<IHurt>();

            // If this hit target has been hit before, ignore it
            if (enemy != null && !enemyList.Contains(enemy))
            {
                onHitAction?.Invoke(enemy, other.ClosestPoint(transform.position));
                enemyList.Add(enemy);
            }
        }
    }
}
