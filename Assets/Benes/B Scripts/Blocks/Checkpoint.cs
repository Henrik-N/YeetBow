using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameLoop.Instance.CurrentCheckpoint = this;
        //Debug.Log("I am current checkpoint" + this.name);
    }
    
    public void PlacePlayerHere()
    {
        GameManager.Instance.player.transform.position = transform.position;
    }
    
}