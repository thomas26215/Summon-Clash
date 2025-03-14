using UnityEngine;

public class Hero : Character
{
    public override void Attack()
    {
        Debug.Log("Attaque avec sa hache !");
    }

    public override void Die()
    {
        Debug.Log("Je suis mort, mais je reviendrai plus fort !");
        Destroy(gameObject);
    }
}

