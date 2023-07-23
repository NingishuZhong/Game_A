using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTail : MonoBehaviour
{
    private SpriteRenderer sr;
    private SFXPlayer sfxPlayer;
    public AudioClip clip;

    private void Awake()
    {
        sfxPlayer = GameObject.FindGameObjectWithTag("SFXPlayer").GetComponent<SFXPlayer>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            sfxPlayer.PlaySFX(clip);
            other.gameObject.GetComponent<AnimalsController>().Damaged(1);
        }
    }

    public void Fade()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        float a = 1;
        while (a > 0)
        {
            a -= 0.2f / TimeController.speedScale;
            sr.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
