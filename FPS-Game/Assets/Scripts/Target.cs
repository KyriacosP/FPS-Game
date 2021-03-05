using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
