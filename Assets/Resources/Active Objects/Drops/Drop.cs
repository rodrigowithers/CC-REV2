using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Header("Physics")]
    public float Gravity = 1;
    private float _originalY;

    private Vector2 _momentum;

    [Header("Collectable")]
    private SpriteRenderer _sprite;

    public bool Collectable = false;
    public AnimationCurve BlinkCurve;

    private GameObject _healthParticles;
    private GameObject _manaParticles;

    public enum DropType
    {
        Health,
        Mana
    }

    public DropType Type;


    public void AddForce(Vector2 direction, float force)
    {
        _momentum += direction * force;
    }

    /// <summary>
    /// Brilha branco
    /// </summary>
    private IEnumerator CBlink()
    {
        Color color = _sprite.color;

        var time = 0.0f;

        while (true)
        {
            _sprite.color = Color.Lerp(color, Color.black, BlinkCurve.Evaluate(time));

            time += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            switch (Type)
            {
                case DropType.Health:
                    collision.GetComponent<Player>().Life += 1;
                    Instantiate(_healthParticles, Player.Instance.transform);
                    break;
                case DropType.Mana:
                    collision.GetComponent<HabilityManager>().Energy += 10;
                    Instantiate(_manaParticles, Player.Instance.transform);
                    break;
                default:
                    break;
            }

            // Toca um som
            SoundManager.Play("Pause");

            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        _manaParticles = Resources.Load<GameObject>("Active Objects/Drops/ManaGet");
        _healthParticles = Resources.Load<GameObject>("Active Objects/Drops/HealthGet");

        _sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        _originalY = transform.position.y;
        AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 1), 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Gravidade
        _momentum += Vector2.down * Gravity * Time.fixedDeltaTime;
        transform.position += _momentum.xyz(transform.position);

        // Checa se chegou ao "Chão"
        if (transform.position.y < _originalY)
        {
            _momentum = Vector2.zero;
            Gravity = 0;

            Collectable = true;
            StartCoroutine(CBlink());
        }
    }
}
