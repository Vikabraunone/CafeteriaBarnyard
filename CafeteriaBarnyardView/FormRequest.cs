using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormRequest : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id
        {
            get { return Convert.ToInt32(comboBoxProduct.SelectedValue); }
            set { comboBoxProduct.SelectedValue = value; }
        }

        public string NameProduct { get { return comboBoxProduct.Text; } }

        public double Weight
        {
            get { return Convert.ToDouble(textBoxWeight.Text); }
            set { textBoxWeight.Text = value.ToString(); }
        }

        public FormRequest(IProductLogic logic)
        {
            InitializeComponent();
            //  Получаем список продуктов и заносим его в выпадающий список
            List<ProductViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxProduct.DisplayMember = "ProductName";
                comboBoxProduct.ValueMember = "Id";
                comboBoxProduct.DataSource = list;
                comboBoxProduct.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedValue == null)
            {
                MessageBox.Show("Выберите продукт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxWeight.Text))
            {
                MessageBox.Show("Заполните поле количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(textBoxWeight.Text, out double res))
            {
                MessageBox.Show("Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}