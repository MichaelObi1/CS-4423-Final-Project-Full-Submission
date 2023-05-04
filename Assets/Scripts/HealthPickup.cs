using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 10;
    public Vector3 rotationSpeed = new Vector3(0, 180, 0);
    AudioSource itemPickup;

    private void Awake() 
    {
        itemPickup = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healAmount);

            if (wasHealed)
            {
                if (itemPickup)
                {
                    AudioSource.PlayClipAtPoint(itemPickup.clip, gameObject.transform.position, itemPickup.volume);
                    Destroy(gameObject);
                }
                
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += rotationSpeed * Time.deltaTime;
    }
}
