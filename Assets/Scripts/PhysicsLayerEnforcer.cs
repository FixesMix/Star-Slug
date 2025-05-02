using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsLayerEnforcer : MonoBehaviour
{
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Player-Bullet"),
            true
        );
    }
}
