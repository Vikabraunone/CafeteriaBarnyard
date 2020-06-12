using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.BusinessLogics;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    /// <summary>
    /// Заявка на продукт
    /// </summary>
    public partial class FormRequest : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRequestLogic requestLogic;

        private readonly ReportLogic reportLogic;

        private Dictionary<int, (string, double)> requestProducts;

        public FormRequest(IRequestLogic requestLogic, ReportLogic reportLogic)
        {
            InitializeComponent();
            this.requestLogic = requestLogic;
            this.reportLogic = reportLogic;
            dataGridViewProducts.Columns.Add("Id", "Id");
            dataGridViewProducts.Columns.Add("DishName", "Название продукта");
            dataGridViewProducts.Columns.Add("ProductName", "Количество/вес");
            dataGridViewProducts.Columns[0].Visible = false;
            dataGridViewProducts.Columns[1].Width = 150;
            dataGridViewProducts.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormRequestProducts_Load(object sender, EventArgs e)
        {
            requestProducts = new Dictionary<int, (string, double)>();
        }

        /// <summary>
        /// Обновить записи продуктов в заявке
        /// </summary>
        private void LoadData()
        {
            try
            {
                dataGridViewProducts.Rows.Clear();
                foreach (var rp in requestProducts)
                    dataGridViewProducts.Rows.Add(new object[] { rp.Key, rp.Value.Item1, rp.Value.Item2 });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавить продукт в заявку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormProductRequest>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (requestProducts.ContainsKey(form.Id))
                    requestProducts[form.Id] = (form.NameProduct, form.Weight);
                else
                    requestProducts.Add(form.Id, (form.NameProduct, form.Weight));
                LoadData();
            }
            else
                MessageBox.Show("Выберите строку с продуктом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Изменить запись продукта в заявке 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormProductRequest>();
                int id = Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Weight = requestProducts[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    requestProducts[form.Id] = (form.NameProduct, form.Weight);
                    LoadData();
                }
            }
            else
                MessageBox.Show("Выберите строку с продуктом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Убрать продукт из заявки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        requestProducts.Remove(Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells[0].Value));
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

        /// <summary>
        /// Обновить записи о продуктах в заявке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool succesfulCreation = requestLogic.Create(new RequestBindingModel
                {
                    ClientId = Program.Client.Id,
                    RequestProducts = requestProducts,
                    DateRequest = DateTime.Now
                });
                if (succesfulCreation)
                {
                    MessageBox.Show("Продукты успешно списаны со склада", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    var form = Container.Resolve<FormInfoMail>();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.IsWord)
                            reportLogic.SendRequestToWord(new ReportRequestBindingModel
                            {
                                ClientId = Program.Client.Id.Value,
                                AdminId = form.AdminId,
                                Request = requestProducts
                            });
                        else
                            reportLogic.SendRequestToExcelFile(new ReportRequestBindingModel
                            {
                                ClientId = Program.Client.Id.Value,
                                AdminId = form.AdminId,
                                Request = requestProducts
                            });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Недостаточно продукта на складе для списания"))
                {
                    MessageBox.Show(ex.Message + " . Вам нужно выбрать ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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