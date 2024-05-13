using UnityEngine;
using UnityEngine.UI;

namespace Instructions
{
    public class InstructionManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] InstructionPages;
        [SerializeField] private GameObject[] InstructionButtons;
        [SerializeField] private GameObject UnderstoodButton;
        public Sprite activePage;
        public Sprite inactivePage;
        private bool allPagesSeen = false;

        public void FirstPageClicked()
        {
            InstructionButtons[0].GetComponent<Image>().sprite = activePage;
            InstructionButtons[1].GetComponent<Image>().sprite = inactivePage;
            InstructionPages[0].SetActive(true);
            InstructionPages[1].SetActive(false);
        }

        public void SecondPageClicked()
        {
            InstructionButtons[0].GetComponent<Image>().sprite = inactivePage;
            InstructionButtons[1].GetComponent<Image>().sprite = activePage;
            InstructionPages[0].SetActive(false);
            InstructionPages[1].SetActive(true);
            UnderstoodButton.SetActive(true);
        }
    }
}