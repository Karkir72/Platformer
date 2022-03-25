using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ground>())
        {
            _speed = -_speed;
        }
    }
}
