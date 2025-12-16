using UnityEngine; 

public class XRSimulatorToggle : MonoBehaviour
{
    public GameObject xrSimulator;

    public void ToggleSimulator()
    {
        xrSimulator.SetActive(!xrSimulator.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
