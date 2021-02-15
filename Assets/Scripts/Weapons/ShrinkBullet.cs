using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShrinkBullet : BulletComponentBase
{

    public static Component ShrinkBehaviorArchetype;

    public override void BulletDie(Collision killedBy)
    {
        if (killedBy.gameObject.TryGetComponent(out EnemyVision comp))
        {
            if(!killedBy.gameObject.GetComponent(ShrinkBehaviorArchetype.GetType()))
                GameObjectHelpers.CopyComponent(ShrinkBehaviorArchetype, killedBy.gameObject);
        }
    }
}
