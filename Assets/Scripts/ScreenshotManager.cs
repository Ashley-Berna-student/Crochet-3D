using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    public string screenshotFolder;
    public string screenshotFileName = "screenshot.png";

    void Start()
    {
        screenshotFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "Downloads");

        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }
    }

    public void CaptureScreenshot()
    {
        string filePath = Path.Combine(screenshotFolder, GenerateScreenshotFileName());

        ScreenCapture.CaptureScreenshot(filePath);

        Debug.Log($"Screenshot saved to: {filePath}");
    }

    private string GenerateScreenshotFileName()
    {
        string timeStamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        return $"screenshot_{timeStamp}.png";
    }
}
