﻿using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Graph.Views
{
    /// <summary>
    /// Логика взаимодействия для CheckAccuracyWindow.xaml
    /// </summary>
    public partial class CheckAccuracyWindow : Window
    {
        VM.CheckAccuracyVM data;
        public DataTable Table;
        public CheckAccuracyWindow()
        {
            InitializeComponent();
        }

        public CheckAccuracyWindow(double[] InputX, double[] InputY, Func<double, double>[] functions, string[] functionNames)
        {
            InitializeComponent();
            data = new VM.CheckAccuracyVM(InputX, InputY, functions, functionNames);
            Table = data.Table;
        }
    }
}