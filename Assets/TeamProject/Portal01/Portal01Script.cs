using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Portal01Script : MonoBehaviour
{
    VisualEffect VFXportal01;

    bool activePortal = false;       // Portal aktif et yada devre dýsý yap
    float lifeTimeRate = 0.050f;     // portal aktif edilirken  ve kapalýrken  yavasca devreye sok
                                     // float limitCoroutine = 1f;       // limit  0 ve 1 arasi deger gelince Coroutine devre dýsý yap     0 portal kapalýrken | 1 devreye sokarken alýr degeri
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
        float time = VFXportal01.GetFloat("LifeTime"); // Portal Effectin ic kýsmýnýn sefaflýk degeri zaman göre art veya azal
        VFXportal01.SetBool("Active", false); // baslangýc effect devreye girsin  etrafa yayýlan parcacýk  Name -> (VFX Portal Particle ) 
        bool resultLimit; // sefaflýk degeri artarken true | azalýrken false calýsýr  



        while (!stop)
        {
            resultLimit = activePortal == true ? time >= 1f : time <= 0f; // active edilirse 1  | devre dýsý 0 degeri alýr
            if (resultLimit)
            {
                // while() durdur 
                stop = true;
                StopCoroutine(PortalState());
            }

            time += activePortal == true ? lifeTimeRate : -lifeTimeRate; // portal active iken ++ | devre dýsý iken -- deger sayacý 
            VFXportal01.SetFloat("LifeTime", time);         // time degeri göre  sefaflýk degerleri  gösterir yada kapaltýr
            yield return new WaitForSeconds(lifeTimeRate);

        }

    }
}
