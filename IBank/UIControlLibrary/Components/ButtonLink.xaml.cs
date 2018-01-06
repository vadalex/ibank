using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIControlLibrary.Interfaces;

namespace UIControlLibrary.Components {
    /// <summary>
    /// Логика взаимодействия для ButtonLink.xaml
    /// </summary>
    public partial class ButtonLink : UserControl {

        public ButtonLink() {
            InitializeComponent();
        }

        public IFrame LinkFrame { get; set; }
        public UIElement LinkObj { get; set; }
        public Type LinkType { get; set; }
        public object ButtonName { get { return UILink.Content; } set { UILink.Content = value; } }

        private void UILink_Click(object sender, RoutedEventArgs e) {

            if (LinkFrame == null) {
                LinkFrame = (Application.Current.MainWindow as IFrame);
            }
            if (LinkFrame == null) throw new ArgumentException("Задайте свойство LinkFrame.");

            if (LinkObj == null)       {

                //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                // Pack://Application:,,, /Subfolder/ResourceFile.xaml 
                //+asm.GetName().Name + ";component/" +

                //Type Type = Type.GetType(LinkType.GetType().ToString());
                ////получаем конструктор
                
                //System.Reflection.ConstructorInfo ci = Type.GetConstructor(new Type[] { });
                ////вызываем конструтор
                //UIElement Obj =(UIElement) ci.Invoke(new object[] { });
                UIElement ui=(UIElement)LinkType.GetConstructor(new Type[]{}).Invoke(new object[]{});
                LinkFrame.SetFrame(ui);
            }
            else
            {
                LinkFrame.SetFrame(LinkObj);
            }
            
        }
    }
}
