using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] List<GameObject> toDestroy = new List<GameObject>();
    [SerializeField] List<MovingWall> movingWalls = new List<MovingWall>();

    [SerializeField] AudioSource buttonSound, crashSound, slideSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>()) {
            
            foreach (var wall in movingWalls) wall.Move();

            buttonSound.Play();
            if (toDestroy.Count > 0) crashSound.Play();
            if (movingWalls.Count > 0) slideSound.Play();

            foreach (var g in toDestroy) Destroy(g);
            toDestroy.Clear();
            
            
            if (movingWalls.Count == 0) {
                Destroy(this);
                var col = GetComponent<SpriteRenderer>().color;
                col.a = 0.25f;
                GetComponent<SpriteRenderer>().color = col;
            }
        }
    }
}
