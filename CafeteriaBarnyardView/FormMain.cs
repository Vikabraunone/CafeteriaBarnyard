﻿using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IProductLogic productLogic;

        public FormMain(IProductLogic productLogic, IClientLogic clientLogic)
        {
            InitializeComponent();
            this.productLogic = productLogic;
            if (!clientLogic.IsAdmin(new ClientBindingModel { Id = Program.Client.Id }))
                пополнитьToolStripMenuItem.Visible = false;
            LoadData();
        }

        /// <summary>
        /// Загрузить список продуктов
        /// </summary>
        private void LoadData()
        {
            try
            {
                var list = productLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Width = 200;
                    dataGridView.Columns[2].Width = 100;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вызвать форму с блюдами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDishes_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDishes>();
            form.ShowDialog();
        }

        /// <summary>
        /// Вызвать форму с заказами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOrders_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormOrders>();
            form.ShowDialog();
        }

        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormProduct>();
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        /// <summary>
        /// Изменить продукт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormProduct>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            else
                MessageBox.Show("Выберите строку с продуктом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обновить список с продуктами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Удалить продукт
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
                        productLogic.Delete(new ProductBindingModel { Id = id });
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

        private void заявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRequest>();
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void пополнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAddProduct>();
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void изменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRegister>();
            form.ShowDialog();
        }

        private void поЗаявкамИЗаказамЗаПериодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportPeriod>();
            form.ShowDialog();
        }
    }
}