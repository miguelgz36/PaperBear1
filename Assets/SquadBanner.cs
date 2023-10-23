using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SquadBanner : MonoBehaviour
{
    [SerializeField] Image squadImage;
    private Squad squad;

    public Squad Squad { get => squad; set => squad = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.squad == null)
        {
            this.HideUI();
        }
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void SetSprite(Sprite sprite)
    {
        squadImage.sprite = sprite;
    }
}
