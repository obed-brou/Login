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
        string contraseña = entryContraseña.Text;
        string repetirContraseña = entryRepetirContraseña.Text;

        // Validar que todas las contraseñas sean iguales
        if (contraseña != repetirContraseña)
        {
            DisplayAlert("Error", "La contraseña y la confirmación no coinciden", "OK");
            return;
        }

        // Validar que todos los campos obligatorios estén completos
        if (string.IsNullOrWhiteSpace(nombreCompleto) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseña))
        {
            DisplayAlert("Error", "Por favor completa todos los campos obligatorios", "OK");
            return;
        }

        // Validar el formato del correo electrónico
        if (!IsValidEmail(email))
        {
            DisplayAlert("Error", "Por favor ingresa un correo electrónico válido", "OK");
            return;
        }

        // Validar la longitud de la contraseña
        if (contraseña.Length < 6)
        {
            DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
            return;
        }

        // Obtener el género seleccionado
        string genero = chkMale.IsChecked ? "M" : "F";

        // Obtener la fecha de nacimiento
        DateTime fechaNacimiento = datePickerFechaNacimiento.Date;

        // Llamar al método CrearUsuario de la clase Conexion para guardar el usuario
        bool usuarioCreado = conexion.CrearUsuario(nombreCompleto, telefono, email, genero, fechaNacimiento, contraseña);

        // Mostrar un mensaje de éxito o error según el resultado
        if (usuarioCreado)
        {
            DisplayAlert("Éxito", "Usuario creado exitosamente", "OK");
        }
        else
        {
            DisplayAlert("Error", "No se pudo crear el usuario. El correo electrónico ya está registrado.", "OK");
        }

        // Función para validar el formato del correo electrónico
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