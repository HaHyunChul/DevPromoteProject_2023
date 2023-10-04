using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneNB_DevPromoteProject2013_Calculator_1
{
    enum OPERATOR_MODE
    {
        EQUAL,
        PULS,
        MINUS,
        NONE,
    };

    enum SIGN_MODE
    {
        POSITIVE,
        NEGATIVE,
    };

    enum KEY_MODE
    {
        KEY_0,
        KEY_1,
        KEY_2,
        KEY_3,
        KEY_4,
        KEY_5,
        KEY_6,
        KEY_7,
        KEY_8,
        KEY_9,
        KEY_SIGN,
        KEY_CLEAR,
        KEY_BACKSPACE,        
        KEY_PULS,
        KEY_MINUS,
        KEY_EQUAL,
        KEY_NONE,        
    };

    public partial class Form1 : Form
    {
        int maxCalcViewNum = int.MaxValue;
        int minCalcViewNum = int.MinValue;

        string calcView = string.Empty;
        string historyView = string.Empty;

        KEY_MODE lastKey = KEY_MODE.KEY_0;

        SIGN_MODE[] paramSign = new SIGN_MODE[2] { SIGN_MODE.POSITIVE, SIGN_MODE.POSITIVE };
        OPERATOR_MODE[] paramOperator = new OPERATOR_MODE[2] { OPERATOR_MODE.NONE, OPERATOR_MODE.NONE };

        int[] paramCalc = new int[2] { 0, 0 };

        public Form1()
        {
            InitializeComponent();

            txtCalcView.Text = "0";
        }

        private void btnKey0_Click(object sender, EventArgs e)
        {
            string view = string.Empty;
            int calc;

            UpdateCalcView(ref view, false);
            int.TryParse(view, out calc);

            if (calc != 0)
            {
                if (AddCalcView("0") == true)
                {
                    lastKey = KEY_MODE.KEY_0;
                }

            }
        }

        private void btnKey1_Click(object sender, EventArgs e)
        {
            if (AddCalcView("1") == true)
            {
                lastKey = KEY_MODE.KEY_1;
            }
        }

        private void btnKey2_Click(object sender, EventArgs e)
        {
            if (AddCalcView("2") == true)
            {
                lastKey = KEY_MODE.KEY_2;
            }
        }

        private void btnKey3_Click(object sender, EventArgs e)
        {
            if (AddCalcView("3") == true)
            {
                lastKey = KEY_MODE.KEY_3;
            }
        }

        private void btnKey4_Click(object sender, EventArgs e)
        {
            if (AddCalcView("4") == true)
            {
                lastKey = KEY_MODE.KEY_4;
            }
        }

        private void btnKey5_Click(object sender, EventArgs e)
        {
            if (AddCalcView("5") == true)
            {
                lastKey = KEY_MODE.KEY_5;
            }
        }

        private void btnKey6_Click(object sender, EventArgs e)
        {
            if (AddCalcView("6") == true)
            {
                lastKey = KEY_MODE.KEY_6;
            }
        }

        private void btnKey7_Click(object sender, EventArgs e)
        {
            if (AddCalcView("7") == true)
            {
                lastKey = KEY_MODE.KEY_7;
            }
        }

        private void btnKey8_Click(object sender, EventArgs e)
        {
            if (AddCalcView("8") == true)
            {
                lastKey = KEY_MODE.KEY_8;
            }
        }

        private void btnKey9_Click(object sender, EventArgs e)
        {
            if (AddCalcView("9") == true)
            {
                lastKey = KEY_MODE.KEY_9;
            }
        }

        private void btnKeySign_Click(object sender, EventArgs e)
        {
            string view = string.Empty;
            int calc;

            UpdateCalcView(ref view, false);
            int.TryParse(view, out calc);

            calc *= -1;
            view = calc.ToString();
            UpdateCalcView(ref view);
        }

        private void btnKeyPlus_Click(object sender, EventArgs e)
        {
            string view = string.Empty;
            int calc;

            UpdateCalcView(ref view, false);
            int.TryParse(view, out calc);

            if(lastKey != KEY_MODE.KEY_PULS && lastKey != KEY_MODE.KEY_MINUS)
            {
                if (paramOperator[0] == OPERATOR_MODE.NONE || paramOperator[0] == OPERATOR_MODE.EQUAL)
                {
                    paramCalc[0] = calc;
                }
                else
                {
                    paramCalc[1] = calc;
                    calc = Calc(paramCalc[0], paramCalc[1], paramOperator[0]);
                    paramCalc[0] = calc;
                }
            }
            
            paramOperator[0] = OPERATOR_MODE.PULS;
            lastKey = KEY_MODE.KEY_PULS;

            view = calc.ToString();
            UpdateCalcView(ref view);

            UpdateHistoryView(ref view);
            AddHistroyView("+");
        }

        private void btnKeyMinus_Click(object sender, EventArgs e)
        {
            string view = string.Empty;
            int calc;

            UpdateCalcView(ref view, false);
            int.TryParse(view, out calc);

            if (lastKey != KEY_MODE.KEY_PULS && lastKey != KEY_MODE.KEY_MINUS)
            {
                if (paramOperator[0] == OPERATOR_MODE.NONE || paramOperator[0] == OPERATOR_MODE.EQUAL)
                {
                    paramCalc[0] = calc;
                }
                else
                {
                    paramCalc[1] = calc;
                    calc = Calc(paramCalc[0], paramCalc[1], paramOperator[0]);
                    paramCalc[0] = calc;
                }
            }
            paramOperator[0] = OPERATOR_MODE.MINUS;
            lastKey = KEY_MODE.KEY_MINUS;

            view = calc.ToString();
            UpdateCalcView(ref view);

            UpdateHistoryView(ref view);
            AddHistroyView("-");
        }

        private void btnKeyEqual_Click(object sender, EventArgs e)
        {
            string view = string.Empty;
            int calc;

            UpdateCalcView(ref view, false);
            int.TryParse(view, out calc);

            if (lastKey != KEY_MODE.KEY_PULS && lastKey != KEY_MODE.KEY_MINUS)
            {
                if (paramOperator[0] == OPERATOR_MODE.NONE || paramOperator[0] == OPERATOR_MODE.EQUAL)
                {
                    paramCalc[0] = calc;
                }
                else
                {
                    paramCalc[1] = calc;
                    calc = Calc(paramCalc[0], paramCalc[1], paramOperator[0]);
                    paramCalc[0] = calc;
                }
            }
            paramOperator[0] = OPERATOR_MODE.EQUAL;
            lastKey = KEY_MODE.KEY_EQUAL;

            view = calc.ToString();
            UpdateCalcView(ref view);

            UpdateHistoryView(ref view);
            AddHistroyView("=");
        }

        private void btnKeyClear_Click(object sender, EventArgs e)
        {
            string view = string.Empty;
            UpdateCalcView(ref view);
            UpdateHistoryView(ref view);

            paramCalc[0] = 0;
            paramCalc[1] = 0;
        
            paramOperator[0] = OPERATOR_MODE.NONE;
            paramOperator[1] = OPERATOR_MODE.NONE;        
            
            lastKey = KEY_MODE.KEY_CLEAR;
        }

        private void btnKeyBackspace_Click(object sender, EventArgs e)
        {
            string view = string.Empty;

            UpdateCalcView(ref view, false);
            if (0 < view.Length)
            {
                view = view.Remove(view.Length - 1, 1);

                UpdateCalcView(ref view);
            }
        }

        private int Calc(int param0, int param1, OPERATOR_MODE operator0)
        {
            int result;

            switch(operator0)
            {
                case OPERATOR_MODE.PULS:
                    result = param0 + param1;
                    break;

                case OPERATOR_MODE.MINUS:
                    result = param0 - param1;
                    break;

                default:
                    result = 0;
                    break;
            }

            return result;
        }

        private void SetParamCalc(int indexParam)
        {
            string view = string.Empty;
            int calc;

            UpdateCalcView(ref view, false);
            int.TryParse(view, out calc);
            
            paramCalc[indexParam] = calc;
        }

        private bool AddCalcView(string addCalc)
        {            
            string view = string.Empty;
            int calc;

            
            UpdateCalcView(ref view, false);

            if (lastKey == KEY_MODE.KEY_PULS || lastKey == KEY_MODE.KEY_MINUS)
            {
                view = "0";
            }

            if (int.TryParse(view + addCalc, out calc) == true)
            {
                if (minCalcViewNum <= calc && calc <= maxCalcViewNum)
                {
                    if (view == "0")
                    {
                        view = addCalc;
                    }
                    else
                    {
                        view += addCalc;
                    }

                    UpdateCalcView(ref view);
                    return true;
                }
            }
            return false;
        }


        private void UpdateCalcView(ref string calc, bool update = true)
        {
            if(update == true)
            {                
                txtCalcView.Text = calc;
            }
            else
            {
                calc = txtCalcView.Text;
            }             
        }

        private bool AddHistroyView(string addHistroy)
        {
            string view = string.Empty;
            int calc;

            UpdateHistoryView(ref view, false);
            view += addHistroy;
            UpdateHistoryView(ref view);

            return false;
        }


        private void UpdateHistoryView(ref string history, bool update = true)
        {            
            if (update == true)
            {
                txtHistoryView.Text = history;
            }
            else
            {
                history = txtHistoryView.Text;
            }
        }
    }
}
