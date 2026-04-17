using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class Movimiento : MonoBehaviour
{
   public float velocidad = 1.0f;
	public float fuerzaSalto = 5.0f;
    private Rigidbody2D rb;
	private bool enSuelo = true;
    
	public int monedas; 
	private SpriteRenderer spriteRenderer;
	
	public int vidas = 3;
	
	public Image heart1;
	public Image heart2;
	public Image heart3;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
	    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal");
	    transform.Translate(movimientoX * velocidad * Time.deltaTime,0f,0f);
	    
	    if(movimientoX > 0)
	    {
	    	spriteRenderer.flipX = false;
	    }
	    if(movimientoX < 0)
	    {
	    	spriteRenderer.flipX = true;
	    }

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
	        enSuelo = false;
	        Debug.Log("Estas saltando");
        }
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        //Debug.Log("Hemos entrado");

        

        if(other.name == "Moneda")
        {
	        //Debug.Log("Conseguiste una Mondea");
	        monedas = monedas+1;
	        Debug.Log("Tienes: " + monedas);
	        Destroy(other.gameObject);
        }
	    if (other.tag == ("Flame") && vidas > 0)
	    {
	    	vidas--;
	    	Debug.Log("Te has quemado");
	    	if (vidas == 2)
	    	{
	    		heart3.enabled = false;
	    	}
	    	else if (vidas == 1)
	    	{
	    		heart2.enabled = false; 
	    	}
	    	else if (vidas == 0)
	    	{
	    		heart1.enabled = false;
	    		Debug.Log("Game Over");
	    	}
	    }
	    
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Estas en el piso");
            enSuelo = true;
        }
    }
	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			Debug.Log("Estas fuera del piso");
			enSuelo = false;
		}
	}
}