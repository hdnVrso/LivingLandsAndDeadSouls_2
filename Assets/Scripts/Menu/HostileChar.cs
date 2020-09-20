﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class HostileChar : MonoBehaviour
    {
        void Start()
        {
            Transform button1T = this.gameObject.transform.Find("FewButton");
            Transform button2T = this.gameObject.transform.Find("MediumButton");
            Transform button3T = this.gameObject.transform.Find("ManyButton");
            _button1 = button1T.gameObject.GetComponent<Image>();
            _button2 = button2T.gameObject.GetComponent<Image>();
            _button3 = button3T.gameObject.GetComponent<Image>();
        }

        public void SmallPressed()
        {
            ParameterManager.Instance.HostileCharVal = Random.Range(50, 150);
            SwitchButtonColor(1);
        }

        public void MedPressed()
        {
            ParameterManager.Instance.HostileCharVal = Random.Range(100, 250);
            SwitchButtonColor(2);
        }

        public void LargePressed()
        {
            ParameterManager.Instance.HostileCharVal = Random.Range(150, 350);
            SwitchButtonColor(3);
        }

        private void SwitchButtonColor(int button)
        {
            switch (button)
            {
                case 1:
                    _button1.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    _button2.color = new Vector4(1f, 1f, 1f, 1f);
                    _button3.color = new Vector4(1f, 1f, 1f, 1f);
                    break;
                case 2:
                    _button1.color = new Vector4(1f, 1f, 1f, 1f);
                    _button2.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    _button3.color = new Vector4(1f, 1f, 1f, 1f);
                    break;
                case 3:
                    _button1.color = new Vector4(1f, 1f, 1f, 1f);
                    _button2.color = new Vector4(1f, 1f, 1f, 1f);
                    _button3.color = new Vector4(0.6f, 0.6f, 0.6f, 1f);
                    break;
            }
        }

        //data members
        private Image _button1;
        private Image _button2;
        private Image _button3;
    }
}//end of namespace Menu
