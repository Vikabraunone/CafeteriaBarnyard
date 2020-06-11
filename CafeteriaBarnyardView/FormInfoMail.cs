using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormInfoMail : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int AdminId { get; private set; }

        public bool IsWord { get; private set; }

        public FormInfoMail(IClientLogic logic)
        {
            InitializeComponent();
            textBox.Text = "Продуктов недостаточно на складе в указанном количестве. " +
                "Укажите адрес электронной почты, куда отправится информация о Вашем запросе";
            List<ClientViewModel> list = logic.ReadAdmins();
            if (list != null)
            {
                comboBox.DisplayMember = "Email";
                comboBox.ValueMember = "Id";
                comboBox.DataSource = list;
                comboBox.SelectedItem = null;
            }
        }

        private void buttonSendToWord_Click(object sender, EventArgs e)
        {
            IsWord = true;
            AdminId = (int)comboBox.SelectedValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonSendToExcelFile_Click(object sender, EventArgs e)
        {
            IsWord = false;
            AdminId = (int)comboBox.SelectedValue;
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