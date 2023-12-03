using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxDistanceToGround = 0.1f;

    public bool IsGameOver = false;

    public static PlayerController Instance => _instance;
    private static PlayerController _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (!IsGameOver)
        {
            HandleTouchInput();
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Отримання відхилення пальця
                float horizontalDelta = touch.deltaPosition.x;

                // Перевірка наявності стін справа та зліва
                if (CanMove(horizontalDelta))
                {
                    // Пересування персонажа вліво або вправо
                    transform.Translate(Vector3.right * horizontalDelta * moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    bool CanMove(float horizontalDelta)
    {
        // Визначення напрямку руху
        Vector3 direction = horizontalDelta > 0 ? Vector3.right : Vector3.left;

        // Визначення позиції перед персонажем
        Vector3 rayOrigin = transform.position + Vector3.up * maxDistanceToGround;

        // Визначення довжини променя (ви можете змінити це значення відповідно до ваших потреб)
        float rayLength = Mathf.Abs(horizontalDelta) * moveSpeed * Time.deltaTime;

        // Визначення RaycastHit для збереження інформації про зіткнення
        RaycastHit hit;

        // Перевірка колізій зі стіною або ворогом
        if (Physics.Raycast(rayOrigin, direction, out hit, rayLength))
        {
            // Перевірка, чи об'єкт має тег "Wall"
            if (hit.collider.CompareTag("Wall"))
            {
                // Якщо об'єкт з тегом "Wall", не дозволяємо рух
                return false;
            }
        }

        return true;
    }

    public void GameOver()
    {
        UIManager.Instance.LooseLGame();
        IsGameOver = true;
    }

}