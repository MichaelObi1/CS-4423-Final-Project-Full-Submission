using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class PlayerEvents
{
    //player was hurt and damage amount.
    public static UnityAction<GameObject, int> playerTookDamage;

    //player was healed and healh amount.
    public static UnityAction<GameObject, int> playerRestoredHealth;
}

