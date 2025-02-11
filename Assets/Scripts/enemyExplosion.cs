using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyExplosion : MonoBehaviour
{
    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
