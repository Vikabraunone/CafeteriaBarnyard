using CafeteriaBarnyardBisinessLogic.BindingModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;

namespace CafeteriaBarnyardView
{
    public partial class FormRegister : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClientLogic logic;

        public FormRegister(IClientLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            if (Program.Client != null)
            {
                ClientViewModel client = logic.Read(new ClientBindingModel { Id = Program.Client.Id })[0];
                textBoxEmail.Text = client.Email;
                textBoxPassword.Text = client.Password;
                textBoxClientFIO.Text = client.ClientFIO;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEmail.Text) && !string.IsNullOrEmpty(textBoxPassword.Text) && !string.IsNullOrEmpty(textBoxClientFIO.Text))
            {
                try
                {
                    if (Program.Client == null)
                        logic.CreateOrUpdate(new ClientBindingModel
                        {
                            Email = textBoxEmail.Text,
                            Password = textBoxPassword.Text,
                            ClientFIO = textBoxClientFIO.Text,
                        });
                    else
                        logic.CreateOrUpdate(new ClientBindingModel
                        {
                            Email = textBoxEmail.Text,
                            Password = textBoxPassword.Text,
                            ClientFIO = textBoxClientFIO.Text,
                            Id = Program.Client.Id,
                            IsAdmin = Program.Client.IsAdmin
                        });
                    MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин, пароль и ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}