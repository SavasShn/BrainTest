using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaycastCamera : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI girdi;                 
    [SerializeField] TextMeshProUGUI soruText;              
    [SerializeField] GameObject[] kutularinHepsi;
    
    [SerializeField] TextMeshProUGUI[] randomSayilar;       
    [SerializeField] TextMeshProUGUI PuanText;
    int puan;
    void Start()
    {
        AlayiniCevirStart();
        uretim();
        soruText.text = "";
        yazilarEkranda = true;
        tusAcKapa();
        
    }

    int x, y, cevap;
    
    public void uretim()
    {
        for (int i = 0; i < randomSayilar.Length; i++)
        {
            randomSayilar[i].text = Random.Range(1, 10).ToString();
        }
        
        x = Random.Range(0, 9);
        y = Random.Range(0, 9);
        islem();
    }

    public void islem()
    {
        if (x < 5)
        {
            cevap = int.Parse(randomSayilar[x].text) + int.Parse(randomSayilar[y].text);
            soruText.text = randomSayilar[x].transform.parent.parent.transform.name + " + " + randomSayilar[y].transform.parent.parent.transform.name;

        }
        else
        {
            if (int.Parse(randomSayilar[x].text) > int.Parse(randomSayilar[y].text))
            {
                cevap = int.Parse(randomSayilar[x].text) - int.Parse(randomSayilar[y].text);
                soruText.text = randomSayilar[x].transform.parent.parent.transform.name + " - " + randomSayilar[y].transform.parent.parent.transform.name;
            }
            else
            {
                cevap = int.Parse(randomSayilar[y].text) - int.Parse(randomSayilar[x].text);
                soruText.text = randomSayilar[y].transform.parent.parent.transform.name + " - " + randomSayilar[x].transform.parent.parent.transform.name;
            }
        }
    }
    public void Play()
    {
        if (!HerKutu.donmeAsamasinda)
        {
            AlayiniCevir();
            islem();



            Invoke(nameof(TusKapaInvoke), 1.1f);
        }
    }
    public void TusKapaInvoke()
    {
        yazilarEkranda = !yazilarEkranda;
        tusAcKapa();
    }
    bool yazilarEkranda = true;
    [SerializeField] Button[] tuslar;
    public void tusAcKapa()
    {
        if (yazilarEkranda)
        {
            foreach (var x in tuslar)
            {
                x.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            foreach (var x in tuslar)
            {
                x.GetComponent<Button>().interactable = true;
            }
        }

    }
    public void AlayiniCevir()
    {
        InvokeRepeating("AlayiniCevirFonksiyonu", 0, 0.07f);
    }

    public static int i;
    void AlayiniCevirFonksiyonu()
    {
        if (i == kutularinHepsi.Length)
        {
            CancelInvoke("AlayiniCevirFonksiyonu");

            i = 0;
        }
        else
        {
            kutularinHepsi[i].transform.GetComponent<HerKutu>().Cevir();
            i++;
        }
    }

    void AlayiniCevirStart()
    {
        for (int i = 0; i < kutularinHepsi.Length; i++)
        {
            kutularinHepsi[i].transform.GetComponent<HerKutu>().Cevir();
        }
    }



    public void Tuslar(int buttonSayi)
    {
        if (girdi.text.Length < 2)
        {
            girdi.text += buttonSayi.ToString();
            if (int.Parse(girdi.text) == cevap)
            {
                AlayiniCevir();
                uretim();
                soruText.text = "Cevap Doðru";
                Invoke("girdiSifirla", 1.5f);
                puan += 10;
                PuanText.text = "Puan: " + puan.ToString();
                yazilarEkranda = true;
                tusAcKapa();
            }
        }
        else
        {
            girdi.text = "";
        }
    }
    public void girdiSifirla()
    {
        girdi.text = "";
        soruText.text = "";
    }

    [SerializeField] Image bar;
    [SerializeField] GameObject kapat;
    float saat;
    bool timerYeniden = true;
    [SerializeField] TextMeshProUGUI toplamPuan;
    private void Update()
    {
        if (timerYeniden)
        {
            saat += Time.deltaTime;
            bar.fillAmount = (60 - saat) * 0.0166667f;
        }

        if (saat >= 60)
        {
            saat = 0;
            kapat.SetActive(true);
            toplamPuan.text = "Toplam: " + puan.ToString();
            timerYeniden = false;
        }
    }
    public void yeniden()
    {
        bar.fillAmount = 1;
        kapat.SetActive(false);

        SceneManager.LoadScene(1);
    }

    public void Kapat()
    {
        Application.Quit();
    }



}
