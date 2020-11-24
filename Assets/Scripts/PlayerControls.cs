using Photon.Pun;
using UnityEngine;

public class PlayerControls : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    private SpriteRenderer spriteRenderer;
    private Vector2 Direction;
    private bool IsRun = false;
    private Animator anim;
    private Transform MainCamera;
    private Transform Player;
    private Vector3 TargetPosition;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Direction);
            stream.SendNext(IsRun);
            stream.SendNext(transform.position);
        }
        else
        {
            Direction = (Vector2)stream.ReceiveNext();
            IsRun = (bool)stream.ReceiveNext();
            TargetPosition = (Vector3)stream.ReceiveNext();
        }
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        MainCamera = GameObject.Find("Main Camera").transform;
        Player = transform;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            transform.Translate(Input.GetAxis("Horizontal") * 0.05f, Input.GetAxis("Vertical") * 0.05f, 0);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                IsRun = true;
            else
                IsRun = false;

            if (Input.GetKey(KeyCode.A)) { Direction = Vector2.left; }
            if (Input.GetKey(KeyCode.D)) { Direction = Vector2.right; }

            MainCamera.position = Vector3.Lerp(MainCamera.position, new Vector3(returnPPX(), returnPPy(), -10), Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, 10 * Time.deltaTime);
        }

        if (IsRun) anim.Play("Run");
        else anim.Play("Idle");

        if (Direction == Vector2.left) spriteRenderer.flipX = true;
        if (Direction == Vector2.right) spriteRenderer.flipX = false;
    }

    private float returnPPX()
    {
        if (Player.position.x > -5.33f && Player.position.x < 5.33f)
        {
            return Player.position.x;
        }
        else
        {
            return MainCamera.position.x;
        }
    }

    private float returnPPy()
    {
        if (Player.position.y > -0.42f && Player.position.y < 4.79f)
        {
            return Player.position.y;
        }
        else
        {
            return MainCamera.position.y;
        }
    }

}