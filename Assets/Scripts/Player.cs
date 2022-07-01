using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
   
    public Sprite[] sprites;
    public float gravity = -9.8f;
    public float strength = 5f;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f,0.15f);
    }

    private void OnEnable()
    {
        Vector3 current = transform.position;
        current.y = 0f;
        transform.position = current;
        direction = Vector3.zero;
    }

    private void AnimateSprite()
    { 
        spriteIndex++;

        if(spriteIndex >= sprites.Length) 
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Obstacle") {
            FindObjectOfType<GameManager>().GameOver();
        }else if(other.gameObject.tag == "Scoring") {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            direction = Vector3.up * strength;
        }

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){
                 direction = Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime;

        transform.position += direction * Time.deltaTime;
    }
}
