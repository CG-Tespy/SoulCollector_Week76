using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectilePrototype : MonoBehaviour2D
{
    [Tooltip("This can stun objects with any of these tags.")]
    [SerializeField] List<string> _stunTags;
    [Tooltip("How long this can stun its targets for.")]
    [SerializeField] float _stunDuration = 1;
    [SerializeField] float _moveSpeed = 5;
    [Tooltip("The longest amount of time (in seconds) this bullet is allowed to exist.")]
    [SerializeField] float lifetime = 5;
    public Vector2 velocity
    {
        get { return rigidbody.velocity; }
        set { rigidbody.velocity = value.normalized * _moveSpeed; }
    }

    float deathTimer;

    protected override void Awake()
    {
        base.Awake();
        deathTimer = lifetime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        DeathCountdown();
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        IStunnable toStun =                     other.gameObject.GetComponent<IStunnable>();
        bool stunOther =                        toStun != null && _stunTags.Contains(other.gameObject.tag);

        if (stunOther)
            toStun.GetStunned(_stunDuration);

        if (!other.collider.isTrigger)
            Destroy(this.gameObject);
    }

    void DeathCountdown()
    {
        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0)
            Destroy(this.gameObject);
    }
}
