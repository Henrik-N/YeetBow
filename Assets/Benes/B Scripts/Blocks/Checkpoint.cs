using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameLoop.Instance.CurrentCheckpoint = this;
        //Debug.Log("I am current checkpoint" + this.name);
        if (clip != null)
            SoundManager.Instance.PlaySoundAtLocation(transform.position, clip);
    }
    
    public void PlacePlayerHere()
    {
        GameManager.Instance.player.transform.position = transform.position;
    }
    
}