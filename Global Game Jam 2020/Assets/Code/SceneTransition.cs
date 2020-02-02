using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
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

        if (screw.GetComponent<Screw>().progress == 1f)
        {
            SceneManager.LoadScene(0);
        }

        if (nail.GetComponent<Nail>().nailInStage == 2 )
        {
            Application.Quit();
        }



    }


}
