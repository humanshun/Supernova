using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum FRUITS_TYPE
{
    sui = 1,
    ka,
    kin,
    chi,
    kai,
    ten,
    dou,
    moku,
    taiyou,

}
public class Fruits : MonoBehaviour
{
    public FRUITS_TYPE fruitsType;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Fruits fruits))
        {
            if (fruits.fruitsType == fruitsType)
            {
                Destroy(gameObject);
            }
        }
    }
}
