using System;
using System.Text;
using UnityEngine;

using TMPro;
public class DebugScript : MonoBehaviour
{
    
    public TextMeshProUGUI debugText ;
    
    StringBuilder stringBuilder = new StringBuilder();

    private float FPS;
    private float MINFS = float.MaxValue;
    private float deltaTime;
    private float updateInterval ;
    public bool isActive;
    float tempTime = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;
        updateInterval = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        tempTime +=  Time.deltaTime;

        if (tempTime > updateInterval)
        {
            updateMetrics();
            tempTime = 0;
        }
    }


    void updateMetrics()
    {
        FPS = 1.0f / Time.deltaTime;
        deltaTime = Time.deltaTime;
        
        
        if (FPS < MINFS && FPS > 0.0f) MINFS = FPS; 
        
        
        stringBuilder.Clear();
        stringBuilder.AppendLine("FPS: " + FPS);
        stringBuilder.AppendLine("MinFS: " + MINFS);
        stringBuilder.AppendLine("deltaTime: " + deltaTime * 1000);
        stringBuilder.AppendLine(" Totale Memory : " + System.GC.GetTotalMemory(false)/(1024*1024));
        debugText.text = stringBuilder.ToString();
    }
}
