using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour
{
    [SerializeField] float destroyAfterSeconds;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log("Destroy gets called");
        Destroy(gameObject, destroyAfterSeconds);   
    }
}
