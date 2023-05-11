using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;


namespace Root_Way.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    // Lo mismo que dije en MainWindow.axaml: 
    // ** En principio esto no hace falta ya que en avalonia por defecto ya te deja cerrar, minimizar y maximizar la app **
    
    /*private void pnlControlBar_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            //mueve la ventana
            BeginMoveDrag(e);
        }
    }
    private void pnlControlBar_PointerEnter(object sender, PointerEventArgs e)
    {
        // obtiene la altura de la pantalla principal
        this.MaxHeight = Screens.Primary.WorkingArea.Height;
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        //cierra la ventana actual
        this.Close();
    }
    
    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
    private void btnMaximize_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Normal)
            this.WindowState = WindowState.Maximized;
        else this.WindowState = WindowState.Normal;
    }*/
}