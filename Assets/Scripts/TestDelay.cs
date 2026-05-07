using System.Collections;
using UnityEngine;

public class TestDelay : MonoBehaviour
{
    // Задержки   //асихронность 

    //Time.time - время в секундах с запуска игры. 

    /*float delay = 2f;
    float startTime;
    bool isTriggered;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!isTriggered & Time.time >= startTime + delay)
        {
            //Задержка
        }
    }*/

    //Invoke 
    /*
    void Start()
    {
        Invoke("DelayedAction", 2f);
        InvokeRepeating("DelayedAction", 2f, 3f); //Вызываю метод DelayedAction, через 2 секунды и повторяю его
        //каждые 3 секунды
    }

    void DelayedAction()
    {
        //какое то действие выполнено
    }*/

    //4 Корутины - асинхронная операция 
    /*int playerHP = 100;
    void Start()
    {
        StartCoroutine(DelayedAction());
    }
    
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(2f);  //Зависима Time.timeScale
        //yield return new WaitForSecondsRealtime(2f);   //независима Time.timeScale
        //yield return new WaitUntil(() => playerHP <= 0);
        //yield return new WaitWhile(() => isPaused);
        Debug.Log("Любые действия");

        //какое то действие выполнено
    }

    //StopCoroutine("DelayedAction");
    //StopAllCoroutine();

    //CancelInvoke("DelayedAction");
    */
    // Time.deltaTime;  - cчетчики
    /*
    float repeatRate = 2f;
    float timer = 0f;

    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer >= repeatRate)
        {
            timer = 0f;
            DelayedAction();
        } 
    }

    void DelayedAction()
    {
        //какое то действие выполнено
    }

    //Task - асихронное программирование (async, await)
    */





}
