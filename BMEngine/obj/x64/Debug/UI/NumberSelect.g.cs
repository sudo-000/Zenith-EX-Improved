﻿#pragma checksum "..\..\..\..\UI\NumberSelect.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FFAA803A0F1D740AF1DDAB6D9E4BA4316F48EB98799AE133E4B19A4D907423FF"
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
using ZenithEngine.UI;


namespace ZenithEngine.UI {
    
    
    /// <summary>
    /// NumberSelect
    /// </summary>
    public partial class NumberSelect : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\UI\NumberSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\UI\NumberSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button downArrow;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\UI\NumberSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button upArrow;
        
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
            System.Uri resourceLocater = new System.Uri("/ZenithEngine;component/ui/numberselect.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\NumberSelect.xaml"
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
            
            #line 8 "..\..\..\..\UI\NumberSelect.xaml"
            ((ZenithEngine.UI.NumberSelect)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.UserControl_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\..\..\UI\NumberSelect.xaml"
            this.textBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBox_KeyDown);
            
            #line default
            #line hidden
            
            #line 30 "..\..\..\..\UI\NumberSelect.xaml"
            this.textBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 30 "..\..\..\..\UI\NumberSelect.xaml"
            this.textBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 30 "..\..\..\..\UI\NumberSelect.xaml"
            this.textBox.TextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_TextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.downArrow = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\UI\NumberSelect.xaml"
            this.downArrow.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.upArrow = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\..\UI\NumberSelect.xaml"
            this.upArrow.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

