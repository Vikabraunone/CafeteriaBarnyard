using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.BusinessLogics;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    // переделать
    public partial class FormOrders : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly OrderLogic logic;

        private readonly IOrderLogic orderLogic;

        public FormOrders(OrderLogic logic, IOrderLogic orderLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.orderLogic = orderLogic;
            dataGridView.Columns.Add("Id", "№ заказа");
            dataGridView.Columns.Add("ClientId", "Id клиента");
            dataGridView.Columns.Add("ClientFIO", "Сотрудник");
            dataGridView.Columns.Add("DateCreate", "Дата создания");
            dataGridView.Columns.Add("Dish", "Блюдо");
            dataGridView.Columns.Add("DishPrice", "Цена блюда");
            dataGridView.Columns.Add("Status", "Статус");
            dataGridView.Columns.Add("OrderSum", "Сумма заказа");

            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[2].Width = 200;
            dataGridView.Columns[3].Width = 150;
            dataGridView.Columns[4].Width = 150;
            dataGridView.Columns[5].Width = 100;
            dataGridView.Columns[6].Width = 70;
            dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView.Rows.Clear();
                var orderList = orderLogic.Read(null);
                if (orderList != null)
                {
                    foreach (var order in orderList)
                    {
                        foreach (var dish in order.OrderDishes)
                        {
                            dataGridView.Rows.Add(new object[]  { order.Id, order.ClientId, order.ClientFIO, order.DateCreate,
                                dish.Value.Item1, dish.Value.Item2, order.Status, order.OrderSum });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                logic.CreateOrder(new CreateOrderBindingModel { ClientId = Program.Client.Id });
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                int idDish = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[4].Value);
                try
                {
                    logic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.FinishOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOrderRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
