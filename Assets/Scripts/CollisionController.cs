using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    private GameObject aboutSprite;
    private GameObject Boy;

    private Scene4Manager scene4Manager;

    [Header("Transform")]
    private Transform enlargerPlayer;
    private Vector2 reducerPlayer;

    private bool enlarge;

    private void Awake()
    {
        aboutSprite = GameObject.FindWithTag("About");

        Boy = GameObject.FindWithTag("Boy");

        scene4Manager = FindObjectOfType<Scene4Manager>();  
    }


    private void Update()
    {
        if(!scene4Manager.bullyAudioSource.isPlaying && enlarge)
        {
            transform.GetComponent<PlayerController>().enabled = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bow"))
        {
            transform.GetComponent<Animator>().CrossFade("Bow", 0.0f);
            transform.GetComponent<PlayerController>().enabled = false;
            Boy.GetComponent<Animator>().SetTrigger("PlayerEntered");
        }

        if (collision.CompareTag("About"))
        {
            aboutSprite.transform.DOScale(new Vector2(1.0f, 1.0f), 1);
        }


        if (collision.CompareTag("Bully"))
        {
            transform.GetComponent<Animator>().Play("Idle");
            transform.GetComponent<PlayerController>().enabled = false;

            scene4Manager.count++;

            scene4Manager.bullyAudioSource.clip = scene4Manager.bullySounds[scene4Manager.count];
            scene4Manager.bullyAudioSource.Play();

            scene4Manager.belt[scene4Manager.count].transform.DOLocalMoveY(1.25f, 1).OnComplete(() =>
            {              
                scene4Manager.belt[scene4Manager.count].transform.DOLocalMoveY(-4.00f, 2.5f).OnComplete(() =>
                {
                    scene4Manager.beltColliders[scene4Manager.count].transform.GetChild(0).gameObject.SetActive(false);
                    scene4Manager.beltColliders[scene4Manager.count].transform.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                    enlarge = true;
                });
            });
        }


        if(collision.CompareTag("Stop"))
        {
            scene4Manager.bully[scene4Manager.count].transform.DOScale(new Vector3(0.4f, 0.4f), 1);
            scene4Manager.bully[scene4Manager.count].transform.DOLocalMoveY(-1, 1);

            reducerPlayer = transform.localScale + new Vector3(0.05f, 0.05f);

            transform.DOScale(new Vector3(1.3f, 1.3f), 0.5f).OnComplete(() =>
            {               
                transform.DOScale(reducerPlayer, 0.5f);
            }); 

            scene4Manager.beltColliders[scene4Manager.count].SetActive(false);
        }


        if(collision.CompareTag("belt"))
        {
            scene4Manager.belt[scene4Manager.count].gameObject.SetActive(false);
        }


        if(collision.CompareTag("SceneChanger"))
        {
            SceneManager.LoadScene(4);
        }
    }
}