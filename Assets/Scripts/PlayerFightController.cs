using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFightController : MonoBehaviour
{
    [Header("float")]
    [SerializeField] private float Damage;
    [SerializeField] private float movementSpeed = 12f;
    private float horizontalMovement;

    [Header("RigidBody")]
    private Rigidbody2D RigidBody;

    [Header("Animator")]
    private Animator Animator;

    [Header("Bool")]
    private bool punch;
    private bool kick;
    private bool IsContactWithEnemy;
    public bool flip;

    [Header("Transform")]
    public Transform PivotRight;
    public Transform PivotLeft;

    [Header("Int")]
    private readonly int Idle = Animator.StringToHash("Idle");

    private readonly int Punch = Animator.StringToHash("Punch");

    private readonly int Kick = Animator.StringToHash("Kick");

    private readonly int Walk = Animator.StringToHash("Walk");

    private int state;
    

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();

        if (LoadPlayer.Instance.characterSprite.sprite.name == "WhiteGirl")
        {
            Animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animators/WhiteGirl");
        }

        if (LoadPlayer.Instance.characterSprite.sprite.name == "BlackGirl")
        {
            Animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animators/BlackGirl");
        }

        if (LoadPlayer.Instance.characterSprite.sprite.name == "WhiteBoy")
        {
            Animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animators/WhiteBoy");
        }

        if (LoadPlayer.Instance.characterSprite.sprite.name == "BlackBoy")
        {
            Animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animators/BlackBoy");
        }

        if (LoadPlayer.Instance.characterSprite.sprite.name == "Man")
        {
            Animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animators/Man");
        }

    }


    public void LeftUp()
    {
        horizontalMovement = 0.0f;

        Walking();
    }

    public void LeftDown()
    {
        horizontalMovement = -1.0f;

        Walking();
    }


    public void RightUp() 
    {
        horizontalMovement = 0.0f;
        Walking();
    }

    public void RightDown() 
    {
        horizontalMovement = 1.0f;
        Walking();
    }


    public void PunchUp()
    {
        punch = false;

        StateChange();
    }

    public void PunchDown()
    {
        punch = true;

        StateChange();
    }


    public void KickUp()
    {
        kick = false;

        StateChange();
    }

    public void KickDown()
    {
        kick = true;

        StateChange();
    }


    public void StateChange()
    {
        if(punch)
        {
            state = Punch;

            if(IsContactWithEnemy)
            {
                StartCoroutine(Damaging());
            }
        }

        else if (kick)
        {
            state = Kick;

            if (IsContactWithEnemy)
            {
                StartCoroutine(Damaging());
            }
        }

        else
        {
            state = Idle;
        }

        Animator.CrossFade(state, 2.0f);
    }


    IEnumerator Damaging()
    {
        yield return new WaitForSeconds(0.2f);

        Scene3Manager.Instance.EnemyHealthBar.fillAmount -= Damage;
    }


    private void Walking()
    {
        if (horizontalMovement == 1.0f)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            flip = false;
        }

        if (horizontalMovement == -1.0f)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            flip = true;
        }

        if (horizontalMovement >= 1.0f || horizontalMovement <= -1.0f)
        {
            state = Walk;
        }

        else
        {
            state = Idle;
        }

        Animator.CrossFade(state, 0.0f);
    }


    private void Update()
    {
        if(Scene3Manager.Instance.PlayerHealthBar.fillAmount == 0.0f)
        {
            SceneManager.LoadScene(1);
        }
    }


    private void FixedUpdate() => RigidBody.velocity = new(horizontalMovement * Time.fixedDeltaTime * movementSpeed,RigidBody.velocity.y);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            IsContactWithEnemy = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IsContactWithEnemy = false;
        }
    }
}