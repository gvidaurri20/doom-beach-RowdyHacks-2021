using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HUDdisabler;

    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEndMenu()
    {
      //  isDead = true;
      gameObject.SetActive(true);
      HUDdisabler.SetActive(false);

    }
    
}
