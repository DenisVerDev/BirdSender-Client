   M S B A M L   `   `   `  ���� J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MessangerClientMessangerClient  C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  $    MessangerClient.SettingsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:MessangerClient     ;controls&clr-namespace:MessangerClient.Controls     b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/    � Title$  Settings��$	��280��$	��380�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources  �� Source�� $ Controls/Dictionary1.xaml?�%    .���� 
�������$��1q�._��� .+�%   MessangerClient.Controls.TopBar     TopBarHeader$ Account Info and settings��$	��Top=�   
ResizeMode .  	   HideMode	 .  
   CanMove
 .  �� $��Center=�$��Center=�$��5,15,5,0q�.+��� -    ��Name$ 	btnavatar��$	��Top=�   btncircleempty#��[  $	��100��$	��100����(  "SharpVectors.Converters.SvgViewbox  $��36��$��36��   Source$ Resources/btnphoto.svg?��� $��0,30,0,0q� �� Orientation$ 
Horizontal=�.+��� ���#B3B3B3$��Arial)�$
��10pt(�$
��Bold"�.��	Username:�� -    ��Name$ 
lbusername��$��72,0,0,0q����#B3B3B3$��Arial)�$
��10pt(�����  �� RelativeSource ��  �� AncestorType$ local:SettingsWindowL� �� Path$ UserName��� $��0,10,0,0q�$ 
Horizontal=�.+��� ���#B3B3B3$��Arial)�$
��10pt(�$
��Bold"�.��Select input device:�� -    ��Name$ inputdevices��$	��200��$	��200��$��20,0,0,0q� �� SelectedIndex$ 0������  �� $ local:SettingsWindowL�$ InputDevices�