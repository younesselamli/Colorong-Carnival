using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShotManager : MonoBehaviour
{
    private string screenshotPath;
    public GameObject UI;
    public GameObject Image;

    public Banner banner;
    public OpenAD OpenAD;


    void Start()
    {
        screenshotPath = Application.persistentDataPath + "/Screenshots/";
        if (!Directory.Exists(screenshotPath))
        {
            Directory.CreateDirectory(screenshotPath);
        }
    }

    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
        StartCoroutine(ToggleUIAfterDelay());
        UI.SetActive(false);

        // Make Image inactive
        Image.SetActive(true);
        banner.OnDestroy();
        OpenAD.ShowAd();
    }

    IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        // Create a texture to hold the screenshot
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Read pixels from the screen and apply them to the texture
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Convert the texture to PNG format
        byte[] bytes = screenshotTexture.EncodeToPNG();

        // Generate a unique filename for the screenshot
        string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

        // Combine the path and filename
        string filePath = Path.Combine(screenshotPath, filename);

        // Save the screenshot to disk
        File.WriteAllBytes(filePath, bytes);

        // Refresh the Android gallery to show the new screenshot
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass androidMediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection");
            androidMediaScanner.CallStatic("scanFile", new object[] { Application.dataPath, null, null, null });
        }

        // Log the path for your reference (you can remove this in a production build)
        Debug.Log("Screenshot saved to: " + filePath);
    }


    IEnumerator ToggleUIAfterDelay()
    {
        // Wait for 2 seconds (you can adjust the duration as needed)
        yield return new WaitForSeconds(3f);

        // Make UI active
        UI.SetActive(true);

        // Make Image inactive
        Image.SetActive(false);
        banner.Start();
       
    }
}
    //IEnumerator ToggleUIBeforDelay()
    //{
    //    // Wait for 2 seconds (you can adjust the duration as needed)
    //    yield return new WaitForSeconds(3f);

    //    // Make UI active
    //    UI.SetActive(true);

    //    // Make Image inactive
    //    Image.SetActive(true);
    //}


