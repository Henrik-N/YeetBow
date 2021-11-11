using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageZone : MonoBehaviour
{
    private BoxCollider2D box;
    public AudioClip clip;

    [SerializeField, Tooltip("Dmg lmao"), Range(1f, 30f)]
    private int damage = 10;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        if (!box.isTrigger)
            box.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;

        other.gameObject.GetComponent<BallHealthComp>().TakeDamage(damage);
        other.gameObject.GetComponentInChildren<CameraShake>().TriggerShake();
        SoundManager.Instance.PlaySoundAtLocation(transform.position, clip);
    }
}