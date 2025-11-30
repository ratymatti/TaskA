using UnityEngine;

public class KenttaAsetin : MonoBehaviour
{
    GameObject ilmastointiteippi;

    GameObject tyokalupakki;

    GameObject sorkkarauta;

    GameObject taskulamppu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ilmastointiteippi = GameObject.Find("Ilmastointiteippi");
        tyokalupakki = GameObject.Find("Tyokalupakki");
        sorkkarauta = GameObject.Find("Sorkkarauta");
        taskulamppu = GameObject.Find("Taskulamppu");

        ilmastointiteippi.transform.position = new Vector3(-7f, 0f, 7f);
        tyokalupakki.transform.position = new Vector3(6f, 0f, -7f);
        sorkkarauta.transform.position = new Vector3(-7f, 1f, -7f);
        taskulamppu.transform.position = new Vector3(7f, 1f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
