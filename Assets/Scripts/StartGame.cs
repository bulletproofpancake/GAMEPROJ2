using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Load()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        //Lahat ng animations mo ilagay mo dito
        /*
            //animation code 
        */ 
        
        //Ilagay mo dun sa animation duration yung haba nung animation na gagawin mo para saka lang magloload kapag tapos na yung animation
        //yield return new WaitForSeconds(animationDuration);
        
        //Comment out mo to pag meron ka nang duration
        yield return null;
        
        SceneLoader.Instance.LoadGame();
    }
}