﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class StartSeason : MonoBehaviour
    {
        void Start()
        {
            _button1 = this.gameObject.transform.Find("Summer").GetComponent<Image>();
            _button2 = this.gameObject.transform.Find("Autumn").GetComponent<Image>();
            _button3 = this.gameObject.transform.Find("Winter").GetComponent<Image>();
            _button4 = this.gameObject.transform.Find("Spring").GetComponent<Image>();
        }
        
        public void Summer()
        {
            GameObject.Find("ParametersManager").GetComponent<ParameterManager>().StartSeason = 0;
            SwitchButtonColor(1);
        }

        public void Autumn()
        {
            GameObject.Find("ParametersManager").GetComponent<ParameterManager>().StartSeason = 1;
            SwitchButtonColor(2);
        }

        public void Winter()
        {
            GameObject.Find("ParametersManager").GetComponent<ParameterManager>().StartSeason = 2;
            SwitchButtonColor(3);
        }

        public void Spring()
        {
            GameObject.Find("ParametersManager").GetComponent<ParameterManager>().StartSeason = 3;
            SwitchButtonColor(4);
        }
        
        private void SwitchButtonColor(int button)
        {
            switch (button)
            {
                case 1:
                    _button1.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    _button2.color = new Vector4(1f, 1f, 1f, 1f);
                    _button3.color = new Vector4(1f, 1f, 1f, 1f);
                    _button4.color = new Vector4(1f, 1f, 1f, 1f);
                    break;
                case 2:
                    _button1.color = new Vector4(1f, 1f, 1f, 1f);
                    _button2.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    _button3.color = new Vector4(1f, 1f, 1f, 1f);
                    _button4.color = new Vector4(1f, 1f, 1f, 1f);
                    break;
                case 3:
                    _button1.color = new Vector4(1f, 1f, 1f, 1f);
                    _button2.color = new Vector4(1f, 1f, 1f, 1f);
                    _button3.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    _button4.color = new Vector4(1f, 1f, 1f, 1f);
                    break;
                case 4:
                    _button1.color = new Vector4(1f, 1f, 1f, 1f);
                    _button2.color = new Vector4(1f, 1f, 1f, 1f);
                    _button3.color = new Vector4(1f, 1f, 1f, 1f);
                    _button4.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    break;
            }
        }
        
        //data members
        private Image _button1;
        private Image _button2;
        private Image _button3;
        private Image _button4;
    }
}//end of namespace Menu
