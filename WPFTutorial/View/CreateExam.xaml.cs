﻿using System;
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
using WPFTutorial.ViewModel;

namespace WPFTutorial.View
{
    /// <summary>
    /// Interaction logic for CreateExam.xaml
    /// </summary>
    public partial class CreateExam : UserControl
    {
        public CreateExam()
        {
            InitializeComponent();
            CreateExamViewModel createExamViewModel = new CreateExamViewModel();
            this.DataContext = createExamViewModel;
        }
    }
}