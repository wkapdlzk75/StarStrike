using UnityEngine;

public class Player : Unit
{
    // 각각 상하좌우 경계선을 터치했는지 여부
    public bool isTouchTop;     // 위
    public bool isTouchBottom;  // 아래
    public bool isTouchRight;   // 우
    public bool isTouchLeft;    // 좌

    public int maxLife;     // 최대 플레이어 목숨
    public int life;        // 현재 플레이어 목숨
    public int maxPower;    // 최대 파워 (최대 총알 레벨)
    public int power;       // 현재 파워 (현재 총알 레벨)
    public int maxBoom;     // 최대 폭탄 갯수
    public int boom;        // 현재 폭탄 갯수

    float lastFireTime;    // 마지막 총알 발사 시각
    bool isDie;             // 플레이어 죽음 여부
    bool isBoomTime;        // 폭탄 터짐 여부

    public GameObject boomEffect; // 폭탄

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        lastFireTime = Time.time;  // 시간 초기화
        isDie = false;
    }

    void Update()
    {
        Move();
        Fire();
        Boom();
    }

    // 총알 발사 *****
    public void Fire()
    {
        // bulletFiringInterval초 마다 총알 생성
        switch (power)
        {
            case 1:
                if (Time.time - lastFireTime > bulletFiringInterval)
                {
                    Instantiate(bulletPrefabA, transform.position + new Vector3(0, 0.7f, 0), transform.rotation, bulletsParent.transform);
                    lastFireTime = Time.time;
                }
                break;
            case 2:
                if (Time.time - lastFireTime > bulletFiringInterval)
                {
                    Instantiate(bulletPrefabA, transform.position + new Vector3(0.1f, 0.7f, 0), transform.rotation, bulletsParent.transform);
                    Instantiate(bulletPrefabA, transform.position + new Vector3(-0.1f, 0.7f, 0), transform.rotation, bulletsParent.transform);
                    lastFireTime = Time.time;
                }
                break;
            case 3:
                if (Time.time - lastFireTime > bulletFiringInterval)
                {
                    Instantiate(bulletPrefabA, transform.position + new Vector3(0.3f, 0.7f, 0), transform.rotation, bulletsParent.transform);
                    Instantiate(bulletPrefabB, transform.position + new Vector3(0, 0.7f, 0), transform.rotation, bulletsParent.transform);
                    Instantiate(bulletPrefabA, transform.position + new Vector3(-0.3f, 0.7f, 0), transform.rotation, bulletsParent.transform);
                    lastFireTime = Time.time;
                }
                break;

        }
    }

    // 폭탄 활성화
    void Boom()
    {
        if (!Input.GetButton("Fire2"))  return;
        if (isBoomTime)                 return;
        if (boom == 0)                  return;

        boom--;
        UIManagerGameScene.instance.UpdateBoom(boom, false);
        boomEffect.SetActive(true);
        isBoomTime = true;
        Invoke("OffBoomEffect", 3);

        // 몹 제거
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        for (int i = 0; i < mobs.Length; i++)
        {
            Mob mob = mobs[i].GetComponent<Mob>();
            mob.OnHit(1000);
        }

        // 몹 총알 제거
        GameObject[] mobBullets = GameObject.FindGameObjectsWithTag("MobBullet");
        for (int i = 0; i < mobBullets.Length; i++)
            Destroy(mobBullets[i]);
    }

    // *****
    public void Move()
    {
        float moveHori = Input.GetAxisRaw("Horizontal");    // 좌우
        float moveVert = Input.GetAxisRaw("Vertical");      // 상하

        moveHori = BorderHorizontal(moveHori);  // 좌우 경계선 처리
        moveVert = BorderVertical(moveVert);    // 상하 경계선 처리

        Vector3 curPos = transform.position;                // 현재위치 가져옴
        Vector3 nextPos = new Vector3(moveHori, moveVert, 0) * speed * Time.deltaTime;  // 이동한 위치값
        transform.position = curPos + nextPos;              // 이동 반영

        // 좌우 이동 버튼을 눌렀을 때와 뗐을 때
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            //에니메이터의 Input값을 moveHori값으로 설정한다
            animator.SetInteger("Input", (int)moveHori);
    }

    // 플레이어 리스폰
    public void RespawnPlayer()
    {
        isDie = false;
        HP = MAX_HP;
        transform.position = new Vector2(0, -4);
        gameObject.SetActive(true);
    }

    // 플레이어 죽음
    public void Die()
    {
        life--;
        UIManagerGameScene.instance.UpdateLife(life, false);

        if (life <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.EndGame();
        }

        gameObject.SetActive(false);
        Invoke("RespawnPlayer", 3); // 3초 뒤 부활
    }

    // 경계선 처리
    public float BorderHorizontal(float _moveHori)
    {
        // 유저가 좌우 경계선에 닿을 경우 (밖으로 나갈 경우)
        if ((isTouchRight && _moveHori == 1) || (isTouchLeft && _moveHori == -1))
            return 0;
        return _moveHori;
    }
    public float BorderVertical(float _moveVert)
    {
        // 유저가 상하 경계선에 닿을 경우 (밖으로 나갈 경우)
        if ((isTouchTop && _moveVert == 1) || (isTouchBottom && _moveVert == -1))
            return 0;
        return _moveVert;
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        // 경계선에 닿을 경우 (밖으로 나갈 경우)
        if (_collision.transform.CompareTag("PlayerBorder"))
            switch (_collision.gameObject.name)
            {
                case "Top": isTouchTop = true; break;
                case "Bottom": isTouchBottom = true; break;
                case "Right": isTouchRight = true; break;
                case "Left": isTouchLeft = true; break;
            }

        // 몹의 총알에 맞았을 경우
        if (_collision.transform.CompareTag("MobBullet"))
        {
            Destroy(_collision.gameObject);

            if (isDie)  return;
            
            if (HP <= 0 && !isDie)
            {
                isDie = true;
                Die();
            }
        }

        // 몹과 충돌 했을 경우
        if (_collision.transform.CompareTag("Mob"))
        {
            if (isDie)  return;
            isDie = true;
            Die();
        }

        // 아이템
        if (_collision.transform.CompareTag("Item"))
        {
            Item item = _collision.transform.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    GameManager.instance.AddScore(100);
                    break;
                case "Power":
                    if (power == maxPower)
                        GameManager.instance.AddScore(50);
                    else
                        power++;
                    break;
                case "Boom":
                    if (boom == maxBoom)
                        GameManager.instance.AddScore(50);
                    else
                        boom++;
                        UIManagerGameScene.instance.UpdateBoom(boom, true);
                    break;

            }
            Destroy(_collision.gameObject);

        }

    }

    // 폭탄 효과 끄기
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        // 경계선에 안 닿았을 경우 (안에 있을 경우)
        if (_collision.transform.CompareTag("PlayerBorder"))
            switch (_collision.gameObject.name)
            {
                case "Top": isTouchTop = false; break;
                case "Bottom": isTouchBottom = false; break;
                case "Right": isTouchRight = false; break;
                case "Left": isTouchLeft = false; break;
            }
    }
}
