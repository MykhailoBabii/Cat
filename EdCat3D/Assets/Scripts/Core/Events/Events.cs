using Game.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Events
{
    //Here will be added some events
    public class GameStartEvent { }

    public class StorageEvent { }

    public class EnemyKillEvent { public EnemyBehaviour Enemy; }

    public class OnPlayerDeathEvent { }
}