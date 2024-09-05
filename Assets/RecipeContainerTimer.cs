using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeContainerTimer : MonoBehaviour, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressChanged;
    }


    [SerializeField] private float maxOrderTimer;

    private float orderTimer;
    private float currentTimerLeft;

    void Start()
    {
        
        if(currentTimerLeft <= 0)
        {
            orderTimer = maxOrderTimer;
        }
        else
        {
            orderTimer = currentTimerLeft;
        }

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressChanged = orderTimer / maxOrderTimer
        });
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            orderTimer -= Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressChanged = orderTimer / maxOrderTimer
            });
            if (orderTimer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void SetCurrentTimer(float currentTimer)
    {
        this.currentTimerLeft = currentTimer;
    }
}
