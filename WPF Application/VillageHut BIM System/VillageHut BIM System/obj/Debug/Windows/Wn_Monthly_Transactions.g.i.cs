﻿#pragma checksum "..\..\..\Windows\Wn_Monthly_Transactions.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57DC848DFDCCA3E9311391CEF68F5B298ABDD5E3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using VillageHut_BIM_System.Windows;


namespace VillageHut_BIM_System.Windows {
    
    
    /// <summary>
    /// Wn_Monthly_Transactions
    /// </summary>
    public partial class Wn_Monthly_Transactions : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock btnMinimize;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock btnClose;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTransactions;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl iCTransDetails;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl iCTransCartDetails;
        
        #line default
        #line hidden
        
        
        #line 265 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 312 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearchMonthlyTrans;
        
        #line default
        #line hidden
        
        
        #line 314 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboYear;
        
        #line default
        #line hidden
        
        
        #line 316 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboMonth;
        
        #line default
        #line hidden
        
        
        #line 320 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblNoOfRecords;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/VillageHut BIM System;component/windows/wn_monthly_transactions.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 40 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnMinimize = ((System.Windows.Controls.TextBlock)(target));
            
            #line 45 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.btnMinimize.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.btnMinimize_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClose = ((System.Windows.Controls.TextBlock)(target));
            
            #line 46 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.btnClose.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.btnClose_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dgTransactions = ((System.Windows.Controls.DataGrid)(target));
            
            #line 53 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.dgTransactions.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dgTransactions_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.iCTransDetails = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 6:
            this.iCTransCartDetails = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 7:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 265 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtSearchMonthlyTrans = ((System.Windows.Controls.TextBox)(target));
            
            #line 312 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.txtSearchMonthlyTrans.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtSearchMonthlyTrans_KeyUp);
            
            #line default
            #line hidden
            
            #line 312 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.txtSearchMonthlyTrans.LostFocus += new System.Windows.RoutedEventHandler(this.txtSearchMonthlyTrans_LostFocus);
            
            #line default
            #line hidden
            
            #line 312 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.txtSearchMonthlyTrans.GotFocus += new System.Windows.RoutedEventHandler(this.txtSearchMonthlyTrans_GotFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.comboYear = ((System.Windows.Controls.ComboBox)(target));
            
            #line 314 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.comboYear.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboYear_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.comboMonth = ((System.Windows.Controls.ComboBox)(target));
            
            #line 316 "..\..\..\Windows\Wn_Monthly_Transactions.xaml"
            this.comboMonth.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboMonth_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.lblNoOfRecords = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

