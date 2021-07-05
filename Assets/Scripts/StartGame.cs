using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

     [SerializeField]
    private GameObject thing;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float animationDuration;

    void Start() 
    {
      
       anim = thing.GetComponent<Animator>();
    }

    public void Load()
    {
         anim.SetBool("isStart", false);
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        //Lahat ng animations mo ilagay mo dito
        anim.SetBool("isStart", true);

        //Ilagay mo dun sa animation duration yung haba nung animation na gagawin mo para saka lang magloload kapag tapos na yung animation
        yield return new WaitForSeconds(animationDuration);
        
        //Comment out mo to pag meron ka nang duration
        //yield return null;
        
        SceneLoader.Instance.LoadGame();
    }
}