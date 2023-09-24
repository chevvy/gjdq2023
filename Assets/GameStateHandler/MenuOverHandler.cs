using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOverHandler : MonoBehaviour
{
    [SerializeField] private bool IsAnObstacleInFinishZone = false;
    [SerializeField] private float TimestampObstacleEnteredFinishZone;
    [SerializeField] private float MinimumTriggerTime = 1.25f;

    private float MaxAmbientLightIntensity;
    [SerializeField] private Image FadeOutImage;

    void Start()
    {
        Assert.IsNotNull(gameObject.GetComponent<Collider>());
        Assert.IsNotNull(FadeOutImage);

        TimestampObstacleEnteredFinishZone = Time.time;
        MaxAmbientLightIntensity = RenderSettings.ambientIntensity;
    }

    void HandleMenuSceneIsOver()
    {
        GameObject.FindGameObjectsWithTag(Tags.Player).ToList().ForEach( p => {
            DontDestroyOnLoad(p);
        });   
        
        SceneManager.LoadScene(SceneIndex.GameScene);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag(Tags.Obstacle)) return;

        IsAnObstacleInFinishZone = true;
        TimestampObstacleEnteredFinishZone = Time.time;
    }

    void OnTriggerExit(Collider collider)
    {
        if (!collider.gameObject.CompareTag(Tags.Obstacle)) return;

        IsAnObstacleInFinishZone = false;
    }

    void FadeIn(float elapsed, float ratio)
    {
        RenderSettings.ambientIntensity = Math.Min(elapsed / MinimumTriggerTime, MaxAmbientLightIntensity);

        var color = FadeOutImage.color;
        color.a = 1 - ratio;
        FadeOutImage.color = color;
    }

    void FadeOut(float ratio)
    {
        RenderSettings.ambientIntensity = Math.Max(1 - ratio, 0);

        var color = FadeOutImage.color;
        color.a = ratio;
        FadeOutImage.color = color;
    }

    void Update()
    {
        var elapsed = Time.time - TimestampObstacleEnteredFinishZone;
        var ratio = elapsed / MinimumTriggerTime;

        if (IsAnObstacleInFinishZone 
         && MinimumTriggerTime + 0.25f < elapsed)
            HandleMenuSceneIsOver();

        if (IsAnObstacleInFinishZone)
            FadeOut(ratio);
        else
            FadeIn(elapsed, ratio);
    }
}
