using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCollision : MonoBehaviour
{

    public bool Active => gameObject.activeSelf;

    private void Start()
    {
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {

            SetGameOver();
            //gameObject.SetActive(false);
            //GameManager.Instance.GameOver();
        }
    }

    public void SetGameOver() {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver();

    }

}
