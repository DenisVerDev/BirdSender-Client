﻿#pragma checksum "..\..\..\Controls\ChannelControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "44D9D6F67760DD6FDF7069595A5B1E18D7396F6CB51E39B264C154336032320E"
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
    /// ChannelControl
    /// </summary>
    public partial class ChannelControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel body;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse avatar;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel channelinfo;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbname;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblast;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border val;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Controls\ChannelControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbval;
        
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
            System.Uri resourceLocater = new System.Uri("/MessangerClient;component/controls/channelcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\ChannelControl.xaml"
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
            this.body = ((System.Windows.Controls.DockPanel)(target));
            
            #line 9 "..\..\..\Controls\ChannelControl.xaml"
            this.body.MouseEnter += new System.Windows.Input.MouseEventHandler(this.body_MouseEnter);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\Controls\ChannelControl.xaml"
            this.body.MouseLeave += new System.Windows.Input.MouseEventHandler(this.body_MouseLeave);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\Controls\ChannelControl.xaml"
            this.body.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.body_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.avatar = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.channelinfo = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.lbname = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblast = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.val = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.lbval = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
