// Author: Allen Zhang, Qiaoxin Huang

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
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        beanText.text = "Score: " + curBean.ToString();
    }

    // Update the score based on the number of collectibles collected
    public void addBean() {
        curBean += 1;
        leftBean -= 1;
        beanText.text = "Score: " + curBean.ToString();
    }

    public void addleftBean()
    {
        leftBean += 1;
    }
}
