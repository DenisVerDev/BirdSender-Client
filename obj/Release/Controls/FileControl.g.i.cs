﻿#pragma checksum "..\..\..\Controls\FileControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5C49DB7BB83EE98C0CE6BEE3D466A441EBFC1C15B6716AD605A37DB1CC101983"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MessangerClient.Controls;
using SharpVectors.Converters;
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


namespace MessangerClient.Controls {
    
    
    /// <summary>
    /// FileControl
    /// </summary>
    public partial class FileControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Controls\FileControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border body;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Controls\FileControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbname;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Controls\FileControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnact;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Controls\FileControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar pbinfo;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Controls\FileControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbfileinfo;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Controls\FileControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbdate;
        
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
            System.Uri resourceLocater = new System.Uri("/MessangerClient;component/controls/filecontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\FileControl.xaml"
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
            this.body = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.lbname = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnact = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Controls\FileControl.xaml"
            this.btnact.Click += new System.Windows.RoutedEventHandler(this.btnact_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.pbinfo = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 5:
            this.tbfileinfo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lbdate = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

