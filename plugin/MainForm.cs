using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ParametersLogic;
using ApiLogic;

namespace plugin
{
    // TODO: комментарии ко всему классу.
    public partial class MainForm : Form
    {
        private Builder _builder;

        public MainForm()
        {
            InitializeComponent();
        }

        // TODO: методы в стиле PASCAL.
        private void buttonRun_Click(object sender, EventArgs e)
        {
            var voidTextBoxes = AreTextBoxesVoid();
            if (!voidTextBoxes)
            {
                var paramDict = new Dictionary<ParamType, Parameter>();

                int.TryParse(textBoxTopWidth.Text, out int topWidth);
                int.TryParse(textBoxTopDepth.Text, out int topDepth);
                int.TryParse(textBoxTopHeight.Text, out int topHeight);
                int.TryParse(textBoxLegsWidth.Text, out int legsWidth);
                int.TryParse(textBoxTableHeight.Text, out int tableHeight);

                paramDict.Add(ParamType.TopWidth, new Parameter(topWidth));
                paramDict.Add(ParamType.TopDepth, new Parameter(topDepth));
                paramDict.Add(ParamType.TopHeight, new Parameter(topHeight));
                paramDict.Add(ParamType.LegWidth, new Parameter(legsWidth));
                paramDict.Add(ParamType.TableHeight, new Parameter(tableHeight));

                var parameters = new Parameters();
                var incorrect = parameters.SetParameters(paramDict);

                if (incorrect.Count == 0 && !voidTextBoxes)
                {
                    BuildModel(parameters);
                }
                else
                {
                    PrintErrors(incorrect, parameters);
                }
            }
        }

        private bool AreTextBoxesVoid()
        {
            labelError.Text = "";
            textBoxTopWidth.BackColor = Color.White;
            textBoxTopDepth.BackColor = Color.White;
            textBoxTopHeight.BackColor = Color.White;
            textBoxLegsWidth.BackColor = Color.White;
            textBoxTableHeight.BackColor = Color.White;

            //TODO: Нормальное название локальной переменной.
            bool rv = false;

            //TODO: Дублирование кода.
            if (textBoxTopWidth.Text == "")
            {
                labelError.Text += "Ошибка: значение ширины столешницы не может быть пустым\n";
                textBoxTopWidth.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxTopDepth.Text == "")
            {
                labelError.Text += "Ошибка: значение глубины столешницы не может быть пустым\n";
                textBoxTopDepth.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxTopHeight.Text == "")
            {
                labelError.Text += "Ошибка: значение толщины столешницы не может быть пустым\n";
                textBoxTopHeight.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxLegsWidth.Text == "")
            {
                labelError.Text += "Ошибка: значение ширины ножек не может быть пустым\n";
                textBoxLegsWidth.BackColor = Color.LightPink;
                rv = true;
            }
            if (textBoxTableHeight.Text == "")
            {
                labelError.Text += "Ошибка: значение высоты стола не может быть пустым\n";
                textBoxTableHeight.BackColor = Color.LightPink;
                rv = true;
            }

            return rv;
        }

        private void TextBox_OnlyDigitKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PrintErrors(List<IncorrectParameters> incorrect, Parameters parameters)
        {
            var dict = parameters.GetParameters();

            foreach (var param in incorrect)
            {
                // TODO: Дублирование кода.
                switch (param)
                {
                    case IncorrectParameters.TopWidthIncorrect:
                        var value = dict[ParamType.TopWidth];
                        labelError.Text += "Ошибка: параметр \"ширина столешницы\" должен входить в диапазон от" + 
                            value.MinValue + "до" + value.MaxValue + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopDepthIncorrect:
                        value = dict[ParamType.TopDepth];
                        labelError.Text += "Ошибка: параметр \"глубина столешницы\" должен входить в диапазон от" + 
                            value.MinValue + "до" + value.MaxValue + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopDepth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopHeightIncorrect:
                        value = dict[ParamType.TopHeight];
                        labelError.Text += "Ошибка: параметр \"высота столешницы\" должен входить в диапазон от" + 
                            value.MinValue + "до" + value.MaxValue + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.LegWidthIncorrect:
                        value = dict[ParamType.LegWidth];
                        labelError.Text += "Ошибка: параметр \"ширина ножек\" должен входить в диапазон от" + 
                            value.MinValue + "до" + value.MaxValue + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxLegsWidth.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TableHeightIncorrect:
                        value = dict[ParamType.TableHeight];
                        labelError.Text += "Ошибка: параметр \"высота стола\" должен входить в диапазон от" + 
                            value.MinValue + "до" + value.MaxValue + "\n";
                        labelError.BackColor = Color.LightPink;
                        textBoxTableHeight.BackColor = Color.LightPink;
                        break;
                    case IncorrectParameters.TopAndLegsAreaIncorrect:
                        labelError.Text += 
                            "Ошибка: связанные параметры \"ширина столешницы, глубина столешницы и ширина ножек\"\n" +
                            "    имеют недопустимые параметры:\n" +
                            "    площадь столешницы должна быть больше площади сечения ножек";
                        labelError.BackColor = Color.LightPink;
                        textBoxTopWidth.BackColor = Color.LightPink;
                        textBoxTopDepth.BackColor = Color.LightPink;
                        textBoxLegsWidth.BackColor = Color.LightPink;
                        break;
                }
            }
        }

        void BuildModel(Parameters parameters)
        {
            Cad cad = radioButtonKompas.Checked
                ? Cad.Kompas
                : Cad.AutoCad;

            _builder = new Builder(parameters, cad);
            _builder.Build();
        }
    }
}
