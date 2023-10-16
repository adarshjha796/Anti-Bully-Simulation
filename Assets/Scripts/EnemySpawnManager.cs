using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("List")]
    [SerializeField] private List<Transform> EnemySpawnPoints;

    [Header("int")]
    private int counter = 1;

    [Header("GameObject")]
    [SerializeField] private GameObject Enemy;
    private GameObject Player;

    [Header("bool")]
    public bool Died;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }


    private void Start()
    {
        Instantiate(Enemy, EnemySpawnPoints[0].position, Quaternion.identity);
    }


    private void Update()
    {
        if (Died)
        {
            Scene3Manager.Instance.Belt.texture = Scene3Manager.Instance.BeltList[counter-1].texture;

            StartCoroutine(SpawnEnemy());
            Died = false;
        }

        else
        {
            return;
        }

        print(counter);
    }


    IEnumerator SpawnEnemy()
    {
        Scene3Manager.Instance.EnemyHealthBar.fillAmount = 1.0f;

        Scene3Manager.Instance.Belt.GetComponent<RectTransform>().DOScale(new Vector2(1.5f, 1.5f), 1);

        yield return new WaitForSeconds(0.5f);

        Scene3Manager.Instance.Belt.GetComponent<RectTransform>().DOScale(new Vector2(1f, 1f), 1);

        yield return new WaitForSeconds(0.5f);

        if (Scene3Manager.Instance.BeltList[counter - 1] == Scene3Manager.Instance.BeltList[Scene3Manager.Instance.BeltList.Count - 1])
        {
            print("AAA");
            SceneManager.LoadScene(3);
        }

        if (counter < 5)
        {
            Enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Scene3Manager.Instance.Labels[counter];
            Instantiate(Enemy, EnemySpawnPoints[counter].position, Quaternion.identity);

            counter++;
        }

        yield return null;
    }
}