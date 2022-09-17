
using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    private int count = 0;

    void Update()
    {
        if (Input.GetKeyDown("f9")) StartCoroutine(ScreenshotEncode());

        if (Input.GetKeyDown("f10"))
        {        
            string screenshotFilename;
            do
            {
                count++;
                screenshotFilename = Application.dataPath + "/../screenshot-" + count + ".png";
            }
			while (System.IO.File.Exists(screenshotFilename));
           
            ScreenCapture.CaptureScreenshot(screenshotFilename);
        }
    }

    IEnumerator ScreenshotEncode()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        yield return 0;
        byte[] bytes = texture.EncodeToPNG();
		
        string screenshotFilename;
        do
        {
            count++;
            screenshotFilename = Application.dataPath + "/../screenshot-" + count + ".png";
        }
		while (System.IO.File.Exists(screenshotFilename));
		
        File.WriteAllBytes(screenshotFilename, bytes);
        count++;

        DestroyObject( texture );
    }
}