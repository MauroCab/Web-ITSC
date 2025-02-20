using System.ComponentModel;

namespace Web_ITSC_Client.Servicios
{
    public class ServicioRol : INotifyPropertyChanged
{
    private string _rolSeleccionado = string.Empty;

    public event PropertyChangedEventHandler PropertyChanged;

    // Método para obtener el rol actual
    public string ObtenerRol()
    {
        return _rolSeleccionado;
    }

    // Método para actualizar el rol y notificar a los suscriptores
    public void SeleccionarRol(string rol)
    {
        if (_rolSeleccionado != rol)
        {
            _rolSeleccionado = rol;
            OnPropertyChanged(nameof(ObtenerRol));  // Notificar el cambio de rol
        }
    }

    // Método para notificar que la propiedad ha cambiado
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
}
