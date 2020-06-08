using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormAddProduct : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IProductLogic logic;

        public FormAddProduct(IProductLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
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
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(textBoxCount.Text, out double res))
            {
                MessageBox.Show("Неверный формат числа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var product = (ProductViewModel)comboBoxProduct.SelectedItem;
            logic.CreateOrUpdate(new ProductBindingModel
            {
                Id = product.Id,
                FillWeight = product.FillWeight.HasValue ? product.FillWeight + res : res,
                Price = product.Price,
                ProductName = product.ProductName
            });
            MessageBox.Show("Склад пополнен успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
