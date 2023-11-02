using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // 각각 상하좌우 경계선을 터치했는지 여부
    public bool isTouchTop;     // 위
    public bool isTouchBottom;  // 아래
    public bool isTouchRight;   // 우
    public bool isTouchLeft;    // 좌

    public int maxLife;     // 최대 플레이어 목숨
    public int curLife;     // 현재 플레이어 목숨
    public int maxPower;    // 최대 파워 (최대 총알 레벨)
    public int curPower;    // 현재 파워 (현재 총알 레벨)
    public int maxBoom;     // 최대 폭탄 갯수
    //public int curBoom;     // 현재 폭탄 갯수
    public int maxFollower; // 최대 팔로워 갯수
    public int curFollower; // 현재 팔로워 갯수

    int cnt;

    float lastFireTime;     // 마지막 총알 발사 시각
    bool isDie;             // 플레이어 죽음 여부
    public bool isBoomActive;      // 폭탄 터짐 여부
    public bool isRespawnTime;     // 플레이어 부활

    public GameObject boomEffect; // 폭탄


    public AudioClip dieSound;
    public AudioClip itemSound;
    public AudioClip coinSound;
    public AudioClip boomSound;

    Animator animator;
    SpriteRenderer spriteRenderer;
    public Follower m_Follow;
    private List<Follower> followers = new List<Follower>();

    public bool fireAble;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        GameManager.Instance.player = this;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        StartCoroutine(PlayerInit());
        lastFireTime = Time.time;  // 시간 초기화
        curFollower = 0;
        curPower = 1;
        isDie = false;
        fireAble = true;
        isRespawnTime = false;
        maxHp = GameManager.Instance.GetStatus(GameManager.EPlayerStatus.maxHp);
        damage = GameManager.Instance.GetStatus(GameManager.EPlayerStatus.damage);

        curHp = maxHp;
    }

    private void OnEnable()
    {

    }



    IEnumerator PlayerInit()
    {
        yield return new WaitUntil(() => BulletManager.Instance.boomEffect != null);
        boomEffect = BulletManager.Instance.boomEffect;
    }

    void Update()
    {
        Move();
        Fire();
        Boom();
    }

    private void poolingBullet(string bulletPrefab, Vector3 vector)
    {
        ObjectManager.Instance.GetRangedObject(bulletPrefab, (poolingBullet) =>
        {
            poolingBullet.transform.position = transform.position + vector;
            poolingBullet.transform.rotation = Quaternion.Euler(0, 0, 0);
        });
    }

    void PushObject(GameObject _gameObject)
    {
        // gameObject.name.Substring();
        string myName = _gameObject.name.Replace("(Clone)", "");
        // gameObject.name.Split('(')[0];

        ObjectManager.Instance.PushRangedObject(myName, _gameObject);
    }

    // 총알 발사 *****
    public void Fire()
    {

        if (!fireAble) return;

        if (Time.time - lastFireTime > bulletFiringInterval)
        {
            switch (curPower)
            {
                case 1:
                    poolingBullet("PlayerBulletA", new Vector3(0, 0.7f, 0));
                    break;
                case 2:
                    poolingBullet("PlayerBulletA", new Vector3(0.1f, 0.7f, 0));
                    poolingBullet("PlayerBulletA", new Vector3(-0.1f, 0.7f, 0));
                    break;
                case 3:
                    poolingBullet("PlayerBulletA", new Vector3(0.3f, 0.7f, 0));
                    poolingBullet("PlayerBulletB", new Vector3(0, 0.7f, 0));
                    poolingBullet("PlayerBulletA", new Vector3(-0.3f, 0.7f, 0));
                    break;
            }


            cnt++;

            if (cnt == 3)
            {
                foreach (var item in followers)
                    item.Fire();
                cnt = 0;
            }

            lastFireTime = Time.time;
        }

    }

    // 폭탄 활성화
    void Boom()
    {
        if (!Input.GetButton("Fire2")) return;
        if (isBoomActive) return;
        if (GameManager.Instance.GetResourceAmount(GameManager.EResource.boom) == 0) return;

        GameManager.Instance.RemoveResource(GameManager.EResource.boom, 1);
        UIManagerGameScene.instance.UpdateBoom(GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
        GameManager.Instance.PlaySound(boomSound, GameManager.Instance.wholeVolume);

        boomEffect.SetActive(true);
        isBoomActive = true;
        Invoke("OffBoomEffect", 2);

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
            PushObject(mobBullets[i]);
    }

    // 폭탄 효과 끄기
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomActive = false;
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

    public void OnHit(int _damage)
    {
        if (isRespawnTime) return;
        curHp -= _damage;
    }

    IEnumerator PlayerSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 반투명 상태로 설정
        yield return new WaitForSeconds(1.5f); // 1.5초 대기
        spriteRenderer.color = new Color(1, 1, 1, 1); // 투명도를 원래대로 돌림
        isRespawnTime = false;
    }

    // 플레이어 리스폰
    public void RespawnPlayer()
    {
        isRespawnTime = true;
        isDie = false;
        curHp = maxHp;
        transform.position = new Vector2(0, -4);
        gameObject.SetActive(true);
        StartCoroutine(PlayerSprite());
    }


    // 플레이어 죽음
    public void Die()
    {
        curFollower = 0;
        curLife--;

        for (int i = 0; i < followers.Count; i++)
        {
            Destroy(followers[i].gameObject);
        }

        followers.Clear();

        UIManagerGameScene.instance.UpdateLife(curLife, false);
        ActiveExplosion("P");

        GameManager.Instance.PlaySound(dieSound, GameManager.Instance.wholeVolume * 0.35f);

        if (curLife <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            GameManager.Instance.EndGame(false);
        }
        else
        {
            gameObject.SetActive(false);
            Invoke("RespawnPlayer", 1.5f); // 1.5초 뒤 부활
        }

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
            PushObject(_collision.gameObject);

            if (isDie) return;
            if (isRespawnTime) return;

            if (curHp <= 0 && !isDie)
            {
                isDie = true;
                Die();
            }
        }

        // 몹과 충돌 했을 경우
        if (_collision.transform.CompareTag("Mob") || _collision.transform.CompareTag("MobBoss"))
        {
            if (isDie) return;
            if (isRespawnTime) return;
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
                    GameManager.Instance.inGameGold += 20;
                    GameManager.Instance.PlaySound(coinSound, GameManager.Instance.wholeVolume * 0.5f);
                    UIManagerGameScene.instance.UpdateGold();
                    GameManager.Instance.AddScore(20);
                    break;
                case "Power":
                    GameManager.Instance.PlaySound(itemSound, GameManager.Instance.wholeVolume);

                    if (curPower < maxPower)
                        curPower++;
                    else if (curFollower < maxFollower)
                        OnItemUse(curFollower);
                    else
                        GameManager.Instance.AddScore(20);
                    break;
                case "Boom":
                    GameManager.Instance.PlaySound(itemSound, GameManager.Instance.wholeVolume);

                    if (GameManager.Instance.GetResourceAmount(GameManager.EResource.boom) == maxBoom)
                    {
                        GameManager.Instance.AddScore(20);
                    }
                    else
                    {
                        GameManager.Instance.AddResource(GameManager.EResource.boom, 1);
                    }

                    UIManagerGameScene.instance.UpdateBoom(GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
                    break;
            }

            string myName = _collision.gameObject.name.Replace("(Clone)", "");
            ObjectManager.Instance.PushRangedObject(myName, _collision.gameObject);
        }

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

    void OnItemUse(int num)
    {
        if (maxFollower == curFollower)
            return;
        Follower fl = Instantiate(m_Follow);
        fl.Create(this, num);
        curFollower++;
        followers.Add(fl);
    }
}
