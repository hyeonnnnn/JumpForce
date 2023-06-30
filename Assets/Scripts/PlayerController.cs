using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB; // �÷��̾� ������ٵ�
    private Animator playerAnim; // �÷��̾� �ִϸ�����
    private AudioSource playerAudio; // �÷��̾� ȿ����
    public ParticleSystem explosionParticle; // ���� ��ƼŬ
    public ParticleSystem dirtParticle; // ����� ��ƼŬ
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10; // ���� ����
    public float gravityModifier = 1; // �߷� ����
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); // ������ٵ� ������Ʈ�� �����Ϸ��� GetComponent ���
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // ����
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ������ٵ� ������Ʈ�� ����ϰ� �����Ƿ� AddForce() �޼��� ����
                                                                          // Impulse: �� ��� ���� (��� ����)
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");  // SetTrigger �޼��尡 ���� Ʈ���� Ȱ��ȭ
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // ��
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } 

        // ��ֹ�
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAnim.SetBool("Death_b", true); // �״� ��� Ȱ��ȭ
            playerAnim.SetInteger("DeathType_int", 1); // ��� ���� ����
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
