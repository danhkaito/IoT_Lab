using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace ChuongGa{
public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
        [SerializeField]
        private ToggleController status_LED;
        [SerializeField]
        private ToggleController status_PUMP;
        [SerializeField]
        private Text temp;
        [SerializeField]
        private Text humi;

        [SerializeField]
        private CanvasGroup allow_sw_LED;
            [SerializeField]
        private CanvasGroup allow_sw_PUMP;

        [SerializeField]
        private CanvasGroup _canvasLayer1;
        [SerializeField]
        private CanvasGroup _canvasLayer2;
        private Tween twenFade;
        public Speedometer gauge;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_ControlLED(ControlLED_Data _control_data)
        {
            {
                allow_sw_LED.interactable = true;
                if (_control_data.status == "ON")
                    status_LED.isOn = true;
                else{
                    status_LED.isOn = false;
                }
            }

        }
    public void Update_ControlPUMP(ControlPUMP_Data _control_data)
        {
            {
                allow_sw_PUMP.interactable = true;
                if (_control_data.status == "ON")
                    status_PUMP.isOn = true;
                else{
                    status_PUMP.isOn = false;
                }
            }

        }
    public ControlLED_Data Update_ControlLED_Value(ControlLED_Data _controlLED)
        {
            _controlLED.status = (status_LED.isOn ? "ON" : "OFF");
            _controlLED.device="LED";
            allow_sw_LED.interactable = false;
            return _controlLED;
        }
    public ControlPUMP_Data Update_ControlPUMP_Value(ControlPUMP_Data _controlPUMP)
        {
            _controlPUMP.status = (status_PUMP.isOn ? "ON" : "OFF");
            _controlPUMP.device="PUMP";

            allow_sw_PUMP.interactable = false;
            return _controlPUMP;
        }

        public void Update_Status(Status_Data _status_data)
        {
            temp.text = _status_data.temperature.ToString()+" °C";
            humi.text = _status_data.humidity.ToString()+" %";
            gauge.speed=_status_data.temperature;
        }












    IEnumerator _IESwitchLayer()
        {
            if (_canvasLayer1.interactable == true)
            {
                FadeOut(_canvasLayer1, 0.25f);
                yield return new WaitForSeconds(0.5f);
                FadeIn(_canvasLayer2, 0.25f);
            }
            else
            {
                FadeOut(_canvasLayer2, 0.25f);
                yield return new WaitForSeconds(0.5f);
                FadeIn(_canvasLayer1, 0.25f);
            }
        }


        public void SwitchLayer()
        {
            StartCoroutine(_IESwitchLayer());
        }


        public void Fade(CanvasGroup _canvas, float endValue, float duration, TweenCallback onFinish)
        {
            if (twenFade != null)
            {
                twenFade.Kill(false);
            }

            twenFade = _canvas.DOFade(endValue, duration);
            twenFade.onComplete += onFinish;
        }

        public void FadeIn(CanvasGroup _canvas, float duration)
        {
            Fade(_canvas, 1f, duration, () =>
            {
                _canvas.interactable = true;
                _canvas.blocksRaycasts = true;
            });
        }

        public void FadeOut(CanvasGroup _canvas, float duration)
        {
            Fade(_canvas, 0f, duration, () =>
            {
                _canvas.interactable = false;
                _canvas.blocksRaycasts = false;
            });
        }
}
}