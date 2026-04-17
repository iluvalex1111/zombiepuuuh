using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	[Header("Movimiento")]
	public float velocidadInicial = 5f;
	public float aceleracion     = 1.2f;
	public float velocidadMaxima = 20f;

	[Header("Salto")]
	public float fuerzaSalto = 10f;
	public KeyCode teclaSalto = KeyCode.Space;

	private Rigidbody2D rb;
	private float velocidadActual;
	private bool enSuelo = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		velocidadActual = velocidadInicial;
	}

	void Update()
	{
		// Acelerar con tope
		velocidadActual = Mathf.Min(
			velocidadActual + aceleracion * Time.deltaTime,
			velocidadMaxima
		);

		// Salto: detectar input aquí, aplicar fuerza aquí
		if (Input.GetKeyDown(teclaSalto) && enSuelo)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // resetear Y antes de saltar
			rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
			enSuelo = false;
		}
	}

	void FixedUpdate()
	{
		// Movimiento horizontal via Rigidbody (no transform.Translate)
		rb.linearVelocity = new Vector2(velocidadActual, rb.linearVelocity.y);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			enSuelo = true;
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			enSuelo = false;
	}
}
