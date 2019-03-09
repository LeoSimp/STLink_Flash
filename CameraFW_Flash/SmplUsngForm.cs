using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraFW_Flash
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SmplUsngForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public UserControl_UI UIInstance = new UserControl_UI(); //;//实例化一个对象

        /// <summary>
        /// 
        /// </summary>
        public SmplUsngForm()
        {
            InitializeComponent();
            AddUsrControl(pModuel);          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void AddUsrControl(Panel p)
        {
            UIInstance.Dock = DockStyle.Fill;
            UIInstance.BackColor = Color.White;
            UIInstance.BorderStyle = BorderStyle.FixedSingle;
            p.Controls.Add(UIInstance);//向controls集合（Panel）增加一个控件时，它会立即出现在窗体上 
        }
    }
}
