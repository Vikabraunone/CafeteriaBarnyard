using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Enums;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormDish : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IDishLogic dLogic;

        private readonly IProductLogic pLogic;

        private int? id;

        private Dictionary<int, (string, double)> dishProducts;

        public FormDish(IDishLogic dLogic, IProductLogic pLogic)
        {
            InitializeComponent();
            this.dLogic = dLogic;
            this.pLogic = pLogic;
            dataGridView.Columns.Add("Id", "Id");
            dataGridView.Columns.Add("ProductName", "Продукт");
            dataGridView.Columns.Add("Weight", "Количество");
            dataGridView.Columns.Add("Price", "Цена продукта");
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Width = 100;
            dataGridView.Columns[2].Width = 80;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormDish_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    DishViewModel view = dLogic.Read(new DishBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.DishName;
                        textBoxPrice.Text = view.Price.ToString();
                        dishProducts = view.DishProducts;
                        comboBoxTypeDish.DataSource = Enum.GetValues(typeof(DishType))
                            .Cast<DishType>()
                            .Select(x => x.ToString())
                            .ToList();
                        comboBoxTypeDish.SelectedItem = null;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                dishProducts = new Dictionary<int, (string, double)>();
        }

        private void CalcSum()
        {
            decimal price = 0;
            foreach (var e in dishProducts)
                price += pLogic.Read(new ProductBindingModel { ProductName = e.Value.Item1 })[0].Price;
            textBoxResult.Text = price.ToString();
        }

        private void LoadData()
        {
            try
            {
                if (dishProducts != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var dp in dishProducts)
                        dataGridView.Rows.Add(new object[] { dp.Key, dp.Value.Item1, dp.Value.Item2 });
                    CalcSum();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDishProducts>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (dishProducts.ContainsKey(form.Id))
                    dishProducts[form.Id] = (form.NameProduct, form.Weight);
                else
                    dishProducts.Add(form.Id, (form.NameProduct, form.Weight));
                LoadData();
            }
            else
                MessageBox.Show("Выберите строку с продуктом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormDishProducts>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Weight = dishProducts[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dishProducts[form.Id] = (form.NameProduct, form.Weight);
                    LoadData();
                }
            }
            else
                MessageBox.Show("Выберите строку с продуктом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dishProducts.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
            else
                MessageBox.Show("Выберите строку с продуктом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(textBoxPrice.Text, out double weight))
            {
                MessageBox.Show("Неккоректно введен вес", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dishProducts == null || dishProducts.Count == 0)
            {
                MessageBox.Show("Заполните ингредиенты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTypeDish.SelectedValue == null)
            {
                MessageBox.Show("Укажите тип блюда", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                // ПРОВЕРИТЬ!!
                dLogic.CreateOrUpdate(new DishBindingModel
                {
                    Id = id,
                    DishName = textBoxName.Text,
                    DishProducts = dishProducts,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    DishType = (DishType)Enum.Parse(typeof(DishType), comboBoxTypeDish.SelectedItem.ToString())
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
