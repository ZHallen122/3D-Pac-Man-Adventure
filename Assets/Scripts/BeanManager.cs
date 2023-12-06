// Author: Allen Zhang

using UnityEngine;
using UnityEngine.UI;

public class BeanManager : MonoBehaviour
{
    public static BeanManager instance;

    public Text beanText;
    public Text leftBeanText;

    int curBean = 0;
    public int leftBean = 0;

    void Awake()
    {
        // Set the instance to this script instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Ensures there is only one instance of BeanManager
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        beanText.text = "Bean get: " + curBean.ToString();
        leftBeanText.text = "Left bean: " + leftBean.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBean() {
        curBean += 1;
        leftBean -= 1;
        beanText.text = "Bean get: " + curBean.ToString();
        leftBeanText.text = "Left bean: " + leftBean.ToString();
    }

    public void addleftBean()
    {
        leftBean += 1;
        leftBeanText.text = "Left bean: " + leftBean.ToString();
    }
}
