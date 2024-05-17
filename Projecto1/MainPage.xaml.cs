namespace Projecto1
{
    public partial class MainPage : ContentPage
    {
        private Conexion conexion;

        public MainPage()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            

            string usuario = eUser.Text;
            string contraseña = ePassword.Text;

            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contraseña))
            {
                try
                {
                    if (conexion.EsInicioSesionValido(usuario, contraseña))
                    {
                        await DisplayAlert("Inicio de Sesión", "Inicio de sesión exitoso", "Aceptar");
                        // Aquí puedes navegar a la página principal o realizar otras acciones
                        await Navigation.PushAsync(new menu());
                    }
                    else
                    {
                        await DisplayAlert("Inicio de Sesión", "Usuario o contraseña incorrectos", "Aceptar");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Ocurrió un error al iniciar sesión: " + ex.Message, "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Inicio de Sesión", "Por favor ingrese usuario y contraseña", "Aceptar");
            }
        }


        private async void btnNewUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoUsuario());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnOlvideContrasena_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CambiarContrasenaPage());
        }
    }

}
