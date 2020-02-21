using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Spoawn_Obi : MonoBehaviour
{
    public ObiActor template;
   

	public int basePhase = 2;
	public int maxInstances = 32;
	public float spawnDelay = 0.3f;

	private int phase = 0;
	private int instances = 0;
	private float timeFromLastSpawn = 0;


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
                timeFromLastSpawn += Time.deltaTime;

            Debug.Log("1");
            if ( Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("split");
                GameObject go = Instantiate(template.gameObject,this.transform.GetChild(0).position,Quaternion.identity);
                go.transform.SetParent(this.transform);

                go.GetComponent<ObiActor>().SetPhase(basePhase + phase);

                phase++;
                instances++;
                timeFromLastSpawn = 0;
                
            }
    }

          
        void obitSplit(){
            timeFromLastSpawn += Time.deltaTime;
            Vector3 cellPos = this.gameObject.transform.GetChild(0).position;

            if ( Input.GetKeyDown(KeyCode.Space)){
                GameObject go = Instantiate(template.gameObject,cellPos,Quaternion.identity);
                go.transform.SetParent(transform.parent);

                go.GetComponent<ObiActor>().SetPhase(basePhase + phase);

                phase++;
                instances++;
                timeFromLastSpawn = 0;
                Debug.Log("split");
            }

        }
}
