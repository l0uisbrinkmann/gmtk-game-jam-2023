using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    KnifeController knifeController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        knifeController = FindObjectOfType<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += knifeController.speed * Time.deltaTime * direction;
    }
}
