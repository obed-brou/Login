namespace Projecto1;

public partial class NuevoUsuario : ContentPage
{
    private Conexion conexion;
    public NuevoUsuario()
	{
		InitializeComponent();
        conexion = new Conexion();
    }
    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        string nombreCompleto = entryNombreCompleto.Text;
        string telefono = entryTelefono.Text;
        string email = entryEmail.Text;
        string contrase�a = entryContrase�a.Text;
        string repetirContrase�a = entryRepetirContrase�a.Text;

        // Validar que todas las contrase�as sean iguales
        if (contrase�a != repetirContrase�a)
        {
            DisplayAlert("Error", "La contrase�a y la confirmaci�n no coinciden", "OK");
            return;
        }

        // Validar que todos los campos obligatorios est�n completos
        if (string.IsNullOrWhiteSpace(nombreCompleto) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contrase�a))
        {
            DisplayAlert("Error", "Por favor completa todos los campos obligatorios", "OK");
            return;
        }

        // Validar el formato del correo electr�nico
        if (!IsValidEmail(email))
        {
            DisplayAlert("Error", "Por favor ingresa un correo electr�nico v�lido", "OK");
            return;
        }

        // Validar la longitud de la contrase�a
        if (contrase�a.Length < 6)
        {
            DisplayAlert("Error", "La contrase�a debe tener al menos 6 caracteres", "OK");
            return;
        }

        // Obtener el g�nero seleccionado
        string genero = chkMale.IsChecked ? "M" : "F";

        // Obtener la fecha de nacimiento
        DateTime fechaNacimiento = datePickerFechaNacimiento.Date;

        // Llamar al m�todo CrearUsuario de la clase Conexion para guardar el usuario
        bool usuarioCreado = conexion.CrearUsuario(nombreCompleto, telefono, email, genero, fechaNacimiento, contrase�a);

        // Mostrar un mensaje de �xito o error seg�n el resultado
        if (usuarioCreado)
        {
            DisplayAlert("�xito", "Usuario creado exitosamente", "OK");
        }
        else
        {
            DisplayAlert("Error", "No se pudo crear el usuario. El correo electr�nico ya est� registrado.", "OK");
        }

        // Funci�n para validar el formato del correo electr�nico
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    private async void btnRegresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}