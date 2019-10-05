using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] BoxCollider2D HBoxUp;
    [SerializeField] BoxCollider2D HBoxDown;
    [SerializeField] BoxCollider2D HBoxLeft;
    [SerializeField] BoxCollider2D HBoxRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        //print(Vector2.SignedAngle(transform.position, Input.mousePosition));
        if (Input.GetMouseButtonDown(0))
        {
            //print(transform.position - Input.mousePosition);
           // print(Vector2.Angle(transform.right, Input.mousePosition - transform.position));
            StartCoroutine(hitBoxActivate(HBoxUp));
        }
    }

    IEnumerator hitBoxActivate(BoxCollider2D hitBox)
    {
        hitBox.enabled = true;
        yield return new WaitForSecondsRealtime(.25f);
        hitBox.enabled = false;
    }

    void OnGUI()
    {
        //Output the angle found above
        GUI.Label(new Rect(25, 25, 200, 40), "Angle Between Objects" + Vector2.SignedAngle(transform.right, Input.mousePosition));
    }
}
