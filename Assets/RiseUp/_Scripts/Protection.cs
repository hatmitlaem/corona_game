using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour
{
    public Rigidbody2D rb;
	
	public AudioClip gemClip;
	public AudioClip virusClip;
	
	private AudioSource audioSource;
	
	SpriteRenderer m_SpriteRenderer;
	
	bool b_Collision;
	
	public void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}
    public void RemoveVelocity()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            RemoveVelocity();
			
			audioSource.clip = virusClip;
			audioSource.Play();
			
		m_SpriteRenderer.color = Color.blue;
        }
    }
	private void OnCollisionExit2D(Collision2D collision)
	{
		if(collision.collider.tag == "Obstacle")
		{
			m_SpriteRenderer.color = Color.white;
		}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ruby")
        {
			audioSource.clip = gemClip;
			audioSource.Play();
			
            CurrencyController.CreditBalance(1);
            Destroy(col.gameObject);
        }
    }
}
