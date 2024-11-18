using System.Collections;
using TMPro;
using UnityEngine;


public class FloatingScore : MonoBehaviour
{
    public TMP_Text txScore;
    private float speed = 2f;
    private float duration = 1f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
