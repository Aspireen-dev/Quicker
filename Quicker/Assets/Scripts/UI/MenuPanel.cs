using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject nbTargetsMenu;

    #region Buttons
    public void OnPlayBtnClick(GameObject btn)
    {
        btn.SetActive(false);
        nbTargetsMenu.SetActive(true);
    }

    public void OnNbTargetsClick(int nbTargets)
    {
        SceneManager.LoadScene(nbTargets.ToString());
    }
    #endregion
}
