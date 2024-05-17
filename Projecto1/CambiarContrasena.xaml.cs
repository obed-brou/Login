namespace Projecto1;

public partial class CambiarContrasenaPage : ContentPage
{
    private Conexion conexion;
    public CambiarContrasenaPage()
	{
		InitializeComponent();
        conexion = new Conexion();
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        string usuario = txtUsuario.Text;
        string password = txtNuevaContrasena.Text;
        string confirmarPwd = txtConfirmarNuevaContrasena.Text;

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmarPwd))
        {
            DisplayAlert("Error", "Por favor completa todos los campos", "OK");
            return;
        }

        if (password != confirmarPwd)
        {
            DisplayAlert("Error", "La contraseña y la confirmación no coinciden", "OK");
            return;
        }

        bool CambiarContrasena = conexion.CambiarContrasena(usuario, password);
        if (CambiarContrasena)
        {
            DisplayAlert("Éxito", "Contraseña actualizada correctamente", "OK");
        }
        else
        {
            DisplayAlert("Error", "No se pudo actualizar la contraseña", "OK");
        }
    }

    private async void btnRegresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}