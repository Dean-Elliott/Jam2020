using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioCorrector : MonoBehaviour
{
    // set the desired aspect ratio
    public float targetAspectRatio;

    //Initialize the main camera
    private Camera cameraComponent;

    private Vector2 originalScreenDimensions;
    private bool initialResolutionChange = false;

    private void Start()
    {
        cameraComponent = gameObject.GetComponent<Camera>();
        originalScreenDimensions = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        if (originalScreenDimensions != new Vector2(Screen.width, Screen.height) || initialResolutionChange == false)
        {
            initialResolutionChange = true;
            originalScreenDimensions = new Vector2(Screen.width, Screen.height);

            //Determine the game window's current aspect ratio
            float windowAspectRatio = (float)Screen.width / (float)Screen.height;

            //Current viewport height should be scaled by this amount
            float scaleHeight = windowAspectRatio / targetAspectRatio;

            //If scaled height is less than current height, add letterbox
            if (scaleHeight < 1.0f)
            {
                Rect cameraLetterRectangle = cameraComponent.rect;

                cameraLetterRectangle.width = 1.0f;
                cameraLetterRectangle.height = scaleHeight;
                cameraLetterRectangle.x = 0;
                cameraLetterRectangle.y = (1.0f - scaleHeight) / 2.0f;

                cameraComponent.rect = cameraLetterRectangle;
            }
            //Else, add a pillarbox
            else
            {
                float scaleWidth = 1.0f / scaleHeight;

                Rect cameraPillarRectangle = cameraComponent.rect;

                cameraPillarRectangle.width = scaleWidth;
                cameraPillarRectangle.height = 1.0f;
                cameraPillarRectangle.x = (1.0f - scaleWidth) / 2.0f;
                cameraPillarRectangle.y = 0;

                cameraComponent.rect = cameraPillarRectangle;
            }
        }
    }
}
