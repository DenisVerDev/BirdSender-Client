   M S B A M L   `   `   `  ����J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MessangerClientMessangerClient  5   	   C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  5   	   $    MessangerClient.SettingsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MessangerClient     5   	   ;controls&clr-namespace:MessangerClient.Controls     5   	   b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/  5   	     � Title$  Settings��5
   	   $	��280��6   $	��380��6'    � WindowStartupLocation$ CenterScreen=�63    � WindowStyle$
 None=�6X    � 
ResizeMode$ NoResize=�6k    � ShowInTaskbar .   � 	Resources 5       �� Source�� 5   
   $ Controls/Dictionary1.xaml?�6   %    6B   .��5      �� 
�������$��1q�6%   ._�5   
   �� .+�5   
   %   MessangerClient.Controls.TopBar     TopBarHeader$ Account Info and settings��6   $	��Top=�6D      
ResizeMode .  	   HideMode	 .  
   CanMove
 .  6�   �� 5   
   $��Center=�6   $��Center=�60   $��5,15,5,0q�6M   .+�5      �� -   5         ��Name$ 	btnavatar��5      $	��Top=�6)      btncircleempty#��[  6A   $	��100��6l   $	��100��6x   ��5      (  "SharpVectors.Converters.SvgViewbox  5      $��36��6*   $��36��65      Source$ Resources/btnphoto.svg?�6A   6c   5      �� 5      $��0,30,0,0q�6    �� Orientation$ 
Horizontal=�6+   .+�5      �� ���#B3B3B3$��Arial)�6.   $
��10pt(�6A   $
��Bold"�6Q   .��6c   	Username:6o   �� 5      -   5         ��Name$ 
lbusername��5      $��72,0,0,0q�6,   ���#B3B3B3$��Arial)�6S   $
��10pt(�6f   ��6v   ��  �� RelativeSource ��  �� AncestorType$ local:SettingsWindowL� �� Path$ UserName�6�   5      �� 5      $��0,10,0,0q�6   $ 
Horizontal=�6+   .+�5      �� ���#B3B3B3$��Arial)�62   $
��10pt(�6E   $
��Bold"�6U   .��6g   Select input device:6}   �� 5      -   5         ��Name$ inputdevices��5   L   $	��200��6   $	��200��6.   $��20,0,0,0q�6:    �� SelectedIndex$ 0��6�   ��6b   ��  �� $ local:SettingsWindowL�$ InputDevices�6  5      5      5       5!      5"      