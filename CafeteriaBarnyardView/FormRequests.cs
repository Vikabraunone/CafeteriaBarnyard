using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.BusinessLogics;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormRequests : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IRequestLogic requestLogic;

        private readonly ReportLogic reportLogic;

        private int? id;

        private Dictionary<int, (string, double)> requestProducts;

        public FormRequests(IRequestLogic requestLogic, ReportLogic reportLogic)
        {
            InitializeComponent();
            this.requestLogic = requestLogic;
            this.reportLogic = reportLogic;
            dataGridView.Columns.Add("Id", "Id");
            dataGridView.Columns.Add("DishName", "Название продукта");
            dataGridView.Columns.Add("ProductName", "Количество/вес");
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Width = 150;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormRequestProducts_Load(object sender, EventArgs e)
        {
            requestProducts = new Dictionary<int, (string, double)>();
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Rows.Clear();
                foreach (var rp in requestProducts)
                    dataGridView.Rows.Add(new object[] { rp.Key, rp.Value.Item1, rp.Value.Item2 });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRequest>();
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormRequest>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        requestProducts.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
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
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    var form = Container.Resolve<FormInfoMail>();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.IsWord)
                        {
                            reportLogic.SendRequestToWord(new ReportRequestBindingModel
                            {
                                ClientId = Program.Client.Id.Value,
                                AdminId = form.AdminId,
                                Request = requestProducts
                            });
                        }
                        else
                        {
                            reportLogic.SendRequestToExcelFile(new ReportRequestBindingModel
                            {
                                ClientId = Program.Client.Id.Value,
                                AdminId = form.AdminId,
                                Request = requestProducts
                            });
                        }
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