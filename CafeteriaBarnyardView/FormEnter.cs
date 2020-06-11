using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormEnter : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClientLogic logic;

        public FormEnter(IClientLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEmail.Text) && !string.IsNullOrEmpty(textBoxPassword.Text))
            {
                try
                {
                    var client = logic.Read(new ClientBindingModel { Email = textBoxEmail.Text, Password = textBoxPassword.Text });
                    if (client.Count == 0)
                    {
                        MessageBox.Show("Неверный логин/пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Program.Client = client[0];
                    this.Visible = false;
                    var form = Container.Resolve<FormMain>();
                    form.ShowDialog();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormRegister>();
            form.ShowDialog();
        }
    }
}
