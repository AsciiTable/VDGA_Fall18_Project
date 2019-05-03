using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTell_Boss : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Transform xyz;
    private BossManager manager;
    private Boss_Charge charge;
    private ShadowParker boss;

    [SerializeField] private Color nuetral;
    [SerializeField] private Color warning;
    [SerializeField] private Color charging;
    [SerializeField] private Color invulnerable;
    [SerializeField] private Color invulnerableWarning;
    [SerializeField] private Color invulnerableCharge;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        xyz = GetComponent<Transform>();
        manager = (BossManager)FindObjectOfType(typeof(BossManager));
        charge = (Boss_Charge)FindObjectOfType(typeof(Boss_Charge));
        boss = (ShadowParker)FindObjectOfType(typeof(ShadowParker));
    }

    private void Update()
    {
        if(!manager.bossStarted)
            sprite.color = nuetral;
        else if (boss.bossImmunity)
            if (charge.tellCharge || charge.charging || charge.regCharging || manager.hit)
                sprite.color = invulnerableCharge;
            else if (charge.readyCharge || charge.readyRegCharge)
                sprite.color = invulnerableWarning;
            else
                sprite.color = invulnerable;
        else if (charge.tellCharge || charge.charging || charge.regCharging)
            sprite.color = charging;
        else if (charge.readyCharge || charge.readyRegCharge)
            sprite.color = warning;
        else
            sprite.color = nuetral;

        if (charge.weakDirection == -1 && manager.bossPhase == 3)
        {
            xyz.localPosition = new Vector3(0.2f, 0, 0);
        }
        else if (charge.weakDirection == 1 && manager.bossPhase == 3)
        {
            xyz.localPosition = new Vector3(-0.2f, 0, 0);
        }

    }
}
