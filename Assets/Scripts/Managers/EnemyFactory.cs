using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFactory { 
    public static GameObject createEnemy(string enemyType) {
        switch(enemyType) {
            case "Bat":
                return Resources.Load<GameObject>("Enemy");
                default: return null;
        }
    }
}
