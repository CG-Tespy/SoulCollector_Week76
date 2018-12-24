using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to just shoot a projectile forwards.
/// </summary>
public class PlayerAttackPrototype : MonoBehaviour2D
{
    [SerializeField] StunProjectilePrototype bulletPrefab;
    [SerializeField] float fireRate =                           1;
    PlayerMovementPrototype movement;

    float fireTimer =                                           0;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        movement =                                              GetComponent<PlayerMovementPrototype>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        TimerCountdown();
        HandleShooting();
    }

    void HandleShooting()
    {
        if (fireTimer <= 0 && Input.GetButton("Fire1"))
        {
            // Instantiate the projectile, have it move where the player is facing.
            StunProjectilePrototype bullet =                    Instantiate<StunProjectilePrototype>(bulletPrefab, 
                                                                                                    transform.position, Quaternion.identity);
            bullet.velocity =                                   movement.directionFacing.ToVector2();

            // Set the delay for the next shot
            fireTimer =                                         1 / fireRate;
        }
    }

    void TimerCountdown()
    {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
        
    }
}
