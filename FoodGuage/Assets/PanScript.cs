using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanScript : MonoBehaviour
{

    public int ingredientCount = 0;
    public bool onStove;
    public bool foodDone;

    public Slider fillSlider;

    public ParticleSystem particleSystem_;
    public AudioSource audioSource_;
    public AudioClip oilsSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientCount == 2 && onStove && foodDone == false)
        {
            fillSlider.gameObject.SetActive(true);
            fillSlider.value += 10 * Time.deltaTime;
        }
        else
        {
            fillSlider.gameObject.SetActive(false);

        }
        if (fillSlider.value >= 100 && foodDone == false)
        {
            foodDone = true;
        }
        
       
       

        if (ingredientCount == 2 && particleSystem_.isPlaying == false && onStove == true && foodDone == false)
        {
            particleSystem_.Play();
            audioSource_.PlayOneShot(oilsSound);
        }
        else if (particleSystem_.isPlaying == true && onStove == false ||  foodDone == true)
        {
            particleSystem_.Stop();
            audioSource_.Stop();
        }
       
    }

    public void AddIngredient()
    {
        ingredientCount++;
    }
}
