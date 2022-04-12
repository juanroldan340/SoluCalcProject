using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculadora
{
    public partial class Form1 : Form
    {
        StringBuilder value = new StringBuilder();
        double acumulatedValue;
        double secondAcumulatedValue;
        string operationValue;
        double length;

        public Form1()
        {
            InitializeComponent();
        }

        private void ShowValues(string n)
        {
            value.Replace(".", ",");
            value.Append(n);
            txtValue.Text = value.ToString();
        }

        private void AcumulateValue(string operation, double dvalue)
        {
            switch (operation)
            {
                case "x":
                    acumulatedValue *= dvalue;
                    break;

                case "÷":
                    acumulatedValue /= dvalue;
                    break;
                case "+":
                    acumulatedValue += dvalue;
                    break;
                case "-":
                    acumulatedValue -= dvalue;
                    break;
            }
            txtValue.Text = acumulatedValue.ToString();
        }

        // Esto se ejecutará cuando sea presionado un botón numérico
        // This will be executed when a numeric button is pressed
        public void SendValue(object sender, EventArgs e) 
        {
            if (txtValue.Text.Length > 0 && txtValue.Text.Length < 2) 
            {
                value.Replace("0", "");
            }
            
            Button button = (Button)sender;
            ShowValues(button.Text);
        }

        // Cuando se presiona el botón Clear (C)
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            value.Remove(0, value.Length);
            acumulatedValue = 0;
            secondAcumulatedValue = 0;
            operationValue = string.Empty;
            length = 0;
            ShowValues("0");
        }

        // Cuando se presiona el botón de alguna operación (+, -, ÷, x)
        // When some operation button is pressed (+, -, ÷, x)
        public void OpValue(object sender, EventArgs e) 
        {
            try
            {
                Button buttonValue = (Button)sender;
                if (operationValue == null)
                {
                    operationValue = string.Empty;
                }
                else
                {
                    if (!operationValue.Contains("+") || !operationValue.Contains("-") || !operationValue.Contains("÷") || !operationValue.Contains("x"))
                    {
                        operationValue = buttonValue.Text;
                        acumulatedValue = double.Parse(txtValue.Text);
                    }
                }
                acumulatedValue = double.Parse(txtValue.Text);
                operationValue = buttonValue.Text;
                length = value.Length;
                ShowValues($"{operationValue}");
            }
            catch (FormatException)
            {
                txtValue.Text = "...";
                lblError.Text = "Sólo se pueden cálculos de dos cifras.";
            }
        }

        // Cuando el botón ( = ) se presiona
        // When Equals Button is pressed
        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {

                if (length > 0)
                {
                    value.Remove(0, (int)length+1);
                    secondAcumulatedValue = double.Parse(value.ToString());
                }
                AcumulateValue(operationValue, secondAcumulatedValue);
                value.Remove(0, value.Length);
                ShowValues($"{txtValue.Text}");
            }
            catch (ArgumentOutOfRangeException)
            {
                txtValue.Text = "...";
                lblError.Text = "Presione en C";
            }
            catch (FormatException) 
            {
                txtValue.Text = "...";
                lblError.Text = "Error de Sintaxis";
            }
        }
        

    }
}
