﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace STLink_Flash
{
    public partial class SmplUsngForm : Form
    {
        public UserControl_UI control = new UserControl_UI(); //;//实例化一个对象
        public SmplUsngForm()
        {
            InitializeComponent();
            AddUsrControl(pModuel);          
        }

        public void AddUsrControl(Panel p)
        {           
            control.Dock = DockStyle.Fill;
            control.BackColor = Color.White;
            control.BorderStyle = BorderStyle.FixedSingle;
            p.Controls.Add(control);//向controls集合（Panel）增加一个控件时，它会立即出现在窗体上 
        }
    }
}