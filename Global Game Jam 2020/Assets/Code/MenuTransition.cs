using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{

    public GameObject screw;
    public GameObject nail;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (screw.GetComponent<Screw>().progress == 1f && nail.GetComponent<Nail>().nailInStage == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

       



    }


}
