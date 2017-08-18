using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    SpriteRenderer myRenderer;
    Shader shaderGUItext;
    Shader shaderSpritesDefault;

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool movingRight = false;
     public bool frozen = false;

    //being lazy is valid :^)
    public void Log(string str)
    {
        Debug.Log(str);
    }
    
    public void Flip() 
	{
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //flip by scaling -1
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

    public void CheckFlip() {
        Rigidbody2D rb2d;
        if ((rb2d = GetComponent<Rigidbody2D>()) != null) {
            if (!facingRight && rb2d.velocity.x > 0)
            {
                Flip();
            }
            else if (facingRight && rb2d.velocity.x < 0)
            {
                Flip();
            }
        }
    }
}
