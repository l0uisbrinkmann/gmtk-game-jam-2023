using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] float destroyAfterSeconds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 direction)
    {
        this.direction = direction;

        float directionX = direction.x;
        float directionY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(directionX == 0 && directionY > 0) //up
        {
            rotation.z = 90f;
        }if(directionX == 0 && directionY < 0) //down
        {
            rotation.z = -90f;
        }if(directionX < 0 && directionY == 0) //left
        {
            rotation.z = 180f;
        }
        else if(directionX > 0 && directionY > 0) //up right
        {
            rotation.z = 45f;
        }else if(directionX > 0 && directionY < 0) //down right
        {
            rotation.z = -45f;
        }else if(directionX < 0 && directionY > 0) //up left
        {
            rotation.z = 135f;
        }else if(directionX < 0 && directionY < 0) //down left
        {
            rotation.z = -135f;
        }
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
