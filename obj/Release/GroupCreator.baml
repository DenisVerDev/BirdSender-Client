   M S B A M L   `   `   `  ���� J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MessangerClientMessangerClient  C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  "    MessangerClient.GroupCreator   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:MessangerClient     ;controls&clr-namespace:MessangerClient.Controls     b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/    � Title$  Create group��$	��600��$	��450�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � 	Resources  �� Source�� $ Controls/Dictionary1.xaml?�%    .���� ���#6CC4C7
�������$��1q�._�]� .+��� $	��Top=�.+�%   MessangerClient.Controls.TopBar  $	��Top=�   HideMode .     CanMove .  	   TopBarHeader$	 Create group��
   
ResizeMode
 .  �� -    ��Name$ 	btnavatar��$��Center=�$��0,10,0,0q�$	��Top=�   btncircleempty#��[  $	��100��$	��100����(  "SharpVectors.Converters.SvgViewbox  $��36��$��36��   Source$ Resources/btnphoto.svg?�*   $MessangerClient.Controls.BirdTextBox  -     Name$ tbname��   	MaxLength$ 25��$	��400��$��30��   DefaultText$ 
Group name��$
��10pt(�$��	0,20,0,20q��� 
�������.+�*   $MessangerClient.Controls.ChannelList  -     Name$ 	userslist��   MultiSelect . $	��450��$	��330���� -   $ 	btncreate��  
birdbutton#��[ $��0,5,0,0q�$��Center=����#4ABFB6
�������$��Arial)�$
��12pt(�$	��122��$��42��$��Create group��