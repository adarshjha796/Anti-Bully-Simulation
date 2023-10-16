using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject Player;
    private PlayerFightController playerFightController;
    private SpriteRenderer spriteRenderer;

    [Header("float")]
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;

    [Header("bool")]
    private bool IsContactWithEnemy;

    [Header("Animator")]
    private Animator Animator;

    [Header("int")]
    private readonly int Walk = Animator.StringToHash("Walk");
    private readonly int Punch = Animator.StringToHash("Punch");
    private readonly int Kick = Animator.StringToHash("Kick");
    private readonly int Die = Animator.StringToHash("Die");
    private int state;

    [Header("RawImage")]
    [SerializeField] private RawImage damageSplatter;


    [SerializeField] private Color splatterAlpha_1;
    [SerializeField] private Color splatterAlpha_2;


    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        damageSplatter = GameObject.FindWithTag("DamageSplatter").GetComponent<RawImage>();
        playerFightController = Player.GetComponent<PlayerFightController>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if((transform.position.x - playerFightController.transform.position.x) == 1.35f || 
            (transform.position.x - playerFightController.transform.position.x) == -1.55f)
        {
            state = Kick;

            if(state == Kick || state == Punch)
            {
                if(IsContactWithEnemy)
                {              
                    damageSplatter.color = Color.Lerp(splatterAlpha_1, splatterAlpha_2, Mathf.PingPong(Time.time * 5.0f,1));
                    Scene3Manager.Instance.PlayerHealthBar.fillAmount -= Damage * Time.deltaTime;
                }
            }
        }

        else
        {
            state = Walk;
        }

        if (Scene3Manager.Instance.EnemyHealthBar.fillAmount == 0.0f)
        {
            print("AAA");
            state = Die;
            StartCoroutine(Dying());
        }

        Animator.CrossFade(state, 0.0f);

        if (playerFightController.flip)
        {
            playerFightController.PivotLeft.gameObject.SetActive(true);
            playerFightController.PivotRight.gameObject.SetActive(false);

            transform.position =  Vector2.MoveTowards(transform.position, 
                                                      playerFightController.PivotLeft.position + new Vector3(-0.75f,0),
                                                      Time.deltaTime * Speed);

            spriteRenderer.flipX = true;


        }

        else
        {
            playerFightController.PivotLeft.gameObject.SetActive(false);
            playerFightController.PivotRight.gameObject.SetActive(true);

            transform.position = Vector2.MoveTowards(transform.position,
                                                      playerFightController.PivotRight.position + new Vector3(0.75f, 0), 
                                                      Time.deltaTime * Speed);

            spriteRenderer.flipX = false;
        }
    }


    IEnumerator Dying()
    {
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<EnemySpawnManager>().Died = true;
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsContactWithEnemy = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsContactWithEnemy = false;
        }
    }
}