using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimer
{
    private static List<ActionTimer> activeTimerList;
    private static GameObject initGameObject;

    private static void InitIfNeeded(){
        if (initGameObject == null){
            initGameObject = new GameObject("ActionTimer_InitGameObject");
            activeTimerList = new List<ActionTimer>();
        }
    }
    public static ActionTimer CreateInterval(Action action, float interval, string timerName = null){
        InitIfNeeded();
        List<float> intervalList = new List<float>();
        GameObject gameObject = new GameObject("Action Interval Timer", typeof(MonoBehaviourHook));
        ActionTimer actionTimer = new ActionTimer(action, interval, gameObject, timerName, intervalList);
        activeTimerList.Add(actionTimer);
        //Debug.Log(Time.time / interval);
       //actionTimer.PopulateIntervalList(interval);
        MonoBehaviourHook monoBehaviourHook = gameObject.GetComponent<MonoBehaviourHook>();
        actionTimer.AppendTimeBetween(interval);
        monoBehaviourHook.interval = interval;
        monoBehaviourHook.onIntervalUpdate = actionTimer.Update;

        for (int i = 0; i < intervalList.Count; i++)
        {
            Debug.Log(intervalList[i]);
        }

     
        return actionTimer;
    }
    public static ActionTimer Create(Action action, float timer, string timerName = null){  
        InitIfNeeded();
        GameObject gameObject = new GameObject("Action Timer", typeof(MonoBehaviourHook));
        ActionTimer actionTimer = new ActionTimer(action , timer, gameObject, timerName);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = actionTimer.Update;

        activeTimerList.Add(actionTimer);

        return actionTimer;
    }
    private static void RemoveTimer(ActionTimer actionTimer){
        InitIfNeeded();
        activeTimerList.Remove(actionTimer);
    }
    private static void StopTimer(string timerName){
        for (int i = 0; i < activeTimerList.Count; i++){
            if (activeTimerList[i].timerName == timerName){
                activeTimerList[i].MarkComplete();
                i--;
            }
        }
    }
    private class MonoBehaviourHook : MonoBehaviour {
        public Action onUpdate;
        public Action<float> onIntervalUpdate;
        public float interval;
        private void Update(){
            if (onUpdate != null)
                onUpdate();
                if(interval != 0 && onIntervalUpdate != null){
                    onIntervalUpdate(this.interval);
                }          
        }
    }
    Action OnTimerComplete;
    bool isComplete;
    string timerName;
    List<float> intervalList;
    float timer;
    private GameObject gameObject;

    private ActionTimer(Action action, float interval,  GameObject gameObject, string timerName = null, List<float> intervalList = null){
        this.OnTimerComplete = action;
        this.timer = interval;
        this.timerName = timerName;
        this.isComplete = false;
        this.gameObject = gameObject;
        if(intervalList != null)
            this.intervalList = intervalList;
    }

    void Update(){
        if (!isComplete){
            timer -= Time.deltaTime;
            if (timer < 0){
                OnTimerComplete();
                MarkComplete();
            }
        }
    }
    void Update(float interval){
        if(!isComplete && this.intervalList[this.intervalList.Count - 1] != -1f){
            for(int i = 0; i < this.intervalList.Count; i++){                
                if(Time.time == this.intervalList[i]){
                    OnTimerComplete();
                }else if(this.intervalList[i] >= Time.time){
                    MarkComplete();
                }
            }   
        }  
    } 
    private void AppendTimeBetween(float interval){
        if(!isComplete){
            float intervalTimer = interval -= Time.time;
            bool isTimerReset = false;
            if (intervalTimer > 0){
                intervalList.Add(intervalTimer);
                isTimerReset = true;
                if (Time.time == intervalList[intervalList.Count - 1]){
                    intervalTimer = -1;
                    intervalList.Add(intervalTimer);
                }                       
            }else if(intervalList[intervalList.Count - 1] == -1 && isTimerReset){
                interval = intervalTimer -= Time.time;
                intervalList.Add(interval);
            }
        }
    }
    private void MarkComplete(){
        this.isComplete = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }

}
