using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    /// <summary>
    /// Блюда
    /// </summary>
    public partial class FormDishes : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDishLogic dishLogic;

        public FormDishes(IDishLogic dishLogic)
        {
            InitializeComponent();
            this.dishLogic = dishLogic;
        }

        private void FormDishes_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Загрузить список блюд
        /// </summary>
        private void LoadData()
        {
            try
            {
                var list = dishLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Width = 200;
                    dataGridView.Columns[2].Width = 80;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавить блюдо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDish>();
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        /// <summary>
        /// Изменить блюдо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormDish>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            else
                MessageBox.Show("Выберите строку с блюдом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        dishLogic.Delete(new DishBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
            else
                MessageBox.Show("Выберите строку с блюдом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обновить список блюд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}