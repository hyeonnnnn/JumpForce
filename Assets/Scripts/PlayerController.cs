using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB; // 플레이어 리지드바디
    private Animator playerAnim; // 플레이어 애니메이터
    private AudioSource playerAudio; // 플레이어 효과음
    public ParticleSystem explosionParticle; // 폭발 파티클
    public ParticleSystem dirtParticle; // 흙먼지 파티클
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10; // 점프 세기
    public float gravityModifier = 1; // 중력 세기
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); // 리지드바디 컴포넌트에 접근하려면 GetComponent 사용
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 리지드바디 컴포넌트를 사용하고 있으므로 AddForce() 메서드 보유
                                                                          // Impulse: 힘 즉시 적용 (즉시 점프)
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");  // SetTrigger 메서드가 점프 트리거 활성화
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // 땅
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } 

        // 장애물
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAnim.SetBool("Death_b", true); // 죽는 모션 활성화
            playerAnim.SetInteger("DeathType_int", 1); // 모션 종류 선택
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
