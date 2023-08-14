using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Portal01Script : MonoBehaviour
{
    VisualEffect VFXportal01;

    bool activePortal = false;       // Portal aktif et yada devre d�s� yap
    float lifeTimeRate = 0.050f;     // portal aktif edilirken  ve kapal�rken  yavasca devreye sok
                                     // float limitCoroutine = 1f;       // limit  0 ve 1 arasi deger gelince Coroutine devre d�s� yap     0 portal kapal�rken | 1 devreye sokarken al�r degeri
    public bool stop = false;    // Coroutine While durdur
    // Start is called before the first frame update
    void Start()
    {
        VFXportal01 = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !activePortal)
        {
            stop = false;
            activePortal = true;
            VFXportal01.Play();

            StartCoroutine(PortalState());
        }
        else if (Input.GetKeyDown(KeyCode.Q)/* && activePortal*/)
        {
            stop = false;
            activePortal = false;
            StartCoroutine(PortalState());
        }
        VFXportal01.SetBool("FirstTrailActive", activePortal);
    }
    IEnumerator PortalState()
    {
        float time = VFXportal01.GetFloat("LifeTime"); // Portal Effectin ic k�sm�n�n sefafl�k degeri zaman g�re art veya azal
        VFXportal01.SetBool("Active", false); // baslang�c effect devreye girsin  etrafa yay�lan parcac�k  Name -> (VFX Portal Particle ) 
        bool resultLimit; // sefafl�k degeri artarken true | azal�rken false cal�s�r  



        while (!stop)
        {
            resultLimit = activePortal == true ? time >= 1f : time <= 0f; // active edilirse 1  | devre d�s� 0 degeri al�r
            if (resultLimit)
            {
                // while() durdur 
                stop = true;
                StopCoroutine(PortalState());
            }

            time += activePortal == true ? lifeTimeRate : -lifeTimeRate; // portal active iken ++ | devre d�s� iken -- deger sayac� 
            VFXportal01.SetFloat("LifeTime", time);         // time degeri g�re  sefafl�k degerleri  g�sterir yada kapalt�r
            yield return new WaitForSeconds(lifeTimeRate);

        }

    }
}
